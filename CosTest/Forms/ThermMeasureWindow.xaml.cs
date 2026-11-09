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
    public partial class ThermMeasureWindow : Window, INotifyPropertyChanged
    {
        #region Binding

        /// <summary>
        /// 当前选中的ThermMeas名称
        /// </summary>
        private string _SelThermMeasName;
        /// <summary>
        /// 当前选中的ThermMeas名称
        /// </summary>
        public string SelThermMeasName
        {
            get { return _SelThermMeasName; }
            set { _SelThermMeasName = value; OnPropertyChanged("SelThermMeasName"); }
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
        /// ThermAreaParamNameList
        /// </summary>
        private ObservableCollection<string> _ThermAreaParamNameList;
        /// <summary>
        /// ThermAreaParamNameList
        /// </summary>
        public ObservableCollection<string> ThermAreaParamNameList
        {
            get { return _ThermAreaParamNameList; }
            set { _ThermAreaParamNameList = value; OnPropertyChanged("ThermAreaParamNameList"); }
        }

        /// <summary>
        /// SelThermAreaParamName
        /// </summary>
        private string _SelThermAreaParamName;
        /// <summary>
        /// SelThermAreaParamName
        /// </summary>
        public string SelThermAreaParamName
        {
            get { return _SelThermAreaParamName; }
            set { _SelThermAreaParamName = value; OnPropertyChanged("SelThermAreaParamName"); }
        }

        /// <summary>
        /// ThermAreaParamName
        /// </summary>
        private string _ThermAreaParamName;
        /// <summary>
        /// ThermAreaParamName
        /// </summary>
        public string ThermAreaParamName
        {
            get { return _ThermAreaParamName; }
            set { _ThermAreaParamName = value; OnPropertyChanged("ThermAreaParamName"); }
        }

        /// <summary>
        /// ThermAreaParamCoordinate
        /// </summary>
        private string _ThermAreaParamCoordinate;
        /// <summary>
        /// ThermAreaParamCoordinate
        /// </summary>
        public string ThermAreaParamCoordinate
        {
            get { return _ThermAreaParamCoordinate; }
            set { _ThermAreaParamCoordinate = value; OnPropertyChanged("ThermAreaParamCoordinate"); }
        }



        /// <summary>
        /// ThermAreaEmissivity
        /// </summary>
        private double _ThermAreaEmissivity;
        /// <summary>
        /// ThermAreaEmissivity
        /// </summary>
        public double ThermAreaEmissivity
        {
            get { return _ThermAreaEmissivity; }
            set { _ThermAreaEmissivity = value; OnPropertyChanged("ThermAreaEmissivity"); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        /// <summary>
        /// 新建的未保存的配置名
        /// </summary>
        private string BlankThermMeasName;

        /// <summary>
        /// Routine Change Event
        /// </summary>
        public event ThermMeasUpdateHandler RoutineUpdated;

        /// <summary>
        /// 当前热像测量区域定义
        /// </summary>
        private List<StThermAreaParam> ThermAreaParamList;
        /// <summary>
        /// 当前热像测量区域定义
        /// </summary>
        private StThermAreaParam ThermAreaParam;
        /// <summary>
        /// TOOL_RPOC
        /// </summary>
        private ToolProc TOOL_RPOC;

        /// <summary>
        /// 是否已经关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        internal ThermMeasureWindow(ToolProc toolProc)
        {
            InitializeComponent();

            // 输入Proc
            this.TOOL_RPOC = toolProc;
            // 绑定事件
            this.TOOL_RPOC.ToolThermalImageUpdate += new ToolThermalImageHandler(this.ToolThermalImageUpdated);

            // binding
            this.DataContext = this;

            //IsClosed
            this.IsClosed = false;

            //默认发射率0.95
            this.ThermAreaEmissivity = 0.95;

            // 配置列表
            this.ThermMeasNameList = new ObservableCollection<string>(ThermMeasUtil.GetAreaNameList());
            // 当前配置
            if (this.ThermMeasNameList.Count > 0)
                this.SelThermMeasName = this.ThermMeasNameList[0];
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


        private void cbThermMeasNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 检查当前配置名称
            if (this.SelThermMeasName == null || this.SelThermMeasName == "")
                return;

            //删除未保存的空模板
            if (this.BlankThermMeasName != null && this.BlankThermMeasName != this.SelThermMeasName)
            {
                // 更新列表
                string selThermMeasName = this.SelThermMeasName;
                this.ThermMeasNameList.Remove(this.BlankThermMeasName);
                this.ThermMeasNameList = null;
                this.ThermMeasNameList = new ObservableCollection<string>(ThermMeasUtil.GetAreaNameList());
                this.BlankThermMeasName = null;
                this.SelThermMeasName = selThermMeasName;
            }

            // 更新
            if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.SelThermMeasName))
                this.UpdateRoutineBinding(ThermMeasUtil.THERM_AREA_DICT[this.SelThermMeasName]);
            else
                this.UpdateRoutineBinding(new StThermAreaDef() { AreaList = new List<StThermAreaParam>() });//使用空模板
        }

        private void UpdateRoutineBinding(StThermAreaDef areadef)
        {
            this.ThermAreaParamNameList = new ObservableCollection<string>();
            this.ThermAreaParamList = areadef.AreaList;

            if (this.ThermAreaParamList != null && this.ThermAreaParamList.Count > 0)
            {
                foreach (StThermAreaParam param in this.ThermAreaParamList)
                {
                    this.ThermAreaParamNameList.Add(param.Name);
                }

                this.SelThermAreaParamName = this.ThermAreaParamList[0].Name;
            }
            else
            {
                this.SelThermAreaParamName = "";
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermMeasameList.Focus();

            // 检查当前配置名称
            if (this.SelThermMeasName == null || this.SelThermMeasName == "")
            {
                WarnWindow.ShowMsg("当前配置名为空！");
                return;
            }

            // 检查配置参数
            StThermAreaDef areaDef = new StThermAreaDef();
            if (!this.ValidateThermAreaDef(ref areaDef))
                return;

            // 保存到内存
            if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.SelThermMeasName))
                ThermMeasUtil.THERM_AREA_DICT[this.SelThermMeasName] = areaDef;
            else
                ThermMeasUtil.THERM_AREA_DICT.Add(this.SelThermMeasName, areaDef);
            // 保存到文件
            ThermMeasUtil.SaveConfig();

            //清除空标志
            if (this.BlankThermMeasName != null)
            {
                this.BlankThermMeasName = null;
            }

            // 通知主界面更新列表
            if (this.RoutineUpdated != null)
                this.RoutineUpdated(this, new ThermMeasUpdateEventArgs());
        }

        private void buttonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermMeasameList.Focus();

            //检查当前配置名称
            if (this.SelThermMeasName == null || this.SelThermMeasName == "")
            {
                WarnWindow.ShowMsg("当前配置名为空！");
                return;
            }

            //弹出窗口, 输入新配置名
            EditThermMeasWindow esWin = new EditThermMeasWindow(this.ThermMeasNameList.ToList(), this.SelThermMeasName, EnThermMeasAct.SAVEAS);
            esWin.ThermMeasEvent += new ThermMeasHandler(this.ThermMeasActCallback);
            esWin.Show();
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermMeasameList.Focus();

            //弹出窗口, 输入新配置名
            EditThermMeasWindow esWin = new EditThermMeasWindow(this.ThermMeasNameList.ToList(), this.SelThermMeasName, EnThermMeasAct.NEW);
            esWin.ThermMeasEvent += new ThermMeasHandler(this.ThermMeasActCallback);
            esWin.Show();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermMeasameList.Focus();

            //检查当前配置名称
            if (this.SelThermMeasName == null || this.SelThermMeasName == "")
            {
                WarnWindow.ShowMsg("当前配置名为空, 无法删除！");
                return;
            }

            //弹出窗口, 输入新配置名
            EditThermMeasWindow esWin = new EditThermMeasWindow(this.ThermMeasNameList.ToList(), this.SelThermMeasName, EnThermMeasAct.DELETE);
            esWin.ThermMeasEvent += new ThermMeasHandler(this.ThermMeasActCallback);
            esWin.Show();
        }

        private void buttonRename_Click(object sender, RoutedEventArgs e)
        {
            // 强制焦点
            this.cbThermMeasameList.Focus();

            //检查当前配置名称
            if (this.SelThermMeasName == null || this.SelThermMeasName == "")
            {
                WarnWindow.ShowMsg("当前配置名为空, 无法重命名！");
                return;
            }

            //弹出窗口, 输入新配置名
            EditThermMeasWindow esWin = new EditThermMeasWindow(this.ThermMeasNameList.ToList(), this.SelThermMeasName, EnThermMeasAct.RENAME);
            esWin.ThermMeasEvent += new ThermMeasHandler(this.ThermMeasActCallback);
            esWin.Show();
        }

        /// <summary>
        /// 配置事件回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ThermMeasActCallback(object sender, ThermMeasEventArgs args)
        {
            StThermAreaDef areadef = new StThermAreaDef() { AreaList = new List<StThermAreaParam>() };
            List<string> nameList = new List<string>();

            switch (args.ACT)
            {
                case EnThermMeasAct.SAVE:
                    // 保存
                    // BASE配置名不是当前配置
                    if (args.BaseName != this.SelThermMeasName)
                    {
                        WarnWindow.ShowMsg(args.BaseName + " : 已经不是当前配置, 无法完成保存操作!");
                        return;
                    }
                    //检查配置参数
                    if (!this.ValidateThermAreaDef(ref areadef))
                        return;
                    // 保存到内存
                    if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.SelThermMeasName))
                        ThermMeasUtil.THERM_AREA_DICT[this.SelThermMeasName] = areadef;
                    else
                        ThermMeasUtil.THERM_AREA_DICT.Add(this.SelThermMeasName, areadef);
                    // 保存到文件
                    ThermMeasUtil.SaveConfig();
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new ThermMeasUpdateEventArgs());
                    break;

                case EnThermMeasAct.SAVEAS:
                    // 另存为
                    if (!ThermMeasUtil.THERM_AREA_DICT.ContainsKey(args.BaseName))
                    {
                        WarnWindow.ShowMsg(args.BaseName + " : 已经不存在, 无法完成另存为操作!");
                        return;
                    }
                    else
                    {
                        areadef = ThermMeasUtil.THERM_AREA_DICT[args.BaseName];
                    }
                    // 保存到内存
                    if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(args.NewName))
                        ThermMeasUtil.THERM_AREA_DICT[args.NewName] = areadef;
                    else
                        ThermMeasUtil.THERM_AREA_DICT.Add(args.NewName, areadef);
                    // 保存到文件
                    ThermMeasUtil.SaveConfig();
                    // 更新到列表中
                    this.ThermMeasNameList.Add(args.NewName);
                    this.SelThermMeasName = args.NewName;
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new ThermMeasUpdateEventArgs());
                    break;

                case EnThermMeasAct.NEW:
                    // 更新到列表中
                    this.ThermMeasNameList.Add(args.NewName);
                    this.SelThermMeasName = args.NewName;
                    // 标记当前为新建空模板
                    this.BlankThermMeasName = args.NewName;
                    // update window from routine
                    break;

                case EnThermMeasAct.DELETE:
                    // 删除
                    if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(args.BaseName))
                        ThermMeasUtil.THERM_AREA_DICT.Remove(args.BaseName);
                    // 保存到文件
                    ThermMeasUtil.SaveConfig();
                    // 更新列表
                    this.ThermMeasNameList.Remove(args.BaseName);
                    // 更新列表的当前项
                    if (this.ThermMeasNameList.Count > 0)
                        this.SelThermMeasName = this.ThermMeasNameList[0];
                    else
                        this.SelThermMeasName = "";
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new ThermMeasUpdateEventArgs());
                    break;

                case EnThermMeasAct.RENAME:
                    // 重命名
                    if (ThermMeasUtil.THERM_AREA_DICT.ContainsKey(args.BaseName))
                    {
                        // 保存到内存
                        areadef = ThermMeasUtil.THERM_AREA_DICT[args.BaseName];
                        ThermMeasUtil.THERM_AREA_DICT.Add(args.NewName, areadef);
                        ThermMeasUtil.THERM_AREA_DICT.Remove(args.BaseName);
                        // 保存到文件
                        ThermMeasUtil.SaveConfig();
                    }
                    // 更新列表
                    for (int i = 0; i < this.ThermMeasNameList.Count; i++)
                    {
                        if (this.ThermMeasNameList[i] == args.BaseName)
                        {
                            this.ThermMeasNameList[i] = args.NewName;
                            break;
                        }
                    }
                    this.SelThermMeasName = args.NewName;
                    // 通知主界面更新列表
                    if (this.RoutineUpdated != null)
                        this.RoutineUpdated(this, new ThermMeasUpdateEventArgs());
                    break;
            }
        }

        private void cbThermAreaParamNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < this.ThermAreaParamList.Count; i++)
            {
                if (this.ThermAreaParamList[i].Name == this.SelThermAreaParamName)
                {
                    this.ThermAreaParam = this.ThermAreaParamList[i];
                    this.ThermAreaParamName = this.ThermAreaParam.Name;
                    this.ThermAreaEmissivity = this.ThermAreaParam.Emissivity;
                    this.ThermAreaParamCoordinate = this.ThermAreaParam.SX + "," + this.ThermAreaParam.SY + "," + this.ThermAreaParam.EX + "," + this.ThermAreaParam.EY;
                    this.DrawCanvasRectangleFromThermArea();
                    return;
                }
            }
            // 空数据
            this.ThermAreaParam = new StThermAreaParam();
            this.ThermAreaParamName = "";
            this.ThermAreaEmissivity = 0.95;
            this.ThermAreaParamCoordinate = "";
            this.ClearCanvasThermArea();
            return;
        }



        private void buttonParamDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelThermAreaParamName != null && this.SelThermAreaParamName != "")
            {
                // 强制焦点
                this.cbThermAreaParamNameList.Focus();

                // 删除Param参数
                bool isDeleted = false;
                for(int i=this.ThermAreaParamList.Count -1;i>=0; i--)
                {
                    if(this.ThermAreaParamList[i].Name == this.SelThermAreaParamName)
                    {
                        this.ThermAreaParamList.RemoveAt(i);
                        this.ThermAreaParamNameList.Remove(this.SelThermAreaParamName);
                        isDeleted = true;
                    }
                }
                // 更新
                if(isDeleted)
                {
                    if (this.ThermAreaParamList.Count > 0)
                    {
                        this.SelThermAreaParamName = this.ThermAreaParamList[0].Name;
                    }
                    else
                    {
                        this.SelThermAreaParamName = "";
                    }
                }
            }
        }

        private void buttonParamAdd_Click(object sender, RoutedEventArgs e)
        {
            if(this.ThermAreaParamName == null || this.ThermAreaParamName == "")
            {
                WarnWindow.ShowMsg("测试区域名称为空！");
                return;
            }
            if(this.ThermAreaEmissivity > 1.0 || this.ThermAreaEmissivity < 0.0)
            {
                WarnWindow.ShowMsg("发射率必须在0.0和1.0之间！");
                return;
            }
            if (this.ThermAreaParamCoordinate == null || this.ThermAreaParamCoordinate == "")
            {
                WarnWindow.ShowMsg("测试区域的坐标不能为空！");
                return;
            }
            //坐标
            bool isExisted = false;
            for (int i = 0; i < this.ThermAreaParamList.Count; i++)
            {
                if (this.ThermAreaParamList[i].Name == this.ThermAreaParamName)
                {
                    this.ThermAreaParam.Name = this.ThermAreaParamName;
                    this.ThermAreaParam.Emissivity = this.ThermAreaEmissivity;
                    this.ThermAreaParamList[i] = this.ThermAreaParam;
                    isExisted = true;
                    break;
                }
            }
            if(!isExisted)
            {
                this.ThermAreaParam.Name = this.ThermAreaParamName;
                this.ThermAreaParam.Emissivity = this.ThermAreaEmissivity;
                this.ThermAreaParamList.Add(this.ThermAreaParam);
                this.ThermAreaParamNameList.Add(this.ThermAreaParamName);
                this.SelThermAreaParamName = this.ThermAreaParamName;
            }
        }

        private void buttonReadFromCamera_Click(object sender, RoutedEventArgs e)
        {
            this.TOOL_RPOC.DoCommand(EnToolCommand.THERMSCAN2);
        }

        private void buttonReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".bmp"; // Default file extension
            dialog.Filter = "bmp (.csv)|*.bmp"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                this.ThermalImage = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void ToolThermalImageUpdated(object sender, ToolThermalImageEventArgs args)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                this.ThermalImage =  args.ThermalImage;
            });
        }

        /// <summary>
        /// 验证脚本数据是否正确
        /// </summary>
        /// <param name="areaDef"></param>
        /// <returns></returns>
        private bool ValidateThermAreaDef(ref StThermAreaDef areaDef)
        {
            if(this.ThermAreaParamList.Count == 0)
            {
                WarnWindow.ShowMsg("测量区域数量不能为0!");
                return false;
            }

            for (int i=0; i<this.ThermAreaParamList.Count;i++)
            {
                if (this.ThermAreaParamList[i].Name == "" || this.ThermAreaParamList[i].Name == null)
                {
                    WarnWindow.ShowMsg("第" + (i+1) + "个区域的名称为空!");
                    return false;
                }
                if (this.ThermAreaParamList[i].Emissivity < 0.0 || this.ThermAreaParamList[i].Emissivity > 1.0)
                {
                    WarnWindow.ShowMsg("第" + (i + 1) + "个区域的发射率必须在0和1之间!");
                    return false;
                }
                // Name
                // SX,SY,EX,EY
                // Emissity?????????????????????????????????????????????????????????????????????????????????
                //if (ValidateRoutineInputString("LIV起始电流(A)", this.LivStart, 0.0, TestSystemUtil.MaxDriveCurrentA, ref areaDef.LivScanCfg.Start) == false)
                //    return false;
            }

            areaDef.Name = this.SelThermMeasName;
            areaDef.AreaList = this.ThermAreaParamList;

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

        #region 矩形绘制
        // 当前正在绘制的矩形
        private Rectangle _currentRectangle;
        // 绘制起点
        private Point _startPoint;
        // 调整大小的模式
        private RectResizeMode _resizeMode = RectResizeMode.None;
        // 移动模式
        private bool _isMoving = false;
        // 鼠标相对矩形的位置（用于移动）
        private Point _relativePosition;
        // 是否绘制新的矩形
        private DrawNewStat _drawingState;
        // 热相机宽度
        private const double THERM_CAM_WIDTH = 640;
        // 热相机高度
        private const double THERM_CAM_HEIGHT = 516;

        private enum DrawNewStat
        {
            None,
            Ready,
            Drawing
        }

        // 调整大小的枚举
        private enum RectResizeMode
        {
            None,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Top,
            Bottom,
            Left,
            Right
        }

        /// <summary>
        /// 根据ThermArea坐标绘制Canvas矩形
        /// </summary>
        private void DrawCanvasRectangleFromThermArea()
        {
            double x = this.ThermAreaParam.SX / THERM_CAM_WIDTH * drawingCanvas.ActualWidth;
            double y = this.ThermAreaParam.SY / THERM_CAM_HEIGHT * drawingCanvas.ActualHeight;
            double width = (this.ThermAreaParam.EX - this.ThermAreaParam.SX) / THERM_CAM_WIDTH * drawingCanvas.ActualWidth;
            double height = (this.ThermAreaParam.EY - this.ThermAreaParam.SY) / THERM_CAM_HEIGHT * drawingCanvas.ActualHeight;

            _drawingState = DrawNewStat.Drawing;
            _currentRectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = new SolidColorBrush(Color.FromArgb(100, 0, 120, 215)), // 半透明蓝色
                StrokeThickness = 2,
                SnapsToDevicePixels = true
            };

            // 设置初始位置和大小
            Canvas.SetLeft(_currentRectangle, x);
            Canvas.SetTop(_currentRectangle, y);
            _currentRectangle.Width = width;
            _currentRectangle.Height = height;

            // 添加到画布
            drawingCanvas.Children.Clear();
            drawingCanvas.Children.Add(_currentRectangle);
        }

        /// <summary>
        /// 清除Canvas矩形
        /// </summary>
        private void ClearCanvasThermArea()
        {
            drawingCanvas.Children.Clear();
        }


        /// <summary>
        /// 更新坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void UpdateThermAreaFromCanvasRectangle(double x, double y, double width, double height)
        {
            this.ThermAreaParam.SX = (int)(x / drawingCanvas.ActualWidth * THERM_CAM_WIDTH);
            this.ThermAreaParam.SY = (int)(y / drawingCanvas.ActualHeight * THERM_CAM_HEIGHT);
            this.ThermAreaParam.EX = (int)((x+ width) / drawingCanvas.ActualWidth * THERM_CAM_WIDTH);
            this.ThermAreaParam.EY = (int)((y + height) / drawingCanvas.ActualHeight * THERM_CAM_HEIGHT);
            this.ThermAreaParamCoordinate = this.ThermAreaParam.SX + "," + this.ThermAreaParam.SY + "," + this.ThermAreaParam.EX + "," + this.ThermAreaParam.EY;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _startPoint = e.GetPosition(drawingCanvas);

                // 检查是否点击了已存在的矩形
                var hitTestResult = VisualTreeHelper.HitTest(drawingCanvas, _startPoint);
                if (hitTestResult.VisualHit is Rectangle hitRectangle && hitRectangle == _currentRectangle)
                {
                    // 修改现有矩形
                    _drawingState =  DrawNewStat.None;

                    // 检查是否点击了边缘（用于调整大小）
                    _resizeMode = DetermineResizeMode(hitRectangle, _startPoint);

                    if (_resizeMode == RectResizeMode.None)
                    {
                        // 如果不是调整大小，则准备移动
                        _isMoving = true;
                        _relativePosition = new Point(
                            _startPoint.X - Canvas.GetLeft(hitRectangle),
                            _startPoint.Y - Canvas.GetTop(hitRectangle));
                    }

                    return;
                }

                // 创建新矩形
                _drawingState = DrawNewStat.Ready;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(drawingCanvas);

                // 正在绘制新矩形
                if (_drawingState == DrawNewStat.Ready)
                {
                    _drawingState = DrawNewStat.Drawing;
                    _currentRectangle = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        Fill = new SolidColorBrush(Color.FromArgb(100, 0, 120, 215)), // 半透明蓝色
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };

                    // 设置初始位置和大小
                    Canvas.SetLeft(_currentRectangle, _startPoint.X);
                    Canvas.SetTop(_currentRectangle, _startPoint.Y);
                    _currentRectangle.Width = 0;
                    _currentRectangle.Height = 0;

                    // 添加到画布
                    drawingCanvas.Children.Clear();
                    drawingCanvas.Children.Add(_currentRectangle);
                }
                // 正在绘制新矩形
                else    if (_drawingState == DrawNewStat.Drawing)
                {
                    // 计算矩形的位置和大小
                    double x = Math.Min(_startPoint.X, currentPoint.X);
                    double y = Math.Min(_startPoint.Y, currentPoint.Y);
                    double width = Math.Abs(currentPoint.X - _startPoint.X);
                    double height = Math.Abs(currentPoint.Y - _startPoint.Y);

                    // 设置矩形属性
                    Canvas.SetLeft(_currentRectangle, x);
                    Canvas.SetTop(_currentRectangle, y);
                    _currentRectangle.Width = width;
                    _currentRectangle.Height = height;

                    // 更新坐标
                    this.UpdateThermAreaFromCanvasRectangle(x, y, width, height);
                }
                // 正在移动选中的矩形
                else if (_currentRectangle != null && _isMoving)
                {
                    double newX = currentPoint.X - _relativePosition.X;
                    double newY = currentPoint.Y - _relativePosition.Y;

                    // 确保矩形不会移出画布
                    newX = Math.Max(0, newX);
                    newY = Math.Max(0, newY);
                    newX = Math.Min(drawingCanvas.ActualWidth - _currentRectangle.Width, newX);
                    newY = Math.Min(drawingCanvas.ActualHeight - _currentRectangle.Height, newY);

                    Canvas.SetLeft(_currentRectangle, newX);
                    Canvas.SetTop(_currentRectangle, newY);

                    // 更新坐标
                    this.UpdateThermAreaFromCanvasRectangle(newX, newY, _currentRectangle.Width, _currentRectangle.Height);
                }
                // 正在调整选中矩形的大小
                else if (_currentRectangle != null && _resizeMode != RectResizeMode.None)
                {
                    ResizeRectangle(_currentRectangle, currentPoint);
                }
            }
            // 鼠标移动但未按下左键，更新鼠标样式
            else if (_currentRectangle != null)
            {
                RectResizeMode mode = DetermineResizeMode(_currentRectangle, e.GetPosition(drawingCanvas));
                UpdateCursor(mode);
            }
            else
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 完成绘制
            if (_currentRectangle != null)
            {
                // 选中新绘制的矩形
                SelectRectangle();
            }

            // 重置移动和调整大小状态
            _drawingState = DrawNewStat.None;
            _isMoving = false;
            _resizeMode = RectResizeMode.None;
            Cursor = Cursors.Arrow;
        }

        // 选择矩形
        private void SelectRectangle()
        {
            if (_currentRectangle != null)
            {
                // 添加选中效果（虚线边框）
                _currentRectangle.StrokeDashArray = new DoubleCollection { 5, 3 };
                _currentRectangle.Stroke = Brushes.Red;
                _currentRectangle.StrokeThickness = 2;
            }
        }

        // 取消当前选择
        private void DeselectCurrentRectangle()
        {
            if (_currentRectangle != null)
            {
                // 移除选中效果
                _currentRectangle.StrokeDashArray = null;
                _currentRectangle.Stroke = Brushes.Black;
                _currentRectangle.StrokeThickness = 2;
                _currentRectangle = null;
            }
        }

        // 确定调整大小的模式
        private RectResizeMode DetermineResizeMode(Rectangle rect, Point point)
        {
            double left = Canvas.GetLeft(rect);
            double top = Canvas.GetTop(rect);
            double right = left + rect.Width;
            double bottom = top + rect.Height;
            double threshold = 5; // 边缘检测阈值

            // 检查是否在角落
            if (point.X >= left - threshold && point.X <= left + threshold &&
                point.Y >= top - threshold && point.Y <= top + threshold)
                return RectResizeMode.TopLeft;

            if (point.X >= right - threshold && point.X <= right + threshold &&
                point.Y >= top - threshold && point.Y <= top + threshold)
                return RectResizeMode.TopRight;

            if (point.X >= left - threshold && point.X <= left + threshold &&
                point.Y >= bottom - threshold && point.Y <= bottom + threshold)
                return RectResizeMode.BottomLeft;

            if (point.X >= right - threshold && point.X <= right + threshold &&
                point.Y >= bottom - threshold && point.Y <= bottom + threshold)
                return RectResizeMode.BottomRight;

            // 检查是否在边缘
            if (point.X >= left - threshold && point.X <= left + threshold &&
                point.Y > top + threshold && point.Y < bottom - threshold)
                return RectResizeMode.Left;

            if (point.X >= right - threshold && point.X <= right + threshold &&
                point.Y > top + threshold && point.Y < bottom - threshold)
                return RectResizeMode.Right;

            if (point.Y >= top - threshold && point.Y <= top + threshold &&
                point.X > left + threshold && point.X < right - threshold)
                return RectResizeMode.Top;

            if (point.Y >= bottom - threshold && point.Y <= bottom + threshold &&
                point.X > left + threshold && point.X < right - threshold)
                return RectResizeMode.Bottom;

            return RectResizeMode.None;
        }

        // 更新鼠标光标
        private void UpdateCursor(RectResizeMode mode)
        {
            switch (mode)
            {
                case RectResizeMode.TopLeft:
                case RectResizeMode.BottomRight:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case RectResizeMode.TopRight:
                case RectResizeMode.BottomLeft:
                    Cursor = Cursors.SizeNESW;
                    break;
                case RectResizeMode.Left:
                case RectResizeMode.Right:
                    Cursor = Cursors.SizeWE;
                    break;
                case RectResizeMode.Top:
                case RectResizeMode.Bottom:
                    Cursor = Cursors.SizeNS;
                    break;
                default:
                    Cursor = Cursors.Hand;
                    break;
            }
        }

        // 调整矩形大小
        private void ResizeRectangle(Rectangle rect, Point currentPoint)
        {
            double left = Canvas.GetLeft(rect);
            double top = Canvas.GetTop(rect);
            double width = rect.Width;
            double height = rect.Height;

            switch (_resizeMode)
            {
                case RectResizeMode.TopLeft:
                    left = currentPoint.X;
                    top = currentPoint.Y;
                    width = (Canvas.GetLeft(rect) + rect.Width) - left;
                    height = (Canvas.GetTop(rect) + rect.Height) - top;
                    break;
                case RectResizeMode.TopRight:
                    top = currentPoint.Y;
                    width = currentPoint.X - left;
                    height = (Canvas.GetTop(rect) + rect.Height) - top;
                    break;
                case RectResizeMode.BottomLeft:
                    left = currentPoint.X;
                    width = (Canvas.GetLeft(rect) + rect.Width) - left;
                    height = currentPoint.Y - top;
                    break;
                case RectResizeMode.BottomRight:
                    width = currentPoint.X - left;
                    height = currentPoint.Y - top;
                    break;
                case RectResizeMode.Left:
                    left = currentPoint.X;
                    width = (Canvas.GetLeft(rect) + rect.Width) - left;
                    break;
                case RectResizeMode.Right:
                    width = currentPoint.X - left;
                    break;
                case RectResizeMode.Top:
                    top = currentPoint.Y;
                    height = (Canvas.GetTop(rect) + rect.Height) - top;
                    break;
                case RectResizeMode.Bottom:
                    height = currentPoint.Y - top;
                    break;
            }

            // 确保矩形有最小尺寸
            if (width < 10) width = 10;
            if (height < 10) height = 10;

            // 更新矩形属性
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            rect.Width = width;
            rect.Height = height;

            // 更新坐标
            this.UpdateThermAreaFromCanvasRectangle(left, top, width, height);
        }

        // 清除所有矩形
        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            drawingCanvas.Children.Clear();
            _currentRectangle = null;
        }
        #endregion
    }

    /// <summary>
    /// 热像测量区域定义更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ThermMeasUpdateHandler(object sender, ThermMeasUpdateEventArgs args);

    /// <summary>
    /// 热像测量区域定义更新
    /// </summary>
    public class ThermMeasUpdateEventArgs : EventArgs
    {

    }
}
