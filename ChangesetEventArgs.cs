using System;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsCommitMonitor.SourceControl;

namespace TfsCommitMonitor
{
    public class ChangesetEventArgs : EventArgs
    {
        public int ChangesetId { get; private set; }

        public string ProjectId { get; private set; }

        public TfsCheckin CurrentChangeset { get; set; }

        public ChangesetEventArgs(string projectId, int changesetId)
        {
            ChangesetId = changesetId;
            ProjectId = projectId;
        }
    }
}
