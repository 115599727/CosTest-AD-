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
using System.ComponentModel;

namespace CosTest
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// EquipName
        /// </summary>
        private string _EquipName;
        /// <summary>
        /// EquipName
        /// </summary>
        public string EquipName
        {
            get { return _EquipName; }
            set { _EquipName = value; OnPropertyChanged("EquipName"); }
        }

        /// <summary>
        /// EquipPartNo
        /// </summary>
        private string _EquipPartNo;
        /// <summary>
        /// EquipPartNo
        /// </summary>
        public string EquipPartNo
        {
            get { return _EquipPartNo; }
            set { _EquipPartNo = value; OnPropertyChanged("EquipPartNo"); }
        }

        /// <summary>
        /// SoftwareVersion
        /// </summary>
        private string _SoftwareVersion;
        /// <summary>
        /// SoftwareVersion
        /// </summary>
        public string SoftwareVersion
        {
            get { return _SoftwareVersion; }
            set { _SoftwareVersion = value; OnPropertyChanged("SoftwareVersion"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        public AboutWindow()
        {
            InitializeComponent();

            // Binding
            this.DataContext = this;

            // IsClosed
            this.IsClosed = false;

            // Info Update
            this.EquipPartNo = "设备型号： " + StationUtil.EquipPartNo;
            this.SoftwareVersion = "软件版本： " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.IsClosed = true;
        }

        private void gridCaption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
