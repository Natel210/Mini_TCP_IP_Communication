using SharedLibrary.DesignPattern;
using SharedLibrary.Object.Enum;
using SharedLibrary.Object.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.Path
{
    internal partial class PathManager : ISingleton<PathManager>, IObjManagerForm<UserPath, string>
    {
    }
    internal partial class PathManager /* I IObjManagerForm<UserPath> */
    {
        private Dictionary<string, UserPath> _paths = new Dictionary<string, UserPath>();
        public void AddItem(UserPath userPath)
        {
            _paths.Add(userPath.Name, userPath);
        }
        public bool DelItem(UserPath userPath)
        {
            return _paths.Remove(userPath.Name);
        }
        public bool DelItem(string key)
        {
            return _paths.Remove(key);
        }
        public bool GetItem(string key, ref UserPath? outValue)
        {
            outValue = null;
            if (!_paths.ContainsKey(key))
                return false;
            outValue = _paths[key];
            return true;
        }

    }
    internal partial class PathManager /* ISingleton<PathManager> -> IObjBase */
    {
        public override string Name { get; } = nameof(PathManager);
        public override string ClassName { get; } = nameof(PathManager);
        public override string ClassCategory { get; } = ELibraryObjectType.Manager.ToString();
    }
}
