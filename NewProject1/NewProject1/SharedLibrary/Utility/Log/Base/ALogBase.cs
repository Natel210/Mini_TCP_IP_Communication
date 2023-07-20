using System.Xml.Linq;

namespace SharedLibrary.Utility.Log.Base
{
    internal abstract class ALogBase : ILog
    {
        public ELogType Type { get; protected set; }
        public string Name { get; protected set; }
        private bool _autoExec = false;
        public bool AutoExec
        {
            get => _autoExec;
            set
            {
                _autoExec = value;
                if (value)
                    Exec();
            } 
        }
        public bool UseData { get; set; }


        protected List<string> _listString = new List<string>();

        public string[] GetStringArry()
        {
            string[] resultArry = new string[0];
            lock (_listString)
                resultArry = _listString.ToArray();
            return resultArry;
        }

        public void AddString(string str, ELogLevel eLogLevel = ELogLevel.None)
        {
            if (eLogLevel == ELogLevel.None && !UseData)
                AddString(str);
            else if (eLogLevel != ELogLevel.None && !UseData)
                AddString($"[{eLogLevel.ToString()}] " + str);
            else if (eLogLevel == ELogLevel.None && UseData)
                AddString($"[{DateTime.Today.ToString("yyyy/MM/dd")}][{DateTime.Now.ToString("HH:mm:ss:fff")}] " + str);
            else if (eLogLevel != ELogLevel.None && UseData)
                AddString($"[{eLogLevel.ToString()}][{DateTime.Today.ToString("yyyy/MM/dd")}][{DateTime.Now.ToString("HH:mm:ss:fff")}] " + str);
        }

        public abstract void AddString(string str);

        public abstract void Exec();

        private ALogBase()
        {
            Type = ELogType.None;
            Name = "";
        }

        protected ALogBase(ELogType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
