using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstStream;

namespace CosTest
{
    class UserUtil
    {
        /// <summary>
        /// 解析用户配置文件
        /// </summary>
        private static VstIniParser UserIniParser;

        /// <summary>
        /// 用户信息列表
        /// </summary>
        private static List<StUser> UserList;

        /// <summary>
        /// 当前登陆的用户
        /// </summary>
        internal static StUser ActiveUser { set; get; }

        /// <summary>
        /// 是否成功加载配置文件
        /// </summary>
        internal static bool LOADED { get; set; }

        /// <summary>
        /// 读取用户配置
        /// </summary>
        internal static void ReadConfig()
        {
            try
            {
                UserList = new List<StUser>();
                UserIniParser = new VstIniParser(StationUtil.GetUserFilePath());
                string[] UserNames = UserIniParser.EnumSectionNames();
                for (int i = 0; i < UserNames.Length; i++)
                {
                    StUser usr = new StUser();
                    usr.NAME = UserNames[i];
                    usr.PASSWORD = UserIniParser.GetSetting(UserNames[i], "PASSWORD");
                    string grp = UserIniParser.GetSetting(UserNames[i], "GROUP");
                    if (grp.ToUpper() == "ADMIN")
                        usr.GROUP = EnGROUP.Admin;
                    else
                        usr.GROUP = EnGROUP.Visitor;
                    UserList.Add(usr);
                }
                LOADED = true;
            }
            catch (Exception ex)
            {
                LOADED = false;
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("读取账号信息文件失败！");
            }
        }

        /// <summary>
        /// 获取所有用户的名称
        /// </summary>
        /// <returns>Scrip Names</returns>
        internal static List<string> GetUserNames()
        {
            List<string> userNameList = new List<string>();
            for (int i = 0; i < UserList.Count; i++)
                userNameList.Add(UserList[i].NAME);
            userNameList.Sort();
            return userNameList;
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="usr"></param>
        internal static void SetUserData(StUser usr)
        {
            //更新内存中的用户数据
            bool usrExisted = false;
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].NAME == usr.NAME)
                {
                    usrExisted = true;
                    UserList[i] = usr;
                    break;
                }
            }
            //新增数据
            if (usrExisted == false)
                UserList.Add(usr);

            //更新或新增解析后的INI文件数据
            UserIniParser.AddSetting(usr.NAME, "PASSWORD", usr.PASSWORD);
            UserIniParser.AddSetting(usr.NAME, "GROUP", usr.GROUP.ToString());

            //保存到文件
            try
            {
                UserIniParser.SaveSettings();
            }
            catch
            {
                WarnWindow.ShowMsg("更新账号信息文件失败！请确认检查配置文件路径是否有效！");
            }
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="userName"></param>
        internal static void DelUserData(string userName)
        {
            // 更新内存中的用户数据
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].NAME == userName)
                {
                    UserList.RemoveAt(i);
                    break;
                }
            }

            // 删除解析后的INI文件数据
            UserIniParser.DeleteSetting(userName);

            // 保存到文件
            try
            {
                UserIniParser.SaveSettings();
            }
            catch
            {
                WarnWindow.ShowMsg("更新账号信息文件失败！请确认检查配置文件路径是否有效！");
            }
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns>登陆成功返回True, 失败返回False</returns>
        internal static bool UserLogOn(string userName, string userPassword)
        {
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].NAME == userName && UserList[i].PASSWORD == userPassword)
                {
                    ActiveUser = UserList[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取指定用户名的账号数据
        /// </summary>
        /// <param name="userName"></param>
        internal static StUser GetUserData(string userName)
        {
            // 查找数据
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].NAME == userName)
                {
                    return UserList[i];
                }
            }

            // 没有找到对应的用户名
            StUser usr = new StUser();
            usr.NAME = "";
            usr.PASSWORD = "";
            usr.GROUP = EnGROUP.Visitor;
            return usr;
        }
    }

    /// <summary>
    /// User Group分类
    /// </summary>
    enum EnGROUP
    {
        Admin,
        Visitor
    }

    /// <summary>
    /// 用户信息结构体
    /// </summary>
    struct StUser
    {
        internal string NAME;
        internal string PASSWORD;
        internal EnGROUP GROUP;
    }

}
