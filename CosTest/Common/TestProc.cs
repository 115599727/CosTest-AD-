using HandyControl.Controls;
using IRIP_API_LIB;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using static IRIP_API_LIB.IRIP_API;

namespace CosTest
{
    class TestProc
    {
        /// <summary>
        /// Motor Controller
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
        /// PD Driver
        /// </summary>
        private PdDriver PowerDriver;
        /// <summary>
        /// Temp Driver
        /// </summary>
        private MeerstetterTECController TempDriver;
        /// <summary>
        /// Beam Camera Driver
        /// </summary>
        private BeamCam BeamCamDriver;
        /// <summary>
        /// Thermal Camera Driver
        /// </summary>
        private ThermCam ThermCamDriver;

        /// <summary>
        /// 测试步骤
        /// </summary>
        private int STEP;
        /// <summary>
        /// 是否为新的STEP
        /// </summary>
        private bool NewStep;
        /// <summary>
        /// 程序状态
        /// </summary>
        private EnTestStat _STAT;
        /// <summary>
        /// 程序状态
        /// </summary>
        internal EnTestStat STAT
        {
            get { return this._STAT; }
            set { this._STAT = value; this.SetTestStat(value); }
        }
        /// <summary>
        /// Test Stat Handler
        /// </summary>
        internal event TestStatHandler TestStatUpdate;
        /// <summary>
        /// Test Data Update Handler
        /// </summary>
        internal event TestDataUpdateHandler TestDataUpdate;
        /// <summary>
        /// Test Report Handler
        /// </summary>
        internal event TestReportHandler TestReport;
        /// <summary>
        /// Test Progess Update Handler
        /// </summary>
        internal event TestProgessUpdateHandler TestProgressUpdate;
        /// <summary>
        /// Test Data Load Handler
        /// </summary>
        internal event TestDataLoadHandler TestDataLoad;
        /// <summary>
        /// Test Di State Update Handler
        /// </summary>
        internal event TestDiStateUpdateHandler TestDiStateUpdate;
        /// <summary>
        /// Test Jig Temperature Update Handler
        /// </summary>
        internal event TestJigTemperatureUpdateHandler TestJigTemperatureUpdate;
        /// <summary>
        /// Test Temperature Set Update Handler
        /// </summary>
        internal event TestTempSetUpdateHandler TestTempSetUpdate;
        /// <summary>
        /// Test Motor Position Update Handler
        /// </summary>
        internal event TestMotorUpdateHandler TestMotorUpdate;
        /// <summary>
        /// Test Warn Handler
        /// </summary>
        internal event TestWarnHandler TestWarn;
        /// <summary>
        /// 运行命令
        /// </summary>
        private EnCommand COMMAND;

        /// <summary>
        /// 门锁状态
        /// </summary>
        internal bool INTERLOCK;
        /// <summary>
        /// 反射镜状态
        /// </summary>
        internal bool MIRROR;
        /// <summary>
        /// Y轴复位
        /// </summary>
        internal bool YHOME;
        /// <summary>
        /// 夹具温度
        /// </summary>
        private double TMP;
        /// <summary>
        /// 读取计数
        /// </summary>
        private int ReadCount;

        /// <summary>
        /// Y轴运动速度(单位为mm) -- 复位、快速定位
        /// </summary>
        private double YSP;
        /// <summary>
        /// NA扫描的起点位置(单位为mm)
        /// </summary>
        private double YPANABG;
        /// <summary>
        /// NA扫描的结束位置(单位为mm)
        /// </summary>
        private double YPANAEND;

        /// <summary>
        /// 光谱测试电流列表
        /// </summary>
        private List<double> SpDrvIfList;
        /// <summary>
        /// 光谱列表索引
        /// </summary>
        private int SpIndex;

        /// <summary>
        /// 温度列表索引
        /// </summary>
        private int TempIndex;
        /// <summary>
        /// N.A.列表所以
        /// </summary>
        private int NaIndex;
        /// <summary>
        /// N.A.测试电流列表
        /// </summary>
        private List<double> NaDrvIfList;
        /// <summary>
        /// N.A.功率阈值 (0到1.0)
        /// </summary>
        private double NA_PO_THRESH;
        /// <summary>
        /// N.A.等效直径
        /// </summary>
        private double NA_EQ_DIAMETER;
        /// <summary>
        ///  N.A. CT头与马达复位点的等效距离
        /// </summary>
        private double NA_ORG_CT_EQ_DIST;
        /// <summary>
        /// NA扫描时间
        /// </summary>
        private int NaScanTimeS;
        /// <summary>
        /// N.A.功率列表
        /// </summary>
        private List<double> NaPowList;
        /// <summary>
        /// N.A.位置列表
        /// </summary>
        private List<double> NaPosList;

        /// <summary>
        /// 光斑测试电流列表
        /// </summary>
        private List<double> BmDrvIfList;
        /// <summary>
        /// 光斑列表索引
        /// </summary>
        private int BmIndex;

        /// <summary>
        /// 热成像测试电流列表
        /// </summary>
        private List<double> ThmDrvIfList;
        /// <summary>
        /// 热成像列表索引
        /// </summary>
        private int ThmIndex;

        /// <summary>
        /// Pre Test Data
        /// </summary>
        StCosTestData CosPreTestData;
        /// <summary>
        /// COS测试结果列表
        /// </summary>
        private StCosTestData CosTestData;
        /// <summary>
        /// Cos测试结果
        /// Pass, Fail, “”
        /// </summary>
        private string CosTestResult;

        /// <summary>
        /// 要测试的批次号
        /// </summary>
        private string CosBatchNumber;
        /// <summary>
        /// 要测试的序列号
        /// </summary>
        private string CosSerialNumber;
        /// <summary>
        /// COS型号
        /// </summary>
        private string CosPartNubmer;
        /// <summary>
        /// COS工位
        /// </summary>
        private string CosTestStage;

        /// <summary>
        /// TEST ROUTINE
        /// </summary>
        private StRoutine ROUTINE;

        /// <summary>
        /// 要加载的数据文件路径
        /// </summary>
        private string LoadDataFilePath;

        /// <summary>
        /// 反射镜移动时的超时计数
        /// </summary>
        private int MirrorMotionTimeOutCount;
        /// <summary>
        /// 反射镜移动时的超时计数Limit
        /// </summary>
        private int MirrorMotionTimeOutLimit;
        /// <summary>
        /// 等待门锁执行的超时计数
        /// </summary>
        private int DoorLockTimeOutCount;
        /// <summary>
        /// 等待门锁执行的超时计数Limit
        /// </summary>
        private int DoorLockTimeOutCountLimit;
        /// <summary>
        /// 异常计数
        /// </summary>
        private int ExceptionCount;
        /// <summary>
        /// 空闲计数
        /// </summary>
        private int IdleCount;
        /// <summary>
        /// 已测试次数
        /// </summary>
        //private int TestCount;
        /// <summary>
        /// 打印正在进行
        /// </summary>
        //private bool IsPrinting;
        /// <summary>
        /// 开始控温的时间
        /// </summary>
        private DateTime DtTempCtrlStart;
        /// <summary>
        /// 温度稳定开始的时间
        /// </summary>
        private DateTime DtTempStableStart;
        /// <summary>
        /// NA测量时稳定上电的时间
        /// </summary>
        private DateTime DtNaDriveStart;
        /// <summary>
        /// 光谱测量时稳定上电的时间
        /// </summary>
        private DateTime DtSpDriveStart;
        /// <summary>
        /// 光谱测量时稳定上电的时间
        /// </summary>
        private DateTime DtNaScanStart;
        /// <summary>
        /// 光斑扫描时稳定上电的时间
        /// </summary>
        private DateTime DtBmDriveStart;
        /// <summary>
        /// 热成像扫描时稳定上电的时间
        /// </summary>
        private DateTime DtThmDriveStart;
        /// <summary>
        /// 夹具降温开始的时间
        /// </summary>
        private DateTime DtCoolDownStart;
        /// <summary>
        /// 反射镜移动时间时间
        /// </summary>
        private DateTime DtMirrorMoveStart;
        /// <summary>
        /// LIV设置
        /// </summary>
        private StGoLiv LIV;
        /// <summary>
        /// 器件连接检查开始时间
        /// </summary>
        private DateTime DtDutCheckStart;


        internal TestProc(MoonsMotor motorDriver, ItDriver laserDriver, PdDriver powerDriver, OcOsa osa, MeerstetterTECController tempDriver, BeamCam beamCamDriver, ThermCam thermalCamDriver)
        {
            // 引用
            this.MotorDriver = motorDriver;
            this.LaserDriver = laserDriver;
            this.PowerDriver = powerDriver;
            this.OSA = osa;
            this.TempDriver = tempDriver;
            this.BeamCamDriver = beamCamDriver;
            this.ThermCamDriver = thermalCamDriver;

            // 马达参数
            this.UpdateFromSysSetting();

            // CosTestResult
            this.CosTestResult = "";
            // 反射镜移动超时计数
            this.MirrorMotionTimeOutCount = 0;
            this.MirrorMotionTimeOutLimit = 20;
            // 门锁超时计数
            this.DoorLockTimeOutCount = 0;
            this.DoorLockTimeOutCountLimit = 10;
            // 异常清零
            this.ExceptionCount = 0;
            // 默认状态
            this.STAT = EnTestStat.STOPED;
        }

        /// <summary>
        /// 更新夹具运动设置
        /// </summary>
        private void UpdateFromSysSetting()
        {
            this.YSP = TestSystemUtil.YSPEED;
            this.YPANABG = TestSystemUtil.YPANABG;
            this.YPANAEND = TestSystemUtil.YPANAEND;
            this.NA_PO_THRESH = TestSystemUtil.NA_PO_THRESH;
            this.NA_EQ_DIAMETER = TestSystemUtil.NA_EQ_DIAMETER;
            this.NA_ORG_CT_EQ_DIST = TestSystemUtil.NA_ORG_CT_EQ_DIST;
        }

        /// <summary>
        /// 运行
        /// </summary>
        internal void Run()
        {
            try
            {
                // 状态
                StDiStat diStat = this.MotorDriver.GetDiStat();
                this.INTERLOCK = diStat.Interlock;
                this.MIRROR = diStat.Mirror;
                this.YHOME = diStat.Home;
                if (this.TestDiStateUpdate != null)
                    this.TestDiStateUpdate(this, new TestDiStateUpdateEventArgs(diStat.Interlock, diStat.Home, diStat.Mirror));
                // 读取温度和坐标
                this.ReadCount++;
                if (this.ReadCount >= 5)
                {
                    this.ReadCount = 0;
                    double posy = this.MotorDriver.GetRealAbsPosition();
                    if (this.TestMotorUpdate != null)
                        this.TestMotorUpdate(this, new TestMotorUpdateEventArgs(posy));

                    this.TMP = this.TempDriver.ReadTemperature_DegreeC();
                    if (this.TestJigTemperatureUpdate != null)
                        this.TestJigTemperatureUpdate(this, new TestJigTemperatureEventArgs(this.TMP));
                }

                // 检查互锁
                if (this.INTERLOCK == false && (this.STAT == EnTestStat.MOVING || this.STAT == EnTestStat.LIV || this.STAT == EnTestStat.SPECTRUM))
                {
                    this.StopOutput();
                    this.COMMAND = EnCommand.STOP;
                    if (this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("门锁打开,停止测试!"));
                }

                // 检查温度
                if ((this.TMP > TestSystemUtil.TEMP_UCL || this.TMP < TestSystemUtil.TEMP_LCL) && (this.STAT == EnTestStat.MOVING || this.STAT == EnTestStat.LIV || this.STAT == EnTestStat.SPECTRUM || this.STAT == EnTestStat.TEMP))
                {
                    this.StopOutput();
                    this.COMMAND = EnCommand.STOP;
                    if (this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("温度传感器异常,停止测试!"));
                }

                // START
                if ((this.STAT == EnTestStat.FINISHED || this.STAT == EnTestStat.STOPED) && this.COMMAND == EnCommand.START)
                {
                    //清除命令
                    this.COMMAND = EnCommand.NONE;
                    //设置STEP信息
                    this.STEP = 1;
                    this.NewStep = true;
                    //更新状态
                    this.STAT = EnTestStat.MOVING; ;
                    //更新夹具运动参数
                    this.UpdateFromSysSetting();
                }
                // STOP
                else if ((this.STAT == EnTestStat.MOVING || this.STAT == EnTestStat.LIV || this.STAT == EnTestStat.SPECTRUM || this.STAT == EnTestStat.NASCAN || this.STAT == EnTestStat.TEMP) && this.COMMAND == EnCommand.STOP)
                {
                    //清除命令
                    this.COMMAND = EnCommand.NONE;
                    //停止运动
                    this.MotorDriver.StopMove();
                    //停止SOURCE
                    LaserDriver.SetVoltageAndCurrent(0, 0);
                    this.LaserDriver.SetOutput(false);
                    //停止控温
                    this.TempDriver.IsOutputEnabled = false;
                    //退回反射镜
                    this.MotorDriver.SetMirror(false);
                    //取消强制互锁
                    this.MotorDriver.SetDoorUnlock(true);
                    //更新状态
                    this.STEP = 0;
                    this.NewStep = false;
                    this.STAT = EnTestStat.STOPED;
                }
                // PAUSE
                else if ((this.STAT == EnTestStat.MOVING || this.STAT == EnTestStat.LIV || this.STAT == EnTestStat.SPECTRUM || this.STAT == EnTestStat.NASCAN || this.STAT == EnTestStat.TEMP) && this.COMMAND == EnCommand.PAUSE)
                {
                    //清除命令
                    this.COMMAND = EnCommand.NONE;
                    //停止运动
                    this.MotorDriver.StopMove();
                    //停止SOURCE
                    LaserDriver.SetVoltageAndCurrent(0, 0);
                    this.LaserDriver.SetOutput(false);
                    //退回反射镜
                    this.MotorDriver.SetMirror(false);
                    //更新状态
                    this.STEP = 0;
                    this.NewStep = false;
                    this.STAT = EnTestStat.PAUSED;
                }
                // RESUME
                else if (this.STAT == EnTestStat.PAUSED && this.COMMAND == EnCommand.RESUME)
                {
                    //清除命令
                    this.COMMAND = EnCommand.NONE;
                    //清除数据
                    this.CosTestData.LivRawData[this.TempIndex].Clear();
                    this.CosTestData.SpRawData[this.TempIndex].Clear();
                    this.CosTestData.NaRawData[this.TempIndex].Clear();
                    this.CosTestData.BmRawData[this.TempIndex].Clear();
                    this.CosTestData.ThmRawData[this.TempIndex].Clear();
                    for (int i = this.CosTestData.LivTestData.Count - 1; i >= 0; i--)
                    {
                        if (this.CosTestData.LivTestData[i].DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex])
                            this.CosTestData.LivTestData.RemoveAt(i);
                    }
                    for (int i = this.CosTestData.SpTestData.Count - 1; i >= 0; i--)
                    {
                        if (this.CosTestData.SpTestData[i].DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex])
                            this.CosTestData.SpTestData.RemoveAt(i);
                    }
                    for (int i = this.CosTestData.NaTestData.Count - 1; i >= 0; i--)
                    {
                        if (this.CosTestData.NaTestData[i].DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex])
                            this.CosTestData.NaTestData.RemoveAt(i);
                    }

