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
    /// Interaction logic for UserLogOnWindow.xaml
    /// </summary>
    public partial class UserLogOnWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// UserPassword
        /// </summary>
        private string UserPassword;

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

        public UserLogOnWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            this.IsClosed = false;

          
        }

        private void buttonLogOn_Click(object sender, RoutedEventArgs e)
        {
            // PasswordBox数据绑定较麻烦, 此处直接读取
            this.UserPassword = this.pbUserPassword.Password;

            // 检查用户名是否为空
            if (this.UserPassword == "")
            {
                WarnWindow.ShowMsg("用户名为空, 请重新输入!");
            }
            else if (UserPassword == "")
            {
                WarnWindow.ShowMsg("用户名为空, 请重新输入!");
            }
            else
            {
                if (UserUtil.UserLogOn(this.UserName, this.UserPassword) == true)
                {
                    if (this.LoginEvent != null)
                        this.LoginEvent(this, new LoginEventArgs(EnLoginAct.LOGON));
                    this.Close();
                    this.IsClosed = true;
                }
                else
                {
                    WarnWindow.ShowMsg("用户名和密码不匹配, 请重新输入!");
                }
            }
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

        private void image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            return;
            UserName = "vsstec";
            UserPassword = "abcd@1234";

            // 检查用户名是否为空
            if (this.LoginEvent != null)
                this.LoginEvent(this, new LoginEventArgs(EnLoginAct.LOGON));
            this.Close();
            this.IsClosed = true;
        }
    }
}
