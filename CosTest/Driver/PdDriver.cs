using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO.Ports;
using NModbus;
using NModbus.Serial;

namespace CosTest
{
    class PdDriver
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
        /// 量程系数
        /// </summary>
        private double[] RAT;
        /// <summary>
        /// RANGE
        /// </summary>
        private ushort range;
        /// <summary>
        /// RANGE
        /// </summary>
        internal ushort RANGE 
        { 
            get { return this.range; }
            set { this.range = value < 4 ? value : (ushort) 3; this.SetPhotoRange(this.range); }
        }
        /// <summary>
        /// PO_CAL
        /// </summary>
        internal double PO_CAL { get; set; }
        /// <summary>
        /// PO_OFFSET
        /// </summary>
        internal double PO_OFFSET { get; set; }
        /// <summary>
        /// PO_CAL_BASE
        /// </summary>
        internal double PO_CAL_BASE { get; set; }
        /// <summary>
        /// PO_OFFSET_BASE
        /// </summary>
        internal double PO_OFFSET_BASE { get; set; }

        internal PdDriver()
        {
            this.MdbsSerialPort = new SerialPort();
            this.MdbsSerialPort.PortName = TestSystemUtil.PD_COMM_PORT;
            this.MdbsSerialPort.StopBits = StopBits.One;
            this.MdbsSerialPort.Parity = Parity.None;
            this.MdbsSerialPort.BaudRate = 115200;
            this.MdbsSerialPort.ReadTimeout = 2000;
            this.MdbsSerialPort.WriteTimeout = 2000;
            this.RAT = new double[4] { 1.0e-4, 1.0e-3, 1.0e-2, 1.0e-1 };
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        internal void Open()
        {
            try
            {
                this.MdbsSerialPort.Open();
                this.MdbsFactory = new ModbusFactory();
                this.MdbsMaster = this.MdbsFactory.CreateRtuMaster(this.MdbsSerialPort);
                this.SetPhotoRange((ushort)TestSystemUtil.PD_RNG);// 0到3
                this.CONNECTED = true;
            }
            catch
            {
                this.CONNECTED = false;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        internal void Close()
        {
            try
            {
                this.MdbsSerialPort?.Close();
                this.CONNECTED = false;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                this.CONNECTED = false;
            }
        }

        /// <summary>
        /// 设置量程
        /// </summary>
        private void SetPhotoRange(ushort range)
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x1000;
            ushort data = range;
            try
            {
                // 量程0到3
                this.MdbsMaster.WriteSingleRegister(node, addr, data);
            }
            catch
            {
                string err = "PD Driver error : NODE " + node + ", Set Range";
                ErrLog.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 读取光功率采样
        /// </summary>
        internal ushort GetPhotoAdc()
        {
            byte node = SLAVE_NODE;
            ushort addr = 0x2000;
            ushort[] data;

            try
            {
                // 最高2位是RANGE
                data = this.MdbsMaster.ReadInputRegisters(node, addr, 1);
                return (ushort)(data[0] & 0x0FFF);
            }
            catch
            {
                string err = "PD Driver error : NODE " + node + ", ADC";
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
                    this.GetPhotoAdc();
            }
            catch
            {
                this.CONNECTED = false;
            }
        }


        /// <summary>
        /// 获取PD光电流, 单位是mA
        /// 获取功率, 单位是W
        /// </summary>
        internal double GetPhotoPower()
        {
            // ADC采样
            int adc = this.GetPhotoAdc();
            adc = adc > TestSystemUtil.PDI_ADC_OFFSET ? (adc - TestSystemUtil.PDI_ADC_OFFSET) : 0;
            // 通道计算
            double PDI = adc * this.RAT[this.RANGE];
            // 基础校准
            PDI = PDI* this.PO_CAL_BASE + this.PO_OFFSET_BASE;
            // 校准
            PDI = Math.Round(PDI * this.PO_CAL + this.PO_OFFSET, 3);
            //ErrLog.WriteLine("ADC " + adc + "; PDI " + PDI);
            return PDI;
        }
    }

    /// <summary>
    /// LIVSCAN参数
    /// 顺序不可以更改
    /// </summary>
    struct StGoLiv
    {
        /// <summary>
        /// 起始电流(mA)
        /// </summary>
        internal double START;
        /// <summary>
        /// 步进电流
        /// </summary>
        internal double STEP;
        /// <summary>
        /// 结束电流(mA)
        /// </summary>
        internal double STOP;
        /// <summary>
        /// 延时时间(ms)
        /// </summary>
        internal int DELAY;
    }

    /// <summary>
    /// PIV结构
    /// </summary>
    struct StGoPIV
    {
        /// <summary>
        /// PD电流值
        /// </summary>
        internal double PDI;
        /// <summary>
        /// LD电流值
        /// </summary>
        internal double LDI;
        /// <summary>
        /// LD电压直
        /// </summary>
        internal double LDV;
    }
}