                    for (int i = this.CosTestData.BmTestData.Count - 1; i >= 0; i--)
                    {
                        if (this.CosTestData.BmTestData[i].DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex])
                            this.CosTestData.BmTestData.RemoveAt(i);
                    }
                    for (int i = this.CosTestData.ThmTestData.Count - 1; i >= 0; i--)
                    {
                        if (this.CosTestData.ThmTestData[i].DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex])
                            this.CosTestData.ThmTestData.RemoveAt(i);
                    }
                    //更新状态
                    this.STEP = 5;
                    this.NewStep = true;
                    //更新夹具运动参数
                    this.UpdateFromSysSetting();
                }
                // SHOW DATA
                else if ((this.STAT == EnTestStat.PAUSED || this.STAT == EnTestStat.STOPED || this.STAT == EnTestStat.FINISHED) && this.COMMAND == EnCommand.SHOWDATA)
                {
                    // 显示Cos数据
                    this.RunShowCosTestData();
                }
                // LOAD DATA
                else if ((this.STAT == EnTestStat.STOPED || this.STAT == EnTestStat.FINISHED) && this.COMMAND == EnCommand.LOADDATA)
                {
                    // 加载数据
                    this.RunLoadResultData();
                }
                // OPEN FILE
                else if (this.STAT == EnTestStat.FINISHED && this.COMMAND == EnCommand.OPENFILE)
                {
                    //清除命令
                    this.COMMAND = EnCommand.NONE;
                    //打开文件
                    TestResult.OpenTestFiles(this.TestWarn);
                }


                switch (this.STEP)
                {
                    #region IDLE
                    case 0:
                        this.IdleCount++;
                        if (this.IdleCount >= 10)
                        {
                            this.IdleCount = 0;
                            this.OSA.GetSerialNumber();
                            this.LaserDriver.GetSerialNumber();
                        }
                        break;
                    #endregion

                    #region INTERLOCK
                    case 1:
                        // 检查互锁
                        if (this.NewStep)
                        {
                            this.MotorDriver.SetDoorUnlock(false);
                            this.NewStep = false;
                            this.DoorLockTimeOutCount = 0;
                        }
                        else
                        {
                            if (this.INTERLOCK)
                            {
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else
                            {
                                this.DoorLockTimeOutCount++;
                                if (this.DoorLockTimeOutCount >= this.DoorLockTimeOutCountLimit)
                                {
                                    this.MotorDriver.SetDoorUnlock(true);
                                    this.STEP = 0;
                                    this.NewStep = false;
                                    this.STAT = EnTestStat.STOPED;
                                    if (this.TestWarn != null)
                                        this.TestWarn(this, new TestWarnEventArgs("门锁没有关闭,请检查!"));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region INITIALIZE
                    case 2:
                        // Init
                        if (this.NewStep)
                        {
                            // 初始化设备
                            this.OSA.IsAutoIntegrationTime = this.ROUTINE.SpScanCfg.IsAutoIntegrationTime;
                            this.OSA.IntegrationTimeMillisec = this.ROUTINE.SpScanCfg.IntegrationTime;
                            this.OSA.NumberOfScansToAverage = this.ROUTINE.SpScanCfg.NumberOfScansToAverage;
                            this.OSA.PibTotalHalfSpan = this.ROUTINE.SpScanCfg.PibTotalHalfSpan;
                            this.OSA.PibCenterHalfSpan = this.ROUTINE.SpScanCfg.PibCenterHalfSpan;
                            this.OSA.BoxcarWidth = this.ROUTINE.SpScanCfg.BoxcarWidth;
                            this.OSA.StartWavelength = this.ROUTINE.SpScanCfg.WavelengthBase - Math.Abs(this.ROUTINE.SpScanCfg.WavelengthScopeNegative);
                            this.OSA.StopWavelength = this.ROUTINE.SpScanCfg.WavelengthBase + Math.Abs(this.ROUTINE.SpScanCfg.WavelengthScopePositive);
                            this.OSA.IsGaussFit = this.ROUTINE.SpScanCfg.IsGaussFit;
                            this.OSA.WavelengthOffset = 0.0;
                            this.OSA.SpectrumNoise = TestSystemUtil.SP_NOISE;
                            this.OSA.SpectrumDeNoiseEnable = TestSystemUtil.SP_DENOISE_ENABLE;
                            foreach (StPoCal poCal in TestSystemUtil.PO_CAL_LIST)
                            {
                                if (poCal.WAVELENGTH_BASE == this.ROUTINE.SpScanCfg.WavelengthBase)
                                {
                                    this.OSA.WavelengthOffset = poCal.PO_WLOFFSET_BASE + poCal.PO_WLOFFSET;
                                    this.PowerDriver.PO_CAL = poCal.PO_CAL;
                                    this.PowerDriver.PO_OFFSET = poCal.PO_OFFSET;
                                    this.PowerDriver.PO_CAL_BASE = poCal.PO_CAL_BASE;
                                    this.PowerDriver.PO_OFFSET_BASE = poCal.PO_OFFSET_BASE;
                                    break;
                                }
                            }
                            this.OSA.UpdateSetting();

                            // 初始化设备
                            this.TempDriver.IsOutputEnabled = false;
                            //this.TempDriver.TemperatureDerivative = TestSystemUtil.TEMP_KD;
                            //this.TempDriver.TemperatureIntegral = TestSystemUtil.TEMP_KI;
                            //this.TempDriver.TemperatureGain = TestSystemUtil.TEMP_KP;

                            // 初始化温度索引
                            this.TempIndex = 0;

                            // 设置功率计RANGE
                            this.PowerDriver.RANGE = (ushort)TestSystemUtil.PD_RNG;

                            // 设置电源
                            LaserDriver.SetVoltageAndCurrent(0, 0);
                            LaserDriver.SetOutput(true);
                           // Thread.Sleep(2000);
                            
                            this.LaserDriver.SetCurrentUpperLimit(TestSystemUtil.LASER_CURR_UCL);
                            this.LaserDriver.SetVoltageUpperLimit(TestSystemUtil.LASER_VOLT_UCL);

                            // 清除马达报警
                            this.MotorDriver.ClearAlarm();
                            // 马达上电
                            this.MotorDriver.MotorOn();
                            // 后退反射镜
                            this.MotorDriver.SetMirror(false);

                            // 更新测量区域
                            if (StationUtil.ThermCamEnable && !string.IsNullOrEmpty(this.ROUTINE.ThermMeasName) && ThermMeasUtil.THERM_AREA_DICT.ContainsKey(this.ROUTINE.ThermMeasName))
                            {
                                this.ThermCamDriver.UpdateMeasAera(ThermMeasUtil.THERM_AREA_DICT[this.ROUTINE.ThermMeasName].AreaList);
                            }

                            // 初始化数据列表
                            this.CosTestData = new StCosTestData();
                            this.CosTestData.TestDateTime = DateTime.Now.ToString();
                            this.CosTestData.SerialNumber = this.CosSerialNumber;
                            this.CosTestData.TestUserName = UserUtil.ActiveUser.NAME;
                            this.CosTestData.TestTemps = new List<double>(this.ROUTINE.TempDrvs);
                            this.CosTestData.LivTestData = new List<StCosParam>();
                            this.CosTestData.SpTestData = new List<StCosParam>();
                            this.CosTestData.NaTestData = new List<StCosParam>();
                            this.CosTestData.BmTestData = new List<StCosParam>();
                            this.CosTestData.ThmTestData = new List<StCosParam>();
                            this.CosTestData.DeltaTestData = new List<StCosParam>();
                            this.CosTestData.LivRawData = new List<List<StGoPIV>>();
                            this.CosTestData.SpRawData = new List<List<StOcSpData>>();
                            this.CosTestData.NaRawData = new List<List<StNaData>>();
                            this.CosTestData.BmRawData = new List<List<StBeamData>>();
                            this.CosTestData.ThmRawData = new List<List<StThmData>>();
                            for (int j = 0; j < this.ROUTINE.TempDrvs.Count; j++)
                            {
                                this.CosTestData.LivRawData.Add(new List<StGoPIV>());
                                this.CosTestData.SpRawData.Add(new List<StOcSpData>());
                                this.CosTestData.NaRawData.Add(new List<StNaData>());
                                this.CosTestData.BmRawData.Add(new List<StBeamData>());
                                this.CosTestData.ThmRawData.Add(new List<StThmData>());
                            }

                            // 初始化Result结果
                            this.CosTestResult = "";
                            if (this.TestProgressUpdate != null)
                                this.TestProgressUpdate(this, new TestProgressUpdateEventArgs(this.CosTestResult));

                            // 标记状态
                            this.NewStep = false;
                            this.STAT = EnTestStat.MOVING;
                            this.MirrorMotionTimeOutCount = 0;

                        }
                        else
                        {
                            // 反射镜后退完毕
                            if (this.MIRROR == false)
                            {
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else
                            {
                                this.MirrorMotionTimeOutCount++;
                                if (this.MirrorMotionTimeOutCount >= this.MirrorMotionTimeOutLimit)
                                {
                                    this.MotorDriver.SetDoorUnlock(true);
                                    this.STEP = 0;
                                    this.NewStep = false;
                                    this.STAT = EnTestStat.STOPED;
                                    if (this.TestWarn != null)
                                        this.TestWarn(this, new TestWarnEventArgs("反射镜后退失败,请检查!"));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region HOME
                    case 3:
                        // 复位
                        if (this.NewStep)
                        {
                            if (this.YHOME)
                            {
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else if (this.MIRROR == false)
                            {
                                this.MotorDriver.SetSpeed(this.YSP);
                                this.MotorDriver.SetTargetPosition(-0.05);
                                //this.MotorDriver.FeedLength();
                                //Thread.Sleep(500);
                                //this.MotorDriver.StopMove();
                                this.MotorDriver.SeekHome();
                                this.NewStep = false;
                                this.STAT = EnTestStat.MOVING;
                            }
                            else
                            {
                                this.MotorDriver.SetDoorUnlock(true);
                                this.STEP = 0;
                                this.NewStep = false;
                                this.STAT = EnTestStat.STOPED;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未后退,不能移动马达,请检查!"));
                            }
                        }
                        else
                        {
                            StMotorStat stat = this.MotorDriver.GetMotorStat();
                            if (stat.Homing == false)
                            {
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else if (stat.Moving == false)
                            {
                                this.STEP = 0;
                                this.STAT = EnTestStat.STOPED;
                            }
                        }
                        break;
                    #endregion

                    #region Check Wire Connection
                    case 4:

                        //this.NewStep = true;
                        //this.STEP += 1;
                        //break;


                        if (this.NewStep)
                        {
                            LaserDriver.SetOvpValue(ROUTINE.LivScanCfg.PreloadingVoltage);//保护电压(OVP阈值)
                            LaserDriver.SetVoltage(ROUTINE.LivScanCfg.PreloadingVoltage);  //工作电压
                            LaserDriver.SetCurrent(ROUTINE.LivScanCfg.PreloadingCurrent);   //预加载电流
                            LaserDriver.SetOutput(true);                                   //打开输出！
                            ErrLog.WriteLine($"Step4 启动输出: V={ROUTINE.LivScanCfg.PreloadingVoltage}V, I={ROUTINE.LivScanCfg.PreloadingCurrent}A, OUTP={LaserDriver.GetOutput()}");
                            Thread.Sleep(1500);
                            this.NewStep = false;
                            this.DtDutCheckStart = DateTime.Now;
                        }
                        else
                        {
                            if ((DateTime.Now - this.DtDutCheckStart).TotalSeconds >= 2)
                            {
                                // bool isOvpTriped = this.LaserDriver.IsOvpTriped();
                                bool isOvpTriped = false;
                                double measI = this.LaserDriver.GetMeasCurrent();
                                double measV = this.LaserDriver.GetMeasVoltage();
                                double setI = ROUTINE.LivScanCfg.PreloadingCurrent;
                                ErrLog.WriteLine($"isOvpTriped={isOvpTriped}; setI={setI:F3}; measI={measI:F3}; measV={measV:F3}");

                                // OVP触发，直接判定异常
                                if (isOvpTriped)
                                {
                                    LaserDriver.SetVoltageAndCurrent(0, 0);
                                    this.LaserDriver.SetOutput(false);
                                    this.MotorDriver.SetDoorUnlock(true);
                                    this.STEP = 0;
                                    this.NewStep = false;
                                    this.STAT = EnTestStat.STOPED;
                                    this.TestWarn?.Invoke(this, new TestWarnEventArgs("OVP保护触发！"));
                                    return;
                                }

                                // ========== 动态阈值判定 ==========
                                bool currentInRange = Math.Abs(measI - setI) < ROUTINE.LivScanCfg.CurrentTol;
                                bool isShort = measV <= ROUTINE.LivScanCfg.ShortVoltThresh;   // 电压拉低 → 短路
                                bool isOpen = measI <= ROUTINE.LivScanCfg.OpenCurrentThresh;  // 电流上不去 → 开路

                                if ((DateTime.Now - this.DtDutCheckStart).TotalSeconds >= 5)
                                {
                                    ErrLog.WriteLine($"TotalSeconds={(DateTime.Now - this.DtDutCheckStart).TotalSeconds} isShort={isShort} isOpen={isOpen} currentInRange={currentInRange}");
                                    // 5s超时还没稳定，判定故障
                                    LaserDriver.SetVoltageAndCurrent(0, 0);
                                    this.LaserDriver.SetOutput(false);
                                    this.MotorDriver.SetDoorUnlock(true);
                                    this.STEP = 0;
                                    this.NewStep = false;
                                    this.STAT = EnTestStat.STOPED;

                                    if (isShort)
                                    {
                                        this.TestWarn?.Invoke(this, new TestWarnEventArgs("负载可能短路，请检查连接状态!"));
                                    }
                                    else if (isOpen)
                                    {
                                        this.TestWarn?.Invoke(this, new TestWarnEventArgs("负载可能开路，请检查连接状态!"));
                                    }
                                    else
                                    {
                                        this.TestWarn?.Invoke(this, new TestWarnEventArgs("DUT上电超时，状态异常!"));
                                    }
                                }
                                else
                                {
                                    // 稳定时间到，状态正常，进入下一个STEP
                                    if (currentInRange && !isShort && !isOpen)
                                    {
                                        LaserDriver.SetVoltageAndCurrent(0, 0);
                                        this.NewStep = true;
                                        this.STEP += 1;
                                    }
                                    // 不满足条件，继续等待，不进STEP（等到5s超时报错）
                                }
                            }





                            //if ((DateTime.Now - this.DtDutCheckStart).TotalSeconds >= 2)
                            //{
                            //    bool isOvpTriped = this.LaserDriver.IsOvpTriped();
                            //    double current = this.LaserDriver.GetMeasCurrent();
                            //    double voltage = this.LaserDriver.GetMeasVoltage();
                            //    ErrLog.WriteLine("isOvpTriped=" + isOvpTriped + "; current=" + current + "; voltage=" + voltage);


                            //    //if (Math.Abs(current - 1.0) < 0.3 && voltage > 3.0 && isOvpTriped == false)
                            //    // {
                            //    LaserDriver.SetVoltageAndCurrent(0, 0);

                            //    this.NewStep = true;
                            //    this.STEP += 1;
                            //    //}
                            //    //else if ((DateTime.Now - this.DtDutCheckStart).TotalSeconds >= 5)
                            //    //{
                            //    //    if (voltage <= 1.0 && Math.Abs(current - 1.0) <= 0.3 || current > 1.1)
                            //    //    {
                            //    //        if (this.TestWarn != null)
                            //    //            this.TestWarn(this, new TestWarnEventArgs("负载可能短路，请检查连接状态!"));
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        if (this.TestWarn != null)
                            //    //            this.TestWarn(this, new TestWarnEventArgs("负载可能开路，请检查连接状态!"));
                            //    //    }
                            //    //    LaserDriver.SetVoltageAndCurrent(0, 0);
                            //    //    this.LaserDriver.SetOutput(false);
                            //    //    this.MotorDriver.SetDoorUnlock(true);
                            //    //    this.STEP = 0;
                            //    //    this.NewStep = false;
                            //    //    this.STAT = EnTestStat.STOPED;
                            //    //}
                            //}
                        }
                        break;
                    #endregion

                    #region SET TEMPERATURE
                    case 5:
                        // 设置温度
                        if (this.NewStep)
                        {
                            // 设置温度
                            this.TempDriver.TemperatureSetPoint_DegreeC = this.ROUTINE.TempDrvs[this.TempIndex];
                            this.TempDriver.IsOutputEnabled = true;
                            // 开始计时
                            this.DtTempCtrlStart = DateTime.Now;
                            this.DtTempStableStart = DateTime.Now;
                            // 更新标志
                            this.NewStep = false;
                            this.STAT = EnTestStat.TEMP;
                            // 更新UI
                            if (this.TestTempSetUpdate != null)
                                this.TestTempSetUpdate(this, new TestTempSetUpdateEventArgs(this.ROUTINE.TempDrvs[this.TempIndex]));
                        }
                        else
                        {
                            // 检查控温的时间
                            if ((DateTime.Now - this.DtTempCtrlStart).TotalSeconds > TestSystemUtil.TEMP_TIME_OUT_S)
                            {
                                this.StopOutput();
                                this.MotorDriver.SetDoorUnlock(true);
                                this.STEP = 0;
                                this.NewStep = false;
                                this.STAT = EnTestStat.STOPED;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("无法控制夹具温度到目标值,请检查!"));
                            }
                            else
                            {
                                if (Math.Abs(this.TMP - this.ROUTINE.TempDrvs[this.TempIndex]) < TestSystemUtil.TEMP_ERR_UCL)
                                {
                                    if ((DateTime.Now - this.DtTempStableStart).TotalSeconds > TestSystemUtil.TEMP_STABLE_TIME_S)
                                    {
                                        // 进入下一步
                                        this.NewStep = true;
                                        this.STEP += 1;
                                        // 初始化索引
                                        this.SpIndex = 0;
                                        this.NaIndex = 0;
                                        this.BmIndex = 0;
                                        this.ThmIndex = 0;
                                    }
                                }
                                else
                                {
                                    this.DtTempStableStart = DateTime.Now;
                                }
                            }
                        }
                        break;
                    #endregion

                    #region LIV
                    case 6:
                        // LIV扫描
                        if (this.NewStep)
                        {
                            // 更新标志
                            this.NewStep = false;
                            this.STAT = EnTestStat.LIV;

                            // 设置电压
                            LaserDriver.SetVoltage(ROUTINE.LivScanCfg.PreloadingVoltage);

                            // 启动LIV测试
                            LIV = new StGoLiv();
                            LIV.START = this.ROUTINE.LivScanCfg.Start;
                            LIV.STOP = this.ROUTINE.LivScanCfg.Stop;
                            LIV.DELAY = this.ROUTINE.LivScanCfg.Delay;
                            LIV.STEP = this.ROUTINE.LivScanCfg.Step;
                        }
                        else
                        {
                            // 开始计时
                            //LIV
                            List<StGoPIV> pivs = new List<StGoPIV>();
                            double current = LIV.START;
                            while (current <= LIV.STOP)
                            {
                                this.LaserDriver.SetCurrent(current);
                                Thread.Sleep(LIV.DELAY);
                                current += LIV.STEP;

                                StGoPIV piv = new StGoPIV();
                                piv.LDI = this.LaserDriver.GetMeasCurrent();
                                piv.LDV = this.LaserDriver.GetMeasVoltage();
                                piv.PDI = this.PowerDriver.GetPhotoPower();
                                pivs.Add(piv);
                            }
                            this.CosTestData.LivRawData[this.TempIndex].AddRange(pivs);

                            // 关闭激光器（重复命令，确保关闭）
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    ErrLog.WriteLine("SetCurrent  4");
                            //    Thread.Sleep(500);
                            //    ErrLog.WriteLine("SetCurrent  5");
                            //    if (!this.LaserDriver.GetOutput())
                            //        break;
                            //}
                            Thread.Sleep(600);
                            // 解析LIV结果
                            this.AnalyzeTestData();
                            // 清除界面数据
                            if (this.TestDataUpdate != null)
                                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.CLEAR, this.CosTestData, this.TempIndex));
                            // 更新界面数据
                            if (this.TestDataUpdate != null)
                                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.LIV, this.CosTestData, this.TempIndex, this.ROUTINE.LivScanCfg.IsShowVF, this.ROUTINE.LivScanCfg.IsShowEFF));
                            // 下一步
                            this.STEP += 1;
                            // New Step
                            this.NewStep = true;
                        }
                        break;
                    #endregion

                    #region SCAN SPECTRUM
                    case 7:
                        // 设置电流
                        if (this.NewStep)
                        {
                            if (this.SpDrvIfList.Count == 0)
                            {
                                // 下一步
                                this.STEP += 1;
                            }
                            else if (this.SpIndex >= this.SpDrvIfList.Count)
                            {
                                //// 关闭激光器（重复命令，确保关闭）
                                //for (int i = 0; i < 3; i++)
                                //{
                                //    ErrLog.WriteLine("SetCurrent  6");
                                //    LaserDriver.SetCurrentAndVoltage();
                                //    this.LaserDriver.SetOutput(false);
                                //    Thread.Sleep(1000);
                                //    ErrLog.WriteLine("SetCurrent  7");
                                //    if (!this.LaserDriver.GetOutput())
                                //        break;
                                //}
                                // 解析SP结果
                                this.AnalyzeSpTestData();
                                // 更新界面数据
                                if (this.TestDataUpdate != null)
                                    this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.SPECTRUM, this.CosTestData, this.TempIndex));
                                // 下一步
                                this.STEP += 1;
                            }
                            // 当前条件无测试参数 或者 没有找到对应的驱动电流
                            else if (this.SpDrvIfList[this.SpIndex] <= 0)
                            {
                                // 读取光谱数据
                                StOcSpData spData = new StOcSpData();
                                // 设置驱动名称
                                string unit = this.ROUTINE.SpDrvs[this.SpIndex].DriveType == "Po(W)" ? "W" : "A";
                                spData.DRV = "Spectrum @ " + this.ROUTINE.SpDrvs[this.SpIndex].DriveValue.ToString() + unit;
                                spData.CenterWaveLength = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.Centroid = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.FHWM = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.PeakWaveLength = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.SNR = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.PIB = TestSystemUtil.DEFAULT_TEST_VALUE;
                                spData.Spectrum = new double[0];
                                spData.Wavelength = new double[0];
                                this.CosTestData.SpRawData[this.TempIndex].Add(spData);
                                // 索引增加
                                this.SpIndex++;
                                // 更新标志
                                this.STAT = EnTestStat.SPECTRUM;
                            }
                            // 找到对应的驱动电流
                            else
                            {
                                // 设置电流
                                ErrLog.WriteLine("SetCurrent  8");

                                ////电流分步设置，避免电流过冲
                                //double spDrvIf = this.SpDrvIfList[this.SpIndex];
                                //int stepCount = 5;
                                //for (int i = 1; i <= stepCount; i++)
                                //{
                                //    double curr = spDrvIf * i / stepCount;   // 等分：1/5,2/5,3/5,4/5,5/5
                                //    this.LaserDriver.SetCurrent(curr);
                                //}
                              ;
                                ErrLog.WriteLine($"SCAN SPECTRUM 电流  {LaserDriver.GetCurrent()})");
                                this.LaserDriver.SetCurrent(this.SpDrvIfList[this.SpIndex]);
                                ErrLog.WriteLine("SetCurrent  9");
                                // 使能输出
                                // this.LaserDriver.SetOutput(true);
                                // 计时
                                this.DtSpDriveStart = DateTime.Now;
                                // 更新标志
                                this.NewStep = false;
                                this.STAT = EnTestStat.SPECTRUM;
                            }
                        }
                        else
                        {
                            if ((DateTime.Now - this.DtSpDriveStart).TotalSeconds >= TestSystemUtil.SP_STABLE_TIME_S)
                            {
                                // 设定积分时间(针对自动积分)
                                this.OSA.SetIntegrationTime();
                                // 延时一下
                                Thread.Sleep(50);
                                // 读取光谱数据
                                StOcSpData spData = this.OSA.GetSpectrumData();
                                // 设置驱动名称
                                string unit = this.ROUTINE.SpDrvs[this.SpIndex].DriveType == "Po(W)" ? "W" : "A";
                                spData.DRV = "Spectrum @ " + this.ROUTINE.SpDrvs[this.SpIndex].DriveValue.ToString() + unit;
                                this.CosTestData.SpRawData[this.TempIndex].Add(spData);
                                // 索引增加
                                this.SpIndex++;
                                // New Step
                                this.NewStep = true;
                            }
                        }
                        break;
                    #endregion

                    #region N.A. SCAN - Goto YPANABG rapidly
                    case 8:
                        if (this.NewStep)
                        {
                            if (this.NaDrvIfList.Count == 0)
                            {
                                // 下个步骤
                                this.STEP += 4;
                            }
                            else if (this.NaIndex >= this.NaDrvIfList.Count)
                            {
                                // 判断测试结果
                                this.AnalyzeNaTestData();
                                // 更新界面数据
                                if (this.TestDataUpdate != null)
                                    this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.NASCAN, this.CosTestData, this.TempIndex));
                                // 下个步骤
                                this.STEP += 4;
                            }
                            // 当前条件无测试参数 或者 没有找到对应的驱动电流
                            else if (this.NaDrvIfList[this.NaIndex] <= 0)
                            {
                                StNaData naData = new StNaData();
                                string unit = this.ROUTINE.NaDrvs[this.NaIndex].DriveType == "Po(W)" ? "W" : "A";
                                naData.DRV = "N.A. @ " + this.ROUTINE.NaDrvs[this.NaIndex].DriveValue.ToString() + unit;
                                naData.NA = TestSystemUtil.DEFAULT_TEST_VALUE;
                                this.CosTestData.NaRawData[this.TempIndex].Add(naData);
                                // 索引增加
                                this.NaIndex += 1;
                                // 更新标志
                                this.STAT = EnTestStat.NASCAN;
                            }
                            // 移动到初始位置
                            else
                            {
                                // 重置列表
                                this.NaPowList = new List<double>();
                                this.NaPosList = new List<double>();
                                // 移动到起始扫描点
                                this.MotorDriver.SetSpeedAndTargetPosition(this.YSP, (int)this.YPANABG);
                                this.MotorDriver.FeedPosition();
                                // 更新标志
                                this.NewStep = false;
                                this.STAT = EnTestStat.NASCAN;
                            }
                        }
                        else
                        {
                            // 下一步
                            this.NewStep = true;
                            this.STEP += 1;
                        }
                        break;
                    #endregion

                    #region N.A. SCAN - Wait Motion Power On Laser Dut 
                    case 9:
                        if (this.NewStep)
                        {
                            // 更新标志
                            this.NewStep = false;
                        }
                        else
                        {
                            if (this.MotorDriver.GetMotorStat().Moving == false)
                            {

                                //// 设置输出为ON (重复命令，确保正确)
                                //for (int i = 0; i < 3; i++)
                                //{
                                //    this.LaserDriver.SetOutput(true);
                                //    Thread.Sleep(500);
                                //    if (this.LaserDriver.GetOutput())
                                //        break;
                                //}

                                //// 设置电流
                                //// 电流分步设置，避免电流过冲
                                //double spDrvIf = this.NaDrvIfList[this.NaIndex];
                                //int stepCount = 5;
                                //for (int i = 1; i <= stepCount; i++)
                                //{
                                //    double curr = spDrvIf * i / stepCount;    
                                //    this.LaserDriver.SetCurrent(curr);
                                //}
                                ErrLog.WriteLine($"N.A. SCAN 电流  {LaserDriver.GetCurrent()})");
                                this.LaserDriver.SetCurrent(this.NaDrvIfList[this.NaIndex]);

                                // 下一步
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                        }
                        break;
                    #endregion

                    #region N.A. SCAN - DELAY
                    case 10:
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                        }
                        else
                        {
                            if ((DateTime.Now - this.DtNaDriveStart).TotalSeconds >= TestSystemUtil.NA_STABLE_TIME_S)
                            {
                                // 读取功率
                                this.NaPowList.Add(this.PowerDriver.GetPhotoPower());
                                this.NaPosList.Add(this.MotorDriver.GetRealAbsPosition());
                                // 下一步
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                        }
                        break;
                    #endregion

                    #region N.A. SCAN - SCAN
                    case 11:
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            // 读取功率
                            this.NaPowList.Add(this.PowerDriver.GetPhotoPower());
                            this.NaPosList.Add(this.MotorDriver.GetRealAbsPosition());
                            // 开始移动
                            this.NaScanTimeS = (int)TestSystemUtil.NA_SCAN_TIME_S;
                            this.NaScanTimeS = this.NaScanTimeS > 30 ? 30 : this.NaScanTimeS;
                            this.NaScanTimeS = this.NaScanTimeS < 5 ? 5 : this.NaScanTimeS;
                            ushort count = (ushort)(100 * this.NaScanTimeS);
                            this.MotorDriver.SetSpeedAndTargetPosition(Math.Abs(this.YPANAEND - this.YPANABG) / this.NaScanTimeS, (int)this.YPANAEND);
                            ErrLog.WriteLine($"N.A. SCAN 开始移动  速度：  {Math.Abs(this.YPANAEND - this.YPANABG) / this.NaScanTimeS})   结束位置2.0 * 20000{YPANAEND}");
                            this.MotorDriver.FeedPosition();
                            this.DtNaScanStart = DateTime.Now;
                        }
                        else
                        {
                            int pos_tick = 0;
                            // 读取功率和位置
                            while (this.MotorDriver.GetMotorStat().Moving)
                            {
                                this.NaPowList.Add(this.PowerDriver.GetPhotoPower());
                                this.NaPosList.Add(this.MotorDriver.GetRealAbsPosition());
                                if (this.NaPowList[this.NaPowList.Count - 1] < this.NaPowList[0] * this.NA_PO_THRESH)
                                {
                                    break;
                                }
                                // 判断超时
                                if ((DateTime.Now - this.DtNaScanStart).TotalSeconds >= TestSystemUtil.NA_SCAN_TIME_S + 10.0)
                                    break;
                                // 界面上更新坐标, 避免界面假死
                                if (++pos_tick > 10)
                                {
                                    pos_tick = 0;
                                    if (this.TestMotorUpdate != null)
                                        this.TestMotorUpdate(this, new TestMotorUpdateEventArgs(this.NaPosList[this.NaPosList.Count - 1]));
                                }
                            }
                            //停止运动
                            this.MotorDriver.StopMove();
                            //计算NA值
                            StNaData naData = new StNaData();
                            string unit = this.ROUTINE.NaDrvs[this.NaIndex].DriveType == "Po(W)" ? "W" : "A";
                            naData.DRV = "N.A. @ " + this.ROUTINE.NaDrvs[this.NaIndex].DriveValue.ToString() + unit;
                            double d = this.NA_EQ_DIAMETER / 2;
                            double l = Math.Abs(this.NaPosList[this.NaPosList.Count - 1] - this.NaPosList[0]) + this.NA_ORG_CT_EQ_DIST;
                            naData.NA = d / Math.Sqrt(d * d + l * l); // NA=sin(theta)  theta is half angle
                            this.CosTestData.NaRawData[this.TempIndex].Add(naData);
                            // 索引增加
                            this.NaIndex++;
                            // 下一个NA扫描
                            this.NewStep = true;
                            this.STEP -= 3;
                        }
                        break;
                    #endregion

                    #region N.A. SCAN - HOME
                    case 12:
                        // 复位
                        if (this.NewStep)
                        {
                            if (this.MIRROR == false)
                            {
                                // 设置运动速度，过快会失步
                                this.MotorDriver.SetSpeed(this.YSP);
                                // 设置目标位置为负，决定了复位方向
                                this.MotorDriver.SetTargetPosition(-1.0);
                                // 开始复位
                                this.MotorDriver.SeekHome();
                                this.NewStep = false;
                                this.STAT = EnTestStat.MOVING;
                            }
                            else
                            {
                                this.STEP = 0;
                                this.NewStep = false;
                                this.STAT = EnTestStat.STOPED;
                                this.MotorDriver.SetDoorUnlock(true);
                                this.TempDriver.IsOutputEnabled = false;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未收回,不能移动马达,请检查!"));
                            }
                        }
                        else
                        {
                            StMotorStat stat = this.MotorDriver.GetMotorStat();
                            if (stat.Homing == false || stat.Moving == false)
                            {
                                // 下一步
                                this.STEP += 1;
                                this.NewStep = true;
                            }
                        }
                        break;
                    #endregion

                    #region MIRROR IN
                    case 13:
                        if (this.NewStep)
                        {
                            if (this.BmDrvIfList.Count > 0)
                            {
                                this.NewStep = false;
                                // 使能输出
                                this.MotorDriver.SetMirror(true);
                                // 计时
                                this.DtMirrorMoveStart = DateTime.Now;
                                // 更新标志
                                this.NewStep = false;
                                this.STAT = EnTestStat.BEAMSCAN;
                            }
                            else
                            {
                                //下个步骤
                                this.STEP += 3;
                            }
                        }
                        else
                        {
                            if (this.MotorDriver.GetDiStat().Mirror == true)
                            {
                                // 下一步
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else if ((DateTime.Now - this.DtMirrorMoveStart).TotalSeconds >= 5.0)
                            {
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜无法到位!请检查硬件!"));
                                this.MotorDriver.MotorOff();
                                LaserDriver.SetVoltageAndCurrent(0, 0);
                                this.LaserDriver.SetOutput(false);
                                this.STAT = EnTestStat.STOPED;
                                this.STEP = 0;
                            }
                        }
                        break;
                    #endregion

                    #region SCAN BEAM
                    case 14:
                        // 设置电流
                        if (this.NewStep)
                        {
                           // ErrLog.WriteLine("SCAN BEAM1");

                            if (this.BmIndex >= this.BmDrvIfList.Count)
                            {
                                //// 关闭激光器（重复命令，确保关闭）
                                //for (int i = 0; i < 3; i++)
                                //{
                                //    this.LaserDriver.SetOutput(false);
                                //    Thread.Sleep(500);
                                //    if (!this.LaserDriver.GetOutput())
                                //        break;
                                //}
                                // 解析Beam结果
                                this.AnalyzeBeamTestData();
                                // 更新界面数据
                                if (this.TestDataUpdate != null)
                                    this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.BEAMSCAN, this.CosTestData, this.TempIndex));
                                // 下一步
                                this.STEP += 1;
                            }
                            // 当前条件无测试参数 或者 没有找到对应的驱动电流
                            else if (this.BmDrvIfList[this.BmIndex] <= 0)
                            {
                                //// 读取光谱数据
                                //StOcSpData spData = new StOcSpData();
                                //// 设置驱动名称
                                //string unit = this.ROUTINE.BeamDrvs[this.BmIndex].DriveType == "Po(W)" ? "W" : "A";
                                //spData.DRV = "Beam @ " + this.ROUTINE.BeamDrvs[this.BmIndex].DriveValue.ToString() + unit;
                                //spData.CenterWaveLength = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.Centroid = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.FHWM = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.PeakWaveLength = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.SNR = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.PIB = TestSystemUtil.DEFAULT_TEST_VALUE;
                                //spData.Spectrum = new double[0];
                                //spData.Wavelength = new double[0];
                                //this.CosTestData.SpRawData[this.TempIndex].Add(spData);

                                // 索引增加
                                this.BmIndex++;
                                // 更新标志
                                this.STAT = EnTestStat.BEAMSCAN;
                            }
                            // 找到对应的驱动电流
                            else
                            {
                                // 设置电流
                                this.LaserDriver.SetCurrent(this.BmDrvIfList[this.BmIndex]);
                                // 使能输出
                                //this.LaserDriver.SetOutput(true);
                                // 计时
                                this.DtBmDriveStart = DateTime.Now;
                                // 更新标志
                                this.NewStep = false;
                                this.STAT = EnTestStat.BEAMSCAN;
                            }
                        }
                        else
                        {
                           // ErrLog.WriteLine("SCAN BEAM2");
                            if ((DateTime.Now - this.DtBmDriveStart).TotalSeconds >= TestSystemUtil.BEAMSCAN_STABLE_TIME_S)
                            {
                               // ErrLog.WriteLine("SCAN BEAM3");
                                StBeamData beamData = new StBeamData();
                                // 读取数据
                                Mat matbeam = this.BeamCamDriver.ReadFrame();
                                beamData.BeamImage = matbeam;
                                //ErrLog.WriteLine("SCAN BEAM4");
                                // 设置驱动名称
                                string unit = this.ROUTINE.BeamCamDrvs[this.BmIndex].DriveType == "Po(W)" ? "W" : "A";
                                beamData.DRV = "Beam @ " + this.ROUTINE.BeamCamDrvs[this.BmIndex].DriveValue.ToString() + unit;
                                this.CosTestData.BmRawData[this.TempIndex].Add(beamData);
                                // 索引增加
                                this.BmIndex++;
                                // New Step
                                this.NewStep = true;
                            }
                        }
                        break;
                    #endregion

                    #region MIRROR OUT
                    case 15:
                        if (this.NewStep)
                        {
                            // 使能输出
                            this.MotorDriver.SetMirror(false);
                            // 计时
                            this.DtThmDriveStart = DateTime.Now;
                            // 更新标志
                            this.NewStep = false;
                            this.STAT = EnTestStat.BEAMSCAN;
                        }
                        else
                        {
                            if (this.MotorDriver.GetDiStat().Mirror == false)
                            {
                                // 下一步
                                this.NewStep = true;
                                this.STEP += 1;
                            }
                            else if ((DateTime.Now - this.DtMirrorMoveStart).TotalSeconds >= 5.0)
                            {
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未收回!请检查硬件!"));
                                this.MotorDriver.MotorOff();
                                LaserDriver.SetVoltageAndCurrent(0, 0);
                                this.LaserDriver.SetOutput(false);
                                this.STAT = EnTestStat.STOPED;
                                this.STEP = 0;
                            }
                        }
                        break;
                    #endregion

                    #region SCAN THERMAL IMAGE
                    case 16:
                        // 设置电流
                        if (this.NewStep)
                        {
                            if (!StationUtil.ThermCamEnable || this.ThmDrvIfList.Count == 0)
                            {
                                // 下一步
                                this.STEP += 1;
                            }
                            else if (this.ThmIndex >= this.ThmDrvIfList.Count)
                            {
                                //// 关闭激光器（重复命令，确保关闭）
                                //for (int i = 0; i < 3; i++)
                                //{
                                //    this.LaserDriver.SetOutput(false);
                                //    Thread.Sleep(500);
                                //    if (!this.LaserDriver.GetOutput())
                                //        break;
                                //}
                                // 解析Beam结果
                                this.AnalyzeThermScanTestData();
                                // 更新界面数据
                                if (this.TestDataUpdate != null)
                                    this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.THERMSCAN, this.CosTestData, this.TempIndex));
                                // 下一步
                                this.STEP += 1;
                            }
                            // 当前条件无测试参数 或者 没有找到对应的驱动电流
                            else if (this.ThmDrvIfList[this.ThmIndex] <= 0)
                            {
                                // 索引增加
                                this.ThmIndex++;
                                // 更新标志
                                this.STAT = EnTestStat.THERMSCAN;
                            }
                            // 找到对应的驱动电流
                            else
                            {
                                // 热成像自动聚焦
                                this.ThermCamDriver.AutoFocus();
                                // 设置电流
                                this.LaserDriver.SetCurrent(this.ThmDrvIfList[this.ThmIndex]);
                                // 使能输出
                                // this.LaserDriver.SetOutput(true);
                                // 计时
                                this.DtThmDriveStart = DateTime.Now;
                                // 更新标志
                                this.NewStep = false;
                                this.STAT = EnTestStat.THERMSCAN;
                            }
                        }
                        else
                        {
                            if ((DateTime.Now - this.DtThmDriveStart).TotalSeconds >= TestSystemUtil.THERMSCAN_STABLE_TIME_S)
                            {
                                StThmData thermalCamData = new StThmData();
                                // 读取数据
                                Bitmap imageIrBitmap;
                                Bitmap irInfoBmp;
                                List<StThmAreaMax> tempMaxData;
                                this.ThermCamDriver.ReadFrame(out imageIrBitmap, out irInfoBmp, out tempMaxData);
                                thermalCamData.ThermalImage = irInfoBmp;
                                thermalCamData.TempMaxData = tempMaxData;
                                // 设置驱动名称
                                string unit = this.ROUTINE.ThermCamDrvs[this.ThmIndex].DriveType == "Po(W)" ? "W" : "A";
                                thermalCamData.DRV = "ThermalCam @ " + this.ROUTINE.ThermCamDrvs[this.ThmIndex].DriveValue.ToString() + unit;
                                this.CosTestData.ThmRawData[this.TempIndex].Add(thermalCamData);

                                // 索引增加
                                this.ThmIndex++;
                                // New Step
                                this.NewStep = true;
                            }
                        }
                        break;
                    #endregion

                    #region TEST RESULT JUDGMENT
                    case 17:
                        if (this.NewStep)
                        {
                            LaserDriver.SetVoltageAndCurrent(0, 0);
                            // 关闭激光器（重复命令，确保关闭）
                            for (int i = 0; i < 3; i++)
                            {
                                this.LaserDriver.SetOutput(false);
                                Thread.Sleep(500);
                                if (!this.LaserDriver.GetOutput())
                                    break;
                            }
                            // 判断测试结果
                            this.JudgeTestResult();
                            this.CosTestResult = (this.CosTestData.RESULT == "PASS" && this.TempIndex < this.ROUTINE.TempDrvs.Count - 1) ? "" : this.CosTestData.RESULT;
                            if (this.TestProgressUpdate != null)
                                this.TestProgressUpdate(this, new TestProgressUpdateEventArgs(this.CosTestResult));
                            // 更新界面数据
                            if (this.CosTestStage == TestSystemUtil.POST_TEST && this.TestDataUpdate != null)
                                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.DELTA, this.CosTestData, this.TempIndex));

                            // 标记
                            this.NewStep = false;
                        }
                        else if (this.MotorDriver.GetMotorStat().Moving == false)
                        {
                            // 下一步
                            this.NewStep = true;
                            this.STEP += 1;
                        }
                        break;
                    #endregion

                    #region NEXT TEMPERATURE
                    case 18:
                        // 下一个温度
                        this.TempIndex++;
                        if (this.TempIndex >= this.ROUTINE.TempDrvs.Count)
                        {
                            // 设置成常温
                            this.TempDriver.TemperatureSetPoint_DegreeC = 25.0;
                            this.DtCoolDownStart = DateTime.Now;
                            // 完成
                            this.STEP += 1;
                        }
                        else
                        {
                            // 下一个温度
                            this.STEP = 5;
                        }

                        break;
                    #endregion

                    #region Write Out Test Data
                    case 19:
                        // 输出数据
                        bool bOk = TestResult.Write(this.CosBatchNumber, this.CosSerialNumber, this.CosPartNubmer, this.CosTestStage, this.ROUTINE, this.CosTestData, this.TestWarn);
                        // 启动报表打印
                        if (this.TestReport != null && bOk)
                        {
                            this.TestReport(this, new TestReportEventArgs(this.CosTestData, this.CosSerialNumber, this.CosPartNubmer, this.CosTestStage, this.ROUTINE.Name, this.ROUTINE.LivScanCfg.IsShowVF, this.ROUTINE.LivScanCfg.IsShowEFF, TestResult.TestReportFolder));
                        }
                        this.STEP += 1;
                        break;
                    #endregion

                    #region Jig HOME
                    case 20:
                        // 复位
                        if (this.NewStep)
                        {
                            if (this.MIRROR == false)
                            {
                                // 设置运动速度，过快会失步
                                this.MotorDriver.SetSpeed(this.YSP);
                                // 设置目标位置为负，决定了复位方向
                                this.MotorDriver.SetTargetPosition(-1.0);
                                // 开始复位
                                this.MotorDriver.SeekHome();
                                this.NewStep = false;
                                this.STAT = EnTestStat.MOVING;
                            }
                            else
                            {
                                this.STEP = 0;
                                this.NewStep = false;
                                this.STAT = EnTestStat.STOPED;
                                this.MotorDriver.SetDoorUnlock(true);
                                this.TempDriver.IsOutputEnabled = false;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未收回,不能移动马达,请检查!"));
                            }
                        }
                        else
                        {
                            StMotorStat stat = this.MotorDriver.GetMotorStat();
                            if (stat.Homing == false || stat.Moving == false)
                            {
                                this.STEP += 1;
                                this.STAT = EnTestStat.TEMP;
                            }
                        }
                        break;
                    #endregion

                    #region Jig Cool Down
                    case 21:
                        // 夹具降温
                        if (this.TempDriver.IsTemperatureStabilized || (DateTime.Now - this.DtCoolDownStart).TotalSeconds >= 120)
                        {
                            this.STEP = 0;
                            this.STAT = EnTestStat.FINISHED;
                            this.MotorDriver.SetDoorUnlock(true);
                            this.TempDriver.IsOutputEnabled = false;
                        }
                        break;
                    #endregion



                    default:
                        this.STEP = 0;
                        break;
                }

                // 异常清零
                if ((this.STEP == 0 && this.IdleCount == 0) || (this.STEP > 0))
                    this.ExceptionCount = 0;
            }
            catch (Exception ex)
            {
                // 异常计数
                this.ExceptionCount++;
                // 保存异常信息
                ErrLog.WriteLine(ex.ToString());

                if (this.ExceptionCount == 3)
                {
                    this.MotorDriver.CheckConnection();
                    this.OSA.CheckConnection();
                    this.LaserDriver.CheckConnection();
                    this.TempDriver.CheckConnection();
                    this.PowerDriver.CheckConnection();

                    if (!this.MotorDriver.CONNECTED && this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("无法连接到运动控制卡!请检查硬件设备并重启软件!"));
                    else if (!this.LaserDriver.CONNECTED && this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("无法连接到驱动电源!请检查硬件设备并重启软件!"));
                    else if (!this.OSA.CONNECTED && this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("无法连接到光谱分析仪!请检查硬件设备并重启软件!"));
                    else if (!this.TempDriver.Initialized && this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("无法连接到温度控制器!请检查硬件设备并重启软件!"));
                    else if (!this.PowerDriver.CONNECTED && this.TestWarn != null)
                        this.TestWarn(this, new TestWarnEventArgs("无法连接到功率计!请检查硬件设备并重启软件!"));
                    else
                        this.TestWarn(this, new TestWarnEventArgs("运行异常!请尝试重启软件!"));

                    if (this.MotorDriver.CONNECTED)
                        this.MotorDriver.MotorOff();
                    if (this.LaserDriver.CONNECTED)
                    {
                        LaserDriver.SetVoltageAndCurrent(0, 0);
                        this.LaserDriver.SetOutput(false);
                    }

                    this.STAT = EnTestStat.STOPED;
                    this.STEP = 0;
                }
                else if (this.ExceptionCount > 4)
                {
                    this.ExceptionCount = 4;
                }
            }

        }

        /// <summary>
        /// 加载结果数据
        /// </summary>
        internal void RunLoadResultData()
        {
            //清除命令
            this.COMMAND = EnCommand.NONE;
            //加载数据
            string batch_number, serialNumber, partNumber, testStage, rtName;
            StCosTestData cosTestData;
            TestResult.Read(this.LoadDataFilePath, out batch_number, out serialNumber, out partNumber, out testStage, out rtName, out cosTestData, this.TestWarn);
            if (serialNumber != null && serialNumber != "")
                this.TestDataLoad(this, new TestDataLoadEventArgs(batch_number, serialNumber, partNumber, testStage, rtName));
            else
            {
                if (this.TestWarn != null)
                    this.TestWarn(this, new TestWarnEventArgs("加载测试结果文件失败!"));
                return;
            }

            // 更新界面 - Progress
            this.CosTestResult = cosTestData.RESULT;
            if (this.TestProgressUpdate != null)
                this.TestProgressUpdate(this, new TestProgressUpdateEventArgs(this.CosTestResult));

            // 更新界面数据 - Raw Data
            if (this.TestDataUpdate != null)
            {
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.CLEAR, cosTestData, -1));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.LIV, cosTestData, -1));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.SPECTRUM, cosTestData, -1));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.NASCAN, cosTestData, -1));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.DELTA, cosTestData, -1));
            }

            // 保存内存备份
            this.CosTestData = cosTestData;

            // ......启动报表显示（加载历史数据后也可查看报表）
            if (this.TestReport != null)
            {
                // ROUTINE 在加载历史数据时可能未初始化，LivScanCfg 为默认 struct（IsShowVF/IsShowEFF 默认 false）
                // 改用 false 作为加载历史数据的默认值（不显示 VF/EFF 曲线，避免无数据时报错）
                bool isShowVF = this.ROUTINE.Name != null && this.ROUTINE.Name != "" ? this.ROUTINE.LivScanCfg.IsShowVF : false;
                bool isShowEFF = this.ROUTINE.Name != null && this.ROUTINE.Name != "" ? this.ROUTINE.LivScanCfg.IsShowEFF : false;
                this.TestReport(this, new TestReportEventArgs(cosTestData, serialNumber, partNumber, testStage, rtName, isShowVF, isShowEFF, StationUtil.GetOutFolder()));
            }
        }

        /// <summary>
        /// 显示测试数据
        /// </summary>
        internal void RunShowCosTestData()
        {
            //清除命令
            this.COMMAND = EnCommand.NONE;
            //更新界面
            if (this.TestDataUpdate != null)
            {
                int idx = this.TempIndex < this.CosTestData.TestTemps.Count ? this.TempIndex : -1;
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.CLEAR, this.CosTestData, idx));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.LIV, this.CosTestData, idx, this.ROUTINE.LivScanCfg.IsShowVF, this.ROUTINE.LivScanCfg.IsShowEFF));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.SPECTRUM, this.CosTestData, idx));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.NASCAN, this.CosTestData, idx));
                this.TestDataUpdate(this, new TestDataUpdateEventArgs(EnTestDataAddType.DELTA, this.CosTestData, idx));
            }
        }

        /// <summary>
        /// 插补数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private double Interpolate(double x, double x1, double x2, double y1, double y2)
        {
            if (x == x1)
                return y1;
            if (x == x2)
                return y2;
            if (x1 == x2)
                return (y1 + y2) / 2;

            return ((x - x1) / (x2 - x1) * (y2 - y1) + y1);
        }

        /// <summary>
        /// 根据值和界限判断PASS or FAIL
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ucl"></param>
        /// <param name="lcl"></param>
        /// <returns></returns>
        private string GetResult(double val, double ucl, double lcl)
        {
            return (val > ucl || val < lcl) ? "FAIL" : "PASS";
        }

        /// <summary>
        /// 分析数据
        /// </summary>
        private void AnalyzeTestData()
        {
            #region LIV PARAMETERS
            // 当前的LIV原始数据
            List<StGoPIV> pivList = this.CosTestData.LivRawData[this.TempIndex];

            // 计算参数
            double ith, slope, res;
            double pdOffset = PowerDriver.PO_OFFSET;// 0.185;

            //this.TestWarn(this, new TestWarnEventArgs("pdOffset=" + pdOffset));
            // CosAlgorithm.Calc(pivList, pdOffset, out ith, out slope, out res);
            CosAlgorithm.Calc(pivList, out ith, out slope, out res);
            //this.TestWarn(this, new TestWarnEventArgs("ith=" + ith));

            // 分析结果
            foreach (StCosParam param in this.ROUTINE.LivTestParams)
            {
                if (param.DriveTemp != this.ROUTINE.TempDrvs[this.TempIndex])
                    continue;
                // 数据变量
                StCosParam data = param;
                // 计算ITH
                if (param.ParamType == "Ith(A)")
                {
                    data.TestValue = Math.Round(ith, 3);
                }
                // 计算Slope
                else if (param.ParamType == "Slope(W/A)")
                {
                    data.TestValue = Math.Round(slope, 3);
                }
                // 计算Res
                else if (param.ParamType == "Res(Ω)")
                {
                    data.TestValue = Math.Round(res, 3);
                }
                // 驱动类型 - 电流
                else if (param.DriveType == "If(A)")
                {
                    // 遍历
                    int j = 0;
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].LDI >= param.DriveValue)
                            break;
                    }
                    // 补偿最后的电流
                    if (j == pivList.Count && Math.Abs(pivList[pivList.Count - 1].LDI - this.ROUTINE.LivScanCfg.Stop) <= 1.0)
                        j = pivList.Count - 1;
                    // 计算参数值
                    if (j >= 0 && j < pivList.Count)
                    {
                        if (param.ParamType == "If(A)")
                        {
                            data.TestValue = Math.Round(param.DriveValue, 3);
                        }
                        else if (param.ParamType == "Po(W)")
                        {
                            data.TestValue = Math.Round(this.Interpolate(param.DriveValue, pivList[j - 1].LDI, pivList[j].LDI, pivList[j - 1].PDI, pivList[j].PDI), 3);
                        }
                        else if (param.ParamType == "Vf(V)")
                        {
                            data.TestValue = Math.Round(this.Interpolate(param.DriveValue, pivList[j - 1].LDI, pivList[j].LDI, pivList[j - 1].LDV, pivList[j].LDV), 3);
                        }
                        else if (param.ParamType == "Eff(%)")
                        {
                            double voltage = this.Interpolate(param.DriveValue, pivList[j - 1].LDI, pivList[j].LDI, pivList[j - 1].LDV, pivList[j].LDV);
                            double current = param.DriveValue;
                            double light = this.Interpolate(param.DriveValue, pivList[j - 1].LDI, pivList[j].LDI, pivList[j - 1].PDI, pivList[j].PDI);
                            double epower = voltage * current;
                            if (epower > 0.0)
                                data.TestValue = Math.Round(light / epower * 100, 1);
                            else
                                data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        }
                        else
                        {
                            data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        }
                    }
                    else
                    {
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                    }
                }
                // 驱动类型 - 功率
                else if (param.DriveType == "Po(W)")
                {
                    // 遍历
                    int j = 0;
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].PDI >= param.DriveValue)
                            break;
                    }

                    // 计算结果
                    if (j >= 0 && j < pivList.Count)
                    {
                        if (param.ParamType == "If(A)")
                        {
                            data.TestValue = Math.Round(this.Interpolate(param.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI), 3);
                        }
                        else if (param.ParamType == "Po(W)")
                        {
                            data.TestValue = Math.Round(param.DriveValue, 3);
                        }
                        else if (param.ParamType == "Vf(V)")
                        {
                            data.TestValue = Math.Round(this.Interpolate(param.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDV, pivList[j].LDV), 3);
                        }
                        else if (param.ParamType == "Eff(%)")
                        {
                            double voltage = this.Interpolate(param.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDV, pivList[j].LDV);
                            double current = this.Interpolate(param.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI);
                            double light = param.DriveValue;
                            double epower = voltage * current;
                            if (epower > 0.0)
                                data.TestValue = Math.Round(light / epower * 100, 1);
                            else
                                data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        }
                        else
                        {
                            data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        }
                    }
                    else
                    {
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                    }
                }
                else
                {
                    data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                }
                // 判断PASS/FAIL, 保存结果
                data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                this.CosTestData.LivTestData.Add(data);
            }
            #endregion

            #region Sp Drv
            // 清空
            this.SpDrvIfList.Clear();

            // 计算SpCurrent
            for (int i = 0; i < this.ROUTINE.SpDrvs.Count; i++)
            {
                StCosParam drv = this.ROUTINE.SpDrvs[i];

                // 是否有对应的参数
                bool isValid = false;
                foreach (StCosParam param in this.ROUTINE.SpTestParams)
                {
                    if (param.DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex] && param.DriveType == drv.DriveType && param.DriveValue == drv.DriveValue)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    this.SpDrvIfList.Add(-9999.0);
                    continue;
                }

                // 计算对应的驱动值
                int j = 0;
                if (drv.DriveType == "If(A)")
                {
                    this.SpDrvIfList.Add(drv.DriveValue);
                }
                else if (drv.DriveType == "Po(W)")
                {
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].PDI >= drv.DriveValue)
                            break;
                    }

                    // 保存数据
                    if (j > 0 && j < pivList.Count)
                        this.SpDrvIfList.Add(this.Interpolate(drv.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI));
                    else
                        this.SpDrvIfList.Add(0.0);
                }
            }
            #endregion

            #region Na Drv
            // 清空
            this.NaDrvIfList.Clear();

            // 计算NaCurrent
            for (int i = 0; i < this.ROUTINE.NaDrvs.Count; i++)
            {
                StCosParam drv = this.ROUTINE.NaDrvs[i];

                // 是否有对应的参数
                bool isValid = false;
                foreach (StCosParam param in this.ROUTINE.NaTestParams)
                {
                    if (param.DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex] && param.DriveType == drv.DriveType && param.DriveValue == drv.DriveValue)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    this.NaDrvIfList.Add(-9999.0);
                    continue;
                }

                // 计算对应的驱动值
                int j = 0;
                if (drv.DriveType == "If(A)")
                {
                    this.NaDrvIfList.Add(drv.DriveValue);
                }
                else if (drv.DriveType == "Po(W)")
                {
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].PDI >= drv.DriveValue)
                            break;
                    }

                    // 保存数据
                    if (j > 0 && j < pivList.Count)
                        this.NaDrvIfList.Add(this.Interpolate(drv.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI));
                    else
                        this.NaDrvIfList.Add(0.0);
                }
            }
            #endregion

            #region Bm Drv
            // 清空
            this.BmDrvIfList.Clear();

            // 计算电流Current
            for (int i = 0; i < this.ROUTINE.BeamCamDrvs.Count; i++)
            {
                StCosParam drv = this.ROUTINE.BeamCamDrvs[i];

                //-------当前只抓图，没有参数
                //// 是否有对应的参数
                //bool isValid = false;
                //foreach (StCosParam param in this.ROUTINE.BeamTestParams)
                //{
                //    if (param.DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex] && param.DriveType == drv.DriveType && param.DriveValue == drv.DriveValue)
                //    {
                //        isValid = true;
                //        break;
                //    }
                //}
                //if (!isValid)
                //{
                //    this.BmDrvIfList.Add(-9999.0);
                //    continue;
                //}

                // 计算对应的驱动值
                int j = 0;
                if (drv.DriveType == "If(A)")
                {
                    this.BmDrvIfList.Add(drv.DriveValue);
                }
                else if (drv.DriveType == "Po(W)")
                {
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].PDI >= drv.DriveValue)
                            break;
                    }

                    // 保存数据
                    if (j > 0 && j < pivList.Count)
                        this.BmDrvIfList.Add(this.Interpolate(drv.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI));
                    else
                        this.BmDrvIfList.Add(0.0);
                }
            }
            #endregion

            #region Thm Drv
            // 清空
            this.ThmDrvIfList.Clear();

            // 计算SpCurrent
            for (int i = 0; i < this.ROUTINE.ThermCamDrvs.Count; i++)
            {
                StCosParam drv = this.ROUTINE.ThermCamDrvs[i];
                // 是否有对应的参数
                bool isValid = false;
                foreach (StCosParam param in this.ROUTINE.ThermTestParams)
                {
                    if (param.DriveTemp == this.ROUTINE.TempDrvs[this.TempIndex] && param.DriveType == drv.DriveType && param.DriveValue == drv.DriveValue)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    this.ThmDrvIfList.Add(-9999.0);
                    continue;
                }

                // 计算对应的驱动值
                int j = 0;
                if (drv.DriveType == "If(A)")
                {
                    this.ThmDrvIfList.Add(drv.DriveValue);
                }
                else if (drv.DriveType == "Po(W)")
                {
                    for (j = 0; j < pivList.Count; j++)
                    {
                        if (pivList[j].PDI >= drv.DriveValue)
                            break;
                    }

                    // 保存数据
                    if (j > 0 && j < pivList.Count)
                        this.ThmDrvIfList.Add(this.Interpolate(drv.DriveValue, pivList[j - 1].PDI, pivList[j].PDI, pivList[j - 1].LDI, pivList[j].LDI));
                    else
                        this.ThmDrvIfList.Add(0.0);
                }
            }
            #endregion
        }

        /// <summary>
        /// 分析SP数据
        /// </summary>
        private void AnalyzeSpTestData()
        {
            // 计算光谱参数
            foreach (StCosParam param in this.ROUTINE.SpTestParams)
            {
                if (param.DriveTemp != this.ROUTINE.TempDrvs[this.TempIndex])
                    continue;

                // 搜索驱动和索引
                int index = -1;
                for (int j = 0; j < this.ROUTINE.SpDrvs.Count; j++)
                {
                    if (param.DriveValue == this.ROUTINE.SpDrvs[j].DriveValue && param.DriveType == this.ROUTINE.SpDrvs[j].DriveType)
                    {
                        index = j;
                        break;
                    }
                }

                // 数据变量
                StCosParam data = param;
                // 测试数据
                if (index >= 0)
                {
                    // 光谱数据
                    if (this.CosTestData.SpRawData == null || this.TempIndex >= this.CosTestData.SpRawData.Count)
                    {
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        continue;
                    }
                    List<StOcSpData> spList = this.CosTestData.SpRawData[this.TempIndex];

                    //Peak(nm) 或者 Center(nm) 或者 Centroid(nm) 或者 FWHM(nm) 或者 SNR(dB)
                    if (param.ParamType == "Peak(nm)")
                    {
                        data.TestValue = Math.Round(spList[index].PeakWaveLength, 3);
                    }
                    else if (param.ParamType == "Center(nm)")
                    {
                        data.TestValue = Math.Round(spList[index].CenterWaveLength, 3);
                    }
                    else if (param.ParamType == "Centroid(nm)")
                    {
                        data.TestValue = Math.Round(spList[index].Centroid, 3);
                    }
                    else if (param.ParamType == "FWHM(nm)")
                    {
                        data.TestValue = Math.Round(spList[index].FHWM, 3);
                    }
                    else if (param.ParamType == "SNR(dB)")
                    {
                        data.TestValue = Math.Round(spList[index].SNR, 3);
                    }
                    else if (param.ParamType == "PIB(%)")
                    {
                        data.TestValue = Math.Round(spList[index].PIB, 3);
                    }
                    else
                    {
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                    }
                }
                else
                {
                    data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                }

                // 判断PASS/FAIL, 保存结果
                data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                this.CosTestData.SpTestData.Add(data);
            }
        }

        /// <summary>
        /// 分析NA数据，计算NA值
        /// </summary>
        private void AnalyzeNaTestData()
        {
            // 计算NA参数
            foreach (StCosParam param in this.ROUTINE.NaTestParams)
            {
                if (param.DriveTemp != this.ROUTINE.TempDrvs[this.TempIndex])
                    continue;

                // 搜索驱动和索引
                int index = -1;
                for (int j = 0; j < this.ROUTINE.NaDrvs.Count; j++)
                {
                    if (param.DriveValue == this.ROUTINE.NaDrvs[j].DriveValue && param.DriveType == this.ROUTINE.NaDrvs[j].DriveType)
                    {
                        index = j;
                        break;
                    }
                }

                // 数据变量
                StCosParam data = param;
                // 测试数据
                if (index >= 0)
                {
                    data.TestValue = Math.Round(this.CosTestData.NaRawData[this.TempIndex][index].NA, 3);
                }
                else
                {
                    data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                }

                // 判断PASS/FAIL, 保存结果
                data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                this.CosTestData.NaTestData.Add(data);
            }
        }

        /// <summary>
        /// 分析光斑数据
        /// </summary>
        private void AnalyzeBeamTestData()
        {
            //// 计算光谱参数
            //foreach (StCosParam param in this.ROUTINE.SpTestParams)
            //{
            //    if (param.DriveTemp != this.ROUTINE.TempDrvs[this.TempIndex])
            //        continue;

            //    // 搜索驱动和索引
            //    int index = -1;
            //    for (int j = 0; j < this.ROUTINE.SpDrvs.Count; j++)
            //    {
            //        if (param.DriveValue == this.ROUTINE.SpDrvs[j].DriveValue && param.DriveType == this.ROUTINE.SpDrvs[j].DriveType)
            //        {
            //            index = j;
            //            break;
            //        }
            //    }

            //    // 数据变量
            //    StCosParam data = param;
            //    // 测试数据
            //    if (index >= 0)
            //    {
            //        // 光谱数据
            //        if (this.CosTestData.SpRawData == null || this.TempIndex >= this.CosTestData.SpRawData.Count)
            //        {
            //            data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
            //            continue;
            //        }
            //        List<StOcSpData> spList = this.CosTestData.SpRawData[this.TempIndex];

            //        //Peak(nm) 或者 Center(nm) 或者 Centroid(nm) 或者 FWHM(nm) 或者 SNR(dB)
            //        if (param.ParamType == "Peak(nm)")
            //        {
            //            data.TestValue = Math.Round(spList[index].PeakWaveLength, 3);
            //        }
            //        else if (param.ParamType == "Center(nm)")
            //        {
            //            data.TestValue = Math.Round(spList[index].CenterWaveLength, 3);
            //        }
            //        else if (param.ParamType == "Centroid(nm)")
            //        {
            //            data.TestValue = Math.Round(spList[index].Centroid, 3);
            //        }
            //        else if (param.ParamType == "FWHM(nm)")
            //        {
            //            data.TestValue = Math.Round(spList[index].FHWM, 3);
            //        }
            //        else if (param.ParamType == "SNR(dB)")
            //        {
            //            data.TestValue = Math.Round(spList[index].SNR, 3);
            //        }
            //        else if (param.ParamType == "PIB(%)")
            //        {
            //            data.TestValue = Math.Round(spList[index].PIB, 3);
            //        }
            //        else
            //        {
            //            data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
            //        }
            //    }
            //    else
            //    {
            //        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
            //    }

            //    // 判断PASS/FAIL, 保存结果
            //    data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
            //    this.CosTestData.SpTestData.Add(data);
            //}
        }

        /// <summary>
        /// 分析热成像数据
        /// </summary>
        private void AnalyzeThermScanTestData()
        {
            // 计算光谱参数
            foreach (StCosParam param in this.ROUTINE.ThermTestParams)
            {
                if (param.DriveTemp != this.ROUTINE.TempDrvs[this.TempIndex])
                    continue;

                // 搜索驱动和索引
                int index = -1;
                for (int j = 0; j < this.ROUTINE.ThermCamDrvs.Count; j++)
                {
                    if (param.DriveValue == this.ROUTINE.ThermCamDrvs[j].DriveValue && param.DriveType == this.ROUTINE.ThermCamDrvs[j].DriveType)
                    {
                        index = j;
                        break;
                    }
                }

                // 数据变量
                StCosParam data = param;
                // 测试数据
                if (index >= 0)
                {
                    // 温度测量数据
                    if (this.CosTestData.ThmRawData == null || this.TempIndex >= this.CosTestData.ThmRawData.Count || index >= this.CosTestData.ThmRawData[this.TempIndex].Count)
                    {
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        continue;
                    }

                    // 温度数据
                    foreach (StThmAreaMax tempMax in this.CosTestData.ThmRawData[this.TempIndex][index].TempMaxData)
                    {
                        if (param.ParamType == tempMax.AreaName)
                        {
                            data.TestValue = Math.Round(tempMax.AreaTempMax, 3);
                        }
                    }
                }
                else
                {
                    data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                }

                // 判断PASS/FAIL, 保存结果
                data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                this.CosTestData.ThmTestData.Add(data);
            }
        }

        /// <summary>
        /// 判断测试结果
        /// </summary>
        private void JudgeTestResult()
        {
            this.CosTestData.RESULT = "PASS";

            // LIV Test Data
            for (int i = 0; i < this.CosTestData.LivTestData.Count; i++)
            {
                if (this.CosTestData.LivTestData[i].TestResult != "PASS")
                {
                    this.CosTestData.RESULT = "FAIL";
                    break;
                }
            }

            // SP Test Data
            for (int i = 0; i < this.CosTestData.SpTestData.Count; i++)
            {
                if (this.CosTestData.SpTestData[i].TestResult != "PASS")
                {
                    this.CosTestData.RESULT = "FAIL";
                    break;
                }
            }

            // N.A. Test Data
            for (int i = 0; i < this.CosTestData.NaTestData.Count; i++)
            {
                if (this.CosTestData.NaTestData[i].TestResult != "PASS")
                {
                    this.CosTestData.RESULT = "FAIL";
                    break;
                }
            }

            // Delta Test Data
            if (this.CosTestStage == TestSystemUtil.POST_TEST)
            {
                // 未找到Pre数据
                if (this.CosPreTestData.SerialNumber == "" || this.CosPreTestData.SerialNumber == null)
                {
                    foreach (StCosParam param in this.ROUTINE.DeltaParams)
                    {
                        // 数据变量
                        StCosParam data = param;
                        data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        // 判断PASS/FAIL, 保存结果
                        data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                        this.CosTestData.DeltaTestData.Add(data);
                    }
                }
                // 找到Pre数据
                else
                {
                    foreach (StCosParam param in this.ROUTINE.DeltaParams)
                    {
                        // 判断Delta参数是否存在的
                        bool deltaPrmExisted = false;
                        foreach (StCosParam prm in this.CosTestData.DeltaTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                deltaPrmExisted = true;
                                break;
                            }
                        }
                        if (deltaPrmExisted)
                            continue;


                        // 数据变量
                        StCosParam data = param;
                        // PreTest变量
                        StCosParam predata = new StCosParam();

                        // PreTest参数，所有合并在LivTestData中
                        foreach (StCosParam prm in this.CosPreTestData.LivTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                predata = prm;
                                break;
                            }
                        }
                        // PostTest变量
                        StCosParam postdata = new StCosParam();
                        foreach (StCosParam prm in this.CosTestData.LivTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                postdata = prm;
                                break;
                            }
                        }
                        foreach (StCosParam prm in this.CosTestData.SpTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                postdata = prm;
                                break;
                            }
                        }
                        foreach (StCosParam prm in this.CosTestData.NaTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                postdata = prm;
                                break;
                            }
                        }
                        foreach (StCosParam prm in this.CosTestData.BmTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                postdata = prm;
                                break;
                            }
                        }
                        foreach (StCosParam prm in this.CosTestData.ThmTestData)
                        {
                            if (param.Name == prm.Name)
                            {
                                postdata = prm;
                                break;
                            }
                        }
                        // 检查变量是否找到
                        if (predata.Name == null || postdata.Name == null)
                        {
                            // 数据变量
                            data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                        }
                        else
                        {
                            if (param.ParamType == "%")
                            {
                                ErrLog.WriteLine(predata.Name + "; " + predata.TestValue);

                                if (predata.TestValue == 0)
                                    data.TestValue = TestSystemUtil.DEFAULT_TEST_VALUE;
                                else
                                    data.TestValue = ((postdata.TestValue / predata.TestValue) - 1.0) * 100.0;
                            }
                            else
                            {
                                data.TestValue = postdata.TestValue - predata.TestValue;
                            }
                        }
                        // 判断PASS/FAIL, 保存结果
                        data.TestResult = this.GetResult(data.TestValue, data.ParamUCL, data.ParamLCL);
                        if (data.TestResult != "PASS")
                            this.CosTestData.RESULT = "FAIL";
                        this.CosTestData.DeltaTestData.Add(data);
                    }
                }
            }

        }

        private double CalcNA(List<double> poList)
        {
            if (poList.Count < 100)
                return TestSystemUtil.DEFAULT_TEST_VALUE;

            //// 输出原始数据
            //string strLine = "";
            //for (int i = 0; i < poList.Count; i++)
            //    strLine += (i + "," + poList[i]+ "\r\n");
            //ErrLog.WriteLine(strLine);

            // 微分
            double[] der = new double[poList.Count];
            for (int i = 15; i < poList.Count - 15; i++)
            {
                if (poList[i - 3] <= 0)
                    der[i] = 0.0;
                else
                    der[i] = (poList[i + 3] - poList[i - 3]) / poList[i - 3];
            }

            // 最小微分
            double minDeri = 9999;
            int iMinDeri = -1;
            for (int i = 0; i < der.Length; i++)
            {
                if (der[i] < minDeri)
                {
                    minDeri = der[i];
                    iMinDeri = i;
                }
            }

            // 功率开始降低的位置
            int iStart = 0;
            for (int i = iMinDeri; i >= 0; i--)
            {
                if (Math.Abs(der[i]) < 0.0005)
                {
                    iStart = i;
                    break;
                }
            }

            // 功率停止下降的位置
            int iStop = 0;
            for (int i = iMinDeri; i < der.Length; i++)
            {
                if (Math.Abs(der[i]) < 0.0005)
                {
                    iStop = i;
                    break;
                }
            }

            // 检测是否找到
            if (iStart == 0 || iStop == 0)
                return TestSystemUtil.DEFAULT_TEST_VALUE;

            // P100
            double sum = 0.0;
            for (int i = iStart - 3; i <= iStart + 3; i++)
                sum += poList[i];
            double p100 = sum / 7;

            // P0
            sum = 0.0;
            for (int i = iStop - 3; i <= iStop + 3; i++)
                sum += poList[i];
            double p0 = sum / 7;

            // P95
            double p95 = p0 + (p100 - p0) * 0.95;
            int idx95 = 0;
            for (int i = iStart; i <= iStop; i++)
            {
                if (poList[i] <= p95)
                {
                    idx95 = i;
                    break;
                }
            }

            // P5
            double p5 = p0 + (p100 - p0) * 0.05;
            int idx5 = 0;
            for (int i = idx95; i <= iStop; i++)
            {
                if (poList[i] <= p5)
                {
                    idx5 = i;
                    break;
                }
            }

            // NA
            double r = Math.Abs((this.YPANAEND - this.YPANABG) / this.NaScanTimeS * (idx95 - idx5) / 100.0) / 2;
            double h = Math.Abs(TestSystemUtil.NA_ORG_CT_EQ_DIST);
            double na = r / Math.Sqrt(r * r + h * h);  // N.A. = Sin(theta)  
            return na;
        }


        /// <summary>
        /// 更新运行状态
        /// </summary>
        /// <param name="stat"></param>
        private void SetTestStat(EnTestStat stat)
        {
            if (this.TestStatUpdate != null)
                this.TestStatUpdate(this, new TestStatEventArgs(stat));
        }

        /// <summary>
        /// 更新ROUTINE
        /// </summary>
        /// <param name="rtName"></param>
        private void SetRoutine(string rtName)
        {
            // 深拷贝
            this.ROUTINE = TestScriptUtil.ROUTINE_DICT[rtName];
            this.ROUTINE.Name = rtName;
            this.ROUTINE.LivScanCfg = TestScriptUtil.ROUTINE_DICT[rtName].LivScanCfg;
            this.ROUTINE.SpScanCfg = TestScriptUtil.ROUTINE_DICT[rtName].SpScanCfg;
            this.ROUTINE.TempDrvs = new List<double>(TestScriptUtil.ROUTINE_DICT[rtName].TempDrvs);
            this.ROUTINE.TempDrvs.Sort();
            this.ROUTINE.SpDrvs = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].SpDrvs);
            this.ROUTINE.NaDrvs = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].NaDrvs);
            this.ROUTINE.BeamCamDrvs = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].BeamCamDrvs);
            this.ROUTINE.ThermCamDrvs = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].ThermCamDrvs);

            this.ROUTINE.LivTestParams = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].LivTestParams);
            this.ROUTINE.SpTestParams = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].SpTestParams);
            this.ROUTINE.NaTestParams = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].NaTestParams);
            this.ROUTINE.BeamTestParams = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].BeamTestParams);
            this.ROUTINE.ThermTestParams = new List<StCosParam>(TestScriptUtil.ROUTINE_DICT[rtName].ThermTestParams);
            // SpDrv
            this.SpDrvIfList = new List<double>();
            // NaDrv
            this.NaDrvIfList = new List<double>();
            // BmDrv
            this.BmDrvIfList = new List<double>();
            // ThmDrv
            this.ThmDrvIfList = new List<double>();
        }

        /// <summary>
        /// 启动 （UI线程）
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="serialNo"></param>
        /// <param name="partNo"></param>
        /// <param name="testStage"></param>
        /// <param name="rtName"></param>
        internal void Start(string batchNo, string serialNo, string partNo, string testStage, string rtName)
        {
            // Check if Running
            if (this.STEP != 0 || this.COMMAND != EnCommand.NONE)
            {
                WarnWindow.ShowMsg("程序仍在运行! 不起启动测试!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }

            // Check STAT
            if (this.STAT == EnTestStat.LIV || this.STAT == EnTestStat.MOVING || this.STAT == EnTestStat.SPECTRUM || this.STAT == EnTestStat.NASCAN || this.STAT == EnTestStat.TEMP)
            {
                WarnWindow.ShowMsg("程序仍在运行! 不起启动测试!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }

            // Batch Number
            if (batchNo == null || batchNo.Trim() == "")
            {
                WarnWindow.ShowMsg("批次号为空! 不起启动测试!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            this.CosBatchNumber = batchNo;

            // Serial Number
            if (serialNo == null || serialNo.Trim() == "")
            {
                WarnWindow.ShowMsg("序列号为空! 不起启动测试!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            this.CosSerialNumber = serialNo;

            // Is Pre Tested
            if (testStage == TestSystemUtil.POST_TEST)
            {
                string preTestFile = TestResult.GetPreTestFile(batchNo, serialNo);
                if (preTestFile == "")
                {
                    WarnWindow.ShowMsg("没有找到Pre Test的数据文件! 不起启动测试!");
                    EnTestStat stat = this.STAT;
                    this.STAT = stat;
                    return;

                    //// 没有PreTest数据,依然继续运行
                    //this.CosPreTestData = new StCosTestData();
                }
                else
                {
                    string preBatchNumber, preSerialNumber, prePartNumber, preTestStage, preScriptName;
                    TestResult.Read(preTestFile, out preBatchNumber, out preSerialNumber, out prePartNumber, out preTestStage, out preScriptName, out this.CosPreTestData, null);

                    if (this.CosPreTestData.SerialNumber == "" || this.CosPreTestData.SerialNumber == null)
                    {
                        WarnWindow.ShowMsg("Pre Test的数据异常! 不起启动测试!");
                        EnTestStat stat = this.STAT;
                        this.STAT = stat;
                        return;
                    }
                }
            }

            // Param
            this.CosSerialNumber = serialNo;
            this.CosPartNubmer = partNo;
            this.CosTestStage = testStage;
            this.SetRoutine(rtName);

            // Start
            this.COMMAND = EnCommand.START;
        }

        /// <summary>
        /// 停止 （UI线程） 
        /// </summary>
        internal void Stop()
        {
            // Check if Running or Paused
            if (this.STEP == 0 && this.STAT != EnTestStat.PAUSED)
            {
                WarnWindow.ShowMsg("测试没有运行! 无需停止!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            // Check STAT
            if (this.STAT != EnTestStat.MOVING && this.STAT != EnTestStat.LIV && this.STAT != EnTestStat.SPECTRUM && this.STAT != EnTestStat.TEMP && this.STAT != EnTestStat.NASCAN && this.STAT != EnTestStat.PAUSED)
            {
                WarnWindow.ShowMsg("测试没有运行! 无需停止!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            // Stop
            this.COMMAND = EnCommand.STOP;
        }

        /// <summary>
        /// 重启 （UI线程） 
        /// </summary>
        internal void Resume()
        {
            // Check if Paused
            if (this.STAT != EnTestStat.PAUSED)
            {
                WarnWindow.ShowMsg("测试没有处于暂停状态! 不能重启!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            // Resume
            this.COMMAND = EnCommand.RESUME;
        }

        /// <summary>
        /// 暂停 （UI线程） 
        /// </summary>
        internal void Pause()
        {
            // Check if Running
            if (this.STEP == 0)
            {
                WarnWindow.ShowMsg("测试没有运行! 不能暂停!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }
            // Check STAT
            if (this.STAT != EnTestStat.MOVING && this.STAT != EnTestStat.LIV && this.STAT != EnTestStat.SPECTRUM && this.STAT != EnTestStat.NASCAN && this.STAT != EnTestStat.TEMP)
            {
                WarnWindow.ShowMsg("测试没有运行! 不能暂停!");
                EnTestStat stat = this.STAT;
                this.STAT = stat;
                return;
            }

            this.COMMAND = EnCommand.PAUSE;
        }

        internal void MotorOn()
        {
            this.COMMAND = EnCommand.PWRON;
        }

        internal void MotorOff()
        {
            this.COMMAND = EnCommand.PWROFF;
        }

        internal void ShowCosTestData()
        {
            this.COMMAND = EnCommand.SHOWDATA;
            this.TempIndex = 9999;
        }

        internal void LoadResultData(string filePath)
        {
            this.COMMAND = EnCommand.LOADDATA;
            this.LoadDataFilePath = filePath;
        }

        internal void OpenResultFiles()
        {
            this.COMMAND = EnCommand.OPENFILE;
        }

        ///// <summary>
        ///// 检查LIV数据是否有误
        ///// </summary>
        ///// <returns></returns>
        //private bool IsLivError()
        //{
        //    // 当前的LIV原始数据
        //    List<StGoPIV> pivList = this.CosTestData.LivRawData[this.TempIndex];

        //    if (pivList == null || pivList.Count == 0)
        //        return true;

        //    // LDI > 0.3A
        //    int i = 0;
        //    while (i < pivList.Count)
        //    {
        //        if (pivList[i].LDI > 0.3)
        //            break;
        //        i++;
        //    }
        //    if (i == pivList.Count)
        //        return true;
        //    // LDI or LDV becomes too low  
        //    while (i < pivList.Count)
        //    {
        //        if (pivList[i].LDI < 0.3 || pivList[i].LDV < 0.5)
        //            return true;
        //        i++;
        //    }

        //    // PDI饱和
        //    i = pivList.Count - 1;
        //    double power = pivList[i].PDI;
        //    double current = pivList[i].LDI;
        //    bool isSat = true;
        //    i -= 1;
        //    while (i > 0 && pivList[i].LDI > current - 1.0)
        //    {
        //        if (power != pivList[i].PDI)
        //        {
        //            isSat = false;
        //            break;
        //        }
        //        i--;
        //    }
        //    if (i == pivList.Count - 2)
        //        isSat = false;

        //    // 电压不稳定
        //    if (CosAlgorithm.CalcVoltKink(pivList) == false)
        //        isSat = true;

        //    return isSat;
        //}

        /// <summary>
        /// 停止电源和温控的输出
        /// </summary>
        private void StopOutput()
        {
            LaserDriver.SetVoltageAndCurrent(0, 0);
            this.LaserDriver.SetOutput(false);
            this.TempDriver.IsOutputEnabled = false;
        }
    }

    /// <summary>
    /// Test Stat Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestStatHandler(object sender, TestStatEventArgs args);

    /// <summary>
    /// Test State Event
    /// </summary>
    class TestStatEventArgs : EventArgs
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        internal EnTestStat STAT { get; set; }

        internal TestStatEventArgs(EnTestStat stat)
        {
            this.STAT = stat;
        }
    }

    /// <summary>
    /// Test DI State Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestDiStateUpdateHandler(object sender, TestDiStateUpdateEventArgs args);

    /// <summary>
    /// Test DI Ste Update Event
    /// </summary>
    class TestDiStateUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 互锁状态
        /// </summary>
        internal bool INTERLOCK { get; set; }
        /// <summary>
        /// 复位状态
        /// </summary>
        internal bool HOME { get; set; }
        /// <summary>
        /// 反射镜状态
        /// </summary>
        internal bool MIRROR { get; set; }

        internal TestDiStateUpdateEventArgs(bool interlock, bool home, bool mirror)
        {
            this.INTERLOCK = interlock;
            this.HOME = home;
            this.MIRROR = mirror;
        }
    }

    /// <summary>
    /// Test Motor Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestMotorUpdateHandler(object sender, TestMotorUpdateEventArgs args);

    /// <summary>
    /// Test Motor Update Event
    /// </summary>
    class TestMotorUpdateEventArgs : EventArgs
    {

        /// <summary>
        /// Y轴坐标
        /// </summary>
        internal double POSY { get; set; }

        internal TestMotorUpdateEventArgs(double posy)
        {
            this.POSY = Math.Round(posy, 2);
        }
    }

    /// <summary>
    /// Test Jig Temerature Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestJigTemperatureUpdateHandler(object sender, TestJigTemperatureEventArgs args);

    /// <summary>
    /// Test Jig Temerature Update Event
    /// </summary>
    class TestJigTemperatureEventArgs : EventArgs
    {
        /// <summary>
        /// 夹具温度1
        /// </summary>
        internal double TMP { get; set; }


        internal TestJigTemperatureEventArgs(double tmp)
        {
            this.TMP = tmp;
        }
    }

    /// <summary>
    /// Test Position Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestPositionUpdateHandler(object sender, TestPositionUpdateEventArgs args);

    /// <summary>
    /// Test Position Update Event
    /// </summary>
    class TestPositionUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 夹具坐在位置
        /// </summary>
        internal string POSITION { get; set; }

        internal TestPositionUpdateEventArgs(string position)
        {
            this.POSITION = position;
        }
    }

    /// <summary>
    /// Test Position Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestTempSetUpdateHandler(object sender, TestTempSetUpdateEventArgs args);

    /// <summary>
    /// Test Temperature Set Update Event
    /// </summary>
    class TestTempSetUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 设定温度
        /// </summary>
        internal double TSET { get; set; }

        internal TestTempSetUpdateEventArgs(double tset)
        {
            this.TSET = tset;
        }
    }

    /// <summary>
    /// Test Data Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestDataUpdateHandler(object sender, TestDataUpdateEventArgs args);

    enum EnTestDataAddType
    {
        CLEAR,
        LIV,
        SPECTRUM,
        NASCAN,
        BEAMSCAN,
        THERMSCAN,
        DELTA
    }

    /// <summary>
    /// Test State Event
    /// </summary>
    class TestDataUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        internal StCosTestData CosTestData { get; set; }
        /// <summary>
        /// 测试温度索引
        /// </summary>
        internal int TempIndex { get; set; }
        /// <summary>
        /// 增加类型
        /// </summary>
        internal EnTestDataAddType AddType { get; set; }
        /// <summary>
        /// 是否显示VF曲线
        /// </summary>
        internal bool IsShowVF { get; set; }
        /// <summary>
        /// 是否显示EFF曲线
        /// </summary>
        internal bool IsShowEFF { get; set; }

        internal TestDataUpdateEventArgs(EnTestDataAddType addType, StCosTestData cosTestData, int tempIndex, bool isShowVF = false, bool isShowEFF = false)
        {
            this.AddType = addType;
            this.CosTestData = cosTestData;
            this.TempIndex = tempIndex;
            this.IsShowVF = isShowVF;
            this.IsShowEFF = isShowEFF;
        }
    }

    /// <summary>
    /// Test Report Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestReportHandler(object sender, TestReportEventArgs args);

    /// <summary>
    /// Test Report Event
    /// </summary>
    class TestReportEventArgs : EventArgs
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        internal StCosTestData CosTestData { get; set; }

        internal string SerialNo { get; set; }

        internal string PartNo { get; set; }

        internal string TestRoutine { get; set; }

        internal string TestStage { get; set; }

        internal string DstFolder { get; set; }

        internal bool IsShowVF { get; set; }

        internal bool IsShowEFF { get; set; }

        internal TestReportEventArgs(StCosTestData cosTestData, string serialNo, string partNo, string testStage, string testRoutine, bool isShowVF, bool isShowEFF, string dstFolder)
        {
            this.CosTestData = cosTestData;
            this.SerialNo = serialNo;
            this.PartNo = partNo;
            this.DstFolder = dstFolder;
            this.TestStage = testStage;
            this.TestRoutine = testRoutine;
            this.IsShowEFF = isShowEFF;
            this.IsShowVF = isShowVF;
        }
    }

    /// <summary>
    /// Test Progress Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestProgessUpdateHandler(object sender, TestProgressUpdateEventArgs args);

    /// <summary>
    /// TestProgressUpdateEventArgs
    /// </summary>
    class TestProgressUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 测试结果
        /// </summary>
        internal string CosResult { get; set; }


        internal TestProgressUpdateEventArgs(string cosResult)
        {
            this.CosResult = cosResult;
        }
    }

    /// <summary>
    /// Test Warn Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestWarnHandler(object sender, TestWarnEventArgs args);

    /// <summary>
    /// Test Warn 
    /// </summary>
    class TestWarnEventArgs : EventArgs
    {
        /// <summary>
        /// Warn
        /// </summary>
        internal string Msg { get; set; }

        internal TestWarnEventArgs(string msg)
        {
            this.Msg = msg;
        }
    }

    /// <summary>
    /// Test Progress Update Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void TestDataLoadHandler(object sender, TestDataLoadEventArgs args);

    /// <summary>
    /// TestDataLoadEventArgs
    /// </summary>
    class TestDataLoadEventArgs : EventArgs
    {
        internal string PartNumber { get; set; }

        internal string TestStage { get; set; }

        internal string RoutineName { get; set; }

        internal string BatchNumber { get; set; }

        internal string SerialNumber { get; set; }

        internal TestDataLoadEventArgs(string batchNo, string serialNo, string partNumber, string testStage, string rtName)
        {
            this.BatchNumber = batchNo;
            this.SerialNumber = serialNo;
            this.PartNumber = partNumber;
            this.TestStage = testStage;
            this.RoutineName = rtName;
        }
    }

    /// <summary>
    /// 运行状态
    /// </summary>
    enum EnTestStat
    {
        //PWRON,
        //PWROFF,
        //HOMED,
        STOPED,
        PAUSED,
        FINISHED,
        MOVING,
        TEMP,
        LIV,
        SPECTRUM,
        NASCAN,
        BEAMSCAN,
        THERMSCAN
    }

    /// <summary>
    /// 命令
    /// </summary>
    enum EnCommand
    {
        NONE,
        HOME,
        START,
        STOP,
        RESUME,
        PAUSE,
        PARK,
        PWROFF,
        PWRON,
        SHOWDATA,
        LOADDATA,
        OPENFILE
    }


    public struct StCosTestData
    {
        /// <summary>
        /// 批次号
        /// </summary>
        internal string BatchNumber;
        /// <summary>
        /// 序列号
        /// </summary>
        internal string SerialNumber;
        /// <summary>
        /// 型号
        /// </summary>
        internal string PartNumber;
        /// <summary>
        /// 测试工序
        /// </summary>
        internal string TestStage;
        /// <summary>
        /// 测试脚本名称
        /// </summary>
        internal string RoutineName;
        /// <summary>
        /// 测试时间
        /// </summary>
        internal string TestDateTime;
        /// <summary>
        /// 测试用户名
        /// </summary>
        internal string TestUserName;
        /// <summary>
        /// 测试温度
        /// </summary>
        internal List<double> TestTemps;
        /// <summary>
        /// LIV扫描原始结果
        /// </summary>
        internal List<List<StGoPIV>> LivRawData;
        /// <summary>
        /// 光谱扫描原始结果
        /// </summary>
        internal List<List<StOcSpData>> SpRawData;
        /// <summary>
        /// NA扫描原始结果
        /// </summary>
        internal List<List<StNaData>> NaRawData;
        /// <summary>
        /// 光斑扫描原始结果
        /// </summary>
        internal List<List<StBeamData>> BmRawData;
        /// <summary>
        /// 热成像扫描原始结果
        /// </summary>
        internal List<List<StThmData>> ThmRawData;

        /// <summary>
        /// LIV结果数据
        /// </summary>
        internal List<StCosParam> LivTestData;
        /// <summary>
        /// 光谱结果
        /// </summary>
        internal List<StCosParam> SpTestData;
        /// <summary>
        /// 数值孔径参数列表
        /// </summary>
        internal List<StCosParam> NaTestData;
        /// <summary>
        /// 光斑扫描参数列表
        /// </summary>
        internal List<StCosParam> BmTestData;
        /// <summary>
        /// 热成像扫描参数列表
        /// </summary>
        internal List<StCosParam> ThmTestData;
        /// <summary>
        /// 参数差异列表
        /// </summary>
        internal List<StCosParam> DeltaTestData;
        /// <summary>
        /// 整体测试结果PASS, FAIL
        /// </summary>
        internal string RESULT;
    }

    /// <summary>
    /// OceanOptics光谱扫描数据
    /// </summary>
    struct StOcSpData
    {
        /// <summary>
        /// 驱动条件
        /// </summary>
        internal string DRV;
        /// <summary>
        /// 光谱峰值数据
        /// </summary>
        internal double[] Spectrum;
        /// <summary>
        /// 光谱波长数据
        /// </summary>
        internal double[] Wavelength;
        /// <summary>
        /// SNR
        /// </summary>
        internal double SNR;
        /// <summary>
        /// PIB
        /// </summary>
        internal double PIB;
        /// <summary>
        /// FHWM
        /// </summary>
        internal double FHWM;
        /// <summary>
        /// Centroid
        /// </summary>
        internal double Centroid;
        /// <summary>
        /// PeakWaveLength
        /// </summary>
        internal double PeakWaveLength;
        /// <summary>
        /// CenterWaveLength
        /// </summary>
        internal double CenterWaveLength;
    }

    /// <summary>
    /// N.A.扫描数据
    /// </summary>
    struct StNaData
    {
        /// <summary>
        /// 驱动条件
        /// </summary>
        internal string DRV;
        /// <summary>
        /// N.A.值
        /// </summary>
        internal double NA;
    }

    /// <summary>
    /// 光斑扫描数据
    /// </summary>
    struct StBeamData
    {
        /// <summary>
        /// 驱动条件
        /// </summary>
        internal string DRV;
        /// <summary>
        /// 图像照片
        /// </summary>
        internal Mat BeamImage;
    }

    /// <summary>
    /// 热成像扫描数据
    /// </summary>
    struct StThmData
    {
        /// <summary>
        /// 驱动条件
        /// </summary>
        internal string DRV;
        /// <summary>
        /// 图像照片
        /// </summary>
        internal Bitmap ThermalImage;
        /// <summary>
        /// 温度数据
        /// </summary>
        internal List<StThmAreaMax> TempMaxData;
    }

    /// <summary>
    /// 热成像温度数据
    /// </summary>
    struct StThmAreaMax
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        internal string AreaName;
        /// <summary>
        /// 最高温度
        /// </summary>
        internal double AreaTempMax;
    }


    struct StCosParam
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal string Name;

        /// <summary>
        /// 驱动电流或功率值
        /// </summary>
        internal double DriveValue;
        /// <summary>
        /// 驱动类型
        /// "If(A)" 或者 "Po(W)"
        /// </summary>
        internal string DriveType;
        /// <summary>
        /// 驱动温度
        /// </summary>
        internal double DriveTemp;

        /// <summary>
        /// 参数类型
        /// "If(A)" 或者 "Po(W)" 或者 "Vf(V)" 或者 "Eff(%)" 或者 "Res(Ω)" 
        /// Peak(nm) 或者 Center(nm) 或者 Centroid(nm) 或者 FWHM(nm) 或者 SNR(dB)
        /// </summary>
        internal string ParamType;
        /// <summary>
        /// 参数上限
        /// </summary>
        internal double ParamUCL;
        /// <summary>
        /// 参数下限
        /// </summary>
        internal double ParamLCL;

        /// <summary>
        /// 结果数据
        /// </summary>
        internal double TestValue;
        /// <summary>
        /// Pass / Fail / N/A
        /// </summary>
        internal string TestResult;

        /// <summary>
        /// 是否显示在报表中
        /// </summary>
        internal bool IsReport;
    }

    struct StLivScanCfg
    {
        /// <summary>
        /// 预加载电压上限(V)
        /// </summary>
        internal double PreloadingVoltage;
        /// <summary>
        /// 同短路判定预加设定电流(A)
        /// </summary>
        public double PreloadingCurrent;      
        /// <summary>
        /// 短路电压阈值：低于这个值判定短路
        /// </summary>
        public double ShortVoltThresh;          
        /// <summary>
        /// 开路电流阈值：低于这个值判定开路
        /// </summary>
        public double OpenCurrentThresh;       
        /// <summary>
        /// 电流容差
        /// </summary>
        public double CurrentTol;              



        /// <summary>
        /// 起始电流(A)
        /// </summary>
        internal double Start;
        /// <summary>
        /// 步进电流(A)
        /// </summary>
        internal double Step;
        /// <summary>
        /// 结束电流(A)
        /// </summary>
        internal double Stop;
        /// <summary>
        /// 延时(ms)
        /// </summary>
        internal int Delay;
        /// <summary>
        /// 是否显示VF
        /// </summary>
        internal bool IsShowVF;
        /// <summary>
        /// 是否显示EFF
        /// </summary>
        internal bool IsShowEFF;
    }

    struct StSpScanCfg
    {
        /// <summary>
        /// 基准波长(nm)
        /// </summary>
        internal double WavelengthBase;
        /// <summary>
        /// 扫描波长范围+
        /// </summary>
        internal double WavelengthScopePositive;
        /// <summary>
        /// 扫描波长范围-
        /// </summary>
        internal double WavelengthScopeNegative;
        /// <summary>
        /// 是否自动积分时间
        /// </summary>
        internal bool IsAutoIntegrationTime;
        /// <summary>
        /// 积分时间(ms)
        /// </summary>
        internal int IntegrationTime;
        /// <summary>
        /// BoxcarWidth
        /// </summary>
        internal int BoxcarWidth;
        /// <summary>
        /// 扫描次数
        /// </summary>
        internal int NumberOfScansToAverage;
        /// <summary>
        /// PIB总半宽
        /// </summary>
        internal double PibTotalHalfSpan;
        /// <summary>
        /// PIB中心半宽
        /// </summary>
        internal double PibCenterHalfSpan;
        /// <summary>
        /// 是否高斯拟合
        /// </summary>
        internal bool IsGaussFit;
    }

    struct StRoutine
    {
        /// <summary>
        /// 脚本名称
        /// </summary>
        internal string Name;

        /// <summary>
        /// LIV扫描配置
        /// </summary>
        internal StLivScanCfg LivScanCfg;
        /// <summary>
        /// 光谱扫描配置
        /// </summary>
        internal StSpScanCfg SpScanCfg;

        /// <summary>
        /// 测试温度驱动
        /// </summary>
        internal List<double> TempDrvs;
        /// <summary>
        /// 光谱驱动条件列表
        /// </summary>
        internal List<StCosParam> SpDrvs;
        /// <summary>
        /// 数值孔径驱动条件列表
        /// </summary>
        internal List<StCosParam> NaDrvs;
        /// <summary>
        /// 光斑扫描驱动条件列表
        /// </summary>
        internal List<StCosParam> BeamCamDrvs;
        /// <summary>
        /// 热像扫描驱动条件列表
        /// </summary>
        internal List<StCosParam> ThermCamDrvs;
        /// <summary>
        /// 热像扫描区域名称定义
        /// </summary>
        internal string ThermMeasName;


        /// <summary>
        /// Piv参数列表
        /// </summary>
        internal List<StCosParam> LivTestParams;
        /// <summary>
        /// 光谱参数列表
        /// </summary>
        internal List<StCosParam> SpTestParams;
        /// <summary>
        /// 数值孔径参数列表
        /// </summary>
        internal List<StCosParam> NaTestParams;
        /// <summary>
        /// 光斑扫描参数列表
        /// </summary>
        internal List<StCosParam> BeamTestParams;
        /// <summary>
        /// 热像扫描参数列表
        /// </summary>
        internal List<StCosParam> ThermTestParams;
        /// <summary>
        /// 参数差异列表
        /// </summary>
        internal List<StCosParam> DeltaParams;
    }

    struct StPoCal
    {
        internal double WAVELENGTH_BASE;

        internal double PO_WLOFFSET;

        internal double PO_WLOFFSET_BASE;

        internal double PO_CAL;

        internal double PO_OFFSET;

        internal double PO_CAL_BASE;

        internal double PO_OFFSET_BASE;
    }

}
