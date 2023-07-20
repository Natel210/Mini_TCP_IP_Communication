using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Logger
{
    public interface Log
    {
        public void Comment(string str);
    }

    public class OutText : Log
    {
        private string? _path = null;
        public OutText(string path) : base() => _path = path;
        private OutText() { }

        public void Comment(string str)
        {
            if (null == _path)
                return;
            if (!File.Exists(_path))
                File.Create(_path).Close();
            File.AppendAllText(_path, str + "\n");
        }
    }

    public class OutConsole : Log
    {
        public void Comment(string str)
        {
            Console.WriteLine(str);
        }
    }

    public class OutDebug : Log
    {
        public void Comment(string str)
        {
            Debug.WriteLine(str);
        }
    }
}
