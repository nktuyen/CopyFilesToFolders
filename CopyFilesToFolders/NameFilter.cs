using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CopyFilesToFolders
{
    public class NameFilter : IFilter
    {
        public string Wildcard { get; set; }
        public string Name { get;private set; }
        public string Title { get;private set;}
        public string Description { get;private set;}
        public bool Enabled { get; set; }
        public NameFilter()
        {
            this.Name = "NameFilter";
            this.Title = "Name Filter";
            this.Description = "Filter files by name";
            this.Wildcard = string.Empty;
            this.Enabled = false;
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
    }
}