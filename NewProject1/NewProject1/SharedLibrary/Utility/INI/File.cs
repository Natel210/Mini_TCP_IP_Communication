using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.INI
{
    public class File
    {
        // Marshalling.
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string? key, string? val, string filepath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string? def, StringBuilder reVal, int size, string filepath);

        public string Path { get; set; }

        internal File()
        {
        }

        public File(string path)
        {
            Path = path;
        }

        public bool CheckFile()
        {
            return System.IO.File.Exists(Path);
        }

        public void CreateFile()
        {
            var info = new System.IO.FileInfo(Path);
            info.Create().Close();
            //info.Attributes = FileAttributes.ReadOnly;
            info.Attributes |= FileAttributes.Hidden;
        }

        public void DeleteFile()
        {
            new System.IO.FileInfo(Path).Delete();
        }

        public void DeleteValue(string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, "", Path);
        }

        public void DeleteKey(string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, null, Path);
        }

        public void DeleteSection(string Section)
        {
            WritePrivateProfileString(Section, null, null, Path);
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        public string? ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            if (0 != GetPrivateProfileString(Section, Key, null, temp, 255, Path))
                return temp.ToString();
            return null;
        }
    }
}
