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
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.IO;
using System.IO.Packaging;
using System.Drawing.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using Wpf.CartesianChart.CustomTooltip;
using QRCoder;


namespace CosTest
{
    /// <summary>
    /// ReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWindow : Window
    {
        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        public ReportDataBinding ReportData { get; set; }

        private FixedPage reportPage;
        private PageContent pageContent;
        private FixedDocument fixedDocument;

        public ReportWindow()
        {
            InitializeComponent();

            //IsClosed
            this.IsClosed = false;

            // Binding Data
            this.ReportData = new ReportDataBinding();

            // Page
            Uri reportViewUri;
            if(StationUtil.EquipCustomer.ToUpper() == "VSSTEC")
                reportViewUri = new Uri("/VsstecLaserTest;Component/Forms/ReportViewVst.xaml", UriKind.Relative);
            else
                reportViewUri = new Uri("/VsstecLaserTest;Component/Forms/ReportViewWcs.xaml", UriKind.Relative);

            if(StationUtil.EquipCustomer.ToUpper() == "GUANGTONG")
                reportViewUri = new Uri("/VsstecLaserTest;Component/Forms/ReportViewGT.xaml", UriKind.Relative);

            this.reportPage = (FixedPage)Application.LoadComponent(reportViewUri);
            Grid reportGrid = reportPage.FindName("ReportGrid") as Grid;
            reportGrid.DataContext = this.ReportData;

            // Add to docViewer
            this.pageContent = new PageContent();
            ((IAddChild)this.pageContent).AddChild(reportPage);
            this.fixedDocument = new FixedDocument();
            this.fixedDocument.Pages.Add(pageContent);
            this.docViewer.Document = fixedDocument;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void Print(string dstFilePath)
        {
            try
            {
                // === 关键：强制 FixedPage 完成布局，确保所有 Binding 被求值 ===
                // LoadComponent 加载的 FixedPage 不在活动 visual tree 中，
                // DocumentViewer 异步布局可能还没来得及处理 Print 就被调用了，
                // 必须手动 Measure + Arrange + UpdateLayout 才能让绑定生效。
                double pageW = this.reportPage.Width;
                double pageH = this.reportPage.Height;
                this.reportPage.Measure(new Size(pageW, pageH));
                this.reportPage.Arrange(new Rect(0, 0, pageW, pageH));
                this.reportPage.UpdateLayout();

                // 打印XPS文
                XpsDocument _xpsDocument = new XpsDocument(dstFilePath, FileAccess.Write);
                XpsDocumentWriter xpsdw = XpsDocument.CreateXpsDocumentWriter(_xpsDocument);
                xpsdw.Write(fixedDocument);//写入XPS文件
                _xpsDocument.Close();

                // 保存照片
                string dstImgPath = dstFilePath.Substring(0, dstFilePath.Length - 3) + "bmp";
                FileStream fs = new FileStream(dstImgPath, FileMode.Create);
                // 修复：1/96 是整数除法结果为 0，必须传正确的 DPI 值 96
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                    (int)pageW, (int)pageH, 96, 96, PixelFormats.Default);
                bmp.Render(this.reportPage);
                BitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();  
            }
            catch(Exception ex)
            {
                ErrLog.WriteLine("打印报表时出错\r\n" + ex.ToString());
            }
        }
    }


    /// <summary>
    /// 报表数据绑定类(XAML BINDING)
    /// </summary>
    public class ReportDataBinding : INotifyPropertyChanged
    {
        /// <summary>
        /// ReportTitle
        /// </summary>
        private string _ReportTitle;
        /// <summary>
        /// ReportTitle
        /// </summary>
        public string ReportTitle
        {
            get { return _ReportTitle; }
            set { _ReportTitle = value; OnPropertyChanged("ReportTitle"); }
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
        /// SerialNoQrCode
        /// </summary>
        private BitmapImage _SerialNoQrCode;
        /// <summary>
        /// SerialNoQrCode
        /// </summary>
        public BitmapImage SerialNoQrCode
        {
            get { return _SerialNoQrCode; }
            set { _SerialNoQrCode = value; OnPropertyChanged("SerialNoQrCode"); }
        }

        /// <summary>
        /// Position
        /// </summary>
        private int _Position;
        /// <summary>
        /// CosIndex
        /// </summary>
        public int Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged("Position"); }
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
        /// TestRoutine
        /// </summary>
        private string _TestRoutine;
        /// <summary>
        /// TestRoutine
        /// </summary>
        public string TestRoutine
        {
            get { return _TestRoutine; }
            set { _TestRoutine = this.TestCondition(value); OnPropertyChanged("TestRoutine"); }
        }

