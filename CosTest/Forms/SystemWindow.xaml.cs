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
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;

namespace CosTest
{
    /// <summary>
    /// Interaction logic for SystemWindow.xaml
    /// </summary>
    partial class SystemWindow : Window, INotifyPropertyChanged
    {
        #region BINDING

        /// <summary>
        /// RackName
        /// </summary>
        private string _RackName;
        /// <summary>
        /// RackName
        /// </summary>
        public string RackName
        {
            get { return _RackName; }
            set { _RackName = value; OnPropertyChanged("RackName"); }
        }

        /// <summary>
        /// RackId
        /// </summary>
        private string _RackId;
        /// <summary>
        /// RackId
        /// </summary>
        public string RackId
        {
            get { return _RackId; }
            set { _RackId = value; OnPropertyChanged("RackId"); }
        }

        /// <summary>
        /// RackFilePath
        /// </summary>
        private string _RackFilePath;
        /// <summary>
        /// RackFilePath
        /// </summary>
        public string RackFilePath
        {
            get { return _RackFilePath; }
            set { _RackFilePath = value; OnPropertyChanged("RackFilePath"); }
        }

        /// <summary>
        /// CalibrationFilePath
        /// </summary>
        private string _CalibrationFilePath;
        /// <summary>
        /// CalibrationFilePath
        /// </summary>
        public string CalibrationFilePath
        {
            get { return _CalibrationFilePath; }
            set { _CalibrationFilePath = value; OnPropertyChanged("CalibrationFilePath"); }
        }

        /// <summary>
        /// UserFilePath
        /// </summary>
        private string _UserFilePath;
        /// <summary>
        /// UserFilePath
        /// </summary>
        public string UserFilePath
        {
            get { return _UserFilePath; }
            set { _UserFilePath = value; OnPropertyChanged("UserFilePath"); }
        }

        /// <summary>
        /// TestMappingFilePath
        /// </summary>
        private string _TestMappingFilePath;
        /// <summary>
        /// TestMappingFilePath
        /// </summary>
        public string TestMappingFilePath
        {
            get { return _TestMappingFilePath; }
            set { _TestMappingFilePath = value; OnPropertyChanged("TestMappingFilePath"); }
        }

        /// <summary>
        /// OutputFolderPath
        /// </summary>
        private string _OutputFolderPath;
        /// <summary>
        /// OutputFolderPath
        /// </summary>
        public string OutputFolderPath
        {
            get { return _OutputFolderPath; }
            set { _OutputFolderPath = value; OnPropertyChanged("OutputFolderPath"); }
        }

        /// <summary>
        /// OutputFolderPath
        /// </summary>
        private string _RoutineFilePath;
        /// <summary>
        /// OutputFolderPath
        /// </summary>
        public string RoutineFilePath
        {
            get { return _RoutineFilePath; }
            set { _RoutineFilePath = value; OnPropertyChanged("RoutineFilePath"); }
        }

        /// <summary>
        /// IS_MOTOR_ENABLED
        /// </summary>
        private bool _IS_MOTOR_ENABLED;
        /// <summary>
        /// IS_MOTOR_ENABLED
        /// </summary>
        public bool IS_MOTOR_ENABLED
        {
            get { return _IS_MOTOR_ENABLED; }
            set { _IS_MOTOR_ENABLED = value; OnPropertyChanged("IS_MOTOR_ENABLED"); }
        }

        /// <summary>
        /// YSPEED
        /// </summary>
        private double _YSPEED;
        /// <summary>
        /// YSPEED
        /// </summary>
        public double YSPEED
        {
            get { return _YSPEED; }
            set { _YSPEED = value; OnPropertyChanged("YSPEED"); }
        }

        /// <summary>
        /// YPANABG
        /// </summary>
        private double _YPANABG;
        /// <summary>
        /// YPANABG
        /// </summary>
        public double YPANABG
        {
            get { return _YPANABG; }
            set { _YPANABG = value; OnPropertyChanged("YPANABG"); }
        }

        /// <summary>
        /// YPANAEND
        /// </summary>
        private double _YPANAEND;
        /// <summary>
        /// YPANAEND
        /// </summary>
        public double YPANAEND
        {
            get { return _YPANAEND; }
            set { _YPANAEND = value; OnPropertyChanged("YPANAEND"); }
        }

        /// <summary>
        /// NA_EQ_DIAMETER
        /// </summary>
        private double _NA_EQ_DIAMETER;
        /// <summary>
        /// NA_EQ_DIAMETER
        /// </summary>
        public double NA_EQ_DIAMETER
        {
            get { return _NA_EQ_DIAMETER; }
            set { _NA_EQ_DIAMETER = value; OnPropertyChanged("NA_EQ_DIAMETER"); }
        }

