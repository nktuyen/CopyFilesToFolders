using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace CopyFilesToFolders
{
    public partial class MainForm : Form
    {
        private List<IFilter> FileFilters { get; set; }
        private string CurrentProjectName { get; set; }
        private bool CurrentProjectChanged { get; set; }
        private bool LoadRecentProject { get; set; }
        private List<string> RecentFiles { get; set; }
        private ToolStripMenuItem RecentFilesMenuItem { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.FileFilters = new List<IFilter>();
            this.FileFilters.Add(new NameFilter(true));
            this.RecentFiles = new List<string>();
            this.CurrentProjectName = null;
            this.CurrentProjectChanged = false;
        }

        private void FilterMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                IFilter filter = menuItem.Tag as IFilter;
                if (filter != null)
                {
                    filter.Enabled = !filter.Enabled;
                    menuItem.Checked = filter.Enabled;
                }
            }
        }

        private void LoadRecentFileMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            if (this.CurrentProjectName != null)
            {
                if (this.CurrentProjectChanged)
                {
                    DialogResult res = MessageBox.Show("Current project is not saved\nDo you want to save it now?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                        return;

                    if (res == DialogResult.Yes)
                    {
                        if (this.CurrentProjectName == string.Empty)
                        {
                            SaveFileDialog dlgSave = new SaveFileDialog();
                            dlgSave.Title = "Save Project";
                            dlgSave.OverwritePrompt = true;
                            dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                            dlgSave.FilterIndex = 1;

                            if (dlgSave.ShowDialog() == DialogResult.OK)
                            {
                                if (RecentFiles.Contains(dlgSave.FileName))
                                {
                                    RecentFiles.Remove(dlgSave.FileName);
                                }
                                RecentFiles.Add(dlgSave.FileName);
                                PopulateRecentFiles();
                                SaveCurrentProjectTo(dlgSave.FileName);
                            }
                        }
                    }
                }
            }

            MainTabControl.TabPages.Clear();
            MainTabControl.Visible = false;
            this.CurrentProjectName = string.Empty;
            this.CurrentProjectChanged = true;


            string filePath = menuItem.Text;
            if(LoadProjectFrom(filePath))
            {
                this.CurrentProjectName = filePath;
                this.CurrentProjectChanged = false;
                CurrentProject_Changed(this, new EventArgs());
            }
        }

        private void PopulateRecentFiles()
        {
            if (RecentFiles.Count <= 0)
                return;

            if (RecentFilesMenuItem == null)
            {
                RecentFilesMenuItem = new ToolStripMenuItem("Open Recent Project");
                fileToolStripMenuItem.DropDownItems.Insert(3, RecentFilesMenuItem);
            }

            if (RecentFilesMenuItem != null)
            {
                RecentFilesMenuItem.DropDownItems.Clear();
                foreach (string file in RecentFiles)
                {
                    ToolStripItem menuItem = RecentFilesMenuItem.DropDownItems.Add(file);
                    menuItem.Click += new EventHandler(LoadRecentFileMenuItem_Click);
                }
            }
        }

        private void FileFilterSettingsMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            NameFilter nameFilter = this.FileFilters[0] as NameFilter;
            if (nameFilter != null)
            {
                NameFilterSettingsForm frm = new NameFilterSettingsForm();
                frm.Wildcard=nameFilter.Wildcard;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    nameFilter.Wildcard = frm.Wildcard;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TabImageList.Images.Add(Properties.Resources.AddProfile);
            MainTabControl.ImageList = TabImageList;

            LoadSettings();
            PopulateRecentFiles();
            loadRecentProjectToolStripMenuItem.Checked = this.LoadRecentProject;

            int count = 0;
            foreach (IFilter filter in this.FileFilters)
            {
                if(filter.Enabled)
                {
                    ToolStripMenuItem menuItem = filterFilesToolStripMenuItem.DropDownItems.Add(filter.Title) as ToolStripMenuItem;
                    menuItem.Tag = filter;
                    menuItem.Checked = filter.Enabled;
                    menuItem.Click += new EventHandler(FilterMenuItem_Click);
                    count++;
                }
            }
            if(count > 0)
            {
                filterFilesToolStripMenuItem.DropDownItems.Add("-");
                ToolStripItem settingsItem = filterFilesToolStripMenuItem.DropDownItems.Add("Settings");
                settingsItem.Click += new EventHandler(FileFilterSettingsMenuItem_Click);
            }


            if(this.LoadRecentProject && RecentFiles.Count > 0)
            {
                if (this.RecentFiles.Contains(this.CurrentProjectName))
                {
                    if (LoadProjectFrom(this.CurrentProjectName))
                    {
                        if (MainTabControl.TabCount > 0)
                            MainTabControl.Visible = true;

                        this.CurrentProjectChanged = false;
                        CurrentProject_Changed(this, new EventArgs());
                    }
                }
            }
        }

        private void CurrentProject_Changed(object sender, EventArgs e)
        {
            saveProjectToolStripMenuItem.Enabled = this.CurrentProjectName != null && this.CurrentProjectChanged == true;
            saveProjectAsToolStripMenuItem.Enabled = this.CurrentProjectName != null;
            closeProjecttoolStripMenuItem.Enabled = this.CurrentProjectName != null;

            btnSaveProject.Enabled = saveProjectToolStripMenuItem.Enabled;
            btnSaveProjectAs.Enabled = saveProjectAsToolStripMenuItem.Enabled;
            btnCloseProject.Enabled = closeProjecttoolStripMenuItem.Enabled;

            btnAddFiles.Enabled = this.CurrentProjectName != null && MainTabControl.Visible;
            btnAddFilesinFolder.Enabled = this.CurrentProjectName != null && MainTabControl.Visible;
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileListEditor editor = MainTabControl.SelectedTab.Controls[0] as FileListEditor;
            if (editor == null)
                return;
            editor.AddFiles();
        }

        private void addFilesInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileListEditor editor = MainTabControl.SelectedTab.Controls[0] as FileListEditor;
            if (editor == null)
                return;
            editor.AddFilesInFolder();
        }

        private string GenerateTabPageTitle(string template)
        {
            string res = template;
            int count = 0;
            bool existing = true;
            while (existing)
            {
                existing = false;
                foreach (TabPage page in MainTabControl.TabPages)
                {
                    if (page.Text.ToLower() == res.ToLower())
                    {
                        existing = true;
                        count++;
                        res = string.Format("{0} {1}", template, count);
                        break;
                    }
                }
            }
            return res;
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            addFilesToolStripMenuItem_Click(sender, e);
        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            addProfileToolStripMenuItem_Click(sender, e);
        }

        private void btnAddFilesinFolder_Click(object sender, EventArgs e)
        {
            addFilesInFolderToolStripMenuItem_Click(sender, e);
        }

        private void addProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileNameForm frm = new ProfileNameForm();
            frm.ProfileName = GenerateTabPageTitle("New Profile");
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            foreach (TabPage page in MainTabControl.TabPages)
            {
                if (string.Compare(page.Text, frm.ProfileName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    MessageBox.Show(string.Format("Profile with name \"{0}\" is already exist", frm.ProfileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            TabPage newPage = new TabPage(frm.ProfileName);
            FileListEditor filesEditor = new FileListEditor(frm.ProfileName);
            newPage.Tag = filesEditor;
            filesEditor.Parent = newPage;
            filesEditor.Dock = DockStyle.Fill;
            filesEditor.FileFilters = this.FileFilters;
            filesEditor.Changed += new EventHandler(FileListChanged);
            newPage.Controls.Add(filesEditor);
            MainTabControl.TabPages.Insert(MainTabControl.TabPages.Count - 1, newPage);
            MainTabControl.Visible = true;
            MainTabControl.SelectedTab = newPage;
            this.CurrentProjectChanged = true;
            CurrentProject_Changed(this, new EventArgs());
        }

        private void FileListChanged(object sender, EventArgs e)
        {
            this.CurrentProjectChanged = true;
            CurrentProject_Changed(this, new EventArgs());
        }

        private bool LoadProjectFrom(string path)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(path))
                {
                    FileListEditor filesEditor = null;
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "Profile")
                            {
                                string profileName = reader.GetAttribute("Name");
                                if(profileName != string.Empty)
                                {
                                    filesEditor = new FileListEditor(profileName);
                                    filesEditor.ReadXML(reader);
                                    filesEditor.Dock = DockStyle.Fill;
                                    filesEditor.FileFilters = this.FileFilters;
                                    filesEditor.Changed += new EventHandler(FileListChanged);
                                }
                            }
                            else if(reader.Name == "Option" || reader.Name == "File" || reader.Name == "Destination")
                            {
                                if (filesEditor != null)
                                    filesEditor.ReadXML(reader);
                            }
                        }
                        else if(reader.NodeType == XmlNodeType.EndElement)
                        {
                            if(reader.Name == "Profile")
                            {
                                if(filesEditor != null)
                                {
                                    TabPage newPage = new TabPage(filesEditor.Title);
                                    filesEditor.Parent = newPage;
                                    newPage.Controls.Add(filesEditor);
                                    newPage.Tag = filesEditor;
                                    MainTabControl.TabPages.Add(newPage);
                                    MainTabControl.Visible = true;
                                    MainTabControl.SelectedTab = newPage;
                                    this.CurrentProjectChanged = true;
                                    CurrentProject_Changed(this, new EventArgs());

                                    filesEditor = null;
                                }
                            }
                        }
                    }

                    TabPage addButtonPage = new TabPage();
                    addButtonPage.ImageIndex = 0;
                    MainTabControl.TabPages.Add(addButtonPage);
                    addButtonPage.UseVisualStyleBackColor = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.Message);
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (System.Exception ex2)
                    {
                        Debug.Print(ex2.Message);
                    }
                }
                return false;
            }

            return true;
        }
        private bool SaveCurrentProjectTo(string path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fileStream))
                using (XmlTextWriter writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 4;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Project");
                    writer.WriteStartElement("Profiles");
                    foreach (TabPage page in MainTabControl.TabPages)
                    {
                        if (page.Tag == null)
                            continue;
                        FileListEditor editor = page.Tag as FileListEditor;
                        if (editor != null)
                        {
                            editor.WriteXML(writer);
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.Message);
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch(System.Exception ex2)
                    {
                        Debug.Print(ex2.Message);
                    }
                }
                return false;
            }

            return true;
        }
        private void newProjecttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentProjectName != null)
            {
                if (this.CurrentProjectChanged)
                {
                    DialogResult res = MessageBox.Show("Current project is not saved\nDo you want to save it now?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                        return;

                    if (res == DialogResult.Yes)
                    {
                        if (this.CurrentProjectName == string.Empty)
                        {
                            SaveFileDialog dlgSave = new SaveFileDialog();
                            dlgSave.Title = "Save Project";
                            dlgSave.OverwritePrompt = true;
                            dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                            dlgSave.FilterIndex = 1;

                            if (dlgSave.ShowDialog() == DialogResult.OK)
                            {
                                if (RecentFiles.Contains(dlgSave.FileName))
                                {
                                    RecentFiles.Remove(dlgSave.FileName);
                                }

                                RecentFiles.Add(dlgSave.FileName);
                                PopulateRecentFiles();
                                SaveCurrentProjectTo(dlgSave.FileName);
                            }
                        }
                    }
                }
            }

            MainTabControl.TabPages.Clear();
            MainTabControl.Visible = false;
            this.CurrentProjectName = string.Empty;
            this.CurrentProjectChanged = true;
            
            if(MainTabControl.TabCount <= 0)
            {
                TabPage newPage = new TabPage("New Profile");
                FileListEditor editor = new FileListEditor(newPage.Text);
                editor.Parent = newPage;
                editor.Dock = DockStyle.Fill;
                editor.FileFilters = this.FileFilters;
                editor.Changed += new EventHandler(FileListChanged);
                newPage.Controls.Add(editor);
                newPage.Tag = editor;
                MainTabControl.TabPages.Add(newPage);
                MainTabControl.SelectedTab = newPage;
                MainTabControl.Visible = true;

                TabPage addButtonPage = new TabPage();
                addButtonPage.ImageIndex = 0;
                MainTabControl.TabPages.Add(addButtonPage);
                addButtonPage.UseVisualStyleBackColor = true;
            }

            CurrentProject_Changed(this, new EventArgs());
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            addProfileToolStripMenuItem.Enabled = this.CurrentProjectName != null;
            addFilesToolStripMenuItem.Enabled = this.CurrentProjectName != null;
            addFilesInFolderToolStripMenuItem.Enabled = this.CurrentProjectName != null;
        }

        private void MainTabControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabPage selectedPage = null;
            for (int i = 0; i < MainTabControl.TabCount; i++)
            {
                TabPage page = MainTabControl.TabPages[i];
                if (page.Tag == null)
                    continue;
                Rectangle tabRect = MainTabControl.GetTabRect(i);
                if (tabRect.Contains(e.Location))
                {
                    selectedPage = MainTabControl.TabPages[i];
                    break;
                }
            }
            if (selectedPage != null)
            {
                string oldName = selectedPage.Text;
                PageRenameForm frm = new PageRenameForm(selectedPage.Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string newName = frm.PageName;
                    if (newName != oldName)
                    {
                        selectedPage.Text = newName;
                        FileListEditor editor = selectedPage.Controls[0] as FileListEditor;
                        if (editor != null)
                        {
                            editor.Title = newName;
                        }
                        this.CurrentProjectChanged = true;
                        CurrentProject_Changed(this, new EventArgs());
                    }
                }
            }
        }

        private void closeProjecttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentProjectChanged)
            {
                DialogResult res = MessageBox.Show("Current project is not saved\nDo you want to save it now?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                    return;

                if (res == DialogResult.Yes)
                {
                    if (this.CurrentProjectName == string.Empty)
                    {
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.Title = "Save Project";
                        dlgSave.OverwritePrompt = true;
                        dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                        dlgSave.FilterIndex = 1;

                        if (dlgSave.ShowDialog() == DialogResult.OK)
                        {
                            if (RecentFiles.Contains(dlgSave.FileName))
                            {
                                RecentFiles.Remove(dlgSave.FileName);
                            }
                            RecentFiles.Add(dlgSave.FileName);
                            PopulateRecentFiles();
                            SaveCurrentProjectTo(dlgSave.FileName);
                        }
                    }
                }
            }

            MainTabControl.TabPages.Clear();
            MainTabControl.Visible = false;
            this.CurrentProjectName = null;
            this.CurrentProjectChanged = false;
            CurrentProject_Changed(this, new EventArgs());
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            saveProjectToolStripMenuItem_Click(sender, e);
        }

        private void btnSaveProjectAs_Click(object sender, EventArgs e)
        {
            saveProjectAsToolStripMenuItem_Click(sender, e);
        }

        private void btnCloseProject_Click(object sender, EventArgs e)
        {
            closeProjecttoolStripMenuItem_Click(sender, e);
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            newProjecttoolStripMenuItem_Click(sender, e);
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            openProjectToolStripMenuItem_Click(sender, e);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentProjectName != null)
            {
                if (this.CurrentProjectChanged)
                {
                    DialogResult res = MessageBox.Show("Current project is not saved\nDo you want to save it now?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                        return;

                    if (res == DialogResult.Yes)
                    {
                        if (this.CurrentProjectName == string.Empty)
                        {
                            SaveFileDialog dlgSave = new SaveFileDialog();
                            dlgSave.Title = "Save Project";
                            dlgSave.OverwritePrompt = true;
                            dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                            dlgSave.FilterIndex = 1;

                            if (dlgSave.ShowDialog() == DialogResult.OK)
                            {
                                if (RecentFiles.Contains(dlgSave.FileName))
                                {
                                    RecentFiles.Remove(dlgSave.FileName);
                                }

                                RecentFiles.Add(dlgSave.FileName);
                                PopulateRecentFiles();
                                SaveCurrentProjectTo(dlgSave.FileName);
                            }
                        }
                    }
                }
            }

            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Multiselect = false;
            dlgOpen.Title = "Open Project";
            dlgOpen.Filter = "All Files|*.*|Project Files|*.copierproject";
            dlgOpen.CheckPathExists = true;
            dlgOpen.CheckFileExists = true;
            dlgOpen.FilterIndex = 2;
            if (dlgOpen.ShowDialog() != DialogResult.OK)
                return;

            MainTabControl.TabPages.Clear();
            MainTabControl.Visible = false;
            this.CurrentProjectName = null;
            this.CurrentProjectChanged = false;
            CurrentProject_Changed(this, new EventArgs());

            if (LoadProjectFrom(dlgOpen.FileName))
            {
                if (RecentFiles.Contains(dlgOpen.FileName))
                {
                    RecentFiles.Remove(dlgOpen.FileName);
                }

                RecentFiles.Add(dlgOpen.FileName);
                PopulateRecentFiles();

                if (MainTabControl.TabCount > 0)
                    MainTabControl.Visible = true;

                this.CurrentProjectName = dlgOpen.FileName;
                this.CurrentProjectChanged = false;
                CurrentProject_Changed(this, new EventArgs());
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.CurrentProjectName==null || this.CurrentProjectName.Length==0)
            {
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.Title = "Save Project";
                dlgSave.OverwritePrompt = true;
                dlgSave.CheckPathExists = true;
                dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                dlgSave.FilterIndex = 2;
                if (dlgSave.ShowDialog() != DialogResult.OK)
                    return;

                if (RecentFiles.Contains(dlgSave.FileName))
                {
                    RecentFiles.Remove(dlgSave.FileName);
                }

                RecentFiles.Add(dlgSave.FileName);
                PopulateRecentFiles();

                this.CurrentProjectName = dlgSave.FileName;
            }

            if (SaveCurrentProjectTo(this.CurrentProjectName))
            {
                this.CurrentProjectChanged = false;
                CurrentProject_Changed(this, new EventArgs());
            }
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Title = "Save Project";
            dlgSave.OverwritePrompt = true;
            dlgSave.CheckPathExists = true;
            dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
            dlgSave.FilterIndex = 1;
            if (dlgSave.ShowDialog() != DialogResult.OK)
                return;

            if (SaveCurrentProjectTo(dlgSave.FileName))
            {
                if (RecentFiles.Contains(dlgSave.FileName))
                {
                    RecentFiles.Remove(dlgSave.FileName);
                }

                RecentFiles.Add(dlgSave.FileName);
                PopulateRecentFiles();

                this.CurrentProjectName = dlgSave.FileName;
                this.CurrentProjectChanged = false;
                CurrentProject_Changed(this, new EventArgs());
            }
        }

        private void loadRecentProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadRecentProject = !this.LoadRecentProject;
            loadRecentProjectToolStripMenuItem.Checked = this.LoadRecentProject;
        }

        private void LoadSettings()
        {
            RegistryKey regKey = null;
            Assembly asm = null;
            FileVersionInfo fileVer = null;
            string registryPath = string.Empty;
            try
            {
                asm = Assembly.GetExecutingAssembly();
                fileVer = FileVersionInfo.GetVersionInfo(asm.Location);
                registryPath = string.Format("SOFTWARE\\{0}\\{1}", fileVer.CompanyName, fileVer.ProductName);
                regKey = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (regKey == null)
            {
                try
                {
                    regKey = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex2)
                {
                    Debug.Print(ex2.Message);
                    return;
                }
            }

            if (regKey == null)
                return;

            try
            {
                object   objLoadRcentFile = regKey.GetValue("LoadRecentProject", RegistryValueKind.DWord);
                if (objLoadRcentFile != null)
                    this.LoadRecentProject = (int)objLoadRcentFile != 0;

                foreach (IFilter filter in this.FileFilters)
                {
                    filter.LoadSettings(regKey);
                }

                if (regKey.GetSubKeyNames().Contains("RecentFiles"))
                {
                    RecentFiles.Clear();
                    RegistryKey recentFilesKey = regKey.CreateSubKey("RecentFiles", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (recentFilesKey != null)
                    {
                        object objFileName = null;
                        string filePath = string.Empty;
                        foreach (string valueName in recentFilesKey.GetValueNames())
                        {
                            objFileName = recentFilesKey.GetValue(valueName);
                            if (objFileName != null)
                            {
                                filePath = objFileName as string;
                                if ((System.IO.File.Exists(filePath)) && !RecentFiles.Contains(filePath))
                                    RecentFiles.Add(filePath);
                            }
                        }

                        objFileName = recentFilesKey.GetValue(string.Empty);
                        if (objFileName != null)
                        {
                            filePath = objFileName as string;
                            if (System.IO.File.Exists(filePath))
                            {
                                this.CurrentProjectName = filePath;
                                if (!RecentFiles.Contains(filePath))
                                    RecentFiles.Add(filePath);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex3)
            {
                Debug.Print(ex3.Message);
                return;
            }
        }

        private void SaveSettings()
        {
            RegistryKey regKey = null;
            Assembly asm = null;
            FileVersionInfo fileVer = null;
            string registryPath = string.Empty;
            try
            {
                asm = Assembly.GetExecutingAssembly();
                fileVer = FileVersionInfo.GetVersionInfo(asm.Location);
                registryPath = string.Format("SOFTWARE\\{0}\\{1}", fileVer.CompanyName, fileVer.ProductName);
                regKey = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if(regKey == null)
            {
                try
                {
                    regKey = Registry.CurrentUser.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch(System.Exception ex2)
                {
                    Debug.Print(ex2.Message);
                    return;
                }
            }

            if (regKey == null)
                return;

            try
            {
                regKey.SetValue("LoadRecentProject", this.LoadRecentProject ? 1 : 0, RegistryValueKind.DWord);
                foreach(IFilter filter in this.FileFilters)
                {
                    filter.SaveSettings(regKey);
                }

                if(regKey.GetSubKeyNames().Contains("RecentFiles"))
                {
                    regKey.DeleteSubKeyTree("RecentFiles");
                }

                RegistryKey recentFilesKey = regKey.CreateSubKey("RecentFiles", RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (recentFilesKey != null)
                {
                    int count = 0;
                    for (int j = RecentFiles.Count - 1; j >= 0; j--)
                    {
                        if (count < 10)
                        {
                            recentFilesKey.SetValue(count.ToString(), RecentFiles[j]);
                            count++;
                        }
                        else
                            break;
                    }
                    recentFilesKey.SetValue(string.Empty, this.CurrentProjectName);
                }
            }
            catch(System.Exception ex3)
            {
                Debug.Print(ex3.Message);
                return;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CurrentProjectChanged)
            {
                DialogResult res = MessageBox.Show("Current project is not saved\nDo you want to save it now?", "New Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (res == DialogResult.Yes)
                {
                    if (this.CurrentProjectName == string.Empty)
                    {
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.Title = "Save Project";
                        dlgSave.OverwritePrompt = true;
                        dlgSave.Filter = "All Files|*.*|Project Files|*.copierproject";
                        dlgSave.FilterIndex = 1;

                        if (dlgSave.ShowDialog() == DialogResult.OK)
                        {
                            SaveCurrentProjectTo(dlgSave.FileName);
                        }
                    }
                }
            }

            SaveSettings();
        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(e.TabPageIndex == MainTabControl.TabPages.Count-1)
            {
                e.Cancel = true;
            }
        }

        private void MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var lastIndex = this.MainTabControl.TabCount - 1;
                if (this.MainTabControl.GetTabRect(lastIndex).Contains(e.Location))
                {
                    addProfileToolStripMenuItem_Click(sender, e);
                    this.MainTabControl.TabPages[lastIndex].UseVisualStyleBackColor = true;
                }
            }
            else if(e.Button== MouseButtons.Middle)
            {
                var lastIndex = this.MainTabControl.TabCount - 1;
                if (this.MainTabControl.GetTabRect(lastIndex).Contains(e.Location))
                {
                    //
                }
                else
                {
                    if(MainTabControl.TabPages.Count<=2)
                    {
                        MessageBox.Show("Cannot delete last profile\nClose project instead", "Delete Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    for (var i = 0; i < this.MainTabControl.TabPages.Count; i++)
                    {
                        var tabRect = this.MainTabControl.GetTabRect(i);
                        if (tabRect.Contains(e.Location))
                        {
                            TabPage clickedPage=MainTabControl.TabPages[i];
                            if(clickedPage.Tag != null)
                            {
                                if(MessageBox.Show("Are you sure?", "Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                                {
                                    MainTabControl.TabPages.Remove(clickedPage);
                                    if(MainTabControl.TabPages.Count == 1)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alwaysOnTopToolStripMenuItem.Checked = !alwaysOnTopToolStripMenuItem.Checked;
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }
    }
}
