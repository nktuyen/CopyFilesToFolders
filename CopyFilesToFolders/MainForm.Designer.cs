namespace CopyFilesToFolders
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnAddFIlesInFolder = new System.Windows.Forms.Button();
            this.btnRemoveSelectedFiles = new System.Windows.Forms.Button();
            this.btnRemoveAllFiles = new System.Windows.Forms.Button();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.btnAddDestinationItem = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.chkOverwite = new System.Windows.Forms.CheckBox();
            this.bgwAddFilesInFolder = new System.ComponentModel.BackgroundWorker();
            this.MainProgressbar = new System.Windows.Forms.ProgressBar();
            this.bgwAddFiles = new System.ComponentModel.BackgroundWorker();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRecentFilesAtStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilesContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesInFolderContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainImgList = new System.Windows.Forms.ImageList(this.components);
            this.MainList = new CopyFilesToFolders.ListViewEx();
            this.colNb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.FileListContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Files:";
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFiles.Location = new System.Drawing.Point(1135, 48);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(123, 32);
            this.btnAddFiles.TabIndex = 2;
            this.btnAddFiles.Text = "Add Files";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnAddFIlesInFolder
            // 
            this.btnAddFIlesInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFIlesInFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFIlesInFolder.Location = new System.Drawing.Point(1135, 86);
            this.btnAddFIlesInFolder.Name = "btnAddFIlesInFolder";
            this.btnAddFIlesInFolder.Size = new System.Drawing.Size(123, 32);
            this.btnAddFIlesInFolder.TabIndex = 2;
            this.btnAddFIlesInFolder.Text = "Add Files in Folder";
            this.btnAddFIlesInFolder.UseVisualStyleBackColor = true;
            this.btnAddFIlesInFolder.Click += new System.EventHandler(this.btnAddFIlesInFolder_Click);
            // 
            // btnRemoveSelectedFiles
            // 
            this.btnRemoveSelectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSelectedFiles.Enabled = false;
            this.btnRemoveSelectedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSelectedFiles.Location = new System.Drawing.Point(1135, 124);
            this.btnRemoveSelectedFiles.Name = "btnRemoveSelectedFiles";
            this.btnRemoveSelectedFiles.Size = new System.Drawing.Size(123, 32);
            this.btnRemoveSelectedFiles.TabIndex = 2;
            this.btnRemoveSelectedFiles.Text = "Remove";
            this.btnRemoveSelectedFiles.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedFiles.Click += new System.EventHandler(this.btnRemoveSelectedFiles_Click);
            // 
            // btnRemoveAllFiles
            // 
            this.btnRemoveAllFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAllFiles.Enabled = false;
            this.btnRemoveAllFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllFiles.Location = new System.Drawing.Point(1135, 162);
            this.btnRemoveAllFiles.Name = "btnRemoveAllFiles";
            this.btnRemoveAllFiles.Size = new System.Drawing.Size(123, 32);
            this.btnRemoveAllFiles.TabIndex = 2;
            this.btnRemoveAllFiles.Text = "Remove All";
            this.btnRemoveAllFiles.UseVisualStyleBackColor = true;
            this.btnRemoveAllFiles.Click += new System.EventHandler(this.btnRemoveAllFiles_Click);
            // 
            // dlgFile
            // 
            this.dlgFile.Multiselect = true;
            // 
            // btnAddDestinationItem
            // 
            this.btnAddDestinationItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDestinationItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDestinationItem.Location = new System.Drawing.Point(1135, 486);
            this.btnAddDestinationItem.Name = "btnAddDestinationItem";
            this.btnAddDestinationItem.Size = new System.Drawing.Size(123, 70);
            this.btnAddDestinationItem.TabIndex = 2;
            this.btnAddDestinationItem.Text = "Add Destination";
            this.btnAddDestinationItem.UseVisualStyleBackColor = true;
            this.btnAddDestinationItem.Click += new System.EventHandler(this.btnAddDestinationItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(7, 461);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1123, 97);
            this.MainPanel.TabIndex = 3;
            // 
            // chkOverwite
            // 
            this.chkOverwite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOverwite.AutoSize = true;
            this.chkOverwite.Checked = true;
            this.chkOverwite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwite.Location = new System.Drawing.Point(1135, 466);
            this.chkOverwite.Name = "chkOverwite";
            this.chkOverwite.Size = new System.Drawing.Size(74, 17);
            this.chkOverwite.TabIndex = 4;
            this.chkOverwite.Text = "Overwrite ";
            this.chkOverwite.UseVisualStyleBackColor = true;
            // 
            // bgwAddFilesInFolder
            // 
            this.bgwAddFilesInFolder.WorkerReportsProgress = true;
            this.bgwAddFilesInFolder.WorkerSupportsCancellation = true;
            this.bgwAddFilesInFolder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAddFilesInFolder_DoWork);
            this.bgwAddFilesInFolder.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwAddFilesInFolder_ProgressChanged);
            this.bgwAddFilesInFolder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAddFilesInFolder_RunWorkerCompleted);
            // 
            // MainProgressbar
            // 
            this.MainProgressbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainProgressbar.Location = new System.Drawing.Point(7, 457);
            this.MainProgressbar.Margin = new System.Windows.Forms.Padding(0);
            this.MainProgressbar.MarqueeAnimationSpeed = 10;
            this.MainProgressbar.Name = "MainProgressbar";
            this.MainProgressbar.Size = new System.Drawing.Size(1122, 4);
            this.MainProgressbar.Step = 100;
            this.MainProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.MainProgressbar.TabIndex = 5;
            this.MainProgressbar.Visible = false;
            // 
            // bgwAddFiles
            // 
            this.bgwAddFiles.WorkerReportsProgress = true;
            this.bgwAddFiles.WorkerSupportsCancellation = true;
            this.bgwAddFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAddFiles_DoWork);
            this.bgwAddFiles.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwAddFiles_ProgressChanged);
            this.bgwAddFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAddFiles_RunWorkerCompleted);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1264, 24);
            this.MainMenu.TabIndex = 6;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.addFilesInFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRecentFilesAtStartupToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // loadRecentFilesAtStartupToolStripMenuItem
            // 
            this.loadRecentFilesAtStartupToolStripMenuItem.Name = "loadRecentFilesAtStartupToolStripMenuItem";
            this.loadRecentFilesAtStartupToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.loadRecentFilesAtStartupToolStripMenuItem.Text = "Load recent files at startup";
            this.loadRecentFilesAtStartupToolStripMenuItem.Click += new System.EventHandler(this.loadRecentFilesAtStartupToolStripMenuItem_Click);
            // 
            // FileListContextMenu
            // 
            this.FileListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesContextMenuItem,
            this.addFilesInFolderContextMenuItem,
            this.removeContextMenuItem,
            this.removeAllContextMenuItem});
            this.FileListContextMenu.Name = "FileListContextMenu";
            this.FileListContextMenu.Size = new System.Drawing.Size(172, 92);
            this.FileListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FileListContextMenu_Opening);
            // 
            // addFilesContextMenuItem
            // 
            this.addFilesContextMenuItem.Name = "addFilesContextMenuItem";
            this.addFilesContextMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addFilesContextMenuItem.Text = "Add Files";
            this.addFilesContextMenuItem.Click += new System.EventHandler(this.addFilesContextMenuItem_Click);
            // 
            // addFilesInFolderContextMenuItem
            // 
            this.addFilesInFolderContextMenuItem.Name = "addFilesInFolderContextMenuItem";
            this.addFilesInFolderContextMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addFilesInFolderContextMenuItem.Text = "Add Files in Folder";
            this.addFilesInFolderContextMenuItem.Click += new System.EventHandler(this.addFilesInFolderContextMenuItem_Click);
            // 
            // removeContextMenuItem
            // 
            this.removeContextMenuItem.Name = "removeContextMenuItem";
            this.removeContextMenuItem.Size = new System.Drawing.Size(171, 22);
            this.removeContextMenuItem.Text = "Remove";
            this.removeContextMenuItem.Click += new System.EventHandler(this.removeContextMenuItem_Click);
            // 
            // removeAllContextMenuItem
            // 
            this.removeAllContextMenuItem.Name = "removeAllContextMenuItem";
            this.removeAllContextMenuItem.Size = new System.Drawing.Size(171, 22);
            this.removeAllContextMenuItem.Text = "Remove All";
            this.removeAllContextMenuItem.Click += new System.EventHandler(this.removeAllContextMenuItem_Click);
            // 
            // MainImgList
            // 
            this.MainImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.MainImgList.ImageSize = new System.Drawing.Size(16, 16);
            this.MainImgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainList
            // 
            this.MainList.AllowDrop = true;
            this.MainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNb,
            this.colPath,
            this.colStatus});
            this.MainList.ContextMenuStrip = this.FileListContextMenu;
            this.MainList.FullRowSelect = true;
            this.MainList.GridLines = true;
            this.MainList.HideSelection = false;
            this.MainList.Location = new System.Drawing.Point(7, 49);
            this.MainList.Name = "MainList";
            this.MainList.ShowItemToolTips = true;
            this.MainList.Size = new System.Drawing.Size(1122, 407);
            this.MainList.TabIndex = 0;
            this.MainList.UseCompatibleStateImageBehavior = false;
            this.MainList.View = System.Windows.Forms.View.Details;
            this.MainList.ItemActivate += new System.EventHandler(this.MainList_ItemActivate);
            this.MainList.SelectedIndexChanged += new System.EventHandler(this.MainList_SelectedIndexChanged);
            this.MainList.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainList_DragDrop);
            this.MainList.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainList_DragEnter);
            this.MainList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainList_KeyUp);
            // 
            // colNb
            // 
            this.colNb.Text = "#";
            this.colNb.Width = 40;
            // 
            // colPath
            // 
            this.colPath.Text = "Path";
            this.colPath.Width = 960;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 100;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 561);
            this.Controls.Add(this.chkOverwite);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.btnAddDestinationItem);
            this.Controls.Add(this.btnRemoveAllFiles);
            this.Controls.Add(this.btnRemoveSelectedFiles);
            this.Controls.Add(this.btnAddFIlesInFolder);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainList);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.MainProgressbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1280, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Files to Folders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.FileListContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewEx MainList;
        private System.Windows.Forms.ColumnHeader colNb;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnAddFIlesInFolder;
        private System.Windows.Forms.Button btnRemoveSelectedFiles;
        private System.Windows.Forms.Button btnRemoveAllFiles;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
        private System.Windows.Forms.OpenFileDialog dlgFile;
        private System.Windows.Forms.Button btnAddDestinationItem;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.CheckBox chkOverwite;
        private System.ComponentModel.BackgroundWorker bgwAddFilesInFolder;
        private System.Windows.Forms.ProgressBar MainProgressbar;
        private System.ComponentModel.BackgroundWorker bgwAddFiles;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRecentFilesAtStartupToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip FileListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addFilesContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesInFolderContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllContextMenuItem;
        private System.Windows.Forms.ImageList MainImgList;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

