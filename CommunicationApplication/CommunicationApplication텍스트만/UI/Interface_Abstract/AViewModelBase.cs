using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationApplication.UI.Interface_Abstract
{
    /// <summary>
    /// 상속시에 원하는 모델에 대한 정보를 기입해주어야한다.
    /// </summary>
    internal class AViewModelBase : INotifyPropertyChanged
    {
        private IModelBase model = null;
        public IModelBase Model { get => model; protected set => model = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected AViewModelBase() { }

    }
}
