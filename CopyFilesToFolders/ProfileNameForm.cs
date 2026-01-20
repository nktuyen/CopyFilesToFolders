using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFilesToFolders
{
    public partial class ProfileNameForm : Form
    {
        public string NewProfileName { get; set; }
        public string CopyingProfileName { get; set; }
        public List<string> AvailableProfiles { get; set; }
        public ProfileNameForm()
        {
            InitializeComponent();
            this.AvailableProfiles = null;
        }

        private void txtProfileName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = txtProfileName.TextLength > 0;
        }

        private void ProfileNameForm_Load(object sender, EventArgs e)
        {
            txtProfileName.Text = this.NewProfileName;
            cbProfiles.Items.Clear();
            if ((this.AvailableProfiles == null) || (this.AvailableProfiles.Count <= 0))
            {
                this.Size = new Size(436, 105);
                chbCopyingProfile.Visible = false;
                cbProfiles.Visible = false;
            }
            else
            {
                foreach (string profileName in this.AvailableProfiles)
                {
                    cbProfiles.Items.Add(profileName);
                }

                if(this.CopyingProfileName != null)
                {
                    int index = cbProfiles.FindString(this.CopyingProfileName);
                    if (index != -1)
                        cbProfiles.SelectedIndex = index;
                }

                if (cbProfiles.SelectedIndex == -1)
                {
                    if (cbProfiles.Items.Count > 0)
                        cbProfiles.SelectedIndex = 0;
                }

                chbCopyingProfile.Visible = true;
                cbProfiles.Visible = true;
                this.Size = new Size(436, 125);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.NewProfileName = txtProfileName.Text;
            this.CopyingProfileName = cbProfiles.Text;
            DialogResult = DialogResult.OK;
        }

        private void chbCopyingProfile_CheckedChanged(object sender, EventArgs e)
        {
            cbProfiles.Enabled = chbCopyingProfile.Checked;
        }
    }
}
