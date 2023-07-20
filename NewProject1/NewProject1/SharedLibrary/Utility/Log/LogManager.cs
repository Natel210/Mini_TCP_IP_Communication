using SharedLibrary.Utility.Log.Base;
using SharedLibrary.Utility.Log.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.Log
{
    /// <summary>
    /// 싱글톤, 팩토리 매서드
    /// </summary>
    public static class LogManager
    {
        private static Dictionary<string, ILog> _dicLog = new Dictionary<string, ILog>();
        public static ILog ConsoleLog { get; internal set; }
        public static ILog DebugLog { get; internal set; }

        static LogManager()
        {
            ConsoleLog = new LogOutConsole("ConsoleLog");
            ConsoleLog.AutoExec = true;
            ConsoleLog.UseData = true;
            _dicLog.Add("ConsoleLog", ConsoleLog);
            DebugLog = new LogOutDebug("DebugLog");
            DebugLog.AutoExec = true;
            DebugLog.UseData = true;
            _dicLog.Add("DebugLog", DebugLog);
        }

        public static bool CreateTextLog(string logName, string rootPath)
        {
            if (IsLog(logName))
                return false;
            LogOutText log = new LogOutText(logName, rootPath);
            _dicLog.Add(logName, log);
            return true;
        }
        public static bool CreateConsoleLog(string logName)
        {
            if (IsLog(logName))
                return false;
            LogOutConsole log = new LogOutConsole(logName);
            _dicLog.Add(logName, log);
            return true;
        }
        public static bool CreateDebugLog(string logName)
        {
            if (IsLog(logName))
                return false;
            LogOutDebug log = new LogOutDebug(logName);
            _dicLog.Add(logName, log);
            return true;
        }

        public static bool IsLog(string key)
        {
            return _dicLog.ContainsKey(key);
        }

        public static bool DeleteLog(string key)
        {
            if (!IsLog(key))
                return false;
            return _dicLog.Remove(key);
        }

        public static ILog? GetLog(string key)
        {
            if (!IsLog(key))
                return null;
            _dicLog.TryGetValue(key, out var log);
            return log;
        }

    }
}
