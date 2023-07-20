using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.TCPIP.Packet
{
    public enum PacketType
    {
        None,
        My1,
    }

    public class PacketHeadItem
    {
        public PacketType PacketType { get; set; }
    }

    public class PacketTail
    {
        public string TailData { get; set; }
    }

}
