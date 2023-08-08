using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Log;
using SharedLibrary.TCP_IP.Common;
using System.Xml.Linq;
using SharedLibrary.Utility.Log.Enum;
using SharedLibrary.Utility.Log.Form;

namespace SharedLibrary.TCP_IP.Server
{
    public abstract partial class ATCP_ServerBase : IObjBase, ILogUseForm
    {
        private TcpListener? _server;
        private int _port = TCP_Common.ServerPort; // 포트 번호
        protected Dictionary<nint, TcpClient> ConnectedClients { get; } = new Dictionary<nint, TcpClient>();

        public async Task Listen()
        {
            try
            {
                if (_server is null)
                    return;
                _server.Start();
                Log("서버 Start.");
                while (true)
                {
                    TcpClient client = await _server.AcceptTcpClientAsync(); // 비동기로 클라이언트 연결 대기
                    Log("클라이언트가 연결되었습니다.");
                    ConnectedClients.Add(client.Client.Handle, client); // 연결된 클라이언트를 리스트에 추가
                    _ = HandleClientAsync(client); // 비동기적으로 클라이언트 처리
                }
            }
            catch (Exception ex)
            {
                Log("Exception: " + ex.Message, ELogLevel.Error);
            }
            Log("서버 End.");
        }

        protected abstract Task HandleClientAsync(TcpClient client);

        protected abstract Task BroadcastMessageAsync(string message);





        protected ATCP_ServerBase(string name, int port = TCP_Common.ServerPort)
        {
            Name = name;
            _port = port;
            CreataLogs();
            _server = new TcpListener(IPAddress.Any, _port);
            _ = Listen();
        }
        private ATCP_ServerBase() { }
        ~ATCP_ServerBase()
        {
            DeleteLog();

        }
    }

    public abstract partial class ATCP_ServerBase /*IObjBase*/
    {
        public string Name { get; private set; } = "";
        public override string ClassName { get; } = nameof(ATCP_ServerBase);
        public override string ClassCategory { get; } = "TCP_Server";
    }

    public abstract partial class ATCP_ServerBase /*ILogInitForm*/
    {
        private ILogItemForm? _consolelog = null;
        private ILogItemForm? _textlog = null;

        public bool CreataLogs()
        {
            var SettingLog = (ILogItemForm? logItem) => {
                if (_consolelog is null)
                    return;
                _consolelog.AutoExec = true;
                _consolelog.UseDate = true;
                _consolelog.UseTime = true;
            };
            bool result = true;
            if (LogManager.Instance.CreateLog(ELogType.Console, Name, out ILogItemForm consoleLog))
                _consolelog = consoleLog;
            else
                result = false;
            SettingLog(_consolelog);
            if (LogManager.Instance.CreateLog(ELogType.Text_intervalDate, Name, out ILogItemForm textlog))
                _textlog = textlog;
            else
                result = false;
            SettingLog(_textlog);
            return result;
        }
        public bool DeleteLog()
        {
            bool reverseResult = false;
            reverseResult |= !LogManager.Instance.DeleteLog(ELogType.Console, Name);
            reverseResult |= !LogManager.Instance.DeleteLog(ELogType.Console, Name);
            return !reverseResult;
        }
        public void Log(string message, ELogLevel level = ELogLevel.Info)
        {
            _consolelog?.AddString(msg, level);
            _textlog?.AddString(msg, level);
        }
    }
}







//    public abstract partial class ATCP_ServerBase : IObjBase, ILogInitForm
//    {
//        //protected void Log(string msg, ELogLevel level = ELogLevel.Info)
//        //{
//        //    _consolelog?.AddString(msg, level);
//        //    _textlog?.AddString(msg, level);
//        //}
//        //private void CreataLogs()
//        //{
//        //    if (LogManager.Instance.CreateConsoleLog($"{Name}_Log_Console"))
//        //        _consolelog = LogManager.Instance.GetLog($"{Name}_Log_Console");
//        //    if (_consolelog is not null)
//        //    {
//        //        _consolelog.AutoExec = true;
//        //        _consolelog.UseDate = true;
//        //        _consolelog.UseTime = true;
//        //    }
//        //    if (LogManager.Instance.CreateTextLog_inntervalDate($"{Name}_Text",$"./TCPIP_Server_Log/{Name}/Port{_port}/"))
//        //        _textlog = LogManager.Instance.GetLog($"{Name}_Text");
//        //    if (_textlog is not null)
//        //    {
//        //        _textlog.AutoExec = true;
//        //        _textlog.UseDate = true;
//        //        _textlog.UseTime = true;
//        //    }
//        }
//        public async Task Listen()
//        {
//            try
//            {
//                if (_server is null)
//                    return;
//                _server.Start();
//                Log("서버 Start.");
//                while (true)
//                {
//                    TcpClient client = await _server.AcceptTcpClientAsync(); // 비동기로 클라이언트 연결 대기
//                    Log("클라이언트가 연결되었습니다.");
//                    ConnectedClients.Add(client.Client.Handle, client); // 연결된 클라이언트를 리스트에 추가
//                    _ = HandleClientAsync(client); // 비동기적으로 클라이언트 처리
//                }
//            }
//            catch (Exception ex)
//            {
//                Log("Exception: " + ex.Message,ELogLevel.Error);
//            }
//            Log("서버 End.");
//        }
//        protected abstract Task HandleClientAsync(TcpClient client);

//        protected abstract Task BroadcastMessageAsync(string message);
//    }








//    public abstract partial class ATCP_ServerBase /*ILogInitForm*/
//    {

//        private ILogItem? _consolelog = null;
//        private ILogItem? _textlog = null;

//        bool CreataLogs()
//        {
//            var SettingLog = (ILogItem? logItem) => {
//                if (_consolelog is null)
//                    return;
//                _consolelog.AutoExec = true;
//                _consolelog.UseDate = true;
//                _consolelog.UseTime = true;
//            };

//            bool result = true;


//        if (LogManager.Instance.CreateLog(ELogType.Console, Name, out ILogItem log)) ;
//                _consolelog = log;

//        SettingLog(_consolelog);
//            if (LogManager.Instance.CreateTextLog_intervalDate($"{Name}_Text", $"./TCPIP_Server_Log/{Name}/Port{_port}/"))
//                _textlog = LogManager.Instance.FindLog($"{Name}_Text");
//            if (_textlog is not null)
//            {
//            _textlog.AutoExec = true;
//            _textlog.UseDate = true;
//            _textlog.UseTime = true;
//            }



//            return result;
//        }

//        bool DeleteLog()
//        {
//            bool result = true;
//            return result;
//        }


//        protected void Log(string msg, ELogLevel level = ELogLevel.Info)
//        {
//            _consolelog?.AddString(msg, level);
//            _textlog?.AddString(msg, level);
//        }
//        private void CreataLogs()
//        {
//            if (LogManager.Instance.CreateConsoleLog($"{Name}_Log_Console"))
//                _consolelog = LogManager.Instance.FindLog($"{Name}_Log_Console");
//            if (_consolelog is not null)
//            {
//                _consolelog.AutoExec = true;
//                _consolelog.UseDate = true;
//                _consolelog.UseTime = true;
//            }
//            if (LogManager.Instance.CreateTextLog_intervalDate($"{Name}_Text", $"./TCPIP_Server_Log/{Name}/Port{_port}/"))
//                _textlog = LogManager.Instance.FindLog($"{Name}_Text");
//            if (_textlog is not null)
//            {
//                _textlog.AutoExec = true;
//                _textlog.UseDate = true;
//                _textlog.UseTime = true;
//            }
//        }
//    }
//}
