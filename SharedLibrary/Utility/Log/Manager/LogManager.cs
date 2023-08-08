using SharedLibrary.DesignPattern;
using SharedLibrary.Object;
using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Log.Item;
using SharedLibrary.Utility.Log.Enum;
using SharedLibrary.Utility.Log.Form;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using SharedLibrary.Object.Enum;

namespace SharedLibrary.Utility.Log
{
    /// <summary>
    /// 싱글톤, 추상 팩토리 매서드
    /// </summary>
    public partial class LogManager : ISingleton<LogManager>, IObjBase
    {
        private Dictionary<string, ILogItemForm> _logs = new Dictionary<string, ILogItemForm>();

        public bool CreatLog(ELogType type, string logName, out ILogItemForm? log, string rootPath = "")
        {
            log = null;
            switch (type)
            {
                case ELogType.None:
#if DEBUG
                    throw new ArgumentException("not exist none type.");
#else
                    return false;
#endif
                case ELogType.Text:
                    log = CreateTextLog(logName, rootPath);
                    return true;
                case ELogType.Text_intervalDate:
                    log = CreateTextLog_intervalDate(logName, rootPath);
                    return true;
                case ELogType.Console:
#if CONSOLE_LOG_ENABLED
                    log = CreateConsoleLog(logName);
                    return true;
#else
#if DEBUG
                    throw new ArgumentException("Active Define \"CONSOLE_LOG_ENABLED\".");
#else
                    return false;
#endif
#endif
                case ELogType.Debug:
#if DEBUG_LOG_ENABLED
                    log = CreateDebugLog(logName);
                    return true;
#else
#if DEBUG
                    throw new ArgumentException("Active Define \"DEBUG_LOG_ENABLED\".");
#else
                    return false;
#endif
#endif
                default:
#if DEBUG
                    throw new ArgumentException("Invalid log type.");
#else
                    return false;
#endif
            }
        }

        public bool IsLog(ELogType type, string logName)
        {
            return _logs.ContainsKey(TypeParser(type, logName));
        }

        public bool DeleteLog(ELogType type, string logName)
        {
            var internalName = TypeParser(type, logName);
            if (!IsLog(type, logName))
                return false;
            _logs[internalName]?.ClearString();
            return _logs.Remove(internalName);
        }

        public bool FindLog(ELogType type, string logName, out ILogItemForm? log)
        {
            log = null;
            var internalName = TypeParser(type, logName);

            if (!IsLog(type, logName))
                return false;
            return _logs.TryGetValue(TypeParser(type, logName), out log);
        }

        private string TypeParser(ELogType type, string logName)
        {
            return string.Concat(logName, @"_Type_", type.ToString());
        }
    }

    public partial class LogManager /* Quick */
    {
#if CONSOLE_LOG_ENABLED
        static public ILogItem ConsoleLog { get; internal set; } = new LogOutConsole(string.Concat(@"Global_Log", @"_Type_", ELogType.Console.ToString())) { AutoExec = true, UseDate = true };
#endif
#if DEBUG_LOG_ENABLED
        static public ILogItem DebugLog { get; internal set; } = new LogOutDebug(string.Concat(@"Global_Log", @"_Type_", ELogType.Debug.ToString())) { AutoExec = true, UseDate = true };
#endif
    }

    public partial class LogManager /*Internal Log Items*/
    {
        private ILogItemForm CreateTextLog(string logName, string rootPath)
        {
            var type = ELogType.Text;
            var internalName = TypeParser(type, logName);
            if (IsLog(type, logName))
                return _logs[internalName];
            var log = new LogOutText(internalName, rootPath);
            _logs.Add(internalName, log);
            return log;
        }
#if CONSOLE_LOG_ENABLED
        private ILogItem CreateConsoleLog(string logName)
        {
            var type = ELogType.Console;
            var internalName = TypeParser(type, logName);
            if (IsLog(type, logName))
                return _logs[internalName];
            var log = new LogOutConsole(internalName);
            _logs.Add(internalName, log);
            return log;
        }
#endif
#if DEBUG_LOG_ENABLED
        private ILogItem CreateDebugLog(string logName)
        {
            var type = ELogType.Debug;
            var internalName = TypeParser(type, logName);
            if (IsLog(type, logName))
                return _logs[internalName];
            var log = new LogOutDebug(internalName);
            _logs.Add(internalName, log);
            return log;
        }
#endif
        private ILogItemForm CreateTextLog_intervalDate(string logName, string rootPath)
        {
            var type = ELogType.Text_intervalDate;
            var internalName = TypeParser(type, logName);
            if (IsLog(type, logName))
                return _logs[internalName];
            var log = new LogOutDebug(internalName);
            _logs.Add(internalName, log);
            return log;
        }
    }

    public partial class LogManager /*IObjBase*/
    {
        public override string Name { get; } = nameof(ObjectManager);
        public override string ClassName { get; } = nameof(ObjectManager);
        public override string ClassCategory { get; } = ELibraryObjectType.Manager.ToString();
    }

}
