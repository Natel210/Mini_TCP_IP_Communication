using SharedLibrary.Utility.Log.Enum;
namespace SharedLibrary.Utility.Log.Form
{
    public interface ILogUseForm
    {
        bool CreataLogs();
        bool DeleteLog();
        void Log(string message, ELogLevel level = ELogLevel.Info);
    }
}
