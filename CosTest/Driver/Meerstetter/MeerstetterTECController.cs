using NF.Interfaces;
using NF.Interfaces.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CosTest
{

    /// <summary>
    /// Meerstetter TEC Controller driver.
    /// </summary>
    public class MeerstetterTECController 
    {

        /// <summary>
        /// 底层串口对象
        /// </summary>
        private SerialPortChassis Chassis { get; set; }
        /// <summary>
        /// 初始化标志
        /// </summary>
        internal bool Initialized { get; set; }

        private UInt16 SequenceNumber = 0;
        private byte Address = 0;
        private int Instance = 1;
        /// <summary>
        /// Initializes an instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="slot"></param>
        /// <param name="subSlot"></param>
        /// <param name="chassis"></param>
        public MeerstetterTECController(string resourceName, string slot = "0", string subSlot = "1")
        {
            Chassis = new SerialPortChassis(resourceName);

            try
            {
                Address = byte.Parse(slot);
                Instance = int.Parse(subSlot);
            }
            catch (Exception ex)
            {
                throw new ArithmeticException(string.Format("Invalid Slot and SubSlot. Slot: {0}; SubSlot: {1}",
                    slot, subSlot), ex);
            }
        }

        public double TemperatureSetPoint_DegreeC
        {
            set
            {

                string command = BuildCommand("VS", (int)MeerstetterTECCommands.SetTemperatureSetPoint, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                _ = GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.GetTemperatureSetPoint, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }


        /// <summary>
        /// Gets or sets whether the TEC controller output is enabled.
        /// </summary>
        public bool IsOutputEnabled
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.OutputEnabled, "");
                string response = this.GetResponseFrame(Query(command));
                int status = Convert.ToInt32(response.Substring(7, 8), 16);
                return status == 1;
            }
            set
            {
                if (value == true)
                {
                    string temp = this.GetResponseFrame(Query(BuildCommand("VS", (int)MeerstetterTECCommands.OutputEnabled, "00000001")));
                }
                else
                {
                    string temp = this.GetResponseFrame(Query(BuildCommand("VS", (int)MeerstetterTECCommands.OutputEnabled, "00000000")));
                }
            }
        }
        public double ReadTemperature_DegreeC()
        {
            //string command = BuildCommand("?T", "");
            string command = BuildCommand("?VR", (int)MeerstetterTECCommands.GetTemperature, "");
            string response = this.GetResponseFrame(Query(command));
            return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
        }



        #region Meerstetter only

        public MeerstetterTECSourceFunction SouceFunction
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.Mode, "");
                string response = this.GetResponseFrame(Query(command));
                MeerstetterTECSourceFunction mode;
                switch (Convert.ToUInt32(response.Substring(7, 8), 16))
                {
                    case 0:
                        mode = MeerstetterTECSourceFunction.StaticMode;
                        break;
                    case 1:
                        mode = MeerstetterTECSourceFunction.LiveMode;
                        break;
                    case 2:
                    default:
                        mode = MeerstetterTECSourceFunction.TemperatureControllerMode;
                        break;
                }
                return mode;
            }
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.Mode, ((int)value).ToString("X8"));
                string response = this.GetResponseFrame(Query(command));
                if (value == MeerstetterTECSourceFunction.LiveMode)
                {
                    command = BuildCommand("VS", (int)MeerstetterTECCommands.TemperatureSourceMode, "00000001");
                    response = this.GetResponseFrame(Query(command));
                }
            }
        }


        /// <summary>
        /// Gets or sets the status is stable.
        /// </summary>
        public int isStable
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.GetStableStatus, "");
                string response = this.GetResponseFrame(Query(command));
                return Convert.ToInt32(response.Substring(7, 8), 16);
            }
        }

        /// <summary>
        /// Gets or sets current protection limit in A.
        /// </summary>
        public double CurrentProtectionLimit_A
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.CurrentProtectionLimit, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.CurrentProtectionLimit, "");
                string response = GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        /// <summary>
        /// Gets or sets voltage protection limit in V.
        /// </summary>
        public double VoltageProtectionLimit_V
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.VoltageProtectionLimit, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.VoltageProtectionLimit, "");
                string response = GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        #region PID setting
        /// <summary>
        /// Gets or sets proportional constant value of PID.
        /// </summary>
        public double TemperatureGain
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.PIDGain, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.PIDGain, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        /// <summary>
        /// Gets or sets derivative constant value of PID.
        /// </summary>
        public double TemperatureDerivative
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.PIDDerivative, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.PIDDerivative, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        /// <summary>
        /// Gets or sets integral constant value of PID.
        /// </summary>
        public double TemperatureIntegral
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.PIDIntegral, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.PIDIntegral, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }
        #endregion

        public string BuildCommand(string Command, int ID, string Data)
        {
            byte[] uc = Encoding.UTF8.GetBytes(Command + ID.ToString("X4") + this.Instance.ToString("X2") + Data);
            return BuildCommand(uc);
        }
        public string BuildCommand(string Command, string Instance, string Data)
        {
            byte[] uc = Encoding.UTF8.GetBytes(Command + Instance + Data);
            return BuildCommand(uc);
        }
        public string BuildCommand(string Command, string Data)
        {
            return BuildCommand(Command, this.Instance.ToString("X2"), Data);
        }
        public string BuildCommand(byte[] ucData)
        {
            char[] ucHex = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int Length = ucData.Length;
            char[] buffer = new char[12 + Length];
            ushort usCRC = 0;
            //Random Seq = new Random(this.SequenceNumber);

            buffer[0] = (char)0x23;
            buffer[1] = ucHex[this.Address / 16];
            buffer[2] = ucHex[this.Address % 16];
            this.SequenceNumber++;
            buffer[3] = ucHex[this.SequenceNumber / 4096];
            buffer[4] = ucHex[(this.SequenceNumber / 256) % 16];
            buffer[5] = ucHex[(this.SequenceNumber % 256) / 16];
            buffer[6] = ucHex[(this.SequenceNumber % 256) % 16];
            for (int i = 0; i < Length; i++)
            {
                buffer[7 + i] = (char)ucData[i];
            }
            usCRC = 0;
            for (int i = 0; i < 7 + ucData.Length; i++)
            {
                usCRC = CRC16Algorithm(usCRC, (byte)buffer[i]);
            }
            buffer[7 + Length] = (ucHex[usCRC / 4096]);
            buffer[8 + Length] = (ucHex[(usCRC / 256) % 16]);
            buffer[9 + Length] = (ucHex[(usCRC % 256) / 16]);
            buffer[10 + Length] = (ucHex[(usCRC % 256) % 16]);
            buffer[11 + Length] = (char)0x0D;
            string command = new string(buffer);
            return new string(buffer);
        }

        ushort CRC16Algorithm(ushort CRC, byte Ch)
        {
            uint genPoly = 0x1021; //CCITT CRC-16 Polynominal
            uint uiCharShifted = ((uint)Ch & 0x00FF) << 8;
            CRC = (ushort)(CRC ^ uiCharShifted);
            for (int i = 0; i < 8; i++)
            {
                if ((CRC & 0x8000) > 0)
                {
                    CRC = (ushort)((CRC << 1) ^ genPoly);
                }
                else
                {
                    CRC = (ushort)(CRC << 1);
                }
            }
            CRC &= 0xFFFF;
            return CRC;
        }
        public string GetResponseFrame(string response)
        {
            string[] responseList = response.Split('\r');
            string frame = null;
            foreach (string item in responseList)
            {
                if (item.Substring(3, 4) == this.SequenceNumber.ToString("X4"))
                {
                    frame = item + '\r';
                    break;
                }
            }
            return frame;
        }

        public DeviceStatus DeviceStatus
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.DeviceStatus, "");
                Chassis.Write(command);
                Thread.Sleep(200);
                string response = GetResponseFrame(Chassis.ReadString()).Substring(7, 8);
                var vale = Convert.ToInt32(response, 16);
                return (DeviceStatus)vale;
            }
        }

        public void Reset()
        {
            var cmd = BuildCommand("RS", "", "");
            GetResponseFrame(Query(cmd));

            System.Threading.Thread.Sleep(4000);
        }

        public double TemperatureDeviationDegreeC
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.TemperatureDeviation, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.TemperatureDeviation, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                GetResponseFrame(Query(command));
            }

        }

        #endregion Meerstetter only

        /// <summary>
        /// 0: Temperature regulation is not active
        /// 1: Is not stable
        /// 2: Is stable
        /// </summary>
        public bool IsTemperatureStabilized
        {
            get
            {

                return isStable == 2;
            }
        }


        #region helper functions

        public string InstrumentIDN => Query(BuildCommand("?IF", "", "")).Substring(7);

        public double HiTempLimit_DegreeC
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.UpperErrorThreshold, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.UpperErrorThreshold, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }
        public double LowTempLimit_DegreeC
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.LowerErrorThreshold, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.LowerErrorThreshold, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        /// <summary>
        /// 最小稳定窗口
        /// </summary>
        public double MinTimeInWindow
        {
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.MinTimeInWindow, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.MinTimeInWindow, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                GetResponseFrame(Query(command));
            }
        }

        public double MaxStablizationTime
        {
            set
            {
                string command = BuildCommand("VS", (int)MeerstetterTECCommands.MaxStabilizationTime, ConvertMath.ConvertFloatToIEEE754ReservedString((float)value));
                this.GetResponseFrame(Query(command));
            }
            get
            {
                string command = BuildCommand("?VR", (int)MeerstetterTECCommands.MaxStabilizationTime, "");
                string response = this.GetResponseFrame(Query(command));
                return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
            }
        }

        /// <summary>
        /// 通过串口和设备通讯
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string Query(string command)
        {
            Chassis.Write(command);

            //wait until data arrived;
            bool dataArrived = false;
            int time_left = 15;
            while (!dataArrived)
            {
                if (Chassis.BytesToRead > 0)
                    dataArrived = true;
                Thread.Sleep(20);
                time_left--;

                //time out and not data received
                if (time_left < 0)
                    throw new /*ZidanInstrument*/Exception("串口操作超时!");
            }
            //yes we get data here 
            return Chassis.ReadString();

        }

        /// <summary>
        /// TEC 电流
        /// </summary>
        /// <returns></returns>
        public double ReadTecDcCurrent_A()
        {
            string command = BuildCommand("?VR", (int)MeerstetterTECCommands.GetTECCurrent, "");
            string response = GetResponseFrame(Query(command));
            return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
        }

        /// <summary>
        /// TEC 电压
        /// </summary>
        /// <returns></returns>
        public double ReadTecDcVoltage_V()
        {
            var command = BuildCommand("?VR", (int)MeerstetterTECCommands.GetTECVoltage, "");
            var response = this.GetResponseFrame(Query(command));
            return ConvertMath.ConvertIEEE754ReservedStringToFloat(response.Substring(7, 8));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private void Initialize()
        {
            try
            {
                if (Initialized == true)
                    return;
                Chassis.Initialize();

                //试一下IDN
                try
                {
                    var idn = InstrumentIDN;
                }
                catch (Exception inner)
                {
                    Chassis.CloseSerialPort();
                    throw inner;
                }
                Initialized = true;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(string.Format("初始化失败，原因={0}", ex.Message));
                Initialized = false;
            }
        }

        public void Cleanup()
        {
            IsOutputEnabled = false;
            //base.Cleanup();
        }

        #endregion

        #region 非原厂代码
        /// <summary>
        /// 打开设备
        /// </summary>
        internal void Open()
        {
            try
            {

                this.Initialize();
                this.IsOutputEnabled = false;
                this.CurrentProtectionLimit_A = TestSystemUtil.MaxTecCurrentA;
                this.VoltageProtectionLimit_V = TestSystemUtil.MaxTecVoltageV;
            }
            catch
            {
                Initialized = false;
            }
        }
        /// <summary>
        /// 检查连接状态
        /// </summary>
        internal void CheckConnection()
        {
            try
            {

                //试一下IDN
                if(Initialized)
                {
                    var idn = InstrumentIDN;
                }
            }
            catch
            {
                Initialized = false;
            }
        }
        #endregion
    }
}
