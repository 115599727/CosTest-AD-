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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using Wpf.CartesianChart.CustomTooltip;
using System.Collections.ObjectModel;

namespace CosTest
{
    /// <summary>
    /// ToolWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToolWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// TOOL_RPOC
        /// </summary>
        private ToolProc TOOL_RPOC;
        /// <summary>
        /// TEST_RPOC
        /// </summary>
        private TestProc TEST_RPOC;

        #region 绑定属性
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
        /// LivStop
        /// </summary>
        private double _LivStop;
        /// <summary>
        /// LivStop
        /// </summary>
        public double LivStop
        {
            get { return _LivStop; }
            set { _LivStop = value; OnPropertyChanged("LivStop"); }
        }

        /// <summary>
        /// LivStep
        /// </summary>
        private double _LivStep;
        /// <summary>
        /// LivStep
        /// </summary>
        public double LivStep
        {
            get { return _LivStep; }
            set { _LivStep = value; OnPropertyChanged("LivStep"); }
        }

        /// <summary>
        /// LivDelay
        /// </summary>
        private int _LivDelay;
        /// <summary>
        /// LivDelay
        /// </summary>
        public int LivDelay
        {
            get { return _LivDelay; }
            set { _LivDelay = value; OnPropertyChanged("LivDelay"); }
        }

        /// <summary>
        /// LivDelay
        /// </summary>
        private double _preloadingVoltage;
        /// <summary>
        /// LivDelay
        /// </summary>
        public double PreloadingVoltage
        {
            get { return _preloadingVoltage; }
            set { _preloadingVoltage = value; OnPropertyChanged("PreloadingVoltage"); }
        }

        

        /// <summary>
        /// IsCosOn
        /// </summary>
        private bool _IsCosOn;
        /// <summary>
        /// IsCosOn
        /// </summary>
        public bool IsCosOn
        {
            get { return _IsCosOn; }
            set { _IsCosOn = value; OnPropertyChanged("IsCosOn"); }
        }

        /// <summary>
        /// CosCurrent
        /// </summary>
        private double _CosCurrent;
        /// <summary>
        /// CosCurrent
        /// </summary>
        public double CosCurrent
        {
            get { return _CosCurrent; }
            set { _CosCurrent = value; OnPropertyChanged("CosCurrent"); }
        }

        /// <summary>
        /// CosVoltage
        /// </summary>
        private double _CosVoltage;
        /// <summary>
        /// CosVoltage
        /// </summary>
        public double CosVoltage
        {
            get { return _CosVoltage; }
            set { _CosVoltage = value; OnPropertyChanged("CosVoltage"); }
        }

        /// <summary>
        /// CosPower
        /// </summary>
        private double _CosPower;
        /// <summary>
        /// CosPower
        /// </summary>
        public double CosPower
        {
            get { return _CosPower; }
            set { _CosPower = value; OnPropertyChanged("CosPower"); }
        }

        /// <summary>
        /// CosCurrentSet
        /// </summary>
        private double _CosCurrentSet;
        /// <summary>
        /// CosCurrentSet
        /// </summary>
        public double CosCurrentSet
        {
            get { return _CosCurrentSet; }
            set { _CosCurrentSet = value; OnPropertyChanged("CosCurrentSet"); }
        }


        /// <summary>
        /// CosCurrentSet
        /// </summary>
        private double _CosVoltageSet;
        /// <summary>
        /// CosCurrentSet
        /// </summary>
        public double CosVoltageSet
        {
            get { return _CosVoltageSet; }
            set { _CosVoltageSet = value; OnPropertyChanged("CosVoltageSet"); }
        }

        /// <summary>
        /// StartWavelength
        /// </summary>
        private double _StartWavelength;
        /// <summary>
        /// StartWavelength
        /// </summary>
        public double StartWavelength
        {
            get { return _StartWavelength; }
            set { _StartWavelength = value; OnPropertyChanged("StartWavelength"); }
        }