        /// <summary>
        /// NA_ORG_CT_EQ_DIST
        /// </summary>
        private double _NA_ORG_CT_EQ_DIST;
        /// <summary>
        /// NA_ORG_CT_EQ_DIST
        /// </summary>
        public double NA_ORG_CT_EQ_DIST
        {
            get { return _NA_ORG_CT_EQ_DIST; }
            set { _NA_ORG_CT_EQ_DIST = value; OnPropertyChanged("NA_ORG_CT_EQ_DIST"); }
        }

        /// <summary>
        /// NA_STABLE_TIME_S
        /// </summary>
        private double _NA_STABLE_TIME_S;
        /// <summary>
        /// NA_STABLE_TIME_S
        /// </summary>
        public double NA_STABLE_TIME_S
        {
            get { return _NA_STABLE_TIME_S; }
            set { _NA_STABLE_TIME_S = value; OnPropertyChanged("NA_STABLE_TIME_S"); }
        }

        /// <summary>
        /// NA_SCAN_TIME_S
        /// </summary>
        private double _NA_SCAN_TIME_S;
        /// <summary>
        /// NA_SCAN_TIME_S
        /// </summary>
        public double NA_SCAN_TIME_S
        {
            get { return _NA_SCAN_TIME_S; }
            set { _NA_SCAN_TIME_S = value; OnPropertyChanged("NA_SCAN_TIME_S"); }
        }

        /// <summary>
        /// SP_STABLE_TIME_S
        /// </summary>
        private double _SP_STABLE_TIME_S;
        /// <summary>
        /// SP_STABLE_TIME_S
        /// </summary>
        public double SP_STABLE_TIME_S
        {
            get { return _SP_STABLE_TIME_S; }
            set { _SP_STABLE_TIME_S = value; OnPropertyChanged("SP_STABLE_TIME_S"); }
        }

        /// <summary>
        /// TEMP_UCL
        /// </summary>
        private double _TEMP_UCL;
        /// <summary>
        /// TEMP_UCL
        /// </summary>
        public double TEMP_UCL
        {
            get { return _TEMP_UCL; }
            set { _TEMP_UCL = value; OnPropertyChanged("TEMP_UCL"); }
        }

        /// <summary>
        /// TEMP_LCL
        /// </summary>
        private double _TEMP_LCL;
        /// <summary>
        /// TEMP_LCL
        /// </summary>
        public double TEMP_LCL
        {
            get { return _TEMP_LCL; }
            set { _TEMP_LCL = value; OnPropertyChanged("TEMP_LCL"); }
        }

        /// <summary>
        /// TEMP_TIME_OUT_S
        /// </summary>
        private double _TEMP_TIME_OUT_S;
        /// <summary>
        /// TEMP_TIME_OUT_S
        /// </summary>
        public double TEMP_TIME_OUT_S
        {
            get { return _TEMP_TIME_OUT_S; }
            set { _TEMP_TIME_OUT_S = value; OnPropertyChanged("TEMP_TIME_OUT_S"); }
        }

        /// <summary>
        /// TEMP_STABLE_TIME_S
        /// </summary>
        private double _TEMP_STABLE_TIME_S;
        /// <summary>
        /// TEMP_STABLE_TIME_S
        /// </summary>
        public double TEMP_STABLE_TIME_S
        {
            get { return _TEMP_STABLE_TIME_S; }
            set { _TEMP_STABLE_TIME_S = value; OnPropertyChanged("TEMP_STABLE_TIME_S"); }
        }

        /// <summary>
        /// TEMP_ERR_UCL
        /// </summary>
        private double _TEMP_ERR_UCL;
        /// <summary>
        /// TEMP_ERR_UCL
        /// </summary>
        public double TEMP_ERR_UCL
        {
            get { return _TEMP_ERR_UCL; }
            set { _TEMP_ERR_UCL = value; OnPropertyChanged("TEMP_ERR_UCL"); }
        }

        /// <summary>
        /// TEC_CURR_UCL
        /// </summary>
        private double _TEC_CURR_UCL;
        /// <summary>
        /// TEC_CURR_UCL
        /// </summary>
        public double TEC_CURR_UCL
        {
            get { return _TEC_CURR_UCL; }
            set { _TEC_CURR_UCL = value; OnPropertyChanged("TEC_CURR_UCL"); }
        }

        /// <summary>
        /// TEC_VOLT_UCL
        /// </summary>
        private double _TEC_VOLT_UCL;
        /// <summary>
        /// TEC_VOLT_UCL
        /// </summary>
        public double TEC_VOLT_UCL
        {
            get { return _TEC_VOLT_UCL; }
            set { _TEC_VOLT_UCL = value; OnPropertyChanged("TEC_VOLT_UCL"); }
        }

