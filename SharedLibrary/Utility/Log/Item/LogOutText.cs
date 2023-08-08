using SharedLibrary.Utility.Log.Base;
using SharedLibrary.Utility.Log.Enum;
using SharedLibrary.Utility.Log.Item;
using SharedLibrary.Utility.Path;
using System.IO;

namespace SharedLibrary.Utility.Log.Item
{
    internal partial class LogOutText : ALogBase, UserPath
    {
        //private string _rootPath;
        private UserPath _userPath;
        private readonly object _fileLock = new object();
        private readonly Mutex _fileMutex = new Mutex();
        //private string makeFileName()
        //{
        //    return Name + ".log";
        //}

    }
    internal partial class LogOutText /*IPathBase*/
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

        public FileAttributes FileAttributes { get; set; }

        public bool CheckDirectory()
        {

        }
        public bool CheckFile()
        {

        }
        public bool CreateDirectory()
        { 
        }
        public bool CreateFile()
        {

        }
    }
    internal partial class LogOutText /*ALogBase*/
    {
        public override void AddString(string str)
        {
            lock (_listString)
                _listString.Add(str);

            if (AutoExec)
                Exec();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="path">위치경로</param>
        internal LogOutText(string logName, string rootPath) : base(ELogType.Text, logName)
        {
            _userPath = new UserPath(logName);
            _userPath.FillPath(rootPath, logName, "log");
        }
    }
    internal partial class LogOutText /*ALogBase -> ILogItemForm*/
    {
        public override void Exec()
        {
            var fullPath = _rootPath + @"\" + makeFileName();
            // empty path/dir return.
            if (string.IsNullOrEmpty(_rootPath))
                return;
            var directory = Path.GetDirectoryName(_rootPath);
            if (string.IsNullOrEmpty(directory))
                return;


            string[] tempArryString;
            lock (_listString)
                tempArryString = _listString.ToArray();

            if (0 == tempArryString.Count())
                return;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(fullPath))
                File.WriteAllText(fullPath, string.Empty);

            bool fileOpened = false;
            try
            {
                // 뮤텍스를 시도하고 대기 (최대 10초 대기)
                fileOpened = _fileMutex.WaitOne(TimeSpan.FromSeconds(10));

                // 뮤텍스를 얻지 못한 경우 처리 (예: 대기 시간이 초과한 경우)
                // 기본적으로 대기하는 로직을 구현하거나 예외를 던지도록 처리할 수 있습니다.
                if (!fileOpened)
                    return;

                lock (_fileLock)
                {
                    if (File.Exists(fullPath))
                    {
                        using (var writer = new StreamWriter(fullPath, true))
                        {
                            foreach (var item in tempArryString)
                                writer.WriteLine(item);
                        }
                    }
                }

            }
            finally
            {
                if (fileOpened)
                    _fileMutex.ReleaseMutex();
            }

            lock (_listString)
                _listString.Clear();
        }
    }
    internal partial class LogOutText /*ALogBase -> IObjBase*/
    {
        public override string ClassName { get; } = nameof(LogOutText);
    }
}
