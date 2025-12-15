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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjecttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjecttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjecttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRecentProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.MainToolbar = new System.Windows.Forms.ToolStrip();
            this.btnNewProject = new System.Windows.Forms.ToolStripButton();
            this.btnOpenProject = new System.Windows.Forms.ToolStripButton();
            this.btnSaveProject = new System.Windows.Forms.ToolStripButton();
            this.btnSaveProjectAs = new System.Windows.Forms.ToolStripButton();
            this.btnCloseProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddFiles = new System.Windows.Forms.ToolStripButton();
            this.btnAddFilesinFolder = new System.Windows.Forms.ToolStripButton();
            this.TabImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainMenu.SuspendLayout();
            this.MainToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(984, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainMenu_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjecttoolStripMenuItem,
            this.openProjecttoolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.closeProjecttoolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newProjecttoolStripMenuItem
            // 
            this.newProjecttoolStripMenuItem.Name = "newProjecttoolStripMenuItem";
            this.newProjecttoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newProjecttoolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.newProjecttoolStripMenuItem.Text = "New Project";
            this.newProjecttoolStripMenuItem.Click += new System.EventHandler(this.newProjecttoolStripMenuItem_Click);
            // 
            // openProjecttoolStripMenuItem
            // 
            this.openProjecttoolStripMenuItem.Name = "openProjecttoolStripMenuItem";
            this.openProjecttoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjecttoolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.openProjecttoolStripMenuItem.Text = "Open Project";
            this.openProjecttoolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Enabled = false;
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As...";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // closeProjecttoolStripMenuItem
            // 
            this.closeProjecttoolStripMenuItem.Enabled = false;
            this.closeProjecttoolStripMenuItem.Name = "closeProjecttoolStripMenuItem";
            this.closeProjecttoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeProjecttoolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.closeProjecttoolStripMenuItem.Text = "Close Project";
            this.closeProjecttoolStripMenuItem.Click += new System.EventHandler(this.closeProjecttoolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(232, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProfileToolStripMenuItem,
            this.addFilesToolStripMenuItem,
            this.addFilesInFolderToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // addProfileToolStripMenuItem
            // 
            this.addProfileToolStripMenuItem.Name = "addProfileToolStripMenuItem";
            this.addProfileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addProfileToolStripMenuItem.Text = "Add Profile";
            this.addProfileToolStripMenuItem.Click += new System.EventHandler(this.addProfileToolStripMenuItem_Click);
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
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRecentProjectToolStripMenuItem,
            this.filterFilesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // loadRecentProjectToolStripMenuItem
            // 
            this.loadRecentProjectToolStripMenuItem.Name = "loadRecentProjectToolStripMenuItem";
            this.loadRecentProjectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loadRecentProjectToolStripMenuItem.Text = "Load Recent Project";
            this.loadRecentProjectToolStripMenuItem.Click += new System.EventHandler(this.loadRecentProjectToolStripMenuItem_Click);
            // 
            // filterFilesToolStripMenuItem
            // 
            this.filterFilesToolStripMenuItem.Name = "filterFilesToolStripMenuItem";
            this.filterFilesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.filterFilesToolStripMenuItem.Text = "File Filter";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Location = new System.Drawing.Point(0, 52);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(986, 510);
            this.MainTabControl.TabIndex = 1;
            this.MainTabControl.Visible = false;
            this.MainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabControl_Selecting);
            this.MainTabControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainTabControl_MouseDoubleClick);
            this.MainTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabControl_MouseDown);
            // 
            // MainToolbar
            // 
            this.MainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewProject,
            this.btnOpenProject,
            this.btnSaveProject,
            this.btnSaveProjectAs,
            this.btnCloseProject,
            this.toolStripSeparator1,
            this.btnAddFiles,
            this.btnAddFilesinFolder});
            this.MainToolbar.Location = new System.Drawing.Point(0, 24);
            this.MainToolbar.Name = "MainToolbar";
            this.MainToolbar.Size = new System.Drawing.Size(984, 25);
            this.MainToolbar.TabIndex = 2;
            this.MainToolbar.Text = "toolStrip1";
            // 
            // btnNewProject
            // 
            this.btnNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewProject.Image = global::CopyFilesToFolders.Properties.Resources.FileNew;
            this.btnNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(23, 22);
            this.btnNewProject.ToolTipText = "New Project";
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenProject.Image = global::CopyFilesToFolders.Properties.Resources.FileOpen;
            this.btnOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(23, 22);
            this.btnOpenProject.ToolTipText = "Open Project";
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveProject.Enabled = false;
            this.btnSaveProject.Image = global::CopyFilesToFolders.Properties.Resources.Save;
            this.btnSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(23, 22);
            this.btnSaveProject.ToolTipText = "Save Project";
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // btnSaveProjectAs
            // 
            this.btnSaveProjectAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveProjectAs.Enabled = false;
            this.btnSaveProjectAs.Image = global::CopyFilesToFolders.Properties.Resources.SaveAs;
            this.btnSaveProjectAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveProjectAs.Name = "btnSaveProjectAs";
            this.btnSaveProjectAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveProjectAs.ToolTipText = "Save Project As...";
            this.btnSaveProjectAs.Click += new System.EventHandler(this.btnSaveProjectAs_Click);
            // 
            // btnCloseProject
            // 
            this.btnCloseProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseProject.Enabled = false;
            this.btnCloseProject.Image = global::CopyFilesToFolders.Properties.Resources.FileClose;
            this.btnCloseProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseProject.Name = "btnCloseProject";
            this.btnCloseProject.Size = new System.Drawing.Size(23, 22);
            this.btnCloseProject.ToolTipText = "Close Project";
            this.btnCloseProject.Click += new System.EventHandler(this.btnCloseProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddFiles.Enabled = false;
            this.btnAddFiles.Image = global::CopyFilesToFolders.Properties.Resources.AddFiles;
            this.btnAddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(23, 22);
            this.btnAddFiles.ToolTipText = "Add FIles";
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnAddFilesinFolder
            // 
            this.btnAddFilesinFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddFilesinFolder.Enabled = false;
            this.btnAddFilesinFolder.Image = global::CopyFilesToFolders.Properties.Resources.AddFilesInFolder;
            this.btnAddFilesinFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFilesinFolder.Name = "btnAddFilesinFolder";
            this.btnAddFilesinFolder.Size = new System.Drawing.Size(23, 22);
            this.btnAddFilesinFolder.ToolTipText = "Add Files in Folder";
            this.btnAddFilesinFolder.Click += new System.EventHandler(this.btnAddFilesinFolder_Click);
            // 
            // TabImageList
            // 
            this.TabImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.TabImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.TabImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.MainToolbar);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CopyFilesToFolders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolbar.ResumeLayout(false);
            this.MainToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.ToolStrip MainToolbar;
        private System.Windows.Forms.ToolStripButton btnNewProject;
        private System.Windows.Forms.ToolStripButton btnOpenProject;
        private System.Windows.Forms.ToolStripButton btnSaveProject;
        private System.Windows.Forms.ToolStripButton btnSaveProjectAs;
        private System.Windows.Forms.ToolStripButton btnCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAddFiles;
        private System.Windows.Forms.ToolStripButton btnAddFilesinFolder;
        private System.Windows.Forms.ToolStripMenuItem closeProjecttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjecttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjecttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRecentProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterFilesToolStripMenuItem;
        private System.Windows.Forms.ImageList TabImageList;
    }
}