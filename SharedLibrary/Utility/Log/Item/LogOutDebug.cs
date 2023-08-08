using SharedLibrary.Utility.Log.Base;
using SharedLibrary.Utility.Log.Enum;
using System.Diagnostics;

namespace SharedLibrary.Utility.Log.Item
{
    internal partial class LogOutDebug : ALogBase
    {
    }
    internal partial class LogOutDebug /*ALogBase*/
    {
        public override void AddString(string str)
        {
            lock (_listString)
                _listString.Add(str);
            if (AutoExec)
                Exec();
        }
        internal LogOutDebug(string logName) : base(ELogType.Debug, logName) {}
    }
    internal partial class LogOutDebug /*ALogBase -> ILogItemForm*/
    {
        public override void Exec()
        {
            string[] tempArryString;
            lock (_listString)
                tempArryString = _listString.ToArray();
            if (0 == tempArryString.Count())
                return;
            foreach (var item in tempArryString)
                Debug.WriteLine(item);
            lock (_listString)
                _listString.Clear();
        }
    }
    internal partial class LogOutDebug /*ALogBase -> IObjBase*/
    {
        public override string ClassName { get; } = nameof(LogOutDebug);
    }
}
