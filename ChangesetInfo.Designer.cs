namespace TfsCommitMonitor
{
    partial class ChangesetInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblChangeset = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.changetype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.diffWithPreviousVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.viewLatestVersionInVisualStudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(172, 11);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(87, 13);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "RESO\\KeviMoor";
            // 
            // lblChangeset
            // 
            this.lblChangeset.AutoSize = true;
            this.lblChangeset.Location = new System.Drawing.Point(32, 11);
            this.lblChangeset.Name = "lblChangeset";
            this.lblChangeset.Size = new System.Drawing.Size(31, 13);
            this.lblChangeset.TabIndex = 4;
            this.lblChangeset.Text = "2356";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.changetype,
            this.filename});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 110);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(687, 203);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // changetype
            // 
            this.changetype.Text = "Type";
            this.changetype.Width = 100;
            // 
            // filename
            // 
            this.filename.Text = "Filename";
            this.filename.Width = 550;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diffWithPreviousVersionToolStripMenuItem,
            this.viewLatestVersionInVisualStudioToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 70);
            // 
            // diffWithPreviousVersionToolStripMenuItem
            // 
            this.diffWithPreviousVersionToolStripMenuItem.Name = "diffWithPreviousVersionToolStripMenuItem";
            this.diffWithPreviousVersionToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.diffWithPreviousVersionToolStripMenuItem.Text = "Diff with previous version";
            this.diffWithPreviousVersionToolStripMenuItem.Click += new System.EventHandler(this.DiffWithPreviousVersionToolStripMenuItemClick);
            // 
            // txtComments
            // 
            this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComments.BackColor = System.Drawing.Color.White;
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComments.Location = new System.Drawing.Point(12, 39);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(687, 65);
            this.txtComments.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(624, 319);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Fermer";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // viewLatestVersionInVisualStudioToolStripMenuItem
            // 
            this.viewLatestVersionInVisualStudioToolStripMenuItem.Name = "viewLatestVersionInVisualStudioToolStripMenuItem";
            this.viewLatestVersionInVisualStudioToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.viewLatestVersionInVisualStudioToolStripMenuItem.Text = "View latest version in Visual Studio";
            this.viewLatestVersionInVisualStudioToolStripMenuItem.Click += new System.EventHandler(this.viewLatestVersionInVisualStudioToolStripMenuItem_Click);
            // 
            // ChangesetInfo
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(711, 346);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblChangeset);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangesetInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changeset Information";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblChangeset;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader changetype;
        private System.Windows.Forms.ColumnHeader filename;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem diffWithPreviousVersionToolStripMenuItem;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem viewLatestVersionInVisualStudioToolStripMenuItem;
    }
}