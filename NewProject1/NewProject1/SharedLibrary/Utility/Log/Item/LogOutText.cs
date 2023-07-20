using SharedLibrary.Utility.Log.Base;


namespace SharedLibrary.Utility.Log.Item
{
    internal class LogOutText : ALogBase
    {
        private string _rootPath;
        private readonly object _fileLock = new object();
        private readonly Mutex _fileMutex = new Mutex();

        public override void AddString(string str)
        {
            lock (_listString)
                _listString.Add(str);

            if (AutoExec)
                Exec();
        }
        private string makeFileName()
        {
            return Name + "_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
        }

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

            if (tempArryString.Any())
                return;

            if (Directory.Exists(directory))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="path">위치경로</param>
        internal LogOutText(string logName, string rootPath) : base(ELogType.Text, logName)
        {
            _rootPath = rootPath;
        }

    }
}