        /// <summary>
        /// LASER_CURR_UCL
        /// </summary>
        private double _LASER_CURR_UCL;
        /// <summary>
        /// LASER_CURR_UCL
        /// </summary>
        public double LASER_CURR_UCL
        {
            get { return _LASER_CURR_UCL; }
            set { _LASER_CURR_UCL = value; OnPropertyChanged("LASER_CURR_UCL"); }
        }

        /// <summary>
        /// LASER_VOLT_UCL
        /// </summary>
        private double _LASER_VOLT_UCL;
        /// <summary>
        /// LASER_VOLT_UCL
        /// </summary>
        public double LASER_VOLT_UCL
        {
            get { return _LASER_VOLT_UCL; }
            set { _LASER_VOLT_UCL = value; OnPropertyChanged("LASER_VOLT_UCL"); }
        }

        /// <summary>
        /// PD_RNG
        /// </summary>
        private int _PD_RNG;
        /// <summary>
        /// PD_RNG
        /// </summary>
        public int PD_RNG
        {
            get { return _PD_RNG; }
            set { _PD_RNG = value; OnPropertyChanged("PD_RNG"); }
        }

        /// <summary>
        /// PD_RNG_LIST
        /// </summary>
        private List<int> _PD_RNG_LIST;
        /// <summary>
        /// PD_RNG_LIST
        /// </summary>
        public List<int> PD_RNG_LIST
        {
            get { return _PD_RNG_LIST; }
            set { _PD_RNG_LIST = value; OnPropertyChanged("PD_RNG_LIST"); }
        }

        /// <summary>
        /// SI_CAL
        /// </summary>
        private double _SI_CAL;
        /// <summary>
        /// SI_CAL
        /// </summary>
        public double SI_CAL
        {
            get { return _SI_CAL; }
            set { _SI_CAL = value; OnPropertyChanged("SI_CAL"); }
        }

        /// <summary>
        /// I_CAL
        /// </summary>
        private double _I_CAL;
        /// <summary>
        /// I_CAL
        /// </summary>
        public double I_CAL
        {
            get { return _I_CAL; }
            set { _I_CAL = value; OnPropertyChanged("I_CAL"); }
        }

        /// <summary>
        /// V_CAL
        /// </summary>
        private double _V_CAL;
        /// <summary>
        /// V_CAL
        /// </summary>
        public double V_CAL
        {
            get { return _V_CAL; }
            set { _V_CAL = value; OnPropertyChanged("V_CAL"); }
        }

        /// <summary>
        /// SI_OFFSET
        /// </summary>
        private double _SI_OFFSET;
        /// <summary>
        /// I_OFFSET
        /// </summary>
        public double SI_OFFSET
        {
            get { return _SI_OFFSET; }
            set { _SI_OFFSET = value; OnPropertyChanged("SI_OFFSET"); }
        }

        /// <summary>
        /// I_OFFSET
        /// </summary>
        private double _I_OFFSET;
        /// <summary>
        /// I_OFFSET
        /// </summary>
        public double I_OFFSET
        {
            get { return _I_OFFSET; }
            set { _I_OFFSET = value; OnPropertyChanged("I_OFFSET"); }
        }

        /// <summary>
        /// V_OFFSET
        /// </summary>
        private double _V_OFFSET;
        /// <summary>
        /// V_OFFSET
        /// </summary>
        public double V_OFFSET
        {
            get { return _V_OFFSET; }
            set { _V_OFFSET = value; OnPropertyChanged("V_OFFSET"); }
        }

        /// <summary>
        /// ST_CAL
        /// </summary>
        private double _ST_CAL;
        /// <summary>
        /// ST_CAL
        /// </summary>
        public double ST_CAL
        {
            get { return _ST_CAL; }
            set { _ST_CAL = value; OnPropertyChanged("ST_CAL"); }
        }

        /// <summary>
        /// ST_OFFSET
        /// </summary>
        private double _ST_OFFSET;
        /// <summary>
        /// ST_OFFSET
        /// </summary>
        public double ST_OFFSET
        {
            get { return _ST_OFFSET; }
            set { _ST_OFFSET = value; OnPropertyChanged("ST_OFFSET"); }
        }

        /// <summary>
        /// RT_CAL
        /// </summary>
        private double _RT_CAL;
        /// <summary>
        /// RT_CAL
        /// </summary>
        public double RT_CAL
        {
            get { return _RT_CAL; }
            set { _RT_CAL = value; OnPropertyChanged("RT_CAL"); }
        }

        /// <summary>
        /// RT_OFFSET
        /// </summary>
        private double _RT_OFFSET;
        /// <summary>
        /// RT_OFFSET
        /// </summary>
        public double RT_OFFSET
        {
            get { return _RT_OFFSET; }
            set { _RT_OFFSET = value; OnPropertyChanged("RT_OFFSET"); }
        }


