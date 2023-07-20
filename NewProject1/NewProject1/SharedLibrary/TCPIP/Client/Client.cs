using SharedLibrary.TCPIP.Packet;
using SharedLibrary.Utility.Log;
using SharedLibrary.Utility.Log.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.TCPIP.Client
{
    public class AClient
    {
        public string Host { get; set; } = Define.HostIP;
        public int Port { get; set; } = Define.Port;
        public bool End { get; set; } = true;

        private TcpClient? _client = null;



        public bool Connect(string host = Define.HostIP, int port = Define.Port)
        {
            Host = host;
            Port = port;
            try
            {
                TcpClient client = new TcpClient(Host, Port);
                _client = client;
            }
            catch (Exception)
            {
                Log("서버 연결에 실패하였습니다. 다시 시도해주세요.",ELogLevel.Error);
                if (_client is not null)
                    _client.Close();
                End = true;
                return false;
            }

            Log($"[소켓 : {_client.Client.Handle}] 서버에 연결되었습니다. ", ELogLevel.Info);
            // 스레드풀에서 RunRecv 메서드 실행.
            if (ThreadPool.QueueUserWorkItem(RunRecv))
            {
                Log($"[소켓 : {_client.Client.Handle}] ThreadPool에 등록되었습니다. RunRecv을 시작합니다.", ELogLevel.Info);
                End = false;
                return true;
            }

            if (_client is not null)
                _client.Close();
            End = true;
            return false;
        }

        public void Send(Packet.Packet packet)
        {
            if (_client is null)
                return;
            byte[] dataByte = packet.ToByteArray();
            NetworkStream stream = _client.GetStream();
            stream.Write(dataByte, 0, dataByte.Length);
        }

        public void RunRecv(object? state)
        {
            if (_client is null)
                return;

            byte[] data = new byte[256];
            NetworkStream stream = _client.GetStream();
            int bytesRead;

            while (!End)
            {
                Console.Write("메시지를 입력하세요 (종료하려면 'exit' 입력): ");
                string? message = Console.ReadLine();
                if (message is null)
                    continue;
                if (message.ToLower() == "exit")
                    break;
                PacketHeadItem packetHead = new PacketHeadItem();// { PacketType = 0 };
                packetHead.PacketType = PacketType.My1;

                PacketTail packetTail = new PacketTail();// { TailData = message };
                packetTail.TailData = message;

                try
                {
                    while ((bytesRead = stream.Read(data, 0, data.Length)) != 0)
                    {
                        Packet.Packet packet = Packet.Packet.FromByteArray(data, 0, bytesRead);
                        /////////////////////////////
                        //처리해야하는 부분
                        Log($"서버에서 받은 메시지 [{packet.Head.PacketType.ToString()}]{packet.Tail.TailData}", ELogLevel.Info);
                        /////////////////////////////
                    }
                }
                catch (Exception ex)
                {
                    Log($"통신 애러 메시지: {ex.Message}",ELogLevel.Warning);
                    End = true;
                }
            }
            _client.Close();
            Log("클라이언트와의 연결이 종료되었습니다.", ELogLevel.Info);
        }










        public bool UseConsoleLog { get; set; } = false;
        private void Log(string log, ELogLevel eLogLevel = ELogLevel.None)
        {
            if (UseConsoleLog)
                LogManager.ConsoleLog.AddString(log, eLogLevel);
            else
                LogManager.DebugLog.AddString(log, eLogLevel);
        }
    }
}
