using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using IRIP_API_LIB;
using Newtonsoft.Json;

namespace CosTest
{
    class ThermCam
    {
        /// <summary>
        /// CONNECTION
        /// </summary>
        internal bool CONNECTED { get; private set; }

        /// <summary>  
        /// 配置文件的文件名
        /// </summary>
        const string ConfigFileName = @"Config\CONFIG.THERMCAM.json";

        /// <summary>
        /// Frame锁定标识
        /// </summary>
        private bool frame_lock = false;

        /// <summary>  
        /// API对象
        /// </summary>  
        private API iripApi;

        /// <summary>  
        /// API热成像相机对象
        /// </summary>  
        private API.IRCamera irCamera;

        /// <summary>  
        /// API红外图像对象
        /// </summary> 
        private API.IRImage irImage;

        /// <summary>  
        /// 搜索USB相机结构体
        /// </summary>  
        private CameraDevices camDevices;

        /// <summary>  
        /// USB相机结构体
        /// </summary> 
        private CameraDevice camDevice;

        /// <summary>  
        /// 当前运行的相机类型，-1：未运行， 1: USB热成像相机，2: HJK网络热成像相机
        /// </summary> 
        private int RunningCameraType;

        /// <summary>  
        /// 热成像相机数据帧结构体
        /// </summary> 
        private CallbackFrameData camFrame;

        /// <summary>  
        /// 热成像相机数据帧结构体Copy
        /// </summary> 
        private CallbackFrameData2 camFrameCopy;

        /// <summary>  
        /// 热成像相机数据帧结构体封装
        /// </summary> 
        private IntPtr camFramePtr;

        /// <summary>  
        /// 图像渲染配置结构体
        /// </summary> 
        private IRIP_Config irip_config;

        /// <summary>  
        /// 获取调色板结构体
        /// </summary> 
        private IR_PALETTES palettes;

        /// <summary>  
        /// 全局测量参数结构体
        /// </summary> 
        private MDParameters mdParameters;

        /// <summary>  
        /// 数据帧回调函数引用
        /// </summary> 
        private RetCallback frameProc;

        /// <summary>  
        /// 保存红外图像回调函数引用
        /// </summary> 
        private RetCallbackPlus saveIrImageCallback;

        /// <summary>  
        /// 注销实例异步回调函数
        /// </summary> 
        private RetCallback destroycallback;

        /// <summary>  
        /// 暂停状态标记
        /// </summary> 
        private bool isPause = false;

        /// <summary>  
        /// 第一次帧回调标记标记
        /// </summary> 
        private bool isFristOK = false;

        /// <summary>  
        /// 关闭状态标记
        /// </summary> 
        private bool isClose = true;

        /// <summary>  
        /// 打快门状态标记
        /// </summary> 
        private bool camHisto = false;

        /// <summary>  
        /// 动态调色板低位颜色表示温度
        /// </summary> 
        private float plteMinTemp;

        /// <summary>  
        /// 动态调色板高位颜色表示温度
        /// </summary> 
        private float plteMaxTemp;

        /// <summary>  
        /// JSON对象
        /// 内含关于热成像相机的各种配置信息
        /// 保存它作为热成像相机的配置文件
        /// </summary> 
        private JSON mJsonObj;

        /// <summary>  
        /// JSON内的温度测量信息对象
        /// </summary> 
        private JSON.cMeasurement mJsonObj_md;

        /// <summary>  
        /// 回调帧JSON.cMeasurement文本数据
        /// </summary> 
        private string frameJsonText = "";

        /// <summary>  
        /// 回调帧解析的JSON.cMeasurement测量对象
        /// </summary> 
        private JSON.cMeasurement mCamera_md;

        /// <summary>  
        /// 红外图像渲染Bitmap
        /// </summary> 
        private Bitmap imageIrBitmap;

        /// <summary>  
        /// 红外图像渲染Bitmap + 测量结果
        /// </summary> 
        private Bitmap imageIrInfoBitmap;

        /// <summary>  
        /// 拍照触发标记
        /// </summary> 
        private bool snapTrigger;

        /// <summary>
        /// 打开热相机
        /// </summary>
        internal void Open()
        {
            this.CONNECTED = false;
            //初始化API
            if (Init() == false)
                return;
            //读取配置文件
            if (ReadConfig(ConfigFileName) == false)
                return; ;
            //更新配置
            UpdateConfig();
            //搜索相相机
            if (camSearch() == 0)
                return;
            //打开相机
            if (camOpen() == false)
                return;
            //连接成功
            this.CONNECTED = true;
        }

        /// <summary>
        /// 关闭热相机
        /// </summary>
        internal void Close()
        {
            camClose();
        }

        /// <summary>
        /// 设置相机的运行状态(仅用于临时占用相机)
        /// </summary>
        /// <param name="enable"></param>
        internal void SetRunStat(bool enable)
        {
            if (enable)
                camRun();
            else
                camPause();
        }

