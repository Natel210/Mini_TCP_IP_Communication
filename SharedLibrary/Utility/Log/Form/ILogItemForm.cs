using Microsoft.VisualBasic;
using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Log.Enum;

namespace SharedLibrary.Utility.Log.Form
{
    public interface ILogItemForm
    {
        ELogType Type { get; }

        bool AutoExec { get; set; }

        bool UseDate { get; set; }
        bool UseTime { get; set; }

        void AddString(string str, ELogLevel eLogLevel);
        string[] GetStringArry();

        void ClearString();
        /// <summary>
        /// execution.
        /// </summary>
        void Exec();
    }
}
