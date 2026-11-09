using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Drawing;

namespace CosTest
{
    class ToolProc
    {
        /// <summary>
        /// Motor Driver
        /// </summary>
        private MoonsMotor MotorDriver;
        /// <summary>
        /// Laser Driver
        /// </summary>
        private ItDriver LaserDriver;
        /// <summary>
        /// Power Driver
        /// </summary>
        private PdDriver PowerDriver;
        /// <summary>
        /// OSA
        /// </summary>
        private OcOsa OSA;
        /// <summary>
        /// Temperature Driver
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
        /// 命令
        /// </summary>
        private EnToolCommand COMMAND;
        /// <summary>
        /// 步骤
        /// </summary>
        private int STEP;
        /// <summary>
        /// 是否为新的STEP
        /// </summary>
        private bool NewStep;
        /// <summary>
        /// 程序状态
        /// </summary>
        public EnToolStat STAT { get; set; }
        /// <summary>
        /// LIV电流结束
        /// </summary>
        private double LivCurrentStop;
        /// <summary>
        /// LIV电流步进
        /// </summary>
        private double LivCurrentStep;
        /// <summary>
        /// LIV延时
        /// </summary>
        private int LivDelayMillisec;
        /// <summary>
        /// LIV扫描结果
        /// </summary>
        private List<StGoPIV> LivTestData;
        /// <summary>
        /// 起始波长
        /// </summary>
        private double StartWavelength;
        /// <summary>
        /// 结束波长
        /// </summary>
        private double StopWavelength;
        /// <summary>
        /// Cos电流
        /// </summary>
        private double CosCurrent;
        /// <summary>
        /// Cos电压
        /// </summary>
        private double CosVoltage;
        
        /// <summary>
        /// 预加载电压
        /// </summary>
        private double PreloadingVoltage;


        /// <summary>
        /// Y轴点动动速度(单位为mm/s)
        /// </summary>
        private double YSPJG;
        /// <summary>
        /// 移动到目标位置(单位为mm)
        /// </summary>
        private double YPATG;
        /// <summary>
        /// Y轴运动速度(单位为mm/s)
        /// </summary>
        private double YSP;
        /// <summary>
        /// 互锁状态
        /// </summary>
        private bool INTERLOCK;
        /// <summary>
        /// 反射镜状态
        /// </summary>
        private bool MIRROR;
        /// <summary>
        /// 马达复位状态
        /// </summary>
        private bool YHOME;
        /// <summary>
        /// 温度器输出
        /// </summary>
        private bool TEMP_OUT;
        /// <summary>
        /// 温度器输出
        /// </summary>
        private double TEMP_SET;
        /// <summary>
        /// 温度器PID之KP
        /// </summary>
        private double TEMP_KP;
        /// <summary>
        /// 温度器PID之KI
        /// </summary>
        private double TEMP_KI;
        /// <summary>
        /// 温度器PID之KD
        /// </summary>
        private double TEMP_KD;

        /// <summary>
        /// Tool Motor Data Handler
        /// </summary>
        internal event ToolMotorDataHandler ToolMotorDataUpdate;
        /// <summary>
        /// Tool Temperature Data Handler
        /// </summary>
        internal event ToolTempDataHandler ToolTempDataUpdate;
        /// <summary>
        /// Tool Liv Data Handler
        /// </summary>
        internal event ToolLivDataHandler ToolLivDataUpdate;
        /// <summary>
        /// Tool Piv Pt Data Handler
        /// </summary>
        internal event ToolPivPtDataHandler ToolPivPtDataUpdate;
        /// <summary>
        /// Tool Spectrum Data Handler
        /// </summary>
        internal event ToolSpectrumDataHandler ToolSpectrumDataUpdate;
        /// <summary>
        /// Tool Beam Image Handler
        /// </summary>
        internal event ToolBeamImageHandler ToolBeamImageUpdate;
        /// <summary>
        /// Tool Thermal Image Handler
        /// </summary>
        internal event ToolThermalImageHandler ToolThermalImageUpdate;
        /// <summary>
        /// Test Warn Handler
        /// </summary>
        internal event TestWarnHandler TestWarn;

        /// <summary>
        /// Mirror移动超时计数
        /// </summary>
        private int MirrorMotionTimeOutCount;
        /// <summary>
        /// Mirror移动超时计数上限
        /// </summary>
        private int MirrorMotionTimeOutLimit;

        /// <summary>
        /// MotorReadCount
        /// </summary>
        private int MotorReadCount;
        /// <summary>
        /// TempReadCount
        /// </summary>
        private int TempReadCount;
        /// <summary>
        /// PivReadCount
        /// </summary>
        private int PivReadCount;

