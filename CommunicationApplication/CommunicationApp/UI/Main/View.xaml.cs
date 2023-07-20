using CommunicationApp.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CommunicationApp.UI.Base.WindowInformation;

namespace CommunicationApp.UI.Main
{
    /// <summary>
    /// View.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
            WindowFormData windowDefaltData = new WindowFormData(Width, Height, FontSize) {
                Title = Title, 
                Width = Width,  
                Height = Height,    
                DefaultFontSize = FontSize,
                MinWidth = MinWidth,
                MinHeight = MinHeight,
                MaxWidth = MaxWidth,
                MaxHeight = MaxHeight, };
            DataContext = new ViewModel(windowDefaltData);

        }
    }
}
