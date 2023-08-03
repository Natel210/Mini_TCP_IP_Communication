using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private TcpListener _server;
        private int _port = 20000; // 포트 번호
        private Dictionary<nint, TcpClient> _connectedClient = new Dictionary<nint, TcpClient>();

        public Server(int port = 20000)
        {
            _port = port;
            _server = new TcpListener(IPAddress.Any, _port);
            Listen();
        }


        public async Task Listen()
        {
            try
            {
                _server.Start();
                Console.WriteLine("server start ... ");
                while (true)
                {
                    TcpClient client = await _server.AcceptTcpClientAsync(); // 비동기로 클라이언트 연결 대기
                    Console.WriteLine("클라이언트가 연결되었습니다.");
                    _connectedClient.Add(client.Client.Handle, client); // 연결된 클라이언트를 리스트에 추가

                    _ = HandleClientAsync(client); // 비동기적으로 클라이언트 처리
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }





        //static TcpListener server;
        //static readonly int port = 20000; // 포트 번호
        //static List<TcpClient> connectedClients = new List<TcpClient>();

        public static async Task Main()
        {
            try
            {
                //server = new TcpListener(IPAddress.Any, port);
                //server.Start();
                //Console.WriteLine("서버 시작...");

                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync(); // 비동기로 클라이언트 연결 대기
                    Console.WriteLine("클라이언트가 연결되었습니다.");

                    connectedClients.Add(client); // 연결된 클라이언트를 리스트에 추가

                    _ = HandleClientAsync(client); // 비동기적으로 클라이언트 처리
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
            }
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); // 비동기로 데이터 수신
                    if (bytesRead == 0)
                        break;

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("클라이언트로부터 메시지 수신: " + message);

                    // 모든 클라이언트에게 메시지 브로드캐스팅
                    await BroadcastMessageAsync(message);
                }
                catch
                {
                    break;
                }
            }

            client.Close();
            connectedClients.Remove(client); // 클라이언트 연결이 끊겼을 때 리스트에서 제거
        }

        static async Task BroadcastMessageAsync(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);

            foreach (TcpClient connectedClient in connectedClients)
            {
                try
                {
                    NetworkStream stream = connectedClient.GetStream();
                    await stream.WriteAsync(buffer, 0, buffer.Length); // 비동기로 데이터 전송
                }
                catch
                {
                    // 예외 처리 필요 (클라이언트 연결이 끊겼을 경우)
                }
            }
        }
    }
}
