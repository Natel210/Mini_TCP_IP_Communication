using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utility.INI
{
    internal interface IAccess
    {
        void LoadIni(string path);
        void SaveIni(string path);
    }
}
