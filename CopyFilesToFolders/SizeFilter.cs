using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CopyFilesToFolders
{
    public enum SizeUnit { bytes = 0, KB, MB, GB, TB, PB };

    public class SizeFilter : IFilter
    {
        public long SizeFrom { get; set; }
        public long SizeTo { get; set; }
        public SizeUnit UnitFrom { get;set; }
        public SizeUnit UnitTo { get; set; }
        public string Name { get; private set; }
        public string Title { get;private set; }
        public string Description { get; private set; }
        public bool Enabled { get; set; }

        public SizeFilter()
        {
            this.Name = "SizeFilter";
            this.Title = "Size Filter";
            this.Description = "Filter files by size";
            this.SizeFrom = 0;
            this.SizeTo = -1;
            this.Enabled = false;
        }

        public bool Initialize()
        {
            return true;
        }

        public bool Filter(string filePath)
        {
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(filePath);
                if (fi.Length < this.SizeFrom)
                    return false;
                if (this.SizeTo >= 0)
                {
                    if (fi.Length > this.SizeTo)
                        return false;
                }
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
