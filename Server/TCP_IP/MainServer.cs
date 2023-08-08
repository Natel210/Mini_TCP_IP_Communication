using SharedLibrary.TCP_IP.Server;
using SharedLibrary.Utility.Log.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.TCP_IP
{
    internal class MainServer : ATCP_ServerBase
    {
        internal MainServer(string name = "MainServer", int port = 20000) : base(name, port)
        {

        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            nint ClientHandle = client.Client.Handle;
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); // 비동기로 데이터 수신
                    if (bytesRead == 0)
                        break;

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Log($"{ClientHandle}클라이언트로부터 메시지 수신: " + message);

                    // 모든 클라이언트에게 메시지 브로드캐스팅
                    await BroadcastMessageAsync(message);
                }
                catch (Exception ex)
                {
                    Log($"{ClientHandle}오류 발생: " + ex.Message, ELogLevel.Error);
                    break;
                }
            }
            ConnectedClients.Remove(ClientHandle); // 클라이언트 연결이 끊겼을 때 리스트에서 제거
            Log($"{ClientHandle}클라이언트 수신 종료.");
            client.Close();
        }


        protected override async Task BroadcastMessageAsync(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);

            foreach (var item in ConnectedClients)
            {
                try
                {
                    NetworkStream stream = item.Value.GetStream();
                    await stream.WriteAsync(buffer, 0, buffer.Length); // 비동기로 데이터 전송
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
