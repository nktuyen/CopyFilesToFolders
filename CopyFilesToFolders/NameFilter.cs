using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace CopyFilesToFolders
{
    public class NameFilter : IFilter
    {
        public string Wildcard { get; set; }
        public string Name { get;private set; }
        public string Title { get;private set;}
        public string Description { get;private set;}
        public bool Enabled { get; set; }
        public NameFilter(bool enabled = false)
        {
            this.Name = "NameFilter";
            this.Title = "Name Filter";
            this.Description = "Filter files by name";
            this.Wildcard = string.Empty;
            this.Enabled = enabled;
        }

        public bool Initialize()
        {
            return true;
        }

        public bool Filter(string filePath)
        {
            if (this.Wildcard == string.Empty || this.Wildcard == "*" || this.Wildcard == "*.*")
                return true;

            string[] wildcards = this.Wildcard.Split(':');
            string regexPattern = string.Empty;
            int nMatched = 0;
            foreach (string wc in wildcards)
            {
                regexPattern = Regex.Escape(wc).Replace(@"\*", ".*").Replace(@"\?", ".");
                regexPattern = "^" + regexPattern + "$";
               if(Regex.IsMatch(filePath, regexPattern, RegexOptions.IgnoreCase))
                    nMatched++;
            }
            return nMatched > 0;
        }

        public DialogResult ShowSettings(IWin32Window owner = null)
        {
            NameFilterSettingsForm frm = new NameFilterSettingsForm();
            frm.Wildcard = this.Wildcard;
            DialogResult res = frm.ShowDialog(owner);
            if (res == DialogResult.OK)
            {
                this.Wildcard = frm.Wildcard;
            }

            return res;
        }

        public bool SaveSettings(RegistryKey registryKey)
        {
            if (registryKey == null)
                return false;

            RegistryKey subKey = null;
            try
            {
                subKey = registryKey.OpenSubKey(this.Name, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if(subKey == null)
            {
                try
                {
                    subKey = registryKey.CreateSubKey(this.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            if (subKey == null)
                return false;

            try
            {
                subKey.SetValue("Wildcard", this.Wildcard);
                subKey.SetValue("Enabled", this.Enabled ? 1 : 0, RegistryValueKind.DWord);
                subKey.Close();
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }

            return true;
        }

        public bool LoadSettings(RegistryKey registryKey)
        {
            if (registryKey == null)
                return false;

            RegistryKey subKey = null;
            try
            {
                subKey = registryKey.OpenSubKey(this.Name, true);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }

            if (subKey == null)
            {
                try
                {
                    subKey = registryKey.CreateSubKey(this.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (System.Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            if (subKey == null)
                return false;

            object objWildcard = null;
            object objEnabled = null;
            try
            {
                objWildcard = subKey.GetValue("Wildcard") as string;
                objEnabled = subKey.GetValue("Enabled");
                subKey.Close();

                if (objWildcard != null)
                    this.Wildcard = objWildcard as string;
                if (objEnabled != null)
                    this.Enabled = (int)objEnabled != 0;
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }

            return true;
        }
    }
}