        /// <summary>
        /// SP_NOISE
        /// </summary>
        private double _SP_NOISE;
        /// <summary>
        /// SP_NOISE
        /// </summary>
        public double SP_NOISE
        {
            get { return _SP_NOISE; }
            set { _SP_NOISE = value; OnPropertyChanged("SP_NOISE"); }
        }

        /// <summary>
        /// SP_DENOISE_ENABLE
        /// </summary>
        private bool _SP_DENOISE_ENABLE;
        /// <summary>
        /// SP_DENOISE_ENABLE
        /// </summary>
        public bool SP_DENOISE_ENABLE
        {
            get { return _SP_DENOISE_ENABLE; }
            set { _SP_DENOISE_ENABLE = value; OnPropertyChanged("SP_DENOISE_ENABLE"); }
        }

        /// <summary>
        /// TEMP_COMM_PORT
        /// </summary>
        private string _TEMP_COMM_PORT;
        /// <summary>
        /// TEMP_COMM_PORT
        /// </summary>
        public string TEMP_COMM_PORT
        {
            get { return _TEMP_COMM_PORT; }
            set { _TEMP_COMM_PORT = value; OnPropertyChanged("TEMP_COMM_PORT"); }
        }

        /// <summary>
        /// LAS_COMM_PORT
        /// </summary>
        private string _PD_COMM_PORT;
        /// <summary>
        /// LAS_COMM_PORT
        /// </summary>
        public string PD_COMM_PORT
        {
            get { return _PD_COMM_PORT; }
            set { _PD_COMM_PORT = value; OnPropertyChanged("PD_COMM_PORT"); }
        }

        /// <summary>
        /// MOTOR_COMM_PORT
        /// </summary>
        private string _MOTOR_COMM_PORT;
        /// <summary>
        /// MOTOR_COMM_PORT
        /// </summary>
        public string MOTOR_COMM_PORT
        {
            get { return _MOTOR_COMM_PORT; }
            set { _MOTOR_COMM_PORT = value; OnPropertyChanged("MOTOR_COMM_PORT"); }
        }

        /// <summary>
        /// CommAddrList
        /// </summary>
        private List<string> _CommAddrList;
        /// <summary>
        /// CommAddrList
        /// </summary>
        public List<string> CommAddrList
        {
            get { return _CommAddrList; }
            set { _CommAddrList = value; OnPropertyChanged("CommAddrList"); }
        }

        /// <summary>
        /// BEAM_USB_CAM
        /// </summary>
        private int _BEAM_USB_CAM_ID;
        /// <summary>
        /// BEAM_USB_CAM
        /// </summary>
        public int BEAM_USB_CAM_ID
        {
            get { return _BEAM_USB_CAM_ID; }
            set { _BEAM_USB_CAM_ID = value; OnPropertyChanged("BEAM_USB_CAM_ID"); }
        }

        /// <summary>
        /// CommAddrList
        /// </summary>
        private List<int> _UsbCamList;
        /// <summary>
        /// CommAddrList
        /// </summary>
        public List<int> UsbCamList
        {
            get { return _UsbCamList; }
            set { _UsbCamList = value; OnPropertyChanged("UsbCamList"); }
        }

