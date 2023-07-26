using SharedLibrary.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    interface IProgress
    {
        public void Progress();
    }
    interface IComm
    {
        void ReceiveData();
        void SendData(byte[] data);
    }


    public partial class AClient<Data> : IComm, IProgress where Data : class, new()
    {
        // IPAddress.Loopback 127.0.0.1.
        private IPAddress _ipAddress;

        private const int s_port = 20000;
        private int _port;

        private TcpClient _tcpClient = new TcpClient();
        

        private List<byte> _receiveBuffer = new List<byte>();



        private static Data s_dummyData = new Data();
        private static readonly byte[] s_dummyString = EncodingData(s_dummyData);
        private static int s_dummySize = s_dummyString.Length;
        private static int s_dummyBit = BitConverter.ToInt32(s_dummyString);
        private static byte[] EncodingData(Data? data)
        {
            if (data is null)
                return new byte[0];
            var dataString = data.ToString();
            if (dataString is null)
                return new byte[0];
            return Encoding.UTF8.GetBytes(dataString);
        }
        private static Data? DecodingData(byte[] data)
        {
            if (data is null)
                return null;
            if (s_dummyString is null)
                return null;
            if (data.Length != s_dummySize)
                return null;
            Data result = new Data();
            GCHandle handle = GCHandle.Alloc(data,GCHandleType.Pinned);
            try
            {
                Marshal.PtrToStructure(handle.AddrOfPinnedObject(), result);
                handle.Free();
                return result;
            }
            catch(Exception e)
            {
                handle.Free();
                return null;
            }
        }



        void IComm.ReceiveData()
        {
            byte[] buffer = new byte[s_dummySize];


            int bytesRead;


            Data test = new Data();
            _tcpClient.GetStream();
            _revePacket.Add(test);
        }

        public void Progress()
        {
            //foreach (var item in _revePacket)
            //{
            //    //item;
            //}
            //_revePacket.Clear();
            ////여기에 커맨드 패턴을 넣을것인가 말것인가?
        }

        void SetAddressPort(IPAddress ipAddress, int port)
        {
            if (null == ipAddress)
                _ipAddress = IPAddress.Loopback;
            else
                _ipAddress = ipAddress;

            if (0 >= port)
                _port = s_port;
            else
                _port = port;
            _log.Comment(string.Format("[Log] [{0}] [Init] Client Init : {1} / {2}", DateTime.Now, _ipAddress.ToString(), _port));
        }

        void Connect()
        {
            _tcpClient.Connect(_ipAddress, _port);
            _log.Comment(string.Format("[Log] [{0}] [Connecting] Client Connecting : {1} / {2}", DateTime.Now, _ipAddress.ToString(), _port));
        }

        protected AClient(int port)
        {
            if (null == _ipAddress)
                _ipAddress = IPAddress.Loopback;
            SetAddressPort(IPAddress.Loopback, port);
            Connect();
        }

        protected AClient(IPAddress ipAddress, int port)
        {
            if (null == _ipAddress)
                _ipAddress = IPAddress.Loopback;
            SetAddressPort(ipAddress, port);
            Connect();
        }

        protected AClient(string ipAddressString, int port)
        {
            if (null == _ipAddress)
                _ipAddress = IPAddress.Loopback;

            IPAddress ipAddress = IPAddress.Loopback;
            try
            {
                ipAddress = IPAddress.Parse(ipAddressString);
            }
            catch (Exception e)
            {
                _log.Comment(string.Format("[Error] [{0}] [Init] Client Init : {1}", DateTime.Now, e.ToString()));
                ipAddress = IPAddress.Loopback;
            }
            SetAddressPort(ipAddress, port);
            Connect();
        }

        protected AClient()
        {
            _ipAddress = IPAddress.Loopback;
        }



    }

    // 
    public partial class AClient<Data>
    {
        protected Log _log = new OutText("Client.txt");


        void IComm.SendData(byte[] data)
        {
            this.SendData(data);
        }

        protected void SendData(Data packet)
        {
            var data = EncodingData(packet);
            if (data is null)
                return;
            this.SendData(data);
        }

        private void SendData(byte[] data)
        {
            if (data is null)
                return;
            _tcpClient.GetStream().Write(data, 0, data.Length);
        }

    }


}
