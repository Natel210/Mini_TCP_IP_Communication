using SharedLibrary.Object.Base;
using SharedLibrary.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.Utility.Log.LogManager;

namespace SharedLibrary.DesignPattern
{
    public abstract class ISingleton<T> : IObjBase where T : class, new() 
    {
        public abstract string Name { get; }
        public abstract string ClassName { get; }
        public static T Instance { get; } = new T();
    }
}
