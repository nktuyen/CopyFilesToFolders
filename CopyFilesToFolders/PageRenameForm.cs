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
    public partial class PageRenameForm : Form
    {
        public string PageName { get; set; }
        public PageRenameForm(string name = "")
        {
            InitializeComponent();
            this.PageName = name;
        }

        private void PageRenameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) 
                this.Close();
        }

        private void PageRenameForm_Load(object sender, EventArgs e)
        {
            txtName.Text = this.PageName;
            txtName.SelectAll();
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = txtName.TextLength > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.PageName = txtName.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