        /// <summary>
        /// PoCalData
        /// </summary>
        private ObservableCollection<DgPoCal> _PoCalData;
        /// <summary>
        /// PoCalData
        /// </summary>
        public ObservableCollection<DgPoCal> PoCalData
        {
            get { return _PoCalData; }
            set { _PoCalData = value; OnPropertyChanged("PoCalData"); }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 运动控制卡
        /// </summary>
        private MoonsMotor MotorController;

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        internal SystemWindow(MoonsMotor motorController)
        {
            InitializeComponent();

            // Binding
            this.DataContext = this;

            //IsClosed
            this.IsClosed = false;

            // GalilController
            this.MotorController = motorController;
            if (this.MotorController != null && this.MotorController.CONNECTED)
                this.IS_MOTOR_ENABLED = true;
            else
                this.IS_MOTOR_ENABLED = false;

            // Rack
            this.RackName = StationUtil.RackName;
            this.RackId = StationUtil.RackID;

            // File Path
            StRoot root = StationUtil.GetRootData();
            this.RackFilePath = root.RackFilePath;
            this.RoutineFilePath = root.RoutineFilePath;
            this.CalibrationFilePath = root.CalibrationFilePath;
            this.UserFilePath = root.UserFilePath;
            this.TestMappingFilePath = root.TestMappingFilePath;
            this.OutputFolderPath = root.OutputFolderPath;

            // Jig & Calibration
            
            this.YSPEED = TestSystemUtil.YSPEED;
            this.YPANABG = TestSystemUtil.YPANABG;
            this.YPANAEND = TestSystemUtil.YPANAEND;
            this.NA_ORG_CT_EQ_DIST = TestSystemUtil.NA_ORG_CT_EQ_DIST;
            this.NA_EQ_DIAMETER = TestSystemUtil.NA_EQ_DIAMETER;
            this.NA_STABLE_TIME_S = TestSystemUtil.NA_STABLE_TIME_S;
            this.NA_SCAN_TIME_S = TestSystemUtil.NA_SCAN_TIME_S;
            this.SP_STABLE_TIME_S = TestSystemUtil.SP_STABLE_TIME_S;
            this.PD_RNG = TestSystemUtil.PD_RNG;
            this.PD_RNG_LIST = new List<int>() { 0, 1, 2, 3 };
            this.TEMP_UCL = TestSystemUtil.TEMP_UCL;
            this.TEMP_LCL = TestSystemUtil.TEMP_LCL;
            this.TEMP_ERR_UCL = TestSystemUtil.TEMP_ERR_UCL;
            this.TEC_CURR_UCL = TestSystemUtil.TEC_CURR_UCL;
            this.TEC_VOLT_UCL = TestSystemUtil.TEC_VOLT_UCL;
            this.TEMP_TIME_OUT_S = TestSystemUtil.TEMP_TIME_OUT_S;
            this.TEMP_STABLE_TIME_S = TestSystemUtil.TEMP_STABLE_TIME_S;
            this.LASER_CURR_UCL = TestSystemUtil.LASER_CURR_UCL;
            this.LASER_VOLT_UCL = TestSystemUtil.LASER_VOLT_UCL;
            this.SI_CAL = TestSystemUtil.SI_CAL;
            this.SI_OFFSET = TestSystemUtil.SI_OFFSET;
            this.I_CAL = TestSystemUtil.IF_CAL;
            this.I_OFFSET = TestSystemUtil.IF_OFFSET;
            this.V_CAL = TestSystemUtil.VF_CAL;
            this.V_OFFSET = TestSystemUtil.VF_OFFSET;
            this.ST_CAL = TestSystemUtil.ST_CAL;
            this.ST_OFFSET = TestSystemUtil.ST_OFFSET;
            this.RT_CAL = TestSystemUtil.RT_CAL;
            this.RT_OFFSET = TestSystemUtil.RT_OFFSET;
            this.SP_NOISE = TestSystemUtil.SP_NOISE;
            this.SP_DENOISE_ENABLE = TestSystemUtil.SP_DENOISE_ENABLE;
            // Comm Addr List
            this.CommAddrList = SerialPort.GetPortNames().ToList();
            this.TEMP_COMM_PORT = TestSystemUtil.TEMP_COMM_PORT.Replace(" ", "").ToUpper();
            this.PD_COMM_PORT = TestSystemUtil.PD_COMM_PORT.Replace(" ", "").ToUpper();
            this.MOTOR_COMM_PORT = TestSystemUtil.MOTOR_COMM_PORT.Replace(" ", "").ToUpper();
            //this.UsbCamList = new List<int>() { 0, 1, 2, 3 };
            //this.BEAM_USB_CAM_ID = TestSystemUtil.BEAM_USB_CAM_ID;
            // PoCal
            this.PoCalData = new ObservableCollection<DgPoCal>();
            int id = 1;
            if(TestSystemUtil.PO_CAL_LIST != null)
            {
                foreach (StPoCal poCal in TestSystemUtil.PO_CAL_LIST)
                {
                    this.PoCalData.Add(new DgPoCal()
                    {
                        ID = id,
                        PO_CAL = poCal.PO_CAL,
                        PO_OFFSET = poCal.PO_OFFSET,
                        WAVELENGTH_BASE = poCal.WAVELENGTH_BASE,
                        WAVELENGTH_OFFSET = poCal.PO_WLOFFSET
                    });
                    id++;
                }
            }
        }

        private void buttonSaveRackInfo_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.gridCaption.Focus();

            //检查设备名称是否为空
            if (this.RackName.Trim() == "")
            {
                WarnWindow.ShowMsg("设备名称为空,请重新输入!");
                return;
            }
            //检查设备名称是否为空
            if (this.RackId.Trim() == "")
            {
                WarnWindow.ShowMsg("设备编号为空,请重新输入!");
                return;
            }

            //保存更新
            StationUtil.SetRack(this.RackName,this.RackId);
        }

        private void buttonSaveRoot_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.gridCaption.Focus();

