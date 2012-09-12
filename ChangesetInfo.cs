using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.TeamFoundation.VersionControl.Client;
using TfsCommitMonitor.Config;
using TfsCommitMonitor.SourceControl;

namespace TfsCommitMonitor
{
    public partial class ChangesetInfo : Form
    {
        #region Private members

        private readonly TfsCheckin _changeset;
        private readonly SourceControlProject _project;

        #endregion  

        #region Constructors

        public ChangesetInfo(SourceControlProject project, TfsCheckin change)
        {
            InitializeComponent();
      
            _changeset = change;
            _project = project;

            if (_changeset == null)
                return;

            var lviLoading = new ListViewItem("Loading...");
            lviLoading.SubItems.Add("Chargement en cours...");
            listView1.Items.Add(lviLoading);

            var x = new Task(() =>
            {
                var changes = project.GetChangesForChangeset(change);

                BeginInvoke(new MethodInvoker(() => listView1.Items.Remove(lviLoading)));

                changes.ToList().ForEach(c =>
                {
                    if (c == null)
                        return;

                    var lvi = new ListViewItem(c.ChangeType.ToString());

                    string name = c.Item.ServerItem;
                    foreach (MonitoredProjectItemConfigurationElement folder in project.ServerConfiguration.Folders)
                        name = name.Replace(folder.MonitoredFolder, "");

                    lvi.SubItems.Add(name);
                    lvi.Tag = c;

                    BeginInvoke(new MethodInvoker(() => listView1.Items.Add(lvi)));
                });
            });            

            lblChangeset.Text = _changeset.ChangesetId.ToString(CultureInfo.InvariantCulture);
            lblUser.Text = _changeset.Committer;
            txtComments.Text = _changeset.Comment;

            x.Start();
        }

        #endregion

        #region Form events

        private void DiffWithPreviousVersionToolStripMenuItemClick(object sender, EventArgs e)
        {
            var item = (Change)listView1.SelectedItems[0].Tag;

            if (!_project.DiffWithPreviousVersion(item, item.Item.ChangesetId))
            {
                MessageBox.Show("Cannot perform diff operation with previous version");
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void viewLatestVersionInVisualStudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (Change)listView1.SelectedItems[0].Tag;
            _project.ViewFile(item.Item);
        }
    }
}
