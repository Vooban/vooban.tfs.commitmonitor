using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsCommitMonitor.Config;
using System.Linq;

namespace TfsCommitMonitor.SourceControl
{
    public class SourceControlProject : IDisposable
    {
        #region Private members

        private readonly TfsCommitMonitorConfigurationSection _configuration;
        private readonly ServerConfigurationElement _tfsServer;
        private readonly StringDictionary _monitoredItems = new StringDictionary();
        private readonly VersionControlServer _versionControlServer;
        private readonly TfsTeamProjectCollection _teamProjectCollection;
        private readonly object _lock = new object();

        #endregion

        #region Properties

        public ServerConfigurationElement ServerConfiguration
        {
            get
            {
                return _tfsServer;
            }
        }

        #endregion

        #region Constructors

        /// <summary>Initialise une nouvelle instance de la classe <see cref="SourceControlProject"/></summary>
        /// <param name="configuration">The application configuration information.</param>
        /// <param name="projectId">The project id used to define which configuration will be used.</param>
        public SourceControlProject(TfsCommitMonitorConfigurationSection configuration, string projectId)
        {
            _configuration = configuration;
            _tfsServer = _configuration.Servers[projectId];

            _teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(ServerConfiguration.TfsTeamProjectCollection));
            _versionControlServer = _teamProjectCollection.GetService<VersionControlServer>();

            foreach (MonitoredProjectItemConfigurationElement item in _tfsServer.Folders)
                _monitoredItems.Add(item.ItemId, item.MonitoredFolder);                        
        }

        #endregion

        public TfsCheckin GetChangeset(int changesetId)
        {
            TfsCheckin result = null;

            foreach (string folderId in _monitoredItems)
            {
                lock (_lock)
                {
                    var change = _versionControlServer.GetChangeset(changesetId, false, false);

                    if (change != null)
                        result = new TfsCheckin(change, ServerConfiguration.Id, ServerConfiguration.TfsTeamProjectCollection, folderId, _monitoredItems[folderId]);
                }
            }

            return result;
        }

        public IEnumerable<TfsCheckin> QueryChangesetHistory(string wildcard)
        {
            var changesets = new List<TfsCheckin>();

            lock (_lock)
            {
                foreach (string folderId in _monitoredItems.Keys)
                {
                    var changesetsFolder = _versionControlServer.QueryHistory(string.Format("{0}/{1}", _monitoredItems[folderId], wildcard), VersionSpec.Latest, 0, RecursionType.Full, null, new ChangesetVersionSpec(1), VersionSpec.Latest, int.MaxValue, true, false);

                    foreach (Changeset c in changesetsFolder)
                        changesets.Add(new TfsCheckin(c, ServerConfiguration.Id, ServerConfiguration.TfsTeamProjectCollection, folderId, _monitoredItems[folderId]));
                }
            }

            return changesets;
        }

        public IEnumerable<TfsCheckin> QueryChangesetHistory(bool ignoreMaximalChangesetRetrieval)
        {
            var changesets = new List<TfsCheckin>();

            lock (_lock)
            {
                foreach (string folderId in _monitoredItems.Keys)
                {
                    try
                    {
                        IEnumerable changesetsFolder;
                        if (!ignoreMaximalChangesetRetrieval)
                            changesetsFolder = _versionControlServer.QueryHistory(_monitoredItems[folderId], VersionSpec.Latest, 0, RecursionType.Full, null, null, null, _tfsServer.ChangesetRetrieved, false, false);
                        else
                            changesetsFolder = _versionControlServer.QueryHistory(_monitoredItems[folderId], VersionSpec.Latest, 0, RecursionType.Full, null, null, null, int.MaxValue, true, false);

                        foreach (Changeset c in changesetsFolder)
                            changesets.Add(new TfsCheckin(c, ServerConfiguration.Id, ServerConfiguration.TfsTeamProjectCollection, folderId, _monitoredItems[folderId]) { Changes = c.Changes });
                    }
                    catch (ItemNotFoundException itemNotFound)
                    {
                        MessageBox.Show(itemNotFound.Message);
                    }
                }
            }

            return changesets;
        }

