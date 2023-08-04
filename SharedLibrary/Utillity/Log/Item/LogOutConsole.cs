using SharedLibrary.Object.Log;
using SharedLibrary.Utility.Log.Base;
using System.Diagnostics;

namespace SharedLibrary.Utility.Log.Item
{
    internal class LogOutConsole : ALogBase
    {
        public override void AddString(string str)
        {
            lock (_listString)
                _listString.Add(str);
            if (AutoExec)
                Exec();
        }
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

        internal LogOutConsole(string logName) : base(ELogType.Console, logName)
        {

        }
    }
}
