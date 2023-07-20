using SharedLibrary.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class ListenServer
    {
        private TcpListener _tcpListener;
        private bool _end;
        private Log _log = new OutConsole();

        public ListenServer(IPAddress iPAddress, int port)
        {
            _tcpListener = new TcpListener(iPAddress, port);
            _end = false;
        }

        public void Run()
        {
            _tcpListener.Start();
            _log.Comment("Listener 시작");
            while (!_end)
            {
                //wait accpet
                var tcpClient = _tcpListener.AcceptTcpClient();
                if (tcpClient == null)
                    continue;
                _log.Comment("Acceot 클라");
            }
        }




    }
}
