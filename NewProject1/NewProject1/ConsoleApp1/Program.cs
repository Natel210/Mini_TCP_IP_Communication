// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;
using ConsoleApp1;

Listener listener = new Listener();
Thread listenThread = new Thread(() => {
    while (true){ listener.Run(); }
});
listenThread.Start();
while (true)
{
    Console.Write("메시지를 입력하세요 (종료하려면 'exit' 입력): ");
    string message = Console.ReadLine();

    if (message.ToLower() == "exit")
        break;
}


