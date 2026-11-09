using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstStream;

namespace CosTest
{
    /// <summary>
    /// 测试系统配置
    /// </summary>
    class TestSystemUtil
    {
        #region 参数区
        /// <summary>
        /// Y轴导程(mm)
        /// </summary>
        internal static double YPITCH = 2.0;
        /// <summary>
        /// Y轴电子齿轮比
        /// </summary>
        internal static double YEGEAR = 20000;
        /// <summary>
        /// Y轴长度(mm)与编码对应关系
        /// 导程 * 电子齿轮比
        /// </summary>
        internal static double YMM2CNT = 2.0 * 20000;


 
        /// <summary>
        /// Y轴初始化时的运动速度
        /// </summary>
        internal static double YSPEEDINIT = 1;
        /// <summary>
        /// Y轴运动速度 - 复位/非扫描是的速度
        /// </summary>
        internal static double YSPEED = 1;
        /// <summary>
        /// Y轴NA扫描的起始位置(单位mm)
        /// </summary>
        internal static double YPANABG = 1;
        /// <summary>
        /// Y轴NA扫描的结束位置(单位mm)
        /// </summary>
        internal static double YPANAEND = 20;
        /// <summary>
        /// Y轴遮光板完全打开时的位置(单位mm)
        /// </summary>
        internal static double YPASRO = 0;
        /// <summary>
        /// Y轴遮光板完全关闭时的位置(单位mm)
        /// </summary>
        internal static double YPASRC = 20;
        /// <summary>
        /// NA扫描时的功率阈值
        /// </summary>
        internal static double NA_PO_THRESH = 0.9;
        /// <summary>
        /// NA等效过孔直径
        /// </summary>
        internal static double NA_EQ_DIAMETER = 5;
        /// <summary>
        /// NA扫描时 CT头与马达复位点的等效距离
        /// </summary>
        internal static double NA_ORG_CT_EQ_DIST = 8.0;
        /// <summary>
        /// NA扫描时上电后的稳定时间
        /// </summary>
        internal static double NA_STABLE_TIME_S = 5.0;
        /// <summary>
        /// NA扫描时的运动时间
        /// </summary>
        internal static double NA_SCAN_TIME_S = 10.0;
        /// <summary>
        /// 最多测试多少次NA
        /// </summary>
        internal static int NA_MAX_TEST_COUNT = 1;
        /// <summary>
        /// 最大自动积分时间
        /// </summary>
        internal static double MaxAutoIntegrationTimeMilliSec = 2000;
        /// <summary>
        /// 参考积分时间
        /// </summary>
        internal static double RefIntegrationTimeMilliSec = 2;
        /// <summary>
        /// 最大驱动电流
        /// </summary>
        internal static double MaxDriveCurrentA = 25;
        /// <summary>
        /// 最大驱动电压
        /// </summary>
        internal static double MaxDriveVoltageV = 40;
        /// <summary>
        /// 最小LIV延时时间毫秒
        /// </summary>
        internal static double MinLivDelayMilliSec = 200;
        /// <summary>
        /// 最大LIV延时时间毫秒
        /// </summary>
        internal static double MaxLivDelayMilliSec = 100000;
        /// <summary>
        /// 温控通讯串口
        /// </summary>
        internal static string TEMP_COMM_PORT;
        /// <summary>
        /// 功率计串口通讯地址
        /// </summary>
        internal static string PD_COMM_PORT;
        /// <summary>
        /// 运动控制器通讯串口地址
        /// </summary>
        internal static string MOTOR_COMM_PORT;
        /// <summary>
        /// 光斑相机索引
        /// </summary>
        internal static int BEAM_USB_CAM_ID;
        /// <summary>
        /// TEMP_UCL
        /// </summary>
        internal static double TEMP_UCL = 40.0;
        /// <summary>
        /// TEMP_LCL
        /// </summary>
        internal static double TEMP_LCL = 10.0;
        /// <summary>
        /// 设定温度的允许偏差
        /// </summary>
        internal static double TEMP_ERR_UCL = 1.0;
        /// <summary>
        /// 温度控制超时时间
        /// </summary>
        internal static double TEMP_TIME_OUT_S = 300.0;
        /// <summary>
        /// 温度稳定后的时间
        /// </summary>
        internal static double TEMP_STABLE_TIME_S = 60.0;
        /// <summary>
        /// 光谱扫描时上电后的稳定时间
        /// </summary>
        internal static double SP_STABLE_TIME_S = 0.1;
        /// <summary>
        /// 光斑扫描时上电后的稳定时间
        /// </summary>
        internal static double BEAMSCAN_STABLE_TIME_S = 10;
        /// <summary>
        /// 热成像扫描时上电后的稳定时间
        /// </summary>
        internal static double THERMSCAN_STABLE_TIME_S = 10;
        /// <summary>
        /// TEC_CURR_UCL
        /// </summary>
        internal static double TEC_CURR_UCL = 40.0;
        /// <summary>
        /// TEC_VOLT_UCL
        /// </summary>
        internal static double TEC_VOLT_UCL = 10.0;
        /// <summary>
        /// LASER_CURR_UCL
        /// </summary>
        internal static double LASER_CURR_UCL = 20.0;
        /// <summary>
        /// LASER_VOLT_UCL
        /// </summary>
        internal static double LASER_VOLT_UCL = 40.0;
        /// <summary>
        /// 最大TEC电流
        /// </summary>
        internal static double MaxTecCurrentA = 10;
        /// <summary>
        /// 最大TEC电压
        /// </summary>
        internal static double MaxTecVoltageV = 15;

