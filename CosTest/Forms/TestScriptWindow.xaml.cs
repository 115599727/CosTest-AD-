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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CosTest
{
    /// <summary>
    /// TestRountineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestScriptWindow : Window, INotifyPropertyChanged
    {
        #region Binding

        /// <summary>
        /// 当前选中的ROUTINE名称
        /// </summary>
        private string _SelRoutineName;
        /// <summary>
        /// 当前选中的ROUTINE名称
        /// </summary>
        public string SelRoutineName
        {
            get { return _SelRoutineName; }
            set { _SelRoutineName = value; OnPropertyChanged("SelRoutineName"); }
        }

        /// <summary>
        /// ROUTINE名列表
        /// </summary>
        private ObservableCollection<string> _RoutineNameList;
        /// <summary>
        /// ROUTINE名列表
        /// </summary>
        public ObservableCollection<string> RoutineNameList
        {
            get { return _RoutineNameList; }
            set { _RoutineNameList = value; OnPropertyChanged("RoutineNameList"); }
        }

        /// <summary>
        /// 新建的未保存的程序名
        /// </summary>
        private string BlankRoutineName;

        /// <summary>
        /// 是否添加到报表
        /// Yes or No
        /// </summary>
        public ObservableCollection<string> IsReportOptionList;

        #region Temperature
        /// <summary>
        /// TemperatureList
        /// </summary>
        public ObservableCollection<double> _TemperatureList;
        /// <summary>
        /// TemperatureList
        /// </summary>
        public ObservableCollection<double> TemperatureList
        {
            get { return _TemperatureList; }
            set { _TemperatureList = value; OnPropertyChanged("TemperatureList"); }
        }

        /// <summary>
        /// TemperatureNewValue
        /// </summary>
        public double _TemperatureNewValue;
        /// <summary>
        /// TemperatureNewValue
        /// </summary>
        public double TemperatureNewValue
        {
            get { return _TemperatureNewValue; }
            set { _TemperatureNewValue = value; OnPropertyChanged("TemperatureNewValue"); }
        }

        /// <summary>
        /// Temperature
        /// </summary>
        public double _Temperature;
        /// <summary>
        /// Temperature
        /// </summary>
        public double Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; OnPropertyChanged("Temperature"); }
        }
        #endregion

        #region LIV
        /// <summary>
        /// PreloadingVoltage
        /// </summary>
        private double _PreloadingVoltage;
        /// <summary>
        /// PreloadingVoltage
        /// </summary>
        public double PreloadingVoltage
        {
            get { return _PreloadingVoltage; }
            set { _PreloadingVoltage = value; OnPropertyChanged("PreloadingVoltage"); }
        }

        /// <summary>
        /// LivStart
        /// </summary>
        private double _LivStart;
        /// <summary>
        /// LivStart
        /// </summary>
        public double LivStart
        {
            get { return _LivStart; }
            set { _LivStart = value; OnPropertyChanged("LivStart"); }
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
        /// LivIsShowVF
        /// </summary>
        private bool _LivIsShowVF;
        /// <summary>
        /// LivIsShowVF
        /// </summary>
        public bool LivIsShowVF
        {
            get { return _LivIsShowVF; }
            set { _LivIsShowVF = value; OnPropertyChanged("LivIsShowVF"); }
        }

        /// <summary>
        /// LivIsShowEFF
        /// </summary>
        private bool _LivIsShowEFF;
        /// <summary>
        /// LivIsShowEFF
        /// </summary>
        public bool LivIsShowEFF
        {
            get { return _LivIsShowEFF; }
            set { _LivIsShowEFF = value; OnPropertyChanged("LivIsShowEFF"); }
        }

        /// <summary>
        /// PreloadingCurrent - 同短路判定预加设定电流(A)
        /// </summary>
        private double _PreloadingCurrent;
        /// <summary>
        /// PreloadingCurrent
        /// </summary>
        public double PreloadingCurrent
        {
            get { return _PreloadingCurrent; }
            set { _PreloadingCurrent = value; OnPropertyChanged("PreloadingCurrent"); }
        }

        /// <summary>
        /// ShortVoltThresh - 短路电压阈值(V)
        /// </summary>
        private double _ShortVoltThresh;
        /// <summary>
        /// ShortVoltThresh
        /// </summary>
        public double ShortVoltThresh
        {
            get { return _ShortVoltThresh; }
            set { _ShortVoltThresh = value; OnPropertyChanged("ShortVoltThresh"); }
        }

        /// <summary>
        /// OpenCurrentThresh - 开路电流阈值(A)
        /// </summary>
        private double _OpenCurrentThresh;
        /// <summary>
        /// OpenCurrentThresh
        /// </summary>
        public double OpenCurrentThresh
        {
            get { return _OpenCurrentThresh; }
            set { _OpenCurrentThresh = value; OnPropertyChanged("OpenCurrentThresh"); }
        }

        /// <summary>
        /// CurrentTol - 电流容差(A)
        /// </summary>
        private double _CurrentTol;
        /// <summary>
        /// CurrentTol
        /// </summary>
        public double CurrentTol
        {
            get { return _CurrentTol; }
            set { _CurrentTol = value; OnPropertyChanged("CurrentTol"); }
        }
        #endregion

        #region SP SCAN & DRIVE
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
        /// SpWavelengthScopePositive
        /// </summary>
        private double _SpWavelengthScopePositive;
        /// <summary>
        /// SpWavelengthScopePositive
        /// </summary>
        public double SpWavelengthScopePositive
        {
            get { return _SpWavelengthScopePositive; }
            set { _SpWavelengthScopePositive = value; OnPropertyChanged("SpWavelengthScopePositive"); }
        }

        /// <summary>
        /// SpWavelengthScopeNegative
        /// </summary>
        private double _SpWavelengthScopeNegative;
        /// <summary>
        /// SpWavelengthScopeNegative
        /// </summary>
        public double SpWavelengthScopeNegative
        {
            get { return _SpWavelengthScopeNegative; }
            set { _SpWavelengthScopeNegative = value; OnPropertyChanged("SpWavelengthScopeNegative"); }
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
        /// SpIsAutoIntegrationTime
        /// </summary>
        private bool _SpIsAutoIntegrationTime;
        /// <summary>
        /// SpIsAutoIntegrationTime
        /// </summary>
        public bool SpIsAutoIntegrationTime
        {
            get { return _SpIsAutoIntegrationTime; }
            set { _SpIsAutoIntegrationTime = value; OnPropertyChanged("SpIsAutoIntegrationTime"); }
        }

        /// <summary>
        /// SpIntegrationTime
        /// </summary>
        private int _SpIntegrationTime;
        /// <summary>
        /// SpIntegrationTime
        /// </summary>
        public int SpIntegrationTime
        {
            get { return _SpIntegrationTime; }
            set { _SpIntegrationTime = value; OnPropertyChanged("SpIntegrationTime"); }
        }

        /// <summary>
        /// SpBoxcarWidth
        /// </summary>
        private int _SpBoxcarWidth;
        /// <summary>
        /// SpBoxcarWidth
        /// </summary>
        public int SpBoxcarWidth
        {
            get { return _SpBoxcarWidth; }
            set { _SpBoxcarWidth = value; OnPropertyChanged("SpBoxcarWidth"); }
        }

        /// <summary>
        /// SpNumberOfScansToAverage
        /// </summary>
        private int _SpNumberOfScansToAverage;
        /// <summary>
        /// SpNumberOfScansToAverage
        /// </summary>
        public int SpNumberOfScansToAverage
        {
            get { return _SpNumberOfScansToAverage; }
            set { _SpNumberOfScansToAverage = value; OnPropertyChanged("SpNumberOfScansToAverage"); }
        }

        /// <summary>
        /// SpIsGaussFit
        /// </summary>
        private bool _SpIsGaussFit;
        /// <summary>
        /// SpIsGaussFit
        /// </summary>
        public bool SpIsGaussFit
        {
            get { return _SpIsGaussFit; }
            set { _SpIsGaussFit = value; OnPropertyChanged("SpIsGaussFit"); }
        }

        /// <summary>
        /// SpPibTotalHalfSpan
        /// </summary>
        private double _SpPibTotalHalfSpan;
        /// <summary>
        /// SpPibTotalHalfSpan
        /// </summary>
        public double SpPibTotalHalfSpan
        {
            get { return _SpPibTotalHalfSpan; }
            set { _SpPibTotalHalfSpan = value; OnPropertyChanged("SpPibTotalHalfSpan"); }
        }

        /// <summary>
        /// SpPibCenterHalfSpan
        /// </summary>
        private double _SpPibCenterHalfSpan;
        /// <summary>
        /// SpPibCenterHalfSpan
        /// </summary>
        public double SpPibCenterHalfSpan
        {
            get { return _SpPibCenterHalfSpan; }
            set { _SpPibCenterHalfSpan = value; OnPropertyChanged("SpPibCenterHalfSpan"); }
        }

        /// <summary>
        /// SpDrive
        /// </summary>
        private string _SpDrive;
        /// <summary>
        /// SpDrive
        /// </summary>
        public string SpDrive
        {
            get { return _SpDrive; }
            set { _SpDrive = value; OnPropertyChanged("SpDrive"); }
        }

        /// <summary>
        /// SpNewDriveValue
        /// </summary>
        private double _SpNewDriveValue;
        /// <summary>
        /// SpNewDriveValue
        /// </summary>
        public double SpNewDriveValue
        {
            get { return _SpNewDriveValue; }
            set { _SpNewDriveValue = value; OnPropertyChanged("SpNewDriveValue"); }
        }

        /// <summary>
        /// SpNewDriveType
        /// </summary>
        private bool _SpNewDriveType;
        /// <summary>
        /// SpNewDriveType
        /// </summary>
        public bool SpNewDriveType
        {
            get { return _SpNewDriveType; }
            set { _SpNewDriveType = value; OnPropertyChanged("SpNewDriveType"); }
        }


        /// <summary>
        /// SpDriveList
        /// </summary>
        private ObservableCollection<string> _SpDriveList;
        /// <summary>
        /// SpDriveList
        /// </summary>
        public ObservableCollection<string> SpDriveList
        {
            get { return _SpDriveList; }
            set { _SpDriveList = value; OnPropertyChanged("SpDriveList"); }
        }
        #endregion

        #region NA DRIVE
        /// <summary>
        /// NaDrive
        /// </summary>
        private string _NaDrive;
        /// <summary>
        /// NaDrive
        /// </summary>
        public string NaDrive
        {
            get { return _NaDrive; }
            set { _NaDrive = value; OnPropertyChanged("NaDrive"); }
        }

        /// <summary>
        /// NaNewDriveValue
        /// </summary>
        private double _NaNewDriveValue;
        /// <summary>
        /// NaNewDriveValue
        /// </summary>
        public double NaNewDriveValue
        {
            get { return _NaNewDriveValue; }
            set { _NaNewDriveValue = value; OnPropertyChanged("NaNewDriveValue"); }
        }

        /// <summary>
        /// NaNewDriveType
        /// </summary>
        private bool _NaNewDriveType;
        /// <summary>
        /// NaNewDriveType
        /// </summary>
        public bool NaNewDriveType
        {
            get { return _NaNewDriveType; }
            set { _NaNewDriveType = value; OnPropertyChanged("NaNewDriveType"); }
        }


        /// <summary>
        /// NaDriveList
        /// </summary>
        private ObservableCollection<string> _NaDriveList;
        /// <summary>
        /// NaDriveList
        /// </summary>
        public ObservableCollection<string> NaDriveList
        {
            get { return _NaDriveList; }
            set { _NaDriveList = value; OnPropertyChanged("NaDriveList"); }
        }
        #endregion

        #region THERMAL CAMERA DRIVE
        /// <summary>
        /// ThermalCameraDrive
        /// </summary>
        private string _ThermalCameraDrive;
        /// <summary>
        /// ThermalCameraDrive
        /// </summary>
        public string ThermalCameraDrive
        {
            get { return _ThermalCameraDrive; }
            set { _ThermalCameraDrive = value; OnPropertyChanged("ThermalCameraDrive"); }
        }

        /// <summary>
        /// ThermalCameraNewDriveValue
        /// </summary>
        private double _ThermalCameraNewDriveValue;
        /// <summary>
        /// ThermalCameraNewDriveValue
        /// </summary>
        public double ThermalCameraNewDriveValue
        {
            get { return _ThermalCameraNewDriveValue; }
            set { _ThermalCameraNewDriveValue = value; OnPropertyChanged("ThermalCameraNewDriveValue"); }
        }

        /// <summary>
        /// ThermalCameraNewDriveType
        /// </summary>
        private bool _ThermalCameraNewDriveType;
        /// <summary>
        /// ThermalCameraNewDriveType
        /// </summary>
        public bool ThermalCameraNewDriveType
        {
            get { return _ThermalCameraNewDriveType; }
            set { _ThermalCameraNewDriveType = value; OnPropertyChanged("ThermalCameraNewDriveType"); }
        }


        /// <summary>
        /// ThermalCameraDriveList
        /// </summary>
        private ObservableCollection<string> _ThermalCameraDriveList;
        /// <summary>
        /// ThermalCameraDriveList
        /// </summary>
        public ObservableCollection<string> ThermalCameraDriveList
        {
            get { return _ThermalCameraDriveList; }
            set { _ThermalCameraDriveList = value; OnPropertyChanged("ThermalCameraDriveList"); }
        }

        /// <summary>
        /// 当前选中的ThermMeas名称
        /// </summary>
        private string _ThermMeasName;
        /// <summary>
        /// 当前选中的ThermMeas名称
        /// </summary>
        public string ThermMeasName
        {
            get { return _ThermMeasName; }
            set { _ThermMeasName = value; OnPropertyChanged("ThermMeasName"); }
        }

        /// <summary>
        /// ThermMeas名列表
        /// </summary>
        private ObservableCollection<string> _ThermMeasNameList;
        /// <summary>
        /// ThermMeas名列表
        /// </summary>
        public ObservableCollection<string> ThermMeasNameList
        {
            get { return _ThermMeasNameList; }
            set { _ThermMeasNameList = value; OnPropertyChanged("ThermMeasNameList"); }
        }
        /// <summary>
        /// 如果是从Routine加载的参数，那么不更新Params
        /// </summary>
        private bool IsThermMeasNameFromRoutine;
        #endregion

        #region BEAM DRIVE
        /// <summary>
        /// BeamDrive
        /// </summary>
        private string _BeamDrive;
        /// <summary>
        /// BeamDrive
        /// </summary>
        public string BeamDrive
        {
            get { return _BeamDrive; }
            set { _BeamDrive = value; OnPropertyChanged("BeamDrive"); }
        }

        /// <summary>
        /// BeamNewDriveValue
        /// </summary>
        private double _BeamNewDriveValue;
        /// <summary>
        /// BeamNewDriveValue
        /// </summary>
        public double BeamNewDriveValue
        {
            get { return _BeamNewDriveValue; }
            set { _BeamNewDriveValue = value; OnPropertyChanged("BeamNewDriveValue"); }
        }

        /// <summary>
        /// BeamNewDriveType
        /// </summary>
        private bool _BeamNewDriveType;
        /// <summary>
        /// BeamNewDriveType
        /// </summary>
        public bool BeamNewDriveType
        {
            get { return _BeamNewDriveType; }
            set { _BeamNewDriveType = value; OnPropertyChanged("BeamNewDriveType"); }
        }


        /// <summary>
        /// BeamDriveList
        /// </summary>
        private ObservableCollection<string> _BeamDriveList;
        /// <summary>
        /// BeamDriveList
        /// </summary>
        public ObservableCollection<string> BeamDriveList
        {
            get { return _BeamDriveList; }
            set { _BeamDriveList = value; OnPropertyChanged("BeamDriveList"); }
        }
        #endregion

        #region LivTestLimitData
        /// <summary>
        /// LivTestLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _LivTestLimitData;
        /// <summary>
        /// LivTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> LivTestLimitData
        {
            get { return _LivTestLimitData; }
            set { _LivTestLimitData = value; OnPropertyChanged("LivTestLimitData"); }
        }
        /// <summary>
        /// Liv参数类型
        /// </summary>
        private ObservableCollection<string> LivParaTypeList;
        /// <summary>
        /// 驱动类型列表
        /// </summary>
        private ObservableCollection<string> DriveTypeList;
        #endregion

        #region SpTestLimitData
        /// <summary>
        /// SpTestLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _SpTestLimitData;
        /// <summary>
        /// SpTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> SpTestLimitData
        {
            get { return _SpTestLimitData; }
            set { _SpTestLimitData = value; OnPropertyChanged("SpTestLimitData"); }
        }
        /// <summary>
        /// Sp参数类型
        /// </summary>
        private ObservableCollection<string> SpParaTypeList;
        #endregion

        #region NaTestLimitData
        /// <summary>
        /// NaTestLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _NaTestLimitData;
        /// <summary>
        /// NaTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> NaTestLimitData
        {
            get { return _NaTestLimitData; }
            set { _NaTestLimitData = value; OnPropertyChanged("NaTestLimitData"); }
        }
        /// <summary>
        /// Na参数类型
        /// </summary>
        private ObservableCollection<string> NaParaTypeList;
        #endregion

        #region BeamTestLimitData
        /// <summary>
        /// BeamTestLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _BeamTestLimitData;
        /// <summary>
        /// BeamTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> BeamTestLimitData
        {
            get { return _BeamTestLimitData; }
            set { _BeamTestLimitData = value; OnPropertyChanged("BeamTestLimitData"); }
        }
        /// <summary>
        /// Beam参数类型
        /// </summary>
        private ObservableCollection<string> BeamParaTypeList;
        #endregion

        #region ThermTestLimitData
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

        /// <summary>
        /// ThermalScanTestLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _ThermTestLimitData;
        /// <summary>
        /// ThermalScanTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> ThermTestLimitData
        {
            get { return _ThermTestLimitData; }
            set { _ThermTestLimitData = value; OnPropertyChanged("ThermTestLimitData"); }
        }
        /// <summary>
        /// ThermalCamera参数类型
        /// </summary>
        private ObservableCollection<string> ThermalCameraParaTypeList;
        #endregion

        #region DeltaLimitData
        /// <summary>
        /// DeltaLimitData
        /// </summary>
        private ObservableCollection<DgTestLimit> _DeltaLimitData;
        /// <summary>
        /// NaTestLimitData
        /// </summary>
        public ObservableCollection<DgTestLimit> DeltaLimitData
        {
            get { return _DeltaLimitData; }
            set { _DeltaLimitData = value; OnPropertyChanged("DeltaLimitData"); }
        }
        /// <summary>
        /// Delta参数类型
        /// </summary>
        private ObservableCollection<string> DeltaParaTypeList;
        #endregion

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Routine Change Event
        /// </summary>
        public event RoutineUpdateHandler RoutineUpdated;
        

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        public TestScriptWindow()
        {
            InitializeComponent();

            // binding
            this.DataContext = this;

            //IsClosed
            this.IsClosed = false;

            // Drive
            this.DriveTypeList = new ObservableCollection<string>();
            this.DriveTypeList.Add("Po(W)");
            this.DriveTypeList.Add("If(A)");

            // LIV
            this.LivTestLimitData = new ObservableCollection<DgTestLimit>();
            this.LivParaTypeList = new ObservableCollection<string>();
            this.LivParaTypeList.Add("Ith(A)");
            this.LivParaTypeList.Add("Slope(W/A)");
            this.LivParaTypeList.Add("Res(Ω)");
            this.LivParaTypeList.Add("Po(W)");
            this.LivParaTypeList.Add("If(A)");
            this.LivParaTypeList.Add("Vf(V)");
            this.LivParaTypeList.Add("Eff(%)");

            // SP
            this.SpTestLimitData = new ObservableCollection<DgTestLimit>();
            this.SpParaTypeList = new ObservableCollection<string>();
            this.SpParaTypeList.Add("Centroid(nm)");
            this.SpParaTypeList.Add("Peak(nm)");
            this.SpParaTypeList.Add("Center(nm)");
            this.SpParaTypeList.Add("FWHM(nm)");
            this.SpParaTypeList.Add("SNR(dB)");
            this.SpParaTypeList.Add("PIB(%)");

            // NA
            this.NaTestLimitData = new ObservableCollection<DgTestLimit>();
            this.NaParaTypeList = new ObservableCollection<string>();
            this.NaParaTypeList.Add("N.A.");

            // BEAM
            this.BeamTestLimitData = new ObservableCollection<DgTestLimit>();
            this.BeamParaTypeList = new ObservableCollection<string>();

            // THERMAL CAMERA
            this.ThermTestLimitData = new ObservableCollection<DgTestLimit>();
            this.ThermalCameraParaTypeList = new ObservableCollection<string>();
            this.ThermCamVisibility = StationUtil.ThermCamEnable ? "Visible" : "Collapsed";

            // DELTA
            this.DeltaLimitData = new ObservableCollection<DgTestLimit>();
            this.DeltaParaTypeList = new ObservableCollection<string>();
            this.DeltaParaTypeList.Add("");
            this.DeltaParaTypeList.Add("%");

            // IsReportList
            this.IsReportOptionList = new ObservableCollection<string>();
            this.IsReportOptionList.Add("Yes");
            this.IsReportOptionList.Add("No");

            // 程序列表
            this.RoutineNameList = new ObservableCollection<string>(TestScriptUtil.GetRoutineNameList());
            // 当前程序
            if (this.RoutineNameList.Count > 0)
                this.SelRoutineName = this.RoutineNameList[0];

            // 基准光谱
            this.SpWavelengthBaseList = new ObservableCollection<double>();
            if(TestSystemUtil.PO_CAL_LIST != null)
            {
                foreach(StPoCal poCal in TestSystemUtil.PO_CAL_LIST)
                {
                    this.SpWavelengthBaseList.Add(poCal.WAVELENGTH_BASE);
                }
            }

            // 热像扫描区域
            this.ThermMeasNameList = new ObservableCollection<string>(ThermMeasUtil.GetAreaNameList());
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


        private void cbRoutineNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 检查当前程序名称
            if (this.SelRoutineName == null || this.SelRoutineName == "")
                return;

            //删除未保存的空模板
            if (this.BlankRoutineName != null && this.BlankRoutineName != this.SelRoutineName)
            {
                // 更新列表
                string selRoutineName = this.SelRoutineName;
                this.RoutineNameList.Remove(this.BlankRoutineName);
                this.RoutineNameList = null;
                this.RoutineNameList = new ObservableCollection<string>(TestScriptUtil.GetRoutineNameList());
                this.BlankRoutineName = null;
                this.SelRoutineName = selRoutineName;
            }

            // 更新
            if (TestScriptUtil.ROUTINE_DICT.ContainsKey(this.SelRoutineName))
                this.UpdateRoutineBinding(TestScriptUtil.ROUTINE_DICT[this.SelRoutineName]);
            else
                this.UpdateRoutineBinding(new StRoutine());//使用空模板
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            // 检查当前程序名称
            if (this.SelRoutineName == null || this.SelRoutineName == "")
            {
                WarnWindow.ShowMsg("当前程序名为空！");
                return;
            }

            // 检查程序参数
            StRoutine routine = new StRoutine();
            if (!this.ValidateRoutineInput(ref routine))
                return;

            // 保存到内存
            if (TestScriptUtil.ROUTINE_DICT.ContainsKey(this.SelRoutineName))
                TestScriptUtil.ROUTINE_DICT[this.SelRoutineName] = routine;
            else
                TestScriptUtil.ROUTINE_DICT.Add(this.SelRoutineName, routine);
            // 保存到文件
            TestScriptUtil.SaveConfig();

            //清除空标志
            if (this.BlankRoutineName != null)
            {
                this.BlankRoutineName = null;
            }

            // 通知主界面更新列表
            if (this.RoutineUpdated != null)
                this.RoutineUpdated(this, new RoutineUpdateEventArgs());
        }

        private void buttonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            //检查当前程序名称
            if (this.SelRoutineName == null || this.SelRoutineName == "")
            {
                WarnWindow.ShowMsg("当前程序名为空！");
                return;
            }

            //弹出窗口, 输入新程序名
            EditScriptWindow esWin = new EditScriptWindow(this.RoutineNameList.ToList(), this.SelRoutineName, EnRoutineAct.SAVEAS);
            esWin.RoutineEvent += new RoutineHandler(this.RoutineActCallback);
            esWin.Show();
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            //弹出窗口, 输入新程序名
            EditScriptWindow esWin = new EditScriptWindow(this.RoutineNameList.ToList(), this.SelRoutineName, EnRoutineAct.NEW);
            esWin.RoutineEvent += new RoutineHandler(this.RoutineActCallback);
            esWin.Show();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            //检查当前程序名称
            if (this.SelRoutineName == null || this.SelRoutineName == "")
            {
                WarnWindow.ShowMsg("当前程序名为空, 无法删除！");
                return;
            }

            //弹出窗口, 输入新程序名
            EditScriptWindow esWin = new EditScriptWindow(this.RoutineNameList.ToList(), this.SelRoutineName, EnRoutineAct.DELETE);
            esWin.RoutineEvent += new RoutineHandler(this.RoutineActCallback);
            esWin.Show();
        }

        private void buttonRename_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            //检查当前程序名称
            if (this.SelRoutineName == null || this.SelRoutineName == "")
            {
                WarnWindow.ShowMsg("当前程序名为空, 无法重命名！");
                return;
            }

            //弹出窗口, 输入新程序名
            EditScriptWindow esWin = new EditScriptWindow(this.RoutineNameList.ToList(), this.SelRoutineName, EnRoutineAct.RENAME);
            esWin.RoutineEvent += new RoutineHandler(this.RoutineActCallback);
            esWin.Show();
        }

        /// <summary>
        /// 程序事件回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RoutineActCallback(object sender, RoutineEventArgs args)
        {
            StRoutine routine = new StRoutine();
            List<string> nameList = new List<string>();

            switch (args.ACT)
            {
                case EnRoutineAct.SAVE:
                    // 保存
                    // BASE程序名不是当前程序
                    if (args.BaseName != this.SelRoutineName)
                    {
                        WarnWindow.ShowMsg(args.BaseName + " : 已经不是当前程序, 无法完成保存操作!");
                        return;
                    }
                    //检查程序参数
                    if (!this.ValidateRoutineInput(ref routine))
                        return;
                    // 保存到内存
                    if (TestScriptUtil.ROUTINE_DICT.ContainsKey(this.SelRoutineName))
                        TestScriptUtil.ROUTINE_DICT[this.SelRoutineName] = routine;
                    else
                        TestScriptUtil.ROUTINE_DICT.Add(this.SelRoutineName, routine);
                    // 保存到文件
                    TestScriptUtil.SaveConfig();
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new RoutineUpdateEventArgs());
                    break;

                case EnRoutineAct.SAVEAS:
                    // 另存为
                    if (!TestScriptUtil.ROUTINE_DICT.ContainsKey(args.BaseName))
                    {
                        WarnWindow.ShowMsg(args.BaseName + " : 已经不存在, 无法完成另存为操作!");
                        return;
                    }
                    else
                    {
                        routine = TestScriptUtil.ROUTINE_DICT[args.BaseName];
                    }
                    // 保存到内存
                    if (TestScriptUtil.ROUTINE_DICT.ContainsKey(args.NewName))
                        TestScriptUtil.ROUTINE_DICT[args.NewName] = routine;
                    else
                        TestScriptUtil.ROUTINE_DICT.Add(args.NewName, routine);
                    // 保存到文件
                    TestScriptUtil.SaveConfig();
                    // 更新到列表中
                    this.RoutineNameList.Add(args.NewName);
                    this.SelRoutineName = args.NewName;
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new RoutineUpdateEventArgs());
                    break;

                case EnRoutineAct.NEW:
                    // 更新到列表中
                    this.RoutineNameList.Add(args.NewName);
                    this.SelRoutineName = args.NewName;
                    // 标记当前为新建空模板
                    this.BlankRoutineName = args.NewName;
                    // update window from routine
                    break;

                case EnRoutineAct.DELETE:
                    // 删除
                    if (TestScriptUtil.ROUTINE_DICT.ContainsKey(args.BaseName))
                        TestScriptUtil.ROUTINE_DICT.Remove(args.BaseName);
                    // 保存到文件
                    TestScriptUtil.SaveConfig();
                    // 更新列表
                    this.RoutineNameList.Remove(args.BaseName);
                    // 更新列表的当前项
                    if (this.RoutineNameList.Count > 0)
                        this.SelRoutineName = this.RoutineNameList[0];
                    else
                        this.SelRoutineName = "";
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new RoutineUpdateEventArgs());
                    break;

                case EnRoutineAct.RENAME:
                    // 重命名
                    if (TestScriptUtil.ROUTINE_DICT.ContainsKey(args.BaseName))
                    {
                        // 保存到内存
                        routine = TestScriptUtil.ROUTINE_DICT[args.BaseName];
                        TestScriptUtil.ROUTINE_DICT.Add(args.NewName, routine);
                        TestScriptUtil.ROUTINE_DICT.Remove(args.BaseName);
                        // 保存到文件
                        TestScriptUtil.SaveConfig();
                    }
                    // 更新列表
                    for (int i = 0; i < this.RoutineNameList.Count; i++)
                    {
                        if (this.RoutineNameList[i] == args.BaseName)
                        {
                            this.RoutineNameList[i] = args.NewName;
                            break;
                        }
                    }
                    this.SelRoutineName = args.NewName;
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new RoutineUpdateEventArgs());
                    break;
            }
        }

        /// <summary>
        /// 验证脚本数据是否正确
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        private bool ValidateRoutineInput(ref StRoutine routine)
        {
            // LivStart & LivStop
            if (this.LivStop <= this.LivStart)
            {
                WarnWindow.ShowMsg("LIV终止电流必须大于LIV起始电流");
                return false;
            }

            // PreloadingVoltage
            if (ValidateRoutineInputString("预加载电压(V)", this.PreloadingVoltage, 0.0, TestSystemUtil.MaxDriveVoltageV, ref routine.LivScanCfg.PreloadingVoltage) == false)
                return false;

            // LivStart
            if (ValidateRoutineInputString("LIV起始电流(A)", this.LivStart, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.Start) == false)
                return false;

            // LivStop
            if (ValidateRoutineInputString("LIV终止电流(A)", this.LivStop, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.Stop) == false)
                return false;

            // LivStep
            if (ValidateRoutineInputString("LIV步进电流(A)", this.LivStep, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.Step) == false)
                return false;

            // LivDelay
            if (ValidateRoutineInputString("LIV步进时间(ms)", this.LivDelay, 0.0, 65535, ref routine.LivScanCfg.Delay) == false)
                return false;

            routine.LivScanCfg.IsShowVF = this.LivIsShowVF;
            routine.LivScanCfg.IsShowEFF = this.LivIsShowEFF;

            // PreloadingCurrent
            if (ValidateRoutineInputString("预加载电流(A)", this.PreloadingCurrent, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.PreloadingCurrent) == false)
                return false;

            // ShortVoltThresh
            if (ValidateRoutineInputString("短路电压阈值(V)", this.ShortVoltThresh, 0.0, TestSystemUtil.MaxDriveVoltageV, ref routine.LivScanCfg.ShortVoltThresh) == false)
                return false;

            // OpenCurrentThresh
            if (ValidateRoutineInputString("开路电流阈值(A)", this.OpenCurrentThresh, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.OpenCurrentThresh) == false)
                return false;

            // CurrentTol
            if (ValidateRoutineInputString("电流容差(A)", this.CurrentTol, 0.0, TestSystemUtil.MaxDriveCurrentA, ref routine.LivScanCfg.CurrentTol) == false)
                return false;

            // SpStartWavelength
            if (ValidateRoutineInputString("基准波长(nm)", this.SpWavelengthBase, 720, 990, ref routine.SpScanCfg.WavelengthBase) == false)
                return false;

            // SpWavelengthScopePositive
            if (ValidateRoutineInputString("扫描范围+(nm)", this.SpWavelengthScopePositive, 0, 300, ref routine.SpScanCfg.WavelengthScopePositive) == false)
                return false;

            // SpWavelengthScopePositive
            if (ValidateRoutineInputString("扫描范围-(nm)", this.SpWavelengthScopeNegative, 0, 300, ref routine.SpScanCfg.WavelengthScopeNegative) == false)
                return false;

            // SpIsAutoIntegrationTime
            routine.SpScanCfg.IsAutoIntegrationTime = this.SpIsAutoIntegrationTime;

            // SpIntegrationTime
            if (ValidateRoutineInputString("积分时间(ms)", this.SpIntegrationTime, 0.0, 9999.0, ref routine.SpScanCfg.IntegrationTime) == false)
                return false;

            // SpBoxcarWidth
            if (ValidateRoutineInputString("平滑点数", this.SpBoxcarWidth, 0.0, 9999.0, ref routine.SpScanCfg.BoxcarWidth) == false)
                return false;

            // NumberOfScansToAverage
            if (ValidateRoutineInputString("扫描次数", this.SpNumberOfScansToAverage, 0.0, 9999.0, ref routine.SpScanCfg.NumberOfScansToAverage) == false)
                return false;

            // SpPibTotalHalfSpan
            if (ValidateRoutineInputString("PIB总半宽", this.SpPibTotalHalfSpan, 0.0, 9999.0, ref routine.SpScanCfg.PibTotalHalfSpan) == false)
                return false;

            // NumberOfScansToAverage
            if (ValidateRoutineInputString("PIB中心半宽", this.SpPibCenterHalfSpan, 0.0, 9999.0, ref routine.SpScanCfg.PibCenterHalfSpan) == false)
                return false;

            // SpIsGaussFit
            routine.SpScanCfg.IsGaussFit = this.SpIsGaussFit;

            // Check Repeated Paramters - Liv
            for (int i = 0; i < this.LivTestLimitData.Count; i++)
            {
                for (int j = i + 1; j < this.LivTestLimitData.Count; j++)
                {
                    if(this.LivTestLimitData[i].ParamName == this.LivTestLimitData[j].ParamName)
                    {
                        WarnWindow.ShowMsg("重复的LIV扫描数据参数名: <序号" + this.LivTestLimitData[i].Id.ToString() + " " + this.LivTestLimitData[i].ParamName + "> 和 <"
                            + this.LivTestLimitData[j].Id.ToString() + " " + this.LivTestLimitData[j].ParamName + ">");
                        return false;
                    }
                }
            }
            // Check Repeated Paramters - Sp
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
            {
                for (int j = i + 1; j < this.SpTestLimitData.Count; j++)
                {
                    if (this.SpTestLimitData[i].ParamName == this.SpTestLimitData[j].ParamName)
                    {
                        WarnWindow.ShowMsg("重复的光谱扫描数据参数名: <序号" + this.SpTestLimitData[i].Id.ToString() + " " + this.SpTestLimitData[i].ParamName + "> 和 <"
                            + this.SpTestLimitData[j].Id.ToString() + " " + this.SpTestLimitData[j].ParamName + ">");
                        return false;
                    }
                }
            }

            // Temperature
            if(this.TemperatureList == null || this.TemperatureList.Count == 0)
            {
                WarnWindow.ShowMsg("未设置测试温度");
                return false;
            }
            else
            {
                foreach(double temp in this.TemperatureList)
                {
                    double tmp = 0;
                    if (ValidateRoutineInputString("测试温度 " + temp + "degC", temp, -10.0, 125, ref tmp) == false)
                        return false;
                }
            }
            routine.TempDrvs = new List<double>(this.TemperatureList);

            // SpDriveCondition
            routine.SpDrvs = new List<StCosParam>();
            foreach(string spDrive in this.SpDriveList)
            {
                if(spDrive.EndsWith("A"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(spDrive.Substring(0, spDrive.Length - 1));
                    drive.DriveType = "If(A)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("光谱测试驱动电流 " + spDrive, drive.DriveValue, 0.0, TestSystemUtil.MaxDriveCurrentA, ref tmp) == false)
                        return false;
                    routine.SpDrvs.Add(drive);
                }
                else if (spDrive.EndsWith("W"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(spDrive.Substring(0, spDrive.Length - 1));
                    drive.DriveType = "Po(W)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("光谱测试驱动功率 " + spDrive, drive.DriveValue, 0.0, 1000.0, ref tmp) == false)
                        return false;
                    routine.SpDrvs.Add(drive);
                }
            }

            // NaDriveCondition
            routine.NaDrvs = new List<StCosParam>();
            foreach (string naDrive in this.NaDriveList)
            {
                if (naDrive.EndsWith("A"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(naDrive.Substring(0, naDrive.Length - 1));
                    drive.DriveType = "If(A)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("数值孔径测试驱动电流 " + naDrive, drive.DriveValue, 0.0, TestSystemUtil.MaxDriveCurrentA, ref tmp) == false)
                        return false;
                    routine.NaDrvs.Add(drive);
                }
                else if (naDrive.EndsWith("W"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(naDrive.Substring(0, naDrive.Length - 1));
                    drive.DriveType = "Po(W)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("数值孔径测试驱动功率 " + naDrive, drive.DriveValue, 0.0, 1000.0, ref tmp) == false)
                        return false;
                    routine.NaDrvs.Add(drive);
                }
            }

            // BeamDriveCondition
            routine.BeamCamDrvs = new List<StCosParam>();
            foreach (string BeamDrive in this.BeamDriveList)
            {
                if (BeamDrive.EndsWith("A"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(BeamDrive.Substring(0, BeamDrive.Length - 1));
                    drive.DriveType = "If(A)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("数值孔径测试驱动电流 " + BeamDrive, drive.DriveValue, 0.0, TestSystemUtil.MaxDriveCurrentA, ref tmp) == false)
                        return false;
                    routine.BeamCamDrvs.Add(drive);
                }
                else if (BeamDrive.EndsWith("W"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(BeamDrive.Substring(0, BeamDrive.Length - 1));
                    drive.DriveType = "Po(W)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("数值孔径测试驱动功率 " + BeamDrive, drive.DriveValue, 0.0, 1000.0, ref tmp) == false)
                        return false;
                    routine.BeamCamDrvs.Add(drive);
                }
            }

            // ThermalCameraDriveCondition
            routine.ThermMeasName = this.ThermMeasName;
            routine.ThermCamDrvs = new List<StCosParam>();
            foreach (string ThermalCameraDrive in this.ThermalCameraDriveList)
            {
                if (ThermalCameraDrive.EndsWith("A"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(ThermalCameraDrive.Substring(0, ThermalCameraDrive.Length - 1));
                    drive.DriveType = "If(A)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("热像扫描测试驱动电流 " + ThermalCameraDrive, drive.DriveValue, 0.0, TestSystemUtil.MaxDriveCurrentA, ref tmp) == false)
                        return false;
                    routine.ThermCamDrvs.Add(drive);
                }
                else if (ThermalCameraDrive.EndsWith("W"))
                {
                    StCosParam drive = new StCosParam();
                    drive.DriveValue = Convert.ToDouble(ThermalCameraDrive.Substring(0, ThermalCameraDrive.Length - 1));
                    drive.DriveType = "Po(W)";
                    double tmp = 0;
                    if (ValidateRoutineInputString("热像扫描测试驱动功率 " + ThermalCameraDrive, drive.DriveValue, 0.0, 1000.0, ref tmp) == false)
                        return false;
                    routine.ThermCamDrvs.Add(drive);
                }
            }

            // PivParams
            routine.LivTestParams = new List<StCosParam>();
            for(int i=0;i< this.LivTestLimitData.Count;i++)
            {
                StCosParam param = new StCosParam()
                {
                    Name = this.LivTestLimitData[i].ParamName,
                    ParamType = this.LivTestLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.LivTestLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.LivTestLimitData[i].ParamMax),
                    DriveType = this.LivTestLimitData[i].DriveType,
                    DriveValue = Convert.ToDouble(this.LivTestLimitData[i].DriveValue),
                    DriveTemp = Convert.ToDouble(this.LivTestLimitData[i].DriveTemp),
                    IsReport = this.LivTestLimitData[i].IsReport == "Yes"
                };
                routine.LivTestParams.Add(param);
            }

            // SpParams
            routine.SpTestParams = new List<StCosParam>();
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
            {
                StCosParam param = new StCosParam()
                {
                    Name = this.SpTestLimitData[i].ParamName,
                    ParamType = this.SpTestLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.SpTestLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.SpTestLimitData[i].ParamMax),
                    DriveType = this.SpTestLimitData[i].DriveType,
                    DriveValue = Convert.ToDouble(this.SpTestLimitData[i].DriveValue),
                    DriveTemp = Convert.ToDouble(this.SpTestLimitData[i].DriveTemp),
                    IsReport = this.SpTestLimitData[i].IsReport == "Yes"
                };
                routine.SpTestParams.Add(param);
            }

            // NaParams
            routine.NaTestParams = new List<StCosParam>();
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
            {
                StCosParam param = new StCosParam()
                {
                    Name = this.NaTestLimitData[i].ParamName,
                    ParamType = this.NaTestLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.NaTestLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.NaTestLimitData[i].ParamMax),
                    DriveType = this.NaTestLimitData[i].DriveType,
                    DriveValue = Convert.ToDouble(this.NaTestLimitData[i].DriveValue),
                    DriveTemp = Convert.ToDouble(this.NaTestLimitData[i].DriveTemp),
                    IsReport = this.NaTestLimitData[i].IsReport == "Yes"
                };
                routine.NaTestParams.Add(param);
            }

            // BeamParams
            routine.BeamTestParams = new List<StCosParam>();
            for (int i = 0; i < this.BeamTestLimitData.Count; i++)
            {
                StCosParam param = new StCosParam()
                {
                    Name = this.BeamTestLimitData[i].ParamName,
                    ParamType = this.BeamTestLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.BeamTestLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.BeamTestLimitData[i].ParamMax),
                    DriveType = this.BeamTestLimitData[i].DriveType,
                    DriveValue = Convert.ToDouble(this.BeamTestLimitData[i].DriveValue),
                    DriveTemp = Convert.ToDouble(this.BeamTestLimitData[i].DriveTemp),
                    IsReport = this.BeamTestLimitData[i].IsReport == "Yes"
                };
                routine.BeamTestParams.Add(param);
            }

            // ThermalCameraParams
            routine.ThermTestParams = new List<StCosParam>();
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
            {
                StCosParam param = new StCosParam()
                {
                    Name = this.ThermTestLimitData[i].ParamName,
                    ParamType = this.ThermTestLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.ThermTestLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.ThermTestLimitData[i].ParamMax),
                    DriveType = this.ThermTestLimitData[i].DriveType,
                    DriveValue = Convert.ToDouble(this.ThermTestLimitData[i].DriveValue),
                    DriveTemp = Convert.ToDouble(this.ThermTestLimitData[i].DriveTemp),
                    IsReport = this.ThermTestLimitData[i].IsReport == "Yes"
                };
                routine.ThermTestParams.Add(param);
            }

            // DeltaParams
            routine.DeltaParams = new List<StCosParam>();
            for (int i = 0; i < this.DeltaLimitData.Count; i++)
            {
                // 检查参数是否存在
                bool existed = false;
                string paramName = this.DeltaLimitData[i].ParamName;
                foreach(DgTestLimit lmt in this.LivTestLimitData)
                {
                    if(lmt.ParamName == paramName)
                    {
                        existed = true;
                        break;
                    }
                }
                foreach (DgTestLimit lmt in this.SpTestLimitData)
                {
                    if (lmt.ParamName == paramName)
                    {
                        existed = true;
                        break;
                    }
                }
                foreach (DgTestLimit lmt in this.NaTestLimitData)
                {
                    if (lmt.ParamName == paramName)
                    {
                        existed = true;
                        break;
                    }
                }
                foreach (DgTestLimit lmt in this.BeamTestLimitData)
                {
                    if (lmt.ParamName == paramName)
                    {
                        existed = true;
                        break;
                    }
                }
                foreach (DgTestLimit lmt in this.ThermTestLimitData)
                {
                    if (lmt.ParamName == paramName)
                    {
                        existed = true;
                        break;
                    }
                }
                if (existed == false)
                {
                    WarnWindow.ShowMsg("DELTA参数: " + paramName + " 尚未配置, 请检查!");
                    return false;
                }

                // 添加参数
                StCosParam param = new StCosParam()
                {
                    Name = this.DeltaLimitData[i].ParamName,
                    ParamType = this.DeltaLimitData[i].ParamType,
                    ParamLCL = Convert.ToDouble(this.DeltaLimitData[i].ParamMin),
                    ParamUCL = Convert.ToDouble(this.DeltaLimitData[i].ParamMax),
                    IsReport = this.DeltaLimitData[i].IsReport == "Yes"
                };
                routine.DeltaParams.Add(param);
            }

            return true;
        }

        /// <summary>
        /// 检查输入参数是否为数字且在规定范围以内
        /// </summary>
        /// <param name="param"></param>
        /// <param name="input"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidateRoutineInputString(string param, double input, double min, double max, ref double value)
        {
            try
            {
                if (input > max)
                {
                    WarnWindow.ShowMsg(param + " : 高于最大值限制 " + max.ToString() + " !");
                    return false;
                }
                else if (input < min)
                {
                    WarnWindow.ShowMsg(param + " : 低于最小值限制 " + min.ToString() + " !");
                    return false;
                }
                else
                {
                    value = input;
                }
            }
            catch
            {
                WarnWindow.ShowMsg(param + " : 输入格式不正确 !");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查输入参数是否为数字且在规定范围以内
        /// </summary>
        /// <param name="param"></param>
        /// <param name="input"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidateRoutineInputString(string param, int input, double min, double max, ref int value)
        {
            try
            {
                if (input > max)
                {
                    WarnWindow.ShowMsg(param + " : 高于最大值限制 " + max.ToString() + " !");
                    return false;
                }
                else if (input < min)
                {
                    WarnWindow.ShowMsg(param + " : 低于于最小值限制 " + min.ToString() + " !");
                    return false;
                }
                else
                {
                    value = input;
                }
            }
            catch
            {
                WarnWindow.ShowMsg(param + " : 输入格式不正确 !");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 检查输入参数是否为数字且在规定范围以内
        /// </summary>
        /// <param name="param"></param>
        /// <param name="input"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidateRoutineInputString(string param, uint input, double min, double max, ref uint value)
        {
            try
            {
                if (input > max)
                {
                    WarnWindow.ShowMsg(param + " : 高于最大值限制 " + max.ToString() + " !");
                    return false;
                }
                else if (input < min)
                {
                    WarnWindow.ShowMsg(param + " : 低于于最小值限制 " + min.ToString() + " !");
                    return false;
                }
                else
                {
                    value = input;
                }
            }
            catch
            {
                WarnWindow.ShowMsg(param + " : 输入格式不正确 !");
                return false;
            }
            return true;
        }


        private void UpdateRoutineBinding(StRoutine routine)
        {
            // Temperature
            if (routine.TempDrvs == null)
                this.TemperatureList = new ObservableCollection<double>();
            else
            {
                routine.TempDrvs.Sort();
                this.TemperatureList = new ObservableCollection<double>(routine.TempDrvs);
            }
            if (this.TemperatureList.Count > 0)
                this.Temperature = this.TemperatureList[0];

            // LIV
            this.PreloadingVoltage = routine.LivScanCfg.PreloadingVoltage;
            this.LivStart = routine.LivScanCfg.Start;
            this.LivStop = routine.LivScanCfg.Stop;
            this.LivStep = routine.LivScanCfg.Step;
            this.LivDelay = routine.LivScanCfg.Delay;
            this.LivIsShowVF = routine.LivScanCfg.IsShowVF;
            this.LivIsShowEFF = routine.LivScanCfg.IsShowEFF;
            this.PreloadingCurrent = routine.LivScanCfg.PreloadingCurrent;
            this.ShortVoltThresh = routine.LivScanCfg.ShortVoltThresh;
            this.OpenCurrentThresh = routine.LivScanCfg.OpenCurrentThresh;
            this.CurrentTol = routine.LivScanCfg.CurrentTol;

            // SP SCAN
            if (this.SpWavelengthBaseList.Contains(routine.SpScanCfg.WavelengthBase))
                this.SpWavelengthBase = routine.SpScanCfg.WavelengthBase;
            else
                this.SpWavelengthBase = this.SpWavelengthBaseList[0];
            this.SpWavelengthScopePositive = routine.SpScanCfg.WavelengthScopePositive;
            this.SpWavelengthScopeNegative = routine.SpScanCfg.WavelengthScopeNegative;
            this.SpIsAutoIntegrationTime = routine.SpScanCfg.IsAutoIntegrationTime;
            this.SpIntegrationTime = routine.SpScanCfg.IntegrationTime;
            this.SpBoxcarWidth = routine.SpScanCfg.BoxcarWidth;
            this.SpNumberOfScansToAverage = routine.SpScanCfg.NumberOfScansToAverage;
            this.SpPibTotalHalfSpan = routine.SpScanCfg.PibTotalHalfSpan;
            this.SpPibCenterHalfSpan = routine.SpScanCfg.PibCenterHalfSpan;
            this.SpIsGaussFit = routine.SpScanCfg.IsGaussFit;

            // SP DRIVE
            this.SpDriveList = new ObservableCollection<string>();
            if(routine.SpDrvs != null)
            {
                foreach (StCosParam drive in routine.SpDrvs)
                {
                    if (drive.DriveType == "If(A)")
                    {
                        this.SpDriveList.Add(drive.DriveValue.ToString() + "A");
                    }
                    else if (drive.DriveType == "Po(W)")
                    {
                        this.SpDriveList.Add(drive.DriveValue.ToString() + "W");
                    }
                }
            }
            if (this.SpDriveList.Count > 0)
                this.SpDrive = this.SpDriveList[0];
            else
                this.SpDrive = null;

            // NA DRIVE
            this.NaDriveList = new ObservableCollection<string>();
            if (routine.NaDrvs != null)
            {
                foreach (StCosParam drive in routine.NaDrvs)
                {
                    if (drive.DriveType == "If(A)")
                    {
                        this.NaDriveList.Add(drive.DriveValue.ToString() + "A");
                    }
                    else if (drive.DriveType == "Po(W)")
                    {
                        this.NaDriveList.Add(drive.DriveValue.ToString() + "W");
                    }
                }
            }
            if (this.NaDriveList.Count > 0)
                this.NaDrive = this.NaDriveList[0];
            else
                this.NaDrive = null;

            // BEAM DRIVE
            this.BeamDriveList = new ObservableCollection<string>();
            if (routine.BeamCamDrvs != null)
            {
                foreach (StCosParam drive in routine.BeamCamDrvs)
                {
                    if (drive.DriveType == "If(A)")
                    {
                        this.BeamDriveList.Add(drive.DriveValue.ToString() + "A");
                    }
                    else if (drive.DriveType == "Po(W)")
                    {
                        this.BeamDriveList.Add(drive.DriveValue.ToString() + "W");
                    }
                }
            }
            if (this.BeamDriveList.Count > 0)
                this.BeamDrive = this.BeamDriveList[0];
            else
                this.BeamDrive = null;

            // THERMAL CAMERA DRIVE
            this.ThermalCameraDriveList = new ObservableCollection<string>();
            if (routine.ThermCamDrvs != null)
            {
                foreach (StCosParam drive in routine.ThermCamDrvs)
                {
                    if (drive.DriveType == "If(A)")
                    {
                        this.ThermalCameraDriveList.Add(drive.DriveValue.ToString() + "A");
                    }
                    else if (drive.DriveType == "Po(W)")
                    {
                        this.ThermalCameraDriveList.Add(drive.DriveValue.ToString() + "W");
                    }
                }
            }
            if (this.ThermalCameraDriveList.Count > 0)
                this.ThermalCameraDrive = this.ThermalCameraDriveList[0];
            else
                this.ThermalCameraDrive = null;
            // THERMAL MEAS AREA
            this.IsThermMeasNameFromRoutine = true;
            this.ThermMeasName = routine.ThermMeasName;


            // LIV TEST LIMIT DATA
            this.LivTestLimitData = new ObservableCollection<DgTestLimit>();
            if(routine.LivTestParams != null)
            {
                // PIV
                int id = 1;
                foreach (StCosParam param in routine.LivTestParams)
                {
                    if(param.ParamType == "Ith(A)" || param.ParamType == "Slope(W/A)" || param.ParamType == "Res(Ω)")
                    {
                        this.LivTestLimitData.Add(new DgTestLimit()
                        {
                            Id = id,
                            ParamName = param.Name,
                            DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                            DriveTemp = param.DriveTemp.ToString(),
                            ParamMax = param.ParamUCL.ToString(),
                            ParamMin = param.ParamLCL.ToString(),
                            ParamType = param.ParamType,
                            ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                            IsReport = param.IsReport ? "Yes" : "No",
                            IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                        });
                    }
                    else
                    {
                        this.LivTestLimitData.Add(new DgTestLimit()
                        {
                            Id = id,
                            ParamName = param.Name,
                            DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                            DriveTemp = param.DriveTemp.ToString(),
                            DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                            DriveType = param.DriveType,
                            DriveValue = param.DriveValue.ToString(),
                            ParamMax = param.ParamUCL.ToString(),
                            ParamMin = param.ParamLCL.ToString(),
                            ParamType = param.ParamType,
                            ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                            IsReport = param.IsReport ? "Yes" : "No",
                            IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                        });
                    }
                    id++;
                }
            }
            
            // SP TEST LIMIT DATA
            this.SpTestLimitData = new ObservableCollection<DgTestLimit>();
            if(routine.SpTestParams != null)
            {
                int id = 1;
                foreach (StCosParam param in routine.SpTestParams)
                {
                    this.SpTestLimitData.Add(new DgTestLimit()
                    {
                        Id = id,
                        ParamName = param.Name,
                        DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                        DriveTemp = param.DriveTemp.ToString(),
                        DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                        DriveType = param.DriveType,
                        DriveList = this.SpDriveList.ToList(),
                        DriveValue = param.DriveValue.ToString(),
                        ParamMax = param.ParamUCL.ToString(),
                        ParamMin = param.ParamLCL.ToString(),
                        ParamType = param.ParamType,
                        ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                        IsReport = param.IsReport ? "Yes" : "No",
                        IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                    });
                    id++;
                }
            }

            // NA TEST LIMIT DATA
            this.NaTestLimitData = new ObservableCollection<DgTestLimit>();
            if (routine.NaTestParams != null)
            {
                int id = 1;
                foreach (StCosParam param in routine.NaTestParams)
                {
                    this.NaTestLimitData.Add(new DgTestLimit()
                    {
                        Id = id,
                        ParamName = param.Name,
                        DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                        DriveTemp = param.DriveTemp.ToString(),
                        DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                        DriveType = param.DriveType,
                        DriveList = this.NaDriveList.ToList(),
                        DriveValue = param.DriveValue.ToString(),
                        ParamMax = param.ParamUCL.ToString(),
                        ParamMin = param.ParamLCL.ToString(),
                        ParamType = param.ParamType,
                        ParamTypeList = new ObservableCollection<string>(this.NaParaTypeList),
                        IsReport = param.IsReport ? "Yes" : "No",
                        IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                    });
                    id++;
                }
            }

            // BEAM TEST LIMIT DATA
            this.BeamTestLimitData = new ObservableCollection<DgTestLimit>();
            if (routine.BeamTestParams != null)
            {
                int id = 1;
                foreach (StCosParam param in routine.BeamTestParams)
                {
                    this.BeamTestLimitData.Add(new DgTestLimit()
                    {
                        Id = id,
                        ParamName = param.Name,
                        DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                        DriveTemp = param.DriveTemp.ToString(),
                        DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                        DriveType = param.DriveType,
                        DriveList = this.BeamDriveList.ToList(),
                        DriveValue = param.DriveValue.ToString(),
                        ParamMax = param.ParamUCL.ToString(),
                        ParamMin = param.ParamLCL.ToString(),
                        ParamType = param.ParamType,
                        ParamTypeList = new ObservableCollection<string>(this.BeamParaTypeList),
                        IsReport = param.IsReport ? "Yes" : "No",
                        IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                    });
                    id++;
                }
            }

            // ThermalCamera TEST LIMIT DATA
            this.ThermalCameraParaTypeList = new ObservableCollection<string>();
            if (this.ThermMeasName != null && this.ThermMeasName != "" && ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.ThermMeasName))
            {
                StThermAreaDef areadef = ThermMeasUtil.THERM_AREA_DICT[this.ThermMeasName];
                foreach (StThermAreaParam area in areadef.AreaList)
                    this.ThermalCameraParaTypeList.Add(area.Name);
            }
            this.ThermTestLimitData = new ObservableCollection<DgTestLimit>();
            if (routine.ThermTestParams != null)
            {
                int id = 1;
                foreach (StCosParam param in routine.ThermTestParams)
                {
                    this.ThermTestLimitData.Add(new DgTestLimit()
                    {
                        Id = id,
                        ParamName = param.Name,
                        DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                        DriveTemp = param.DriveTemp.ToString(),
                        DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                        DriveType = param.DriveType,
                        DriveList = this.ThermalCameraDriveList.ToList(),
                        DriveValue = param.DriveValue.ToString(),
                        ParamMax = param.ParamUCL.ToString(),
                        ParamMin = param.ParamLCL.ToString(),
                        ParamType = param.ParamType,
                        ParamTypeList = new ObservableCollection<string>(this.ThermalCameraParaTypeList),
                        IsReport = param.IsReport ? "Yes" : "No",
                        IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                    });
                    id++;
                }
            }

            // DELTA LIMIT DATA
            this.DeltaLimitData = new ObservableCollection<DgTestLimit>();
            if (routine.DeltaParams != null)
            {
                int id = 1;
                foreach (StCosParam param in routine.DeltaParams)
                {
                    this.DeltaLimitData.Add(new DgTestLimit()
                    {
                        Id = id,
                        ParamName = param.Name,
                        ParamMax = param.ParamUCL.ToString(),
                        ParamMin = param.ParamLCL.ToString(),
                        ParamType = param.ParamType,
                        ParamTypeList = new ObservableCollection<string>(this.DeltaParaTypeList),
                        IsReport = param.IsReport ? "Yes" : "No",
                        IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                    });
                    id++;
                }
            }
        }

        private void buttonTempDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.TemperatureList == null || this.TemperatureList.Count == 0)
                return;
            if (this.TemperatureList.Contains(this.Temperature) == false)
                return;

            // 强制焦点
            this.cbSpDriveList.Focus();

            // 删除Param参数 - LivTestLimitData
            bool testLimitUpdated = false;
            for (int i = this.LivTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.LivTestLimitData[i].DriveTemp == this.Temperature.ToString())
                {
                    this.LivTestLimitData.RemoveAt(i);
                    testLimitUpdated = true;
                }
            }
            if (testLimitUpdated)
            {
                for (int i = 0; i < this.LivTestLimitData.Count; i++)
                {
                    this.LivTestLimitData[i].Id = i + 1;
                }
            }

            // 删除Param参数 - SpTestLimitData
            testLimitUpdated = false;
            for (int i = this.SpTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.SpTestLimitData[i].DriveTemp == this.Temperature.ToString())
                {
                    this.SpTestLimitData.RemoveAt(i);
                    testLimitUpdated = true;
                }
            }
            if (testLimitUpdated)
            {
                for (int i = 0; i < this.SpTestLimitData.Count; i++)
                {
                    this.SpTestLimitData[i].Id = i + 1;
                }
            }

            // 删除Param参数 - NaTestLimitData
            testLimitUpdated = false;
            for (int i = this.NaTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.NaTestLimitData[i].DriveTemp == this.Temperature.ToString())
                {
                    this.NaTestLimitData.RemoveAt(i);
                    testLimitUpdated = true;
                }
            }
            if (testLimitUpdated)
            {
                for (int i = 0; i < this.NaTestLimitData.Count; i++)
                {
                    this.NaTestLimitData[i].Id = i + 1;
                }
            }

            // 删除Param参数 - BeamTestLimitData
            testLimitUpdated = false;
            for (int i = this.BeamTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.BeamTestLimitData[i].DriveTemp == this.Temperature.ToString())
                {
                    this.BeamTestLimitData.RemoveAt(i);
                    testLimitUpdated = true;
                }
            }
            if (testLimitUpdated)
            {
                for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                {
                    this.BeamTestLimitData[i].Id = i + 1;
                }
            }

            // 删除Param参数 - ThermalCameraTestLimitData
            testLimitUpdated = false;
            for (int i = this.ThermTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.ThermTestLimitData[i].DriveTemp == this.Temperature.ToString())
                {
                    this.ThermTestLimitData.RemoveAt(i);
                    testLimitUpdated = true;
                }
            }
            if (testLimitUpdated)
            {
                for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                {
                    this.ThermTestLimitData[i].Id = i + 1;
                }
            }

            // 从List中删除
            this.TemperatureList.Remove(this.Temperature);
            // 更新当前选择
            if (this.TemperatureList.Count > 0)
                this.Temperature = this.TemperatureList[0];

            // 刷新参数中的温度列表
            List<string> drvTempList = this.Cvt2StrList(this.TemperatureList);
            for (int i = 0; i < this.LivTestLimitData.Count; i++)
                this.LivTestLimitData[i].DriveTempList = new ObservableCollection<string>( drvTempList);
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
                this.SpTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
                this.NaTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                this.BeamTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                this.ThermTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
        }

        private void buttonTempAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbTempList.Focus();
            // 检查是否已存在
            if (this.TemperatureList.Contains(this.TemperatureNewValue))
            {
                WarnWindow.ShowMsg("该值已存在");
                return;
            }
            // 添加TemperatureList
            int idx = 0;
            for(;idx < this.TemperatureList.Count;idx++ )
            {
                if (this.TemperatureNewValue < this.TemperatureList[idx])
                    break;
            }
            this.TemperatureList.Insert(idx, this.TemperatureNewValue);
            // 更新Temperature
            this.Temperature = this.TemperatureNewValue;

            // 新的温度
            string driveTemp = this.Temperature.ToString();

            // 删除已有参数 - LIV
            for (int i = this.LivTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.LivTestLimitData[i].DriveTemp == driveTemp)
                    this.LivTestLimitData.RemoveAt(i);
            }
            // 更新ID - LIV
            for (int i = 0; i < this.LivTestLimitData.Count; i++)
                this.LivTestLimitData[i].Id = (i + 1);
            // 删除已有参数 - SP
            for (int i = this.SpTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.SpTestLimitData[i].DriveTemp == driveTemp)
                    this.SpTestLimitData.RemoveAt(i);
            }
            // 更新ID - SP
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
                this.SpTestLimitData[i].Id = (i + 1);
            // 删除已有参数 - NA
            for (int i = this.NaTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.NaTestLimitData[i].DriveTemp == driveTemp)
                    this.NaTestLimitData.RemoveAt(i);
            }
            // 更新ID - NA
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
                this.NaTestLimitData[i].Id = (i + 1);
            // 删除已有参数 - BEAM
            for (int i = this.BeamTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.BeamTestLimitData[i].DriveTemp == driveTemp)
                    this.BeamTestLimitData.RemoveAt(i);
            }
            // 更新ID - BEAM
            for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                this.BeamTestLimitData[i].Id = (i + 1);
            // 删除已有参数 - THERMAL CAMERA
            for (int i = this.ThermTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.ThermTestLimitData[i].DriveTemp == driveTemp)
                    this.ThermTestLimitData.RemoveAt(i);
            }
            // 更新ID - THERMAL CAMERA
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                this.ThermTestLimitData[i].Id = (i + 1);

            // 刷新参数中的温度列表
            List<string> drvTempList = this.Cvt2StrList(this.TemperatureList);
            for (int i = 0; i < this.LivTestLimitData.Count; i++)
                this.LivTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
                this.SpTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
                this.NaTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                this.BeamTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                this.ThermTestLimitData[i].DriveTempList = new ObservableCollection<string>(drvTempList);

            // 添加默认参数
            this.AddDefaultLivLimit(driveTemp);
            foreach(string spDrive in this.SpDriveList)
            {
                if (spDrive == null || spDrive == "")
                    continue;
                string driveType = spDrive.EndsWith("A") ? "If(A)" : "Po(W)";
                string driveValue = spDrive.Substring(0, spDrive.Length - 1);
                this.AddDefaultSpLimit(driveValue, driveType, driveTemp);
            }
            foreach (string naDrive in this.NaDriveList)
            {
                if (naDrive == null || naDrive == "")
                    continue;
                string driveType = naDrive.EndsWith("A") ? "If(A)" : "Po(W)";
                string driveValue = naDrive.Substring(0, naDrive.Length - 1);
                this.AddDefaultNaLimit(driveValue, driveType, driveTemp);
            }
        }

        /// <summary>
        /// 添加默认的LIV参数
        /// </summary>
        /// <param name="driveTemp"></param>
        private void AddDefaultLivLimit(string driveTemp)
        {
            // 添加默认参数
            this.LivTestLimitData.Add(new DgTestLimit()
            {
                Id = this.LivTestLimitData.Count + 1,
                ParamName = "Ith@" + driveTemp + "C",
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Ith(A)",
                ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.LivTestLimitData.Add(new DgTestLimit()
            {
                Id = this.LivTestLimitData.Count + 1,
                ParamName = "Slope@" + driveTemp + "C",
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Slope(W/A)",
                ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.LivTestLimitData.Add(new DgTestLimit()
            {
                Id = this.LivTestLimitData.Count + 1,
                ParamName = "Res@" + driveTemp + "C",
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Res(Ω)",
                ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonSpDriveDelete_Click(object sender, RoutedEventArgs e)
        {
            if(this.SpDrive  != null && this.SpDrive != "")
            {
                // 强制焦点
                this.cbSpDriveList.Focus();

                // 删除Param参数
                bool testLimitUpdated = false;
                for(int i= this.SpTestLimitData.Count-1; i>=0;i--)
                {
                    if((this.SpTestLimitData[i].DriveType == "If(A)" && this.SpDrive.EndsWith("A") || this.SpTestLimitData[i].DriveType == "Po(W)" && this.SpDrive.EndsWith("W"))
                        && this.SpTestLimitData[i].DriveValue == this.SpDrive.Substring(0,this.SpDrive.Length-1))
                    {
                        this.SpTestLimitData.RemoveAt(i);
                        testLimitUpdated = true;
                    }
                }
                if(testLimitUpdated)
                {
                    for (int i = 0; i < this.SpTestLimitData.Count; i++)
                    {
                        this.SpTestLimitData[i].Id = i + 1;
                    }
                }

                // 从SpDriveList中删除
                this.SpDriveList.Remove(this.SpDrive);
                // 更新当前选择
                if (this.SpDriveList.Count > 0)
                    this.SpDrive = this.SpDriveList[0];
                else
                    this.SpDrive = "";
                // 刷新驱动值
                for (int i = 0; i < this.SpTestLimitData.Count; i++)
                    this.SpTestLimitData[i].DriveList = this.SpDriveList.ToList();
            }
        }

        private void buttonSpDriveAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbSpDriveList.Focus();

            string newSpDrive = this.SpNewDriveValue.ToString();
            if (this.SpNewDriveType)
                newSpDrive += "A";
            else
                newSpDrive += "W";

            if(this.SpDriveList.Contains(newSpDrive))
            {
                WarnWindow.ShowMsg("该值已存在");
                return;
            }
            if (this.TemperatureList == null || this.TemperatureList.Count == 0)
            {
                WarnWindow.ShowMsg("请选择温度");
                return;
            }

            // 添加到SpDriveList
            this.SpDriveList.Add(newSpDrive);
            this.SpDrive = newSpDrive;

            // 添加默认参数
            string driveType = this.SpNewDriveType ? "If(A)" : "Po(W)";
            string driveValue = this.SpNewDriveValue.ToString();
            // 删除已有参数
            for(int i= this.SpTestLimitData.Count-1;i>=0;i--)
            {
                if(this.SpTestLimitData[i].DriveType == driveType && this.SpTestLimitData[i].DriveValue == driveValue)
                {
                    this.SpTestLimitData.RemoveAt(i);
                }
            }
            // 更新Id和DriveList
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
            {
                this.SpTestLimitData[i].Id = (i + 1);
                this.SpTestLimitData[i].DriveList = this.SpDriveList.ToList();
            }
                
            // 添加默认参数
            foreach (double temp in this.TemperatureList)
                this.AddDefaultSpLimit(driveValue, driveType, temp.ToString());
        }

        /// <summary>
        /// 添加默认的SP参数
        /// </summary>
        /// <param name="driveValue"></param>
        /// <param name="driveType"></param>
        /// <param name="driveTemp"></param>
        private void AddDefaultSpLimit(string driveValue, string driveType, string driveTemp)
        {
            // New Drive Name
            string driveName = driveValue.ToString() + (driveType == "If(A)" ? "A" : "W") + "," + driveTemp+"C";

            // 添加加默认参数
            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "Peak@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Peak(nm)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "Center@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Center(nm)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "Centroid@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "Centroid(nm)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "FWHM@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "FWHM(nm)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "SNR@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "SNR(dB)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = this.SpTestLimitData.Count + 1,
                ParamName = "PIB@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.SpDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "PIB(%)",
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonNaDriveDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.NaDrive != null && this.NaDrive != "")
            {
                // 强制焦点
                this.cbNaDriveList.Focus();

                // 删除Param参数
                bool testLimitUpdated = false;
                for (int i = this.NaTestLimitData.Count - 1; i >= 0; i--)
                {
                    if ((this.NaTestLimitData[i].DriveType == "If(A)" && this.NaDrive.EndsWith("A") || this.NaTestLimitData[i].DriveType == "Po(W)" && this.NaDrive.EndsWith("W"))
                        && this.NaTestLimitData[i].DriveValue == this.NaDrive.Substring(0, this.NaDrive.Length - 1))
                    {
                        this.NaTestLimitData.RemoveAt(i);
                        testLimitUpdated = true;
                    }
                }
                if (testLimitUpdated)
                {
                    for (int i = 0; i < this.NaTestLimitData.Count; i++)
                    {
                        this.NaTestLimitData[i].Id = i + 1;
                    }
                }

                // 从NaDriveList中删除
                this.NaDriveList.Remove(this.NaDrive);
                // 更新当前选择
                if (this.NaDriveList.Count > 0)
                    this.NaDrive = this.NaDriveList[0];
                else
                    this.NaDrive = "";
                // 刷新驱动值
                for (int i = 0; i < this.NaTestLimitData.Count; i++)
                    this.NaTestLimitData[i].DriveList = this.NaDriveList.ToList();
            }
        }

        private void buttonNaDriveAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbNaDriveList.Focus();

            string newNaDrive = this.NaNewDriveValue.ToString();
            if (this.NaNewDriveType)
                newNaDrive += "A";
            else
                newNaDrive += "W";

            if (this.NaDriveList.Contains(newNaDrive))
            {
                WarnWindow.ShowMsg("该值已存在");
                return;
            }
            if(this.TemperatureList == null || this.TemperatureList.Count == 0)
            {
                WarnWindow.ShowMsg("请选择温度");
                return;
            }

            // 添加到NaDriveList
            this.NaDriveList.Add(newNaDrive);
            this.NaDrive = newNaDrive;

            // 添加默认参数
            string driveType = this.NaNewDriveType ? "If(A)" : "Po(W)";
            string driveValue = this.NaNewDriveValue.ToString();
            // 删除已有参数
            for (int i = this.NaTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.NaTestLimitData[i].DriveType == driveType && this.NaTestLimitData[i].DriveValue == driveValue)
                {
                    this.NaTestLimitData.RemoveAt(i);
                }
            }
            // 更新Id和DriveList
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
            {
                this.NaTestLimitData[i].Id = (i + 1);
                this.NaTestLimitData[i].DriveList = this.NaDriveList.ToList();
            }

            // 添加默认参数
            this.AddDefaultNaLimit(driveValue, driveType, this.Temperature.ToString());
            List<string> tempstrList = new List<string>();
            foreach (double temp in this.TemperatureList)
                tempstrList.Add(temp.ToString());
        }

        /// <summary>
        /// 添加默认的N.A.参数
        /// </summary>
        /// <param name="driveValue"></param>
        /// <param name="driveType"></param>
        /// <param name="driveTemp"></param>
        private void AddDefaultNaLimit(string driveValue, string driveType, string driveTemp)
        {
            // New Drive Name
            string driveName = driveValue.ToString() + (driveType == "If(A)" ? "A" : "W") + "," + driveTemp + "C";

            // 添加加默认参数
            this.NaTestLimitData.Add(new DgTestLimit()
            {
                Id = this.NaTestLimitData.Count + 1,
                ParamName = "NA@" + driveName,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTemp = driveTemp,
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveType = driveType,
                DriveList = this.NaDriveList.ToList(),
                DriveValue = driveValue,
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamType = "N.A.",
                ParamTypeList = new ObservableCollection<string>(this.NaParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonBeamDriveDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.BeamDrive != null && this.BeamDrive != "")
            {
                // 强制焦点
                this.cbBeamDriveList.Focus();

                // 删除Param参数
                bool testLimitUpdated = false;
                for (int i = this.BeamTestLimitData.Count - 1; i >= 0; i--)
                {
                    if ((this.BeamTestLimitData[i].DriveType == "If(A)" && this.BeamDrive.EndsWith("A") || this.BeamTestLimitData[i].DriveType == "Po(W)" && this.BeamDrive.EndsWith("W"))
                        && this.BeamTestLimitData[i].DriveValue == this.BeamDrive.Substring(0, this.BeamDrive.Length - 1))
                    {
                        this.BeamTestLimitData.RemoveAt(i);
                        testLimitUpdated = true;
                    }
                }
                if (testLimitUpdated)
                {
                    for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                    {
                        this.BeamTestLimitData[i].Id = i + 1;
                    }
                }

                // 从BeamDriveList中删除
                this.BeamDriveList.Remove(this.BeamDrive);
                // 更新当前选择
                if (this.BeamDriveList.Count > 0)
                    this.BeamDrive = this.BeamDriveList[0];
                else
                    this.BeamDrive = "";
                // 刷新驱动值
                for (int i = 0; i < this.BeamTestLimitData.Count; i++)
                    this.BeamTestLimitData[i].DriveList = this.BeamDriveList.ToList();
            }
        }

        private void buttonBeamDriveAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbBeamDriveList.Focus();

            string newBeamDrive = this.BeamNewDriveValue.ToString();
            if (this.BeamNewDriveType)
                newBeamDrive += "A";
            else
                newBeamDrive += "W";

            if (this.BeamDriveList.Contains(newBeamDrive))
            {
                WarnWindow.ShowMsg("该值已存在");
                return;
            }
            if (this.TemperatureList == null || this.TemperatureList.Count == 0)
            {
                WarnWindow.ShowMsg("请选择温度");
                return;
            }

            // 添加到BeamDriveList
            this.BeamDriveList.Add(newBeamDrive);
            this.BeamDrive = newBeamDrive;

            // 添加默认参数
            string driveType = this.BeamNewDriveType ? "If(A)" : "Po(W)";
            string driveValue = this.BeamNewDriveValue.ToString();
            // 删除已有参数
            for (int i = this.BeamTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.BeamTestLimitData[i].DriveType == driveType && this.BeamTestLimitData[i].DriveValue == driveValue)
                {
                    this.BeamTestLimitData.RemoveAt(i);
                }
            }
            // 更新Id和DriveList
            for (int i = 0; i < this.BeamTestLimitData.Count; i++)
            {
                this.BeamTestLimitData[i].Id = (i + 1);
                this.BeamTestLimitData[i].DriveList = this.BeamDriveList.ToList();
            }

            // 添加默认参数
            //this.AddDefaultBeamLimit(driveValue, driveType, this.Temperature.ToString());
        }

        private void buttonThermalCameraDriveDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ThermalCameraDrive != null && this.ThermalCameraDrive != "")
            {
                // 强制焦点
                this.cbThermalCameraDriveList.Focus();

                // 删除Param参数
                bool testLimitUpdated = false;
                for (int i = this.ThermTestLimitData.Count - 1; i >= 0; i--)
                {
                    if ((this.ThermTestLimitData[i].DriveType == "If(A)" && this.ThermalCameraDrive.EndsWith("A") || this.ThermTestLimitData[i].DriveType == "Po(W)" && this.ThermalCameraDrive.EndsWith("W"))
                        && this.ThermTestLimitData[i].DriveValue == this.ThermalCameraDrive.Substring(0, this.ThermalCameraDrive.Length - 1))
                    {
                        this.ThermTestLimitData.RemoveAt(i);
                        testLimitUpdated = true;
                    }
                }
                if (testLimitUpdated)
                {
                    for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                    {
                        this.ThermTestLimitData[i].Id = i + 1;
                    }
                }

                // 从ThermalCameraDriveList中删除
                this.ThermalCameraDriveList.Remove(this.ThermalCameraDrive);
                // 更新当前选择
                if (this.ThermalCameraDriveList.Count > 0)
                    this.ThermalCameraDrive = this.ThermalCameraDriveList[0];
                else
                    this.ThermalCameraDrive = "";
                // 刷新驱动值
                for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                    this.ThermTestLimitData[i].DriveList = this.ThermalCameraDriveList.ToList();
            }
        }

        private void buttonThermalCameraDriveAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermalCameraDriveList.Focus();

            string newThermalCameraDrive = this.ThermalCameraNewDriveValue.ToString();
            if (this.ThermalCameraNewDriveType)
                newThermalCameraDrive += "A";
            else
                newThermalCameraDrive += "W";

            if (this.ThermalCameraDriveList.Contains(newThermalCameraDrive))
            {
                WarnWindow.ShowMsg("该值已存在");
                return;
            }
            if (this.TemperatureList == null || this.TemperatureList.Count == 0)
            {
                WarnWindow.ShowMsg("请选择温度");
                return;
            }

            // 添加到ThermalCameraDriveList
            this.ThermalCameraDriveList.Add(newThermalCameraDrive);
            this.ThermalCameraDrive = newThermalCameraDrive;

            // 添加默认参数
            string driveType = this.ThermalCameraNewDriveType ? "If(A)" : "Po(W)";
            string driveValue = this.ThermalCameraNewDriveValue.ToString();
            // 删除已有参数
            for (int i = this.ThermTestLimitData.Count - 1; i >= 0; i--)
            {
                if (this.ThermTestLimitData[i].DriveType == driveType && this.ThermTestLimitData[i].DriveValue == driveValue)
                {
                    this.ThermTestLimitData.RemoveAt(i);
                }
            }
            // 更新Id和DriveList
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
            {
                this.ThermTestLimitData[i].Id = (i + 1);
                this.ThermTestLimitData[i].DriveList = this.ThermalCameraDriveList.ToList();
            }

            // 添加默认参数
            this.AddDefaultThermalCameraLimit(driveValue, driveType, this.Temperature.ToString());
        }

        /// <summary>
        /// 添加默认的热成像参数
        /// </summary>
        /// <param name="driveValue"></param>
        /// <param name="driveType"></param>
        /// <param name="driveTemp"></param>
        private void AddDefaultThermalCameraLimit(string driveValue, string driveType, string driveTemp)
        {
            // New Drive Name
            string driveName = driveValue.ToString() + (driveType == "If(A)" ? "A" : "W") + "," + driveTemp + "C";
            // AreaList
            if (this.ThermMeasName == null || this.ThermMeasName == "" || !ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.ThermMeasName))
                return;
            List<StThermAreaParam> areaList = ThermMeasUtil.THERM_AREA_DICT[this.ThermMeasName].AreaList;
            if (areaList == null || areaList.Count == 0)
                return;
            // ThermalCameraParaTypeList
            this.ThermalCameraParaTypeList = new ObservableCollection<string>();
            foreach (StThermAreaParam area in areaList)
                this.ThermalCameraParaTypeList.Add(area.Name);
            // Add Default Limit
            foreach (StThermAreaParam area in areaList)
            {
                this.ThermTestLimitData.Add(new DgTestLimit()
                {
                    Id = this.ThermTestLimitData.Count + 1,
                    ParamName = area.Name + "@" + driveName,
                    DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                    DriveTemp = driveTemp,
                    DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                    DriveType = driveType,
                    DriveList = this.ThermalCameraDriveList.ToList(),
                    DriveValue = driveValue,
                    ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                    ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                    ParamType = area.Name,
                    ParamTypeList = new ObservableCollection<string>(this.ThermalCameraParaTypeList),
                    IsReport = "Yes",
                    IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
                });
            }
        }

        private void cbThermMeasNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 从Routine加载则不更新
            if (this.IsThermMeasNameFromRoutine)
            {
                this.IsThermMeasNameFromRoutine = false;
                return;
            }
            // 清除所有参数
            this.ThermTestLimitData.Clear();
            // 添加默认参数
            foreach (double temperature in this.TemperatureList)
            {
                foreach(string drive in this.ThermalCameraDriveList)
                {
                    string driveType = drive.EndsWith("A") ? "If(A)" : "Po(W)";
                    string driveValue = drive.Substring(0, drive.Length - 1);
                    this.AddDefaultThermalCameraLimit(driveValue, driveType, temperature.ToString());
                }
            }
        }


        private void buttonLivLimitAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            this.LivTestLimitData.Add(new DgTestLimit()
            {
                Id = this.LivTestLimitData.Count+1,
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamTypeList = new ObservableCollection<string>(this.LivParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonLivLimitDelete_Click(object sender, RoutedEventArgs e)
        {
            //// 删除选择项
            //if (this.dataGridLivLimit.SelectedIndex >= 2) // ITH和Slope不能删除
            //{
            //    this.LivTestLimitData.RemoveAt(this.dataGridLivLimit.SelectedIndex);
            //    // 更新ID
            //    for (int i = 0; i < this.LivTestLimitData.Count; i++)
            //        this.LivTestLimitData[i].Id = (i + 1);
            //}     
            //else if(this.dataGridLivLimit.SelectedIndex >= 0 && this.dataGridLivLimit.SelectedIndex <= 1)
            //{
            //    WarnWindow.ShowMsg("Ith和Slope参数为必须参数, 不能删除!");
            //}

            // 删除选择项
            if (this.dataGridLivLimit.SelectedIndex >= 0)
                this.LivTestLimitData.RemoveAt(this.dataGridLivLimit.SelectedIndex);
            // 更新ID
            for (int i = 0; i < this.LivTestLimitData.Count; i++)
                this.LivTestLimitData[i].Id = (i + 1);
        }

        private void buttonSpLimitAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            this.SpTestLimitData.Add(new DgTestLimit()
            {
                Id = (this.SpTestLimitData.Count + 1),
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveList = this.SpDriveList.ToList(),
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamTypeList = new ObservableCollection<string>(this.SpParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonSpLimitDelete_Click(object sender, RoutedEventArgs e)
        {
            // 删除选择项
            if (this.dataGridSpLimit.SelectedIndex >= 0)
                this.SpTestLimitData.RemoveAt(this.dataGridSpLimit.SelectedIndex);
            // 更新ID
            for (int i = 0; i < this.SpTestLimitData.Count; i++)
                this.SpTestLimitData[i].Id = (i + 1);
        }

        private void buttonNALimitAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            this.NaTestLimitData.Add(new DgTestLimit()
            {
                Id = (this.NaTestLimitData.Count + 1),
                DriveTempList = new ObservableCollection<string>(this.Cvt2StrList(this.TemperatureList)),
                DriveTypeList = new ObservableCollection<string>(this.DriveTypeList),
                DriveList = this.NaDriveList.ToList(),
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamTypeList = new ObservableCollection<string>(this.NaParaTypeList),
                IsReport = "Yes",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonNALimitDelete_Click(object sender, RoutedEventArgs e)
        {
            // 删除选择项
            if (this.dataGridNaLimit.SelectedIndex >= 0)
                this.NaTestLimitData.RemoveAt(this.dataGridNaLimit.SelectedIndex);
            // 更新ID
            for (int i = 0; i < this.NaTestLimitData.Count; i++)
                this.NaTestLimitData[i].Id = (i + 1);
        }

        private List<string> Cvt2StrList(ObservableCollection<double> data)
        {
            List<string> strList = new List<string>();
            foreach (double val in data)
                strList.Add(val.ToString());
            return strList;
        }

        private void buttonDeltaLimitAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            this.DeltaLimitData.Add(new DgTestLimit()
            {
                Id = (this.DeltaLimitData.Count + 1),
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamTypeList = new ObservableCollection<string>(this.DeltaParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonDeltaLimitDelete_Click(object sender, RoutedEventArgs e)
        {
            // 删除选择项
            if (this.dataGridDeltaLimit.SelectedIndex >= 0)
                this.DeltaLimitData.RemoveAt(this.dataGridDeltaLimit.SelectedIndex);
            // 更新ID
            for (int i = 0; i < this.DeltaLimitData.Count; i++)
                this.DeltaLimitData[i].Id = (i + 1);
        }

        private void buttonThmLimitAdd_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbRoutineNameList.Focus();

            this.ThermTestLimitData.Add(new DgTestLimit()
            {
                Id = (this.ThermTestLimitData.Count + 1),
                ParamMax = TestSystemUtil.DEFAULT_MAX_LIMIT.ToString(),
                ParamMin = TestSystemUtil.DEFAULT_MIN_LIMIT.ToString(),
                ParamTypeList = new ObservableCollection<string>(this.ThermalCameraParaTypeList),
                IsReport = "No",
                IsReportList = new ObservableCollection<string>(this.IsReportOptionList)
            });
        }

        private void buttonThmLimitDelete_Click(object sender, RoutedEventArgs e)
        {
            // 删除选择项
            if (this.dataGridThermalScanLimit.SelectedIndex >= 0)
                this.ThermTestLimitData.RemoveAt(this.dataGridThermalScanLimit.SelectedIndex);
            // 更新ID
            for (int i = 0; i < this.ThermTestLimitData.Count; i++)
                this.ThermTestLimitData[i].Id = (i + 1);
        }
    }

    public class DgTestLimit : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Id
        /// </summary>
        private int id;
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        /// <summary>
        /// ParamName
        /// </summary>
        private string param_name;
        /// <summary>
        /// ParamName
        /// </summary>
        public string ParamName 
        {
            get { return param_name; }
            set { param_name = value; OnPropertyChanged("ParamName"); }
        }

        /// <summary>
        /// DriveValue
        /// </summary>
        private string drive_value;
        /// <summary>
        /// DriveValue
        /// </summary>
        public string DriveValue
        {
            get { return drive_value; }
            set { drive_value = value; OnPropertyChanged("DriveValue"); }
        }

        /// <summary>
        /// DriveType
        /// </summary>
        private string drive_type;
        /// <summary>
        /// DriveType
        /// </summary>
        public string DriveType
        {
            get { return drive_type; }
            set { drive_type = value; OnPropertyChanged("DriveType"); UpdateDriveValue(); }
        }

        /// <summary>
        /// DriveTemp
        /// </summary>
        private string drive_temp;
        /// <summary>
        /// DriveTemp
        /// </summary>
        public string DriveTemp
        {
            get { return drive_temp; }
            set { drive_temp = value; OnPropertyChanged("DriveTemp"); }
        }


        /// <summary>
        /// ParamMax
        /// </summary>
        private string param_max;
        /// <summary>
        /// ParamMax
        /// </summary>
        public string ParamMax
        {
            get { return param_max; }
            set { param_max = value; OnPropertyChanged("ParamMax"); }
        }

        /// <summary>
        /// ParamMin
        /// </summary>
        private string param_min;
        /// <summary>
        /// ParamMin
        /// </summary>
        public string ParamMin
        {
            get { return param_min; }
            set { param_min = value; OnPropertyChanged("ParamMin"); }
        }

        /// <summary>
        /// ParamType
        /// </summary>
        private string param_type;
        /// <summary>
        /// ParamType
        /// </summary>
        public string ParamType
        {
            get { return param_type; }
            set { param_type = value; OnPropertyChanged("ParamType"); }
        }

        /// <summary>
        /// IsReport
        /// </summary>
        private string is_report;
        /// <summary>
        /// IsReport
        /// </summary>
        public string IsReport
        {
            get { return is_report; }
            set { is_report = value; OnPropertyChanged("IsReport"); }
        }

        public ObservableCollection<string> DriveValueList { get; set; }

        public ObservableCollection<string> DriveTempList { get; set; }

        public ObservableCollection<string> DriveTypeList { get; set; }

        public ObservableCollection<string> ParamTypeList { get; set; }

        public ObservableCollection<string> IsReportList { get; set; }

        private List<string> drive_list;
        public List<string> DriveList
        {
            private get { return drive_list; }
            set { drive_list = value; UpdateDriveValue(); }
        }

        private void UpdateDriveValue()
        {
            if (DriveList == null)
                return;
            // New List
            List<string> lt = new List<string>();
            if (DriveType == "If(A)")
            {
                foreach (string drv in DriveList)
                {
                    if (drv.EndsWith("A"))
                        lt.Add(drv.Substring(0, drv.Length - 1));
                }
            }
            else if (DriveType == "Po(W)")
            {
                foreach (string drv in DriveList)
                {
                    if (drv.EndsWith("W"))
                        lt.Add(drv.Substring(0, drv.Length - 1));
                }
            }

            // Clear DriveValueList
            if (DriveValueList == null)
            {
                DriveValueList = new ObservableCollection<string>(lt);
            }
            else
            {
                for(int i= DriveValueList.Count-1;i>=0;i--)
                {
                    if (lt.Contains(DriveValueList[i]))
                        continue;
                    DriveValueList.RemoveAt(i);
                }

                for (int i = 0; i < lt.Count; i++)
                {
                    if (DriveValueList.Contains(lt[i]))
                        continue;
                    DriveValueList.Add(lt[i]);
                }
            }
        }
    }

    /// <summary>
    /// 程序更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void RoutineUpdateHandler(object sender, RoutineUpdateEventArgs args);

    /// <summary>
    /// 程序更新
    /// </summary>
    public class RoutineUpdateEventArgs : EventArgs
    {

    }
}
