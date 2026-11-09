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
    /// EditRoutineWindow.xaml 的交互逻辑
    /// </summary>
    partial class EditScriptWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// TextMsg
        /// </summary>
        private string textMsg;
        /// <summary>
        /// TextMsg
        /// </summary>
        public string TextMsg
        {
            get { return textMsg; }
            set { textMsg = value; OnPropertyChanged("TextMsg"); }
        }

        /// <summary>
        /// TextMsgHint
        /// </summary>
        private string textMsgHint;
        /// <summary>
        /// TextMsgHint
        /// </summary>
        public string TextMsgHint
        {
            get { return textMsgHint; }
            set { textMsgHint = value; OnPropertyChanged("TextMsgHint"); }
        }

        /// <summary>
        /// TextMsgReadOnly
        /// </summary>
        private bool textMsgReadOnly;
        /// <summary>
        /// TextMsgReadOnly
        /// </summary>
        public bool TextMsgReadOnly
        {
            get { return textMsgReadOnly; }
            set { textMsgReadOnly = value; OnPropertyChanged("TextMsgReadOnly"); }
        }

        /// <summary>
        /// 脚本事件声明
        /// </summary>
        public event RoutineHandler RoutineEvent;

        /// <summary>
        /// 已存在基本名称
        /// </summary>
        private List<string> ExistedNames;

        /// <summary>
        /// 基于某脚本进行修改
        /// </summary>
        private string BaseName;

        /// <summary>
        /// 当前脚本名或者新脚本名
        /// </summary>
        private string NewName;

        /// <summary>
        /// 脚本修改类型
        /// </summary>
        private EnRoutineAct ACT;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public EditScriptWindow(List<string> existedNames, string baseName, EnRoutineAct act)
        {
            InitializeComponent();

            //绑定XMAL
            this.DataContext = this;

            this.ExistedNames = existedNames;
            this.BaseName = baseName;
            this.ACT = act;

            switch (this.ACT)
            {
                case EnRoutineAct.SAVE:
                    this.TextMsg = this.BaseName;
                    this.textMsgHint = "请确认是否保存当前程序";
                    this.TextMsgReadOnly = true;
                    break;
                case EnRoutineAct.SAVEAS:
                    this.TextMsg = this.BaseName;
                    this.textMsgHint = "另存为程序，请输入新脚本名";
                    this.TextMsgReadOnly = false;
                    break;
                case EnRoutineAct.NEW:
                    this.TextMsg = "";
                    this.textMsgHint = "创建空白程序，请输入程序名";
                    this.TextMsgReadOnly = false;
                    break;
                case EnRoutineAct.DELETE:
                    this.TextMsg = this.BaseName;
                    this.textMsgHint = "请确认是否删除此程序";
                    this.TextMsgReadOnly = true;
                    break;
                case EnRoutineAct.RENAME:
                    this.TextMsg = this.BaseName;
                    this.textMsgHint = "重命名程序，请输入程序名";
                    this.TextMsgReadOnly = false;
                    break;
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.gridCaption.Focus();

            switch (this.ACT)
            {
                case EnRoutineAct.SAVEAS:
                case EnRoutineAct.NEW:
                case EnRoutineAct.RENAME:
                    this.NewName = this.TextMsg.Trim();
                    if (this.NewName == "")
                    {
                        WarnWindow.ShowMsg("程序名不能为空!");
                        return;
                    }
                    else if (this.IsTheNameExisted(this.NewName))
                    {
                        WarnWindow.ShowMsg(this.NewName + " : 已经存在,请选择重新输入!");
                        return;
                    }
                    else
                    {
                        if (this.RoutineEvent != null)
                            this.RoutineEvent(this, new RoutineEventArgs(this.NewName, this.BaseName, this.ACT));
                    }
                    break;

                case EnRoutineAct.SAVE:
                    if (this.RoutineEvent != null)
                        this.RoutineEvent(this, new RoutineEventArgs(this.NewName, this.BaseName, this.ACT));
                    break;

                case EnRoutineAct.DELETE:
                    if (this.RoutineEvent != null)
                        this.RoutineEvent(this, new RoutineEventArgs(this.NewName, this.BaseName, this.ACT));
                    break;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 检查该脚本名是否已经存在
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        private bool IsTheNameExisted(string newName)
        {
            if (this.ExistedNames == null)
                return false;

            foreach (string nm in this.ExistedNames)
            {
                if (nm == newName)
                    return true;
            }

            return false;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gridCaption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }

    /// <summary>
    /// 脚本事件参数类型
    /// </summary>
    public class RoutineEventArgs : EventArgs
    {
        public string NewName { get; set; }

        public string BaseName { get; set; }

        public EnRoutineAct ACT { get; set; }

        public RoutineEventArgs(string newName, string baseName, EnRoutineAct act)
        {
            NewName = newName;
            BaseName = baseName;
            ACT = act;
        }
    }

    /// <summary>
    /// 脚本事件类型
    /// </summary>
    public enum EnRoutineAct
    {
        SAVE,
        SAVEAS,
        NEW,
        DELETE,
        RENAME
    }

    /// <summary>
    /// 脚本事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void RoutineHandler(object sender, RoutineEventArgs args);
}
