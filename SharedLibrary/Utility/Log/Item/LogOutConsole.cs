using SharedLibrary.Utility.Log.Base;
using SharedLibrary.Utility.Log.Enum;
using System.Diagnostics;

namespace SharedLibrary.Utility.Log.Item
{
    internal partial class LogOutConsole : ALogBase
    {
    }
    internal partial class LogOutConsole /*ALogBase*/
    {
        public override void AddString(string str)
        {
            lock (_listString)
                _listString.Add(str);
            if (AutoExec)
                Exec();
        }
        internal LogOutConsole(string logName) : base(ELogType.Console, logName) {}
    }
    internal partial class LogOutConsole /*ALogBase -> ILogItemForm*/
    {
        public override void Exec()
        {
            if (0 == _listString.Count)
                return;
            string[] tempArryString;
            lock (_listString)
                tempArryString = _listString.ToArray();
            if (0 == tempArryString.Count())
                return;
            foreach (var item in tempArryString)
                Console.WriteLine(item);
            lock (_listString)
                _listString.Clear();
        }
    }
    internal partial class LogOutConsole /*ALogBase -> IObjBase*/
    {
        public override string ClassName { get; } = nameof(LogOutConsole);
    }
}
