using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    interface ICommData
    {
        void ReceiveData();
        void SendData(byte[] data);
    }
}