        /// <summary>
        /// 读取成像数据
        /// </summary>
        internal void ReadFrame(out Bitmap irBmp, out Bitmap irInfoBmp, out List<StThmAreaMax> tempMaxData)
        {
            snapTrigger = false;
            imageIrBitmap = null;
            imageIrInfoBitmap = null;

            //打开相机
            camRun();
            //等待
            int cnt = 0;
            while (cnt++ < 50)
            {
                if(snapTrigger)
                    break;
                Thread.Sleep(50);
            }
            //暂停相机
            camPause();

            //返回数据
            if (snapTrigger)
            {
                irBmp = imageIrBitmap;
                irInfoBmp = imageIrInfoBitmap;
                tempMaxData = new List<StThmAreaMax>();
                //矩形区域测温
                JSON.cMeasurement jmd = mCamera_md;
                if (jmd.show.areaShow && jmd.area != null)
                {
                    for (int i = 0; i < jmd.area.Count; i++)
                    {
                        JSON.cMeasurement.AreaPlus plus = jmd.area[i];
                        tempMaxData.Add(new StThmAreaMax() { AreaName = plus.name, AreaTempMax = plus.md.max / 100.0 });
                    }
                }
            }
            else
            {
                irBmp = null;
                irInfoBmp = null;
                tempMaxData = new List<StThmAreaMax>();
            }
        }

        /// <summary>
        /// 读取成像数据
        /// </summary>
        internal Bitmap ReadFrame()
        {
            Bitmap imageIrBitmap;
            Bitmap irInfoBmp;
            List<StThmAreaMax> tempMaxData;
            ReadFrame(out imageIrBitmap, out irInfoBmp, out tempMaxData);
            return irInfoBmp;
        }

        /// <summary>
        /// 自动聚焦
        /// </summary>
        internal void AutoFocus()
        {
            //打开相机
            camRun();
            //自动聚焦
            camAutoFocus();
        }

