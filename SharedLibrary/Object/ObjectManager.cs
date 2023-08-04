using SharedLibrary.DesignPattern;
using SharedLibrary.Object.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Object
{
    public class ObjectManager : ISingleton<ObjectManager>, IObjBase
    {
        public override string Name { get; } = nameof(ObjectManager);
        public override string ClassName { get; } = nameof(ObjectManager);


    }
}
