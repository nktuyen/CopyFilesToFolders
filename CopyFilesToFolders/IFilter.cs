using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CopyFilesToFolders
{
    public interface IFilter
    {
        string Name { get; }
        string Title { get; }
        string Description { get; }
        bool Enabled { get; set; }
        bool Initialize();
        bool Filter(string filePath);
    }
}