            //检查文件是否存在 RackFilePath
            if (!File.Exists(this.RackFilePath) && this.RackFilePath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("设备信息配置文件的路径错误,请重新输入!");
                return;
            }
            //检查文件是否存在 RoutineFilePath
            if (!File.Exists(this.RoutineFilePath) && this.RoutineFilePath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("测试程序配置文件的路径错误,请重新输入!");
                return;
            }
            //检查文件是否存在 CalibrationFilePath
            if (!File.Exists(this.CalibrationFilePath) && this.CalibrationFilePath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("校准数据配置文件的路径错误,请重新输入!");
                return;
            }
            //检查文件是否存在 UserFilePath
            if (!File.Exists(this.UserFilePath) && this.UserFilePath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("账号信息配置文件的路径错误,请重新输入!");
                return;
            }
            //检查文件是否存在 TestMappingFilePath
            if (!File.Exists(this.TestMappingFilePath) && this.TestMappingFilePath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("测试规则数据库的路径错误,请重新输入!");
                return;
            }
            //检查文件夹是否存在 OutputFolderPath
            if (!Directory.Exists(this.OutputFolderPath) && this.OutputFolderPath.Trim().ToUpper() != StationUtil.DEFAULT)
            {
                WarnWindow.ShowMsg("测试数据输出的路径错误,请重新输入!");
                return;
            }

            //保存更新
            StRoot rootInfo = new StRoot()
            {
                RackFilePath = this.RackFilePath,
                RoutineFilePath = this.RoutineFilePath,
                CalibrationFilePath = this.CalibrationFilePath,
                UserFilePath = this.UserFilePath,
                OutputFolderPath = this.OutputFolderPath,
                TestMappingFilePath = this.TestMappingFilePath
            };
            StationUtil.SetRoot(rootInfo);
        }

