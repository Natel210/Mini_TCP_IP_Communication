using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationApp.Utility
{
    public interface IAccessINI
    {
        void LoadIni(string path = null);
        void SaveIni(string path = null);
    }
}
