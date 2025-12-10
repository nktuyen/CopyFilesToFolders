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
    public partial class NameFilterSettingsForm : Form
    {
        public string Wildcard { get; set; }
        public NameFilterSettingsForm()
        {
            InitializeComponent();
            this.Wildcard = string.Empty;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Wildcard = txtWildcard.Text;
        }

        private void NameFilterSettingsForm_Load(object sender, EventArgs e)
        {
            txtWildcard.Text = this.Wildcard;
        }

        private void NameFilterSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
