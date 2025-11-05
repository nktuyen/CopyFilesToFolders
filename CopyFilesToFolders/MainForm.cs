using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFilesToFolders
{
    public partial class MainForm : Form
    {
        private int DestinationTop { get; set; }
        private int DestinationSpacing { get; set; }
        private int DestinationHeight { get; set; }
        private int DestinationRightPadding = 0;
        private Dictionary<Control, bool> EnabledControls { get; set; }
        private bool LoadRecentFilesAtStartup { get; set; }
        private List<string> RecentFiles { get; set; }
        private List<DestinationItem> DestinationItems { get; set; }

        public MainForm()
        {
            InitializeComponent();
            DestinationTop = 0;
            this.DestinationSpacing = 3;
            this.DestinationHeight = 22;
            this.EnabledControls = new Dictionary<Control, bool>();
            this.LoadRecentFilesAtStartup = false;
            this.RecentFiles = new List<string>();
            this.DestinationItems = new List<DestinationItem>();
        }

        private bool LoadSettings()
        {
            string settingsKeyName = "Settings";
            string destinationListKeyName = "DestinationList";
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi= FileVersionInfo.GetVersionInfo(asm.Location);
            RegistryKey mainKey = null;
            string mainKeyPath = string.Format("SOFTWARE\\{0}\\{1}\\", fvi.CompanyName, fvi.ProductName);

            try
            {
                mainKey = Registry.CurrentUser.OpenSubKey(mainKeyPath, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (mainKey == null)
            {
                try
                {
                    mainKey = mainKey = Registry.CurrentUser.CreateSubKey(mainKeyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                    return false;
                }
            }

            if (mainKey == null)
                return false;

            RegistryKey settingsKey = null;
            try
            {
                settingsKey = mainKey.OpenSubKey(settingsKeyName, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (settingsKey == null)
            {
                try
                {
                    settingsKey = mainKey.CreateSubKey(settingsKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                    return false;
                }
            }

            if (settingsKey != null)
            {
                object objLoadRecentFilesAtStartup = null;

                try
                {
                    objLoadRecentFilesAtStartup = settingsKey.GetValue("LoadRecentFilesAtStartup");
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }

                if (objLoadRecentFilesAtStartup != null)
                {
                    this.LoadRecentFilesAtStartup = ((int)objLoadRecentFilesAtStartup != 0);
                }
            }

            if (this.LoadRecentFilesAtStartup)
            {
                this.RecentFiles.Clear();
                System.IO.StreamReader reader = null;
                System.IO.FileInfo fi = null;
                string filePath = string.Empty;
                try
                {
                    fi = new System.IO.FileInfo(asm.Location);
                    reader = new System.IO.StreamReader(System.IO.Path.Combine(fi.DirectoryName, "RecentFiles.txt"), Encoding.UTF8);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                if (reader != null)
                {
                    filePath = reader.ReadLine();
                    while (filePath != null)
                    {
                        try
                        {
                            fi = new System.IO.FileInfo(filePath);
                            this.RecentFiles.Add(fi.FullName);
                        }
                        catch (System.Exception ex)
                        {
                            Debug.Print(ex.Message);
                        }
                        filePath = reader.ReadLine();
                    }
                    reader.Close();
                }
            }

            RegistryKey destListKey = null;
            try
            {
                destListKey = mainKey.OpenSubKey(destinationListKeyName, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (destListKey == null)
            {
                try
                {
                    destListKey = mainKey.CreateSubKey(destinationListKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            if (destListKey != null)
            {
                string objCount = null;
                int nCount = 0;

                try
                {
                    objCount = destListKey.GetValue("") as string;
                }
                catch(System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }

                if (objCount != null)
                {
                    int.TryParse(objCount, out nCount);
                }

                object objData = null;
                for (int j = 0; j < nCount; j++)
                {
                    try
                    {
                        objData = destListKey.GetValue(j.ToString());
                    }
                    catch (System.Exception ex) 
                    {
                        Debug.Print(ex.Message);
                        objData = null;
                    }
                    if (objData != null)
                    {
                        AddDestination(1, null, objData as string);
                    }
                }

                foreach(string valueName in destListKey.GetValueNames())
                {
                    try
                    {
                        destListKey.DeleteValue(valueName);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }

                try
                {
                    destListKey.SetValue("", string.Empty);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            return true;
        }

        private bool SaveSettings()
        {
            string settingsKeyName = "Settings";
            string destinationListKeyName = "DestinationList";
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            RegistryKey mainKey = null;
            string mainKeyPath = string.Format("SOFTWARE\\{0}\\{1}\\", fvi.CompanyName, fvi.ProductName);

            if (this.LoadRecentFilesAtStartup)
            {
                System.IO.StreamWriter writer = null;
                System.IO.FileInfo fi = null;
                try
                {
                    fi = new System.IO.FileInfo(asm.Location);
                    writer = new System.IO.StreamWriter(System.IO.Path.Combine(fi.DirectoryName, "RecentFiles.txt"), false, Encoding.UTF8);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }

                if (writer != null)
                {
                    string filePath = string.Empty;
                    foreach (ListViewItem item in MainList.Items)
                    {
                        if (item.SubItems.Count > 1)
                        {
                            filePath = item.SubItems[1].Text;
                            writer.WriteLine(filePath);
                        }

                    }

                    writer.Flush();
                    writer.Close();
                }
            }

            try
            {
                mainKey = Registry.CurrentUser.OpenSubKey(mainKeyPath, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (mainKey == null)
            {
                try
                {
                    mainKey = mainKey = Registry.CurrentUser.CreateSubKey(mainKeyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                    return false;
                }
            }

            if (mainKey == null)
                return false;

            RegistryKey settingsKey = null;
            try
            {
                settingsKey = mainKey.OpenSubKey(settingsKeyName, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (settingsKey == null)
            {
                try
                {
                    settingsKey = mainKey.CreateSubKey(settingsKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            if (settingsKey != null)
            {
                try
                {
                    settingsKey.SetValue("LoadRecentFilesAtStartup", this.LoadRecentFilesAtStartup ? 1 : 0, RegistryValueKind.DWord);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            RegistryKey destListKey = null;
            try
            {
                destListKey = mainKey.OpenSubKey(destinationListKeyName, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (destListKey == null)
            {
                try
                {
                    destListKey = mainKey.CreateSubKey(destinationListKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            if (destListKey != null && this.DestinationItems.Count > 0)
            {
                int j = 0;
                foreach (DestinationItem dest in this.DestinationItems)
                {
                    destListKey.SetValue(j.ToString(), dest.Path);
                    j++;
                }
                destListKey.SetValue("", j.ToString());
            }

            return true;
        }

        private void EnableControl(Control ctrl, bool enabled)
        {
            this.EnabledControls[ctrl] = ctrl.Enabled;
            ctrl.Enabled = enabled;
        }

        private void RestoreControlEnabled(Control ctrl)
        {
            if (this.EnabledControls.ContainsKey(ctrl))
                ctrl.Enabled = this.EnabledControls[ctrl];
        }

        private void ArrangeControls()
        {
            MainPanel.Location = new Point(MainList.Left, MainList.Top + MainList.Height + this.DestinationSpacing);
            MainPanel.Size = new Size(MainList.Width, MainPanel.Height);
            if (MainPanel.Controls.Count >= 4)
                this.DestinationRightPadding = 20;
            else
                this.DestinationRightPadding = this.DestinationSpacing;

            this.DestinationTop = 0;
            int count = 1;
            foreach (DestinationItem ctrl in MainPanel.Controls)
            {
                ctrl.Size = new System.Drawing.Size(MainPanel.Width - this.DestinationRightPadding, this.DestinationHeight);
                ctrl.Location = new Point(0, DestinationTop);
                ctrl.Title = string.Format("Destination {0}", count++);
                DestinationTop += ctrl.Height;
                DestinationTop += this.DestinationSpacing;
            }
        }

        private void AddDestination(int num, string title = null, string path = null)
        {
            for (int i = 0; i < num; i++)
            {
                DestinationItem destItem = new DestinationItem();
                destItem.Visible = true;
                destItem.Parent = MainPanel;
                destItem.Location = new Point(0, DestinationTop);
                destItem.HasCustomButton = true;
                destItem.CustomButtonText = "X";
                destItem.Size = new System.Drawing.Size(MainPanel.Width - this.DestinationRightPadding, this.DestinationHeight);
                if (title == null || title == string.Empty)
                    destItem.Title = string.Format("Destination {0}", MainPanel.Controls.Count);
                else
                    destItem.Title = title;
                if (path != null)
                    destItem.Path = path;

                destItem.BrowseButtonClicked += new EventHandler(BrowserButton_Click);
                destItem.CopyButtonClicked += new EventHandler(CopyButton_Click);
                destItem.CustomButtonClicked += new EventHandler(RemoveDestinationButton_Click);
                
                MainPanel.Controls.Add(destItem);
                this.DestinationItems.Add(destItem);

                DestinationTop += destItem.Height;
                DestinationTop += this.DestinationSpacing;
            }
        }

        private void RemoveDestinationButton_Click(object sender, EventArgs e)
        {
            DestinationItem destItem = (DestinationItem)sender;
            MainPanel.Controls.Remove(destItem);
            MainPanel.Invalidate();
            this.DestinationItems.Remove(destItem);
            ArrangeControls();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            DestinationItem item = sender as DestinationItem;
            if (item == null)
                return;

            dlgFolder.SelectedPath = item.Path;
            dlgFolder.ShowNewFolderButton = true;
            if (dlgFolder.ShowDialog() != DialogResult.OK)
                return;

            item.Path = dlgFolder.SelectedPath;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (MainList.Items.Count <= 0)
            {
                MessageBox.Show("There is no file to copy", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DestinationItem item = sender as DestinationItem;
            if (!System.IO.Directory.Exists(item.Path))
            {
                MessageBox.Show(string.Format("Directory \"{0}\" is not exist", item.Path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sourceFilePath = string.Empty;
            string destFilePath = string.Empty;
            System.IO.FileInfo sourceFileInfo = null;
            foreach (ListViewItem listItem in MainList.Items)
            {
                sourceFilePath = listItem.SubItems[1].Text;
                try
                {
                    sourceFileInfo = new System.IO.FileInfo(sourceFilePath);
                    destFilePath = System.IO.Path.Combine(item.Path, sourceFileInfo.Name);
                    System.IO.File.Copy(sourceFilePath, destFilePath, chkOverwite.Checked);
                    listItem.SubItems[2].Text = "OK";
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                    listItem.SubItems[2].Text = "NG";
                }
            }
        }

        private void btnAddDestinationItem_Click(object sender, EventArgs e)
        {
            AddDestination(1);
            ArrangeControls();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            if (this.DestinationItems.Count <= 0)
                AddDestination(4);

            MainPanel.AutoScroll = true;
            loadRecentFilesAtStartupToolStripMenuItem.Checked = this.LoadRecentFilesAtStartup;

            ArrangeControls();

            if (this.LoadRecentFilesAtStartup && this.RecentFiles.Count > 0)
            {
                bgwAddFiles.RunWorkerAsync(this.RecentFiles.ToArray());
            }

            this.RecentFiles.Clear();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void MainList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Or DragDropEffects.Move
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length <= 0)
                    return;

                System.IO.FileInfo fi = null;
                try
                {
                    fi = new FileInfo(files[0]);
                    if((fi.Attributes & FileAttributes.Directory) != 0)
                    {
                        bgwAddFilesInFolder.RunWorkerAsync(files[0]);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }

                if (bgwAddFiles.IsBusy)
                    bgwAddFiles.CancelAsync();
                bgwAddFiles.RunWorkerAsync(files);
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Choose files";
            dlgFile.CheckPathExists = true;
            dlgFile.CheckFileExists = true;
            dlgFile.ShowReadOnly = true;
            dlgFile.Multiselect = true;
            dlgFile.Filter = "All Files(*.*)|*.*";
            if (dlgFile.ShowDialog() != DialogResult.OK)
                return;

            if (bgwAddFiles.IsBusy)
                bgwAddFiles.CancelAsync();
            bgwAddFiles.RunWorkerAsync(dlgFile.FileNames);
        }

        private void btnRemoveSelectedFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to remove selected files from list?", "Remove Selected Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            foreach (ListViewItem item in MainList.SelectedItems)
                MainList.Items.Remove(item);

            btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;
        }

        private void MainList_ItemActivate(object sender, EventArgs e)
        {
            btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
        }

        private void MainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
        }

        private void btnRemoveAllFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to remove all files in the list?", "Remove All Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            MainList.Items.Clear();
            MainImgList.Images.Clear();
            btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;
        }

        private void btnAddFIlesInFolder_Click(object sender, EventArgs e)
        {
            dlgFolder.ShowNewFolderButton = false;
            if (dlgFolder.ShowDialog() != DialogResult.OK)
                return;

            if (bgwAddFilesInFolder.IsBusy)
                bgwAddFilesInFolder.CancelAsync();
            bgwAddFilesInFolder.RunWorkerAsync(dlgFolder.SelectedPath);
        }

        private void WalkDir(string dirName, int currentDepth, int maxDepth = -1)
        {
            if (maxDepth != -1 && currentDepth >= maxDepth)
                return;
            int count = MainList.Items.Count;
            FileInfo fi = null;
            try
            {
                foreach (string filePath in Directory.GetFiles(dirName))
                {
                    if (bgwAddFilesInFolder.CancellationPending)
                        break;

                    count++;
                    try
                    {
                        fi = new FileInfo(filePath);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Print(ex.Message);
                        fi = null;
                    }
                    if (MainList.InvokeRequired)
                    {
                        MainList.Invoke(new MethodInvoker(delegate
                        {
                            ListViewItem item = MainList.Items.Add(count.ToString());
                            item.SubItems.Add(filePath);
                            item.SubItems.Add(string.Empty);//Status
                        }));
                    }
                    else
                    {
                        ListViewItem item = MainList.Items.Add(count.ToString());
                        item.SubItems.Add(filePath);
                        item.SubItems.Add(string.Empty);//Status
                    }
                    bgwAddFilesInFolder.ReportProgress(count, filePath);
                }

                foreach (string d in Directory.GetDirectories(dirName))
                {
                    if (bgwAddFilesInFolder.CancellationPending)
                        break;
                    this.WalkDir(d, currentDepth, maxDepth);
                }
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void bgwAddFilesInFolder_Prepare(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    
                    MainProgressbar.Maximum = 100;
                    MainProgressbar.Minimum = 0;
                    MainProgressbar.Value = 0;
                    MainProgressbar.Style = ProgressBarStyle.Marquee;
                    MainProgressbar.MarqueeAnimationSpeed = 100;
                    MainProgressbar.Visible = true;

                    EnableControl(btnAddFiles, false);
                    EnableControl(btnAddFIlesInFolder, false);
                    EnableControl(btnRemoveSelectedFiles, false);
                    EnableControl(btnRemoveAllFiles, false);
                    EnableControl(MainPanel, false);
                    EnableControl(btnAddDestinationItem, false);
                    EnableControl(MainMenu, false);
                    EnableControl(FileListContextMenu, false);
                }));
            }
            else
            {

                MainProgressbar.Maximum = 100;
                MainProgressbar.Minimum = 0;
                MainProgressbar.Value = 0;
                MainProgressbar.Style = ProgressBarStyle.Marquee;
                MainProgressbar.MarqueeAnimationSpeed = 100;
                MainProgressbar.Visible = true;

                EnableControl(btnAddFiles, false);
                EnableControl(btnAddFIlesInFolder, false);
                EnableControl(btnRemoveSelectedFiles, false);
                EnableControl(btnRemoveAllFiles, false);
                EnableControl(MainPanel, false);
                EnableControl(btnAddDestinationItem, false);
                EnableControl(MainMenu, false);
                EnableControl(FileListContextMenu, false);
            }
        }

        private void bgwAddFilesInFolder_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwAddFilesInFolder_Prepare(sender, e);

            string directoryPath = e.Argument as string;
            WalkDir(directoryPath, 0);
        }

        private void bgwAddFilesInFolder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (MainProgressbar.InvokeRequired)
            {
                MainProgressbar.Invoke(new MethodInvoker(delegate {
                    if (MainProgressbar.Value >= MainProgressbar.Maximum)
                        MainProgressbar.Value = MainProgressbar.Minimum;
                    else
                        MainProgressbar.Value += MainProgressbar.Step;
                }));
            }
            else
            {
                if (MainProgressbar.Value >= MainProgressbar.Maximum)
                        MainProgressbar.Value = MainProgressbar.Minimum;
                    else
                        MainProgressbar.Value += MainProgressbar.Step;
            }
        }

        private void bgwAddFilesInFolder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    MainProgressbar.Visible = false;

                    RestoreControlEnabled(btnAddFiles);
                    RestoreControlEnabled(btnAddFIlesInFolder);
                    RestoreControlEnabled(btnRemoveSelectedFiles);
                    RestoreControlEnabled(btnRemoveAllFiles);
                    RestoreControlEnabled(MainPanel);
                    RestoreControlEnabled(btnAddDestinationItem);
                    RestoreControlEnabled(MainMenu);
                    RestoreControlEnabled(FileListContextMenu);

                    btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
                    btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;

                }));
            }
            else
            {
                MainProgressbar.Visible = false;

                RestoreControlEnabled(btnAddFiles);
                RestoreControlEnabled(btnAddFIlesInFolder);
                RestoreControlEnabled(btnRemoveSelectedFiles);
                RestoreControlEnabled(btnRemoveAllFiles);
                RestoreControlEnabled(MainPanel);
                RestoreControlEnabled(btnAddDestinationItem);
                RestoreControlEnabled(MainMenu);
                RestoreControlEnabled(FileListContextMenu);

                btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
                btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;
            }
        }


        private void bgwAddFiles_Prepare(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    MainProgressbar.Style = ProgressBarStyle.Continuous;
                    MainProgressbar.Maximum = files.Length;
                    MainProgressbar.Minimum = 0;
                    MainProgressbar.Value = 0;
                    MainProgressbar.Visible = true;

                    EnableControl(btnAddFiles, false);
                    EnableControl(btnAddFIlesInFolder, false);
                    EnableControl(btnRemoveSelectedFiles, false);
                    EnableControl(btnRemoveAllFiles, false);
                    EnableControl(MainPanel, false);
                    EnableControl(btnAddDestinationItem, false);
                    EnableControl(MainMenu, false);
                    EnableControl(FileListContextMenu, false);
                }));
            }
            else
            {
                MainProgressbar.Style = ProgressBarStyle.Continuous;
                MainProgressbar.Maximum = files.Length;
                MainProgressbar.Minimum = 0;
                MainProgressbar.Value = 0;
                MainProgressbar.Visible = true;

                EnableControl(btnAddFiles, false);
                EnableControl(btnAddFIlesInFolder, false);
                EnableControl(btnRemoveSelectedFiles, false);
                EnableControl(btnRemoveAllFiles, false);
                EnableControl(MainPanel, false);
                EnableControl(btnAddDestinationItem, false);
                EnableControl(MainMenu, false);
                EnableControl(FileListContextMenu, false);
            }
        }
        private void bgwAddFiles_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwAddFiles_Prepare(sender, e);

            string[] files = e.Argument as string[];
            if (files.Length <= 0)
                return;

            ListViewItem item = null;
            int count = 0;
            System.IO.FileAttributes attrs = System.IO.FileAttributes.Archive;

            foreach (string filePath in files)
            {
                if (bgwAddFiles.CancellationPending)
                    break;

                count++;
                bgwAddFiles.ReportProgress(count, filePath);

                try
                {
                    attrs = System.IO.File.GetAttributes(filePath);
                    if ((attrs & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                        continue;
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                    continue;
                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        item = MainList.Items.Add(string.Format("{0}", (MainList.Items.Count + 1).ToString()));
                        item.SubItems.Add(filePath);
                        item.SubItems.Add(string.Empty);
                    }));
                }
                else
                {
                    item = MainList.Items.Add(string.Format("{0}", (MainList.Items.Count + 1).ToString()));
                    item.SubItems.Add(filePath);
                    item.SubItems.Add(string.Empty);
                }
            }
        }

        private void bgwAddFiles_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    MainProgressbar.Value = e.ProgressPercentage;
                }));
            }
            else
            {
                MainProgressbar.Value = e.ProgressPercentage;
            }
        }

        private void bgwAddFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    MainProgressbar.Visible = false;

                    RestoreControlEnabled(btnAddFiles);
                    RestoreControlEnabled(btnAddFIlesInFolder);
                    RestoreControlEnabled(btnRemoveSelectedFiles);
                    RestoreControlEnabled(btnRemoveAllFiles);
                    RestoreControlEnabled(MainPanel);
                    RestoreControlEnabled(btnAddDestinationItem);
                    RestoreControlEnabled(MainMenu);
                    RestoreControlEnabled(FileListContextMenu);

                    btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
                    btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;

                }));
            }
            else
            {
                MainProgressbar.Visible = false;

                RestoreControlEnabled(btnAddFiles);
                RestoreControlEnabled(btnAddFIlesInFolder);
                RestoreControlEnabled(btnRemoveSelectedFiles);
                RestoreControlEnabled(btnRemoveAllFiles);
                RestoreControlEnabled(MainPanel);
                RestoreControlEnabled(btnAddDestinationItem);
                RestoreControlEnabled(MainMenu);
                RestoreControlEnabled(FileListContextMenu);

                btnRemoveSelectedFiles.Enabled = MainList.SelectedIndices.Count > 0;
                btnRemoveAllFiles.Enabled = MainList.Items.Count > 0;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (bgwAddFiles.IsBusy)
                    bgwAddFiles.CancelAsync();
                else if (bgwAddFilesInFolder.IsBusy)
                    bgwAddFilesInFolder.CancelAsync();
            }
        }

        private void MainList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (bgwAddFiles.IsBusy)
                    bgwAddFiles.CancelAsync();
                else if (bgwAddFilesInFolder.IsBusy)
                    bgwAddFilesInFolder.CancelAsync();
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                bool bSelectedAll = MainList.SelectedIndices.Count == MainList.Items.Count;
                foreach (ListViewItem item in MainList.Items)
                {
                    item.Selected = !bSelectedAll;
                }
            }
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFiles_Click(sender, e);
        }

        private void addFilesInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFIlesInFolder_Click(sender, e);
        }

        private void loadRecentFilesAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadRecentFilesAtStartup = !this.LoadRecentFilesAtStartup;
            loadRecentFilesAtStartupToolStripMenuItem.Checked = this.LoadRecentFilesAtStartup;
        }

        private void addFilesContextMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFiles_Click(sender, e);
        }

        private void addFilesInFolderContextMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFIlesInFolder_Click(sender, e);
        }

        private void removeContextMenuItem_Click(object sender, EventArgs e)
        {
            btnRemoveSelectedFiles_Click(sender, e);
        }

        private void removeAllContextMenuItem_Click(object sender, EventArgs e)
        {
            btnRemoveAllFiles_Click(sender, e);
        }

        private void FileListContextMenu_Opening(object sender, CancelEventArgs e)
        {
            removeContextMenuItem.Enabled = MainList.SelectedItems.Count > 0;
            removeAllContextMenuItem.Enabled = MainList.Items.Count > 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgwAddFilesInFolder.IsBusy)
                bgwAddFilesInFolder.CancelAsync();
            if(bgwAddFiles.IsBusy)
                bgwAddFiles.CancelAsync();

            SaveSettings();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileVersionInfo ver = FileVersionInfo.GetVersionInfo(asm.Location);
                MessageBox.Show(string.Format("Product:{0}\nVersion:{1}\nCopyright:{2}", ver.ProductName, ver.ProductVersion, ver.LegalCopyright), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
            	
            }
        }
    }
}
