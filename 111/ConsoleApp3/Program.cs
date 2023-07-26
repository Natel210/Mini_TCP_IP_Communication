// See https://aka.ms/new-console-template for more information
using SharedLibrary.Logger;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");
TcpClient tcpClient = new TcpClient(); 
IPAddress address = IPAddress.Parse("127.0.0.1");
int port = 21562;

Log log = new OutConsole();

try
{
    tcpClient.Connect(address, port);
    log.Comment("Connet");
    while (true)
    {

    }
}
catch (Exception)
{

    throw;
}