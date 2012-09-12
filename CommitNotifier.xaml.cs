using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using TfsCommitMonitor.SourceControl;
using WPFTaskbarNotifier;

namespace TfsCommitMonitor
{
    /// <summary>
    /// Logique d'interaction pour CommitNotifier.xaml
    /// </summary>
    public partial class CommitNotifier : TaskbarNotifier
    {
        private ObservableCollection<NotifyObject> _notifyContent;

        public CommitNotifier()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A collection of NotifyObjects that the main window can add to.
        /// </summary>
        public ObservableCollection<NotifyObject> NotifyContent
        {
            get
            {
                if (_notifyContent == null)
                {
                    // Not yet created.
                    // Create it.
                    NotifyContent = new ObservableCollection<NotifyObject>();
                }

                return _notifyContent;
            }
            set
            {
                _notifyContent = value;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            Hyperlink hyperlink = sender as Hyperlink;

            if (hyperlink == null)
                return;

            // Close the current window
            ForceHidden();

            var notifyObject = hyperlink.Tag as NotifyObject;
            if (notifyObject != null && notifyObject.ChangesetInfo != null)
            {
                OnChangesetLinkClicked(notifyObject.ChangesetInfo);
            }
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.ForceHidden();
        }

        public event EventHandler<ChangesetEventArgs> ChangesetLinkClicked;

        protected ChangesetEventArgs OnChangesetLinkClicked(TfsCheckin changeset)
        {
            if (ChangesetLinkClicked != null)
            {
                var args = new ChangesetEventArgs(changeset.ServerId, changeset.ChangesetId) { CurrentChangeset = changeset };
                ChangesetLinkClicked(this, args);

                return args;
            }

            return null;
        }
    }
}