        /// <summary>
        /// StopWavelength
        /// </summary>
        private double _StopWavelength;
        /// <summary>
        /// StopWavelength
        /// </summary>
        public double StopWavelength
        {
            get { return _StopWavelength; }
            set { _StopWavelength = value; OnPropertyChanged("StopWavelength"); }
        }

        /// <summary>
        /// PeakWavelength
        /// </summary>
        private double _PeakWavelength;
        /// <summary>
        /// PeakWavelength
        /// </summary>
        public double PeakWavelength
        {
            get { return _PeakWavelength; }
            set { _PeakWavelength = value; OnPropertyChanged("PeakWavelength"); }
        }

        /// <summary>
        /// FWHM
        /// </summary>
        private double _FWHM;
        /// <summary>
        /// FWHM
        /// </summary>
        public double FWHM
        {
            get { return _FWHM; }
            set { _FWHM = value; OnPropertyChanged("FWHM"); }
        }

        /// <summary>
        /// SNR
        /// </summary>
        private double _SNR;
        /// <summary>
        /// SNR
        /// </summary>
        public double SNR
        {
            get { return _SNR; }
            set { _SNR = value; OnPropertyChanged("SNR"); }
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
        /// MOVE
        /// </summary>
        private bool _MOVE;
        /// <summary>
        /// MOVE
        /// </summary>
        public bool MOVE
        {
            get { return _MOVE; }
            set { _MOVE = value; OnPropertyChanged("MOVE"); }
        }

        /// <summary>
        /// ENBL
        /// </summary>
        private bool _ENBL;
        /// <summary>
        /// ENBL
        /// </summary>
        public bool ENBL
        {
            get { return _ENBL; }
            set { _ENBL = value; OnPropertyChanged("ENBL"); }
        }

        /// <summary>
        /// ALARM
        /// </summary>
        private bool _ALARM;
        /// <summary>
        /// ALARM
        /// </summary>
        public bool ALARM
        {
            get { return _ALARM; }
            set { _ALARM = value; OnPropertyChanged("ALARM"); }
        }






        /// <summary>
        /// LOCK
        /// </summary>
        private bool _LOCK;
        /// <summary>
        /// LOCK
        /// </summary>
        public bool LOCK
        {
            get { return _LOCK; }
            set { _LOCK = value; OnPropertyChanged("LOCK"); }
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
        /// JogSpeedY
        /// </summary>
        private double _JogSpeedY;
        /// <summary>
        /// JogSpeedY
        /// </summary>
        public double JogSpeedY
        {
            get { return _JogSpeedY; }
            set { _JogSpeedY = value; OnPropertyChanged("JogSpeedY"); }
        }

        /// <summary>
        /// JogShutterUpBrush
        /// </summary>
        private string _JogShutterUpBrush;
        /// <summary>
        /// JogShutterUpBrush
        /// </summary>
        public string JogShutterUpBrush
        {
            get { return _JogShutterUpBrush; }
            set { _JogShutterUpBrush = value; OnPropertyChanged("JogShutterUpBrush"); }
        }

        /// <summary>
        /// JogShutterDownBrush
        /// </summary>
        private string _JogShutterDownBrush;
        /// <summary>
        /// JogShutterDownBrush
        /// </summary>
        public string JogShutterDownBrush
        {
            get { return _JogShutterDownBrush; }
            set { _JogShutterDownBrush = value; OnPropertyChanged("JogShutterDownBrush"); }
        }

        /// <summary>
        /// TargetPositionY
        /// </summary>
        private double _TargetPositionY;
        /// <summary>
        /// TargetPositionY
        /// </summary>
        public double TargetPositionY
        {
            get { return _TargetPositionY; }
            set { _TargetPositionY = value; OnPropertyChanged("TargetPositionY"); }
        }

        /// <summary>
        /// CosIndex
        /// </summary>
        private int _CosId;
        /// <summary>
        /// CosIndex
        /// </summary>
        public int CosId
        {
            get { return _CosId; }
            set { _CosId = value; OnPropertyChanged("CosId"); }
        }

        /// <summary>
        /// TGET
        /// </summary>
        private double _TGET;
        /// <summary>
        /// TGET
        /// </summary>
        public double TGET
        {
            get { return _TGET; }
            set { _TGET = value; OnPropertyChanged("TGET"); }
        }

        /// <summary>
        /// IGET
        /// </summary>
        private double _IGET;
        /// <summary>
        /// IGET
        /// </summary>
        public double IGET
        {
            get { return _IGET; }
            set { _IGET = value; OnPropertyChanged("IGET"); }
        }

        /// <summary>
        /// VGET
        /// </summary>
        private double _VGET;
        /// <summary>
        /// VGET
        /// </summary>
        public double VGET
        {
            get { return _VGET; }
            set { _VGET = value; OnPropertyChanged("VGET"); }
        }

        /// <summary>
        /// TOUT
        /// </summary>
        private bool _TOUT;
        /// <summary>
        /// TOUT
        /// </summary>
        public bool TOUT
        {
            get { return _TOUT; }
            set { _TOUT = value; OnPropertyChanged("TOUT"); }
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
        /// KP
        /// </summary>
        private double _KP;
        /// <summary>
        /// KP
        /// </summary>
        public double KP
        {
            get { return _KP; }
            set { _KP = value; OnPropertyChanged("KP"); }
        }

        /// <summary>
        /// KI
        /// </summary>
        private double _KI;
        /// <summary>
        /// KI
        /// </summary>
        public double KI
        {
            get { return _KI; }
            set { _KI = value; OnPropertyChanged("KI"); }
        }

        /// <summary>
        /// KD
        /// </summary>
        private double _KD;
        /// <summary>
        /// KD
        /// </summary>
        public double KD
        {
            get { return _KD; }
            set { _KD = value; OnPropertyChanged("KD"); }
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
        /// SpWavelengthBase
        /// </summary>
        private double _SpWavelengthBase;
        /// <summary>
        /// SpWavelengthBase
        /// </summary>
        public double SpWavelengthBase
        {
            get { return _SpWavelengthBase; }
            set { _SpWavelengthBase = value; OnPropertyChanged("SpWavelengthBase"); }
        }

        /// <summary>
        /// SpWavelengthBaseList
        /// </summary>
        private ObservableCollection<double> _SpWavelengthBaseList;
        /// <summary>
        /// SpWavelengthBaseList
        /// </summary>
        public ObservableCollection<double> SpWavelengthBaseList
        {
            get { return _SpWavelengthBaseList; }
            set { _SpWavelengthBaseList = value; OnPropertyChanged("SpWavelengthBaseList"); }
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
        /// 热成像可视性
        /// </summary>
        private string _ThermCamVisibility;
        /// <summary>
        /// 热成像可视性
        /// </summary>
        public string ThermCamVisibility
        {
            get { return _ThermCamVisibility; }
            set { _ThermCamVisibility = value; OnPropertyChanged("ThermCamVisibility"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 私有参数
        /// <summary>
        /// 当前点动的轴
        /// </summary>
        private string JogAxis;
        #endregion

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }
        internal ToolWindow(ToolProc toolProc, TestProc testProc)
        {
            InitializeComponent();

            // 绑定
            this.DataContext = this;

            // Window
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            //IsClosed
            this.IsClosed = false;

            // 输入Proc
            this.TOOL_RPOC = toolProc;
            this.TEST_RPOC = testProc;
            // 绑定事件
            this.TOOL_RPOC.ToolMotorDataUpdate += new ToolMotorDataHandler(this.ToolMotorDataUpdated);
            this.TOOL_RPOC.ToolTempDataUpdate += new ToolTempDataHandler(this.ToolTempDataUpdated);
            this.TOOL_RPOC.ToolLivDataUpdate += new ToolLivDataHandler(this.ToolLivDataUpdated);
            this.TOOL_RPOC.ToolSpectrumDataUpdate += new ToolSpectrumDataHandler(this.ToolSpectrumDataUpdated);
            this.TOOL_RPOC.ToolPivPtDataUpdate += new ToolPivPtDataHandler(this.ToolPivPtDataUpdated);
            this.TOOL_RPOC.ToolBeamImageUpdate += new ToolBeamImageHandler(this.ToolBeamImageUpdated);
            this.TOOL_RPOC.ToolThermalImageUpdate += new ToolThermalImageHandler(this.ToolThermalImageUpdated);
            // 基准光谱
            this.SpWavelengthBaseList = new ObservableCollection<double>();
            if (TestSystemUtil.PO_CAL_LIST != null)
            {
                foreach (StPoCal poCal in TestSystemUtil.PO_CAL_LIST)
                {
                    this.SpWavelengthBaseList.Add(poCal.WAVELENGTH_BASE);
                }
            }
            if (this.SpWavelengthBaseList.Count > 0)
                this.SpWavelengthBase = this.SpWavelengthBaseList[0];
            // 默认Jog参数
            this.JogShutterUpBrush = "Transparent";
            this.JogShutterDownBrush = "Transparent";
            this.JogSpeedY = 0.5;
            this.TargetPositionY = 0.0;
            this.CosId = 1;
            // 默认温度
            this.TSET = 25.0;
            // 默认Chart参数
            this.LivStop = 10.0;
            this.LivStep = 0.1;
            this.LivDelay = 200;
            this.StartWavelength = 400;
            this.StopWavelength = 1100;
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
                XAxisName = "电流",
                SelectionMode = TooltipSelectionMode.SharedXInSeries,
                ShowTitle = true,
                ShowSeries = true,
                BulletSize = 0,
                IsWrapped = false
            };
            this.SpToolTip = new CustomTooltip()
            {
                XAxisName = "波长",
                SelectionMode = TooltipSelectionMode.SharedXInSeries,
                ShowTitle = true,
                ShowSeries = true,
                BulletSize = 0,
                IsWrapped = false
            };
            // 热像仪使能
            this.ThermCamVisibility = StationUtil.ThermCamEnable ? "Visible" : "Collapsed";
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

        private void btnJOG_Click(object sender, RoutedEventArgs e)
        {
            // 强迫正在输入的文本框失去焦点
            this.tbPositionY.Focus();

            // 设置
            string content = ((Button)sender).Content.ToString();

            if (this.JogAxis == content)
            {
                // Stop
                switch (content)
                {
                    case "Y+":
                        this.JogShutterUpBrush = "Transparent";
                        break;
                    case "Y-":
                        this.JogShutterDownBrush = "Transparent";
                        break;
                    default:
                        break;
                }

                this.TOOL_RPOC.DoCommand(EnToolCommand.JOGOFF);
                this.JogAxis = "";
            }
            else
            {
                // Stop
                if (this.JogAxis != "")
                {
                    switch (this.JogAxis)
                    {
                        case "Z+":
                            this.JogShutterUpBrush = "Transparent";
                            break;
                        case "Z-":
                            this.JogShutterDownBrush = "Transparent";
                            break;
                        default:
                            break;
                    }
                }

                if (Math.Abs(this.JogSpeedY) > 5)
                {
                    WarnWindow.ShowMsg("马达速度不能超过5mm/s!");
                    return;
                }

                // Start Another Axis
                switch (content)
                {
                    case "Y+":
                        this.TOOL_RPOC.SetJog(Math.Abs(this.JogSpeedY));
                        this.JogShutterUpBrush = "Gold";
                        break;
                    case "Y-":
                        this.TOOL_RPOC.SetJog(-Math.Abs(this.JogSpeedY));
                        this.JogShutterDownBrush = "Gold";
                        break;
                    default:
                        break;
                }

                this.TOOL_RPOC.DoCommand(EnToolCommand.JOGON);
                this.JogAxis = content;
            }
        }

        private void btnJogStop_Click(object sender, RoutedEventArgs e)
        {
            // Stop
            this.JogShutterUpBrush = "Transparent";
            this.JogShutterDownBrush = "Transparent";
            this.TOOL_RPOC.DoCommand(EnToolCommand.JOGOFF);
            this.JogAxis = "";
        }

        private void btnJOG_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Stop moving axis
            if (this.JogAxis != "")
            {
                switch (this.JogAxis)
                {
                    case "Y+":
                        this.JogShutterUpBrush = "Transparent";
                        break;
                    case "Y-":
                        this.JogShutterDownBrush = "Transparent";
                        break;
                    default:
                        break;
                }
                // Send Command
                this.TOOL_RPOC.DoCommand(EnToolCommand.JOGOFF);
            }
        }

        private void btnJOG_MouseLeave(object sender, MouseEventArgs e)
        {
            // Stop moving axis
            if (this.JogAxis != "")
            {
                switch (this.JogAxis)
                {
                    case "Y+":
                        this.JogShutterUpBrush = "Transparent";
                        break;
                    case "Y-":
                        this.JogShutterDownBrush = "Transparent";
                        break;
                    default:
                        break;
                }
                // Send Command
                this.TOOL_RPOC.DoCommand(EnToolCommand.JOGOFF);
            }
        }

        private void btnJOG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 强迫正在输入的文本框失去焦点
            this.tbPositionY.Focus();

            // 检查反射镜
            if (this.MIRROR)
            {
                WarnWindow.ShowMsg("必须首先移开反射镜!");
                return;
            }

            // Check Speed
            if (Math.Abs(this.JogSpeedY) > 5)
            {
                WarnWindow.ShowMsg("马达速度不能超过5mm/s!");
                return;
            }

            // Stop other moving axis
            if (this.JogAxis != "")
            {
                switch (this.JogAxis)
                {
                    case "Y+":
                        this.JogShutterUpBrush = "Transparent";
                        break;
                    case "Y-":
                        this.JogShutterDownBrush = "Transparent";
                        break;
                    default:
                        break;
                }
            }

            // 设置
            string content = ((Button)sender).Content.ToString();

            // Start Another Axis
            switch (content)
            {
                case "Y+":
                    this.TOOL_RPOC.SetJog(Math.Abs(this.JogSpeedY));
                    this.JogShutterUpBrush = "Gold";
                    break;
                case "Y-":
                    this.TOOL_RPOC.SetJog(-Math.Abs(this.JogSpeedY));
                    this.JogShutterDownBrush = "Gold";
                    break;
                default:
                    break;
            }

            // Send Command
            this.TOOL_RPOC.DoCommand(EnToolCommand.JOGON);
            this.JogAxis = content;
        }

        private void buttonPowerOn_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.MotorOn();
        }

        private void buttonPowerOff_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.MotorOff();
        }

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            // 检查反射镜
            if (this.MIRROR)
            {
                WarnWindow.ShowMsg("必须首先移开反射镜!");
                return;
            }
            // 发送命令
            this.TOOL_RPOC.DoCommand(EnToolCommand.HOME);
        }

        private void buttonLock_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.LOCK);
        }

