using Microsoft.VisualBasic;
using SharedLibrary.Object.Base;
namespace SharedLibrary.Object.Log
{
    public interface ILogItem
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
