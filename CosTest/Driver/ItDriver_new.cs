using NationalInstruments.Visa;
using System;
using System.Threading;

namespace CosTest
{
    class ItDriver
    {
        #region VISA
        private MessageBasedSession mbSession;
        #endregion
        /// <summary>
        /// 设备连接
        /// </summary>
        private bool connected;
        /// <summary>
        /// 设备连接
        /// </summary>
        internal bool CONNECTED { get { return this.connected; } }
        /// <summary>
        /// 设备序列号
        /// </summary>
        internal string SerialNumber { get; set; }

        internal ItDriver()
        {

        }

        /// <summary>
        /// 打开设备
        /// </summary>
        internal void Open()
        {
            this.connected = false;

            try
            {
                // 寻找电流源的资源
                string res = "";
                var rmSession = new ResourceManager();
                var resources = rmSession.Find("(ASRL|GPIB|TCPIP|USB)?*");
                foreach (string s in resources)
                {
                    if (s.Contains("0x6900"))
                    {
                        res = s;
                        break;
                    }
                }

                //连接电流源
                if (res != "")
                {
                    mbSession = (MessageBasedSession)rmSession.Open(res);
                    mbSession.TimeoutMilliseconds = 2000;
                    mbSession.TerminationCharacter = (byte)'\n';
                    mbSession.TerminationCharacterEnabled = true;

                    // 清除状态
                    CLS();
                    // 检查错误
                    string error_ret = GetSystemError();
                    if (error_ret != "")
                    {
                        ErrLog.WriteLine("ItDriver Err:" + error_ret);
                    }
                    //读序列号
                    this.SerialNumber = this.GetSerialNumber();
                    //远程模
                    this.Remote();
                    //关闭输出
                    this.SetOutput(false);


                    SetVoltage(0);
                    SetCurrent(0);
                    SetOutput(true);

                    if (GetMeasCurrent() != 0 || GetMeasVoltage() != 0)
                    {
                        Thread.Sleep(2000);
                        TestSystemUtil
                        ErrLog.WriteLine($"电压：{GetMeasVoltage()} 电流：{GetMeasCurrent()}");
                    }
                    else
                    {
                        ErrLog.WriteLine($"电压：{GetMeasVoltage()} 电流：{GetMeasCurrent()}");
                    }
                    SetVoltage(1.7);


                    //ErrLog.WriteLine("设置电压为0.5V");
                    //this.SetVoltage(1.7);
                    //SetCurrent(0.0);
                    //Thread.Sleep(2000);
                    //ErrLog.WriteLine("设置电压为0V");
                    //this.SetVoltage(0);

                    // 设置默认电压上下限
                    this.SetVoltageLowerLimit(0);
                    // 设置默认电压上上限
                    this.SetVoltageUpperLimit(TestSystemUtil.LASER_VOLT_UCL);

                    this.connected = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                this.mbSession?.Dispose();
            }

            // 输出错误
            ErrLog.WriteLine("没有找到电流源设备!");
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        internal void Close()
        {
            try
            {
                try { SetOutput(false); } catch { }         // 关闭输出后再断开
                try { SendCommand("SYST:LOC"); } catch { }  // 恢复本地模式
                this.mbSession?.Dispose();
                this.connected = false;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                this.connected = false;
            }
        }

        /// <summary>
        /// 检查连接状态
        /// </summary>
        internal void CheckConnection()
        {
            try
            {
                if (this.connected)
                {
                    if (this.GetSerialNumber() == "")
                    {
                        this.connected = false;
                    }
                }
            }
            catch
            {
                this.connected = false;
            }
        }

        object _visaLock = new object();
        /// <summary>
        /// 获取SN
        /// </summary>
        /// <returns></returns>
        internal string GetSerialNumber()
        {
            string serialNumber = "";
            lock (_visaLock)
            {
                string ret = QueryCommand("*IDN?");
                if (ret == null)
                    return serialNumber;
                // ITECH Ltd，IT6922A，0123456789AF，1.00
                string[] retSplit = ret.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (retSplit.Length == 4)
                    serialNumber = retSplit[2];
                return serialNumber;
            }
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        internal string GetSystemError()
        {
            return QueryCommand("SYST:ERR?");
        }

        /// <summary>
        /// 清除状态
        /// </summary>
        internal void CLS()
        {
            SendCommand("*CLS");
            Thread.Sleep(100);
        }

        /// <summary>
        /// 远程连接
        /// </summary>
        /// <returns></returns>
        internal void Remote()
        {
            SendCommand("SYST:REM");
            Thread.Sleep(100);
        }

        #region 电流设置
        /// <summary>
        /// 设置电流
        /// </summary>
        /// <returns></returns>
        internal void SetCurrent(double curr)
        {
            curr = curr * TestSystemUtil.SI_CAL_BASE + TestSystemUtil.SI_OFFSET_BASE;
            curr = curr * TestSystemUtil.SI_CAL + TestSystemUtil.SI_OFFSET;
            curr = curr > 0.0 ? curr : 0.0;
            SendCommand("CURR " + curr);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 设置电流输出下限
        /// </summary>
        /// <returns></returns>
        internal void SetCurrentUpperLimit(double limit)
        {
            // Send Command 该命令在IT6500中没有设置
            // this.mbSession.RawIO.Write("CURR:LIMIT " + limit);
        }
        #endregion

        #region 电压设置
        /// <summary>
        /// 设置电压
        /// </summary>
        /// <returns></returns>
        internal void SetVoltage(double volt)
        {
            SendCommand("VOLT " + volt);
            Thread.Sleep(100);
        }


        /// <summary>
        /// 是否过压保护
        /// </summary>
        /// <returns></returns>
        internal bool IsOvpTriped()
        {
            //// Send Command
            //this.mbSession.RawIO.Write("VOLT:PROT:TRIP?");
            //// Read Return
            //string ret = this.mbSession.RawIO.ReadString();
            //// 0 OFF; 1 ON;
            //int res = 0;
            //if (int.TryParse(ret, out res))
            //    return (res == 1);
            //else
            //    return false;

            return false;
        }

        /// <summary>
        /// 设置电压输出下限
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageLowerLimit(double limit)
        {
            SendCommand("VOLT:LIM:LOW  " + limit);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 设置电压输出上限
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageUpperLimit(double range)
        {
            SendCommand("VOLT:LIM:HIGH " + range);
            Thread.Sleep(100);
        }
        #endregion

        #region 输出

        /// <summary>
        /// 设置输出
        /// </summary>
        /// <returns></returns>
        internal void SetOutput(bool enable)
        {
            if (enable)
                SendCommand("OUTP 1");
            else
                SendCommand("OUTP 0");
            Thread.Sleep(100);
        }

        /// <summary>
        /// 读取输出
        /// </summary>
        /// <returns></returns>
        internal bool GetOutput()
        {
            string ret = QueryCommand("OUTP?");
            int res = 0;  // 0 OFF; 1 ON;
            if (int.TryParse(ret, out res))
                return (res == 1);
            else
                return false;
        }
        #endregion

        #region 量测命令

        /// <summary>
        /// 读取量测电流
        /// </summary>
        /// <returns></returns>
        internal double GetMeasCurrent()
        {
            string ret = QueryCommand("MEAS:CURR?");
           // ErrLog.WriteLine("GetMeasCurrent ret:" + ret);
            double res = 0;
            if (double.TryParse(ret, out res))
            {
                res = res * TestSystemUtil.IF_CAL_BASE + TestSystemUtil.IF_OFFSET_BASE;
                res = Math.Round(res * TestSystemUtil.IF_CAL + TestSystemUtil.IF_OFFSET, 3);
                //ErrLog.WriteLine("GetMeasCurrent res:" + res);
                return res;
            }
            else
                return 0.0;
        }

        /// <summary>
        /// 读取量测电压
        /// </summary>
        /// <returns></returns>
        internal double GetMeasVoltage()
        {
            string ret = QueryCommand("MEAS:VOLT?");
            //ErrLog.WriteLine("GetMeasVoltage ret:" + ret);
            double res = 0;
            if (double.TryParse(ret, out res))
            {
                res = res * TestSystemUtil.VF_CAL_BASE + TestSystemUtil.VF_OFFSET_BASE;
                res = Math.Round(res * TestSystemUtil.VF_CAL + TestSystemUtil.VF_OFFSET, 3);
               // ErrLog.WriteLine("GetMeasVoltage res:" + res);
                return res;
            }
            else
                return 0.0;
        }

        /// <summary>
        /// 读取测量功率 
        /// </summary>
        internal double GetMeasPower()
        {
            string ret = QueryCommand("MEAS:POW?");
            double res = 0;
            if (double.TryParse(ret, out res))
            {
                res = Math.Round(res, 6);
                return res;
            }
            return 0.0;
        }


        /// <summary>
        /// 发送命令
        /// </summary>
        internal void SendCommand(string cmd)
        {
            lock (_visaLock)
            {
                if (mbSession == null)
                    throw new InvalidOperationException("设备未连接");
                mbSession.RawIO.Write(cmd + "\n");
            }
        }

        /// <summary>
        /// 查询命令
        /// </summary>
        internal string QueryCommand(string cmd)
        {
            lock (_visaLock)
            {
                if (mbSession == null)
                    throw new InvalidOperationException("设备未连接");

                mbSession.RawIO.Write(cmd + "\n");

                // 记录开始读取的时间
                 var startTime = DateTime.Now;
                string response = mbSession.RawIO.ReadString();
                 var elapsed = (DateTime.Now - startTime).TotalMilliseconds;

                ErrLog.WriteLine($"QueryCommand: {cmd}, Response: {response}, Elapsed Time: {elapsed} ms");
                return response;
            }
        }
        #endregion
    }

    internal enum EnItStat
    {
        OFF,
        CV,
        CC,
        ERR
    }
}