using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsCommitMonitor.Config;
using TfsCommitMonitor.SourceControl;

namespace TfsCommitMonitor
{
    public partial class MainForm : Form
    {
        #region Internal Class

        private class ChangeStatistics
        {
            public string ServerId{ get; set; }
            
            public int Checkins { get; set; }

            public int Changes { get; set; }
        }

        #endregion

        #region Private members

        private DateTime? _lastPass;
        private readonly Timer _timer = new Timer();
        private readonly Dictionary<int, TfsCheckin> _changesetIds = new Dictionary<int, TfsCheckin>();
        private readonly CommitNotifier _commitNotifier = new CommitNotifier();
        private readonly TfsCommitMonitorConfigurationSection _configuration;
        private readonly List<SourceControlProject> _projects = new List<SourceControlProject>();

        #endregion

        #region Constructors

        public MainForm(IConfigurationProvider configurationProvider)
        {
            _configuration = configurationProvider.GetConfiguration();
            foreach (ServerConfigurationElement project in _configuration.Servers)
                _projects.Add(new SourceControlProject(_configuration, project.Id));

            InitializeComponent();

            ConfigureCommitNotifier();

            ConfigureCheckUpdateTimer();
        }

        private void ConfigureCheckUpdateTimer()
        {
            _timer.Tick += TimerTick;
            _timer.Interval = _configuration.CheckIntervalInSeconds*1000;
            _timer.Start();
        }

        private void ConfigureCommitNotifier()
        {
            _commitNotifier .OpeningMilliseconds = _configuration.NotifierOpeningMilliseconds;
            _commitNotifier.HidingMilliseconds = _configuration.NotifierHidingMilliseconds;
            _commitNotifier.StayOpenMilliseconds = _configuration.NotifierStayOpenMilliseconds;
            _commitNotifier.MinWidth = 600;
            _commitNotifier.MaxWidth = 600;                                  

            _commitNotifier.Show();
            _commitNotifier.ChangesetLinkClicked += (sender, args) =>
                                                        {
                                                            var selectedProject =
                                                                _projects.Single(p => p.ServerConfiguration.Id == args.ProjectId);

                                                            using (
                                                                var ci = new ChangesetInfo(selectedProject,
                                                                                           args.CurrentChangeset))
                                                                ci.ShowDialog();
                                                        };
        }

        #endregion

        #region Form events

        void TimerTick(object sender, EventArgs e)
        {
            var x = new Task(CheckForUpdates);

            x.Start();  
        }

        void OptionsLoad(object sender, EventArgs e)
        {
            var x = new Task(() => LoadAll(true));

            x.Start();            
        }

        private void QuitterToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OptionsFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e == null || e.CloseReason != CloseReason.UserClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private void ListView1MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e == null)
                return;

            TopMost = false;
            var selectedItem = listView1.GetItemAt(e.X, e.Y);
            var checkin = (TfsCheckin)selectedItem.Tag;
            selectedItem.Font = new Font(Font, FontStyle.Regular);

