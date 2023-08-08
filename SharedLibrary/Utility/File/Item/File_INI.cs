using SharedLibrary.Utility.Path;
using System.Runtime.InteropServices;
using System.Text;

namespace SharedLibrary.Utility.File.INI
{
    public partial class File_INI : UserPath
    {
        
        //public string Path { get; set; } = string.Empty;

        // Marshalling.
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string? key, string? Val, string filePath);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string? value, StringBuilder result, int size, string filePath);

        private File_INI()
        {
        }
        public File_INI(string name) : base(name)
        {
        }
        public File_INI(string name, string fullPath) : base(name,fullPath)
        {
        }
        public File_INI(string name, string directory, string filename, string extension) : base(name, directory, filename, extension)
        {
        }

        public override bool CreateFile()
        {
            bool result = base.CreateFile();
            if (result is true)
                FileAttributes |= FileAttributes.Hidden;
            return result;
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

    public partial class File_INI /*IObjBase*/
    {
        public override string ClassName { get; } = nameof(File_INI);
    }
}
