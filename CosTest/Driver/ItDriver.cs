using HandyControl.Controls;
using NationalInstruments.Visa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace CosTest
{
    class ItDriver
    {

        #region VISA
        /// <summary>
        /// Message Based Session
        /// </summary>
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

        object _visaLock = new object();
        /// <summary>
        /// 发送SCPI命令（自动追加\n终止符 + 线程锁）
        /// </summary>
        private void SendCommand(string cmd)
        {
            lock (_visaLock)
            {
                if (mbSession == null)
                    throw new InvalidOperationException("设备未连接");
                mbSession.RawIO.Write(cmd + "\n");
            }
        }
        /// <summary>
        /// 查询SCPI命令（自动追加\n终止符、读取响应 + 线程锁）
        /// </summary>
        private string QueryCommand(string cmd)
        {
            lock (_visaLock)
            {
                if (mbSession == null)
                    throw new InvalidOperationException("设备未连接");
                mbSession.RawIO.Write(cmd + "\n");
                return mbSession.RawIO.ReadString();
            }
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
                    if(s.Contains("0x6500"))
                    {
                        res = s;
                        break;
                    }    
                }

                //连接电流源
                if(res != "")
                {
                    this.mbSession = (MessageBasedSession)rmSession.Open(res);
                    this.mbSession.TimeoutMilliseconds = 2000;
                    this.mbSession.TerminationCharacter = (byte)'\n';
                    this.mbSession.TerminationCharacterEnabled = true;
                    this.SerialNumber = this.GetSerialNumber();
                    this.Remote();
                    SetVoltageAndCurrent(0, 0);
                    this.SetOutput(false);
                    this.SetVoltageLowerLimit(0);
                    this.SetVoltageUpperLimit(TestSystemUtil.LASER_VOLT_UCL);
                    //this.SetVoltage(TestSystemUtil.LASER_VOLT_UCL); 使用预加载电压
                    this.connected = true;
                    return;
                }
                
            }
            catch(Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                // Do Nothing
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
                        this.connected = false;
                }
            }
            catch
            {
                this.connected = false;
            }
        }

        /// <summary>
        /// 读取系统错误(SCPI标准命令，返回 "code, message")
        /// </summary>
        /// <returns></returns>
        internal string GetSystemError()
        {
            string ret = QueryCommand("SYST:ERR?");
            return ret == null ? "" : ret.Trim();
        }



        /// <summary>
        /// 获取SN
        /// </summary>
        /// <returns></returns>
        internal string GetSerialNumber()
        {
            string serialNumber = "";
            string ret = QueryCommand("*IDN?");
            if (ret == null)
                return serialNumber;
            // ITECH Ltd，IT6922A，0123456789AF，1.00
            string[] retSplit = ret.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (retSplit.Length == 4)
                serialNumber = retSplit[2];
            return serialNumber;
        }

        /// <summary>
        /// 读取电源状态
        /// </summary>
        /// <returns></returns>
        internal EnItStat GetCondition()
        {
            string ret = QueryCommand("STAT:QUES:COND?");
            // 0 关闭; 1 CV; 2 CC; 3 Error
            int con = 3;
            if (int.TryParse(ret, out con))
                return (EnItStat)con;
            else
                return EnItStat.ERR;
        }

        /// <summary>
        /// 远程连接
        /// </summary>
        /// <returns></returns>
        internal void Remote()
        {
            SendCommand("SYST:REM");
        }

        #region 电流设置


        //
        /// <summary>
        /// 设置电压和电流0
        /// </summary>
        /// <param name="curr"></param>
        internal void SetCurrentAndVoltage()
        {
            SendCommand("APPL 0,0");
        }

        


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
        }

        /// <summary>
        /// 设置电流输出下限
        /// </summary>
        /// <returns></returns>
        internal void SetCurrentUpperLimit(double limit)
        {
            // Send Command 该命令在IT6500中没有设置
            // SendCommand("CURR:LIMIT " + limit);
        }

        /// <summary>
        /// 设置电流输出上限
        /// </summary>
        /// <returns></returns>
        internal void SetCurrentRange(double range)
        {
            SendCommand("CURR:RANGE " + range);
        }

        /// <summary>
        /// 设置电流步进
        /// </summary>
        /// <returns></returns>
        internal void SetCurrentStep(double step)
        {
            SendCommand("CURR:STEP " + step);
        }

        /// <summary>
        /// 增加电流
        /// </summary>
        /// <returns></returns>
        internal void IncreaseCurrentByStep()
        {
            SendCommand("CURR UP");
        }

        /// <summary>
        /// 降低电流
        /// </summary>
        /// <returns></returns>
        internal void DecreaseCurrentByStep()
        {
            SendCommand("CURR DOWN");
        }

        /// <summary>
        /// 设置过流保护阈值
        /// </summary>
        /// <returns></returns>
        internal void SetOcpValue(double ocpVal)
        {
            SendCommand("CURR:PROT " + ocpVal);
        }

        /// <summary>
        /// 启动或者关闭过流保护功能
        /// </summary>
        /// <returns></returns>
        internal void SetOcpState(bool enable)
        {
            if (enable)
                SendCommand("CURR:PROT:STAT 1");
            else
                SendCommand("CURR:PROT:STAT 0");
        }

        /// <summary>
        /// 是否过流保护
        /// </summary>
        /// <returns></returns>
        internal bool IsOcpTriped()
        {
            string ret = QueryCommand("CURR:PROT:TRIP?");
            // 0 OFF; 1 ON;
            int res = 0;
            if (int.TryParse(ret, out res))
                return (res == 1);
            else
                return false;
        }

        /// <summary>
        /// 清除过流保护功能
        /// </summary>
        /// <returns></returns>
        internal void ClearOcpTrip()
        {
            SendCommand("CURR:PROT:CLE");
        }

        /// <summary>
        /// 读取电流设置
        /// </summary>
        /// <returns></returns>
        internal double GetCurrent()
        {
            string ret = QueryCommand("CURR?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }

        /// <summary>
        /// 读取电流步进
        /// </summary>
        /// <returns></returns>
        internal double GetCurrentStep()
        {
            string ret = QueryCommand("CURR:STEP?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
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
        }

        /// <summary>
        /// 设置电压步进
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageStep(double step)
        {
            SendCommand("VOLT:STEP " + step);
        }

        /// <summary>
        /// 增加电压
        /// </summary>
        /// <returns></returns>
        internal void IncreaseVoltageByStep()
        {
            SendCommand("VOLT UP");
        }

        /// <summary>
        /// 降低电压
        /// </summary>
        /// <returns></returns>
        internal void DecreaseVoltageByStep()
        {
            SendCommand("VOLT DOWN");
        }

        /// <summary>
        /// 设置电压保护阈值
        /// </summary>
        /// <returns></returns>
        internal void SetOvpValue(double ocpVal)
        {
            SendCommand("VOLT:PROT " + ocpVal);
        }

        /// <summary>
        /// 启动或者关闭过压保护功能
        /// </summary>
        /// <returns></returns>
        internal void SetOvpState(bool enable)
        {
            if (enable)
                SendCommand("VOLT:PROT:STAT 1");
            else
                SendCommand("VOLT:PROT:STAT 0");
        }

        /// <summary>
        /// 是否过压保护
        /// </summary>
        /// <returns></returns>
        internal bool IsOvpTriped()
        {
            //// Send Command
            //SendCommand("VOLT:PROT:TRIP?");
            //string ret = QueryCommand("VOLT:PROT:TRIP?");
            //// 0 OFF; 1 ON;
            //int res = 0;
            //if (int.TryParse(ret, out res))
            //    return (res == 1);
            //else
            //    return false;

            return false;
        }

        /// <summary>
        /// 清除过压保护功能
        /// </summary>
        /// <returns></returns>
        internal void ClearOvpTrip()
        {
            SendCommand("VOLT:PROT:CLE");
        }

        /// <summary>
        /// 设置电压输出下限
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageLowerLimit(double limit)
        {
            SendCommand("VOLT:LIMIT " + limit);
        }

        /// <summary>
        /// 设置电压输出上限
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageUpperLimit(double range)
        {
            SendCommand("VOLT:RANGE " + range);
        }

        /// <summary>
        /// 读取电压设置
        /// </summary>
        /// <returns></returns>
        internal double GetVoltage()
        {
            string ret = QueryCommand("VOLT?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }

        /// <summary>
        /// 读取电压步进
        /// </summary>
        /// <returns></returns>
        internal double GetVoltageStep()
        {
            string ret = QueryCommand("VOLT:STEP?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }

        /// <summary>
        /// 读取电压输出上限
        /// </summary>
        /// <returns></returns>
        internal double GetVoltageLimit()
        {
            string ret = QueryCommand("VOLT:LIMIT?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }
        #endregion

        #region 输出
        /// <summary>
        /// 设置输出
        /// </summary>
        /// <returns></returns>
        internal void SetOutput(bool enable)
        {
           // SetVoltageAndCurrent(0, 0);
            if (enable)
                SendCommand("OUTP 1");
            else
                SendCommand("OUTP 0");
        }

        /// <summary>
        /// 读取输出
        /// </summary>
        /// <returns></returns>
        internal bool GetOutput()
        {
            string ret = QueryCommand("OUTP?");
            // 0 OFF; 1 ON;
            int res = 0;
            if (int.TryParse(ret, out res))
                return (res == 1);
            else
                return false;
        }
        #endregion

        #region 上升下降时间
        /// <summary>
        /// 读取上升时间
        /// </summary>
        /// <returns></returns>
        internal double GetRiseTime()
        {
            string ret = QueryCommand("RISE?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }

        /// <summary>
        /// 设置上升时间
        /// </summary>
        /// <returns></returns>
        internal void SetRiseTime(double rise)
        {
            SendCommand("RISE " + rise);
        }


        /// <summary>
        /// 读取下降时间
        /// </summary>
        /// <returns></returns>
        internal double GetFallTime()
        {
            string ret = QueryCommand("FALL?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
        }

        /// <summary>
        /// 设置下降时间
        /// </summary>
        /// <returns></returns>
        internal void SetFallTime(double fall)
        {
            SendCommand("FALL " + fall);
        }

        #endregion

        #region APPLY
        /// <summary>
        /// 设置电流和电压
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageAndCurrent(double volt, double curr)
        {
            SendCommand("APPL " + volt + "," + curr);
        }

        /// <summary>
        /// 设置电压
        /// </summary>
        /// <returns></returns>
        internal void SetVoltageAndCurrent(double volt, double minVolt, double maxVolt, double curr, double minCurr, double maxCurr)
        {
            SendCommand("APPL " + volt + "|" + minVolt + "|" + maxVolt + "," + curr + "|" + minCurr + "|" + maxCurr);
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
            // ErrLog.WriteLine("GetMeasCurrent raw:" + ret);
            double res = 0;
            if (double.TryParse(ret, out res))
            {
                res = res * TestSystemUtil.IF_CAL_BASE + TestSystemUtil.IF_OFFSET_BASE;
                res = Math.Round(res * TestSystemUtil.IF_CAL + TestSystemUtil.IF_OFFSET, 3);
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
            double res = 0;
            if (double.TryParse(ret, out res))
            {
                res = res * TestSystemUtil.VF_CAL_BASE + TestSystemUtil.VF_OFFSET_BASE;
                res = Math.Round(res * TestSystemUtil.VF_CAL + TestSystemUtil.VF_OFFSET, 3);
                return res;
            }
            else
                return 0.0;
        }

        /// <summary>
        /// 读取量测功率
        /// </summary>
        /// <returns></returns>
        internal double GetMeasPower()
        {
            string ret = QueryCommand("MEAS:POW?");
            double res = 0;
            if (double.TryParse(ret, out res))
                return res;
            else
                return 0.0;
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
