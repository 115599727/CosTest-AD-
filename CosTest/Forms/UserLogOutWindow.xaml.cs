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
    /// Interaction logic for UserLogOutWindow.xaml
    /// </summary>
    public partial class UserLogOutWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// UserName
        /// </summary>
        private string userName;
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 登陆/退出事件声明
        /// </summary>
        public event LoginHandler LoginEvent;

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        public UserLogOutWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            this.UserName = UserUtil.ActiveUser.NAME;

            this.IsClosed = false;
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            //退出登陆
            StUser noUser = new StUser();
            noUser.NAME = "";
            noUser.GROUP = EnGROUP.Visitor;
            UserUtil.ActiveUser = noUser;

            //更新主界面
            if (this.LoginEvent != null)
                this.LoginEvent(this, new LoginEventArgs(EnLoginAct.LOGOUT));

            this.Close();
            this.IsClosed = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.IsClosed = true;
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

    /// <summary>
    /// 脚本事件参数类型
    /// </summary>
    public class LoginEventArgs : EventArgs
    {
        public EnLoginAct ACT { get; set; }

        public LoginEventArgs(EnLoginAct act)
        {
            ACT = act;
        }
    }

    /// <summary>
    /// 登陆/退出事件类型
    /// </summary>
    public enum EnLoginAct
    {
        LOGON,
        LOGOUT
    }

    /// <summary>
    /// 脚本事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void LoginHandler(object sender, LoginEventArgs args);
}