        /// <summary>
        /// 异常计数
        /// </summary>
        private int ExceptionCount;

        /// <summary>
        /// 空闲计数
        /// </summary>
        private int IdleCount;

        /// <summary>
        /// LIV设置
        /// </summary>
        private StGoLiv LIV;

        public ToolProc(MoonsMotor motorDriver, ItDriver laserDriver, PdDriver powerDriver, OcOsa osa, MeerstetterTECController tempDriver, BeamCam beamCamDriver, ThermCam thermalCamDriver)
        {
            // 引用
            this.MotorDriver = motorDriver;
            this.LaserDriver = laserDriver;
            this.PowerDriver = powerDriver;
            this.OSA = osa;
            this.TempDriver = tempDriver;
            this.BeamCamDriver = beamCamDriver;
            this.ThermCamDriver = thermalCamDriver;

            // 光谱参数
            this.StartWavelength = 400;
            this.StopWavelength = 1100;

            // 马达参数
            this.UpdateJigMotionSetting();

            // 标志位
            this.STEP = 0;
            this.NewStep = false;
            this.STAT = EnToolStat.IDLE;
            this.MotorReadCount = 0;

            // Pin计数
            this.MirrorMotionTimeOutCount = 0;
            this.MirrorMotionTimeOutLimit = 50;

            // 异常清零
            this.ExceptionCount = 0;
        }

        /// <summary>
        /// 更新夹具运动设置
        /// </summary>
        private void UpdateJigMotionSetting()
        {
            this.YSP = TestSystemUtil.YSPEED ;
        }

