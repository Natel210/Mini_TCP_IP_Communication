using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Windows.Input;

namespace CommunicationApp.Utility
{
    // Func.
    public partial class IniFile
    {
        internal bool CheckFile()
        {
            return File.Exists(FilePath);
        }
        internal void CreateFile()
        {
            var info = new FileInfo(FilePath);
            info.Create().Close();
            //info.Attributes = FileAttributes.ReadOnly;
            info.Attributes |= FileAttributes.Hidden;
        }
        internal void DeleteFile()
        {
            new FileInfo(FilePath).Delete();
        }

        internal void IniDeleteValue(string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, "", FilePath);
        }
        internal void IniDeleteKey(string Section, string Key)
        {
            WritePrivateProfileString(Section, Key, null, FilePath);
        }
        internal void IniDeleteValue(string Section)
        {
            WritePrivateProfileString(Section, null, null, FilePath);
        }
        internal void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, FilePath);
        }

        internal string IniReadValue(string Section, string Key)
        {

            StringBuilder temp = new StringBuilder(255);
            if (0 != GetPrivateProfileString(Section, Key, null, temp, 255, FilePath))
                return temp.ToString();
            return null;
        }
    }

    //
    public partial class IniFile
    {
        public IniFile()
        {
        }
        internal IniFile(string path)
        {
            FilePath = path;
        }
    }

    // Value.
    public partial class IniFile
    {
        public string FilePath { get => _iniPath; set => _iniPath = value; }

        private string _iniPath = null;
    }

    // Marshalling.
    public partial class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filepath);
    }
}
