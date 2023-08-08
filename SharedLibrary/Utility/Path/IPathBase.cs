using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.Path
{
    public interface IPathBase
    {
        string Directory { get; }
        string FileName { get; }
        string Extension { get; }

        FileAttributes FileAttributes { get; }

        bool CheckDirectory();
        bool CheckFile();
        bool CreateDirectory();
        bool CreateFile();
        bool DeleteFile();
        bool DeleteDirectory();
        void FillPath(string fullPath);
        void FillPath(string directory, string filename, string extension);
    }
}
