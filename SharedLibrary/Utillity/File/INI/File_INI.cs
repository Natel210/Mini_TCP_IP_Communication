using SharedLibrary.Object.Base;
using SharedLibrary.Object.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utillity.File.INI
{
    public class File_INI : IObjBase
    {
        public string Name { get; private set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        // Marshalling.
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string? key, string? Val, string filePath);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string? value, StringBuilder result, int size, string filePath);

        private File_INI()
        {
        }

        public File_INI(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public bool CheckFile()
        {
            if (0 < Path.Length)
                return System.IO.File.Exists(Path);
            return false;
        }

        public void CreateFile()
        {
            var info = new FileInfo(Path);
            info.Create().Close();
            //info.Attributes = FileAttributes.ReadOnly;
            info.Attributes |= FileAttributes.Hidden;
        }

        public void DeleteFile()
        {
            new FileInfo(Path).Delete();
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
