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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// UserNameList
        /// </summary>
        private List<string> userNameList;
        /// <summary>
        /// UserNameList
        /// </summary>
        public List<string> UserNameList
        {
            get { return userNameList; }
            set { userNameList = value; OnPropertyChanged("UserNameList"); }
        }

        /// <summary>
        /// GroupList
        /// </summary>
        private List<string> groupList;
        /// <summary>
        /// GroupList
        /// </summary>
        public List<string> GroupList
        {
            get { return groupList; }
            set { groupList = value; OnPropertyChanged("GroupList"); }
        }

        /// <summary>
        /// SelUserName
        /// </summary>
        private string selUserName;
        /// <summary>
        /// SelUserName
        /// </summary>
        public string SelUserName
        {
            get { return selUserName; }
            set { selUserName = value; OnPropertyChanged("SelUserName"); }
        }

        /// <summary>
        /// SelUserPassword
        /// </summary>
        private string selUserPassword;
        /// <summary>
        /// SelUserPassword
        /// </summary>
        public string SelUserPassword
        {
            get { return selUserPassword; }
            set { selUserPassword = value; OnPropertyChanged("SelUserPassword"); }
        }

        /// <summary>
        /// SelUserGroup
        /// </summary>
        private string selUserGroup;
        /// <summary>
        /// SelUserGroup
        /// </summary>
        public string SelUserGroup
        {
            get { return selUserGroup; }
            set { selUserGroup = value; OnPropertyChanged("SelUserGroup"); }
        }

        /// <summary>
        /// NewUserName
        /// </summary>
        private string newUserName;
        /// <summary>
        /// NewUserName
        /// </summary>
        public string NewUserName
        {
            get { return newUserName; }
            set { newUserName = value; OnPropertyChanged("NewUserName"); }
        }

        /// <summary>
        /// NewUserPassword
        /// </summary>
        private string newUserPassword;
        /// <summary>
        /// NewUserPassword
        /// </summary>
        public string NewUserPassword
        {
            get { return newUserPassword; }
            set { newUserPassword = value; OnPropertyChanged("NewUserPassword"); }
        }

        /// <summary>
        /// NewUserGroup
        /// </summary>
        private string newUserGroup;
        /// <summary>
        /// NewUserGroup
        /// </summary>
        public string NewUserGroup
        {
            get { return newUserGroup; }
            set { newUserGroup = value; OnPropertyChanged("NewUserGroup"); }
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

        public UserWindow()
        {
            InitializeComponent();

            //绑定
            this.DataContext = this;

            //更新用户数据
            //Group
            List<string> groups = new List<string>();
            groups.Add(EnGROUP.Admin.ToString());
            groups.Add(EnGROUP.Visitor.ToString());
            this.GroupList = groups;
            //UserNames
            this.UserNameList = UserUtil.GetUserNames();
            //IsClosed
            this.IsClosed = false;
        }

        private void cbUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StUser usr = UserUtil.GetUserData(this.SelUserName);
            this.SelUserPassword = usr.PASSWORD;
            this.SelUserGroup = usr.GROUP.ToString();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //检查SelUserName是否为空
            if (this.SelUserName == null || this.SelUserName == "")
            {
                WarnWindow.ShowMsg("当前没有选中任何账号, 无法进行编辑!");
                return;
            }
            //检查SelUserPassword是否为空
            if (this.SelUserPassword == null || this.SelUserPassword == "")
            {
                WarnWindow.ShowMsg("请先为要编辑的账号选择输入密码!");
                return;
            }
            //检查SelUserGroup是否为空
            if (this.SelUserGroup == null || this.SelUserGroup == "")
            {
                WarnWindow.ShowMsg("请先为要编辑的账号选择权限!");
                return;
            }
            //保存更改
            StUser usr = new StUser();
            usr.NAME = this.SelUserName;
            usr.GROUP = this.SelUserGroup == EnGROUP.Admin.ToString() ? EnGROUP.Admin : EnGROUP.Visitor;
            usr.PASSWORD = this.SelUserPassword;
            UserUtil.SetUserData(usr);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            //检查SelUserName是否为空
            if (this.SelUserName == null || this.SelUserName == "")
            {
                WarnWindow.ShowMsg("当前没有选中任何账号, 无法删除!");
                return;
            }
            //删除
            UserUtil.DelUserData(this.SelUserName);
            this.UserNameList.Remove(this.SelUserName);
            List<string> usrNameList = this.UserNameList;
            this.UserNameList = null;
            this.UserNameList = usrNameList;
            if (this.UserNameList.Count > 0)
                this.SelUserName = this.UserNameList[0];
            else
                this.SelUserName = "";
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            //检查NewUserName是否为空
            if (this.NewUserName == null || this.NewUserName == "")
            {
                WarnWindow.ShowMsg("新建账号名不能为空!");
                return;
            }
            //检查NewUserPassword是否为空
            if (this.NewUserPassword == null || this.NewUserPassword == "")
            {
                WarnWindow.ShowMsg("请先为要创建的账号选择输入密码!");
                return;
            }
            //检查NewUserGroup是否为空
            if (this.NewUserGroup == null || this.NewUserGroup == "")
            {
                WarnWindow.ShowMsg("请先为要创建的账号选择权限!");
                return;
            }
            //检查用户是否已经存在;
            foreach(string usrName in this.UserNameList)
            {
                if(usrName == this.NewUserName)
                {
                    WarnWindow.ShowMsg("要创建的账号已经存在!");
                    return;
                }
            }
            //新建
            StUser usr = new StUser();
            usr.NAME = this.NewUserName;
            usr.GROUP = this.NewUserGroup == EnGROUP.Admin.ToString() ? EnGROUP.Admin : EnGROUP.Visitor;
            usr.PASSWORD = this.NewUserPassword;
            UserUtil.SetUserData(usr);
            //更新已有账号UI
            this.UserNameList.Add(this.NewUserName);
            List<string> usrNameList = this.UserNameList;
            this.UserNameList = null;
            this.UserNameList = usrNameList;
            this.SelUserName = this.NewUserName;
            //清空新建用户UI
            this.NewUserName = "";
            this.NewUserPassword = "";
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
