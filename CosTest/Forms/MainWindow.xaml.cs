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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using System.Data;
using System.Data.OleDb;
using Wpf.CartesianChart.CustomTooltip;
using System.Threading;
using System.Drawing.Imaging;
using QRCoder;

//using System.Data.OleDb;

namespace CosTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// 运动控制
        /// </summary>
        private MoonsMotor MotorDriver;
        /// <summary>
        /// Laser Driver
        /// </summary>
        private ItDriver LaserDriver;
        /// <summary>
        /// OSA
        /// </summary>
        private OcOsa OSA;
        /// <summary>
        /// Temperature Driver
        /// </summary>
        private MeerstetterTECController TempDriver;
        /// <summary>
        /// Power Driver
        /// </summary>
        private PdDriver PowerDriver;
        /// <summary>
        /// Beam Camera Driver
        /// </summary>
        private BeamCam BeamCamDriver;
        /// <summary>
        /// Thermal Camera Driver
        /// </summary>
        private ThermCam ThermCamDriver;
        /// <summary>
        /// 定时器（非UI线程）
        /// </summary>
        private System.Timers.Timer TickTimer;
        /// <summary>
        /// 测试过程
        /// </summary>
        private TestProc TestProcInst;
        /// <summary>
        /// 调试过程
        /// </summary>
        private ToolProc ToolProcInst;

        /// <summary>
        /// 启动界面
        /// </summary>
        private SplashWindow SplashForm;

        /// <summary>
        /// 是否清空数据
        /// </summary>
        private bool IsDataCleared;

        /// <summary>
        /// 系统设置界面
        /// </summary>
        private SystemWindow FrmSystem;
        /// <summary>
        /// 工程调试界面
        /// </summary>
        private ToolWindow FrmTool;
        /// <summary>
        /// 用户配置界面
        /// </summary>
        private UserWindow FrmUser;
        /// <summary>
        /// 程序配置界面
        /// </summary>
        private TestScriptWindow FrmTestRoutine;
        /// <summary>
        /// 热成像参数配置界面
        /// </summary>
        private ThermMeasureWindow FrmThermMeasConfig;
        /// <summary>
        /// 关于界面
        /// </summary>
        private AboutWindow FrmAbout;
        /// <summary>
        /// 用户登陆窗口
        /// </summary>
        private UserLogOnWindow FrmUserLogOn;
        /// <summary>
        /// 用户退出窗口
        /// </summary>
        private UserLogOutWindow FrmUserLogoff;
        /// <summary>
        /// 报表窗口
        /// </summary>
        private ReportWindow FrmReport;
        /// <summary>
        /// 之前的SerialNo
        /// </summary>
        private string PrevSerialNo;

        /// <summary>
        /// 定时器（非UI线程）
        /// </summary>
        private System.Timers.Timer ReportPrintTimer;
        /// <summary>
        /// 定时器（非UI线程）
        /// </summary>
        private System.Timers.Timer ReportShowTimer;
        /// <summary>
        /// ReportCosTestData
        /// </summary>
        private StCosTestData ReportCosTestData;
        /// <summary>
        /// ReportDstFolder
        /// </summary>
        private string ReportDstFolder;
        /// <summary>
        /// ReportDelayCount
        /// </summary>
        private int ReportDelayCount;
        /// <summary>
        /// ShowCosPositionDisabled
        /// </summary>
        private bool ShowCosPositionDisabled;


        #region  Binding
        /// <summary>
        /// MAX_NORMAL图标
        /// </summary>
        private bool max_noraml_icon;
        /// <summary>
        /// MAX_NORMAL图标
        /// </summary>
        public bool MAX_NORMAL_ICON
        {
            get { return max_noraml_icon; }
            set { max_noraml_icon = value; OnPropertyChanged("MAX_NORMAL_ICON"); }
        }
        /// <summary>
        /// MAX_NORMAL图标提示
        /// </summary>
        private string max_normal_tips;
        /// <summary>
        /// MAX_NORMAL图标提示
        /// </summary>
        public string MAX_NORMAL_TIPS
        {
            get { return max_normal_tips; }
            set { max_normal_tips = value; OnPropertyChanged("MAX_NORMAL_TIPS"); }
        }

        /// <summary>
        /// LOGIN_TIPS
        /// </summary>
        private string _LOGIN_TIPS;
        /// LOGIN_TIPS
        /// BatchNo
        /// </summary>
        public string LOGIN_TIPS
        {
            get { return _LOGIN_TIPS; }
            set { _LOGIN_TIPS = value; OnPropertyChanged("LOGIN_TIPS"); }
        }

        /// <summary>
        /// LOGIN_ICON
        /// </summary>
        private string _LOGIN_ICON;
        /// <summary>
        /// LOGIN_ICON
        /// </summary>
        public string LOGIN_ICON
        {
            get { return _LOGIN_ICON; }
            set { _LOGIN_ICON = value; OnPropertyChanged("LOGIN_ICON"); }
        }

        /// <summary>
        /// BatchNoEditEnabled
        /// </summary>
        private bool _BatchNoEditEnabled;
        /// <summary>
        /// BatchNoEditEnabled
        /// </summary>
        public bool BatchNoEditEnabled
        {
            get { return _BatchNoEditEnabled; }
            set { _BatchNoEditEnabled = value; OnPropertyChanged("BatchNoEditEnabled"); }
        }

        /// <summary>
        /// BatchNo
        /// </summary>
        private string _BatchNo;
        /// <summary>
        /// BatchNo
        /// </summary>
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; OnPropertyChanged("BatchNo"); }
        }

        /// <summary>
        /// SerialNo
        /// </summary>
        private string _SerialNo;
        /// <summary>
        /// SerialNo
        /// </summary>
        public string SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; OnPropertyChanged("SerialNo"); }
        }

        /// <summary>
        /// PartNo
        /// </summary>
        private string _PartNo;
        /// <summary>
        /// PartNo
        /// </summary>
        public string PartNo
        {
            get { return _PartNo; }
            set { _PartNo = value; OnPropertyChanged("PartNo"); }
        }

        /// <summary>
        /// TestStage
        /// </summary>
        private string _TestStage;
        /// <summary>
        /// TestStage
        /// </summary>
        public string TestStage
        {
            get { return _TestStage; }
            set { _TestStage = value; OnPropertyChanged("TestStage"); }
        }

        /// <summary>
        /// StageList
        /// </summary>
        private List<string> _StageList;
        /// <summary>
        /// StageList
        /// </summary>
        public List<string> StageList
        {
            get { return _StageList; }
            set { _StageList = value; OnPropertyChanged("StageList"); }
        }

        /// <summary>
        /// SelRoutineName
        /// </summary>
        private string _SelScriptName;
        /// <summary>
        /// SelRoutineName
        /// </summary>
        public string SelScriptName
        {
            get { return _SelScriptName; }
            set { _SelScriptName = value; OnPropertyChanged("SelScriptName"); }
        }

        /// <summary>
        /// ScriptNameList
        /// </summary>
        private List<string> _ScriptNameList;
        /// <summary>
        /// ScriptNameList
        /// </summary>
        public List<string> ScriptNameList
        {
            get { return _ScriptNameList; }
            set { _ScriptNameList = value; OnPropertyChanged("ScriptNameList"); }
        }

        /// <summary>
        /// 光谱Series
        /// </summary>
        private SeriesCollection spectrumSeries;
        /// <summary>
        /// 光谱Series
        /// </summary>
        public SeriesCollection SpectrumSeries
        {
            get { return spectrumSeries; }
            set { spectrumSeries = value; OnPropertyChanged("SpectrumSeries"); }
        }

        /// <summary>
        /// LIV Series
        /// </summary>
        private SeriesCollection livSeries;
        /// <summary>
        /// LIV Series
        /// </summary>
        public SeriesCollection LivSeries
        {
            get { return livSeries; }
            set { livSeries = value; OnPropertyChanged("LivSeries"); }
        }

        /// <summary>
        /// LivAxesY
        /// </summary>
        private AxesCollection livAxesY;
        /// <summary>
        /// LivAxesY
        /// </summary>
        public AxesCollection LivAxesY
        {
            get { return livAxesY; }
            set { livAxesY = value; OnPropertyChanged("LivAxesY"); }
        }

        /// <summary>
        /// BatchLoadEnabled
        /// </summary>
        private bool _BatchLoadEnabled;
        /// <summary>
        /// BatchLoadEnabled
        /// </summary>
        public bool BatchLoadEnabled
        {
            get { return _BatchLoadEnabled; }
            set { _BatchLoadEnabled = value; OnPropertyChanged("BatchLoadEnabled"); }
        }

        /// <summary>
        /// BtnStartEnabled
        /// </summary>
        private bool _BtnStartEnabled;
        /// <summary>
        /// BtnStartEnabled
        /// </summary>
        public bool BtnStartEnabled
        {
            get { return _BtnStartEnabled; }
            set { _BtnStartEnabled = value; OnPropertyChanged("BtnStartEnabled"); }
        }

        /// <summary>
        /// BtnStopEnabled
        /// </summary>
        private bool _BtnStopEnabled;
        /// <summary>
        /// BtnStopEnabled
        /// </summary>
        public bool BtnStopEnabled
        {
            get { return _BtnStopEnabled; }
            set { _BtnStopEnabled = value; OnPropertyChanged("BtnStopEnabled"); }
        }

        /// <summary>
        /// BtnPauseEnabled
        /// </summary>
        private bool _BtnPauseEnabled;
        /// <summary>
        /// BtnPauseEnabled
        /// </summary>
        public bool BtnPauseEnabled
        {
            get { return _BtnPauseEnabled; }
            set { _BtnPauseEnabled = value; OnPropertyChanged("BtnPauseEnabled"); }
        }

        /// <summary>
        /// BtnResumeEnabled
        /// </summary>
        private bool _BtnResumeEnabled;
        /// <summary>
        /// BtnResumeEnabled
        /// </summary>
        public bool BtnResumeEnabled
        {
            get { return _BtnResumeEnabled; }
            set { _BtnResumeEnabled = value; OnPropertyChanged("BtnResumeEnabled"); }
        }

        /// <summary>
        /// SpChartWavelengthLCL
        /// </summary>
        private double _SpChartWavelengthLCL;
        /// <summary>
        /// SpChartWavelengthLCL
        /// </summary>
        public double SpChartWavelengthLCL
        {
            get { return _SpChartWavelengthLCL; }
            set { _SpChartWavelengthLCL = value; OnPropertyChanged("SpChartWavelengthLCL"); }
        }

        /// <summary>
        /// SpChartWavelengthUCL
        /// </summary>
        private double _SpChartWavelengthUCL;
        /// <summary>
        /// SpChartWavelengthUCL
        /// </summary>
        public double SpChartWavelengthUCL
        {
            get { return _SpChartWavelengthUCL; }
            set { _SpChartWavelengthUCL = value; OnPropertyChanged("SpChartWavelengthUCL"); }
        }

        /// <summary>
        /// SpChartWavelengthStep
        /// </summary>
        private double _SpChartWavelengthStep;
        /// <summary>
        /// SpChartWavelengthStep
        /// </summary>
        public double SpChartWavelengthStep
        {
            get { return _SpChartWavelengthStep; }
            set { _SpChartWavelengthStep = value; OnPropertyChanged("SpChartWavelengthStep"); }
        }

        /// <summary>
        /// SpChartSpectrumUCL
        /// </summary>
        private double _SpChartSpectrumUCL;
        /// <summary>
        /// SpChartSpectrumUCL
        /// </summary>
        public double SpChartSpectrumUCL
        {
            get { return _SpChartSpectrumUCL; }
            set { _SpChartSpectrumUCL = value; OnPropertyChanged("SpChartSpectrumUCL"); }
        }

        /// <summary>
        /// SpChartSpectrumStep
        /// </summary>
        private double _SpChartSpectrumStep;
        /// <summary>
        /// SpChartSpectrumStep
        /// </summary>
        public double SpChartSpectrumStep
        {
            get { return _SpChartSpectrumStep; }
            set { _SpChartSpectrumStep = value; OnPropertyChanged("SpChartSpectrumStep"); }
        }

        /// <summary>
        /// LivChartCurrentStop
        /// </summary>
        private double _LivChartCurrentStop;
        /// <summary>
        /// LivChartCurrentStop
        /// </summary>
        public double LivChartCurrentStop
        {
            get { return _LivChartCurrentStop; }
            set { _LivChartCurrentStop = value; OnPropertyChanged("LivChartCurrentStop"); }
        }

        /// <summary>
        /// LivChartCurrentStep
        /// </summary>
        private double _LivChartCurrentStep;
        /// <summary>
        /// LivChartCurrentStep
        /// </summary>
        public double LivChartCurrentStep
        {
            get { return _LivChartCurrentStep; }
            set { _LivChartCurrentStep = value; OnPropertyChanged("LivChartCurrentStep"); }
        }

        /// <summary>
        /// LivChartPowerUCL
        /// </summary>
        private double _LivChartPowerUCL;
        /// <summary>
        /// LivChartPowerUCL
        /// </summary>
        public double LivChartPowerUCL
        {
            get { return _LivChartPowerUCL; }
            set { _LivChartPowerUCL = value; OnPropertyChanged("LivChartPowerUCL"); }
        }

        /// <summary>
        /// LivChartPowerStep
        /// </summary>
        private double _LivChartPowerStep;
        /// <summary>
        /// LivChartPowerStep
        /// </summary>
        public double LivChartPowerStep
        {
            get { return _LivChartPowerStep; }
            set { _LivChartPowerStep = value; OnPropertyChanged("LivChartPowerStep"); }
        }

        /// <summary>
        /// LivChartVoltageUCL
        /// </summary>
        private double _LivChartVoltageUCL;
        /// <summary>
        /// LivChartVoltageUCL
        /// </summary>
        public double LivChartVoltageUCL
        {
            get { return _LivChartVoltageUCL; }
            set { _LivChartVoltageUCL = value; OnPropertyChanged("LivChartVoltageUCL"); }
        }

        /// <summary>
        /// LivChartVoltageStep
        /// </summary>
        private double _LivChartVoltageStep;
        /// <summary>
        /// LivChartVoltageStep
        /// </summary>
        public double LivChartVoltageStep
        {
            get { return _LivChartVoltageStep; }
            set { _LivChartVoltageStep = value; OnPropertyChanged("LivChartVoltageStep"); }
        }

        /// <summary>
        /// TMP
        /// </summary>
        private double _TMP;
        /// <summary>
        /// TMP
        /// </summary>
        public double TMP
        {
            get { return _TMP; }
            set { _TMP = value; OnPropertyChanged("TMP"); }
        }

        /// <summary>
        /// TSET
        /// </summary>
        private double _TSET;
        /// <summary>
        /// TSET
        /// </summary>
        public double TSET
        {
            get { return _TSET; }
            set { _TSET = value; OnPropertyChanged("TSET"); }
        }

        /// <summary>
        /// HOME
        /// </summary>
        private bool _HOME;
        /// <summary>
        /// HOME
        /// </summary>
        public bool HOME
        {
            get { return _HOME; }
            set { _HOME = value; OnPropertyChanged("HOME"); }
        }

        /// <summary>
        /// MIRROR
        /// </summary>
        private bool _MIRROR;
        /// <summary>
        /// MIRROR
        /// </summary>
        public bool MIRROR
        {
            get { return _MIRROR; }
            set { _MIRROR = value; OnPropertyChanged("MIRROR"); }
        }

        /// <summary>
        /// POSY
        /// </summary>
        private double _POSY;
        /// <summary>
        /// POSY
        /// </summary>
        public double POSY
        {
            get { return _POSY; }
            set { _POSY = value; OnPropertyChanged("POSY"); }
        }

        /// <summary>
        /// Formatter
        /// </summary>
        private Func<double, string> _Formatter;
        /// <summary>
        /// Formatter
        /// </summary>
        public Func<double, string> Formatter
        {
            get { return _Formatter; }
            set { _Formatter = value; OnPropertyChanged("Formatter"); }
        }

        /// <summary>
        /// LivToolTip
        /// </summary>
        private CustomTooltip _LivToolTip;
        /// <summary>
        /// LivToolTip
        /// </summary>
        public CustomTooltip LivToolTip
        {
            get { return _LivToolTip; }
            set { _LivToolTip = value; OnPropertyChanged("LivToolTip"); }
        }

        /// <summary>
        /// SpToolTip
        /// </summary>
        private CustomTooltip _SpToolTip;
        /// <summary>
        /// LivToolTip
        /// </summary>
        public CustomTooltip SpToolTip
        {
            get { return _SpToolTip; }
            set { _SpToolTip = value; OnPropertyChanged("SpToolTip"); }
        }

        /// <summary>
        /// CosTestResultData 列表显示结果
        /// </summary>
        private ObservableCollection<DgTestResult> _CosTestResultData;
        /// <summary>
        /// LivTestLimitData 列表显示结果
        /// </summary>
        public ObservableCollection<DgTestResult> CosTestResultData
        {
            get { return _CosTestResultData; }
            set { _CosTestResultData = value; OnPropertyChanged("CosTestResultData"); }
        }

        /// <summary>
        /// CurrTestStatus
        /// </summary>
        private string _CurrTestStatus;
        /// <summary>
        /// CurrTestStatus
        /// </summary>
        public string CurrTestStatus
        {
            get { return _CurrTestStatus; }
            set { _CurrTestStatus = value; OnPropertyChanged("CurrTestStatus"); }
        }

        /// <summary>
        /// CurrTestSpotFill
        /// </summary>
        private string _CurrTestSpotFill;
        /// <summary>
        /// CurrTestSpotFill
        /// </summary>
        public string CurrTestSpotFill
        {
            get { return _CurrTestSpotFill; }
            set { _CurrTestSpotFill = value; OnPropertyChanged("CurrTestSpotFill"); }
        }

        /// <summary>
        /// CurrTestResult
        /// </summary>
        private string _CurrTestResult;
        /// <summary>
        /// CurrTestResult
        /// </summary>
        public string CurrTestResult
        {
            get { return _CurrTestResult; }
            set { _CurrTestResult = value; OnPropertyChanged("CurrTestResult"); }
        }

        /// <summary>
        /// CurrTestResultColor
        /// </summary>
        private string _CurrTestResultColor;
        /// <summary>
        /// CurrTestResultColor
        /// </summary>
        public string CurrTestResultColor
        {
            get { return _CurrTestResultColor; }
            set { _CurrTestResultColor = value; OnPropertyChanged("CurrTestResultColor"); }
        }

        /// <summary>
        /// CurrTestSubAction
        /// </summary>
        private string _CurrTestSubAction;
        /// <summary>
        /// CurrTestSubAction
        /// </summary>
        public string CurrTestSubAction
        {
            get { return _CurrTestSubAction; }
            set { _CurrTestSubAction = value; OnPropertyChanged("CurrTestSubAction"); }
        }

        /// <summary>
        /// TestDataHeaderText
        /// </summary>
        private string _TestDataHeaderText;
        /// <summary>
        /// TestDataHeaderText
        /// </summary>
        public string TestDataHeaderText
        {
            get { return _TestDataHeaderText; }
            set { _TestDataHeaderText = value; OnPropertyChanged("TestDataHeaderText"); }
        }

        /// <summary>
        /// SysEditEnabled
        /// </summary>
        private bool _SysEditEnabled;
        /// <summary>
        /// SysEditEnabled
        /// </summary>
        public bool SysEditEnabled
        {
            get { return _SysEditEnabled; }
            set { _SysEditEnabled = value; OnPropertyChanged("SysEditEnabled"); }
        }

        /// <summary>
        /// LockImage
        /// </summary>
        private string _LockImage;
        /// <summary>
        /// LockImage
        /// </summary>
        public string LockImage
        {
            get { return _LockImage; }
            set { _LockImage = value; OnPropertyChanged("LockImage"); }
        }

        /// <summary>
        /// LockStatus
        /// </summary>
        private string _LockStatus;
        /// <summary>
        /// LockStatus
        /// </summary>
        public string LockStatus
        {
            get { return _LockStatus; }
            set { _LockStatus = value; OnPropertyChanged("LockStatus"); }
        }

        /// <summary>
        /// EquipPartNo
        /// </summary>
        private string _EquipPartNo;
        /// <summary>
        /// EquipPartNo
        /// </summary>
        public string EquipPartNo
        {
            get { return _EquipPartNo; }
            set { _EquipPartNo = value; OnPropertyChanged("EquipPartNo"); }
        }

        /// <summary>
        /// SoftwareVersion
        /// </summary>
        private string _SoftwareVersion;
        /// <summary>
        /// SoftwareVersion
        /// </summary>
        public string SoftwareVersion
        {
            get { return _SoftwareVersion; }
            set { _SoftwareVersion = value; OnPropertyChanged("SoftwareVersion"); }
        }

        /// <summary>
        /// BtnClearEnabled
        /// </summary>
        private bool _BtnClearEnabled;
        /// <summary>
        /// BtnClearEnabled
        /// </summary>
        public bool BtnClearEnabled
        {
            get { return _BtnClearEnabled; }
            set { _BtnClearEnabled = value; OnPropertyChanged("BtnClearEnabled"); }
        }

        /// <summary>
        /// BtnOpenResultEnabled
        /// </summary>
        private bool _BtnOpenResultEnabled;
        /// <summary>
        /// BtnOpenResultEnabled
        /// </summary>
        public bool BtnOpenResultEnabled
        {
            get { return _BtnOpenResultEnabled; }
            set { _BtnOpenResultEnabled = value; OnPropertyChanged("BtnOpenResultEnabled"); }
        }

        /// <summary>
        /// BtnLoadResultEnabled
        /// </summary>
        private bool _BtnLoadResultEnabled;
        /// <summary>
        /// BtnLoadResultEnabled
        /// </summary>
        public bool BtnLoadResultEnabled
        {
            get { return _BtnLoadResultEnabled; }
            set { _BtnLoadResultEnabled = value; OnPropertyChanged("BtnLoadResultEnabled"); }
        }

        /// <summary>
        /// BeamImage
        /// </summary>
        private BitmapImage _BeamImage;
        /// <summary>
        /// BeamImage
        /// </summary>
        public BitmapImage BeamImage
        {
            get { return _BeamImage; }
            set { _BeamImage = value; OnPropertyChanged("BeamImage"); }
        }

        /// <summary>
        /// BeamImageVisibility
        /// </summary>
        private string _BeamImageVisibility;
        /// <summary>
        /// BeamImageVisibility
        /// </summary>
        public string BeamImageVisibility
        {
            get { return _BeamImageVisibility; }
            set { _BeamImageVisibility = value; OnPropertyChanged("BeamImageVisibility"); }
        }

        /// <summary>
        /// ThermalImage
        /// </summary>
        private BitmapImage _ThermalImage;
        /// <summary>
        /// ThermalImage
        /// </summary>
        public BitmapImage ThermalImage
        {
            get { return _ThermalImage; }
            set { _ThermalImage = value; OnPropertyChanged("ThermalImage"); }
        }

        /// <summary>
        /// ThermalImageVisibility
        /// </summary>
        private string _ThermalImageVisibility;
        /// <summary>
        /// ThermalImageVisibility
        /// </summary>
        public string ThermalImageVisibility
        {
            get { return _ThermalImageVisibility; }
            set { _ThermalImageVisibility = value; OnPropertyChanged("ThermalImageVisibility"); }
        }

        /// <summary>
        /// 热成像可视性
        /// </summary>
        private string _ThermConfigVisibility;
        /// <summary>
        /// 热成像可视性
        /// </summary>
        public string ThermConfigVisibility
        {
            get { return _ThermConfigVisibility; }
            set { _ThermConfigVisibility = value; OnPropertyChanged("ThermConfigVisibility"); }
        }

        /// <summary>
        /// ChartVisibility
        /// </summary>
        private string _ChartVisibility;
        /// <summary>
        /// ChartVisibility
        /// </summary>
        public string ChartVisibility
        {
            get { return _ChartVisibility; }
            set { _ChartVisibility = value; OnPropertyChanged("ChartVisibility"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MainWindow()
        {
            //检查是否正在运行另外一个线程
            if (ProcessCheck.IsRunning())
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }


            //打开启动界面
            this.SplashForm = new SplashWindow();
            this.SplashForm.Show();

            InitializeComponent();

            // Binding
            this.DataContext = this;
            // Window
            this.MAX_NORMAL_ICON = true;
            this.MAX_NORMAL_TIPS = "最大化";
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            // Login
            this.LOGIN_ICON = @"/Resources/Logout.png";
            this.LOGIN_TIPS = "未登陆";

            //加载文件
            StationUtil.ReadConfig();
            if (!StationUtil.LOADED)
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }
            TestScriptUtil.ReadConfig();
            if (!TestScriptUtil.LOADED)
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }
            ThermMeasUtil.ReadConfig();
            if (!ThermMeasUtil.LOADED)
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }
            UserUtil.ReadConfig();
            if (!UserUtil.LOADED)
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }
            TestSystemUtil.ReadConfig();
            if (!TestSystemUtil.LOADED)
            {
                // 关闭程序
                this.Close();
                System.Environment.Exit(0);
            }

            try
            {

                //连接硬件设备
                //MotorDriver
                this.MotorDriver = new MoonsMotor();
                this.MotorDriver.Open();
                if (!this.MotorDriver.CONNECTED)
                    WarnWindow.ShowMsg("无法连接到运动控制卡!请检查硬件设备并重启软件!");
                //LaserDriver
                this.LaserDriver = new ItDriver();
                this.LaserDriver.Open();
                if (!this.LaserDriver.CONNECTED)
                    WarnWindow.ShowMsg("无法连接到驱动电源!请检查硬件设备并重启软件!");
                //OSA
                this.OSA = new OcOsa();
                this.OSA.Open();
                if (!this.OSA.CONNECTED)
                    WarnWindow.ShowMsg("无法连接到光谱分析仪!请检查硬件设备并重启软件!");
                //TempDriver
                this.TempDriver = new MeerstetterTECController("portname:" + TestSystemUtil.TEMP_COMM_PORT);
                this.TempDriver.Open();
                if (!this.TempDriver.Initialized)
                    WarnWindow.ShowMsg("无法连接到温度控制器!请检查硬件设备并重启软件!");
                //PowerDriver
                this.PowerDriver = new PdDriver();
                this.PowerDriver.Open();
                if (!this.PowerDriver.CONNECTED)
                    WarnWindow.ShowMsg("无法连接到光功率计!请检查硬件设备并重启软件!");
                //ThermCamDriver
                this.ThermCamDriver = new ThermCam();
                if(StationUtil.ThermCamEnable)
                {
                    this.ThermCamDriver.Open();
                    if (!this.ThermCamDriver.CONNECTED)
                        WarnWindow.ShowMsg("无法连接到热成像相机!请检查硬件设备并重启软件!");
                    else
                        this.ThermCamDriver.SetRunStat(true);
                }
                //BeamCamDriver
                this.BeamCamDriver = new BeamCam();
                this.BeamCamDriver.Open();
                if (!this.BeamCamDriver.CONNECTED)
                    WarnWindow.ShowMsg("无法连接到光斑相机!请检查硬件设备并重启软件!");
                //ThermCamDriver SetRunStat(false)
                if (StationUtil.ThermCamEnable)
                {
                    if (this.ThermCamDriver.CONNECTED)
                        this.ThermCamDriver.SetRunStat(false);
                }

                // License SN
                //StationUtil.SetDevSN(this.LaserDriver.SerialNumber, this.OSA.SerialNumber, "");

                // Info Update
                this.EquipPartNo = "设备型号：" + StationUtil.EquipPartNo;
                this.SoftwareVersion = "软件版本：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                // PrevBatchNo
                this.PrevSerialNo = "";

                // 加载程序列表
                this.ScriptNameList = TestScriptUtil.GetRoutineNameList();

                // 默认Button状态
                this.BtnStartEnabled = true;
                this.BtnStopEnabled = false;
                this.BtnPauseEnabled = false;
                this.BtnResumeEnabled = false;
                this.BtnClearEnabled = true;
                this.BtnOpenResultEnabled = false;
                this.BtnLoadResultEnabled = true;
                this.BatchLoadEnabled = true;

                // 默认状态参数
                this.LockStatus = "互锁打开";
                this.LockImage = "/Resources/UnLock.png";
                this.TestDataHeaderText = "测试数据";
                this.CurrTestStatus = "马达关闭";
                this.CurrTestSpotFill = "LightGray";
                this.CurrTestResult = "";
                this.CurrTestSubAction = "无";
                this.StageList = new List<string>() { TestSystemUtil.PRE_TEST, TestSystemUtil.POST_TEST };
                this.TestStage = this.StageList[0];

                // 默认Chart参数
                this.ThermConfigVisibility = StationUtil.ThermCamEnable ? "Visible" : "Collapsed";
                this.ChartVisibility = "Visible";
                this.BeamImageVisibility = "Hidden";
                this.ThermalImageVisibility = "Hidden";
                this.SpChartWavelengthLCL = 700;
                this.SpChartWavelengthUCL = 1000;
                this.SpChartWavelengthStep = 50;
                this.SpChartSpectrumUCL = 20000;
                this.SpChartSpectrumStep = 2000;
                this.LivChartCurrentStop = 30;
                this.LivChartCurrentStep = 5;
                this.LivChartVoltageUCL = 5;
                this.LivChartVoltageStep = 0.5;
                this.LivChartPowerUCL = 25;
                this.LivChartPowerStep = 5;
                this.Formatter = value => Math.Round(value, 2).ToString();
                this.LivToolTip = new CustomTooltip()
                {
                    XAxisName = "IF", // XAxisName = "电流",
                    SelectionMode = TooltipSelectionMode.SharedXInSeries,
                    ShowTitle = true,
                    ShowSeries = true,
                    BulletSize = 0,
                    IsWrapped = false
                };
                this.SpToolTip = new CustomTooltip()
                {
                    XAxisName = "WL", // XAxisName = "波长",
                    SelectionMode = TooltipSelectionMode.SharedXInSeries,
                    ShowTitle = true,
                    ShowSeries = true,
                    BulletSize = 0,
                    IsWrapped = false
                };
                // 添加Power坐标
                this.LivAxesY = new AxesCollection();
                this.LivAxesY.Add(new Axis()
                {
                    Title = "Power(W)",
                    Position = AxisPosition.LeftBottom,
                    MinValue = 0.0,
                    MaxValue = 25.0,
                    Foreground = Brushes.Black,
                    LabelFormatter = this.Formatter,
                    FontSize = 14,
                    Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, Step = 5.0, StrokeThickness = 1, Stroke = Brushes.Pink }
                });

                // 默认结果列表
                this.CosTestResultData = new ObservableCollection<DgTestResult>();
                this.CosTestResultData.Add(new DgTestResult());

                //初始化PROC
                this.TestProcInst = new TestProc(this.MotorDriver, this.LaserDriver, this.PowerDriver, this.OSA, this.TempDriver, this.BeamCamDriver, this.ThermCamDriver);
                this.ToolProcInst = new ToolProc(this.MotorDriver, this.LaserDriver, this.PowerDriver, this.OSA, this.TempDriver, this.BeamCamDriver, this.ThermCamDriver);
                //添加事件响应
                this.TestProcInst.TestStatUpdate += new TestStatHandler(this.TestStatUpdated);
                this.TestProcInst.TestDataUpdate += new TestDataUpdateHandler(this.TestDataUpdated);
                this.TestProcInst.TestTempSetUpdate += new TestTempSetUpdateHandler(this.TestTempSetUpdated);
                this.TestProcInst.TestProgressUpdate += new TestProgessUpdateHandler(this.TestProgressUpdated);
                this.TestProcInst.TestDiStateUpdate += new TestDiStateUpdateHandler(this.TestDiStateUpdated);
                this.TestProcInst.TestJigTemperatureUpdate += new TestJigTemperatureUpdateHandler(this.TestJigTemperatureUpdated);
                this.TestProcInst.TestMotorUpdate += new TestMotorUpdateHandler(this.TestMotorUpdated);
                this.TestProcInst.TestWarn += new TestWarnHandler(this.TestWarned);
                this.TestProcInst.TestReport += new TestReportHandler(this.TestReported);
                this.TestProcInst.TestDataLoad += new TestDataLoadHandler(this.TestDataLoaded);
                this.ToolProcInst.TestWarn += new TestWarnHandler(this.TestWarned);

                //创建FrmReport
                this.FrmReport = new ReportWindow();

                //设置ReportShowTimer
                this.ReportShowTimer = new System.Timers.Timer(200.0);
                this.ReportShowTimer.AutoReset = false;
                this.ReportShowTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReportShowTimer_Elapsed);

                //设置ReportPrintTimer
                this.ReportPrintTimer = new System.Timers.Timer(2000.0);
                this.ReportPrintTimer.AutoReset = false;
                this.ReportPrintTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReportPrintTimer_Elapsed);


                //设置TickTimer
                this.TickTimer = new System.Timers.Timer(200.0);
                this.TickTimer.AutoReset = false;
                this.TickTimer.Elapsed += new System.Timers.ElapsedEventHandler(TickTimer_Elapsed);

                //启动DMC和TickTimer
                this.TickTimer.Enabled = this.MotorDriver.CONNECTED && this.LaserDriver.CONNECTED && this.OSA.CONNECTED && this.TempDriver.Initialized && this.PowerDriver.CONNECTED;

                // 关闭Splash
                this.SplashForm.Close();
            }
            catch(Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
            }
            
           
            
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                this.MAX_NORMAL_ICON = false;
                this.MAX_NORMAL_TIPS = "正常大小";
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.MAX_NORMAL_ICON = true;
                this.MAX_NORMAL_TIPS = "最大化";
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            // 检查程序是否正在运行
            if(this.CurrTestStatus == "正在运行")
            {
                WarnWindow.ShowMsg("测试正在进行,不能退出!");
                return;
            }

            ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.EXIT);
            confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
            confirmForm.Show();
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        private void Exit()
        {
            // 关闭马达和激光器
            if (this.MotorDriver != null && this.MotorDriver.CONNECTED)
                this.MotorDriver.MotorOff();
            if (this.LaserDriver != null && this.LaserDriver.CONNECTED)
                this.LaserDriver.SetOutput(false);
            if (this.TempDriver != null && this.TempDriver.Initialized)
                this.TempDriver.IsOutputEnabled = false;

            // 关闭程序
            this.Close();
            System.Environment.Exit(0);
        }

        private void gridCaption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 登陆/退出登陆的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LoginActCallback(object sender, LoginEventArgs args)
        {
            if (args.ACT == EnLoginAct.LOGOUT)
            {
                this.LOGIN_ICON = @"/Resources/Logout.png";
                this.LOGIN_TIPS = "未登陆";
                this.SysEditEnabled = false;
            }
            else
            {
                this.LOGIN_ICON = @"/Resources/Logon.png";
                this.LOGIN_TIPS = UserUtil.ActiveUser.NAME + " [" + UserUtil.ActiveUser.GROUP.ToString() + "]";
                if (UserUtil.ActiveUser.NAME != "" && UserUtil.ActiveUser.GROUP == EnGROUP.Admin)
                {
                    this.SysEditEnabled = true;
                }
                else
                {
                    this.SysEditEnabled = false;
                }
            }
        }

        private void buttonSystem_Click(object sender, RoutedEventArgs e)
        {
            // 检查程序是否正在运行
            if (this.CurrTestStatus == "正在运行")
            {
                WarnWindow.ShowMsg("测试正在进行,不能进行系统设置!");
                return;
            }
            // 检查程序是否处于在暂停状态
            if (this.CurrTestStatus == "已暂停")
            {
                WarnWindow.ShowMsg("测试仍处于暂停状态,不能进行系统设置!");
                return;
            }
            // Show
            if (this.FrmSystem == null || this.FrmSystem.IsClosed == true)
                this.FrmSystem = new SystemWindow(this.MotorDriver);
            this.FrmSystem.Show();
        }

        private void buttonTool_Click(object sender, RoutedEventArgs e)
        {
            // 检查程序是否正在运行
            if (this.CurrTestStatus == "正在运行")
            {
                WarnWindow.ShowMsg("测试正在进行,不能进行工程调试!");
                return;
            }
            // 检查程序是否处于在暂停状态
            if (this.CurrTestStatus == "已暂停")
            {
                WarnWindow.ShowMsg("测试仍处于暂停状态,不能进行工程调试!");
                return;
            }
            // Show
            if (this.FrmTool== null || this.FrmTool.IsClosed == true)
                this.FrmTool = new ToolWindow(this.ToolProcInst, this.TestProcInst);
            this.FrmTool.Show();
        }

        private void buttonScript_Click(object sender, RoutedEventArgs e)
        {
            // 检查程序是否正在运行
            if (this.CurrTestStatus == "正在运行")
            {
                WarnWindow.ShowMsg("测试正在进行,不能进行程序编辑!");
                return;
            }
            // 检查程序是否处于在暂停状态
            if (this.CurrTestStatus == "已暂停")
            {
                WarnWindow.ShowMsg("测试仍处于暂停状态,不能进行程序编辑!");
                return;
            }

            if (this.FrmTestRoutine == null || this.FrmTestRoutine.IsClosed == true)
            {
                this.FrmTestRoutine = new TestScriptWindow();
                this.FrmTestRoutine.RoutineUpdated += new RoutineUpdateHandler(this.TestRoutineUpdated);
            }
            this.FrmTestRoutine.Show();
        }

        private void buttonThermConfig_Click(object sender, RoutedEventArgs e)
        {
            // 检查程序是否正在运行
            if (this.CurrTestStatus == "正在运行")
            {
                WarnWindow.ShowMsg("测试正在进行,不能进行程序编辑!");
                return;
            }
            // 检查程序是否处于在暂停状态
            if (this.CurrTestStatus == "已暂停")
            {
                WarnWindow.ShowMsg("测试仍处于暂停状态,不能进行程序编辑!");
                return;
            }
            if (this.FrmThermMeasConfig == null || this.FrmThermMeasConfig.IsClosed == true)
            {
                this.FrmThermMeasConfig = new ThermMeasureWindow(this.ToolProcInst);
            }
            this.FrmThermMeasConfig.Show();
        }

        private void buttonUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.FrmUser == null || this.FrmUser.IsClosed == true)
                this.FrmUser = new UserWindow();
            this.FrmUser.Show();
        }

        private void buttonAbout_Click(object sender, RoutedEventArgs e)
        {
            if (this.FrmAbout == null || this.FrmAbout.IsClosed == true)
                this.FrmAbout = new AboutWindow();
            this.FrmAbout.Show();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            //未登陆
            if (UserUtil.ActiveUser.NAME == null || UserUtil.ActiveUser.NAME == "")
            {
                if(this.FrmUserLogOn == null || this.FrmUserLogOn.IsClosed == true)
                {
                    this.FrmUserLogOn = new UserLogOnWindow();
                    this.FrmUserLogOn.LoginEvent += new LoginHandler(this.LoginActCallback);
                }
                this.FrmUserLogOn.Show();
            }
            //已登陆
            else
            {
                if(this.FrmUserLogoff == null || this.FrmUserLogoff.IsClosed == true)
                {
                    this.FrmUserLogoff = new UserLogOutWindow();
                    this.FrmUserLogoff.LoginEvent += new LoginHandler(this.LoginActCallback);
                }
                this.FrmUserLogoff.Show();
            }
        }

        /// <summary>
        /// 定时器触发---无限循环(非UI线程)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TickTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //运行TestProc
            if (this.ToolProcInst.STAT != EnToolStat.RUNNING)
                this.TestProcInst.Run();
            //运行ToolProc
            if (this.TestProcInst.STAT != EnTestStat.MOVING && this.TestProcInst.STAT != EnTestStat.LIV && this.TestProcInst.STAT != EnTestStat.SPECTRUM && this.TestProcInst.STAT != EnTestStat.NASCAN && this.TestProcInst.STAT != EnTestStat.TEMP)
                this.ToolProcInst.Run();
            //重启Timer
            this.TickTimer.Enabled = true;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            // 检查批次号是否满足要求
            if (this.BatchNo == null || this.BatchNo.Trim() == "")
            {
                WarnWindow.ShowMsg("批次号不能为空!");
                return;
            }

            // 检查序列号
            if (this.SerialNo == null || this.SerialNo.Trim() == "")
            {
                WarnWindow.ShowMsg("序列号不能为空!");
                return;
            }

            // 检查产品型号是否正确
            if (this.PartNo == null || this.PartNo.Trim() == "")
            {
                WarnWindow.ShowMsg("型号不能为空!");
                return;
            } 

            // 检查工序是否正确
            if (this.TestStage == null || this.TestStage.Trim() == "")
            {
                WarnWindow.ShowMsg("工序不能为空!");
                return;
            }

            // 检查脚本名是否正确
            if (this.SelScriptName == null || this.SelScriptName.Trim() == "" || this.SelScriptName.Trim() == "N/A")
            {
                WarnWindow.ShowMsg("请先选择测试程序!");
                return;
            }

            // 检查序列号是否重复
            if (this.SerialNo == this.PrevSerialNo)
            {
                ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.STARTAGAIN);
                confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
                confirmForm.Show();
            }
            else
            {
                ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.START);
                confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
                confirmForm.Show();
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.STOP);
            confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
            confirmForm.Show();
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.PAUSE);
            confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
            confirmForm.Show();
        }

        private void buttonResume_Click(object sender, RoutedEventArgs e)
        {
            ConfirmWindow confirmForm = new ConfirmWindow(EnTestAct.RESUME);
            confirmForm.TestConfirmed += new ConfirmHandler(this.TestActConfirmed);
            confirmForm.Show();
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            // 检查批次号是否满足要求
            if (this.BatchNo == null || this.BatchNo.Trim() == "")
            {
                WarnWindow.ShowMsg("批次号不能为空!");
                return;
            }

            // 检查序列号是否满足要求
            if (this.SerialNo == null || this.SerialNo.Trim() == "")
            {
                WarnWindow.ShowMsg("序列号不能为空!");
                return;
            }

            // 连接数据库
            OleDbConnection conn = null;

            try
            {
                //连接数据库
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + StationUtil.GetTestMappingFilePath() + ";";
                conn = new OleDbConnection(strConn);
                //加载数据
                string sql = "SELECT * FROM TEST where SERIAL_NO='" + this.SerialNo + "'";
                DataTable dt = new DataTable();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, conn);
                dataAdapter.Fill(dt);
                //判断数据
                if (dt.Rows.Count > 0)
                {
                    this.PartNo = (string)dt.Rows[0]["PART_NO"];
                    this.SelScriptName = (string)dt.Rows[0]["TEST_SCRIPT"];
                }
                else
                {
                    this.PartNo = null;
                    this.SelScriptName = null;
                    WarnWindow.ShowMsg("该批次号未添加预定义测试规则!");
                }
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("连接预定测试规则数据库时发生错误!");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        /// <summary>
        /// 操作确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestActConfirmed(object sender, ConfirmEventArgs args)
        {
            if (args.YES)
            {
                switch (args.ACT)
                {
                    case EnTestAct.START:
                    case EnTestAct.STARTAGAIN:
                        if (this.BtnStartEnabled)
                            this.Start();
                        break;
                    case EnTestAct.STOP:
                        if (this.BtnStopEnabled)
                            this.Stop();
                        break;
                    case EnTestAct.PAUSE:
                        if (this.BtnPauseEnabled)
                            this.Pause();
                        break;
                    case EnTestAct.RESUME:
                        if (this.BtnResumeEnabled)
                            this.Resume();
                        break;
                    case EnTestAct.EXIT:
                        this.Exit();
                        break;
                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// Start
        /// </summary>

        private void Start()
        {
            //检查硬件连接是否正常
            if (!this.MotorDriver.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到运动控制卡!不能启动测试!");
                return;
            }
            if (!this.LaserDriver.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到驱动电源!不能启动测试!");
                return;
            }
            if (!this.OSA.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到光谱分析仪!不能启动测试!");
                return;
            }
            if (this.TMP > TestSystemUtil.TEMP_UCL || this.TMP < TestSystemUtil.TEMP_LCL)
            {
                ErrLog.WriteLine("TEMP " + this.TMP + "; TEMP_UCL " + TestSystemUtil.TEMP_UCL + "; LCL " + TestSystemUtil.TEMP_LCL);
                WarnWindow.ShowMsg("夹具温度异常!不能启动测试!");
                return;
            }

            // 清空图表数据
            this.SpectrumSeries = null;
            this.LivSeries = null;
            this.CosTestResultData = new ObservableCollection<DgTestResult>();
            this.CosTestResultData.Add(new DgTestResult());
            this.CurrTestResult = "";
            this.ChartVisibility = "Visible";
            this.BeamImageVisibility = "Hidden";
            this.ThermalImageVisibility = "Hidden";

            // 检查是否License过期
            //if (DateTime.Now > StationUtil.LicenseDueDate)
            //{
            //    WarnWindow.ShowMsg("许可证信息不匹配,无法启动");
            //    return;
            //}

            // 暂时禁用所有Button, 等待运行线程的响应
            this.BtnStartEnabled = false;
            this.BtnStopEnabled = false;
            this.BtnPauseEnabled = false;
            this.BtnResumeEnabled = false;
            this.BatchLoadEnabled = false;

            // Record BatchNo
            this.PrevSerialNo = this.SerialNo;

            // 启动
            this.TestProcInst.Start(this.BatchNo, this.SerialNo, this.PartNo, this.TestStage, this.SelScriptName);
        }

        /// <summary>
        /// Stop
        /// </summary>
        private void Stop()
        {
            // 暂时禁用所有Button, 等待运行线程的响应
            this.BtnStartEnabled = false;
            this.BtnStopEnabled = false;
            this.BtnPauseEnabled = false;
            this.BtnResumeEnabled = false;
            this.BatchLoadEnabled = false;

            // 停止
            this.TestProcInst.Stop();
        }

        /// <summary>
        /// Resume
        /// </summary>
        private void Resume()
        {
            //检查硬件连接是否正常
            if (!this.MotorDriver.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到运动控制卡!不能重启测试!");
                return;
            }
            if (!this.LaserDriver.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到驱动电源!不能重启测试!");
                return;
            }
            if (!this.OSA.CONNECTED)
            {
                WarnWindow.ShowMsg("无法连接到光谱分析仪!不能重启测试!");
                return;
            }
            if (!this.MotorDriver.GetDiStat().Interlock)
            {
                WarnWindow.ShowMsg("门锁未关闭!不能重启测试!");
                return;
            }
            if (this.TMP > TestSystemUtil.TEMP_UCL || this.TMP < TestSystemUtil.TEMP_LCL)
            {
                WarnWindow.ShowMsg("温度传感器1异常!不能启动测试!");
                return;
            }

            // 暂时禁用所有Button, 等待运行线程的响应
            this.BtnStartEnabled = false;
            this.BtnStopEnabled = false;
            this.BtnPauseEnabled = false;
            this.BtnResumeEnabled = false;
            this.BatchLoadEnabled = false;

            // 重启
            this.TestProcInst.Resume();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        private void Pause()
        {
            // 暂时禁用所有Button, 等待运行线程的响应
            this.BtnStartEnabled = false;
            this.BtnStopEnabled = false;
            this.BtnPauseEnabled = false;
            this.BtnResumeEnabled = false;
            this.BatchLoadEnabled = false;

            // 暂停
            this.TestProcInst.Pause();
        }

        private void gridCosTag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsDataCleared)
                return;
            if (this.ShowCosPositionDisabled)
                return;
            this.TestProcInst.ShowCosTestData();
            if (!this.TickTimer.Enabled)
                this.TestProcInst.RunShowCosTestData();
        }

        /// <summary>
        /// 脚本更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestRoutineUpdated(object sender, RoutineUpdateEventArgs args)
        {
            // 更新列表
            List<string> routineNames = TestScriptUtil.GetRoutineNameList();
            this.ScriptNameList = routineNames;

            // 更新Routine内容
            string selRtName = this.SelScriptName;
            this.SelScriptName = null;
            if (this.ScriptNameList.Contains(selRtName))
                this.SelScriptName = selRtName;
        }

        /// <summary>n
        /// 更新测试进度，每个COS的pass/fail及其总数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestProgressUpdated(object sender, TestProgressUpdateEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.CurrTestResult = args.CosResult;
                if (this.CurrTestResult.ToUpper() == "PASS")
                    this.CurrTestResultColor = "Green";
                else
                    this.CurrTestResultColor = "Red";
            });
        }

        /// <summary>
        /// 更新夹具位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestTempSetUpdated(object sender, TestTempSetUpdateEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.TSET = Math.Round(args.TSET, 2);
            });
        }

        /// <summary>
        /// 更新互锁状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestDiStateUpdated(object sender, TestDiStateUpdateEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                if(args.INTERLOCK == true)
                {
                    this.LockStatus = "互锁关闭";
                    this.LockImage = "/Resources/Lock.png";
                }
                else
                {
                    this.LockStatus = "互锁打开";
                    this.LockImage = "/Resources/UnLock.png";
                }

                this.MIRROR = args.MIRROR;
                this.HOME = args.HOME;
            });
        }

        /// <summary>
        /// 更新夹具温度状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestJigTemperatureUpdated(object sender, TestJigTemperatureEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.TMP = Math.Round(args.TMP, 2);
            });
        }

        /// <summary>
        /// 更新马达状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestMotorUpdated(object sender, TestMotorUpdateEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.POSY = Math.Round(args.POSY, 2);
            });
        }

        /// <summary>
        /// TestWarned
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestWarned(object sender, TestWarnEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                WarnWindow.ShowMsg(args.Msg);
            });
        }

        /// <summary>
        /// 测试状态更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestStatUpdated(object sender, TestStatEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                switch (args.STAT)
                {
                    //case EnTestStat.PWROFF:
                    //    this.CurrTestStatus = "马达关闭";
                    //    this.CurrTestSpotFill = "LightGray";
                    //    this.CurrTestSubAction = "无";
                    //    this.BtnJigInitEnabled = true;
                    //    this.BtnStartEnabled = false;
                    //    this.BtnStopEnabled = false;
                    //    this.BtnPauseEnabled = false;
                    //    this.BtnResumeEnabled = false;
                    //    this.BtnClearEnabled = true;
                    //    this.BtnOpenResultEnabled = false;
                    //    this.BtnLoadResultEnabled = true;
                    //    this.BatchLoadEnabled = true;
                    //    this.ShowCosPositionDisabled = false;
                    //    break;
                    //case EnTestStat.PWRON:
                    //    this.CurrTestStatus = "马达上电";
                    //    this.CurrTestSpotFill = "Yellow";
                    //    this.CurrTestSubAction = "无";
                    //    this.BtnJigInitEnabled = true;
                    //    this.BtnStartEnabled = true;
                    //    this.BtnStopEnabled = false;
                    //    this.BtnPauseEnabled = false;
                    //    this.BtnResumeEnabled = false;
                    //    this.BtnClearEnabled = true;
                    //    this.BtnOpenResultEnabled = false;
                    //    this.BtnLoadResultEnabled = true;
                    //    this.BatchLoadEnabled = true;
                    //    this.ShowCosPositionDisabled = false;
                    //    break;
                    case EnTestStat.LIV:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "LIV扫描";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.MOVING:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "夹具移动";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.SPECTRUM:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "光谱扫描";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.TEMP:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "温度控制";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.NASCAN:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "N.A.扫描";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.BEAMSCAN:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "光斑扫描";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.THERMSCAN:
                        this.CurrTestStatus = "正在运行";
                        this.CurrTestSpotFill = "Green";
                        this.CurrTestSubAction = "热像扫描";
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = true;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = false;
                        this.ShowCosPositionDisabled = true;
                        break;
                    case EnTestStat.FINISHED:
                        this.CurrTestStatus = "已完成";
                        this.CurrTestSpotFill = "Yellow";
                        this.CurrTestSubAction = "无";
                        this.BtnStartEnabled = true;
                        this.BtnStopEnabled = false;
                        this.BtnPauseEnabled = false;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = true;
                        this.BtnOpenResultEnabled = true;
                        this.BtnLoadResultEnabled = true;
                        this.BatchLoadEnabled = true;
                        this.ShowCosPositionDisabled = false;
                        break;
                    case EnTestStat.STOPED:
                        this.CurrTestStatus = "已结束";
                        this.CurrTestSpotFill = "Yellow";
                        this.CurrTestSubAction = "无";
                        this.BtnStartEnabled = true;
                        this.BtnStopEnabled = false;
                        this.BtnPauseEnabled = false;
                        this.BtnResumeEnabled = false;
                        this.BtnClearEnabled = true;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = true;
                        this.BatchLoadEnabled = true;
                        this.ShowCosPositionDisabled = false;
                        break;
                    case EnTestStat.PAUSED:
                        this.CurrTestStatus = "已暂停";
                        this.CurrTestSpotFill = "Yellow";
                        this.CurrTestSubAction = "无"; 
                        this.BtnStartEnabled = false;
                        this.BtnStopEnabled = true;
                        this.BtnPauseEnabled = false;
                        this.BtnResumeEnabled = true;
                        this.BtnClearEnabled = false;
                        this.BtnOpenResultEnabled = false;
                        this.BtnLoadResultEnabled = false;
                        this.BatchLoadEnabled = true;
                        this.ShowCosPositionDisabled = true;
                        break;
                }
            });
        }

        /// <summary>
        /// 测试结果加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestDataLoaded(object sender, TestDataLoadEventArgs args)
        {
            this.BatchNo = args.BatchNumber;
            this.SerialNo = args.SerialNumber;
            this.PartNo = args.PartNumber;
            this.TestStage = args.TestStage;
            this.SelScriptName = args.RoutineName;
       
        }

        /// <summary>
        /// 测试结果更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestDataUpdated(object sender,TestDataUpdateEventArgs  args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                // 标志
                this.IsDataCleared = false;

                // 清除数据
                if (args.AddType == EnTestDataAddType.CLEAR)
                {
                    // 隐藏Beam和Thermal照片
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";
                    // 清空数据
                    if (this.LivSeries != null)
                        this.LivSeries.Clear();
                    if (this.SpectrumSeries != null)
                        this.SpectrumSeries.Clear();
                    this.BeamImage = null;
                    this.ThermalImage = null;
                }
                // 添加LIV数据
                else if (args.AddType == EnTestDataAddType.LIV)
                {
                    // 隐藏Beam和Thermal照片
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";

                    // LIV 数据
                    double maxPower = 1.0, maxVoltage = 1.0, maxCurrent = 1.0;
                    this.LivSeries = new SeriesCollection();
                    if (args.CosTestData.LivRawData != null)
                    {
                        for (int i = 0; i < args.CosTestData.LivRawData.Count; i++)
                        {
                            if (args.TempIndex >= 0 && args.TempIndex != i)
                                continue;

                            #region 曲线数据
                            var valuesPower = new ChartValues<Point>();
                            var valuesVoltage = new ChartValues<Point>();
                            var valuesEff = new ChartValues<Point>();

                            for (int j = 0; j < args.CosTestData.LivRawData[i].Count; j++)
                            {
                                var pointPower = new Point() { X = args.CosTestData.LivRawData[i][j].LDI, Y = args.CosTestData.LivRawData[i][j].PDI };
                                valuesPower.Add(pointPower);

                                var pointVoltage = new Point() { X = args.CosTestData.LivRawData[i][j].LDI, Y = args.CosTestData.LivRawData[i][j].LDV };
                                valuesVoltage.Add(pointVoltage);

                                double eff = 0.0;
                                if (args.CosTestData.LivRawData[i][j].LDI <= 0.0 || args.CosTestData.LivRawData[i][j].LDV <= 0.0 || args.CosTestData.LivRawData[i][j].PDI <= 0.0)
                                    eff = 0.0;
                                else
                                    eff = args.CosTestData.LivRawData[i][j].PDI / (args.CosTestData.LivRawData[i][j].LDI * args.CosTestData.LivRawData[i][j].LDV);
                                var pointEff = new Point() { X = args.CosTestData.LivRawData[i][j].LDI, Y = eff };
                                if (args.CosTestData.LivRawData[i][j].LDI > TestSystemUtil.EFF_MIN_CURRENT)
                                    valuesEff.Add(pointEff);

                                maxPower = args.CosTestData.LivRawData[i][j].PDI > maxPower ? args.CosTestData.LivRawData[i][j].PDI : maxPower;
                                maxVoltage = args.CosTestData.LivRawData[i][j].LDV > maxVoltage ? args.CosTestData.LivRawData[i][j].LDV : maxVoltage;
                                maxCurrent = args.CosTestData.LivRawData[i][j].LDI > maxCurrent ? args.CosTestData.LivRawData[i][j].LDI : maxCurrent;
                            }
                            #endregion

                            #region Power
                            this.LivSeries.Add(new LineSeries()
                            {
                                Configuration = new CartesianMapper<Point>()
                                    .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                                    .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                                    .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                                    .Fill(point => Brushes.Black),
                                Title = "PO@" + args.CosTestData.TestTemps[i] + "C",
                                Values = valuesPower,
                                PointGeometry = null,
                                LineSmoothness = 0,
                                Stroke = Brushes.Black,
                                Fill = new SolidColorBrush(Color.FromArgb(0x3F, 0xD3, 0xD3, 0xD3))
                            });

                            // Chart Axis
                            double livChartPowerUCL = 5.0;
                            while (livChartPowerUCL < maxPower)
                                livChartPowerUCL += 5;
                            double livChartCurrentStep = 1.0;
                            while (maxCurrent / livChartCurrentStep >= 15)
                            {
                                if (livChartCurrentStep == 2.0)
                                    livChartCurrentStep = 5.0;
                                else
                                    livChartCurrentStep *= 2.0;
                            }
                            this.LivAxesY[0].MaxValue = livChartPowerUCL;
                            this.LivAxesY[0].Separator.Step = livChartPowerUCL / 5;
                            this.LivChartCurrentStop = maxCurrent;
                            this.LivChartCurrentStep = livChartCurrentStep;
                            #endregion

                            #region 查找VF & EFF轴并删除
                            for (int k = this.LivAxesY.Count - 1; k >= 0; k--)
                            {
                                if (this.LivAxesY[k].Title == "Voltage(V)" && args.IsShowVF == false)
                                {
                                    this.LivAxesY.RemoveAt(k);
                                }
                                if (this.LivAxesY[k].Title == "Efficiency(%)" && args.IsShowEFF == false)
                                {
                                    this.LivAxesY.RemoveAt(k);
                                }
                            }
                            #endregion

                            #region Voltage
                            if (args.IsShowVF)
                            {
                                // 查找VF轴
                                int iAxisVF = -1;
                                for (int k = 0; k < this.LivAxesY.Count; k++)
                                {
                                    if(this.LivAxesY[k].Title == "Voltage(V)")
                                    {
                                        iAxisVF = k;
                                        break;
                                    }
                                }
                                // 添加VF轴
                                if(iAxisVF < 0)
                                {
                                    this.LivAxesY.Add(new Axis()
                                    {
                                        Title = "Voltage(V)",
                                        Position = AxisPosition.RightTop,
                                        MinValue = 0.0,
                                        Foreground = Brushes.Blue,
                                        LabelFormatter = this.Formatter,
                                        FontSize = 14,
                                        Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, StrokeThickness = 1, Stroke = Brushes.Pink }
                                    });
                                    iAxisVF = this.LivAxesY.Count - 1;
                                }

                                // Chart Axis
                                double livChartVoltageUCL = 5.0;
                                while (livChartVoltageUCL < maxVoltage)
                                    livChartVoltageUCL += 1.0;
                                double livChartVoltageStep = livChartVoltageUCL / 10;
                                this.LivAxesY[iAxisVF].MaxValue = livChartVoltageUCL;
                                this.LivAxesY[iAxisVF].Separator.Step = livChartVoltageStep;

                                // 添加曲线
                                this.LivSeries.Add(new LineSeries()
                                {
                                    Configuration = new CartesianMapper<Point>()
                                        .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                                        .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                                        .Stroke(point => Brushes.Blue) // Define a function that returns a Brush based on the current data item
                                        .Fill(point => Brushes.Blue),
                                    Title = "VF@" + args.CosTestData.TestTemps[i] + "C",
                                    Values = valuesVoltage,
                                    PointGeometry = null,
                                    LineSmoothness = 0,
                                    Stroke = Brushes.Blue,
                                    Fill = new SolidColorBrush(Color.FromArgb(0x3F, 0xAD, 0xD8, 0xE6)),
                                    ScalesYAt = iAxisVF
                                });
                            }
                            #endregion

                            #region Efficiency
                            if (args.IsShowEFF)
                            {
                                // 查找EFF轴
                                int iAxisEFF = -1;
                                for (int k = 0; k < this.LivAxesY.Count; k++)
                                {
                                    if (this.LivAxesY[k].Title == "Efficiency(%)")
                                    {
                                        iAxisEFF = k;
                                        break;
                                    }
                                }
                                // 添加EFF轴
                                if (iAxisEFF < 0)
                                {
                                    this.LivAxesY.Add(new Axis()
                                    {
                                        Title = "Efficiency(%)",
                                        Position = AxisPosition.LeftBottom,
                                        MinValue = 0.0,
                                        MaxValue = 1.0,
                                        Foreground = Brushes.Red,
                                        LabelFormatter = this.Formatter,
                                        FontSize = 14,
                                        Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, Step = 0.2, StrokeThickness = 1, Stroke = Brushes.Pink }
                                    });
                                    iAxisEFF = this.LivAxesY.Count - 1;
                                }

                                // 添加曲线
                                this.LivSeries.Add(new LineSeries()
                                {
                                    Configuration = new CartesianMapper<Point>()
                                        .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                                        .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                                        .Stroke(point => Brushes.Red) // Define a function that returns a Brush based on the current data item
                                        .Fill(point => Brushes.Red),
                                    Title = "EFF@" + args.CosTestData.TestTemps[i] + "C",
                                    Values = valuesEff,
                                    PointGeometry = null,
                                    LineSmoothness = 0,
                                    Stroke = Brushes.Red,
                                    Fill = new SolidColorBrush(Color.FromArgb(0x3F, 0xFF, 0xB6, 0xC1)),
                                    ScalesYAt = iAxisEFF
                                });
                            }
                            #endregion
                        }
                    }


                    // Test Data
                    this.CosTestResultData = new ObservableCollection<DgTestResult>();
                    if (args.CosTestData.LivTestData != null)
                    {
                        int id = 1;
                        // LIV Param
                        foreach (StCosParam param in args.CosTestData.LivTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                        if (args.CosTestData.SpTestData != null)
                        {
                            // SP Param
                            foreach (StCosParam param in args.CosTestData.SpTestData)
                            {
                                this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                                id++;
                            }
                        }
                        if (args.CosTestData.ThmTestData != null)
                        {
                            // Therm Param
                            foreach (StCosParam param in args.CosTestData.ThmTestData)
                            {
                                this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                                id++;
                            }
                        }
                        if (args.CosTestData.NaTestData != null)
                        {
                            // NA Param
                            foreach (StCosParam param in args.CosTestData.NaTestData)
                            {
                                this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                                id++;
                            }
                        }
                    }
                }
                // 添加光谱数据
                else if (args.AddType == EnTestDataAddType.SPECTRUM)
                {
                    // 隐藏Beam和Thermal照片
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";

                    // 光谱数据
                    this.SpectrumSeries = new SeriesCollection();
                    // Chart Axis
                    double maxSpectrum = 5000;
                    double spChartWavelengthLCL = 1000;
                    double spChartWavelengthUCL = 700;

                    if (args.CosTestData.SpRawData != null)
                    {
                        for (int i = 0; i < args.CosTestData.SpRawData.Count; i++)
                        {
                            if (args.TempIndex >= 0 && args.TempIndex != i)
                                continue;

                            foreach (StOcSpData ocSpData in args.CosTestData.SpRawData[i])
                            {
                                if (ocSpData.Wavelength == null || ocSpData.Wavelength.Length <= 0)
                                    continue;

                                // 数据点 — 三重平滑: SG(锁峰位,温和w=9) + 高斯×2(彻底磨基线锯齿)
                                double[] smoothSpectrum = CosAlgorithm.SmoothSpectrum(ocSpData.Wavelength, ocSpData.Spectrum, mode: 0, strength: 2);
                                var values = new ChartValues<Point>();
                                for (int j = 0; j < ocSpData.Wavelength.Length; j++)
                                {
                                    var point = new Point() { X = ocSpData.Wavelength[j], Y = smoothSpectrum[j] };
                                    values.Add(point);
                                }
                                // 更新 Y轴 上限基准 — 用原始数据的 max (不受 smooth 强度影响)
                                if (ocSpData.Spectrum.Max<double>() > maxSpectrum)
                                    maxSpectrum = ocSpData.Spectrum.Max<double>();
                                // drv
                                int idx = ocSpData.DRV.LastIndexOf('@') + 1;
                                string drv = ocSpData.DRV.Substring(idx, ocSpData.DRV.Length - idx).Trim();
                                // 添加Sery
                                this.SpectrumSeries.Add(new LineSeries()
                                {
                                    Configuration = new CartesianMapper<Point>()
                                          .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                                          .Y(point => point.Y), // Define a function that returns a value that should map to the y-axis
                                    Title = "Sp@" + drv + "," +args.CosTestData.TestTemps[i] + "C",
                                    Values = values,
                                    PointGeometry = null,
                                    LineSmoothness = 1
                                });
                                // 波长上下限
                                if (ocSpData.Wavelength.First<double>() < spChartWavelengthLCL)
                                    spChartWavelengthLCL = ocSpData.Wavelength.First<double>();
                                if (ocSpData.Wavelength.Last<double>() > spChartWavelengthUCL)
                                    spChartWavelengthUCL = ocSpData.Wavelength.Last<double>();
                            }
                        }                           
                    }

                    // Wavelength LCL & UCL & STEP
                    if (spChartWavelengthLCL > spChartWavelengthUCL)
                    {
                        spChartWavelengthLCL = 700;
                        spChartWavelengthUCL = 1000;
                    }
                    spChartWavelengthLCL = Math.Floor(spChartWavelengthLCL / 10) * 10;
                    spChartWavelengthUCL = Math.Ceiling(spChartWavelengthUCL / 10) * 10;
                    double spChartWavelengthStep = 5.0;
                    while ((spChartWavelengthUCL - spChartWavelengthLCL) / spChartWavelengthStep > 10)
                        spChartWavelengthStep *= 2;
                    this.SpChartWavelengthUCL = spChartWavelengthUCL;
                    this.SpChartWavelengthLCL = spChartWavelengthLCL;
                    this.SpChartWavelengthStep = spChartWavelengthStep;

                    // Spectrum UCL — 基于平滑后数据最大值, 留 5% 余量, 好数归一化 (细粒度)
                    {
                        double target = maxSpectrum * 1.05;                 // 仅留 5% 余量
                        if (target < 100) target = 100;
                        // 好数表 (细粒度): 覆盖 1.1x ~ 5x 各量级
                        double[] goodSteps = { 1.0, 1.1, 1.2, 1.25, 1.5, 1.75, 2.0, 2.25, 2.5, 3.0, 4.0, 5.0 };
                        double power = Math.Pow(10, Math.Floor(Math.Log10(target)));
                        double normalized = target / power;
                        double pick = goodSteps[goodSteps.Length - 1];
                        foreach (double gs in goodSteps)
                            if (gs >= normalized) { pick = gs; break; }
                        this.SpChartSpectrumUCL = pick * power;
                        // Step: 让 Y轴 大约 5~7 个刻度
                        this.SpChartSpectrumStep = this.SpChartSpectrumUCL / 6.0;
                    }


                    // Test Data
                    this.CosTestResultData = new ObservableCollection<DgTestResult>();
                    int id = 1;
                    // LIV Param
                    if (args.CosTestData.LivTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.LivTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // SP Param
                    if (args.CosTestData.SpTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.SpTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // Therm Param
                    if (args.CosTestData.ThmTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.ThmTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // N.A. Param
                    if (args.CosTestData.NaTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.NaTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f3"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
                // 添加NA数据
                else if (args.AddType == EnTestDataAddType.NASCAN)
                {
                    // Test Data
                    this.CosTestResultData = new ObservableCollection<DgTestResult>();
                    int id = 1;
                    // LIV Param
                    if (args.CosTestData.LivTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.LivTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // SP Param
                    if (args.CosTestData.SpTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.SpTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // Therm Param
                    if (args.CosTestData.ThmTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.ThmTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // N.A. Param
                    if (args.CosTestData.NaTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.NaTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f3"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
                // 添加BEAM数据
                else if (args.AddType == EnTestDataAddType.BEAMSCAN)
                {
                    // Test Data
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>??????????????????????

                    // Beam Image
                    this.BeamImage = null;
                    // 默认只显示LIV和SP
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";
                    if (args.CosTestData.BmRawData != null)
                    {
                        for (int i = args.CosTestData.BmRawData.Count - 1; i >= 0; i--)
                        {
                            if(args.CosTestData.BmRawData[i].Count > 0)
                            {
                                this.BeamImage = Mat2BitmapImage.Cvt(args.CosTestData.BmRawData[i].Last().BeamImage);
                                break;
                            }
                        }
                    }
                }
                // 添加THERMAL数据
                else if (args.AddType == EnTestDataAddType.THERMSCAN)
                {
                    // Thermal Image
                    this.ThermalImage = null;
                    // 默认只显示LIV和SP
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";
                    if (args.CosTestData.ThmRawData != null)
                    {
                        for (int i = args.CosTestData.ThmRawData.Count - 1; i >= 0; i--)
                        {
                            if(args.CosTestData.ThmRawData[i].Count > 0)
                            {
                                this.ThermalImage = Bitmap2BitmapImage.Cvt(args.CosTestData.ThmRawData[i].Last().ThermalImage);
                                break;
                            }
                        }
                    }

                    // Test Data
                    this.CosTestResultData = new ObservableCollection<DgTestResult>();
                    int id = 1;
                    // LIV Param
                    if (args.CosTestData.LivTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.LivTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                    // SP Param
                    if (args.CosTestData.SpTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.SpTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                    // Therm Param
                    if (args.CosTestData.ThmTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.ThmTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                    // N.A. Param
                    if (args.CosTestData.NaTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.NaTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f3"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
                // 添加DELTA数据
                else if (args.AddType == EnTestDataAddType.DELTA)
                {
                    // Test Data
                    this.CosTestResultData = new ObservableCollection<DgTestResult>();
                    int id = 1;
                    // LIV Param
                    if (args.CosTestData.LivTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.LivTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // SP Param
                    if (args.CosTestData.SpTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.SpTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // Therm Param
                    if (args.CosTestData.ThmTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.ThmTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // N.A. Param
                    if (args.CosTestData.NaTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.NaTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f3"), TestResult = param.TestResult });
                            id++;
                        }
                    }

                    // DELTA Param
                    if (args.CosTestData.DeltaTestData != null)
                    {
                        foreach (StCosParam param in args.CosTestData.DeltaTestData)
                        {
                            this.CosTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = "DELTA-" + param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 更新报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TestReported(object sender, TestReportEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                if(this.FrmReport != null && this.FrmReport.IsClosed == false)
                    this.FrmReport.Close();

                if (args.CosTestData.SerialNumber == null || args.CosTestData.SerialNumber == "")
                    return;

                this.ReportCosTestData = args.CosTestData;
                this.ReportDstFolder = args.DstFolder;

                this.FrmReport.ReportData.PartNo = args.PartNo;
                this.FrmReport.ReportData.TestRoutine = args.TestRoutine;
                this.FrmReport.ReportData.TestStage = args.TestStage;
                this.FrmReport.ReportData.IsShowVF = args.IsShowVF;
                this.FrmReport.ReportData.IsShowEFF = args.IsShowEFF;
                this.FrmReport.Show();
                
                this.ReportShowTimer.Enabled = true;
            });
        }

        /// <summary>
        /// 定时器触发---无限循环(非UI线程)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportShowTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                try
                {
                    // 1. 先填充曲线和参数数据（核心功能，不能因为二维码崩了就全部跳过）
                    this.FrmReport.ReportData.SetCosTestData(this.ReportCosTestData);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("ReportShowTimer_Elapsed SetCosTestData error: " + ex.ToString());
                }

                try
                {
                    // 2. 二维码独立生成，防止 QRCoder.dll 缺失阻塞报告数据
                    this.FrmReport.ReportData.SetQrCode();
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("ReportShowTimer_Elapsed SetQrCode error: " + ex.ToString());
                }

                // 3. 启动打印定时器
                this.ReportPrintTimer.Enabled = true;
                this.ReportDelayCount = 0;
            });
        }

        /// <summary>
        /// 定时器触发---无限循环(非UI线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportPrintTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                // 检查图表是否刷新完成
                if(this.ReportDelayCount < 5  && (!this.FrmReport.ReportData.ChartUpdated))
                {
                    // 继续等待Chart刷新
                    this.ReportDelayCount++;
                    this.ReportPrintTimer.Enabled = true;
                    return;
                }

                // 打印报表
                string stage = (this.TestStage == TestSystemUtil.POST_TEST) ? TestSystemUtil.POST_TEST_ABBR : TestSystemUtil.PRE_TEST_ABBR;
                string sn = this.ReportCosTestData.SerialNumber;
                sn = (sn == null || sn == "") ? "" : ("_" + sn);
                string dstFilePath = this.ReportDstFolder + @"\" + this.ReportCosTestData.SerialNumber + @"_" + stage + @"_Report"  + ".xps";
                this.FrmReport.Print(dstFilePath);

                // 隐藏报表窗口
                this.Dispatcher.Invoke((Action)delegate
                {
                    this.FrmReport.Hide();
                });
            });
        }

        private void buttonLoadResultFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "csv (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.TestProcInst.LoadResultData(dialog.FileName);
                // Load Data Separately If The Timer Is Not Enabled
                if (!this.TickTimer.Enabled)
                    this.TestProcInst.RunLoadResultData();
            }


            //// Configure open file dialog box
            //var dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.DefaultExt = ".csv"; // Default file extension
            //dialog.Filter = "csv (.csv)|*.csv"; // Filter files by extension

            //// Show open file dialog box
            //bool? result = dialog.ShowDialog();

            //// Process open file dialog box results
            //if (result == true)
            //{
            //    double[] wavelength, spectrum;
            //    GaussAmpTest gaTest = new GaussAmpTest();
            //    gaTest.Do(dialog.FileName, out wavelength, out spectrum);

            //    // 光谱数据
            //    this.SpectrumSeries = new SeriesCollection();
            //    // Chart Axis
            //    double maxSpectrum = 10000;
            //    double spChartWavelengthLCL = 1000;
            //    double spChartWavelengthUCL = 700;

            //    // 数据点
            //    var values = new ChartValues<Point>();
            //    for (int j = 0; j < wavelength.Length; j++)
            //    {
            //        var point = new Point() { X = wavelength[j], Y = spectrum[j] };
            //        values.Add(point);
            //    }
            //    // 添加Sery
            //    this.SpectrumSeries.Add(new LineSeries()
            //    {
            //        Configuration = new CartesianMapper<Point>()
            //              .X(point => point.X) // Define a function that returns a value that should map to the x-axis
            //              .Y(point => point.Y), // Define a function that returns a value that should map to the y-axis
            //        Title = "Sp",
            //        Values = values,
            //        PointGeometry = null,
            //        LineSmoothness = 1
            //    });
            //    // 波长上下限
            //    spChartWavelengthLCL = wavelength.First<double>();
            //    spChartWavelengthUCL = wavelength.Last<double>();

            //    // Wavelength LCL & UCL & STEP
            //    if (spChartWavelengthLCL > spChartWavelengthUCL)
            //    {
            //        spChartWavelengthLCL = 700;
            //        spChartWavelengthUCL = 1000;
            //    }
            //    spChartWavelengthLCL = Math.Floor(spChartWavelengthLCL / 10) * 10;
            //    spChartWavelengthUCL = Math.Ceiling(spChartWavelengthUCL / 10) * 10;
            //    double spChartWavelengthStep = 5.0;
            //    while ((spChartWavelengthUCL - spChartWavelengthLCL) / spChartWavelengthStep > 10)
            //        spChartWavelengthStep *= 2;
            //    this.SpChartWavelengthUCL = spChartWavelengthUCL;
            //    this.SpChartWavelengthLCL = spChartWavelengthLCL;
            //    this.SpChartWavelengthStep = spChartWavelengthStep;

            //    // Spectrum UCL
            //    maxSpectrum = spectrum.Max();
            //    this.SpChartSpectrumUCL = ((int)(maxSpectrum / 10000) + 2) * 10000;
            //}
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            this.BatchNo = "";
            this.SerialNo = "";
            this.PartNo = "";
            this.SelScriptName = null;
            this.SpectrumSeries = null;
            this.LivSeries = null;
            this.BeamImage = null;
            this.ThermalImage = null;
            this.CosTestResultData = new ObservableCollection<DgTestResult>();
            this.CosTestResultData.Add(new DgTestResult());
            this.CurrTestResult = "";
            this.ChartVisibility = "Visible";
            this.BeamImageVisibility = "Hidden";
            this.ThermalImageVisibility = "Hidden";
            this.IsDataCleared = true;
        }

        private void buttonOpenResultFiles_Click(object sender, RoutedEventArgs e)
        {
            this.TestProcInst.OpenResultFiles();
        }

        private void ThermalImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.ChartVisibility = "Visible";
                this.BeamImageVisibility = "Hidden";
                this.ThermalImageVisibility = "Hidden";
            }
        }

        private void BeamImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.ThermalImage != null)
                {
                    this.ChartVisibility = "Hidden";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Visible";
                }
                else
                {
                    this.ChartVisibility = "Visible";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Hidden";
                }
            }
        }

        private void LivSpImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(this.BeamImage != null)
                {
                    this.ChartVisibility = "Hidden";
                    this.BeamImageVisibility = "Visible";
                    this.ThermalImageVisibility = "Hidden";
                }
                else if(this.ThermalImage != null)
                {
                    this.ChartVisibility = "Hidden";
                    this.BeamImageVisibility = "Hidden";
                    this.ThermalImageVisibility = "Visible";
                }
            }
        }
    }

    public class DgTestResult
    {
        public string Id { get; set; }

        public string ParamName { get; set; }

        public string ParamMax { get; set; }

        public string ParamMin { get; set; }

        public string TestValue { get; set; }

        public string TestResult { get; set; }

    }
}
