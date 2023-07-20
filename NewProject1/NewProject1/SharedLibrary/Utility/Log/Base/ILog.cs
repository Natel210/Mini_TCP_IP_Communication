namespace SharedLibrary.Utility.Log.Base
{
    public interface ILog
    {
        public ELogType Type { get; }
        public bool AutoExec { get; set; }
        public bool UseData { get; set; }

        public void AddString(string str, ELogLevel eLogLevel);
        public string[] GetStringArry();
        /// <summary>
        /// execution.
        /// </summary>
        public void Exec();
    }
}
