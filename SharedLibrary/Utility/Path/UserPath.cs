using SharedLibrary.Object.Base;
using SharedLibrary.Object.Enum;
using System.IO;

namespace SharedLibrary.Utility.Path
{
    public partial class UserPath : IObjBase, IPathBase
    {
        private bool IsDirectory => string.IsNullOrEmpty(Extension);
        private string FullPath => IsDirectory ? System.IO.Path.Combine(Directory, FileName) : System.IO.Path.Combine(Directory, FileName + "." + Extension);
        public bool CheckPath()
        {
            return IsDirectory ? CheckDirectory() : CheckFile();
        }
        public bool CreatePath()
        {
            if (CheckPath())
                return false;

            if (IsDirectory)
                return CreateDirectory();
            else
                return CreateFile();
        }
        public bool DeletePath()
        {
            if (!CheckPath())
                return false;
            if (IsDirectory)
                return DeleteDirectory();
            else
                return DeleteFile();
        }


        public UserPath(string name)
        {
            Name = name;
            PathManager.Instance.AddItem(this);
        }
        public UserPath(string name, string fullPath)
        {
            Name = name;
            FillPath(fullPath);
            PathManager.Instance.AddItem(this);
        }
        public UserPath(string name, string directory, string filename, string extension)
        {
            Name = name;
            FillPath(directory, filename, extension);
            PathManager.Instance.AddItem(this);
        }
        protected UserPath() { }

    }

    public partial class UserPath /*IFileBase*/
    {
        public string Directory { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;

        public FileAttributes FileAttributes
        {
            get
            {
                if (CheckPath())
                    return 0L;
                return new FileInfo(FullPath).Attributes;
            }
            set {
                if (CheckPath())
                    return;
                new FileInfo(FullPath).Attributes = value;
            }
        }

        public bool CheckDirectory()
        {
            if (string.IsNullOrEmpty(Directory))
                return false;
            return System.IO.Directory.Exists(FullPath);
        }
        public bool CheckFile()
        {
            if (string.IsNullOrEmpty(Directory))
                return false;

            if (IsDirectory)
                return System.IO.Directory.Exists(FullPath);
            else
                return System.IO.File.Exists(FullPath);
        }
        public bool CreateDirectory()
        {
            if (CheckDirectory())
                return false;
            return !System.IO.Directory.CreateDirectory(FullPath).Exists;
        }
        public virtual bool CreateFile()
        {
            if (CheckFile())
                return false;
            if (IsDirectory)
                return !System.IO.Directory.CreateDirectory(FullPath).Exists;
            System.IO.File.Create(FullPath);
            var info = new FileInfo(FullPath);
            if (!info.Exists)
                info.Create().Close();
            FileAttributes |= FileAttributes.Hidden;
            return true;
        }
        public bool DeleteDirectory()
        {
            if (!CheckDirectory())
                return false;
            System.IO.Directory.Delete(Directory);
            return true;
        }
        public bool DeleteFile()
        {
            if (!CheckFile())
                return false;
            System.IO.File.Delete(Directory);
            return true;
        }
        public void FillPath(string fullPath)
        {
            Directory = System.IO.Path.GetDirectoryName(fullPath) ?? string.Empty;
            FileName = System.IO.Path.GetFileName(fullPath) ?? string.Empty;
            Extension = System.IO.Path.GetExtension(fullPath) ?? string.Empty;
        }
        public void FillPath(string directory, string filename, string extension)
        {
            Directory = directory;
            FileName = filename;
            Extension = extension;
        }
    }

    public partial class UserPath /*IObjBase*/
    {
        public string Name { get; set; } = nameof(UserPath);
        public override string ClassName { get; } = nameof(UserPath);
        public string ClassCategory { get; } = ELibraryObjectType.File.ToString();
    }
}
