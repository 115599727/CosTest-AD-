using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using NModbus;
using NModbus.Serial;


namespace CosTest
{
    class MoonsMotor
    {
        /// <summary>
        /// Modbus串口
        /// </summary>
        private SerialPort MdbsSerialPort;
        /// <summary>
        /// ModbusFactory
        /// </summary>
        private ModbusFactory MdbsFactory;
        /// <summary>
        /// MODBUS主机
        /// </summary>
        private IModbusMaster MdbsMaster;
        /// <summary>
        /// CONNECTION
        /// </summary>
        internal bool CONNECTED { get; set; }
        /// <summary>
        /// 运动控制器节点
        /// </summary>
        private const int SLAVE_NODE = 1;

        /// <summary>
        /// Moons运动控制器
        /// </summary>
        internal MoonsMotor()
        {
            this.MdbsSerialPort = new SerialPort();
            this.MdbsSerialPort.PortName = TestSystemUtil.MOTOR_COMM_PORT;
            this.MdbsSerialPort.StopBits = StopBits.One;
            this.MdbsSerialPort.Parity = Parity.None;
            this.MdbsSerialPort.BaudRate = 115200;
            this.MdbsSerialPort.ReadTimeout = 2000;
            this.MdbsSerialPort.WriteTimeout = 2000;
        }

        /// <summary>
        /// 连接驱动器
        /// </summary>
        internal void Open()
        {
            try
            {
                this.MdbsSerialPort.Open();
                this.MdbsFactory = new ModbusFactory();
                this.MdbsMaster = this.MdbsFactory.CreateRtuMaster(this.MdbsSerialPort);
                this.ClearAlarm();
                this.MotorOff();
                this.SetDoorUnlock(true);
                this.CONNECTED = true;
            }
            catch
            {
                this.CONNECTED = false;
            }
        }

        /// <summary>
        /// 关闭驱动器
        /// </summary>
        internal void Close()
        {
            try
            {
                this.MdbsSerialPort.Close();
                this.CONNECTED = false;
            }
            catch
            {
                this.CONNECTED = false;
            }
        }

        #region 运动

        /// Modbus 寄存器表中寄存器40125被定义为操作码寄存器
        /// 向40125寄存器写入相应的操作码，即执行相应操作码的动作


        /// <summary>
        /// 马达上电
        /// </summary>
        internal void MotorOn()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x009F;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Motor On";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 马达下电
        /// </summary>
        internal void MotorOff()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x009E;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Motor Off";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 清除报警
        /// </summary>
        internal void ClearAlarm()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x00BA;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Clear Alarm";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 复位
        /// </summary>
        internal void SeekHome()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort[] data = new ushort[] { 0x6E, 0x33, 0x00 };
            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Seek Home";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置速度(单位是rps 每秒圈数)
        /// </summary>
        internal void SetSpeed(double speed)
        {
            double rps = speed / TestSystemUtil.YPITCH;
            this.SetRps(rps);
        }

