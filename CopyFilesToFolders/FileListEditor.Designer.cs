namespace CopyFilesToFolders
{
    partial class FileListEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnAddFilesInFolder = new System.Windows.Forms.Button();
            this.btnRemoveFiles = new System.Windows.Forms.Button();
            this.btnRemoveAllFiles = new System.Windows.Forms.Button();
            this.filesListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddDestination = new System.Windows.Forms.Button();
            this.DestinationsPanel = new System.Windows.Forms.Panel();
            this.lvFiles = new CopyFilesToFolders.ListViewEx();
            this.colNb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chbOverwriteFiles = new System.Windows.Forms.CheckBox();
            this.filesListContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFiles.Location = new System.Drawing.Point(861, 0);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(140, 32);
            this.btnAddFiles.TabIndex = 1;
            this.btnAddFiles.Text = "Add Files";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnAddFilesInFolder
            // 
            this.btnAddFilesInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFilesInFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFilesInFolder.Location = new System.Drawing.Point(861, 38);
            this.btnAddFilesInFolder.Name = "btnAddFilesInFolder";
            this.btnAddFilesInFolder.Size = new System.Drawing.Size(140, 32);
            this.btnAddFilesInFolder.TabIndex = 1;
            this.btnAddFilesInFolder.Text = "Add Files in Folder";
            this.btnAddFilesInFolder.UseVisualStyleBackColor = true;
            this.btnAddFilesInFolder.Click += new System.EventHandler(this.btnAddFilesInFolder_Click);
            // 
            // btnRemoveFiles
            // 
            this.btnRemoveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFiles.Enabled = false;
            this.btnRemoveFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveFiles.Location = new System.Drawing.Point(861, 76);
            this.btnRemoveFiles.Name = "btnRemoveFiles";
            this.btnRemoveFiles.Size = new System.Drawing.Size(140, 32);
            this.btnRemoveFiles.TabIndex = 1;
            this.btnRemoveFiles.Text = "Remove";
            this.btnRemoveFiles.UseVisualStyleBackColor = true;
            this.btnRemoveFiles.Click += new System.EventHandler(this.btnRemoveFiles_Click);
            // 
            // btnRemoveAllFiles
            // 
            this.btnRemoveAllFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAllFiles.Enabled = false;
            this.btnRemoveAllFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllFiles.Location = new System.Drawing.Point(861, 114);
            this.btnRemoveAllFiles.Name = "btnRemoveAllFiles";
            this.btnRemoveAllFiles.Size = new System.Drawing.Size(140, 32);
            this.btnRemoveAllFiles.TabIndex = 1;
            this.btnRemoveAllFiles.Text = "Remove All";
            this.btnRemoveAllFiles.UseVisualStyleBackColor = true;
            this.btnRemoveAllFiles.Click += new System.EventHandler(this.btnRemoveAllFiles_Click);
            // 
            // filesListContextMenuStrip
            // 
            this.filesListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.addFilesInFolderToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.removeAllToolStripMenuItem,
            this.showInExplorerToolStripMenuItem});
            this.filesListContextMenuStrip.Name = "filesListContextMenuStrip";
            this.filesListContextMenuStrip.Size = new System.Drawing.Size(172, 114);
            this.filesListContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.filesListContextMenuStrip_Opening);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addFilesToolStripMenuItem.Text = "Add Files";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.addFilesToolStripMenuItem_Click);
            // 
            // addFilesInFolderToolStripMenuItem
            // 
            this.addFilesInFolderToolStripMenuItem.Name = "addFilesInFolderToolStripMenuItem";
            this.addFilesInFolderToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addFilesInFolderToolStripMenuItem.Text = "Add Files in Folder";
            this.addFilesInFolderToolStripMenuItem.Click += new System.EventHandler(this.addFilesInFolderToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.removeAllToolStripMenuItem.Text = "Remove All";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler(this.removeAllToolStripMenuItem_Click);
            // 
            // showInExplorerToolStripMenuItem
            // 
            this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            this.showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showInExplorerToolStripMenuItem.Text = "Show In Explorer";
            this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInExplorerToolStripMenuItem_Click);
            // 
            // btnAddDestination
            // 
            this.btnAddDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDestination.Location = new System.Drawing.Point(861, 466);
            this.btnAddDestination.Name = "btnAddDestination";
            this.btnAddDestination.Size = new System.Drawing.Size(140, 32);
            this.btnAddDestination.TabIndex = 1;
            this.btnAddDestination.Text = "Add Destination";
            this.btnAddDestination.UseVisualStyleBackColor = true;
            this.btnAddDestination.Click += new System.EventHandler(this.btnAddDestination_Click);
            // 
            // DestinationsPanel
            // 
            this.DestinationsPanel.AutoScroll = true;
            this.DestinationsPanel.Location = new System.Drawing.Point(3, 466);
            this.DestinationsPanel.Name = "DestinationsPanel";
            this.DestinationsPanel.Size = new System.Drawing.Size(857, 110);
            this.DestinationsPanel.TabIndex = 4;
            // 
            // lvFiles
            // 
            this.lvFiles.AllowDrop = true;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNb,
            this.colPath,
            this.colStatus});
            this.lvFiles.ContextMenuStrip = this.filesListContextMenuStrip;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.ShowItemToolTips = true;
            this.lvFiles.Size = new System.Drawing.Size(860, 460);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragDrop);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragEnter);
            // 
            // colNb
            // 
            this.colNb.Text = "#";
            this.colNb.Width = 48;
            // 
            // colPath
            // 
            this.colPath.Text = "Path";
            this.colPath.Width = 710;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 52;
            // 
            // chbOverwriteFiles
            // 
            this.chbOverwriteFiles.AutoSize = true;
            this.chbOverwriteFiles.Location = new System.Drawing.Point(861, 443);
            this.chbOverwriteFiles.Name = "chbOverwriteFiles";
            this.chbOverwriteFiles.Size = new System.Drawing.Size(130, 17);
            this.chbOverwriteFiles.TabIndex = 5;
            this.chbOverwriteFiles.Text = "Overwrite existing files";
            this.chbOverwriteFiles.UseVisualStyleBackColor = true;
            this.chbOverwriteFiles.CheckedChanged += new System.EventHandler(this.chbOverwriteFiles_CheckedChanged);
            // 
            // FileListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbOverwriteFiles);
            this.Controls.Add(this.DestinationsPanel);
            this.Controls.Add(this.btnAddDestination);
            this.Controls.Add(this.btnRemoveAllFiles);
            this.Controls.Add(this.btnRemoveFiles);
            this.Controls.Add(this.btnAddFilesInFolder);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.lvFiles);
            this.Name = "FileListEditor";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.FileListEditor_Load);
            this.SizeChanged += new System.EventHandler(this.FileListEditor_SizeChanged);
            this.Resize += new System.EventHandler(this.FileListEditor_Resize);
            this.filesListContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewEx lvFiles;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnAddFilesInFolder;
        private System.Windows.Forms.Button btnRemoveFiles;
        private System.Windows.Forms.Button btnRemoveAllFiles;
        private System.Windows.Forms.ColumnHeader colNb;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ContextMenuStrip filesListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInExplorerToolStripMenuItem;
        private System.Windows.Forms.Button btnAddDestination;
        private System.Windows.Forms.Panel DestinationsPanel;
        private System.Windows.Forms.CheckBox chbOverwriteFiles;
    }
}