        /// <summary>
        /// PD RGANGE
        /// </summary>
        internal static int PD_RNG = 0x02;

        /// <summary>
        /// SI校准系数-CAL
        /// </summary>
        internal static double SI_CAL = 1.0;
        /// <summary>
        /// SI校准系数-OFFSET
        /// </summary>
        internal static double SI_OFFSET = 0.0;
        /// <summary>
        /// IF校准系数-CAL
        /// </summary>
        internal static double IF_CAL = 1.0;
        /// <summary>
        /// IF校准系数-OFFSET
        /// </summary>
        internal static double IF_OFFSET = 0.0;
        /// <summary>
        /// VF校准系数-CAL
        /// </summary>
        internal static double VF_CAL = 1.0;
        /// <summary>
        /// VF校准系数-OFFSET
        /// </summary>
        internal static double VF_OFFSET = 0.0;

        /// <summary>
        /// TEMP校准系数(设置)-CAL
        /// </summary>
        internal static double ST_CAL = 1.0;
        /// <summary>
        /// TEMP校准系数(设置)-OFFSET
        /// </summary>
        internal static double ST_OFFSET = 0.0;

        /// <summary>
        /// TEMP校准系数(读取)-CAL
        /// </summary>
        internal static double RT_CAL = 1.0;
        /// <summary>
        /// TEMP校准系数(读取)-OFFSET
        /// </summary>
        internal static double RT_OFFSET = 0.0;

        /// <summary>
        /// SP光谱噪声
        /// </summary>
        internal static double SP_NOISE = 0.0;
        /// <summary>
        /// SP光谱噪声
        /// </summary>
        internal static bool SP_DENOISE_ENABLE = false;

        /// <summary>
        /// SI校准系数-CAL
        /// </summary>
        internal static double SI_CAL_BASE = 1.0;
        /// <summary>
        /// SI校准系数-OFFSET
        /// </summary>
        internal static double SI_OFFSET_BASE = 0.0;
        /// <summary>
        /// IF校准系数-CAL
        /// </summary>
        internal static double IF_CAL_BASE = 1.0;
        /// <summary>
        /// IF校准系数-OFFSET
        /// </summary>
        internal static double IF_OFFSET_BASE = 0.0;
        /// <summary>
        /// VF校准系数-CAL
        /// </summary>
        internal static double VF_CAL_BASE = 1.0;
        /// <summary>
        /// VF校准系数-OFFSET
        /// </summary>
        internal static double VF_OFFSET_BASE = 0.0;
        /// <summary>
        /// WL校准系数-OFFSET
        /// </summary>
        internal static double WL_OFFSET_BASE = 0.0;
        /// <summary>
        /// PO校准系数-CAL
        /// </summary>
        internal static List<StPoCal> PO_CAL_LIST;
        /// <summary>
        /// PDI_DARK_BASE
        /// </summary>
        internal static int PDI_ADC_OFFSET = 0;