        /// <summary>
        /// 设置速度(单位是rps 每秒圈数)
        /// </summary>
        private void SetRps(double rps)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x001D;
            byte[] bs = BitConverter.GetBytes((short)(rps * 240));
            ushort data = BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0);
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Rps";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置绝对坐标
        /// </summary>
        internal void SetAbsolutePosition(int count)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            byte[] bs = BitConverter.GetBytes(count);
            ushort[] data = new ushort[] { 0xA5, BitConverter.ToUInt16(new byte[] { bs[2], bs[3] }, 0), BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0) }; // SP0
            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Absolute Position";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置编码器坐标(单位count)
        /// </summary>
        internal void SetEncoderPosition(int count)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            byte[] bs = BitConverter.GetBytes(count);
            ushort[] data = new ushort[] { 0x98, BitConverter.ToUInt16(new byte[] { bs[2], bs[3] }, 0), BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0) }; // EP0
            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Position";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置速度(单位是rps 每秒圈数)
        /// 设置目标位置或者相对运动距离(单位count)
        /// </summary>
        private void SetRpsAndTargetCount(double rps, int count)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x001D;
            byte[] bs = BitConverter.GetBytes(count);
            ushort[] data = new ushort[] { (ushort)Math.Abs(rps * 240), BitConverter.ToUInt16(new byte[] { bs[2], bs[3] }, 0), BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0) };
            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Speed and Position";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置速度(mm/s)
        /// 设置目标位置或者相对运动距离(mm)
        /// </summary>
        internal void SetSpeedAndTargetPosition(double speed, double position)
        {
            double rps = speed / TestSystemUtil.YPITCH;
            int count = (int)(position * TestSystemUtil.YMM2CNT);
            this.SetRpsAndTargetCount(rps, count);
        }

        /// <summary>
        /// 设置目标位置或者相对运动距离(单位count)
        /// </summary>
        private void SetTargetCount(int count)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x001E;
            byte[] bs = BitConverter.GetBytes(count);
            ushort[] data = new ushort[] { BitConverter.ToUInt16(new byte[] { bs[2], bs[3] }, 0), BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0) };
            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Target Count";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置目标位置或者相对运动距离(mm)
        /// </summary>
        internal void SetTargetPosition(double position)
        {
            int count = (int)(position * TestSystemUtil.YMM2CNT);
            this.SetTargetCount(count);
        }

        /// <summary>
        /// 执行相对运动，SCL指令FL
        /// </summary>
        internal void FeedLength()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x0066;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Motor On";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 执行绝对运动，SCL指令FP
        /// </summary>
        internal void FeedPosition()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x0067;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Motor On";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 以最大减速度停止，SCL指令SK
        /// </summary>
        internal void StopMove()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x00E1;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Stop Move";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 开始点动
        /// </summary>
        internal void StartJog()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x0096;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Start Jog";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 停止点动
        /// </summary>
        internal void StopJog()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort data = 0x00D8;
            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Stop Jog";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置点动速度(单位是rps 每秒圈数)
        /// </summary>
        /// <param name="rps"></param>
        internal void SetJogRps(double rps)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x0030;

            byte[] bs = BitConverter.GetBytes((short)(rps * 240));
            ushort data = BitConverter.ToUInt16(new byte[] { bs[0], bs[1] }, 0);

            try
            {
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Jog Rps";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置点动速度(单位是mm/s)
        /// </summary>
        /// <param name="rps"></param>
        internal void SetJogSpeed(double speed)
        {
            double rps = speed / TestSystemUtil.YPITCH;
            this.SetJogRps(rps);
        }

        /// <summary>
        /// 读取实际绝对位置
        /// </summary>
        /// <returns></returns>
        internal double GetRealAbsPosition()
        {
            return this.GetRealAbsPositionCount() / TestSystemUtil.YMM2CNT;
        }

        /// <summary>
        /// 读取实际绝对位置
        /// </summary>
        /// <returns></returns>
        private int GetRealAbsPositionCount()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x0006;
            ushort[] data;

            try
            {
                data = this.MdbsMaster.ReadHoldingRegisters(node, addr, 2);
                byte[] bsh = BitConverter.GetBytes(data[0]);
                byte[] bsl = BitConverter.GetBytes(data[1]);
                return BitConverter.ToInt32(new byte[] { bsl[0], bsl[1], bsh[0], bsh[1] }, 0);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Get Real Abs Position";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 读取目标位置
        /// </summary>
        internal int GetTargetPositionCount()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x001E;
            ushort[] data;

            try
            {
                data = this.MdbsMaster.ReadHoldingRegisters(node, addr, 2);
                byte[] bsh = BitConverter.GetBytes(data[0]);
                byte[] bsl = BitConverter.GetBytes(data[1]);
                return BitConverter.ToInt32(new byte[] { bsl[0], bsl[1], bsh[0], bsh[1] }, 0);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Read Target Position";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 读取状态代码
        /// </summary>
        internal ushort GetStatusCode()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x0001;
            ushort[] data;

            try
            {
                data = this.MdbsMaster.ReadHoldingRegisters(node, addr, 1);
                return data[0];
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Get Status Code";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 读取报警代码
        /// </summary>
        internal ushort GetAlarmCode()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x0000;
            ushort[] data;

            try
            {
                data = this.MdbsMaster.ReadHoldingRegisters(node, addr, 1);
                return data[0];
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Get Alarm Code";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 数字输入
        /// </summary>
        internal bool[] GetDI()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x0003;
            ushort[] data;
            bool[] di = new bool[8];

            try
            {
                data = this.MdbsMaster.ReadHoldingRegisters(node, addr, 1);
                for(int i=0; i<8;i++)
                {
                    di[i] = ((data[0] >> i) & 0x01) == 0;
                }
                return di;
            }
            catch(Exception ex)
            {
                string err = "Motor error : NODE " + node + ", Get DI";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 获取输入口状态
        /// </summary>
        /// <returns></returns>
        internal StDiStat GetDiStat()
        {
            bool[] di = this.GetDI();
            StDiStat stat = new StDiStat();
            stat.Home = di[2];      // X3
            stat.Interlock = di[3]; // X4
            stat.Mirror = di[6];    // X7
            return stat;
        }

        /// <summary>
        /// 获取马达状态
        /// </summary>
        /// <returns></returns>
        internal StMotorStat GetMotorStat()
        {
            ushort statusCode = this.GetStatusCode();
            StMotorStat stat = new StMotorStat();
            stat.Enable = ((statusCode >> 0) & 0x01) == 1;
            stat.Sampling = ((statusCode >> 1) & 0x01) == 1;
            stat.Error = ((statusCode >> 2) & 0x01) == 1;
            stat.InPos = ((statusCode >> 3) & 0x01) == 1;
            stat.Moving = ((statusCode >> 4) & 0x01) == 1;
            stat.Jogging = ((statusCode >> 5) & 0x01) == 1;
            stat.Decelerating = ((statusCode >> 6) & 0x01) == 1;
            stat.ParamSaving = ((statusCode >> 8) & 0x01) == 1;
            stat.Alarm = ((statusCode >> 9) & 0x01) == 1;
            stat.Homing = ((statusCode >> 10) & 0x01) == 1;
            stat.WaitTime = ((statusCode >> 11) & 0x01) == 1;
            stat.Initialized = ((statusCode >> 15) & 0x01) == 1;
            return stat;
        }

        /// <summary>
        /// 以最大减速度停止，SCL指令SK
        /// </summary>
        internal void SetDO(int iDo, bool enable)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort[] data = new ushort[3];

            data[0] = 0x008B;
            data[1] = (ushort) iDo;
            data[2] = enable ? (ushort)1 : (ushort)0;

            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set DO";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 设置反射镜
        /// </summary>
        /// <param name="enable"></param>
        internal void SetMirror(bool enable)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort[] data = new ushort[3];

            data[0] = 0x008B;
            data[1] = 0x0032;// 输出口Y2
            data[2] = enable ? (ushort)0x4C: (ushort)0x48;

            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Door Unlock";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 取消强制门锁
        /// </summary>
        /// <param name="enable"></param>
        internal void SetDoorUnlock(bool enable)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x007C;
            ushort[] data = new ushort[3];

            data[0] = 0x008B;
            data[1] = 0x0031;// 输出口Y1
            data[2] = enable ? (ushort)0x4C : (ushort)0x48;

            try
            {
                this.MdbsMaster.WriteMultipleRegisters(node, addr, data);
            }
            catch
            {
                string err = "Motor error : NODE " + node + ", Set Door Unlock";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 检查连接状态
        /// </summary>
        internal void CheckConnection()
        {
            try
            {
                if (this.CONNECTED)
                    this.GetStatusCode();
            }
            catch
            {
                this.CONNECTED = false;
            }
        }
        #endregion
    }

    struct StDiStat
    {
        /// <summary>
        /// 互锁信号
        /// </summary>
        internal bool Interlock;
        /// <summary>
        /// 反射镜信号
        /// </summary>
        internal bool Mirror;
        /// <summary>
        /// 反射镜信号
        /// </summary>
        internal bool Home;
    }

    struct StMotorStat
    {
        /// <summary>
        /// 使能
        /// </summary>
        internal bool Enable;
        /// <summary>
        /// 采样中
        /// </summary>
        internal bool Sampling;
        /// <summary>
        /// 驱动器故障
        /// </summary>
        internal bool Error;
        /// <summary>
        /// 运动到位
        /// </summary>
        internal bool InPos;
        /// <summary>
        /// 运动中
        /// </summary>
        internal bool Moving;
        /// <summary>
        /// 点动运行中
        /// </summary>
        internal bool Jogging;
        /// <summary>
        /// 减速中
        /// </summary>
        internal bool Decelerating;
        /// <summary>
        /// 等待输入
        /// </summary>
        //internal bool WaitInput;
        /// <summary>
        /// 参数保存中
        /// </summary>
        internal bool ParamSaving;
        /// <summary>
        /// 驱动器报警
        /// </summary>
        internal bool Alarm;
        /// <summary>
        /// 回原点中
        /// </summary>
        internal bool Homing;
        /// <summary>
        /// 等待时间
        /// </summary>
        internal bool WaitTime;
        /// <summary>
        /// 初始化
        /// </summary>
        internal bool Initialized;
    }
}
