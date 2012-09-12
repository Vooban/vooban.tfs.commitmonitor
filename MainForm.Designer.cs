namespace TfsCommitMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.projectId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commiter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lastCheckedOn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripAllRead = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatisques = new System.Windows.Forms.ToolStripButton();
            this.tstxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsIncludeFiles = new System.Windows.Forms.ToolStripButton();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "TFS Commit Monitor";
            this.trayIcon.BalloonTipTitle = "TFS Commit Monitor";
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Tray Icon";
            this.trayIcon.Visible = true;
            this.trayIcon.Click += new System.EventHandler(this.TrayIconClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showChangesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitterToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(155, 54);
            // 
            // showChangesToolStripMenuItem
            // 
            this.showChangesToolStripMenuItem.Name = "showChangesToolStripMenuItem";
            this.showChangesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showChangesToolStripMenuItem.Text = "Show changes";
            this.showChangesToolStripMenuItem.Click += new System.EventHandler(this.ShowChangesToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.quitterToolStripMenuItem.Text = "&Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.QuitterToolStripMenuItemClick);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.date,
            this.number,
            this.projectId,
            this.commiter,
            this.comments});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1222, 430);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1MouseDoubleClick);
            // 
            // date
            // 
            this.date.Text = "@";
            this.date.Width = 125;
            // 
            // number
            // 
            this.number.Text = "# Number";
            this.number.Width = 100;
            // 
            // projectId
            // 
            this.projectId.Text = "Folder name";
            this.projectId.Width = 125;
            // 
            // commiter
            // 
            this.commiter.Text = "Commiter";
            this.commiter.Width = 200;
            // 
            // comments
            // 
            this.comments.Text = "Comments";
            this.comments.Width = 575;
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lastCheckedOn});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1222, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(91, 17);
            this.toolStripStatusLabel1.Text = "Last checked on :";
            // 
            // lastCheckedOn
            // 
            this.lastCheckedOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lastCheckedOn.Name = "lastCheckedOn";
            this.lastCheckedOn.Size = new System.Drawing.Size(36, 17);
            this.lastCheckedOn.Text = "Never";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAllRead,
            this.toolStripStatisques,
            this.toolStripSeparator1,
            this.tstxtSearch,
            this.tsIncludeFiles,
            this.tsSearch,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tsSearchProgress});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1222, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripAllRead
            // 
            this.toolStripAllRead.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAllRead.Image")));
            this.toolStripAllRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAllRead.Name = "toolStripAllRead";
            this.toolStripAllRead.Size = new System.Drawing.Size(102, 22);
            this.toolStripAllRead.Text = "Mark all as read";
            this.toolStripAllRead.Click += new System.EventHandler(this.ToolStripAllReadClick);
            // 
            // toolStripStatisques
            // 
            this.toolStripStatisques.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatisques.Image")));
            this.toolStripStatisques.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStatisques.Name = "toolStripStatisques";
            this.toolStripStatisques.Size = new System.Drawing.Size(83, 22);
            this.toolStripStatisques.Text = "Statistiques";
            this.toolStripStatisques.Click += new System.EventHandler(this.ToolStripStatisquesClick);
            // 
            // tstxtSearch
            // 
            this.tstxtSearch.Name = "tstxtSearch";
            this.tstxtSearch.Size = new System.Drawing.Size(200, 25);
            this.tstxtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TstxtSearchKeyUp);
            // 
            // tsIncludeFiles
            // 
            this.tsIncludeFiles.CheckOnClick = true;
            this.tsIncludeFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsIncludeFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsIncludeFiles.Image")));
            this.tsIncludeFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsIncludeFiles.Name = "tsIncludeFiles";
            this.tsIncludeFiles.Size = new System.Drawing.Size(23, 22);
            this.tsIncludeFiles.Text = "Includes files";
            // 
            // tsSearch
            // 
            this.tsSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsSearch.Image")));
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(60, 22);
            this.tsSearch.Text = "Search";
            this.tsSearch.Click += new System.EventHandler(this.tsSearch_Click);
            // 
            // tsSearchProgress
            // 
            this.tsSearchProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tsSearchProgress.Name = "tsSearchProgress";
            this.tsSearchProgress.Size = new System.Drawing.Size(100, 22);
            this.tsSearchProgress.Step = 20;
            this.tsSearchProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabel1.Text = "Operation progress";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 480);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TFS Commit Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsFormClosing);
            this.Load += new System.EventHandler(this.OptionsLoad);
            this.trayMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader number;
        private System.Windows.Forms.ColumnHeader commiter;
        private System.Windows.Forms.ColumnHeader comments;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lastCheckedOn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripAllRead;
        private System.Windows.Forms.ToolStripButton toolStripStatisques;
        private System.Windows.Forms.ToolStripTextBox tstxtSearch;
        private System.Windows.Forms.ToolStripMenuItem showChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader projectId;
        private System.Windows.Forms.ToolStripButton tsIncludeFiles;
        private System.Windows.Forms.ToolStripButton tsSearch;
        private System.Windows.Forms.ToolStripProgressBar tsSearchProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

