using SharedLibrary.DesignPattern;
using SharedLibrary.Object;
using SharedLibrary.Object.Base;
using SharedLibrary.Object.Log;
using SharedLibrary.Utility.Log.Item;
using SharedLibrary.Utillity.Log.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.Log
{
    /// <summary>
    /// 싱글톤, 추상 팩토리 매서드
    /// </summary>
    public partial class LogManager : ISingleton<LogManager>, IObjBase
    {
        private Dictionary<string, ILogItem> _logs = new Dictionary<string, ILogItem>();
        public ILogItem ConsoleLog { get; internal set; }
        public ILogItem DebugLog { get; internal set; }

        public LogManager()
        {
            ConsoleLog = new LogOutConsole("Console_Log");
            ConsoleLog.AutoExec = true;
            ConsoleLog.UseDate = true;
            _logs.Add("Console_Log", ConsoleLog);
            DebugLog = new LogOutDebug("Debug_Log");
            DebugLog.AutoExec = true;
            DebugLog.UseDate = true;
            _logs.Add("Debug_Log", DebugLog);
        }

        public bool CreateTextLog(string logName, string rootPath)
        {
            if (IsLog(logName))
                return false;
            LogOutText log = new LogOutText(logName, rootPath);
            _logs.Add(logName, log);
            return true;
        }
        public bool CreateConsoleLog(string logName)
        {
            if (IsLog(logName))
                return false;
            LogOutConsole log = new LogOutConsole(logName);
            _logs.Add(logName, log);
            return true;
        }
        public bool CreateDebugLog(string logName)
        {
            if (IsLog(logName))
                return false;
            LogOutDebug log = new LogOutDebug(logName);
            _logs.Add(logName, log);
            return true;
        }

        public bool CreateTextLog_inntervalDate(string logName, string rootPath)
        {
            if (IsLog(logName))
                return false;
            LogOutText_intervalDate log = new LogOutText_intervalDate(logName, rootPath);
            _logs.Add(logName, log);
            return true;
        }
        public bool IsLog(string key)
        {
            return _logs.ContainsKey(key);
        }

        public bool DeleteLog(string key)
        {
            if (!IsLog(key))
                return false;
            _logs[key].ClearString();
            return _logs.Remove(key);
        }

        public ILogItem? GetLog(string key)
        {
            if (!IsLog(key))
                return null;
            _logs.TryGetValue(key, out var log);
            return log;
        }

    }

    public partial class LogManager
    {
        //IObjBase 구현
        public override string Name { get; } = nameof(ObjectManager);
        public override string ClassName { get; } = nameof(ObjectManager);
    }
}
