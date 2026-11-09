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
    public delegate void ConfirmHandler(object sender, ConfirmEventArgs args);

    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// 确认按钮
        /// </summary>
        public event ConfirmHandler TestConfirmed;
        /// <summary>
        /// TRAY操作
        /// </summary>
        private EnTestAct TrayAct;

        /// <summary>
        /// TextMsg
        /// </summary>
        private string _TextMsg;
        /// <summary>
        /// TextMsg
        /// </summary>
        public string TextMsg
        {
            get { return _TextMsg; }
            set { _TextMsg = value; OnPropertyChanged("TextMsg"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ConfirmWindow(EnTestAct trayAct)
        {
            InitializeComponent();

            // 绑定数据
            this.DataContext = this;
            // 操作类型
            this.TrayAct = trayAct;
            // 显示信息
            switch(trayAct)
            {
                case EnTestAct.START:
                    this.TextMsg = "确定要<开始>吗?";
                    break;
                case EnTestAct.STOP:
                    this.TextMsg = "确定要<停止>吗?";
                    break;
                case EnTestAct.PAUSE:
                    this.TextMsg = "确定要<暂停>吗?";
                    break;
                case EnTestAct.RESUME:
                    this.TextMsg = "确定要<重启>吗?";
                    break;
                case EnTestAct.EXIT:
                    this.TextMsg = "确定要<退出程序>吗?";
                    break;
                case EnTestAct.STARTAGAIN:
                    this.TextMsg = "序列号与上次测试相同,确定要<开始>吗?";
                    break;
                default:
                    this.TextMsg = "";
                    break;
            }
            
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gridCaption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            if (this.TestConfirmed != null)
                this.TestConfirmed(this, new ConfirmEventArgs(true, this.TrayAct));
            this.Close();
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// Resume All
    /// </summary>
    public class ConfirmEventArgs : EventArgs
    {
        public bool YES { get; set; }

        public EnTestAct ACT { get; set; }

        public ConfirmEventArgs(bool yes, EnTestAct act)
        {
            this.YES = yes;
            this.ACT = act;
        }
    }

    /// <summary>
    /// TRAY操作事件类型
    /// </summary>
    public enum EnTestAct
    {
        START,
        STOP,
        PAUSE,
        RESUME,
        EXIT,
        STARTAGAIN
    }
}