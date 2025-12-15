using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CopyFilesToFolders
{
    public partial class FileListEditor : UserControl
    {
        public string Title { get; set; }
        private List<DestinationItem> Destinations { get; set; }
        public int DestinationsVisibleItems { get; set; }
        public int DestinationHeight { get; set; } 
        private int ControlsHorizontalSpacing { get; set; }
        private int ControlsVerticalSpacing { get; set; }
        private Dictionary<string, int> FileToInstancesMap;
        private string LastSelectedFolder { get; set; }
        public List<IFilter> FileFilters { get; set; }
        public event EventHandler Changed;


        public FileListEditor(string title = "")
        {
            InitializeComponent();
            this.Title = title;
            this.Destinations = new List<DestinationItem>();
            this.DestinationHeight = 22;
            this.DestinationsVisibleItems = 4;
            this.ControlsHorizontalSpacing = 2;
            this.ControlsVerticalSpacing = 2;
            this.FileToInstancesMap = new Dictionary<string, int>();
            this.LastSelectedFolder = Environment.CurrentDirectory;
            this.FileFilters = null;
        }

        public void ReadXML(XmlReader reader)
        {
            if(reader.NodeType == XmlNodeType.Element)
            {
                if(reader.Name == "Option")
                {
                    string optionValue = reader.GetAttribute(chbOverwriteFiles.Text);
                    if(optionValue != null)
                    {
                        optionValue=optionValue.ToLower().Trim();
                        if (optionValue == "true" || optionValue == "1" || optionValue == "yes")
                            chbOverwriteFiles.Checked = true;
                        else
                            chbOverwriteFiles.Checked = false;
                    }
                }
                else if(reader.Name == "File")
                {
                    string filePath = reader.GetAttribute("Name");
                    if(System.IO.File.Exists(filePath))
                    {
                        int count = lvFiles.Items.Count;
                        count++;
                        ListViewItem item = lvFiles.Items.Add(count.ToString());
                        item.SubItems.Add(filePath);
                        item.SubItems.Add(string.Empty);
                        btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;
                    }
                }
                else if(reader.Name == "Destination")
                {
                    string destPath = reader.GetAttribute("Path");
                    if(destPath != null)
                    {
                        DestinationItem destinationItem = new DestinationItem();
                        destinationItem.Path = destPath;
                        destinationItem.BrowseButtonClicked += new EventHandler(DestinationBrowserButton_Click);
                        destinationItem.CustomButtonClicked += new EventHandler(DestinationDeleteButton_Click);
                        destinationItem.CopyButtonClicked += new EventHandler(DestinationCopyButton_Click);
                        destinationItem.PathTextChanged += new EventHandler(DestinationPath_Changed);

                        this.Destinations.Add(destinationItem);
                        ArrangeDestinations();

                        if (this.Changed != null)
                            this.Changed(this, new EventArgs());
                    }
                }
            }
        }

        public void WriteXML(XmlWriter writer)
        {
            try
            {
                writer.WriteStartElement("Profile");
                writer.WriteAttributeString("Name", this.Title);
                {
                    writer.WriteStartElement("Options");
                    {
                        writer.WriteStartElement("Option");
                        {
                            writer.WriteAttributeString("name", chbOverwriteFiles.Text);
                            writer.WriteAttributeString("value", chbOverwriteFiles.Checked ? "true" : "false");
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Files");
                    {
                        foreach (ListViewItem item in lvFiles.Items)
                        {
                            writer.WriteStartElement("File");
                            {
                                writer.WriteAttributeString("Name", item.SubItems[1].Text);
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Destinations");
                    {
                        foreach (DestinationItem dest in this.Destinations)
                        {
                            writer.WriteStartElement("Destination");
                            {
                                writer.WriteAttributeString("Name", dest.Title);
                                writer.WriteAttributeString("Path", dest.Path);
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void AddFiles()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Title = "Choose Files";
            dlgOpen.Filter = "All Files|*.*|EXE Files|*.exe|DLL Files|*.dll|OCX Files|*.ocx|TXT Files|*.txt|LOG Files|*.log|DOC Files|*.doc|DOCX Files|*.docx|XLS Files|*.xls|XLSX Files|*.xlsx|PPT Files|*.ppt|PPTX Files|*.pptx|HTML Files|*.html|HTM Files|*.htm";
            dlgOpen.CheckPathExists = true;
            dlgOpen.CheckFileExists = true;
            dlgOpen.ShowReadOnly = true;
            dlgOpen.Multiselect = true;
            if (dlgOpen.ShowDialog() != DialogResult.OK)
                return;

            int instances = 0;
            int count = lvFiles.Items.Count;
            bool satisfied = false;
            foreach (string filePath in dlgOpen.FileNames)
            {
                satisfied = false;
                Debug.Print(filePath);

                if (this.FileFilters != null && this.FileFilters.Count > 0)
                {
                    foreach(IFilter filter in this.FileFilters)
                    {
                        if(filter.Enabled && filter.Filter(filePath))
                        {
                            satisfied = true;
                            break;
                        }
                    }
                }
                else
                {
                    satisfied = true;
                }

                if (!satisfied)
                    continue;

                if (this.FileToInstancesMap.ContainsKey(filePath))
                {
                    instances = this.FileToInstancesMap[filePath];
                }
                else
                {
                    instances = 0;
                    count++;
                    ListViewItem item = lvFiles.Items.Add(count.ToString());
                    item.SubItems.Add(filePath);
                    item.SubItems.Add(string.Empty);
                }
                instances++;
                this.FileToInstancesMap[filePath] = instances;
            }
            
            btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;

            if (this.Changed != null)
                Changed(this, new EventArgs());
        }

        private void WalkDirAddFiles(string dirName, bool recursive = true)
        {
            System.IO.DirectoryInfo di = null;
            Debug.Print(dirName);
            try
            {
                di = new System.IO.DirectoryInfo(dirName);
            }
            catch(System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (di == null)
                return;

            bool satisfied = false;
            int instances = 0;
            int count = lvFiles.Items.Count;
            foreach (System.IO.FileInfo fi in di.GetFiles())
            {
                satisfied = false;
                Debug.Print(fi.FullName);

                if(this.FileFilters != null && this.FileFilters.Count > 0)
                {
                    foreach(IFilter filter in this.FileFilters)
                    {
                        if(filter.Enabled && filter.Filter(fi.FullName))
                        {
                            satisfied = true;
                            break;
                        }
                    }
                }
                else
                {
                    satisfied = true;
                }

                if (!satisfied)
                    continue;

                if (this.FileToInstancesMap.ContainsKey(fi.FullName))
                {
                    instances = this.FileToInstancesMap[fi.FullName];
                }
                else
                {
                    instances = 0;
                    count++;
                    ListViewItem item = lvFiles.Items.Add(count.ToString());
                    item.SubItems.Add(fi.FullName);
                    item.SubItems.Add(string.Empty);
                }
                instances++;
                this.FileToInstancesMap[fi.FullName] = instances;
            }

            if (recursive)
            {
                foreach (System.IO.DirectoryInfo diSub in di.GetDirectories())
                {
                    WalkDirAddFiles(diSub.FullName, recursive);
                }
            }
        }

        public void AddFilesInFolder()
        {
            FolderBrowserDialog dlgFolderBrowser = new FolderBrowserDialog();
            dlgFolderBrowser.SelectedPath = this.LastSelectedFolder;
            dlgFolderBrowser.ShowNewFolderButton = false;
            if (dlgFolderBrowser.ShowDialog() != DialogResult.OK)
                return;
            this.LastSelectedFolder = dlgFolderBrowser.SelectedPath;
            WalkDirAddFiles(dlgFolderBrowser.SelectedPath, true);

            btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;

            if (this.Changed != null)
                Changed(this, new EventArgs());
        }

        public void RemoveSelectedFiles()
        {
            if (lvFiles.SelectedItems.Count <= 0)
                return;
            foreach (ListViewItem item in lvFiles.SelectedItems)
            {
                this.FileToInstancesMap.Remove(item.SubItems[1].Text);
                lvFiles.Items.Remove(item);
            }
            btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;

            if (this.Changed != null)
                Changed(this, new EventArgs());
        }

        public void RemoveAllFiles()
        {
            if (MessageBox.Show("Are you sure?", "Remove All Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            lvFiles.Items.Clear();
            FileToInstancesMap.Clear();
            btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
            btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;

            if (this.Changed != null)
                Changed(this, new EventArgs());
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            this.AddFiles();
        }

        private void btnAddFilesInFolder_Click(object sender, EventArgs e)
        {
            this.AddFilesInFolder();
        }

        private void btnRemoveFiles_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedFiles();
        }

        private void btnRemoveAllFiles_Click(object sender, EventArgs e)
        {
            this.RemoveAllFiles();
        }

        private void ArrangeDestinations()
        {
            DestinationsPanel.Controls.Clear();
            int count = 1;
            int iTop = 0;
            this.lvFiles.Width = this.Width - btnAddFiles.Width - this.ControlsHorizontalSpacing;
            this.lvFiles.Height = this.Height - (this.DestinationsVisibleItems * this.DestinationHeight) - this.ControlsVerticalSpacing;
            btnAddDestination.Top = lvFiles.Top + lvFiles.Height + this.ControlsVerticalSpacing;
            btnAddDestination.Left = btnAddFiles.Left;
            btnAddDestination.Width = btnAddFiles.Width;
            btnAddDestination.Height = (this.DestinationsVisibleItems * this.DestinationHeight);
            DestinationsPanel.Left = lvFiles.Left;
            DestinationsPanel.Top = lvFiles.Top + lvFiles.Height + this.ControlsVerticalSpacing;
            DestinationsPanel.Width = lvFiles.Width;
            DestinationsPanel.Height = (this.DestinationsVisibleItems * this.DestinationHeight);
            chbOverwriteFiles.Left = btnRemoveAllFiles.Left;
            chbOverwriteFiles.Top = lvFiles.Bottom - chbOverwriteFiles.Height;
            foreach (DestinationItem destination in this.Destinations)
            {
                destination.Title = string.Format("Destination {0}", count++);
                destination.Left = lvFiles.Left;
                destination.Top = iTop;
                if (this.Destinations.Count > this.DestinationsVisibleItems)
                    destination.Width = lvFiles.Width - 20;
                else
                    destination.Width = lvFiles.Width;

                destination.Height = this.DestinationHeight;
                destination.Parent = DestinationsPanel;
                DestinationsPanel.Controls.Add(destination);
                iTop += this.DestinationHeight;
            }
        }

        private void FileListEditor_Load(object sender, EventArgs e)
        {
            if(this.Destinations.Count <= 0)
            {
                for (int i = 0; i < this.DestinationsVisibleItems; i++)
                {
                    DestinationItem dest = new DestinationItem();
                    dest.Parent = DestinationsPanel;
                    dest.CustomButtonText = "x";
                    dest.HasCustomButton = true;
                    this.Destinations.Add(dest);

                    dest.BrowseButtonClicked += new EventHandler(DestinationBrowserButton_Click);
                    dest.CustomButtonClicked += new EventHandler(DestinationDeleteButton_Click);
                    dest.CopyButtonClicked += new EventHandler(DestinationCopyButton_Click);
                    dest.PathTextChanged += new EventHandler(DestinationPath_Changed);
                }
                ArrangeDestinations();

                if (this.Changed != null)
                    this.Changed(this, new EventArgs());
            }
        }

        private void FileListEditor_SizeChanged(object sender, EventArgs e)
        {
            ArrangeDestinations();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddFiles();
        }

        private void addFilesInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddFilesInFolder();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedFiles();
        }

        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveAllFiles();
        }

        private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selItem = lvFiles.SelectedItems[0];
            if(selItem != null)
            {
                string filePath = selItem.SubItems[1].Text;
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("The file is not exist", "Show In Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Process proc = new Process();
                proc.StartInfo = new ProcessStartInfo("explorer.exe");
                proc.StartInfo.Arguments = string.Format("/select,{0}", filePath);
                proc.Start();
            }
        }

        private void filesListContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(lvFiles.SelectedIndices.Count <= 0)
            {
                removeToolStripMenuItem.Enabled = false;
            }
            else
            {
                removeToolStripMenuItem.Enabled = true;
            }

            if (lvFiles.Items.Count <= 0)
            {
                removeAllToolStripMenuItem.Enabled = false;
            }
            else
            {
                removeAllToolStripMenuItem.Enabled = true;
            }

            if (lvFiles.SelectedIndices.Count != 1)
            {
                showInExplorerToolStripMenuItem.Enabled = false;
            }
            else
            {
                showInExplorerToolStripMenuItem.Enabled = true;
            }
        }

        private void FileListEditor_Resize(object sender, EventArgs e)
        {
            ArrangeDestinations();
        }

        private void DestinationBrowserButton_Click(object sender, EventArgs e)
        {
            DestinationItem dest = sender as DestinationItem;
            if (dest == null)
                return;

            FolderBrowserDialog dlgFolder = new FolderBrowserDialog();
            dlgFolder.SelectedPath = dest.Path;
            if (dlgFolder.ShowDialog() == DialogResult.OK)
                dest.Path = dlgFolder.SelectedPath;
        }

        private void DestinationDeleteButton_Click(object sender, EventArgs e)
        {
            if(this.Destinations.Count<=1)
            {
                MessageBox.Show("The only destination cannot be deleted", "Remove Destination", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Destinations.Remove(sender as DestinationItem);
            ArrangeDestinations();

            if (this.Changed != null)
                this.Changed(this, new EventArgs());
        }

        private void DestinationCopyButton_Click(object sender, EventArgs e)
        {
            if(lvFiles.Items.Count <= 0)
            {
                MessageBox.Show("There is no file to copy", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DestinationItem destination = sender as DestinationItem;
            if (destination != null)
            {
                if(!System.IO.Directory.Exists(destination.Path))
                {
                    if(MessageBox.Show(string.Format("{0} is not exist\nDo you want to create it now?", destination.Path), "Copy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(destination.Path);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!System.IO.Directory.Exists(destination.Path))
                        return;
                }

                string srcFilePath = string.Empty;
                System.IO.FileInfo fi = null;
                string destFilePath = string.Empty;
                foreach (ListViewItem item in lvFiles.Items)
                {
                    srcFilePath = item.SubItems[1].Text;
                    item.SubItems[2].Text = string.Empty;
                    try
                    {
                        fi = new FileInfo(srcFilePath);
                    }
                    catch (System.Exception ex1)
                    {
                        Debug.Print(ex1.Message);
                        item.SubItems[2].Text = ex1.Message;
                    }
                    if(fi != null)
                    {
                        destFilePath = System.IO.Path.Combine(destination.Path, fi.Name);
                        try
                        {
                            System.IO.File.Copy(srcFilePath, destFilePath, chbOverwriteFiles.Checked);
                            item.SubItems[2].Text = "OK";
                        }
                        catch (System.Exception ex2)
                        {
                            item.SubItems[2].Text = ex2.Message;
                        }
                    }
                }
            }
        }

        private void DestinationPath_Changed(object sender, EventArgs e)
        {
            if(this.Changed != null) 
                this.Changed(this, new EventArgs());
        }

        private void btnAddDestination_Click(object sender, EventArgs e)
        {
            DestinationItem dest1 = new DestinationItem();
            dest1.Parent = DestinationsPanel;
            dest1.CustomButtonText = "x";
            dest1.HasCustomButton = true;
            this.Destinations.Add(dest1);
            dest1.BrowseButtonClicked += new EventHandler(DestinationBrowserButton_Click);
            dest1.CustomButtonClicked += new EventHandler(DestinationDeleteButton_Click);
            dest1.CopyButtonClicked += new EventHandler(DestinationCopyButton_Click);
            dest1.PathTextChanged += new EventHandler(DestinationPath_Changed);

            ArrangeDestinations();

            if (this.Changed != null)
                Changed(this, new EventArgs());
        }

        private void lvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void lvFiles_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];
                int instances = 0;
                int count = lvFiles.Items.Count;
                bool satisfied = false;
                System.IO.FileInfo fi = null;
                foreach (string filePath in paths)
                {
                    satisfied = false;
                    Debug.Print(filePath);

                    try
                    {
                        fi = new FileInfo(filePath);
                    }
                    catch(System.Exception ex)
                    {
                        Debug.Print(ex.Message);
                        continue;
                    }

                    if ((fi.Attributes & FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                    {
                        this.WalkDirAddFiles(filePath, true);
                    }
                    else
                    {

                        if (this.FileFilters != null && this.FileFilters.Count > 0)
                        {
                            foreach (IFilter filter in this.FileFilters)
                            {
                                if (filter.Enabled && filter.Filter(filePath))
                                {
                                    satisfied = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            satisfied = true;
                        }

                        if (!satisfied)
                            continue;

                        if (this.FileToInstancesMap.ContainsKey(filePath))
                        {
                            instances = this.FileToInstancesMap[filePath];
                        }
                        else
                        {
                            instances = 0;
                            count++;
                            ListViewItem item = lvFiles.Items.Add(count.ToString());
                            item.SubItems.Add(filePath);
                            item.SubItems.Add(string.Empty);
                        }
                        instances++;
                        this.FileToInstancesMap[filePath] = instances;

                        
                    }
                }

                btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
                btnRemoveAllFiles.Enabled = lvFiles.Items.Count > 0;

                if (this.Changed != null)
                    Changed(this, new EventArgs());
            }
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveFiles.Enabled = lvFiles.SelectedIndices.Count > 0;
        }

        private void chbOverwriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Changed != null)
                Changed(this, new EventArgs());
        }
    }
}
