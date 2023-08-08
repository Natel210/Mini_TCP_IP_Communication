using SharedLibrary.DesignPattern;
using SharedLibrary.Object.Base;
using SharedLibrary.Object.Enum;
using SharedLibrary.Object.Form;
using SharedLibrary.Utility.Path;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Object
{
    public partial class ObjectManager : ISingleton<ObjectManager>, IObjManagerForm<IObjBase,KeyValuePair<string,string>>
    {


    }

    public partial class ObjectManager /*IObjManagerForm<IObjBase>*/
    {
        Dictionary<string/*Category*/, Dictionary<string/*Name*/, IObjBase/*Item*/>> _dic = new Dictionary<string, Dictionary<string, IObjBase>>();
        public void AddItem(IObjBase item)
        {
            if (!_dic.ContainsKey(item.ClassCategory))
                _dic.Add(item.ClassCategory, new Dictionary<string/*Name*/, IObjBase/*Item*/>());
            var dic_from_category = _dic[item.ClassCategory];
            if (!dic_from_category.ContainsKey(item.Name))
                dic_from_category.Add(item.Name, item);
        }
        public bool DelItem(IObjBase item)
        {
            if (!_dic.ContainsKey(item.ClassCategory))
                return false;
            return _dic[item.ClassCategory].Remove(item.Name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValuePair"> classCatregory, itemName</param>
        /// <returns></returns>
        public bool DelItem(KeyValuePair<string, string> key)
        {
            if (!_dic.ContainsKey(key.Key))
                return false;
            return _dic[key.Key].Remove(key.Value);
        }
        public bool GetItem(KeyValuePair<string, string> key, ref IObjBase? outValue)
        {
            outValue = null;
            if (!_dic.ContainsKey(key.Key))
                return false;
            if (!_dic[key.Key].ContainsKey(key.Value))
                return false;
            outValue = _dic[key.Key][key.Value];
            return true;
        }
    }

    public partial class ObjectManager /* ISingleton<ObjectManager> -> IObjBase */
    {
        public override string Name { get; } = nameof(ObjectManager);
        public override string ClassName { get; } = nameof(ObjectManager);
        public override string ClassCategory { get; } = ELibraryObjectType.Manager.ToString();
    }

}
