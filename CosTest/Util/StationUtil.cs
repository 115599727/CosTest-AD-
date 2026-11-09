using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using VstStream;

namespace CosTest
{
    class StationUtil
    {
        /// <summary>
        /// 默认文件路径表示符
        /// </summary>
        public static readonly string DEFAULT = "[DEFAULT]";

        /// <summary>
        /// 根路径文件
        /// </summary>
        private static readonly string RootFileName = "Config\\CONFIG.ROOT.VSTINI";

        /// <summary>
        /// RACK配置文件
        /// </summary>
        private static string RackFilePath;

        /// <summary>
        /// 测试程序配置文件
        /// </summary>
        private static string RoutineFilePath;

        /// <summary>
        /// 热像测量区域配置文件
        /// </summary>
        private static string ThermMeasFilePath;

        /// <summary>
        /// 许可证文件配置路径
        /// </summary>
        private static string LicenseFilePath;

        /// <summary>
        /// 用户信息文件配置路径
        /// </summary>
        private static string UserFilePath;

        /// <summary>
        /// 校准数据文件配置路径
        /// </summary>
        private static string CalibrationFilePath;

        /// <summary>
        /// 测试数据输出文件夹
        /// </summary>
        private static string OutputFolderPath;

        /// <summary>
        /// 设备配置路径
        /// </summary>
        private static string EquipFilePath;

        /// <summary>
        /// 许可证文件名
        /// </summary>
        private static readonly string RackFileName = "Config\\CONFIG.RACK.VSTINI";

        /// <summary>
        /// 许可证文件名
        /// </summary>
        private static readonly string LicenseFileName = "Config\\CONFIG.LICENSE.VSTINI";

        /// <summary>
        /// Equip文件名
        /// </summary>
        private static readonly string EquipFileName = "Config\\CONFIG.EQUIP.VSTINI";

        /// <summary>
        /// 默认用户文件名
        /// </summary>
        private static readonly string UserFileName = "Config\\CONFIG.USER.VSTINI";

        /// <summary>
        /// 默认校准文件名
        /// </summary>
        private static readonly string CalibrationFileName = "Config\\CONFIG.CALIBRATION.VSTINI";

        /// <summary>
        /// 默认程序文件名
        /// </summary>
        private static readonly string RoutineFileName = "Config\\CONFIG.SCRIPT.VSTINI";

        /// <summary>
        /// 默认热像测量区域文件名
        /// </summary>
        private static readonly string ThermMeasFileName = "Config\\CONFIG.THERM.VSTINI";

        /// <summary>
        /// 默认系统文件名
        /// </summary>
        private static readonly string SystemFileName = "Config\\CONFIG.SYSTEM.VSTINI";

        /// <summary>
        /// 测试映射文件路径
        /// </summary>
        private static string TestMappingFilePath;

        /// <summary>
        /// 解析根配置文件
        /// </summary>
        private static VstIniParser RootIniParser;

        /// <summary>
        /// 解析机台配置文件
        /// </summary>
        private static VstIniParser RackIniParser;

        /// <summary>
        /// 解析许可证文件
        /// </summary>
        private static VstIniParser LicenseIniParser;
        /// <summary>
        /// 许可证LICENS PKEY
        /// </summary>
        private static string LicensePKEY { get; set; }
        /// <summary>
        /// 许可证LICENS PKEY
        /// </summary>
        private static string LicenseMKEY { get; set; }
        /// <summary>
        /// 许可证LICENS PKEY
        /// </summary>
        private static string LicenseOKEY { get; set; }
        /// <summary>
        /// 许可证LICENS DKEY
        /// </summary>
        private static string LicenseDKEY { get; set; }
        /// <summary>
        /// 许可证有效期
        /// </summary>
        public static DateTime LicenseDueDate { get; set; }

        /// <summary>
        /// 解析设备文件
        /// </summary>
        private static VstIniParser EquipIniParser;
        /// <summary>
        /// 设备名称
        /// </summary>
        public static string EquipName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public static string EquipPartNo { get; set; }
        /// <summary>
        /// 设备客户
        /// </summary>
        public static string EquipCustomer { get; set; }
        /// <summary>
        /// 热成像仪
        /// </summary>
        public static bool ThermCamEnable { get; set; }

        /// <summary>
        /// 机台名称
        /// </summary>
        public static string RackName { get; set; }
        /// <summary>
        /// 机台编号
        /// </summary>
        public static string RackID { get; set; }

        /// <summary>
        /// 是否成功加载配置文件
        /// </summary>
        public static bool LOADED { get; set; }

        /// <summary>
        /// 读取配置数据
        /// </summary>
        public static void ReadConfig()
        {
            try
            {
                ReadRoot();
                ReadRack();
                ReadEquip();
                ReadLicense();
                LOADED = true;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("读取基础配置信息失败！");
                LOADED = false;
            }
        }

        #region 根配置

