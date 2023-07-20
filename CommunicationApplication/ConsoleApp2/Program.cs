// See https://aka.ms/new-console-template for more information
using SharedLibrary;
using SharedLibrary.Logger;
using System.Net;

Console.WriteLine("Hello, World!");
Log a = new OutText("test.txt");
a.Comment("HI");
a.Comment("HI");
a.Comment("HI");
a.Comment("HI");
a.Comment("HI");

ListenServer Listen = new ListenServer(IPAddress.Any,21562);
Listen.Run();