        private void buttonOpenRackFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "CONFIG.RACK"; // Default file name
            dialog.DefaultExt = ".ini"; // Default file extension
            dialog.Filter = "ini (.ini)|*.ini"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.RackFilePath = dialog.FileName;
            }
        }

        private void buttonOpenScriptFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "CONFIG.ROUTINE"; // Default file name
            dialog.DefaultExt = ".ini"; // Default file extension
            dialog.Filter = "ini (.ini)|*.ini"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.RoutineFilePath = dialog.FileName;
            }
        }

        private void buttonOpenCalibrationFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "CONFIG.CALIBRATION"; // Default file name
            dialog.DefaultExt = ".ini"; // Default file extension
            dialog.Filter = "config (.ini)|*.ini"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.CalibrationFilePath = dialog.FileName;
            }
        }

        private void buttonOpenUserFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "CONFIG.USER"; // Default file name
            dialog.DefaultExt = ".ini"; // Default file extension
            dialog.Filter = "config (.ini)|*.ini"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.CalibrationFilePath = dialog.FileName;
            }
        }

        private void buttonOpenOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            var dlgInitialVideosSearchDirectory = new Microsoft.Win32.Dialogs.OpenFolderDialog();
            dlgInitialVideosSearchDirectory.Description = "请选择存放测试数据的文件夹";
            dlgInitialVideosSearchDirectory.RootFolder = Environment.SpecialFolder.MyComputer;
            dlgInitialVideosSearchDirectory.SelectedPath = "Data";
            dlgInitialVideosSearchDirectory.ShowNewFolderButton = true;
            if (dlgInitialVideosSearchDirectory.ShowDialog() == true)
            {
                this.OutputFolderPath = dlgInitialVideosSearchDirectory.SelectedPath;
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

        private void buttonDefault_Click(object sender, RoutedEventArgs e)
        {
            this.RackFilePath = StationUtil.DEFAULT;
            this.RoutineFilePath = StationUtil.DEFAULT;
            this.CalibrationFilePath = StationUtil.DEFAULT;
            this.UserFilePath = StationUtil.DEFAULT;
            this.TestMappingFilePath = StationUtil.DEFAULT;
            this.OutputFolderPath = StationUtil.DEFAULT;
        }

        private void buttonOpenTestMappingFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "CONFIG.TEST"; // Default file name
            dialog.DefaultExt = ".mdb"; // Default file extension
            dialog.Filter = "mdb (.mdb)|*.mdb"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.TestMappingFilePath = dialog.FileName;
            }
        }

        private void buttonSaveJigParam_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.gridCaption.Focus();

            // 检查速度
            if (this.YSPEED <= 0 || this.YSPEED > 5)
            {
                WarnWindow.ShowMsg("Y轴马达速度只能介于0到5mm/s!");
                return;
            }
            if (this.TEMP_UCL <= 0 || this.TEMP_UCL > 125)
            {
                WarnWindow.ShowMsg("温度上限只能介于0到125℃!");
                return;
            }
            if (this.TEMP_LCL <= 0 || this.TEMP_LCL > 125)
            {
                WarnWindow.ShowMsg("温度下限只能介于0到125℃!");
                return;
            }
            if (this.TEMP_LCL >= this.TEMP_UCL)
            {
                WarnWindow.ShowMsg("温度下限必须小于温度上限!");
                return;
            }
            if (this.NA_ORG_CT_EQ_DIST <= 0 || this.NA_ORG_CT_EQ_DIST > 100)
            {
                WarnWindow.ShowMsg("遮光板与插芯距离只能介于0到100mm!");
                return;
            }
            if (this.NA_EQ_DIAMETER <= 0 || this.NA_EQ_DIAMETER > 500)
            {
                WarnWindow.ShowMsg("遮光板与插芯距离只能介于0到500mm!");
                return;
            }
            if (this.YPANABG >= this.YPANAEND)
            {
                WarnWindow.ShowMsg("N.A.起始扫描位置必须小于N.A.结束扫描位置!");
                return;
            }
            if (this.SP_STABLE_TIME_S < 0)
            {
                WarnWindow.ShowMsg("光谱加电稳定时间不能小于0s!");
                return;
            }
            if (this.NA_STABLE_TIME_S < 0)
            {
                WarnWindow.ShowMsg("N.A.加电稳定时间不能小于0s!");
                return;
            }
            if (this.NA_SCAN_TIME_S < 5 || this.NA_SCAN_TIME_S > 300)
            {
                WarnWindow.ShowMsg("N.A.扫描行程时间只能介于5到300s!");
                return;
            }
            if (this.TEMP_STABLE_TIME_S < 10)
            {
                WarnWindow.ShowMsg("温控稳定时间不能小于10s!");
                return;
            }
            if (this.TEMP_TIME_OUT_S < 300)
            {
                WarnWindow.ShowMsg("温控超时时间不能小于300s!");
                return;
            }
            if (this.TEMP_TIME_OUT_S <= this.TEMP_STABLE_TIME_S)
            {
                WarnWindow.ShowMsg("温控超时时间必须大于温控超时时间!");
                return;
            }
            if (this.TEMP_ERR_UCL < 0.2)
            {
                WarnWindow.ShowMsg("最大温控误差不能小于0.2℃!");
                return;
            }
            if (this.TEC_CURR_UCL < 0.0)
            {
                WarnWindow.ShowMsg("TEC电流上限不能小于0.0A!");
                return;
            }
            if (this.TEC_CURR_UCL > TestSystemUtil.MaxTecCurrentA)
            {
                WarnWindow.ShowMsg("TEC电流上限不能大于" + TestSystemUtil.MaxTecCurrentA +"A!");
                return;
            }
            if (this.TEC_VOLT_UCL < 0)
            {
                WarnWindow.ShowMsg("TEC电压上限不能小于0.0V");
                return;
            }
            if (this.TEC_VOLT_UCL > TestSystemUtil.MaxTecVoltageV)
            {
                WarnWindow.ShowMsg("TEC电压上限不能大于" + TestSystemUtil.MaxTecVoltageV + "V!");
                return;
            }
            if (this.TEMP_ERR_UCL < 0.2)
            {
                WarnWindow.ShowMsg("最大温控误差不能小于0.2℃!");
                return;
            }

            if (this.LASER_CURR_UCL < 0.0)
            {
                WarnWindow.ShowMsg("激光器电流上限不能小于0.0A!");
                return;
            }
            if (this.LASER_CURR_UCL > TestSystemUtil.MaxDriveCurrentA)
            {
                WarnWindow.ShowMsg("激光器电流上限不能大于" + TestSystemUtil.MaxDriveCurrentA + "A!");
                return;
            }
            if (this.LASER_VOLT_UCL < 0)
            {
                WarnWindow.ShowMsg("激光器电压上限不能小于0.0V");
                return;
            }
            if (this.LASER_VOLT_UCL > TestSystemUtil.MaxDriveVoltageV)
            {
                WarnWindow.ShowMsg("激光器电压上限不能大于" + TestSystemUtil.MaxDriveVoltageV + "V!");
                return;
            }

            // Jig
            TestSystemUtil.YSPEED = this.YSPEED;
            TestSystemUtil.YPANABG = this.YPANABG;
            TestSystemUtil.YPANAEND = this.YPANAEND;
            TestSystemUtil.NA_STABLE_TIME_S = this.NA_STABLE_TIME_S;
            TestSystemUtil.NA_SCAN_TIME_S = this.NA_SCAN_TIME_S;
            TestSystemUtil.SP_STABLE_TIME_S = this.SP_STABLE_TIME_S;
            TestSystemUtil.NA_ORG_CT_EQ_DIST = this.NA_ORG_CT_EQ_DIST;
            TestSystemUtil.NA_EQ_DIAMETER = this.NA_EQ_DIAMETER;
            TestSystemUtil.PD_RNG = this.PD_RNG;
            TestSystemUtil.TEMP_ERR_UCL = this.TEMP_ERR_UCL;
            TestSystemUtil.TEMP_UCL = this.TEMP_UCL;
            TestSystemUtil.TEMP_LCL = this.TEMP_LCL;
            TestSystemUtil.TEMP_TIME_OUT_S = this.TEMP_TIME_OUT_S;
            TestSystemUtil.TEMP_STABLE_TIME_S = this.TEMP_STABLE_TIME_S;
            TestSystemUtil.TEC_CURR_UCL = this.TEC_CURR_UCL;
            TestSystemUtil.TEC_VOLT_UCL = this.TEC_VOLT_UCL;
            TestSystemUtil.LASER_CURR_UCL = this.LASER_CURR_UCL;
            TestSystemUtil.LASER_VOLT_UCL = this.LASER_VOLT_UCL;

            TestSystemUtil.SaveConfig();
        }

        private void btnSetX1ST_Click(object sender, RoutedEventArgs e)
        {
            //if(this.GalilController != null && this.GalilController.CONNECTED)
            //{
            //    this.X1ST = this.GalilController.GetXmm();
            //}
        }

        private void btnSetPARK_Click(object sender, RoutedEventArgs e)
        {
            //if (this.GalilController != null && this.GalilController.CONNECTED)
            //{
            //    this.XPARK = this.GalilController.GetXmm();
            //}
        }


        private void btnSetYPANABG_Click(object sender, RoutedEventArgs e)
        {
            if(this.YPANABG >= this.YPANAEND)
            {
                WarnWindow.ShowMsg("马达速度只能介于5到120mm/s!");
                return;
            }
            
            if (this.MotorController != null && this.MotorController.CONNECTED)
            {
                this.YPANABG = this.MotorController.GetRealAbsPosition();
            }
        }

        private void btnSetYPANAEND_Click(object sender, RoutedEventArgs e)
        {
            {
                if (this.MotorController != null && this.MotorController.CONNECTED)
                {
                    //this.YPANAEND = this.GalilController.GetZmm();
                    this.YPANAEND = this.MotorController.GetRealAbsPosition();
                }
            }
        }

        private void buttonSaveCalibrationParam_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.gridCaption.Focus();

            // Calibration
            TestSystemUtil.SI_CAL = this.SI_CAL;
            TestSystemUtil.SI_OFFSET = this.SI_OFFSET;
            TestSystemUtil.IF_CAL = this.I_CAL;
            TestSystemUtil.IF_OFFSET = this.I_OFFSET;
            TestSystemUtil.VF_CAL = this.V_CAL;
            TestSystemUtil.VF_OFFSET = this.V_OFFSET;
            TestSystemUtil.ST_CAL = this.ST_CAL;
            TestSystemUtil.ST_OFFSET = this.ST_OFFSET;
            TestSystemUtil.RT_CAL = this.RT_CAL;
            TestSystemUtil.RT_OFFSET = this.RT_OFFSET;
            TestSystemUtil.SP_NOISE = this.SP_NOISE;
            TestSystemUtil.SP_DENOISE_ENABLE = this.SP_DENOISE_ENABLE;

            for (int i=0;i<this.PoCalData.Count;i++)
            {
                for(int j=0;j< TestSystemUtil.PO_CAL_LIST.Count;j++)
                {
                    if(this.PoCalData[i].WAVELENGTH_BASE == TestSystemUtil.PO_CAL_LIST[j].WAVELENGTH_BASE)
                    {
                        StPoCal poCal = TestSystemUtil.PO_CAL_LIST[j];
                        poCal.PO_CAL = this.PoCalData[i].PO_CAL;
                        poCal.PO_OFFSET = this.PoCalData[i].PO_OFFSET;
                        poCal.PO_WLOFFSET = this.PoCalData[i].WAVELENGTH_OFFSET;
                        TestSystemUtil.PO_CAL_LIST[j] = poCal;
                    }
                }
            }
            TestSystemUtil.SaveConfig();
        }

        private void buttonSaveCommAddr_Click(object sender, RoutedEventArgs e)
        {
            TestSystemUtil.TEMP_COMM_PORT = this.TEMP_COMM_PORT;
            TestSystemUtil.PD_COMM_PORT = this.PD_COMM_PORT;
            TestSystemUtil.MOTOR_COMM_PORT = this.MOTOR_COMM_PORT;
            //TestSystemUtil.BEAM_USB_CAM_ID = this.BEAM_USB_CAM_ID;
            TestSystemUtil.SaveConfig();
        }
    }

    public class DgPoCal
    {
        public int ID { get; set; }

        public double WAVELENGTH_BASE { get; set; }

        public double PO_CAL { get; set; }

        public double PO_OFFSET { get; set; }

        public double WAVELENGTH_OFFSET { get; set; }
    }
}
