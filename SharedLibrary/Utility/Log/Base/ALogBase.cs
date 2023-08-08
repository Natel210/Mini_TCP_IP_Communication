using SharedLibrary.Object;
using SharedLibrary.Object.Base;
using SharedLibrary.Object.Enum;
using SharedLibrary.Utility.Log.Enum;
using SharedLibrary.Utility.Log.Form;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace SharedLibrary.Utility.Log.Base
{
    internal abstract partial class ALogBase : ILogItemForm, IObjBase
    {
        protected List<string> _listString = new List<string>();
        public abstract void AddString(string str);
    }
    internal abstract partial class ALogBase /*ILogItemForm*/
    {
        public ELogType Type { get; protected set; }
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
        public bool UseDate { get; set; }
        public bool UseTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="eLogLevel">ELogLevel.None 설정 시 Level 카테고리 제외</param>
        public void AddString(string str, ELogLevel eLogLevel = ELogLevel.None)
        {
            string tempString = string.Empty;
            if (UseDate)
                tempString += "[" + DateTime.Today.ToString("yyyy/MM/dd") + "]\t";
            if (UseTime)
                tempString += "[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]\t";
            if (eLogLevel != ELogLevel.None)
                tempString += "[" + eLogLevel.ToString() + "]\t";
            tempString += str;
            AddString(tempString);
        }
        public string[] GetStringArry()
        {
            string[] resultArry = new string[0];
            lock (_listString)
                resultArry = _listString.ToArray();
            return resultArry;
        }
        public void ClearString()
        {
            _listString.Clear();
        }
        public abstract void Exec();
    }
    internal abstract partial class ALogBase /*IObjBase*/
    {
        public string Name { get; } = nameof(ALogBase);
        public abstract string ClassName { get; }
        public string ClassCategory { get; } = ELibraryObjectType.Log.ToString();
    }
    internal abstract partial class ALogBase /*abstract*/
    {
        private ALogBase()
        {
            Type = ELogType.None;
        }

        protected ALogBase(ELogType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
