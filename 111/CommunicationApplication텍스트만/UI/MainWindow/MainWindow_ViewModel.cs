using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using CommunicationApplication.UI;
namespace CommunicationApplication.UI.MainWindow
{
    internal class MainWindow_ViewModel : Interface_Abstract.AViewModelBase
    {



        internal MainWindow_ViewModel() //: AViewModelBase()
        {
            Model = new Mainwindow_Model();
        }



        //public Model.Model1 Model1 { get; set; }
        //public ViewModel1() { Model1 = new Model.Model1(); }
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }

}
