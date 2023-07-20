using SharedLibrary;
using SharedLibrary.TCPIP;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    internal class Listener
    {
        IPAddress _ipAddress = IPAddress.Any;
        int _port = Define.Port;
        TcpListener listener;
        internal Listener()
        {
            listener = new TcpListener(_ipAddress, _port);
            Console.WriteLine("리슨 서버가 시작되었습니다.");
        }
        public void Run()
        {
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine(String.Format("클라이언트({0})가 연결되었습니다.", client.Client.Handle));
            UserClient userClient = new UserClient(client);
            Thread thread = new Thread(() => { userClient.Run(); });
            thread.Start();
        }
        

    }
}