        /// <summary>
        /// TEMP校准系数(设置)-CAL
        /// </summary>
        internal static double ST_CAL_BASE = 1.0;
        /// <summary>
        /// TEMP校准系数(设置)-OFFSET
        /// </summary>
        internal static double ST_OFFSET_BASE = 0.0;

        /// <summary>
        /// TEMP校准系数(读取)-CAL
        /// </summary>
        internal static double RT_CAL_BASE = 1.0;
        /// <summary>
        /// TEMP校准系数(读取)-OFFSET
        /// </summary>
        internal static double RT_OFFSET_BASE = 0.0;

        /// <summary>
        /// 缺省最大LIMIT
        /// </summary>
        internal static double DEFAULT_MAX_LIMIT = 9999;
        /// <summary>
        /// 缺省最小LIMIT
        /// </summary>
        internal static double DEFAULT_MIN_LIMIT = -9999;
        /// <summary>
        /// 缺省测试值
        /// </summary>
        internal static double DEFAULT_TEST_VALUE = -9999.9;


        /// <summary>
        /// 线阻补偿
        /// </summary>
        internal static double WRES_BASE = 0.01;

        /// <summary>
        /// 预导通电流，用于去掉探针氧化层，保证加电稳定性
        /// </summary>
        internal const double PRE_ON_CURRENT = 5.0;
        /// <summary>
        /// 计算效率时的最小电流，低于此值默认无效
        /// </summary>
        internal const double EFF_MIN_CURRENT = 0.5;
        /// <summary>
        /// LIV的点数采用这个点
        /// </summary>
        internal const int MIN_LIV_COUNT = 50;
        /// <summary>
        /// LIV的点数采用这个点
        /// </summary>
        internal const int MAX_LIV_COUNT = 1500;



        #endregion

        /// <summary>
        /// 解析机台配置文件
        /// </summary>
        private static VstIniParser SystemIniParser;

        /// <summary>
        /// 解析校准配置文件
        /// </summary>
        private static VstIniParser CalibrationIniParser;

        /// <summary>
        /// 是否成功加载配置文件
        /// </summary>
        internal static bool LOADED { get; set; }

        /// <summary>
        /// PRE_TEST
        /// </summary>
        internal const string PRE_TEST = "Pre Test";
        /// <summary>
        /// POST_TEST
        /// </summary>
        internal const string POST_TEST = "Final Test";
        /// <summary>
        /// PRE_TEST
        /// </summary>
        internal const string PRE_TEST_ABBR = "PRE";
        /// <summary>
        /// POST_TEST
        /// </summary>
        internal const string POST_TEST_ABBR = "FINAL";

