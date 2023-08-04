using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Log;
using SharedLibrary.Object.Log;

namespace SharedLibrary.TCP_IP.Server
{
    public abstract class ATCP_Server_Base : IObjBase
    {
        public string Name { get; private set; } = "";
        public ILogItem? _consolelog = null;
        public ILogItem? _textlog = null;

        private TcpListener? _server;
        private int _port = 20000; // 포트 번호
        protected Dictionary<nint, TcpClient> ConnectedClients { get; } = new Dictionary<nint, TcpClient>();

        protected ATCP_Server_Base(string name, int port = 20000)
        {
            Name = name;
            _port = port;
            CreataLogs();
            _server = new TcpListener(IPAddress.Any, _port);
            _ = Listen();
        }
        private ATCP_Server_Base() { }
        ~ATCP_Server_Base()
        {
            LogManager.Instance.DeleteLog($"{Name}_{_port}_Log_Console");
            LogManager.Instance.DeleteLog($"{Name}_{_port}_Log_DateText");
        }

        protected void Log(string msg, ELogLevel level = ELogLevel.Info)
        {
            _consolelog?.AddString(msg, level);
            _textlog?.AddString(msg, level);
        }
        private void CreataLogs()
        {
            if (LogManager.Instance.CreateConsoleLog($"{Name}_Log_Console"))
                _consolelog = LogManager.Instance.GetLog($"{Name}_Log_Console");
            if (_consolelog is not null)
            {
                _consolelog.AutoExec = true;
                _consolelog.UseDate = true;
                _consolelog.UseTime = true;
            }
            if (LogManager.Instance.CreateTextLog_inntervalDate($"{Name}_Text",$"./TCPIP_Server_Log/{Name}/Port{_port}/"))
                _textlog = LogManager.Instance.GetLog($"{Name}_Text");
            if (_textlog is not null)
            {
                _textlog.AutoExec = true;
                _textlog.UseDate = true;
                _textlog.UseTime = true;
            }
        }
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
                Log("Exception: " + ex.Message,ELogLevel.Error);
            }
            Log("서버 End.");
        }
        protected abstract Task HandleClientAsync(TcpClient client);

        protected abstract Task BroadcastMessageAsync(string message);
    }
}
