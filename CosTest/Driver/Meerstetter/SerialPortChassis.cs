using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    /// <summary>
    /// chassis wrapper for class CSerialPort
    /// </summary>
    public class SerialPortChassis
    {
        //property variables

        /// <summary>
        /// sp object
        /// </summary>
        private SerialPort _comPort = null;

        /// <summary>
        /// Resourece Name
        /// </summary>
        private string ResourceName;


        #region 串口默认配置
        //enable/disable dtr
        private bool dtr = false;

        //enable/disable rts
        private bool rts = false;

        /// <summary>
        /// time out in seconds;
        /// </summary>
        private int time_out_count = 5;

        /// <summary>
        /// The baud rate must be supported by the user's serial driver. The default value is 9600 bits per second (bps).
        /// </summary>
        private int _baudRate = 57600;

        /// <summary>
        /// Gets or sets the parity-checking protocol.
        /// </summary>
        private Parity _parity = Parity.None;

        /// <summary>
        /// Gets or sets the standard number of stopbits per byte.The default value for StopBits is One.
        /// </summary>
        private StopBits _stopBits = StopBits.One;

        /// <summary>
        /// Gets or sets the standard length of data bits per byte.
        /// The range of values for this property is from 5 through 8. The default value is 8.
        /// </summary>
        private int _dataBits = 8;

        /// <summary>
        /// flow control for the serial port
        /// </summary>
        private Handshake hankShakeOption = Handshake.None;

        /// <summary>
        /// Gets or sets the port for communications, including but not limited to all available COM ports.
        /// A list of valid port names can be obtained using the GetPortNames method.
        /// </summary>
        private string _portName = string.Empty;



        /// <summary>
        /// Gets or sets the byte encoding for pre- and post-transmission conversion of text.
        /// An EncodingEncoding object. The default is ASCIIEncoding.
        /// </summary>
        private Encoding _stringEncoding = new ASCIIEncoding();

        #endregion

        public int BytesToRead => _comPort.IsOpen ? _comPort.BytesToRead : 0;

        /// <summary>
        /// initial the Serial port Chassis
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resourceName"></param>
        /// <param name="logger"></param>
        public SerialPortChassis(string resourceName)
        {
            ResourceName = resourceName;
            _comPort = new SerialPort();
        }

        /// <summary>
        /// initialize chassis, open the comport if online is set to be true;
        /// </summary>
        public void Initialize()
        {
            #region Parse config string

            string[] ConfigParams = ResourceName.Split(';');

            for (int i = 0; i < ConfigParams.Length; i++)
            {
                string[] ConfigItem = ConfigParams[i].Split(':');
                if (ConfigItem.Length < 2)
                    continue;
                switch (ConfigItem[0].ToLower())
                {
                    case "portname":
                        {
                            if (SerialPort.GetPortNames().Contains(ConfigItem[1].ToUpper()) == false)
                            {
                                throw new /*ZidanInstrument*/Exception("没有配置串口名:" + ConfigItem[1]);
                            }
                            else
                            {
                                _portName = ConfigItem[1].ToUpper();
                            }
                        }
                        break;
                    case "baudrate":
                        _baudRate = Convert.ToInt32(ConfigItem[1]); break;
                    case "parity":
                        _parity = (Parity)Enum.Parse(typeof(Parity), ConfigItem[1]); break;
                    case "stopbits":
                        _stopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigItem[1]); break;
                    case "databits":
                        _dataBits = Convert.ToInt16(ConfigItem[1]); break;
                    case "handshake":
                        hankShakeOption = (Handshake)Enum.Parse(typeof(Handshake), ConfigItem[1]); break;
                    case "dtr":
                        dtr = Convert.ToBoolean(ConfigItem[1]); break;
                    case "rts":
                        rts = Convert.ToBoolean(ConfigItem[1]); break;
                    default:
                        break;
                }
            }
            #endregion

            //create the SerialPort object and config it;

            _comPort.PortName = _portName;
            _comPort.BaudRate = _baudRate;
            _comPort.DataBits = _dataBits;
            //_comPort.Encoding = _stringEncoding;
            _comPort.StopBits = _stopBits;
            _comPort.Parity = _parity;
            //_comPort.Handshake = hankShakeOption;
            //_comPort.DtrEnable = dtr;
            //_comPort.RtsEnable = rts;
            //_comPort.RtsEnable = true;// 硬编码
            if (_comPort.IsOpen == false)
            {
                try
                {
                    _comPort.Close();
                    _comPort.Open();
                    _comPort.DiscardInBuffer();
                    _comPort.DiscardOutBuffer();
                }
                catch(Exception ex)
                {
                    ErrLog.WriteLine(ex.ToString());
                    throw new /*ZidanInstrument*/Exception("不能打开串口:" + _portName);
                }

            }
        }


        /// <summary>
        /// 写入字符串数据到串口
        /// </summary>
        /// <param name="data"></param>
        public void Write(string data)
        {
            if (_comPort.IsOpen == false)
                _comPort.Open();
            _comPort.Write(data);

            //wait until all data writes
            int timeout = time_out_count;
            while (_comPort.BytesToWrite > 0 && timeout > 0)
            {
                System.Threading.Thread.Sleep(40);
                timeout--;
            }
        }

        /// <summary>
        /// 写入byte 数据到port
        /// </summary>
        /// <param name="data"></param>
        public void Write(byte[] data)
        {
            if (_comPort.IsOpen == false)
                _comPort.Open();
            _comPort.Write(data, 0, data.Length);

            //wait until all data writes
            int timeout = time_out_count;
            while (_comPort.BytesToWrite > 0 && timeout > 0)
            {
                System.Threading.Thread.Sleep(40);
                timeout--;
            }
        }

        /// <summary>
        /// 从串口读取字符串
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            if (_comPort.IsOpen == false)
                _comPort.Open();
            string str = _comPort.ReadExisting();

            // the ReadExisting() method returns the contents of the stream and internal buffer of
            // the SerialPort object as a string. This method does not use a time-out.
            // Note that this method can leave trailing lead bytes in the internal buffer, 
            // which makes the BytesToRead value greater than zero.
            if (_comPort.BytesToRead > 0)
            {
                _comPort.DiscardInBuffer();
            }
            return str;
        }

        /// <summary>
        /// 读取串口的字节数据
        /// </summary>
        /// <returns></returns>
        public byte[] ReadByteArray()
        {
            if (_comPort.IsOpen == false)
                _comPort.Open();

            int bytes = _comPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            _comPort.Read(buffer, 0, bytes);
            return buffer;
        }

        /// <summary>
        /// read bytes from port
        /// </summary>
        /// <param name="countToRead"></param>
        /// <returns></returns>
        public byte[] ReadByteArray(int countToRead)
        {
            if (_comPort.IsOpen == false)
                _comPort.Open();

            int bytes = _comPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            _comPort.Read(buffer, 0, bytes);

            //prepare the return data
            if (bytes <= countToRead)
            {
                return buffer;
            }
            else
            {
                byte[] retBytes = new byte[countToRead];
                Array.Copy(buffer, retBytes, countToRead);
                return retBytes;
            }
        }

        /// <summary>
        /// 清理
        /// </summary>
        protected void OnDisposeManaged()
        {
            if (_comPort != null && _comPort.IsOpen == true)
                _comPort.Close();
            _comPort = null;
        }


        /// <summary>
        /// 丢弃当前串口的数据
        /// </summary>
        public void CleanInBuffer()
        {
            if (_comPort.IsOpen == false)
                return;
            _comPort.DiscardInBuffer();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void CloseSerialPort()
        {
            if (_comPort != null && _comPort.IsOpen == true)
                _comPort.Close();
        }
    }
}