        /// <summary>
        /// 读取机台配置
        /// </summary>
        internal static void ReadConfig()
        {
            try
            {
                SystemIniParser = new VstIniParser(StationUtil.GetSystemFilePath());

                YPITCH = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YPITCH"));
                YEGEAR = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YEGEAR"));
                YSPEEDINIT = Convert.ToInt32(SystemIniParser.GetSetting("SYSTEM", "YSPEEDINIT"));
                YSPEED = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YSPEED"));
                
               
                YSPEED = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YSPEED"));
                YPANABG = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YPANABG"));
                YPANAEND = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YPANAEND"));
                YPASRO = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YPASRO"));
                YPASRC = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "YPASRC"));
                SP_STABLE_TIME_S = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "SP_STABLE_TIME_S"));
                NA_PO_THRESH = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "NA_PO_THRESH"));
                NA_EQ_DIAMETER = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "NA_EQ_DIAMETER"));
                NA_ORG_CT_EQ_DIST = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "NA_ORG_CT_EQ_DIST"));
                NA_STABLE_TIME_S = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "NA_STABLE_TIME_S"));
                NA_SCAN_TIME_S = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "NA_SCAN_TIME_S"));
                NA_MAX_TEST_COUNT = Convert.ToInt32(SystemIniParser.GetSetting("SYSTEM", "NA_MAX_TEST_COUNT"));
                TEMP_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEMP_UCL"));
                TEMP_LCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEMP_LCL"));
                TEMP_ERR_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEMP_ERR_UCL"));
                TEMP_TIME_OUT_S = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEMP_TIME_OUT_S"));
                TEMP_STABLE_TIME_S = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEMP_STABLE_TIME_S"));
                TEC_CURR_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEC_CURR_UCL"));
                TEC_VOLT_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "TEC_VOLT_UCL"));
                LASER_CURR_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "LASER_CURR_UCL"));
                LASER_VOLT_UCL = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "LASER_VOLT_UCL"));
                RefIntegrationTimeMilliSec = Convert.ToDouble(SystemIniParser.GetSetting("SYSTEM", "RefIntegrationTimeMilliSec"));
                PD_RNG = Convert.ToInt32(SystemIniParser.GetSetting("SYSTEM", "PD_RNG"));
                PD_COMM_PORT = SystemIniParser.GetSetting("SYSTEM", "PD_COMM_PORT");
                TEMP_COMM_PORT = SystemIniParser.GetSetting("SYSTEM", "TEMP_COMM_PORT");
                MOTOR_COMM_PORT = SystemIniParser.GetSetting("SYSTEM", "MOTOR_COMM_PORT");
                //BEAM_USB_CAM_ID = Convert.ToInt32(SystemIniParser.GetSetting("SYSTEM", "BEAM_USB_CAM_ID"));

                MaxDriveCurrentA = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxDriveCurrentA"));
                MaxDriveVoltageV = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxDriveVoltageV"));
                MaxTecCurrentA = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxTecCurrentA"));
                MaxTecVoltageV = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxTecVoltageV"));
                MinLivDelayMilliSec = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MinLivDelayMilliSec"));
                MaxLivDelayMilliSec = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxLivDelayMilliSec"));
                MaxAutoIntegrationTimeMilliSec = Convert.ToDouble(SystemIniParser.GetSetting("LIMIT", "MaxAutoIntegrationTimeMilliSec"));

                CalibrationIniParser = new VstIniParser(StationUtil.GetCalibrationFilePath());
                SI_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "SI_CAL"));
                SI_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "SI_OFFSET"));
                IF_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "I_CAL"));
                IF_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "I_OFFSET"));
                VF_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "V_CAL"));
                VF_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "V_OFFSET"));
                ST_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "ST_CAL"));
                ST_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "ST_OFFSET"));
                RT_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "RT_CAL"));
                RT_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "RT_OFFSET"));
                SP_NOISE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "SP_NOISE"));
                SP_DENOISE_ENABLE = Convert.ToBoolean(CalibrationIniParser.GetSetting("CALIBRATION", "SP_DENOISE_ENABLE"));

                PDI_ADC_OFFSET = Convert.ToInt32(CalibrationIniParser.GetSetting("CALIBRATION", "PDI_ADC_OFFSET"));
                SI_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "SI_CAL_BASE"));
                SI_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "SI_OFFSET_BASE"));
                IF_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "I_CAL_BASE"));
                IF_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "I_OFFSET_BASE"));
                VF_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "V_CAL_BASE"));
                VF_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "V_OFFSET_BASE"));
                ST_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "ST_CAL_BASE"));
                ST_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "ST_OFFSET_BASE"));
                RT_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "RT_CAL_BASE"));
                RT_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "RT_OFFSET_BASE"));
                WL_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "WL_OFFSET_BASE"));
                WRES_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", "WRES_BASE"));

                PO_CAL_LIST = new List<StPoCal>();
                List<double> wls = new List<double>();
                // Setting List
                string[] settingNames = CalibrationIniParser.EnumSection("CALIBRATION");
                // Search
                foreach (string settingName in settingNames)
                {
                    if (settingName.StartsWith("PO_"))
                    {
                        double wavelength = Convert.ToDouble(settingName.Substring(settingName.IndexOf('@') + 1, settingName.Length - 1 - settingName.IndexOf('@')));
                        int index = wls.IndexOf(wavelength);
                        if (index < 0)
                        {
                            PO_CAL_LIST.Add(new StPoCal() { WAVELENGTH_BASE = wavelength, PO_CAL = 1.0, PO_CAL_BASE = 1.0, PO_OFFSET = 0.0, PO_OFFSET_BASE = 0.0, PO_WLOFFSET_BASE = 0.0 });
                            wls.Add(wavelength);
                            index = wls.Count - 1;
                        }

                         if (settingName.StartsWith("PO_CAL_BASE"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_CAL_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                        else if (settingName.StartsWith("PO_OFFSET_BASE"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                        else if(settingName.StartsWith("PO_CAL"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_CAL = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                        else if (settingName.StartsWith("PO_OFFSET"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_OFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                        else if (settingName.StartsWith("PO_WLOFFSET_BASE"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_OFFSET_BASE = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                        else if (settingName.StartsWith("PO_WLOFFSET"))
                        {
                            StPoCal poCal = PO_CAL_LIST[index];
                            poCal.PO_WLOFFSET = Convert.ToDouble(CalibrationIniParser.GetSetting("CALIBRATION", settingName));
                            PO_CAL_LIST[index] = poCal;
                        }
                    }
                }
                // 检查YMM2CNT的值
                if(YPITCH <= 0)
                    throw new Exception("YPITCH is too small");
                if (YEGEAR <= 0)
                    throw new Exception("YEGEAR is too small");
                YMM2CNT = YEGEAR / YPITCH;

                LOADED = true;
            }
            catch (Exception ex)
            {
                LOADED = false;
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("读取系统配置信息或校准信息失败！");
            }
        }


        /// <summary>
        /// 保存机台信息
        /// </summary>
        /// <param name="rackInfo"></param>
        internal static void SaveConfig()
        {
            SystemIniParser.DeleteSetting("SYSTEM");
            SystemIniParser.AddSetting("SYSTEM", "YPITCH", YPITCH.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YEGEAR", YEGEAR.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YSPEEDINIT", YSPEEDINIT.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YSPEED", YSPEED.ToString());
            
            SystemIniParser.AddSetting("SYSTEM", "YPANABG", YPANABG.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YPANAEND", YPANAEND.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YPASRO", YPASRO.ToString());
            SystemIniParser.AddSetting("SYSTEM", "YPASRC", YPASRC.ToString());
            SystemIniParser.AddSetting("SYSTEM", "SP_STABLE_TIME_S", SP_STABLE_TIME_S.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_PO_THRESH", NA_PO_THRESH.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_EQ_DIAMETER", NA_EQ_DIAMETER.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_ORG_CT_EQ_DIST", NA_ORG_CT_EQ_DIST.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_MAX_TEST_COUNT", NA_MAX_TEST_COUNT.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_STABLE_TIME_S", NA_STABLE_TIME_S.ToString());
            SystemIniParser.AddSetting("SYSTEM", "NA_SCAN_TIME_S", NA_SCAN_TIME_S.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_UCL", TEMP_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_LCL", TEMP_LCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_ERR_UCL", TEMP_ERR_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_TIME_OUT_S", TEMP_TIME_OUT_S.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_STABLE_TIME_S", TEMP_STABLE_TIME_S.ToString());

            SystemIniParser.AddSetting("SYSTEM", "TEC_CURR_UCL", TEC_CURR_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEC_VOLT_UCL", TEC_VOLT_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "LASER_CURR_UCL", LASER_CURR_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "LASER_VOLT_UCL", LASER_VOLT_UCL.ToString());
            SystemIniParser.AddSetting("SYSTEM", "RefIntegrationTimeMilliSec", RefIntegrationTimeMilliSec.ToString());
            SystemIniParser.AddSetting("SYSTEM", "PD_RNG", PD_RNG.ToString());
            SystemIniParser.AddSetting("SYSTEM", "PD_COMM_PORT", PD_COMM_PORT.ToString());
            SystemIniParser.AddSetting("SYSTEM", "TEMP_COMM_PORT", TEMP_COMM_PORT.ToString());
            SystemIniParser.AddSetting("SYSTEM", "MOTOR_COMM_PORT", MOTOR_COMM_PORT.ToString());
            //SystemIniParser.AddSetting("SYSTEM", "BEAM_USB_CAM_ID", BEAM_USB_CAM_ID.ToString());

            SystemIniParser.AddSetting("LIMIT", "MaxDriveCurrentA", MaxDriveCurrentA.ToString());
            SystemIniParser.AddSetting("LIMIT", "MaxDriveVoltageV", MaxDriveVoltageV.ToString());
            SystemIniParser.AddSetting("LIMIT", "MaxTecCurrentA", MaxTecCurrentA.ToString());
            SystemIniParser.AddSetting("LIMIT", "MaxTecVoltageV", MaxTecVoltageV.ToString());
            SystemIniParser.AddSetting("LIMIT", "MinLivDelayMilliSec", MinLivDelayMilliSec.ToString());
            SystemIniParser.AddSetting("LIMIT", "MaxLivDelayMilliSec", MaxLivDelayMilliSec.ToString());
            SystemIniParser.AddSetting("LIMIT", "MaxAutoIntegrationTimeMilliSec", MaxAutoIntegrationTimeMilliSec.ToString());

            CalibrationIniParser.DeleteSetting("CALIBRATION");
            CalibrationIniParser.AddSetting("CALIBRATION", "SI_CAL", SI_CAL.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "SI_OFFSET", SI_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "I_CAL", IF_CAL.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "I_OFFSET", IF_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "V_CAL", VF_CAL.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "V_OFFSET", VF_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "PDI_ADC_OFFSET", PDI_ADC_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "ST_CAL", ST_CAL.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "ST_OFFSET", ST_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "RT_CAL", RT_CAL.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "RT_OFFSET", RT_OFFSET.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "SP_NOISE", SP_NOISE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "SP_DENOISE_ENABLE", SP_DENOISE_ENABLE.ToString());

            CalibrationIniParser.AddSetting("CALIBRATION", "SI_CAL_BASE", SI_CAL_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "SI_OFFSET_BASE", SI_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "I_CAL_BASE", IF_CAL_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "I_OFFSET_BASE", IF_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "V_CAL_BASE", VF_CAL_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "V_OFFSET_BASE", VF_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "ST_CAL_BASE", ST_CAL_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "ST_OFFSET_BASE", ST_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "RT_CAL_BASE", RT_CAL_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "RT_OFFSET_BASE", RT_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "WL_OFFSET_BASE", WL_OFFSET_BASE.ToString());
            CalibrationIniParser.AddSetting("CALIBRATION", "WRES_BASE", WRES_BASE.ToString());

            foreach (StPoCal poCal in PO_CAL_LIST)
            {
                string strWL = poCal.WAVELENGTH_BASE.ToString();
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_CAL@" + strWL, poCal.PO_CAL.ToString());
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_OFFSET@" + strWL, poCal.PO_OFFSET.ToString());
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_CAL_BASE@" + strWL, poCal.PO_CAL_BASE.ToString());
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_OFFSET_BASE@" + strWL, poCal.PO_OFFSET_BASE.ToString());
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_WLOFFSET@" + strWL, poCal.PO_WLOFFSET.ToString());
                CalibrationIniParser.AddSetting("CALIBRATION", "PO_WLOFFSET_BASE@" + strWL, poCal.PO_WLOFFSET_BASE.ToString());
            }

            //保存到文件
            try
            {
                SystemIniParser.SaveSettings();
                CalibrationIniParser.SaveSettings();
            }
            catch
            {
                ErrLog.WriteLine("更新系统配置文件失败！请确认检查配置文件路径是否有效！");
                WarnWindow.ShowMsg("更新系统配置文件失败！请确认检查配置文件路径是否有效！");
            }
        }
    }
}
