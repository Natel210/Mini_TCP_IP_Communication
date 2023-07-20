using SharedLibrary.TCPIP.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    internal class UserClient
    {
        TcpClient _client;
        bool _disposed;
        internal UserClient(TcpClient client)
        {
            _client = client;
        }
        public void Run()
        {
            byte[] data = new byte[256];
            NetworkStream stream = _client.GetStream();

            int bytesRead;
            try
            {
                while ((bytesRead = stream.Read(data, 0, data.Length)) != 0)
                {
                    Packet packet = Packet.FromByteArray(data, 0, bytesRead);
                    Console.WriteLine($"클라이언트로부터 받은 메시지: {packet.Tail.TailData}");
                    byte[] response = Encoding.UTF8.GetBytes("서버가 메시지를 받았습니다.");
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"통신 애러 메시지: {ex.Message}");
            }
            finally
            {
                _client.Close();
                Console.WriteLine("클라이언트와의 연결이 종료되었습니다.");
            }
        }
    }
}
