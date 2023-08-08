using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Path;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Object.Form
{
    public interface IObjManagerForm<T,Key> where T : IObjBase
    {
        void AddItem(T item);
        bool DelItem(T item);
        bool DelItem(Key key);
        bool GetItem(Key key, ref T? outValue);
    }
}