        /// <summary>
        /// UserName
        /// </summary>
        private string _UserName;
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        /// <summary>
        /// TestDate
        /// </summary>
        private string _TestDate;
        /// <summary>
        /// TestDate
        /// </summary>
        public string TestDate
        {
            get { return _TestDate; }
            set { _TestDate = value; OnPropertyChanged("TestDate"); }
        }

        /// <summary>
        /// TestResult
        /// </summary>
        private string _TestResult;
        /// <summary>
        /// TestResult
        /// </summary>
        public string TestResult
        {
            get { return _TestResult; }
            set { _TestResult = value; OnPropertyChanged("TestResult"); }
        }

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
        /// SpChartTitle
        /// </summary>
        private string _SpChartTitle;
        /// <summary>
        /// SpChartTitle
        /// </summary>
        public string SpChartTitle
        {
            get { return _SpChartTitle; }
            set { _SpChartTitle = value; OnPropertyChanged("SpChartTitle"); }
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
        /// LivChartTitle
        /// </summary>
        private string _LivChartTitle;
        /// <summary>
        /// LivChartTitle
        /// </summary>
        public string LivChartTitle
        {
            get { return _LivChartTitle; }
            set { _LivChartTitle = value; OnPropertyChanged("LivChartTitle"); }
        }

        /// <summary>
        /// TestResultColor
        /// </summary>
        private string _TestResultColor;
        /// <summary>
        /// TestResultColor
        /// </summary>
        public string TestResultColor
        {
            get { return _TestResultColor; }
            set { _TestResultColor = value; OnPropertyChanged("TestResultColor"); }
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
        /// 是否显示Voltage曲线
        /// </summary>
        internal bool IsShowVF { get; set; }

        /// <summary>
        /// 是否显示EFF曲线
        /// </summary>
        internal bool IsShowEFF { get; set; }

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
        /// LivTestResultData 列表显示结果
        /// </summary>
        private ObservableCollection<DgTestResult> _LivTestResultData;
        /// <summary>
        /// LivTestLimitData 列表显示结果
        /// </summary>
        public ObservableCollection<DgTestResult> LivTestResultData
        {
            get { return _LivTestResultData; }
            set { _LivTestResultData = value; OnPropertyChanged("LivTestResultData"); }
        }
        /// <summary>
        /// SpTestResultData 列表显示结果
        /// </summary>
        private ObservableCollection<DgTestResult> _SpTestResultData;
        /// <summary>
        /// LivTestLimitData 列表显示结果
        /// </summary>
        public ObservableCollection<DgTestResult> SpTestResultData
        {
            get { return _SpTestResultData; }
            set { _SpTestResultData = value; OnPropertyChanged("SpTestResultData"); }
        }
        /// <summary>
        /// 图表更新完成标志
        /// </summary>
        public bool ChartUpdated { private set; get; }
        /// <summary>
        /// Liv图表更新完成标志
        /// </summary>
        private bool LivChartUpdated;
        /// <summary>
        /// Liv图表更新完成
        /// </summary>
        public MyCommand<LiveCharts.Wpf.CartesianChart> LivUpdaterTickCommand { get; set; }
        /// <summary>
        /// Sp图表更新完成标志
        /// </summary>
        private bool SpChartUpdated;
        /// <summary>
        /// Sp图表更新完成
        /// </summary>
        public MyCommand<LiveCharts.Wpf.CartesianChart> SpUpdaterTickCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReportDataBinding()
        {
            // 默认Chart参数
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
                XAxisName = "Current",
                SelectionMode = TooltipSelectionMode.SharedXInSeries,
                ShowTitle = true,
                ShowSeries = true,
                BulletSize = 0,
                IsWrapped = false
            };
            this.SpToolTip = new CustomTooltip()
            {
                XAxisName = "Wavelength",
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
                Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, Step = 5.0, StrokeThickness = 1, Stroke = StationUtil.EquipCustomer.ToUpper() == "VSSTEC" ?  Brushes.Gray  : Brushes.Transparent  }
            });