        /// <summary>
        /// 运行
        /// </summary>
        internal void Run()
        {
            try
            {
                #region 更新界面数据
                // Motor State Data
                this.MotorReadCount++;
                if (this.MotorReadCount >= 1)
                {
                    this.MotorReadCount = 0;
                    if (this.ToolMotorDataUpdate != null)
                    {
                        StDiStat di = this.MotorDriver.GetDiStat();
                        StMotorStat stat = this.MotorDriver.GetMotorStat();
                        double pos = this.MotorDriver.GetRealAbsPosition();
                        this.INTERLOCK = di.Interlock;
                        this.MIRROR = di.Mirror;
                        this.YHOME = di.Home;

                        this.ToolMotorDataUpdate(this, new ToolMotorDataEventArgs()
                        {
                            HOME = di.Home,
                            LOCK = di.Interlock,
                            MIRROR = di.Mirror,
                            ENABLE = stat.Enable,
                            ALARM = (stat.Error || stat.Alarm),
                            MOVING = stat.Moving || stat.Jogging,
                            POS = pos
                        });
                    }
                }

                // Temperature State Data
                this.TempReadCount++;
                if (this.TempReadCount >= 5)
                {
                    this.TempReadCount = 0;
                    if (this.ToolTempDataUpdate != null)
                    {
                        double tGet = this.TempDriver.ReadTemperature_DegreeC();
                        double iSet = this.TempDriver.ReadTecDcCurrent_A();
                        double vSet = this.TempDriver.ReadTecDcVoltage_V();
                        bool enOut = this.TempDriver.IsOutputEnabled;

                        this.ToolTempDataUpdate(this, new ToolTempDataEventArgs()
                        {
                            TGET = tGet,
                            IGET = iSet,
                            VGET = vSet,
                            OUT = enOut
                        });
                    }
                }

                // PIV Point Data
                this.PivReadCount++;
                if (this.PivReadCount >= 2)
                {
                    this.PivReadCount = 0;
                    if (this.ToolPivPtDataUpdate != null && this.STEP == 0)
                    {
                        StGoPIV piv = new StGoPIV();
                        piv.LDI = this.LaserDriver.GetMeasCurrent();
                        piv.LDV = this.LaserDriver.GetMeasVoltage();
                        piv.PDI = this.PowerDriver.GetPhotoPower();
                        this.ToolPivPtDataUpdate(this, new ToolPivPtDataEventArgs(piv));
                    }
                }
                #endregion


                // 如果有动作正在执行, 忽略当前命令
                if (this.STEP != 0 && this.COMMAND != EnToolCommand.STOP)
                    this.COMMAND = EnToolCommand.NONE;

                // 处理命令
                switch (this.COMMAND)
                {
                    case EnToolCommand.STOP:
                        this.NewStep = true;
                        this.STEP = 1;
                        break;
                    case EnToolCommand.HOME:
                        this.NewStep = true;
                        this.STEP = 2;
                        break;
                    case EnToolCommand.JOGON:
                        this.NewStep = true;
                        this.STEP = 3;
                        break;
                    case EnToolCommand.JOGOFF:
                        this.NewStep = true;
                        this.STEP = 4;
                        break;
                    case EnToolCommand.MIRRIN:
                        this.NewStep = true;
                        this.STEP = 5;
                        break;
                    case EnToolCommand.MIRROUT:
                        this.NewStep = true;
                        this.STEP = 6;
                        break;
                    case EnToolCommand.LOCK:
                        this.NewStep = true;
                        this.STEP = 7;
                        break;
                    case EnToolCommand.UNLOCK:
                        this.NewStep = true;
                        this.STEP = 8;
                        break;
                    case EnToolCommand.SPECTRUM:
                        this.NewStep = true;
                        this.STEP = 9;
                        break;
                    case EnToolCommand.LIV:
                        this.LivTestData = new List<StGoPIV>();
                        this.NewStep = true;
                        this.STEP = 10;
                        break;
                    case EnToolCommand.COSON:
                        this.NewStep = true;
                        this.STEP = 11;
                        break;
                    case EnToolCommand.COSOFF:
                        this.NewStep = true;
                        this.STEP = 12;
                        break;
                    case EnToolCommand.COSCURR:
                        this.NewStep = true;
                        this.STEP = 13;
                        break;
                    case EnToolCommand.FEED:
                        this.NewStep = true;
                        this.STEP = 14;
                        break;
                    case EnToolCommand.TMPSET:
                        this.NewStep = true;
                        this.STEP = 15;
                        break;
                    case EnToolCommand.TMPOUT:
                        this.NewStep = true;
                        this.STEP = 16;
                        break;
                    case EnToolCommand.PID:
                        this.NewStep = true;
                        this.STEP = 17;
                        break;
                    case EnToolCommand.MTRON:
                        this.NewStep = true;
                        this.STEP = 18;
                        break;
                    case EnToolCommand.MTROFF:
                        this.NewStep = true;
                        this.STEP = 19;
                        break;
                    case EnToolCommand.BEAMSCAN:
                        this.NewStep = true;
                        this.STEP = 20;
                        break;
                    case EnToolCommand.THERMSCAN:
                        if(StationUtil.ThermCamEnable)
                        {
                            this.NewStep = true;
                            this.STEP = 21;
                        }
                        break;
                    case EnToolCommand.THERMSCAN2:
                        this.NewStep = true;
                        this.STEP = 22;
                        break;
                    case EnToolCommand.NONE:
                        // Keep Step
                        break;
                    default:
                        this.STEP = 0;
                        break;
                }
                // 清除命令
                this.COMMAND = EnToolCommand.NONE;

                // 检查互锁
                if (this.INTERLOCK == false)
                {
                    if (this.STEP == 2 || this.STEP == 3 || this.STEP == 14)
                    {
                        if (this.TestWarn != null)
                            this.TestWarn(this, new TestWarnEventArgs("门锁已打开,停止运动!"));
                        // 停止运动
                        this.NewStep = true;
                        this.STEP = 1;
                    }
                    else if (this.STEP == 10 || this.STEP == 11 || this.STEP == 13)
                    {
                        if (this.TestWarn != null)
                            this.TestWarn(this, new TestWarnEventArgs("门锁已打开,停止激光输出!"));
                        // 停止运动
                        this.NewStep = true;
                        this.STEP = 1;
                    }
                }

                // 检查反射镜
                if(this.MIRROR)
                {
                    if (this.STEP == 2 || this.STEP == 3 || this.STEP == 14)
                    {
                        if (this.TestWarn != null)
                            this.TestWarn(this, new TestWarnEventArgs("反射镜未收回,停止运动!"));
                        // 停止运动
                        this.NewStep = true;
                        this.STEP = 1;
                    }
                }



                // 步骤
                switch (this.STEP)
                {
                    #region IDLE
                    case 0:
                        // Do Nothing
                        this.IdleCount++;
                        if (this.IdleCount >= 10)
                        {
                            this.IdleCount = 0;
                            //this.OSA.GetSerialNumber();
                            //this.LaserDriver.GetSerialNumber();
                        }
                        break;
                    #endregion

                    #region STOP
                    case 1:
                        // 停止运动
                        if (this.NewStep)
                        {
                            this.MotorDriver.StopJog();
                            this.MotorDriver.StopMove();
                            this.NewStep = false;
                            this.STAT = EnToolStat.IDLE;
                        }
                        else
                        {
                            this.STEP = 0;
                        }
                        break;
                    #endregion

                    #region 复位
                    case 2:
                        // HOME
                        if (this.NewStep)
                        {
                            this.UpdateJigMotionSetting();
                            // 设置运动速度，过快会失步
                            this.MotorDriver.SetSpeed(this.YSP);
                            // 设置目标位置为负，决定了复位方向
                            this.MotorDriver.SetTargetPosition(-0.05);
                            //this.MotorDriver.FeedLength();
                            //Thread.Sleep(500);
                            //this.MotorDriver.StopMove();
                            // 开始复位
                            this.MotorDriver.SeekHome();
                            this.NewStep = false;
                            this.STAT = EnToolStat.RUNNING;
                        }
                        else
                        {
                            if (this.MotorDriver.GetMotorStat().Moving == false)
                            {
                                this.MotorDriver.SetEncoderPosition(0);
                                this.MotorDriver.SetAbsolutePosition(0);
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                            }
                        }
                        break;
                    #endregion

                    #region JOGON
                    case 3:
                        // 点动开始
                        if (this.NewStep)
                        {
                            // 检查反射镜状态
                            if (this.MIRROR)
                            {
                                this.NewStep = false;
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未收回,停止运动!"));
                            }
                            // 开始运动
                            else
                            {
                                this.MotorDriver.SetJogSpeed(this.YSPJG);
                                this.MotorDriver.StartJog();
                                this.NewStep = false;
                                this.STAT = EnToolStat.RUNNING;
                            }
                        }
                        else
                        {
                            this.STEP = 0;
                        }
                        break;
                    #endregion

                    #region JOGOFF
                    case 4:
                        // 点动结束
                        if (this.NewStep)
                        {
                            this.MotorDriver.StopJog();
                            this.MotorDriver.StopMove();
                            this.NewStep = false;
                            this.STAT = EnToolStat.IDLE;
                        }
                        else
                        {
                            this.STEP = 0;
                        }
                        break;
                    #endregion

                    #region  MIRROR IN
                    case 5:
                        // 推入反射镜
                        if (this.NewStep)
                        {
                            // 马达复位状态
                            if (this.YHOME == false)
                            {
                                this.NewStep = false;
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("马达未复位,不能移入反射镜!"));
                            }
                            // 开始运动
                            else
                            {
                                this.MotorDriver.SetMirror(true);
                                this.NewStep = false;
                                this.STAT = EnToolStat.RUNNING;
                                this.MirrorMotionTimeOutCount = 0;
                            }
                        }
                        else
                        {
                            if (this.MIRROR == true)
                            {
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                            }
                            else
                            {
                                this.MirrorMotionTimeOutCount++;
                                if (this.MirrorMotionTimeOutCount >= this.MirrorMotionTimeOutLimit)
                                {
                                    this.STEP = 0;
                                    this.STAT = EnToolStat.IDLE;
                                    if (this.TestWarn != null)
                                       this.TestWarn(this, new TestWarnEventArgs("未能检查到反射镜推入成功信号,请检查!"));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region MIRROR OUT
                    case 6:
                        // 退出反射镜
                        if (this.NewStep)
                        {
                            this.MotorDriver.SetMirror(false);
                            this.NewStep = false;
                            this.STAT = EnToolStat.RUNNING;
                            this.MirrorMotionTimeOutCount++;
                        }
                        else
                        {
                            if (this.MotorDriver.GetDiStat().Mirror == false)
                            {
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                            }
                            else
                            {
                                this.MirrorMotionTimeOutCount++;
                                if (this.MirrorMotionTimeOutCount >= this.MirrorMotionTimeOutLimit)
                                {
                                    this.STEP = 0;
                                    this.STAT = EnToolStat.IDLE;
                                    if (this.TestWarn != null)
                                        this.TestWarn(this, new TestWarnEventArgs("未能检查到反射镜推出成功信号,请检查!"));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region  Door Lock
                    case 7:
                        // 打开强制互锁
                        if (this.NewStep)
                        {
                            this.MotorDriver.SetDoorUnlock(false);
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region  Door UnLock
                    case 8:
                        // 打开强制互锁
                        if (this.NewStep)
                        {
                            this.MotorDriver.SetDoorUnlock(true);
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region SCAN SPECTRUM
                    case 9:
                        //光谱扫描
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            // 读取Spectrum
                            this.OSA.StartWavelength = this.StartWavelength;
                            this.OSA.StopWavelength = this.StopWavelength;
                            this.OSA.IsAutoIntegrationTime = true;
                            this.OSA.SetIntegrationTime();
                            StOcSpData spData = this.OSA.GetSpectrumData();
                            // 更新Spectrum数据到界面
                            if (this.ToolSpectrumDataUpdate != null)
                                this.ToolSpectrumDataUpdate(this, new ToolSpectrumDataEventArgs(spData));
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region LIV
                    case 10:
                        // LIV扫描
                        if (this.NewStep)
                        {
                            // RUNNING
                            this.STAT = EnToolStat.RUNNING;
                            // 设置功率计RANGE
                            this.PowerDriver.RANGE = (ushort)TestSystemUtil.PD_RNG;
                            //// 设置电流为0
                            //this.LaserDriver.SetCurrent(0);
                            //// 设置输出为ON (重复命令，确保正确)
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    this.LaserDriver.SetOutput(true);
                            //    Thread.Sleep(500);
                            //    if (this.LaserDriver.GetOutput())
                            //        break;
                            //}

                           // this.LaserDriver.SetCurrent(1.0);
                            this.LaserDriver.SetVoltage(PreloadingVoltage);
                            for (int i = 0; i < 3; i++)
                            {
                                this.LaserDriver.SetOutput(true);
                                if (this.LaserDriver.GetOutput())
                                    break;
                            }

                            Thread.Sleep(100);
                            //// 设置预加电流 (重复命令，确保正确)
                            //double preOnCurrent = Math.Min(this.LivCurrentStop, TestSystemUtil.PRE_ON_CURRENT);
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    this.LaserDriver.SetCurrent(preOnCurrent);
                            //    break;
                            //    // Thread.Sleep(200);
                            //}
                            //Thread.Sleep(2000);
                            //// 延时后取消预加电流
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    this.LaserDriver.SetCurrent(0.0);
                            //    break;
                            //    // Thread.Sleep(200);
                            //}
                            //Thread.Sleep(500);


                            // 启动LIV测试
                            LIV = new StGoLiv();
                            LIV.START = 0.0;
                            LIV.STOP = this.LivCurrentStop;
                            LIV.STEP = this.LivCurrentStep;
                            // 更新状态
                            this.NewStep = false;
                        }
                        else
                        {
                            this.LivTestData = new List<StGoPIV>();
                            double current = 0.0;
                            while(current <= this.LivCurrentStop)
                            {
                               // ErrLog.WriteLine($"LIV测试电流: {current:F3} A");
                                this.LaserDriver.SetCurrent(current);
                                Thread.Sleep(this.LivDelayMillisec);
                                current += this.LivCurrentStep;
                                StGoPIV piv = new StGoPIV();
                                piv.LDI = this.LaserDriver.GetMeasCurrent();
                                piv.LDV = this.LaserDriver.GetMeasVoltage();
                                piv.PDI = this.PowerDriver.GetPhotoPower();
                                this.LivTestData.Add(piv);
                            }

                           
                            // 关闭激光器（重复命令，确保关闭）
                            for (int i = 0; i < 3; i++)
                            {
                                LaserDriver.SetVoltageAndCurrent(0, 0);
                                this.LaserDriver.SetOutput(false);
                                Thread.Sleep(500);
                                if (!this.LaserDriver.GetOutput())
                                    break;
                            }
                            //  ErrLog.WriteLine($"关闭激光器");

                            // 更新LIV数据到界面
                            if (this.ToolLivDataUpdate != null)
                                this.ToolLivDataUpdate(this, new ToolLivDataEventArgs(this.LivTestData));
                            // 输出到LIV数据
                            TestResult.WriteToolLivData(this.LivTestData);

                            Thread.Sleep(15000);

                            // 清空STEP
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region COSON
                    case 11:
                        // COS上电
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            // 设置RANGE
                            // this.PowerDriver.SetPhotoCurrentRange(1);
                            // 设置输出为ON (重复命令，确保打开)
                            for (int i = 0; i < 3; i++)
                            {
                                LaserDriver.SetVoltageAndCurrent(0, 0);
                                this.LaserDriver.SetOutput(true);
                                Thread.Sleep(500);
                                if (this.LaserDriver.GetOutput())
                                    break;
                            }
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region COSOFF
                    case 12:
                        // COS下电
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            // 关闭激光器（重复命令，确保关闭）
                            for (int i = 0; i < 3; i++)
                            {
                                LaserDriver.SetVoltageAndCurrent(0, 0);
                                this.LaserDriver.SetOutput(false);
                                Thread.Sleep(500);
                                if (!this.LaserDriver.GetOutput())
                                    break;
                            }
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region COSCURR
                    case 13:
                        // COS电流设置
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            // 设置驱动电流
                            this.LaserDriver.SetCurrent(this.CosCurrent);
                            this.LaserDriver.SetVoltage(this.CosVoltage);
                            Thread.Sleep(200);
                            //for(int i=0;i<3;i++)
                            //{
                            //    this.LaserDriver.SetCurrent(this.CosCurrent);
                            //        break;
                            //    Thread.Sleep(200);
                            //}
                        }
                        else
                        {
                            this.STEP = 0;
                        }
                        break;
                    #endregion

                    #region FEED
                    case 14:
                        // 停车
                        if (this.NewStep)
                        {
                            // 检查反射镜状态
                            if (this.MIRROR)
                            {
                                this.NewStep = false;
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                                if (this.TestWarn != null)
                                    this.TestWarn(this, new TestWarnEventArgs("反射镜未收回,停止运动!"));
                            }
                            // 开始运动
                            else
                            {
                                this.UpdateJigMotionSetting();
                                this.MotorDriver.SetSpeedAndTargetPosition(this.YSPJG, (int)this.YPATG);
                                this.MotorDriver.FeedPosition();
                                this.NewStep = false;
                                this.STAT = EnToolStat.RUNNING;
                            }
                        }
                        else
                        {
                            if (this.MotorDriver.GetMotorStat().Moving == false)
                            {
                                this.STEP = 0;
                                this.STAT = EnToolStat.IDLE;
                            }
                        }
                        break;
                    #endregion

                    #region TMPSET
                    case 15:
                        // 设置目标温度
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.TempDriver.TemperatureSetPoint_DegreeC = this.TEMP_SET;
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region TMPOUT
                    case 16:
                        // 设置温控输出
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.TempDriver.IsOutputEnabled = this.TEMP_OUT;
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region PID
                    case 17:
                        // 设置PID
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.TempDriver.TemperatureGain = this.TEMP_KP;
                            this.TempDriver.TemperatureIntegral = this.TEMP_KI; 
                            this.TempDriver.TemperatureDerivative = this.TEMP_KD;
                        }
                        else
                        {
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                        }
                        break;
                    #endregion

                    #region MOTORON
                    case 18:
                        // MOTORON
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                            this.MotorDriver.ClearAlarm();
                            this.MotorDriver.MotorOn();
                        }
                        break;
                    #endregion

                    #region MOTOROFF
                    case 19:
                        // MOTORON
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                            this.MotorDriver.MotorOff();
                        }
                        break;
                    #endregion

                    #region BEAMCAM
                    case 20:
                        // BEAMCAM
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                            BitmapImage beamBmpImage = Mat2BitmapImage.Cvt(this.BeamCamDriver.ReadFrame());
                            this.ToolBeamImageUpdate?.Invoke(this, new ToolBeamImageEventArgs(beamBmpImage));
                        }
                        break;
                    #endregion

                    #region THERMCAM
                    case 21:
                        // THERMCAM
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                            BitmapImage irInfoBmpImage = Bitmap2BitmapImage.Cvt(this.ThermCamDriver.ReadFrame());
                            this.ToolThermalImageUpdate?.Invoke(this, new ToolThermalImageEventArgs(irInfoBmpImage));
                        }
                        break;
                    #endregion

                    #region THERMCAM2
                    case 22:
                        // THERMCAM
                        if (this.NewStep)
                        {
                            this.NewStep = false;
                            this.STEP = 0;
                            this.STAT = EnToolStat.IDLE;
                            BitmapImage irBmpImage = Bitmap2BitmapImage.Cvt(this.ThermCamDriver.ReadRawFrame());
                            this.ToolThermalImageUpdate?.Invoke(this, new ToolThermalImageEventArgs(irBmpImage));
                        }
                        break;
                    #endregion

                    default:
                        this.STEP = 0;
                        break;
                }

                // 异常清零
                if ((this.STEP == 0 && this.IdleCount == 0) || this.STEP > 0)
                {
                    if (this.MotorDriver.CONNECTED && this.LaserDriver.CONNECTED && this.OSA.CONNECTED && this.TempDriver.Initialized && this.PowerDriver.CONNECTED)
                        this.ExceptionCount = 0;
                }
            }
            catch (Exception ex)
            {
                // 异常计数
                this.ExceptionCount++;


                if(this.ExceptionCount < 3)
                {
                    // 保存异常信息
                    ErrLog.WriteLine(ex.ToString());
                }
                else if (this.ExceptionCount == 3)
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

                    this.STAT = EnToolStat.IDLE;
                    this.STEP = 0;
                }
                else if (this.ExceptionCount > 4)
                {
                    this.ExceptionCount = 4;
                }
            }
        }

        internal void SetCosCurrent(double cosCurrent)
        {
            this.CosCurrent = cosCurrent;
        }
        internal void SetCosVoltage(double cosVoltage)
        {
            this.CosVoltage = cosVoltage;
        }
         
        internal void SetSpectrumRange(double startWavelength, double stopWavelength)
        {
            this.StartWavelength = startWavelength;
            this.StopWavelength = stopWavelength;
        }

        internal void SetWavelengthBase(double wavelengthBase)
        {
            foreach (StPoCal poCal in TestSystemUtil.PO_CAL_LIST)
            {
                if (poCal.WAVELENGTH_BASE == wavelengthBase)
                {
                    this.OSA.WavelengthOffset = poCal.PO_WLOFFSET_BASE + poCal.PO_WLOFFSET;
                    this.PowerDriver.PO_CAL = poCal.PO_CAL;
                    this.PowerDriver.PO_OFFSET = poCal.PO_OFFSET;
                    this.PowerDriver.PO_CAL_BASE = poCal.PO_CAL_BASE;
                    this.PowerDriver.PO_OFFSET_BASE = poCal.PO_OFFSET_BASE;
                    break;
                }
            }
        }

        /// <summary>
        /// 电流单位为A
        /// 延时单位为ms
        /// </summary>
        /// <param name="stopCurrent"></param>
        /// <param name="stepCurrent"></param>
        /// <param name="delayMillisec"></param>
        internal bool SetLivParameters(double stopCurrent, double stepCurrent, int delayMillisec,double preloadingVoltage)
        {
            if (stopCurrent <= 0 || stepCurrent <= 0 || stepCurrent >= stopCurrent || delayMillisec <= 0 || preloadingVoltage < 0)
            {
                WarnWindow.ShowMsg("LIV参数设置错误");
                return false;
            }

            this.LivCurrentStop = stopCurrent;
            this.LivCurrentStep = stepCurrent;
            this.LivDelayMillisec = delayMillisec;
            this.PreloadingVoltage = preloadingVoltage;
            return true;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="toolCommand"></param>
        internal void DoCommand(EnToolCommand toolCommand)
        {
            this.COMMAND = toolCommand;
        }

        /// <summary>
        /// 设置JOG速度
        /// </summary>
        /// <param name="speed"></param>
        internal void SetJog(double speed)
        {
            this.YSPJG = speed;
        }

        internal void MoveToTargePositionZ(double speed, double position)
        {
            this.YSPJG = speed;
            this.YPATG = position;
        }

        /// <summary>
        /// 设置目标温度
        /// </summary>
        /// <param name="temperature"></param>
        internal void SetTempTarget(double temperature)
        {
            this.TEMP_SET = temperature;
        }

        /// <summary>
        /// 设置PID
        /// </summary>
        /// <param name="temperature"></param>
        internal void SetTempPid(double kp, double ki, double kd)
        {
            this.TEMP_KP = kp;
            this.TEMP_KI = ki;
            this.TEMP_KD = kd;
        }

        /// <summary>
        /// 设置温控器输出
        /// </summary>
        /// <param name="enable"></param>
        internal void SetTempOut(bool enable)
        {
            this.TEMP_OUT = enable;
        }

        /// <summary>
        /// 马达上电
        /// </summary>
        internal void MotorOn()
        {
            this.COMMAND = EnToolCommand.MTRON;
        }

        /// <summary>
        /// 马达下电
        /// </summary>
        internal void MotorOff()
        {
            this.COMMAND = EnToolCommand.MTROFF;
        }

        /// <summary>
        /// 检查LIV数据是否有误
        /// </summary>
        /// <returns></returns>
        private bool IsLivError()
        {
            if (this.LivTestData == null || this.LivTestData.Count == 0)
                return true;

            // LDI > 0.3A
            int i = 0;
            while (i < this.LivTestData.Count)
            {
                if (this.LivTestData[i].LDI > 0.3)
                    break;
                i++;
            }
            if (i == this.LivTestData.Count)
                return true;
            // LDI or LDV becomes too low  
            while (i < this.LivTestData.Count)
            {
                if (this.LivTestData[i].LDI < 0.3 || this.LivTestData[i].LDV < 0.5)
                    return true;
                i++;
            }

            // PDI饱和
            i = this.LivTestData.Count - 1;
            double power = this.LivTestData[i].PDI;
            double current = this.LivTestData[i].LDI;
            bool isSat = true;
            i -= 1;
            while (i > 0 && this.LivTestData[i].LDI > current - 1.0)
            {
                if (power != this.LivTestData[i].PDI)
                {
                    isSat = false;
                    break;
                }
                i--;
            }
            if (i == this.LivTestData.Count - 2)
                isSat = false;

            return isSat;
        }
    }

    /// <summary>
    /// 运行状态
    /// </summary>
    enum EnToolStat
    {
        IDLE,
        RUNNING,
    }

    /// <summary>
    /// 命令
    /// </summary>
    enum EnToolCommand
    {
        NONE,
        STOP,
        HOME,
        JOGON,
        JOGOFF,
        FEED,
        LOCK,
        UNLOCK,
        MIRRIN,
        MIRROUT,
        TMPSET,
        TMPOUT,
        SPECTRUM,
        LIV,
        PID,
        COSON,
        COSOFF,
        COSCURR,
        MTRON,
        MTROFF,
        BEAMSCAN,
        THERMSCAN,
        THERMSCAN2
    }

    /// <summary>
    /// Tray Liv Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolLivDataHandler(object sender, ToolLivDataEventArgs args);

    /// <summary>
    /// Tray Common Data Event
    /// </summary>
    class ToolLivDataEventArgs : EventArgs
    {
        /// <summary>
        /// LIV DATA
        /// </summary>
        internal List<StGoPIV> LivTestData { get; set; }

        internal ToolLivDataEventArgs(List<StGoPIV> livTestData)
        {
            this.LivTestData = livTestData;
        }
    }

    /// <summary>
    /// ToolPivPtDataHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolPivPtDataHandler(object sender, ToolPivPtDataEventArgs args);

    class ToolPivPtDataEventArgs : EventArgs
    {
        /// <summary>
        /// PIV测试
        /// </summary>
        internal StGoPIV PivPtData { get; set; }

        internal ToolPivPtDataEventArgs(StGoPIV pivPtData)
        {
            this.PivPtData = pivPtData;
        }
    }

    /// <summary>
    /// Tray Spectrum Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolSpectrumDataHandler(object sender, ToolSpectrumDataEventArgs args);

    /// <summary>
    /// Tray Common Data Event
    /// </summary>
    class ToolSpectrumDataEventArgs : EventArgs
    {
        /// <summary>
        /// Spectrum DATA
        /// </summary>
        internal StOcSpData SpectrumTestData { get; set; }

        internal ToolSpectrumDataEventArgs(StOcSpData spectrumTestData)
        {
            this.SpectrumTestData = spectrumTestData;
        }
    }

    /// <summary>
    /// Tool Motor Data Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolMotorDataHandler(object sender, ToolMotorDataEventArgs args);

    /// <summary>
    /// Tool Motor Data Event
    /// </summary>
    class ToolMotorDataEventArgs : EventArgs
    {
        /// <summary>
        /// 马达使能
        /// </summary>
        internal bool ENABLE { get; set; }
        /// <summary>
        /// 马达正在运动
        /// </summary>
        internal bool MOVING { get; set; }
        /// <summary>
        /// 马达复位
        /// </summary>
        internal bool HOME { get; set; }

        // <summary>
        /// 马达位置
        /// </summary>
        internal double POS { get; set; }
        /// <summary>
        /// 反射镜
        /// </summary>
        internal bool MIRROR { get; set; }
        /// <summary>
        /// 马达报警
        /// </summary>
        internal bool ALARM { get; set; }
        /// <summary>
        /// 门锁
        /// </summary>
        internal bool LOCK { get; set; }
    }

    /// <summary>
    /// Tool Temperature Data Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolTempDataHandler(object sender, ToolTempDataEventArgs args);

    /// <summary>
    /// Tool Temperature Data Event
    /// </summary>
    class ToolTempDataEventArgs : EventArgs
    {
        // <summary>
        /// 夹具温度
        /// </summary>
        internal double TGET { get; set; }
        // <summary>
        /// TEC电流
        /// </summary>
        internal double IGET { get; set; }
        // <summary>
        /// TEC电压
        /// </summary>
        internal double VGET { get; set; }
        // <summary>
        /// TEC温控器输出
        /// </summary>
        internal bool OUT { get; set; }
    }


    /// <summary>
    /// Tool Beam Camera Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolBeamImageHandler(object sender, ToolBeamImageEventArgs args);

    /// <summary>
    /// Tool Beam Image Event
    /// </summary>
    class ToolBeamImageEventArgs : EventArgs
    {
        /// <summary>
        /// Beam Image
        /// </summary>
        internal BitmapImage BeamImage { get; set; }

        internal ToolBeamImageEventArgs(BitmapImage image)
        {
            this.BeamImage = image;
        }
    }

    /// <summary>
    /// Tool Thermal Camera Handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void ToolThermalImageHandler(object sender, ToolThermalImageEventArgs args);

    /// <summary>
    /// Tool Thermal Image Event
    /// </summary>
    class ToolThermalImageEventArgs : EventArgs
    {
        /// <summary>
        /// Spectrum DATA
        /// </summary>
        internal BitmapImage ThermalImage { get; set; }

        internal ToolThermalImageEventArgs(BitmapImage image)
        {
            this.ThermalImage = image;
        }
    }
}
