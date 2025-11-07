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
    public partial class FiltersSettingsForm : Form
    {
        private List<IFilter> _filters = null;
        public FiltersSettingsForm(List<IFilter> filters)
        {
            InitializeComponent();
            this._filters = filters;
        }

        private void FiltersSettingsForm_Load(object sender, EventArgs e)
        {
            if (this._filters != null)
            {
                foreach (IFilter filter in this._filters)
                {
                    MainTab.TabPages.Add(filter.Title);
                }
            }
        }
    }
}
