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
    /// WarnWindow.xaml 的交互逻辑
    /// </summary>
    partial class WarnWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// TextMsg
        private string _TextMsg;
        /// <summary>
        /// TextMsg
        /// </summary>
        public string TextMsg
        {
            get { return _TextMsg; }
            set { _TextMsg = value; OnPropertyChanged("TextMsg"); }
        }

        /// <summary>
        /// 是否关闭, 如果不关闭, 就只是隐藏
        /// </summary>
        private bool NeverClose;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        internal WarnWindow(string _warnMsg, bool neverClose = false)
        {
            InitializeComponent();

            this.DataContext = this;
            this.NeverClose = neverClose;
            this.TextMsg = _warnMsg;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.NeverClose)
                this.Hide();
            else
                this.Close();
        }

        internal static void ShowMsg(string _warnMsg)
        {
            WarnWindow wwMsg = new WarnWindow(_warnMsg);
            wwMsg.Show();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.NeverClose)
                this.Hide();
            else
                this.Close();
        }

        private void gridCaption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