            using (var ci = new ChangesetInfo(_projects.Single(p => p.ServerConfiguration.Id == checkin.ServerId), checkin))
            {
                Hide();
                ci.ShowDialog();
                if(!IsDisposed)
                    Show();
            }
        }

        private void ToolStripAllReadClick(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
                lvi.Font = new Font(Font, FontStyle.Regular);
        }

        private void TrayIconClick(object sender, EventArgs e)
        {
            if (e == null)
                return;

            if (((MouseEventArgs) e).Button != MouseButtons.Left) 
                return;

            if (!Visible || WindowState == FormWindowState.Minimized)
            {
                _commitNotifier.Hide();
                Show();
                WindowState = FormWindowState.Normal;
            }

            TopMost = true;
            Focus();
            BringToFront();
            TopMost = false;
        }

        private void ToolStripStatisquesClick(object sender, EventArgs e)
        {
            var x = new Task(CalculateStatistics);

            x.Start();
        }

        private void TstxtSearchKeyUp(object sender, KeyEventArgs e)
        {
            var x = new Task(() => PerformSearch(e));

            x.Start();
        }

        private void ShowChangesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_commitNotifier.Visibility != System.Windows.Visibility.Visible)
            {
                _commitNotifier.Show();

                if (_commitNotifier.NotifyContent.Count > 0)
                    _commitNotifier.Notify();
            }
            else
                _commitNotifier.Hide();
        }

        private void tsSearch_Click(object sender, EventArgs e)
        {
            var x = new Task(() => PerformSearch(new KeyEventArgs(Keys.Enter)));

            x.Start();
        }

        #endregion

        #region Private methods

        private void PerformSearch(KeyEventArgs e)
        {
            if (e == null)
                return;

            if (e.KeyData != Keys.Enter) 
                return;

            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    tsSearchProgress.Maximum = 40 + _projects.Count * 10;
                    tsSearchProgress.Step = 10;
                    tsSearchProgress.Value = 0;
                }));

                Invoke(new MethodInvoker(listView1.Items.Clear));
                Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

                if (!string.IsNullOrEmpty(tstxtSearch.Text))
                {
                    var searchedStrings = new Dictionary<TfsCheckin, List<String>>();
                    _changesetIds.Values.ToList().ForEach(v => {
                        var searched= new List<string>();

                        if(!string.IsNullOrEmpty(v.Comment))
                            searched.Add(v.Comment.ToLower());
                        if (!string.IsNullOrEmpty(v.FolderId))
                            searched.Add(v.FolderId.ToLower());
                        if (!string.IsNullOrEmpty(v.Committer))
                            searched.Add(v.Committer.ToLower());
                        if (!string.IsNullOrEmpty(v.FolderPath))
                            searched.Add(v.FolderPath.ToLower());
                        if (!string.IsNullOrEmpty(v.Owner))
                            searched.Add(v.Owner.ToLower());
                        if (!string.IsNullOrEmpty(v.ServerId))
                            searched.Add(v.ServerId.ToLower());
                        if (!string.IsNullOrEmpty(v.TeamProjectCollection))
                            searched.Add(v.TeamProjectCollection.ToLower());
                        searched.Add(v.ChangesetId.ToString(CultureInfo.InvariantCulture));

                        searchedStrings.Add(v, searched);
                    });

                    var checkins = (from c in searchedStrings.Keys
                                    where searchedStrings[c].Any(v=> Regex.IsMatch(v, tstxtSearch.Text.ToLower()))
                                    select c).ToList();

                    Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

                    if (tsIncludeFiles.Checked)
                    {
                        // On recherche dans l'history pour les nom de fichiers correspondants
                        foreach (var p in _projects)
                        {
                            checkins.AddRange(p.QueryChangesetHistory(string.Format("*{0}*", tstxtSearch.Text)));
                            Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));
                        }
                    }

                    var sortedChangesets = checkins.Distinct(new TfsCheckinEqualityComparer()).ToList();
                    Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

                    sortedChangesets.ForEach(c => AddChangeSet(c, false, true, FontStyle.Regular));
                    Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));
                }
                else
                    LoadAll(false);
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(() =>
                {
                    _commitNotifier.NotifyContent.Add(new NotifyObject(ex.Message, "Exception", 0, "Erreur", "PerformSearch", true));
                    _commitNotifier.Notify();
                }));
            }
            finally
            {
                Invoke(new MethodInvoker(() =>
                    {
                        tsSearchProgress.Value = 0;
                    }));
            }
        }

        private void CalculateStatistics()
        {
            var stats = new Dictionary<string, ChangeStatistics>();
            bool[] alternate = { false };

            Invoke(new MethodInvoker(_commitNotifier.Show));
            Invoke(new MethodInvoker(_commitNotifier.NotifyContent.Clear));

            foreach (var project in _projects)
            {
                var checkins = project.QueryChangesetHistory(true);

                foreach (var x in from TfsCheckin x in checkins where IsUserDisplayed(x) select x)
                {
                    if (!stats.ContainsKey(x.Committer))
                        stats.Add(x.Committer, new ChangeStatistics());

                    stats[x.Committer].ServerId = project.ServerConfiguration.Id;
                    stats[x.Committer].Checkins++;

                    var changes = x.Changes;
                    stats[x.Committer].Changes += changes.Count(c => c != null && c.ChangeType.IsOneFlagSet(ChangeType.Add, ChangeType.Edit)); // ((c.ChangeType & ChangeType.Add) == ChangeType.Add || (c.ChangeType & ChangeType.Edit) == ChangeType.Edit)
                }
            }

            var ordered = stats.OrderByDescending(x => x.Value.Changes).Select(x => x.Key).ToList();

            foreach (var key in ordered)
            {
                var localKey = key;

                Invoke(new MethodInvoker(() =>
                {
                    _commitNotifier.NotifyContent.Add(new NotifyObject(string.Format("{0} checkins, avec {1} changements ({2} changes per checkin)", stats[localKey].Checkins, stats[localKey].Changes, Math.Round((stats[localKey].Changes / (double)stats[localKey].Checkins), 2)), localKey, stats[localKey].Changes, stats[localKey].ServerId, "Stats", alternate[0]));
                    alternate[0] = !alternate[0];
                }));
            }

            Invoke(new MethodInvoker(_commitNotifier.Notify));
        }

        private void CheckForUpdates()
        {
            try
            {
                var itemAdded = false;
                bool[] textIconTestSet = {false};
                bool[] alternate = { false };

                foreach (var project in _projects)
                {
                    var checkins = project.QueryChangesetHistory(_lastPass, DateTime.Now);

                    foreach (var x in from TfsCheckin x in checkins where !_changesetIds.ContainsKey(x.ChangesetId) select x)
                    {
                        AddChangeSet(x, true, true);

                        var changesetInfo = x;
                        var projectSafe = project;

                        if (IsUserDisplayed(x))
                        {
                            if (!itemAdded)
                            {
                                itemAdded = true;
                                // We clear the commit notifier only if new changeset are found, otherwise the user can still view the latest changeset from previous checks
                                Invoke(new MethodInvoker(_commitNotifier.NotifyContent.Clear));
                            }

                            Invoke(new MethodInvoker(() =>
                            {
                                if (!textIconTestSet[0])
                                {
                                    var trayText = string.Format("{0} - {1}", changesetInfo.Committer, changesetInfo.Comment.Trim());
                                    trayIcon.Text = trayText.Length >= 64 ? trayText.Substring(0, 63) : trayText;
                                    textIconTestSet[0] = true;
                                }

                                _commitNotifier.NotifyContent.Add(new NotifyObject(changesetInfo.Comment, changesetInfo.Committer, changesetInfo.ChangesetId, changesetInfo.ServerId, changesetInfo.FolderId, alternate[0]) { ChangesetInfo = changesetInfo, ProjectId = projectSafe.ServerConfiguration.Id });
                                alternate[0] = !alternate[0];
                            }));
                        }
                    }
                }

                // Only popup the notifier if items were added
                if (itemAdded)
                {
                    Invoke(new MethodInvoker(_commitNotifier.Show));                
                    Invoke(new MethodInvoker(_commitNotifier.Notify));
                }
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(() =>
                {
                    _commitNotifier.NotifyContent.Add(new NotifyObject(ex.Message, "Exception", 0, "Erreur", "CheckForUpdates", true));
                    Invoke(new MethodInvoker(_commitNotifier.Show));                
                    _commitNotifier.Notify();
                }));
            }
            finally
            {
                _lastPass = DateTime.Now;
                lastCheckedOn.Text = _lastPass.Value.ToString("Le yyyy-MM-dd à HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }

        private void LoadAll(bool notify)
        {
            Invoke(new MethodInvoker(() =>
            {
                tsSearchProgress.Maximum = 40 + _projects.Count * 10;
                tsSearchProgress.Step = 10;
                tsSearchProgress.Value = 0;
            }));

            Invoke(new MethodInvoker(listView1.Items.Clear));
            Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

            var total = 0;
            bool[] alternate = { false };
            bool[] trayIconSet = {false};
            var checkins = new List<TfsCheckin>();

            foreach (var project in _projects)
            {
                checkins.AddRange(project.QueryChangesetHistory(false));
                Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));
            }

            checkins.Sort((checkin, tfsCheckin) => tfsCheckin.CreationDate.CompareTo(checkin.CreationDate));
            Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

            foreach (var x in checkins)
            {
                var tfsCheckin = x;
                var project = _projects.Single(p => p.ServerConfiguration.Id == tfsCheckin.ServerId);
                AddChangeSet(x, false, total <= project.ServerConfiguration.ChangesetDisplayed, FontStyle.Regular);

                if (!IsUserDisplayed(x))
                    continue;

                if (total <= 5)
                {
                    var changesetInfo = x;
                    Invoke(new MethodInvoker(() =>
                                                {
                                                    if (!trayIconSet[0])
                                                    {
                                                        var trayText = string.Format("{0} - {1}", changesetInfo.Committer, changesetInfo.Comment.Trim());
                                                        trayIcon.Text = trayText.Length >= 64 ? trayText.Substring(0, 63) : trayText;
                                                        trayIconSet[0] = true;
                                                    }

                                                    _commitNotifier.NotifyContent.Add(new NotifyObject(changesetInfo.Comment, changesetInfo.Committer, changesetInfo.ChangesetId, changesetInfo.ServerId, changesetInfo.FolderId, alternate[0]) { ChangesetInfo = changesetInfo, ProjectId = project.ServerConfiguration.Id });
                                                    alternate[0] = !alternate[0];
                                                }));
                }

                total++;
            }

            Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));
            
            if (notify)
            {
                Invoke(new MethodInvoker(_commitNotifier.Show));
                Invoke(new MethodInvoker(_commitNotifier.Notify));
            }

            lastCheckedOn.Text = DateTime.Now.ToString("Le yyyy-MM-dd à HH:mm:ss", CultureInfo.InvariantCulture);
            Invoke(new MethodInvoker(() => tsSearchProgress.PerformStep()));

            Invoke(new MethodInvoker(() => tsSearchProgress.Value = 0 ));
        }

        private void AddChangeSet(TfsCheckin currentChangeset, bool insert, bool addToListView, FontStyle fontVariation = FontStyle.Bold)
        {
            if (currentChangeset == null)
                return;

            if (addToListView && IsUserDisplayed(currentChangeset))
            {
                var lvi = new ListViewItem{
                    Font = new Font(Font, fontVariation),
                    ForeColor = String.Compare(currentChangeset.CommitterDomainName, String.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName), StringComparison.OrdinalIgnoreCase) == 0 ? Color.DarkSlateBlue : Color.DarkSlateGray, 
                    Text = currentChangeset.CreationDate.ToString("[yyyy-MM-dd] - HH:mm", CultureInfo.InvariantCulture), 
                    Tag = currentChangeset
                };

                lvi.SubItems.Add(currentChangeset.ChangesetId.ToString(CultureInfo.InvariantCulture));
                lvi.SubItems.Add(currentChangeset.FolderId);
                lvi.SubItems.Add(currentChangeset.Committer);
                lvi.SubItems.Add(currentChangeset.Comment);
                lvi.Tag = currentChangeset;

                BeginInvoke(new MethodInvoker(() =>
                {
                    if (insert)
                        listView1.Items.Insert(0, lvi);
                    else                    
                        listView1.Items.Add(lvi);                    
                }));
            }

            if (!_changesetIds.ContainsKey(currentChangeset.ChangesetId))
                _changesetIds.Add(currentChangeset.ChangesetId, currentChangeset);
        }

        private bool IsUserDisplayed(TfsCheckin currentChangeset)
        {
            var ignoredUsers = _configuration.IgnoredUsers.ToList();
            return ignoredUsers.All(c => !c.Id.Equals(currentChangeset.Committer, StringComparison.InvariantCultureIgnoreCase) && !c.Id.Equals(currentChangeset.CommitterDomainName, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion
    }
}
