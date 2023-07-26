using System.ComponentModel;
using System.Runtime.CompilerServices;
using static CommunicationApp.UI.Base.WindowInformation;

namespace CommunicationApp.UI.Base
{
    /// <summary>
    /// <br> View Model Abstract </br>
    /// <br> When inheriting, you must bind information about the model you want. </br>
    /// <br> Use ModelData </br>
    /// <code>
    /// <br> // Ex</br>
    /// <br> class ViewModel : AViewModelBase </br>
    /// <br> {                                </br>
    /// <br>     ViewModel() : base()         </br>
    /// <br>     {                            </br>
    /// <br>         ModelData = new Model(); </br>
    /// <br>     }                            </br>
    /// <br> }                                </br>
    /// </code>
    /// </summary>

    //Model Form                                                 //[ToolboxItem(false)]
    public partial class AViewModelBase : INotifyPropertyChanged
    {
        private IModelBase _model = null;
        /// <value name= "ModelData" > Model Inheriting <br/> default => null </value>
        public IModelBase ModelData { get => _model; set { _model = value; OnPropertyChanged(nameof(ModelData)); } }
    }

    //Window Defualt Data Binding
    public partial class AViewModelBase
    {
        private readonly string _iniPath;
        public string IniPath { get => _iniPath; }
        private WindowInformation _windowInfo = null;
        public WindowInformation WindowInfo
        {
            get => _windowInfo;
            set
            {
                _windowInfo = value;
                OnPropertyChanged(nameof(WindowInfo));
            }
        }
    }

    // INotifyPropertyChanged interface.
    public partial class AViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Abstract Painting.
    public partial class AViewModelBase
    {
        private AViewModelBase() { }
        protected AViewModelBase(string path, WindowInformation.WindowFormData windowDefaltData)
        {
            _iniPath = path;
            _windowInfo = new WindowInformation(IniPath, windowDefaltData);
        }
    }


}
