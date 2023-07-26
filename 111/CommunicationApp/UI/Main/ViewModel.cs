using CommunicationApp.UI.Base;
using CommunicationApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.DesignerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationApp.UI.Main
{
    public partial class ViewModel : AViewModelBase
    {
        const string _iniPath = "./UI/Main.ini";
        
        public ViewModel(WindowInformation.WindowFormData windowDefaltData) : base(_iniPath, windowDefaltData)
        {
            ModelData = new Model();
            SaveWindowInformationData();
        }



        void SaveWindowInformationData()
        {
            WindowInfo.SaveIni();
        }
    }

}
