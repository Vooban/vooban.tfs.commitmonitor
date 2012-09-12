using System;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsCommitMonitor.SourceControl;

namespace TfsCommitMonitor
{
    public class NotifyObject
    {
        #region Constructors

        public NotifyObject(string message, string title, int changeset, string projectId, string folderId, bool alternate)
        {
            Message = message;
            Title = title;
            Backcolor = alternate ? "#FFFFFF" : "#DFDFDF";
            ChangesetNumber = changeset;
            ProjectId = projectId;
            FolderId = folderId;
        }

        #endregion

        #region Public properties

        public string Title { get; set; }

        public string ProjectId { get; set; }

        public string FolderId { get; set; }

        public TfsCheckin ChangesetInfo { get; set; }

        public string Message { get; set; }

        public String Backcolor { get; set; }

        public int ChangesetNumber { get; set; }

        #endregion
    }
}
