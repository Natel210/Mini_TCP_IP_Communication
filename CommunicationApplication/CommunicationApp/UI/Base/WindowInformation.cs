using CommunicationApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace CommunicationApp.UI.Base
{
    public partial class WindowInformation : INotifyPropertyChanged, IAccessINI
    {
        public WindowInformation(string path, WindowFormData windowDefaltData)
        {
            _windowDefaltData = windowDefaltData;
            WindowData = new WindowFormData(windowDefaltData.DesignWidth, windowDefaltData.DesignHeight
                , windowDefaltData.DesignDefaultFontSize);

            LoadIni(path);
        }
    }
    public partial class WindowInformation
    {

    }



    // IAccess INI
    public partial class WindowInformation
    {
        private IniFile _iniFile = new IniFile();
        const string WindowSection = "WindowData";
        const string ButtonSection = "ButtonList";

        public void SaveIni(string path = null)
        {
            SaveIni_Window();
            SaveIni_Button();
        }

        public void LoadIni(string path = null)
        {
            // Check Valid.
            if ("" != path.Trim())
                _iniFile.FilePath = path;
            if (!_iniFile.CheckFile())
                _iniFile.CreateFile();

            LoadIni_Window();
            LoadIni_Button();
        }

        private void SaveIni_Window()
        {
            if (_iniFile.CheckFile())
                _iniFile.DeleteFile();
            _iniFile.CreateFile();

            SetIniValue(WindowSection, "Title", WindowData.Title);
            SetIniValue(WindowSection, "Width", WindowData.Width.ToString());
            SetIniValue(WindowSection, "Height", WindowData.Height.ToString());

            SetIniValue(WindowSection, "WhiteSpace", WindowData.WhiteSpace.ToString());
            SetIniValue(WindowSection, "WhiteSpaceColor", WindowData.WhiteSpaceColor);

            SetIniValue(WindowSection, "MinWidth", WindowData.MinWidth.ToString());
            SetIniValue(WindowSection, "MaxWidth", WindowData.MinWidth.ToString());
            SetIniValue(WindowSection, "MinHeight", WindowData.MinWidth.ToString());
            SetIniValue(WindowSection, "MaxHeight", WindowData.MinWidth.ToString());
        }
        private void SaveIni_Button()
        {
        }

        private void LoadIni_Window()
        {
            if (!_iniFile.CheckFile())
                return;

            WindowData.Title = GetIniValue(WindowSection, "Title", WindowDefaultData.Title);
            WindowData.Width = CheckToDouble(GetIniValue(WindowSection, "Width", WindowDefaultData.Width.ToString()));
            WindowData.Height = CheckToDouble(GetIniValue(WindowSection, "Height", WindowDefaultData.Height.ToString()));

            WindowData.WhiteSpace = CheckToInt(GetIniValue(WindowSection, "WhiteSpace", WindowDefaultData.WhiteSpace.ToString()));
            WindowData.WhiteSpaceColor = GetIniValue(WindowSection, "WhiteSpaceColor", WindowDefaultData.WhiteSpaceColor);

            WindowData.MinWidth = CheckToDouble(GetIniValue(WindowSection, "MinWidth", WindowDefaultData.MinWidth.ToString()));
            WindowData.MaxWidth = CheckToDouble(GetIniValue(WindowSection, "MaxWidth", WindowDefaultData.MinWidth.ToString()));
            WindowData.MinHeight = CheckToDouble(GetIniValue(WindowSection, "MinHeight", WindowDefaultData.MinWidth.ToString()));
            WindowData.MaxHeight = CheckToDouble(GetIniValue(WindowSection, "MaxHeight", WindowDefaultData.MinWidth.ToString()));
        }
        private void LoadIni_Button()
        {

            if (!_iniFile.CheckFile())
                return;
            int ButtonCount = CheckToInt(GetIniValue(ButtonSection, "Count", "0"));
            for (int idx = 0; idx < ButtonCount; ++idx)
            {
                string Item = GetIniValue(ButtonSection, string.Format("item{0}", idx), "");
                if (Item != "")
                    ButtonData.Add(Item, new ButtonFormData());
            }
            foreach (var item in ButtonData)
            {
                item.Value.Text = GetIniValue(item.Key, "Text", "");
                item.Value.Background = GetIniValue(item.Key, "Background", "");
            }
        }
        /// <summary>
        /// INI Value.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValueString">Get Fail Return String</param>
        /// <returns></returns>
        private string GetIniValue(string section, string key, string defaultValueString)
        {
            var readValue = _iniFile.IniReadValue(section, key);
            if (readValue == null)
            {
                _iniFile.IniWriteValue(section, key, defaultValueString);
                return defaultValueString;
            }
            return readValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetIniValue(string section, string key, string value)
        {
            _iniFile.IniWriteValue(section, key, value);
        }
        /// <summary>
        /// Safe To Int32.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int CheckToInt(string str)
        {
            int result = 0;
            if (Int32.TryParse(str, out result))
                return result;
            return 0;
        }
        /// <summary>
        /// Safe To Double.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private double CheckToDouble(string str)
        {
            double result = 0.0;
            if (double.TryParse(str, out result))
                return result;
            return 0.0;
        }
    }
    // Inner class/struct
    public partial class WindowInformation
    {
        private WindowFormData _windowData = null;
        internal WindowFormData WindowData
        {
            get => _windowData;
            set
            {
                _windowData = value;
                OnPropertyChanged(nameof(WindowData));
            }
        }

        private readonly WindowFormData _windowDefaltData;
        /// <summary>
        /// 
        /// </summary>
        internal WindowFormData WindowDefaultData
        {
            get => _windowDefaltData;
        }

        public class WindowFormData
        {
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public string Title { get; set; } = "";
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double Width { get; set; } = 0.0;
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double Height { get; set; } = 0.0;
            /// <summary>
            /// Calculated applied value.
            /// </summary>
            public double DefaultFontSize { get; set; } = 0.0;
            /// <summary>
            /// Only OneTime Set.
            /// </summary>
            private readonly double _designDefaultFontSize;
            /// <summary>
            /// A value set in Xaml.
            /// </summary>
            public double DesignDefaultFontSize { get => _designDefaultFontSize; }
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public int WhiteSpace { get; set; } = 0;
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public string WhiteSpaceColor { get; set; } = "";
            /// <summary>
            /// Only OneTime Set.
            /// </summary>
            private readonly double _designWidth;
            /// <summary>
            /// A value set in Xaml.
            /// </summary>
            public double DesignWidth { get => _designWidth; }
            /// <summary>
            /// Only OneTime Set.
            /// </summary>
            private readonly double _designHeight;
            /// <summary>
            /// A value set in Xaml.
            /// </summary>
            public double DesignHeight { get => _designHeight; }
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double MinWidth { get; set; } = 0.0;
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double MinHeight { get; set; } = 0.0;
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double MaxWidth { get; set; } = 0.0;
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public double MaxHeight { get; set; } = 0.0;
            /// <summary>
            /// Y ratio base Cal.
            /// </summary>
            public void CalDefaultFont()
            {
                DefaultFontSize = DesignDefaultFontSize * Ratio.Y;
            }
            /// <summary>
            /// X, Y ratio.
            /// </summary>
            public Vector Ratio
            {
                get
                {
                    // Zero Division.
                    if (DesignWidth <= 0.0 || DesignHeight <= 0.0)
                        return new Vector();
                    return new Vector(Width / DesignWidth, Height / DesignHeight);
                }
            }
            /// <summary>
            /// Must Init ReadOnly Value.
            /// </summary>
            private WindowFormData() { }
            /// <summary>
            /// Must Insert View Design Data.
            /// </summary>
            /// <param name="designWidth">View -> Width</param>
            /// <param name="designHeight">View -> Height</param>
            /// <param name="designDefaultFontSize">View -> Window Default Font Size</param>
            /// <param name="designTitleFontSize">View -> Title Font Size</param>
            public WindowFormData(double designWidth, double designHeight, double designDefaultFontSize)
            {
                _designWidth = designWidth;
                _designHeight = designHeight;
                _designDefaultFontSize = designDefaultFontSize;
            }
        }

        /// <summary> [ <c>OnPropertyChanged()</c> ] is not implemented.<br/>
        /// You need to implement [ <c>OnPropertyChanged()</c> ] depending on the ButtonItem in the ViewModel to inject.
        /// </summary>
        internal Dictionary<string, ButtonFormData> ButtonData { get; set; } = new Dictionary<string, ButtonFormData>();
        internal class ButtonFormData
        {
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// A value set in INI.
            /// </summary>
            public string Background { get; set; }
        }
    }
    // INotifyPropertyChanged implemented.
    public partial class WindowInformation
    {
        private PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _propertyChanged += value;
            remove => _propertyChanged -= value;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