        /// <summary>
        /// 读取根配置
        /// </summary>
        private static void ReadRoot()
        {
            string appPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            RootIniParser = new VstIniParser(appPath + RootFileName);

            RackFilePath = RootIniParser.GetSetting("RACK", "PATH");
            RoutineFilePath = RootIniParser.GetSetting("ROUTINE", "PATH");
            UserFilePath = RootIniParser.GetSetting("USER", "PATH");
            TestMappingFilePath = RootIniParser.GetSetting("TEST", "PATH");
            OutputFolderPath = RootIniParser.GetSetting("OUTPUT", "PATH");
            CalibrationFilePath = RootIniParser.GetSetting("CALIBRATION", "PATH");
            ThermMeasFilePath = RootIniParser.GetSetting("THERMMEAS", "PATH");

            LicenseFilePath = appPath + LicenseFileName;
            EquipFilePath = appPath + EquipFileName;
        }

        /// <summary>
        /// 设置根配置
        /// </summary>
        /// <param name="rackFilePath"></param>
        /// <param name="scriptFilePath"></param>
        /// <param name="calibrationFilePath"></param>
        /// <param name="licenseFilePath"></param>
        public static void SetRoot(StRoot rootInfo)
        {
            // 文件是否存在的检查由调用者来完成
            RackFilePath = rootInfo.RackFilePath;
            RootIniParser.AddSetting("RACK", "PATH", RackFilePath);
            RoutineFilePath = rootInfo.RoutineFilePath;
            RootIniParser.AddSetting("SCRIPT", "PATH", RoutineFilePath);
            UserFilePath = rootInfo.UserFilePath;
            RootIniParser.AddSetting("USER", "PATH", UserFilePath);
            TestMappingFilePath = rootInfo.TestMappingFilePath;
            RootIniParser.AddSetting("TEST", "PATH", TestMappingFilePath);
            OutputFolderPath = rootInfo.OutputFolderPath;
            RootIniParser.AddSetting("OUTPUT", "PATH", OutputFolderPath);
            CalibrationFilePath = rootInfo.CalibrationFilePath;
            ThermMeasFilePath = rootInfo.ThermMeasFilePath;
            // 保存到文件
            try
            {
                RootIniParser.SaveSettings();
            }
            catch
            {
                WarnWindow.ShowMsg("更新根文件失败！请确认检查根文件是否存在！");
            }
        }

        /// <summary>
        /// 从内存中获得各个配置文件的路径
        /// </summary>
        /// <returns></returns>
        public static StRoot GetRootData()
        {
            StRoot rootInfo = new StRoot();
            rootInfo.RackFilePath = RackFilePath;
            rootInfo.RoutineFilePath = RoutineFilePath;
            rootInfo.UserFilePath = UserFilePath;
            rootInfo.TestMappingFilePath = TestMappingFilePath;
            rootInfo.OutputFolderPath = OutputFolderPath;
            rootInfo.CalibrationFilePath = CalibrationFilePath;
            return rootInfo;
        }