        public IEnumerable<TfsCheckin> QueryChangesetHistory(DateTime? from, DateTime? to)
        {
            var fromSpec = from.HasValue ? VersionSpec.ParseSingleSpec("D" + from, null) : VersionSpec.ParseSingleSpec("D" + DateTime.Today.ToShortDateString() + " 00:00:01", null);
            var toSpec = to.HasValue ? VersionSpec.ParseSingleSpec("D" + to, null) : VersionSpec.ParseSingleSpec("D" + DateTime.Now, null);
            var changesets = new List<TfsCheckin>();

            lock (_lock)
            {
                foreach (string folderId in _monitoredItems.Keys)
                {
                    var changesetsFolder = _versionControlServer.QueryHistory(_monitoredItems[folderId], VersionSpec.Latest, 0, RecursionType.Full, null, fromSpec, toSpec, int.MaxValue, false, false);

                    foreach (Changeset c in changesetsFolder)
                        changesets.Add(new TfsCheckin(c, ServerConfiguration.Id, ServerConfiguration.TfsTeamProjectCollection, folderId, _monitoredItems[folderId]));
                }
            }

            return changesets;
        }

        public Change[] GetChangesForChangeset(TfsCheckin changeset)
        {
            lock (_lock)
            {
                return _versionControlServer.GetChangesForChangeset(changeset.ChangesetId, true, 20, null);
            }
        }

        public Item GetItem(int itemId, int changesetId)
        {
            lock (_lock)
            {
                return _versionControlServer.GetItem(itemId, changesetId, GetItemsOptions.Download);
            }
        }

        public bool DiffWithPreviousVersion(Change theChange, int changesetId)
        {
            var changeId = (theChange.Item.DeletionId != 0) ?  theChange.Item.ChangesetId - 1 : theChange.Item.ChangesetId;
            var version = new  ChangesetVersionSpec(changeId);
            var versionFrom = new ChangesetVersionSpec(1);
            var changesets = _versionControlServer.QueryHistory(theChange.Item.ServerItem, version, 0, RecursionType.None, null, versionFrom, VersionSpec.Latest, int.MaxValue, true, false, true);
            var historyChangeset = changesets.Cast<Changeset>().Skip(1).FirstOrDefault();

            if (historyChangeset != null)
            {
                var previousItem = historyChangeset.Changes.Single(c => c.Item.ServerItem == theChange.Item.ServerItem).Item;

                return DiffFiles(theChange.Item, previousItem);
            }

            return false;
        }
        
        private bool DiffFiles(Item item1, Item item2)
        {
            if (item1 == null || item2 == null || item1.ItemType != ItemType.File)
                return false;

            lock (_lock)
            {
                Difference.VisualDiffItems(_versionControlServer,
                                        Difference.CreateTargetDiffItem(_versionControlServer, item2.ServerItem, new ChangesetVersionSpec(item2.ChangesetId), 0, new ChangesetVersionSpec(item2.ChangesetId)),
                                        Difference.CreateTargetDiffItem(_versionControlServer, item1.ServerItem, new ChangesetVersionSpec(item1.ChangesetId), 0, new ChangesetVersionSpec(item1.ChangesetId)));
            }

            return true;
        }

        public void ViewFile(Item theItem)
        {
            if (theItem != null && string.IsNullOrEmpty(theItem.ServerItem))
                return;

            var filename = Path.Combine(Path.GetTempPath(), Path.GetFileName(theItem.ServerItem));

            try
            {
                theItem.DownloadFile(filename);

                Process.Start(filename);
            }
            finally
            {
                File.Delete(filename);
            }
        }

        public void Dispose()
        {
            _teamProjectCollection.Dispose();
        }
    }
}
