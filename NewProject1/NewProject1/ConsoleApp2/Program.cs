// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SharedLibrary.TCPIP.Client;
using SharedLibrary.TCPIP.Packet;

AClient a = new AClient();
a.Connect();
while (true)
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

    a.Send(new Packet(packetHead, packetTail));
}

//while (true)
//{
//    Console.Write("메시지를 입력하세요 (종료하려면 'exit' 입력): ");
//    string? message = Console.ReadLine();
//    if (message is null)
//        continue;
//    if (message.ToLower() == "exit")
//        break;

//    PacketHeadItem packetHead = new PacketHeadItem();// { PacketType = 0 };
//    packetHead.PacketType = PacketType.My1;

//    PacketTail packetTail = new PacketTail();// { TailData = message };
//    packetTail.TailData = message;

//    Packet packet = new Packet(packetHead, packetTail);
//    var a = packet.ToByteArray();
//    var b = Packet.FromByteArray(a, 0, a.Length);
//    Console.WriteLine(Encoding.UTF8.GetString(a));


//    NetworkStream stream = client.GetStream();
//    stream.Write(a, 0, a.Length);
//    /////////////////////////////////////////////////////
//    byte[] response = new byte[256];
//    int bytesRead = stream.Read(response, 0, response.Length);
//    string serverResponse = Encoding.UTF8.GetString(response, 0, bytesRead);
//    Console.WriteLine($"서버 응답: {serverResponse}");
//}

//client.Close();
//Console.WriteLine("서버와의 연결이 종료되었습니다.");