        /// <summary>
        /// 获取输出数据文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetOutFolder()
        {
            if (OutputFolderPath == "" || OutputFolderPath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Result");
            else
                return OutputFolderPath;
        }

        /// <summary>
        /// 获取用户文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetUserFilePath()
        {
            if (UserFilePath == "" || UserFilePath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + UserFileName);
            else
                return UserFilePath;
        }

        /// <summary>
        /// 获取校准文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetCalibrationFilePath()
        {
            if (CalibrationFilePath == "" || CalibrationFilePath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + CalibrationFileName);
            else
                return CalibrationFilePath;
        }

        /// <summary>
        /// 获取测试映射文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetTestMappingFilePath()
        {
            if (TestMappingFilePath == "" || TestMappingFilePath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Rule\\RULE.TEST.mdb");
            else
                return TestMappingFilePath;
        }

        /// <summary>
        /// 获取测试程序文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetRoutineFilePath()
        {
            if (RoutineFilePath == "" || RoutineFilePath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + RoutineFileName);
            else
                return RoutineFilePath;
        }

        /// <summary>
        /// 获取热像区域文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetThermMeasFilePath()
        {
            if (ThermMeasFilePath == "" || ThermMeasFilePath.ToUpper() == DEFAULT)
                return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + ThermMeasFileName);
            else
                return ThermMeasFilePath;
        }

        /// <summary>
        /// 获取系统配置文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetSystemFilePath()
        {
            return (System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + SystemFileName);
        }

        #endregion

        #region 机台配置

        /// <summary>
        /// 读取机台配置
        /// </summary>
        private static void ReadRack()
        {
            if (RackFilePath == "" || RackFilePath.ToUpper() == DEFAULT)
                RackIniParser = new VstIniParser(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + RackFileName);
            else
                RackIniParser = new VstIniParser(RackFilePath);
            RackName = RackIniParser.GetSetting("RACK", "RACK_NAME");
            RackID = RackIniParser.GetSetting("RACK", "RACK_ID");
        }


        /// <summary>
        /// 保存机台信息
        /// </summary>
        /// <param name="rackInfo"></param>
        public static void SetRack(string rackName, string rackID)
        {
            RackName = rackName;
            RackID = rackID;
            RackIniParser.AddSetting("RACK", "RACK_NAME", rackName);
            RackIniParser.AddSetting("RACK", "RACK_ID", rackID);
            //保存到文件
            try
            {
                RackIniParser.SaveSettings();
            }
            catch
            {
                WarnWindow.ShowMsg("更新机台信息文件失败！请确认检查配置文件路径是否有效！");
            }
        }

        #endregion     

        #region 许可证

        /// <summary>
        /// 读取运行记录
        /// </summary>
        private static void ReadLicense()
        {
            LicenseIniParser = new VstIniParser(LicenseFilePath);
            string[] licenseNames = LicenseIniParser.EnumSectionNames();
            if (licenseNames.Length > 0)
            {
                LicensePKEY = LicenseIniParser.GetSetting(licenseNames[0], "PKEY");
                LicenseOKEY = LicenseIniParser.GetSetting(licenseNames[0], "OKEY");
                LicenseMKEY = LicenseIniParser.GetSetting(licenseNames[0], "MKEY");
                LicenseDKEY = LicenseIniParser.GetSetting(licenseNames[0], "DKEY");
            }
            else
            {
                LicensePKEY = "";
                LicenseOKEY = "";
                LicenseMKEY = "";
                LicenseDKEY = "";
            }
            // 默认有效期
            LicenseDueDate = new DateTime(1990, 12, 31);
        }

        /// <summary>
        /// 设置PLC SN
        /// </summary>
        /// <param name="sn"></param>
        public static void SetDevSN(string srcSn, string osaSn, string motorSn)
        {
            try
            {
                // SN null
                if (srcSn == null || osaSn == null || motorSn == null)
                {
                    LicenseDueDate = new DateTime(1990, 12, 31);
                    return;
                }

                // 加密算法的 KEY
                string key = "vsstechl";
                // 加密算法的 IV
                string iv = "cosbt#$*";
                // 解密SN
                string pkey = DESDecrypt(LicensePKEY, key, iv);
                string okey = DESDecrypt(LicenseOKEY, key, iv);
                string mkey = DESDecrypt(LicenseMKEY, key, iv);

                string strSrcSN = srcSn.PadLeft(8, '0');
                if (strSrcSN.Length > 8)
                    strSrcSN = strSrcSN.Substring(strSrcSN.Length - 8, 8);

                string strOsaSN = osaSn.PadLeft(8, '0');
                if (strOsaSN.Length > 8)
                    strOsaSN = strOsaSN.Substring(strOsaSN.Length - 8, 8);

                string strMotorSN = motorSn.PadLeft(8, '0');
                if (strMotorSN.Length > 8)
                    strMotorSN = strMotorSN.Substring(strMotorSN.Length - 8, 8);

                if (pkey == strSrcSN && okey == strOsaSN && mkey == strMotorSN)
                {
                    // 解密日期
                    string strDue = DESDecrypt(LicenseDKEY, key, iv);
                    // 更新日期
                    LicenseDueDate = new DateTime(Convert.ToInt32(strDue.Substring(0, 4).ToString()), Convert.ToInt32(strDue.Substring(4, 2)), Convert.ToInt32(strDue.Substring(6, 2)));
                }
                else
                {
                    LicenseDueDate = new DateTime(1990, 12, 31);
                }
            }
            catch
            {
                LicenseDueDate = new DateTime(1990, 12, 31);
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        private static string DESDecrypt(string data, string key, string iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            byEnc = Convert.FromBase64String(data);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        #endregion

        #region 设备信息

        /// <summary>
        /// 读取设备信息
        /// </summary>
        private static void ReadEquip()
        {
            EquipIniParser = new VstIniParser(EquipFilePath);
            string[] equipNames = EquipIniParser.EnumSectionNames();
            if (equipNames.Length > 0)
            {
                EquipName = EquipIniParser.GetSetting(equipNames[0], "NAME");
                EquipPartNo = EquipIniParser.GetSetting(equipNames[0], "PN");
                EquipCustomer = EquipIniParser.GetSetting(equipNames[0], "CUSTOMER");
                if (EquipCustomer.ToUpper() == "")
                    EquipCustomer = "WINCOSEMI";
                string thermalCameraEnable = EquipIniParser.GetSetting(equipNames[0], "THERMCAM");
                ErrLog.WriteLine(thermalCameraEnable);
                if (thermalCameraEnable.ToUpper() == "")
                    ThermCamEnable = true;
                else
                    ThermCamEnable = Convert.ToBoolean(thermalCameraEnable);
            }
            else
            {
                EquipPartNo = "";
            }
        }
        #endregion


    }

    /// <summary>
    /// Root配置结构体
    /// </summary>
    public struct StRoot
    {
        public string RackFilePath;
        public string RoutineFilePath;
        public string UserFilePath;
        public string TestMappingFilePath;
        public string OutputFolderPath;
        public string CalibrationFilePath;
        public string ThermMeasFilePath;
    }
}