        /// <summary>
        /// 读取原始成像数据
        /// </summary>
        internal Bitmap ReadRawFrame()
        {
            Bitmap imageIrBitmap;
            Bitmap irInfoBmp;
            List<StThmAreaMax> tempMaxData;
            ReadFrame(out imageIrBitmap, out irInfoBmp, out tempMaxData);
            return imageIrBitmap;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        private bool ReadConfig(string configFileName)
        {
            string configStr = "";
            try
            {
                try
                {
                    //读取配置文件
                    FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + configFileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    string str = sr.ReadLine();
                    while (str != null)
                    {
                        configStr += str;
                        str = sr.ReadLine();
                    }
                    sr.Close();
                    fs.Close();
                }
                catch (Exception e)
                {
                    ErrLog.WriteLine("配置文件读取失败\r\n" + e.ToString());
                    mJsonObj = null;
                }

                if (configStr.Length > 0)
                {
                    /// 从文件加载的创建JSON对象
                    mJsonObj = JsonConvert.DeserializeObject<JSON>(configStr);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                ErrLog.WriteLine("配置文件解析失败\r\n" + e.ToString());
                mJsonObj = null;
                return false;
            }
        }

        /// <summary>
        /// 初始化API
        /// </summary>
        private bool Init()
        {
            //新建API实例
            iripApi = new API();
            if (!iripApi.DLL())
            {
                ErrLog.WriteLine("热成像相机加载库文件失败！");
                return false;
            }
            else
            {
                int vercode = iripApi.vercode();

                //检查许可证状态，许可证字串在API类设置LICENCE_CODE或使用iripApi.LicenceCode(string code)函数加载
                //0: 验证通过, -1: 许可验证错误, -2: 许可证过期, -3: 许可证文件不存在
                if (iripApi.LicenceState() == 0)
                {
                    //创建实例后内建相机及图像处理对象才可以正常使用
                    iripApi.init(-1);
                }
                else if (iripApi.LicenceState() == -1)
                {
                    ErrLog.WriteLine("热成像相机许可证验证失败！");
                    return false;
                }
                else if (iripApi.LicenceState() == -2)
                {
                    ErrLog.WriteLine("热成像相机许可证已过期！");
                    return false;
                }
                else if (iripApi.LicenceState() == -3)
                {
                    ErrLog.WriteLine("热成像相机许可证文件不存在！");
                    return false;
                }
            }

            //获取热成像相机对象
            irCamera = iripApi.getIRCamera();
            irImage = iripApi.getIRImage();

            //新建相机列表结构体，用于搜索相机和启动相机
            camDevice = new CameraDevice();
            camDevices = new CameraDevices();

            //当前运行的相机类型
            RunningCameraType = -1;

            //新建热成像相机回调帧结构体及绑定IntPtr
            camFrame = new CallbackFrameData();
            camFrameCopy = new CallbackFrameData2();
            camFramePtr = Marshal.AllocHGlobal(Marshal.SizeOf(camFrame));
            Marshal.StructureToPtr(camFrame, camFramePtr, true);

            //引用帧回调函数，防止被系统回收
            frameProc = new RetCallback(FrameProc);
            //RetCallback callbackDelegate = FrameProc;
            //IntPtr frameProc1 = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

            //保存红外图像回调函数
            saveIrImageCallback = new RetCallbackPlus(SaveIrImageCallback);

            //注销实例异步回调函数
            destroycallback = new RetCallback(DestroyCallback);

            return true;
        }

        private void UpdateConfig()
        {
            //如果文件内容加载失败
            if (mJsonObj == null)
            {
                /// 创建JSON对象
                mJsonObj = new JSON();
            }

            //引用mJsonObj_md对象
            mJsonObj_md = mJsonObj.Measurement;

            //图像渲染配置结构体，由于结构体默认参数为0，必须获取默认配置信息活Copy Json对象内的参数
            irip_config = new IRIP_Config();
            //获取默认配置信息
            //Copy参数
            IRIP_API.CopyIripConfig(mJsonObj.IripConfig, ref irip_config);
            //设置配置信息
            irCamera.SetIRIPConfig(ref irip_config);

            //如许修改配置，修改相应选项后使用SetIRIPConfig函数设置，详细说明参考结构体注释
            //irip_config.DDEType = true;
            //irCamera.SetIRIPConfig(ref irip_config);

            //新建调色板列表结构体并获取调色板
            int max = iripApi.getPalette().getIRPalettesNUM();
            if (max > 0)
            {
                //如果记录值超出范围
                int selIndex = mJsonObj.Show.pIndex >= max ? max - 1 : mJsonObj.Show.pIndex;
                //设置热成像相机的调色板
                irCamera.SelectPalette((uint)selIndex);
            }

            //设置自动打快门校正时间
            if (mJsonObj.Camera.refreshIndex >= IRIP_API.AUTO_REFRESH_TIME.Length)
            {
                mJsonObj.Camera.refreshIndex = IRIP_API.AUTO_REFRESH_TIME.Length - 1;
            }
            //setAutoRefresh(); //此项需要启动相机后设置有效

            //设置温度量程
            if (mJsonObj.Camera.tempType < 0 || mJsonObj.Camera.tempType >= IRIP_API.TEMPTYPE.TEMPTYPE_NAME.Length)
            {
                mJsonObj.Camera.tempType = IRIP_API.TEMPTYPE.TEMPTYPE_NAME.Length - 1;
            }


            //设置旋转角度
            int rotateIndex = -1;
            for (int i = 0; i < IRIP_API.ROTATE.ROTATE_ANGLE.Length; i++)
            {
                int angle = IRIP_API.ROTATE.ROTATE_ANGLE[i];
                if (mJsonObj.Show.rotate == angle)
                {
                    rotateIndex = i;
                }
            }
            if (rotateIndex == -1)
            {
                rotateIndex = 0;
                mJsonObj.Show.rotate = IRIP_API.ROTATE.ROTATE_ANGLE[rotateIndex];
            }
            //设置相机图像的旋转角度和镜像
            setAdjustDirection();

            //将发射率和反射温度等转换单位
            float reflected = 0;
            float highTemp = 0;
            float compensation = 0;
            iripApi.ConversionUnit(ref reflected, mJsonObj_md.reflected, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);
            iripApi.ConversionUnit(ref highTemp, mJsonObj.Alert.highTemp / 100.0f, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);
            iripApi.ConversionUnitRelative(ref compensation, mJsonObj_md.compensation / 100.0f, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);
            //新建全局测量参数结构体
            mdParameters = new MDParameters((uint)mJsonObj_md.unit, mJsonObj_md.compensation / 100.0f, mJsonObj_md.reflected, mJsonObj_md.distance, mJsonObj_md.winTmte, mJsonObj_md.winTemp);
            //设置全局测量参数结构体
            irCamera.SetGlobalMDParameters(ref mdParameters);

            //设置测量模式
            mJsonObj_md.show.hiloShow = true; //高低温测量
            mJsonObj_md.show.spotShow = false; //点测温（如未添加元素不会返回相关信息）
            mJsonObj_md.show.lineShow = false; //线测温（如未添加元素不会返回相关信息）
            mJsonObj_md.show.areaShow = true; //矩形区域测温（如未添加元素不会返回相关信息）
            setMeasurementMode();

            //设置相机的测温信息, setMeasurementMode()等控件设置响应函数已包含
            //camSetMeasuremen();
        }

        /// <summary>
        /// 更新测试区域
        /// </summary>
        /// <param name="ThermAreaParamList"></param>
        public void UpdateMeasAera(List<StThermAreaParam> ThermAreaParamList)
        {
            if (mJsonObj_md == null)
                return;

            mJsonObj_md.area.Clear();
            foreach(StThermAreaParam thmArea in ThermAreaParamList)
            {
                mJsonObj_md.addArea(thmArea.SX, thmArea.SY, thmArea.EX, thmArea.EY, thmArea.Name, false, (float)thmArea.Emissivity);
            }

            camSetMeasuremen();
        }

        /// <summary>  
        /// 显示调色板  
        /// </summary> 
        private void ShowPalette(object state)
        {
            CallbackFrameData2 frame = camFrameCopy;
            //显示动态调色板
            if (frame.dPLT != null && frame.dPLT_Len == 256)
            {
                //int index = comboBox_plteList.SelectedIndex;
                //index = index < palettes.num ? index : palettes.num - 1;
                //Bitmap image = To8bitBitmap(frame.dPLT, (int)frame.dPLT_Len, 1, palettes.palettes[index].palette);
                //for (int i = 0; i < pictureBox_plteIR.Height; i++)
                //{
                //    gPlteIR.DrawImage(image, 0, i, pictureBox_plteIR.Width, 1);
                //}
            }

            //显示动态高低位对应温度
            if (frame.dTHR != null && frame.dTHR_Len == 4)
            {
                iripApi.EnRefEmissivityTransmittanceUnit(ref plteMinTemp, frame.dTHR[0] / 100.0f, mJsonObj_md.unit, mJsonObj_md.compensation / 100.0f, mJsonObj_md.emissivity, mJsonObj_md.reflected, mJsonObj_md.distance);
                iripApi.EnRefEmissivityTransmittanceUnit(ref plteMaxTemp, frame.dTHR[1] / 100.0f, mJsonObj_md.unit, mJsonObj_md.compensation / 100.0f, mJsonObj_md.emissivity, mJsonObj_md.reflected, mJsonObj_md.distance);
            }
        }


        /// <summary>  
        /// 设置旋转角度、左右和上下镜像，传递给热成像相机对象
        /// </summary> 
        private void setAdjustDirection()
        {
            if (RunningCameraType == 1)
            {
                irCamera.SetAdjustDirection(mJsonObj.Show.rotate, mJsonObj.Show.mirrorHorizontal, mJsonObj.Show.mirrorVertically);
            }
        }

        /// <summary>  
        /// 设置自动打快门，传递给热成像相机对象
        /// </summary> 
        private void setAutoRefresh()
        {
            int time = IRIP_API.AUTO_REFRESH_TIME[mJsonObj.Camera.refreshIndex];

            if (!isPause)
            {
                if (RunningCameraType == 1)
                {
                    irCamera.SetAutoRefreshTime((uint)time);
                }
            }

        }

        /// <summary>  
        /// 设置自动对焦模式，仅支持 IRCamera 和 NetIRCamera热成像相机
        /// </summary> 
        private void setFocusMode(int mode)
        {
            if (RunningCameraType == 1)
            {
                irCamera.SetFocusMode(mode);
            }
        }

        /// <summary>  
        /// 设置测温量程
        /// </summary> 
        private void setTempType()
        {
            if (RunningCameraType == 1)
            {
                irCamera.SetTempType((uint)mJsonObj.Camera.tempType);
            }
        }

        /// <summary>  
        /// 设置测量模式，传递给热成像相机对象
        /// </summary> 
        private void setMeasurementMode()
        {
            camSetMeasuremen();
        }

        /// <summary>  
        /// 测量元素数据传递给热成像相机对象
        /// </summary> 
        private void camSetMeasuremen()
        {
            if (mJsonObj != null)
            {
                string resultJson = JsonConvert.SerializeObject(mJsonObj_md);
                if (RunningCameraType == 1)
                {
                    irCamera.SetMeasurement("JSON", resultJson, null, IntPtr.Zero);
                }
            }
        }


        /// <summary>  
        /// 搜索USB热成像相机
        /// </summary> 
        private int camSearch()
        {
            //搜索已连接的USB热成像相机
            int ret = irCamera.SearchCameraDevices(ref camDevices);
            if (ret == 0)
            {
                return camDevices.devNum;
            }
            else
            {
                return 0;
            }    
        }

        /// <summary>  
        /// 打开相机
        /// </summary> 
        private bool camOpen()
        {
            if (camDevices.devNum == 0)
                return false;

            //先关闭cam
            camClose();
            //复位启动类型
            RunningCameraType = 1;

            //保存启动的相机结构体数据
            camDevice = camDevices.devs[0];

            int ret = -1;
            ushort width = 640;
            ushort height = 516;

            ret = irCamera.Start(ref camDevice);
            width = camDevice.width;
            height = camDevice.height;

            if (ret != 0)
            {
                ErrLog.WriteLine("相机连接失败 ret: " + ret);
                return false;
            }

            if (width > 0 && height > 0)
            {
                isClose = false;

                if (mJsonObj.imgW != width || mJsonObj.imgH != height)
                {
                    mJsonObj.imgW = width;
                    mJsonObj.imgH = height;
                    mJsonObj.resolution = width.ToString() + " x " + height.ToString();
                }

                //回调选项，根据需要回调，TEMP必选，其他可选，如果未包含调色板IRIP_API.OPTION.PLTE，保存红外图像时将使用irImage设置的调色板
                //irImage设置的调色板使用irImage.SelectPalette()或irImage.SetPalette()接口
                uint ops = IRIP_API.OPTION.PLTE | IRIP_API.OPTION.TEMP | IRIP_API.OPTION.TEMP2 | IRIP_API.OPTION.JSON | IRIP_API.OPTION.IMGA | IRIP_API.OPTION.IDAT | IRIP_API.OPTION.DPLT | IRIP_API.OPTION.DTHR;

                //IMGA图像数据输出类型 
                uint type = IRIP_API.IR.BMP;

                //IMGA图像输出宽度和高度，可区别于实际相机的输出图像尺寸，这是未旋转的尺寸，如果已旋转，在帧数据以rotated标识
                ushort imgWidth = width;
                ushort imgHeight = height;
                //申请回调数据帧内存
                if (iripApi.irFrameMemoryAllocation(camFramePtr, width, height, imgWidth, imgHeight, type, ops) == 0)
                {
                    //标记启动类型
                    RunningCameraType = 1;
                    //设置回调函数和数据帧
                    ret = irCamera.SetFrameCallback(camFramePtr, frameProc, IntPtr.Zero);
                    if(ret != 0)
                    {
                        ErrLog.WriteLine("相机启动失败，ret: " + ret);
                        return false;
                    }
                    //启动图像运行(如果不启动运行的话,首次抓不到图像)
                    if (!camRun())
                        return false;
                    Thread.Sleep(500);
                    //传递测量元素
                    camSetMeasuremen();
                    //设置自动打快门时间
                    setAutoRefresh();
                    //设置自动对焦模式
                    //0: 图像梯度;   1: 最高温;   2: 最低温
                    setFocusMode(0);
                    //启动图像运行
                    camPause();
                }
            }
            else
            {
                ErrLog.WriteLine("相机启动失败，ret: " + ret);
                return false;
            }

            return true;
        }

        /// <summary>  
        /// 打开相机
        /// </summary> 
        private bool camRun()
        {
            if (RunningCameraType == 1)
            {
                // 继续运行
                if (isPause)
                {
                    int ret = irCamera.Run();
                    if (ret == 0)
                    {
                        isPause = false;
                        return true;
                    }
                    else
                    {
                        ErrLog.WriteLine("相机启动失败，ret: " + ret);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>  
        /// 暂停相机输出
        /// </summary> 
        private void camPause()
        {
            if (!isPause)
            {
                //暂停
                if (RunningCameraType == 1)
                {
                    irCamera.Stop();
                }

                isPause = true;
            }
        }

        /// <summary>  
        /// 关闭相机
        /// </summary> 
        private void camClose()
        {
            if (RunningCameraType == 1)
            {
                int ret = irCamera.SetFrameCallback(IntPtr.Zero, null, IntPtr.Zero);
                //关闭
                ret = irCamera.Close();
                isClose = true;
                RunningCameraType = -1;

                return;
            }
        }

        /// <summary>  
        /// 打开门校正
        /// </summary> 
        private void camRefresh()
        {
            if (RunningCameraType == 1)
            {
                irCamera.Refresh();
            }
        }

        /// <summary>  
        /// 打开门校正
        /// </summary> 
        private void camAutoFocus()
        {
            if (RunningCameraType == 1)
            {
                CInt32Rect rect = new CInt32Rect();
                irCamera.AutoFocus(ref rect);
            }
        }

        /// <summary>  
        /// 数据帧回调函数  
        /// <para>@param ret 状态码</para>  
        /// <para>@param pUser</para>  
        /// </summary> 
        private void FrameProc(int ret, IntPtr pUser)
        {
            //如果值为-4可判断为相机断开
            if (ret == -4)
            {
                ErrLog.WriteLine("相机已断开连接");
                return;
            }

            if (!isFristOK)
            {
                isFristOK = true;
            }

            if (ret != 0)
            {
                ErrLog.WriteLine("错误: " + ret.ToString());
                return;
            }


            if (frame_lock)
            {
                return;
            }
            frame_lock = true;

            IRIP_API.CopyFrameData(camFramePtr, ref camFrameCopy);

            //测试在新线程中处理容易出问题且影响性能，暂时不开启新线程
            //Thread thread = new Thread(() =>
            //{

            CallbackFrameData2 frame = camFrameCopy;

            int length = frame.width * frame.height;
            if (length <= 0)
            {
                frame_lock = false;
                return;
            }

            if (frame.jSON_Len > 2)
            {
                try
                {
                    //拷贝测量json文本
                    //frameJsonText = System.Text.Encoding.UTF8.GetString(frame.jSON);
                    frameJsonText = BytesToString(frame.jSON);
                    //解析测量数据
                    mCamera_md = JsonConvert.DeserializeObject<JSON.cMeasurement>(frameJsonText);
                }
                catch
                {
                    ErrLog.WriteLine("解析JSON出现错误");
                }
            }
            else
            {
                mCamera_md = null;
            }

            //图像数据
            if (frame.IMGA_Len > 0)
            {
                //创建Bitmap
                imageIrBitmap = BytesToBitmap(frame.IMGA);
                //绘制测量信息
                imageIrInfoBitmap = DrawMeasurementInfo(imageIrBitmap);
                //图像抓取标志位
                snapTrigger = true;
            }

            frame_lock = false;

            //});
            //thread.Start();

        }

        private void SaveIrImageCallback(int ret, UInt64 size, IntPtr pUser)
        {
            //ErrLog.WriteLine("保存红外图像 ret: " + ret.ToString() + ", size: " + size.ToString());
            if (ret == 0)
            {
                // 保存成功
            }
        }

        public void DestroyCallback(int id, IntPtr pUser)
        {
            if (id >= 0)
            {
                Console.WriteLine("已注销实例");

                //Thread.Sleep(2000);

                //iripApi.irFrameMemoryRelease(camFramePtr);

                //SyncContext.Post(CloseWindow, 0);
            }
        }

        /// <summary>  
        /// 字节数组转文本
        /// <para>@param Bytes 字节数组</para>  
        /// <para>@return string</para>  
        /// </summary>  
        public static string BytesToString(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0)
                {
                    break;
                }
                strBuilder.Append(Convert.ToChar(bytes[i]));
            }
            return strBuilder.ToString();
        }

        /// <summary>  
        /// 封装Bitmap对象
        /// <para>@param Bytes 图像字节数组</para>  
        /// <para>@return Bitmap对象</para>  
        /// </summary>  
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            if (Bytes == null)
            {
                return null;
            }
            MemoryStream stream = null;
            Bitmap bitmap = null;
            try
            {
                stream = new MemoryStream(Bytes);
                bitmap = new Bitmap(stream);
            }
            //catch (ArgumentNullException ex)
            //{
            //    throw ex;
            //}
            //catch (ArgumentException ex)
            //{
            //    throw ex;
            //}
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return new Bitmap(bitmap);
        }

        /// <summary>  
        /// 8位字节数组转换为8bit位图  
        /// <para>@param rawValues 显示字节数组</para>  
        /// <para>@param width 图像宽度</para>  
        /// <para>@param height 图像高度</para>  
        /// <para>@param plte 32位调色板</para>  
        /// <para>@return Bitmap位图</para>  
        /// </summary>  
        public static Bitmap To8bitBitmap(byte[] grayValues, int width, int height, uint[] plte)
        {
            //申请目标位图的变量，并将其内存区域锁定  
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            //获取图像参数  
            int stride = bmpData.Stride;  // 扫描线的宽度  
            int offset = stride - width;  // 显示宽度与扫描线宽度的间隙  
            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置  
            int scanBytes = stride * height;// 用stride宽度，表示这是内存区域的大小  

            //下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            int posScan = 0, posReal = 0;// 分别设置两个位置指针，指向源数组和目标数组  
            byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存  

            for (int x = 0; x < height; x++)
            {
                //下面的循环节是模拟行扫描  
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = grayValues[posReal++];
                }
                posScan += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”  
            }

            //用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域  

            //下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度  
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb((int)plte[i]);// Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            return bmp;
        }

        /// <summary>  
        /// 绘制测量元素到画布
        /// </summary>  
        private Bitmap DrawMeasurementInfo(Bitmap irbmp)
        {
            Bitmap irmeasbmp = new Bitmap(irbmp);

            if (mCamera_md != null)
            {
                JSON.cMeasurement jmd = mCamera_md;

                using (Graphics graphics = Graphics.FromImage(irmeasbmp))
                {
                    //创建画笔
                    Pen pen = new Pen(Color.White);
                    Font font = new Font("微软亚黑", 8, FontStyle.Regular);
                    int fontHeight = (int)font.GetHeight();
                    SolidBrush brush = new SolidBrush(Color.White);

                    #region other
                    ////绝对全局高低温点，视频模式下获取该帧的绝对最高最低温度和坐标，由于没有使用粘滞功能，坐标跳动比较厉害，仅供参考用
                    //if (mCamera_md.minx >= 0)
                    //{
                    //    int x, y;
                    //    Point[] points = new Point[3];

                    //    //绘制低温点三角坐标指示符
                    //    x = mCamera_md.minx;
                    //    y = mCamera_md.miny;

                    //    points[0] = new Point(x, y);
                    //    points[1] = new Point(x + 4, y - 4);
                    //    points[2] = new Point(x - 4, y - 4);
                    //    pen.Color = Color.Blue;
                    //    pen.Width = 1;
                    //    graphics.DrawPolygon(pen, points);

                    //    //绘制高温点三角坐标指示符
                    //    x = mCamera_md.maxx;
                    //    y = mCamera_md.maxy;

                    //    points[0] = new Point(x, y);
                    //    points[1] = new Point(x + 4, y + 4);
                    //    points[2] = new Point(x - 4, y + 4);
                    //    pen.Color = Color.Red;
                    //    pen.Width = 1;
                    //    graphics.DrawPolygon(pen, points);
                    //}

                    ////高低温测量
                    //if (jmd.show.hiloShow && jmd.hilo != null)
                    //{
                    //    pen.Color = Color.White;
                    //    pen.Width = 3;

                    //    int x, y, x2, y2;

                    //    //绘制低温点十字坐标
                    //    int minx = jmd.hilo.minx;
                    //    int miny = jmd.hilo.miny;

                    //    x = minx - 7;
                    //    y = miny;
                    //    x2 = minx + 8;
                    //    y2 = miny;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    x = minx;
                    //    y = miny - 7;
                    //    x2 = minx;
                    //    y2 = miny + 8;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    pen.Color = Color.Blue;
                    //    pen.Width = 1;

                    //    x = minx - 6;
                    //    y = miny;
                    //    x2 = minx + 6;
                    //    y2 = miny;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    x = minx;
                    //    y = miny - 6;
                    //    x2 = minx;
                    //    y2 = miny + 6;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    //绘制高温点十字坐标
                    //    int maxx = jmd.hilo.maxx;
                    //    int maxy = jmd.hilo.maxy;

                    //    pen.Color = Color.White;
                    //    pen.Width = 3;

                    //    x = maxx - 7;
                    //    y = maxy;
                    //    x2 = maxx + 8;
                    //    y2 = maxy;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    x = maxx;
                    //    y = maxy - 7;
                    //    x2 = maxx;
                    //    y2 = maxy + 8;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    pen.Color = Color.Red;
                    //    pen.Width = 1;

                    //    x = maxx - 6;
                    //    y = maxy;
                    //    x2 = maxx + 6;
                    //    y2 = maxy;
                    //    graphics.DrawLine(pen, x, y, x2, y2);

                    //    x = maxx;
                    //    y = maxy - 6;
                    //    x2 = maxx;
                    //    y2 = maxy + 6;
                    //    graphics.DrawLine(pen, x, y, x2, y2);
                    //}

                    ////点测温
                    //if (jmd.show.spotShow && jmd.spot != null)
                    //{
                    //    var stringFormat = new StringFormat();
                    //    stringFormat.Alignment = StringAlignment.Center;

                    //    for (int i = 0; i < jmd.spot.Count; i++)
                    //    {
                    //        JSON.cMeasurement.SpotPlus plus = jmd.spot[i];
                    //        string name = plus.getName();

                    //        pen.Color = Color.Black;
                    //        pen.Width = 3;

                    //        int x, y, x2, y2;

                    //        //绘制十字坐标
                    //        int sx = plus.sx;
                    //        int sy = plus.sy;

                    //        x = sx - 8;
                    //        y = sy;
                    //        x2 = sx + 8;
                    //        y2 = sy;
                    //        graphics.DrawLine(pen, x, y, x2, y2);

                    //        x = sx;
                    //        y = sy - 8;
                    //        x2 = sx;
                    //        y2 = sy + 8;
                    //        graphics.DrawLine(pen, x, y, x2, y2);

                    //        pen.Color = Color.White;
                    //        pen.Width = 1;

                    //        x = sx - 7;
                    //        y = sy;
                    //        x2 = sx + 7;
                    //        y2 = sy;
                    //        graphics.DrawLine(pen, x, y, x2, y2);

                    //        x = sx;
                    //        y = sy - 7;
                    //        x2 = sx;
                    //        y2 = sy + 7;
                    //        graphics.DrawLine(pen, x, y, x2, y2);

                    //        //绘制名称
                    //        sy += 8;
                    //        brush.Color = Color.Black;
                    //        graphics.DrawString(name, font, brush, sx - 1, sy, stringFormat);
                    //        graphics.DrawString(name, font, brush, sx + 1, sy, stringFormat);
                    //        graphics.DrawString(name, font, brush, sx, sy + 1, stringFormat);
                    //        graphics.DrawString(name, font, brush, sx, sy - 1, stringFormat);
                    //        brush.Color = Color.White;
                    //        graphics.DrawString(name, font, brush, sx, sy, stringFormat);

                    //    }
                    //}

                    ////线测温
                    //if (jmd.show.lineShow && jmd.line != null)
                    //{
                    //    for (int i = 0; i < jmd.line.Count; i++)
                    //    {
                    //        JSON.cMeasurement.LinePlus plus = jmd.line[i];
                    //        if (plus.md != null)
                    //        {
                    //            string name = plus.getName();

                    //            pen.Color = Color.White;
                    //            pen.Width = 1;

                    //            int sx, sy, ex, ey;
                    //            sx = plus.sx;
                    //            sy = plus.sy;
                    //            ex = plus.ex;
                    //            ey = plus.ey;

                    //            //绘制线
                    //            graphics.DrawLine(pen, sx, sy, ex, ey);

                    //            //绘制名称
                    //            sy -= fontHeight + 1;
                    //            brush.Color = Color.Black;
                    //            graphics.DrawString(name, font, brush, sx - 1, sy);
                    //            graphics.DrawString(name, font, brush, sx + 1, sy);
                    //            graphics.DrawString(name, font, brush, sx, sy + 1);
                    //            graphics.DrawString(name, font, brush, sx, sy - 1);
                    //            brush.Color = Color.White;
                    //            graphics.DrawString(name, font, brush, sx, sy);

                    //            //绘制测量点
                    //            if (plus.md != null)
                    //            {
                    //                pen.Color = Color.White;
                    //                pen.Width = 3;

                    //                int x, y, x2, y2;

                    //                //绘制低温点十字坐标
                    //                int minx = plus.md.minx;
                    //                int miny = plus.md.miny;

                    //                x = minx - 5;
                    //                y = miny;
                    //                x2 = minx + 6;
                    //                y2 = miny;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                x = minx;
                    //                y = miny - 5;
                    //                x2 = minx;
                    //                y2 = miny + 6;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                pen.Color = Color.Blue;
                    //                pen.Width = 1;

                    //                x = minx - 4;
                    //                y = miny;
                    //                x2 = minx + 4;
                    //                y2 = miny;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                x = minx;
                    //                y = miny - 4;
                    //                x2 = minx;
                    //                y2 = miny + 4;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                //绘制低高点十字坐标
                    //                int maxx = plus.md.maxx;
                    //                int maxy = plus.md.maxy;

                    //                pen.Color = Color.White;
                    //                pen.Width = 3;

                    //                x = maxx - 5;
                    //                y = maxy;
                    //                x2 = maxx + 6;
                    //                y2 = maxy;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                x = maxx;
                    //                y = maxy - 5;
                    //                x2 = maxx;
                    //                y2 = maxy + 6;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                pen.Color = Color.Red;
                    //                pen.Width = 1;

                    //                x = maxx - 4;
                    //                y = maxy;
                    //                x2 = maxx + 4;
                    //                y2 = maxy;
                    //                graphics.DrawLine(pen, x, y, x2, y2);

                    //                x = maxx;
                    //                y = maxy - 4;
                    //                x2 = maxx;
                    //                y2 = maxy + 4;
                    //                graphics.DrawLine(pen, x, y, x2, y2);
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    //矩形区域测温
                    if (jmd.show.areaShow && jmd.area != null)
                    {
                        for (int i = 0; i < jmd.area.Count; i++)
                        {
                            JSON.cMeasurement.AreaPlus plus = jmd.area[i];
                            if (plus.md != null)
                            {
                                string name = plus.getName();

                                pen.Color = Color.White;
                                pen.Width = 1;

                                int sx, sy, ex, ey;
                                sx = plus.sx;
                                sy = plus.sy;
                                ex = plus.ex;
                                ey = plus.ey;

                                //绘制矩形
                                graphics.DrawRectangle(pen, Math.Min(sx, ex), Math.Min(sy, ey), Math.Abs(ex - sx), Math.Abs(ey - sy));

                                //绘制名称
                                sy -= fontHeight + 1;
                                //brush.Color = Color.Black;
                                //graphics.DrawString(name, font, brush, sx - 1, sy);
                                //graphics.DrawString(name, font, brush, sx + 1, sy);
                                //graphics.DrawString(name, font, brush, sx, sy + 1);
                                //graphics.DrawString(name, font, brush, sx, sy - 1);
                                //brush.Color = Color.White;
                                //graphics.DrawString(name, font, brush, sx, sy);
                                brush.Color = Color.Black;
                                graphics.DrawString(name + " : " + plus.md.max / 100.0 + "°C", font, brush, sx, sy);

                                //绘制测量点
                                if (plus.md != null)
                                {
                                    pen.Color = Color.White;
                                    pen.Width = 3;

                                    int x, y, x2, y2;

                                    ////绘制低温点十字坐标
                                    //int minx = plus.md.minx;
                                    //int miny = plus.md.miny;

                                    //x = minx - 5;
                                    //y = miny;
                                    //x2 = minx + 6;
                                    //y2 = miny;
                                    //graphics.DrawLine(pen, x, y, x2, y2);

                                    //x = minx;
                                    //y = miny - 5;
                                    //x2 = minx;
                                    //y2 = miny + 6;
                                    //graphics.DrawLine(pen, x, y, x2, y2);

                                    //pen.Color = Color.Blue;
                                    //pen.Width = 1;

                                    //x = minx - 4;
                                    //y = miny;
                                    //x2 = minx + 4;
                                    //y2 = miny;
                                    //graphics.DrawLine(pen, x, y, x2, y2);

                                    //x = minx;
                                    //y = miny - 4;
                                    //x2 = minx;
                                    //y2 = miny + 4;
                                    //graphics.DrawLine(pen, x, y, x2, y2);

                                    //绘制高温点十字坐标
                                    int maxx = plus.md.maxx;
                                    int maxy = plus.md.maxy;

                                    pen.Color = Color.White;
                                    pen.Width = 3;

                                    x = maxx - 5;
                                    y = maxy;
                                    x2 = maxx + 5;
                                    y2 = maxy;
                                    graphics.DrawLine(pen, x, y, x2, y2);

                                    x = maxx;
                                    y = maxy - 5;
                                    x2 = maxx;
                                    y2 = maxy + 6;
                                    graphics.DrawLine(pen, x, y, x2, y2);

                                    pen.Color = Color.Red;
                                    pen.Width = 1;

                                    x = maxx - 4;
                                    y = maxy;
                                    x2 = maxx + 6;
                                    y2 = maxy;
                                    graphics.DrawLine(pen, x, y, x2, y2);

                                    x = maxx;
                                    y = maxy - 4;
                                    x2 = maxx;
                                    y2 = maxy + 4;
                                    graphics.DrawLine(pen, x, y, x2, y2);
                                }
                            }
                        }
                    }
                }
            }

            return irmeasbmp;
        }

        #region not used
        /// <summary>  
        /// 设置调色板，传递给热成像相机对象
        /// </summary> 
        private void setPalette()
        {
            int index = 0; // int index = comboBox_plteList.SelectedIndex;
            if (mJsonObj.Show.pIndex != index && index < palettes.num)
            {
                mJsonObj.Show.pIndex = index;
                mJsonObj.Show.pName = System.Text.Encoding.UTF8.GetString(palettes.palettes[index].name_cn).TrimEnd('\0');

                if (RunningCameraType == 1)
                {
                    irCamera.SelectPalette((uint)mJsonObj.Show.pIndex);
                }


                if (isClose || isPause)
                {
                    ShowPalette(null);
                }
            }

        }

        /// <summary>  
        /// 设置温度单位，传递给热成像相机对象
        /// </summary> 
        private void setUnit()
        {

            float reflected = 0;
            float highTemp = 0;
            float compensation = 0;
            iripApi.ConversionUnit(ref reflected, mJsonObj_md.reflected, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);
            iripApi.ConversionUnitRelative(ref compensation, mJsonObj_md.compensation / 100.0f, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);
            iripApi.ConversionUnit(ref highTemp, mJsonObj.Alert.highTemp / 100.0f, IRIP_API.UNIT.CENTIGRADE, mJsonObj_md.unit);

            /*
            //设置irCamera.SetMeasurement测量模板时会更新为测量模板的全局测量参数，故在这里无需单独设置
            mdParameters.unit = mJsonObj_md.unit;
            //设置全局测量参数结构体
            if (RunningCameraType == 1)
            {
                irCamera.SetGlobalMDParameters(ref mdParameters);
            }
            //*/

            camSetMeasuremen();
        }

        /// <summary>  
        /// 设置温度补偿，传递给热成像相机对象
        /// </summary> 
        private void setCompensation()
        {
            /*
            //设置irCamera.SetMeasurement测量模板时会更新为测量模板的全局测量参数，故在这里无需单独设置
            mdParameters.compensation = mJsonObj_md.compensation / 100.0f;
            mdParameters.distance = mJsonObj_md.distance;
            //设置全局测量参数结构体
            if (RunningCameraType == 1)
            {
                irCamera.SetGlobalMDParameters(ref mdParameters);
            }
            //*/

            camSetMeasuremen();
        }

        /// <summary>  
        /// 设置反射率和反射温度，传递给热成像相机对象
        /// </summary> 
        private void setEmissivityAndReflected()
        {

            /*
            //设置irCamera.SetMeasurement测量模板时会更新为测量模板的全局测量参数，故在这里无需单独设置
            mdParameters.emissivity = mJsonObj_md.emissivity;
            mdParameters.reflected = mJsonObj_md.reflected;
            //设置全局测量参数结构体
            if (RunningCameraType == 1)
            {
                irCamera.SetGlobalMDParameters(ref mdParameters);
            }
            //*/


            camSetMeasuremen();
        }
        #endregion
    }
}
