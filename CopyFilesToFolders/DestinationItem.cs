using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFilesToFolders
{
    public partial class DestinationItem : UserControl
    {
        public event EventHandler PathTextChanged;
        public event EventHandler BrowseButtonClicked;
        public event EventHandler CopyButtonClicked;
        public event EventHandler CustomButtonClicked;

        public int LineHeight
        {
            get
            {
                return btnCopy.Height;
            }
            set
            {
                btnCopy.Height = value;
                lblPath.Height = value;
                txtFolderPath.Height = value;
                btnBrowse.Height = value;
                btnCustom.Height = value;

                ArrangeControls();
            }
        }

        public string Path
        {
            get
            {
                return txtFolderPath.Text;
            }
            set
            {
                txtFolderPath.Text = value;
                btnCopy.Enabled = txtFolderPath.TextLength > 0;
            }
        }

        public string Title
        {
            get
            {
                return lblPath.Text;
            }
            set
            {
                lblPath.Text = value;
                ArrangeControls();
            }
        }

        public bool HasCustomButton
        {
            get
            {
                return btnCustom.Visible;
            }
            set
            {
                btnCustom.Visible = value;
                ArrangeControls();
            }
        }

        public Image CustomButtonImage
        {
            get
            {
                return btnCustom.Image;
            }
            set
            {
                btnCustom.Image = value;
            }
        }

        public string CustomButtonText
        {
            get
            {
                return btnCustom.Text;
            }
            set
            {
                btnCustom.Text = value;
            }
        }

        public DestinationItem()
        {
            InitializeComponent();
            this.LineHeight = 24;

        }

        private void ArrangeControls()
        {
            const int SPACING = 1;
            int CUSTOM = 0;
            if (this.HasCustomButton)
                CUSTOM = btnCustom.Width;

            lblPath.Location = new Point(SPACING, this.Height / 2 - lblPath.Height / 2);
            txtFolderPath.Location = new Point(lblPath.Left + lblPath.Width + SPACING, this.Height / 2 - txtFolderPath.Height / 2);
            txtFolderPath.Size = new System.Drawing.Size(this.Width - SPACING - btnCopy.Width - SPACING - btnBrowse.Width - SPACING - lblPath.Width - SPACING-CUSTOM, txtFolderPath.Height);
            btnBrowse.Location = new Point(txtFolderPath.Left + txtFolderPath.Width + SPACING, this.Height / 2 - btnBrowse.Height / 2);
            btnCopy.Location = new Point(btnBrowse.Left + btnBrowse.Width + SPACING, this.Height / 2 - btnCopy.Height / 2);
            btnCustom.Location = new Point(btnCopy.Left + btnCopy.Width + SPACING, this.Height / 2 - btnCustom.Height / 2);
        }

        private void CopyUI_Resize(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void CopyUI_Load(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = txtFolderPath.TextLength > 0;

            if (this.PathTextChanged != null)
                this.PathTextChanged(this, e);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.BrowseButtonClicked != null)
                this.BrowseButtonClicked(this, e);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (this.CopyButtonClicked != null)
                this.CopyButtonClicked(this, e);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            if (this.CustomButtonClicked != null)
                this.CustomButtonClicked(this, e);
        }
    }
}