            // 报告数据
            this.ReportTitle = StationUtil.EquipCustomer.ToUpper() == "VSSTEC" ? "Final Test Data Sheet" : "Manufacture Test Report";
            this.TestResultColor = "Black";

            // 更新响应函数
            this.ChartUpdated = false;
            this.LivChartUpdated = false;
            this.SpChartUpdated = false;
            this.LivUpdaterTickCommand = new MyCommand<LiveCharts.Wpf.CartesianChart>
            {
                ExecuteDelegate = c => { this.LivChartUpdated = true; this.ChartUpdated = this.LivChartUpdated && this.SpChartUpdated; }
            };
            this.SpUpdaterTickCommand = new MyCommand<LiveCharts.Wpf.CartesianChart>
            {
                ExecuteDelegate = c => { this.SpChartUpdated = true; this.ChartUpdated = this.LivChartUpdated && this.SpChartUpdated; }
            };
        }

        /// <summary>
        /// 更新COS数据
        /// </summary>
        /// <param name="cosTestData"></param>
        public void SetCosTestData(StCosTestData cosTestData)
        {
            this.RackName = StationUtil.RackName;
            this.RackId = StationUtil.RackID;
            this.UserName = UserUtil.ActiveUser.NAME;

            this.SerialNo = cosTestData.SerialNumber;
            this.TestDate = cosTestData.TestDateTime.Substring(0, Math.Min(10, cosTestData.TestDateTime.Length));
            this.TestResult = cosTestData.RESULT;
            this.TestResultColor = "Black"; //this.TestResult == "PASS" ? "Green" : "Red";
            // 二维码生成延迟到 MainWindow 调用，防止 QRCoder.dll 缺失导致整个 SetCosTestData JIT 编译失败
            // this.SerialNoQrCode = this.CreateQrCode(this.SerialNo);

            #region LIV

            // 曲线名称
            this.LivChartTitle = "P-I Curve";

            // LIV 数据
            this.LivSeries = new SeriesCollection();
            double maxPower = 1.0, maxVoltage = 1.0, maxCurrent = 1.0;

            if (cosTestData.LivRawData != null)
            {
                for (int i = 0; i < cosTestData.LivRawData.Count; i++)
                {
                    // 防护 TestTemps 索引越界
                    double temperature = (cosTestData.TestTemps != null && i < cosTestData.TestTemps.Count)
                        ? cosTestData.TestTemps[i] : 0.0;

                #region 曲线数据
                var valuesPower = new ChartValues<Point>();
                var valuesVoltage = new ChartValues<Point>();
                var valuesEff = new ChartValues<Point>();

                for (int j = 0; j < cosTestData.LivRawData[i].Count; j++)
                {
                    if (cosTestData.LivRawData[i][j].LDI < 0.9)
                        continue;

                    var pointPower = new Point() { X = cosTestData.LivRawData[i][j].LDI, Y = cosTestData.LivRawData[i][j].PDI };
                    valuesPower.Add(pointPower);

                    var pointVoltage = new Point() { X = cosTestData.LivRawData[i][j].LDI, Y = cosTestData.LivRawData[i][j].LDV };
                    valuesVoltage.Add(pointVoltage);

                    double eff = 0.0;
                    if (cosTestData.LivRawData[i][j].LDI <= 0.0 || cosTestData.LivRawData[i][j].LDV <= 0.0 || cosTestData.LivRawData[i][j].PDI <= 0.0)
                        eff = 0.0;
                    else
                        eff = cosTestData.LivRawData[i][j].PDI / (cosTestData.LivRawData[i][j].LDI * cosTestData.LivRawData[i][j].LDV);
                    var pointEff = new Point() { X = cosTestData.LivRawData[i][j].LDI, Y = eff };
                    if (cosTestData.LivRawData[i][j].LDI > TestSystemUtil.EFF_MIN_CURRENT)                      
                        valuesEff.Add(pointEff);

                    maxPower = cosTestData.LivRawData[i][j].PDI > maxPower ? cosTestData.LivRawData[i][j].PDI : maxPower;
                    maxVoltage = cosTestData.LivRawData[i][j].LDV > maxVoltage ? cosTestData.LivRawData[i][j].LDV : maxVoltage;
                    maxCurrent = cosTestData.LivRawData[i][j].LDI > maxCurrent ? cosTestData.LivRawData[i][j].LDI : maxCurrent;
                }
                #endregion

                #region Power
                this.LivSeries.Add(new LineSeries()
                {
                    Configuration = new CartesianMapper<Point>()
                                .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                                .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                                .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                                .Fill(point => Brushes.Transparent),
                    Title = "PO@" + temperature + "C",
                    Values = valuesPower,
                    PointGeometry = null,
                    LineSmoothness = 0.0,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Transparent
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
                    if (this.LivAxesY[k].Title == "Voltage(V)" && this.IsShowVF == false)
                    {
                        this.LivAxesY.RemoveAt(k);
                    }
                    if (this.LivAxesY[k].Title == "Efficiency(%)" && this.IsShowEFF == false)
                    {
                        this.LivAxesY.RemoveAt(k);
                    }
                }
                #endregion

                #region Voltage
                if (this.IsShowVF)
                {
                    // 查找VF轴
                    int iAxisVF = -1;
                    for (int k = 0; k < this.LivAxesY.Count; k++)
                    {
                        if (this.LivAxesY[k].Title == "Voltage(V)")
                        {
                            iAxisVF = k;
                            break;
                        }
                    }
                    // 添加VF轴
                    if (iAxisVF < 0)
                    {
                        this.LivAxesY.Add(new Axis()
                        {
                            Title = "Voltage(V)",
                            Position = AxisPosition.RightTop,
                            MinValue = 0.0,
                            Foreground = Brushes.Black,
                            LabelFormatter = this.Formatter,
                            FontSize = 14,
                            Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, StrokeThickness = 1, Stroke = Brushes.Transparent }
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
                            .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                            .Fill(point => Brushes.Transparent),
                        Title = "VF@" + temperature + "C",
                        Values = valuesVoltage,
                        PointGeometry = null,
                        LineSmoothness = 0,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent,
                        ScalesYAt = iAxisVF
                    });
                }
                #endregion

                #region Efficiency
                if (this.IsShowEFF)
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
                            Foreground = Brushes.Black,
                            LabelFormatter = this.Formatter,
                            FontSize = 14,
                            Separator = new LiveCharts.Wpf.Separator() { IsEnabled = true, Step = 0.2, StrokeThickness = 1, Stroke = Brushes.Transparent }
                        });
                        iAxisEFF = this.LivAxesY.Count - 1;
                    }

                    // 添加曲线
                    this.LivSeries.Add(new LineSeries()
                    {
                        Configuration = new CartesianMapper<Point>()
                            .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                            .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                            .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                            .Fill(point => Brushes.Transparent),
                        Title = "EFF@" + temperature + "C",
                        Values = valuesEff,
                        PointGeometry = null,
                        LineSmoothness = 0,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent,
                        ScalesYAt = iAxisEFF
                    });
                }
                #endregion
                }
            }

            // Test Data
            this.LivTestResultData = new ObservableCollection<DgTestResult>();
            int id = 1;
            if (cosTestData.LivTestData != null)
            {
                // LIV Param
                foreach (StCosParam param in cosTestData.LivTestData)
                {
                    if (param.IsReport == false)
                        continue;
                    this.LivTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                    id++;
                }
            }
            // N.A.
            if (cosTestData.NaTestData != null)
            {
                // NA Param
                foreach (StCosParam param in cosTestData.NaTestData)
                {
                    if (param.IsReport == false)
                        continue;
                    this.LivTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f3"), TestResult = param.TestResult });
                    id++;
                }
            }
            // DELTA
            if (cosTestData.DeltaTestData != null && this.TestStage =="Final Test")
            {
                foreach (StCosParam param in cosTestData.DeltaTestData)
                {
                    if (param.IsReport == false)
                        continue;
                    foreach (StCosParam prm in cosTestData.LivTestData)
                    {
                        if (param.Name == prm.Name)
                        {
                            this.LivTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = "DELTA-" + param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                    foreach (StCosParam prm in cosTestData.NaTestData)
                    {
                        if (param.Name == prm.Name)
                        {
                            this.LivTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = "DELTA-" + param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
            }

            #endregion

            #region SP

            // 曲线名称
            string spTitle = "Spetrum Curve";
            if (cosTestData.SpRawData != null && cosTestData.SpRawData.Count == 1 && cosTestData.SpRawData[0].Count > 0)
            {
                // drv
                int idx = cosTestData.SpRawData[0][0].DRV.LastIndexOf('@') + 1;
                string drv = cosTestData.SpRawData[0][0].DRV.Substring(idx, cosTestData.SpRawData[0][0].DRV.Length - idx).Trim();
                spTitle += "@" + drv;
            }
            this.SpChartTitle = spTitle;

            // 光谱数据
            this.SpectrumSeries = new SeriesCollection();

            // Chart Axis
            double maxSpectrum = 5000;
            double spChartWavelengthLCL = 1000;
            double spChartWavelengthUCL = 700;
            if (cosTestData.SpRawData != null)
            {
                for (int i = 0; i < cosTestData.SpRawData.Count; i++)
                {
                    // 防护 TestTemps 索引越界
                    double temperature = (cosTestData.TestTemps != null && i < cosTestData.TestTemps.Count)
                        ? cosTestData.TestTemps[i] : 0.0;

                    // Chart Series
                    foreach (StOcSpData ocSpData in cosTestData.SpRawData[i])
                {
                    if (ocSpData.Wavelength == null || ocSpData.Wavelength.Length <= 0)
                        continue;

                    // 数据点 — 三重平滑, 报表里用推荐强度 (平衡峰形和锯齿)
                    double[] smoothSpectrum = CosAlgorithm.SmoothSpectrum(ocSpData.Wavelength, ocSpData.Spectrum, mode: 0, strength: 1);
                    var values = new ChartValues<Point>();
                    for (int j = 0; j < ocSpData.Wavelength.Length; j++)
                    {
                        var point = new Point() { X = ocSpData.Wavelength[j], Y = smoothSpectrum[j] };
                        values.Add(point);
                    }
                    // drv
                    int idx = ocSpData.DRV.LastIndexOf('@') + 1;
                    string drv = ocSpData.DRV.Substring(idx, ocSpData.DRV.Length - idx).Trim();
                    // 添加Sery
                    this.SpectrumSeries.Add(new LineSeries()
                    {
                        Configuration = new CartesianMapper<Point>()
                              .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                              .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                              .Stroke(point => Brushes.Black) // Define a function that returns a Brush based on the current data item
                              .Fill(point => Brushes.Transparent),
                        Title = "Sp@" + drv + "," + temperature + "C",
                        Values = values,
                        PointGeometry = null,
                        LineSmoothness = 1.0,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent
                    });
                    // 波长上下限
                    if (ocSpData.Wavelength.First<double>() < spChartWavelengthLCL)
                        spChartWavelengthLCL = ocSpData.Wavelength.First<double>();
                    if (ocSpData.Wavelength.Last<double>() > spChartWavelengthUCL)
                        spChartWavelengthUCL = ocSpData.Wavelength.Last<double>();
                    // 光强最大值
                    if (ocSpData.Spectrum.Max<double>() > maxSpectrum)
                        maxSpectrum = ocSpData.Spectrum.Max<double>();
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

            // Spectrum UCL — 基于原始数据最大值, 留 5% 余量, 好数归一化 (细粒度)
            {
                double target = maxSpectrum * 1.05;                 // 仅留 5% 余量
                if (target < 100) target = 100;
                double[] goodSteps = { 1.0, 1.1, 1.2, 1.25, 1.5, 1.75, 2.0, 2.25, 2.5, 3.0, 4.0, 5.0 };
                double power = Math.Pow(10, Math.Floor(Math.Log10(target)));
                double normalized = target / power;
                double pick = goodSteps[goodSteps.Length - 1];
                foreach (double gs in goodSteps)
                    if (gs >= normalized) { pick = gs; break; }
                this.SpChartSpectrumUCL = pick * power;
                this.SpChartSpectrumStep = this.SpChartSpectrumUCL / 6.0;
            }

            // Test Data
            this.SpTestResultData = new ObservableCollection<DgTestResult>();
            id = 1;
            // SP Param
            if (cosTestData.SpTestData != null)
            {
                foreach (StCosParam param in cosTestData.SpTestData)
                {
                    if (param.IsReport == false)
                        continue;
                    this.SpTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                    id++;
                }
            }
            // DELTA
            if (cosTestData.DeltaTestData != null && this.TestStage == "Final Test")
            {
                foreach (StCosParam param in cosTestData.DeltaTestData)
                {
                    if (param.IsReport == false)
                        continue;
                    foreach (StCosParam prm in cosTestData.SpTestData)
                    {
                        if (param.Name == prm.Name)
                        {
                            this.SpTestResultData.Add(new DgTestResult() { Id = id.ToString(), ParamMax = param.ParamUCL.ToString(), ParamMin = param.ParamLCL.ToString(), ParamName = "DELTA-" + param.Name, TestValue = param.TestValue.ToString("f2"), TestResult = param.TestResult });
                            id++;
                        }
                    }
                }
            }
            #endregion

            // 清除更新完成标志
            this.ChartUpdated = false;
            this.LivChartUpdated = false;
            this.SpChartUpdated = false;
        }

        /// <summary>
        /// Get Test Condition From Routine Name
        /// </summary>
        /// <param name="testRountine"></param>
        /// <returns></returns>
        private string TestCondition(string testRountine)
        {
            if (testRountine == null)
                return "";
            string[] strSegs = testRountine.Split(new char[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strSegs.Length > 2)
                return strSegs[strSegs.Length - 2];
            return testRountine;
        }

        /// <summary>
        /// 安全地生成二维码（独立于 SetCosTestData 调用，防止 QRCoder.dll 缺失阻塞整个报告数据填充）
        /// </summary>
        public void SetQrCode()
        {
            try
            {
                this.SerialNoQrCode = this.CreateQrCode(this.SerialNo);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("SetQrCode 失败（二维码将为空，不影响报告其他数据）: " + ex.ToString());
                this.SerialNoQrCode = null;
            }
        }

        /// <summary>
        /// 创建二维码图像
        /// </summary>
        /// <param name="content">要写入的内容</param>
        /// <returns></returns>
        private BitmapImage CreateQrCode(string content)
        {
            try
            {
                // 创建QRCodeGenerator实例
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                // 创建QRCodeData实例
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                // 创建QRCode实例
                QRCode qrCode = new QRCode(qrCodeData);
                // 生成二维码的Bitmap图像
                System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20); // 这里的20是像素大小
                // 转换成BitmapImage                                                          
                MemoryStream stream = new MemoryStream();
                qrCodeImage.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
            catch(Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                return null;
            }
        }
    }

    public class MyCommand<T> : ICommand where T : class
    {
        public Predicate<T> CanExecuteDelegate { get; set; }
        public Action<T> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null) ExecuteDelegate((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }



}
