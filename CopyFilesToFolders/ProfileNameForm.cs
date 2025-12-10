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
        public string ProfileName { get; set; }
        public ProfileNameForm()
        {
            InitializeComponent();
        }

        private void txtProfileName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = txtProfileName.TextLength > 0;
        }

        private void ProfileNameForm_Load(object sender, EventArgs e)
        {
            txtProfileName.Text = this.ProfileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ProfileName = txtProfileName.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
