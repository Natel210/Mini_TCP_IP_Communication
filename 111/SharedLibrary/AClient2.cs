using System.Net;

namespace SharedLibrary
{
    public class MyClass
    {

    }

    public class AClient2 : AClient<MyClass>
    {
        public AClient2(int port) : base(port)
        {
        }

        public AClient2(IPAddress ipAddress, int port) : base(ipAddress, port)
        {
        }

        public AClient2(string ipAddressString, int port) : base(ipAddressString, port)
        {
        }

    }
}
