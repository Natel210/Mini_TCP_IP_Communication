using SharedLibrary.Utility.Log.Base;
using System.Diagnostics;

namespace SharedLibrary.Utility.Log.Item
{
    internal class LogOutDebug : ALogBase
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
            if (_listString.Any())
                return;

            string[] tempArryString;
            lock (_listString)
                tempArryString = _listString.ToArray();

            foreach (var item in tempArryString)
                Debug.WriteLine(item);

            lock (_listString)
                _listString.Clear();
        }

        internal LogOutDebug(string logName) : base(ELogType.Debug, logName)
        {

        }
    }
}