        private void buttonUnlock_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.UNLOCK);
        }

        private void buttonStopMotion_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.STOP);
        }

        private void buttonMoveToPositionZ_Click(object sender, RoutedEventArgs e)
        {
            // 强迫正在输入的文本框失去焦点
            this.tbPositionY.Focus();

            // 检查反射镜
            if (this.MIRROR)
            {
                WarnWindow.ShowMsg("必须首先移开反射镜!");
                return;
            }
            // 检查速度
            if (Math.Abs(this.JogSpeedY) > 5)
            {
                WarnWindow.ShowMsg("马达速度必须小于5mm/s!");
                return;
            }

            this.TOOL_RPOC.MoveToTargePositionZ(Math.Abs(this.JogSpeedY), this.TargetPositionY);
            this.TOOL_RPOC.DoCommand(EnToolCommand.FEED);
        }

        private void buttonMirrorIn_Click(object sender, RoutedEventArgs e)
        {
            // 检查反射镜
            if (this.MOVE)
            {
                WarnWindow.ShowMsg("马达正在运动, 无法加载反射镜!");
                return;
            }
            // 发送命令
            this.TOOL_RPOC.DoCommand(EnToolCommand.MIRRIN);
        }

        private void buttonMirrorOut_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.MIRROUT);
        }

        private void buttonJigTempSet_Click(object sender, RoutedEventArgs e)
        {
            // 强迫正在输入的文本框失去焦点
            this.tbPositionY.Focus();

            // 检查温度设置
            if (Math.Abs(this.TSET) > 125 || Math.Abs(this.TSET) < 0)
            {
                WarnWindow.ShowMsg("输入的温度超过允许范围(0到125C)!");
                return;
            }

            this.TOOL_RPOC.SetTempTarget(this.TSET);
            this.TOOL_RPOC.DoCommand(EnToolCommand.TMPSET);
        }

        private void buttonJigTempOutputOn_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.SetTempOut(true);
            this.TOOL_RPOC.DoCommand(EnToolCommand.TMPOUT);
        }

        private void buttonJigTempOutputOff_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.SetTempOut(false);
            this.TOOL_RPOC.DoCommand(EnToolCommand.TMPOUT);
        }

        private void buttonReadSpectrum_Click(object sender, RoutedEventArgs e)
        {
            this.SpectrumSeries = null;
            this.tbPositionY.Focus();//强迫正在输入的文本框失去焦点
            this.TOOL_RPOC.SetSpectrumRange(this.StartWavelength, this.StopWavelength);
            this.TOOL_RPOC.DoCommand(EnToolCommand.SPECTRUM);
        }

        private void buttonLivTest_Click(object sender, RoutedEventArgs e)
        {
            // 检查反射镜
            if (this.LOCK == false)
            {
                WarnWindow.ShowMsg("门锁未关闭, 请检查!");
                return;
            }

            // LIV扫描
            this.LivSeries = null;
            this.tbPositionY.Focus();//强迫正在输入的文本框失去焦点

            if (this.LivStop < 1 || this.LivStop > TestSystemUtil.MaxDriveCurrentA)
            {
                WarnWindow.ShowMsg("LIV结束只能介于1和" + TestSystemUtil.MaxDriveCurrentA + "A");
                return;
            }
            if (this.LivStep < 0.05 || this.LivStep > TestSystemUtil.MaxDriveCurrentA)
            {
                WarnWindow.ShowMsg("LIV结束只能介于0.05和" + TestSystemUtil.MaxDriveCurrentA + "A");
                return;
            }
            if (this.LivDelay < TestSystemUtil.MinLivDelayMilliSec || this.LivDelay > TestSystemUtil.MaxLivDelayMilliSec)
            {
                WarnWindow.ShowMsg("LIV结束只能介于" + TestSystemUtil.MinLivDelayMilliSec + "和" + TestSystemUtil.MaxLivDelayMilliSec + "ms");
                return;
            }

            if (this.TOOL_RPOC.STAT == EnToolStat.RUNNING)
            {
                WarnWindow.ShowMsg("运行中请稍后再试！");
                return;
            }

            if (this.TOOL_RPOC.SetLivParameters(this.LivStop, this.LivStep, this.LivDelay,this.PreloadingVoltage))
                this.TOOL_RPOC.DoCommand(EnToolCommand.LIV);
          
        }

        private void tbnON_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.COSOFF);
            this.IsCosOn = false;
        }

        private void tbnON_Checked(object sender, RoutedEventArgs e)
        {
            // 检查反射镜
            if (this.LOCK == false)
            {
                WarnWindow.ShowMsg("门锁未关闭, 请检查!");
                return;
            }
            // 上电
            this.TOOL_RPOC.DoCommand(EnToolCommand.COSON);
            this.IsCosOn = true;
        }

        private void btnSetCurrent_Click(object sender, RoutedEventArgs e)
        {
            // 检查反射镜
            if (this.LOCK == false)
            {
                WarnWindow.ShowMsg("门锁未关闭, 请检查!");
                return;
            }
            // 设置电流
            this.tbPositionY.Focus();//强迫正在输入的文本框失去焦点
            if(this.CosCurrentSet < 0 || this.CosCurrentSet > TestSystemUtil.MaxDriveCurrentA)
            {
                WarnWindow.ShowMsg("激光器电流只能介于0和" + TestSystemUtil.MaxDriveCurrentA + "A");
                return;
            }
            this.TOOL_RPOC.SetCosCurrent(this.CosCurrentSet);
            this.TOOL_RPOC.SetCosVoltage(this.CosVoltageSet);
            this.TOOL_RPOC.DoCommand(EnToolCommand.COSCURR);
        }
        private void buttonJigPidSet_Click(object sender, RoutedEventArgs e)
        {
            this.tbPositionY.Focus();//强迫正在输入的文本框失去焦点
            this.TOOL_RPOC.SetTempPid(this.KP, this.KI, this.KD);
            this.TOOL_RPOC.DoCommand(EnToolCommand.PID);
        }

        private void ToolMotorDataUpdated(object sender, ToolMotorDataEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.ENBL = args.ENABLE;
                this.HOME = args.HOME;
                this.MOVE = args.MOVING;
                this.POSY = Math.Round(args.POS, 2);
                this.ALARM = args.ALARM;
                this.LOCK = args.LOCK;
                this.MIRROR = args.MIRROR;
            });
        }

        private void ToolTempDataUpdated(object sender, ToolTempDataEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.TGET = Math.Round(args.TGET,2);
                this.IGET = Math.Round(args.IGET,2);
                this.VGET = Math.Round(args.VGET,2);
                this.TOUT = args.OUT;
            });
        }

        private void ToolLivDataUpdated(object sender, ToolLivDataEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                var valuesPower = new ChartValues<Point>();
                var valuesVoltage = new ChartValues<Point>();
                double maxPower = 1.0, maxVoltage = 1.0, maxCurrent = 1.0;

                for (int i = 0; i < args.LivTestData.Count; i++)
                {
                    var pointPower = new Point() { X = args.LivTestData[i].LDI, Y = args.LivTestData[i].PDI };
                    valuesPower.Add(pointPower);
                    var pointVoltage = new Point() { X = args.LivTestData[i].LDI, Y = args.LivTestData[i].LDV };
                    valuesVoltage.Add(pointVoltage);

                    maxPower = args.LivTestData[i].PDI > maxPower ? args.LivTestData[i].PDI : maxPower;
                    maxVoltage = args.LivTestData[i].LDV > maxVoltage ? args.LivTestData[i].LDV : maxVoltage;
                    maxCurrent = args.LivTestData[i].LDI > maxCurrent ? args.LivTestData[i].LDI : maxCurrent;
                }

                this.LivSeries = new SeriesCollection()
                {
                   new LineSeries()
                   {
                          Configuration = new CartesianMapper<Point>()
                            .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                            .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                            .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                            .Fill(point => Brushes.Black),
                          Title = "Power",
                          Values = valuesPower,
                          PointGeometry = null,
                          LineSmoothness = 1,
                          Stroke = Brushes.Black,
                          Fill = new SolidColorBrush(Color.FromArgb(0x3F,0xD3,0xD3,0xD3))
                       },

                       new LineSeries()
                       {
                          Configuration = new CartesianMapper<Point>()
                           .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                           .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                           .Stroke(point => Brushes.Blue) // Define a function that returns a Brush based on the current data item
                           .Fill(point => Brushes.Blue),
                          Title = "Voltage",
                          Values = valuesVoltage,
                          PointGeometry = null,
                          LineSmoothness = 1,
                          Stroke = Brushes.Blue,
                          Fill = new SolidColorBrush(Color.FromArgb(0x3F,0xAD,0xD8,0xE6)),
                          ScalesYAt = 1
                       }
                };

                // Chart Axis
                double livChartPowerUCL = 5.0;
                while (livChartPowerUCL < maxPower)
                    livChartPowerUCL += 5;
                double livChartVoltageUCL = 5.0;
                while (livChartVoltageUCL < maxVoltage)
                    livChartVoltageUCL += 1.0;
                double livChartCurrentStep = 10;
                while (maxCurrent / livChartCurrentStep < 5)
                    livChartCurrentStep /= 2.0;
                if (livChartCurrentStep < 1.0)
                    livChartCurrentStep = 1.0;

                this.LivChartPowerUCL = livChartPowerUCL;
                this.LivChartPowerStep = livChartPowerUCL / 5;
                this.LivChartVoltageUCL = livChartVoltageUCL;
                this.LivChartVoltageStep = livChartVoltageUCL / 10;
                this.LivChartCurrentStop = maxCurrent;
                this.LivChartCurrentStep = livChartCurrentStep;
            });
        }

        private void ToolSpectrumDataUpdated(object sender, ToolSpectrumDataEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                var values = new ChartValues<Point>();
                if (args.SpectrumTestData.Wavelength != null)
                {
                    for (int i = 0; i < args.SpectrumTestData.Wavelength.Length; i++)
                    {
                        var point = new Point() { X = args.SpectrumTestData.Wavelength[i], Y = args.SpectrumTestData.Spectrum[i] };
                        values.Add(point);
                    }
                }


                this.SpectrumSeries = new SeriesCollection()
                {
                   new LineSeries()
                   {
                     Configuration = new CartesianMapper<Point>()
                     .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                     .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                     .Stroke(point => Brushes.Blue) // Define a function that returns a Brush based on the current data item
                     .Fill(point => Brushes.Blue),
                      Title = "Spectrum",
                      Values = values,
                      PointGeometry = null,
                      LineSmoothness = 1
                   }
                };


                double spChartWavelengthLCL = 700;
                double spChartWavelengthUCL = 1000;
                if (args.SpectrumTestData.Wavelength != null)
                {
                    spChartWavelengthLCL = args.SpectrumTestData.Wavelength.First<double>();
                    spChartWavelengthUCL = args.SpectrumTestData.Wavelength.Last<double>();
                }
                double spChartWavelengthStep = 10.0;
                while ((spChartWavelengthUCL - spChartWavelengthLCL) / spChartWavelengthStep > 10)
                    spChartWavelengthStep += 10.0;

                this.SpChartWavelengthUCL = spChartWavelengthUCL;
                this.SpChartWavelengthLCL = spChartWavelengthLCL;
                this.SpChartWavelengthStep = spChartWavelengthStep;

                this.PeakWavelength = args.SpectrumTestData.PeakWaveLength;
                this.FWHM = args.SpectrumTestData.FHWM;
                this.SNR = args.SpectrumTestData.SNR;
            });
        }

        private void ToolPivPtDataUpdated(object sender, ToolPivPtDataEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.CosCurrent = args.PivPtData.LDI;
                this.CosVoltage = args.PivPtData.LDV;
                this.CosPower = args.PivPtData.PDI;
            });
        }

        private void ToolBeamImageUpdated(object sender, ToolBeamImageEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.BeamImage = args.BeamImage;
            });
        }

        private void ToolThermalImageUpdated(object sender, ToolThermalImageEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.ThermalImage = args.ThermalImage;
            });
        }

        private void cbSpWavelengthBaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TOOL_RPOC.SetWavelengthBase(this.SpWavelengthBase);
        }

        private void btnBeamCapture_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.BEAMSCAN);
        }

        private void btnThermalCapture_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.THERMSCAN);
        }
    }
}
