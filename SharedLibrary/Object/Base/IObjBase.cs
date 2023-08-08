using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Object.Base
{
    public interface IObjBase
    {
        string Name { get; }
        string ClassName { get; }
        string ClassCategory { get; }
    }
}
