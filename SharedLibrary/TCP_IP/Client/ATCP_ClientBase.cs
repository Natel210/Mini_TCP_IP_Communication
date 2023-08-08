using SharedLibrary.Object.Base;
using SharedLibrary.TCP_IP.Common;
using SharedLibrary.Utility.Log.Form;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.TCP_IP.Client
{
    public class ATCP_ClientBase : IObjBase
    {
        public string Name { get; private set; } = "";
        public ILogItemForm? _consolelog = null;
        public ILogItemForm? _textlog = null;

        private int _port = TCP_Common.ServerPort; // 서버 포트 번호
        private string _ip = TCP_Common.ServerIP; // 서버 IP 주소
        
        protected ATCP_ClientBase(string name, string ip = TCP_Common.ServerIP, int port = TCP_Common.ServerPort)
        {
            Name = name;
            _port = port;
            CreataLogs();
        }


        public async Task Connect()
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(serverIP, port); // 비동기로 서버에 연결
                Console.WriteLine("서버에 연결되었습니다.");

                NetworkStream stream = client.GetStream();
            }
            catch (Exception ex)
            {
            }
        }


        public static async Task Main()
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(serverIP, port); // 비동기로 서버에 연결
                Console.WriteLine("서버에 연결되었습니다.");

                NetworkStream stream = client.GetStream();

                _ = ReceiveMessageAsync(stream); // 비동기로 서버로부터 메시지 수신

                while (true)
                {
                    Console.Write("메시지를 입력하세요 (종료: 'exit'): ");
                    string message = Console.ReadLine();

                    if (message.ToLower() == "exit")
                        break;

                    byte[] buffer = Encoding.ASCII.GetBytes(message);
                    await stream.WriteAsync(buffer, 0, buffer.Length); // 비동기로 데이터 전송
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
            }
        }
        static async Task ReceiveMessageAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); // 비동기로 데이터 수신
                    if (bytesRead == 0)
                        break;

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("서버로부터 메시지 수신: " + message);
                }
                catch
                {
                    break;
                }
            }
        }
    }

}
