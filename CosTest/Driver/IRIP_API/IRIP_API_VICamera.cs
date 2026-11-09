using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>可见光相机</br>
    /// </summary>
    public class IRIP_API_VICamera
    {
        private IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
            api_searchUsbCameraDevices = _IRIP_API_searchUsbCameraDevices;

        api_Start = _IRIP_API_viCamera_Start;
        api_Run = _IRIP_API_viCamera_Run;
        api_Stop = _IRIP_API_viCamera_Stop;
        api_Close = _IRIP_API_viCamera_Close;
        api_GetDeviceInfo = _IRIP_API_viCamera_GetDeviceInfo;
        api_GetDropFrameNum = _IRIP_API_viCamera_GetDropFrameNum;
        api_SetDropFrameNum = _IRIP_API_viCamera_SetDropFrameNum;
        api_GetOutMaxFps = _IRIP_API_viCamera_GetOutMaxFps;
        api_SetOutMaxFps = _IRIP_API_viCamera_SetOutMaxFps;
        api_GetReRunNumMax = _IRIP_API_viCamera_GetReRunNumMax;
        api_SetReRunNumMax = _IRIP_API_viCamera_SetReRunNumMax;
        api_SetFrameCallback = _IRIP_API_viCamera_SetFrameCallback;
        api_SetAdjustDirection = _IRIP_API_viCamera_SetAdjustDirection;
        api_GetVideoProcAmpConfigs = _IRIP_API_viCamera_GetVideoProcAmpConfigs;
        api_GetVideoProcAmpConfig = _IRIP_API_viCamera_GetVideoProcAmpConfig;
        api_SetVideoProcAmpConfigs = _IRIP_API_viCamera_SetVideoProcAmpConfigs;
        api_SetVideoProcAmpConfig = _IRIP_API_viCamera_SetVideoProcAmpConfig;
        api_GetCameraControlConfigs = _IRIP_API_viCamera_GetCameraControlConfigs;
        api_GetCameraControlConfig = _IRIP_API_viCamera_GetCameraControlConfig;
        api_SetCameraControlConfigs = _IRIP_API_viCamera_SetCameraControlConfigs;
        api_SetCameraControlConfig = _IRIP_API_viCamera_SetCameraControlConfig;
#else
            api_searchUsbCameraDevices = (IRIP_API_searchUsbCameraDevices)dll.Invoke("IRIP_API_searchUsbCameraDevices", typeof(IRIP_API_searchUsbCameraDevices));

            api_Start = (IRIP_API_viCamera_Start)dll.Invoke("IRIP_API_viCamera_Start", typeof(IRIP_API_viCamera_Start));
            api_Run = (IRIP_API_viCamera_Run)dll.Invoke("IRIP_API_viCamera_Run", typeof(IRIP_API_viCamera_Run));
            api_Stop = (IRIP_API_viCamera_Stop)dll.Invoke("IRIP_API_viCamera_Stop", typeof(IRIP_API_viCamera_Stop));
            api_Close = (IRIP_API_viCamera_Close)dll.Invoke("IRIP_API_viCamera_Close", typeof(IRIP_API_viCamera_Close));
            api_GetDeviceInfo = (IRIP_API_viCamera_GetDeviceInfo)dll.Invoke("IRIP_API_viCamera_GetDeviceInfo", typeof(IRIP_API_viCamera_GetDeviceInfo));
            api_GetDropFrameNum = (IRIP_API_viCamera_GetDropFrameNum)dll.Invoke("IRIP_API_viCamera_GetDropFrameNum", typeof(IRIP_API_viCamera_GetDropFrameNum));
            api_SetDropFrameNum = (IRIP_API_viCamera_SetDropFrameNum)dll.Invoke("IRIP_API_viCamera_SetDropFrameNum", typeof(IRIP_API_viCamera_SetDropFrameNum));
            api_GetOutMaxFps = (IRIP_API_viCamera_GetOutMaxFps)dll.Invoke("IRIP_API_viCamera_GetOutMaxFps", typeof(IRIP_API_viCamera_GetOutMaxFps));
            api_SetOutMaxFps = (IRIP_API_viCamera_SetOutMaxFps)dll.Invoke("IRIP_API_viCamera_SetOutMaxFps", typeof(IRIP_API_viCamera_SetOutMaxFps));
            api_GetReRunNumMax = (IRIP_API_viCamera_GetReRunNumMax)dll.Invoke("IRIP_API_viCamera_GetReRunNumMax", typeof(IRIP_API_viCamera_GetReRunNumMax));
            api_SetReRunNumMax = (IRIP_API_viCamera_SetReRunNumMax)dll.Invoke("IRIP_API_viCamera_SetReRunNumMax", typeof(IRIP_API_viCamera_SetReRunNumMax));
            api_SetFrameCallback = (IRIP_API_viCamera_SetFrameCallback)dll.Invoke("IRIP_API_viCamera_SetFrameCallback", typeof(IRIP_API_viCamera_SetFrameCallback));
            api_SetAdjustDirection = (IRIP_API_viCamera_SetAdjustDirection)dll.Invoke("IRIP_API_viCamera_SetAdjustDirection", typeof(IRIP_API_viCamera_SetAdjustDirection));
            api_GetVideoProcAmpConfigs = (IRIP_API_viCamera_GetVideoProcAmpConfigs)dll.Invoke("IRIP_API_viCamera_GetVideoProcAmpConfigs", typeof(IRIP_API_viCamera_GetVideoProcAmpConfigs));
            api_GetVideoProcAmpConfig = (IRIP_API_viCamera_GetVideoProcAmpConfig)dll.Invoke("IRIP_API_viCamera_GetVideoProcAmpConfig", typeof(IRIP_API_viCamera_GetVideoProcAmpConfig));
            api_SetVideoProcAmpConfigs = (IRIP_API_viCamera_SetVideoProcAmpConfigs)dll.Invoke("IRIP_API_viCamera_SetVideoProcAmpConfigs", typeof(IRIP_API_viCamera_SetVideoProcAmpConfigs));
            api_SetVideoProcAmpConfig = (IRIP_API_viCamera_SetVideoProcAmpConfig)dll.Invoke("IRIP_API_viCamera_SetVideoProcAmpConfig", typeof(IRIP_API_viCamera_SetVideoProcAmpConfig));
            api_GetCameraControlConfigs = (IRIP_API_viCamera_GetCameraControlConfigs)dll.Invoke("IRIP_API_viCamera_GetCameraControlConfigs", typeof(IRIP_API_viCamera_GetCameraControlConfigs));
            api_GetCameraControlConfig = (IRIP_API_viCamera_GetCameraControlConfig)dll.Invoke("IRIP_API_viCamera_GetCameraControlConfig", typeof(IRIP_API_viCamera_GetCameraControlConfig));
            api_SetCameraControlConfigs = (IRIP_API_viCamera_SetCameraControlConfigs)dll.Invoke("IRIP_API_viCamera_SetCameraControlConfigs", typeof(IRIP_API_viCamera_SetCameraControlConfigs));
            api_SetCameraControlConfig = (IRIP_API_viCamera_SetCameraControlConfig)dll.Invoke("IRIP_API_viCamera_SetCameraControlConfig", typeof(IRIP_API_viCamera_SetCameraControlConfig));
#endif
        }

#if _X86
            [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_searchUsbCameraDevices", CallingConvention = CallingConvention.StdCall)]
            static private extern int _IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);

    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_Start", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_Start(int id, ref CameraDevice device);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_Run", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_Run(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_Stop", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_Stop(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_Close", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_Close(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetDeviceInfo", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetDeviceInfo(int id, ref CameraDevice device);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetDropFrameNum", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetDropFrameNum(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetDropFrameNum", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetDropFrameNum(int id, int num);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetOutMaxFps(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetOutMaxFps(int id, int fps);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetReRunNumMax", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetReRunNumMax(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetReRunNumMax", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetReRunNumMax(int id, int num);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetFrameCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetAdjustDirection", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetVideoProcAmpConfigs", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetVideoProcAmpConfigs(int id, ref IAMConfig iamConfigs, int count);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetVideoProcAmpConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetVideoProcAmpConfig(int id, ref IAMConfig iamConfig);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetVideoProcAmpConfigs", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetVideoProcAmpConfigs(int id, ref IAMConfig iamConfigs, int count);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetVideoProcAmpConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetVideoProcAmpConfig(int id, ref IAMConfig iamConfig);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetCameraControlConfigs", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetCameraControlConfigs(int id, ref IAMConfig iamConfigs, int count);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_GetCameraControlConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_GetCameraControlConfig(int id, ref IAMConfig iamConfig);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetCameraControlConfigs", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetCameraControlConfigs(int id, ref IAMConfig iamConfigs, int count);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viCamera_SetCameraControlConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_viCamera_SetCameraControlConfig(int id, ref IAMConfig iamConfig);
#endif

        private delegate int IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);
        private IRIP_API_searchUsbCameraDevices api_searchUsbCameraDevices;

        private delegate int IRIP_API_viCamera_Start(int id, ref CameraDevice device);
        private IRIP_API_viCamera_Start api_Start;

        private delegate int IRIP_API_viCamera_Run(int id);
        private IRIP_API_viCamera_Run api_Run;

        private delegate int IRIP_API_viCamera_Stop(int id);
        private IRIP_API_viCamera_Stop api_Stop;

        private delegate int IRIP_API_viCamera_Close(int id);
        private IRIP_API_viCamera_Close api_Close;

        private delegate int IRIP_API_viCamera_GetDeviceInfo(int id, ref CameraDevice device);
        private IRIP_API_viCamera_GetDeviceInfo api_GetDeviceInfo;

        private delegate int IRIP_API_viCamera_GetDropFrameNum(int id);
        private IRIP_API_viCamera_GetDropFrameNum api_GetDropFrameNum;

        private delegate int IRIP_API_viCamera_SetDropFrameNum(int id, int num);
        private IRIP_API_viCamera_SetDropFrameNum api_SetDropFrameNum;

        private delegate int IRIP_API_viCamera_GetOutMaxFps(int id);
        private IRIP_API_viCamera_GetOutMaxFps api_GetOutMaxFps;

        private delegate int IRIP_API_viCamera_SetOutMaxFps(int id, int fps);
        private IRIP_API_viCamera_SetOutMaxFps api_SetOutMaxFps;

        private delegate int IRIP_API_viCamera_GetReRunNumMax(int id);
        private IRIP_API_viCamera_GetReRunNumMax api_GetReRunNumMax;

        private delegate int IRIP_API_viCamera_SetReRunNumMax(int id, int num);
        private IRIP_API_viCamera_SetReRunNumMax api_SetReRunNumMax;

        private delegate int IRIP_API_viCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_viCamera_SetFrameCallback api_SetFrameCallback;

        private delegate int IRIP_API_viCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
        private IRIP_API_viCamera_SetAdjustDirection api_SetAdjustDirection;

        private delegate int IRIP_API_viCamera_GetVideoProcAmpConfigs(int id, ref IAMConfig iamConfigs, int count);
        private IRIP_API_viCamera_GetVideoProcAmpConfigs api_GetVideoProcAmpConfigs;

        private delegate int IRIP_API_viCamera_GetVideoProcAmpConfig(int id, ref IAMConfig iamConfig);
        private IRIP_API_viCamera_GetVideoProcAmpConfig api_GetVideoProcAmpConfig;

        private delegate int IRIP_API_viCamera_SetVideoProcAmpConfigs(int id, ref IAMConfig iamConfigs, int count);
        private IRIP_API_viCamera_SetVideoProcAmpConfigs api_SetVideoProcAmpConfigs;

        private delegate int IRIP_API_viCamera_SetVideoProcAmpConfig(int id, ref IAMConfig iamConfig);
        private IRIP_API_viCamera_SetVideoProcAmpConfig api_SetVideoProcAmpConfig;

        private delegate int IRIP_API_viCamera_GetCameraControlConfigs(int id, ref IAMConfig iamConfigs, int count);
        private IRIP_API_viCamera_GetCameraControlConfigs api_GetCameraControlConfigs;

        private delegate int IRIP_API_viCamera_GetCameraControlConfig(int id, ref IAMConfig iamConfig);
        private IRIP_API_viCamera_GetCameraControlConfig api_GetCameraControlConfig;

        private delegate int IRIP_API_viCamera_SetCameraControlConfigs(int id, ref IAMConfig iamConfigs, int count);
        private IRIP_API_viCamera_SetCameraControlConfigs api_SetCameraControlConfigs;

        private delegate int IRIP_API_viCamera_SetCameraControlConfig(int id, ref IAMConfig iamConfig);
        private IRIP_API_viCamera_SetCameraControlConfig api_SetCameraControlConfig;

        /// <summary>
        /// <br>指定VID和PID检索USB相机</br>
        /// <para>@param devices 结构体CameraDevices</para>
        /// <para>@param vid [指定vid可选]</para>
        /// <para>@param pid [指定pid可选]</para>
        /// <para>@return</para>
        /// <summary>
        public int SearchCameraDevices(ref CameraDevices devices, ushort vid = 0, ushort pid = 0)
        {
            if (api_searchUsbCameraDevices == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }

            devices.vid = vid;
            devices.pid = pid;

            return api_searchUsbCameraDevices(ref devices);
        }

        /// <summary>
        /// <br>启动相机</br>
        /// <para>@param device 要启动的相机结构体（若已设置分辨率，自动查找并使用最接近的分辨率），启动成功返回包含应用的实际分辨率</para>
        /// <br>       device.bitCount项为设置相机返回原始数据类型(非实际回调数据类型, 回调后此值还是实际单像素数据位数) 0: RGB, 1: YUYV, 2: MJPG, 3: YUY2</br>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int Start(ref CameraDevice device)
        {
            if (api_Start == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Start(IRIP.GetID(), ref device);
        }

        /// <summary>
        /// <br>启动输出数据（如相机已被占用，则无法输出并回调数据）</br>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int Run()
        {
            if (api_Run == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Run(IRIP.GetID());
        }

        /// <summary>
        /// <br>停止输出数据</br>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int Stop()
        {
            if (api_Stop == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Stop(IRIP.GetID());
        }

        /// <summary>
        /// <br>关闭相机</br>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int Close()
        {
            if (api_Close == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Close(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取启动的相机信息</br>
        /// <para>@param device 相机结构体</para>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int GetDeviceInfo(ref CameraDevice device)
        {
            if (api_GetDeviceInfo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetDeviceInfo(IRIP.GetID(), ref device);
        }

        /// <summary>
        /// <br>设置间隔N帧输出一帧数据，以达到降帧目的</br>
        /// <para>@return 间隔帧数</para>
        /// </summary>
        public int GetDropFrameNum()
        {
            if (api_GetDropFrameNum == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetDropFrameNum(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取设置的间隔输出帧数</br>
        /// <para>@param num 间隔帧数</para>
        /// </summary>
        public int SetDropFrameNum(int num)
        {
            if (api_SetDropFrameNum == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetDropFrameNum(IRIP.GetID(), num);
        }

        /// <summary>
        /// <br>获取设置的输出最大帧率</br>
        /// <para>@return 帧率</para>
        /// </summary>
        public int GetOutMaxFps()
        {
            if (api_GetOutMaxFps == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetOutMaxFps(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置最大输出帧率，按时间计算，间隔帧设置值为0时有效</br>
        /// <para>@param fps 帧率</para>
        /// </summary>
        public int SetOutMaxFps(int fps)
        {
            if (api_SetOutMaxFps == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetOutMaxFps(IRIP.GetID(), fps);
        }

        /// <summary>
        /// <br>获取非主动断开链接重试启动相机最大次数（如相机意外断开连接，会尝试重新连接）</br>
        /// <para>@return 重试次数</para>
        /// </summary>
        public int GetReRunNumMax()
        {
            if (api_GetReRunNumMax == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetReRunNumMax(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置非主动断开链接重试启动相机最大次数（如相机意外断开连接，会尝试重新连接）</br>
        /// </summary>
        public int SetReRunNumMax(int num)
        {
            if (api_SetReRunNumMax == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetReRunNumMax(IRIP.GetID(), num);
        }

        /// <summary>
        /// <br>设置回调帧和回调函数</br>
        /// <para>@param frame 回调帧结构体，需要根据分辨率预审请需要返回项的内存</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int SetFrameCallback(IntPtr frame, RetCallback callback, IntPtr pUser)
        {
            if (api_SetFrameCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetFrameCallback(IRIP.GetID(), frame, callback, pUser);
        }

        /// <summary>
        /// <br>设置输出图像旋转方向</br>
        /// <para>@param rotate 旋转0/90/180/270</para>
        /// <para>@param mirror_h 水平镜像</para>
        /// <para>@param mirror_v 垂直镜像</para>
        /// <para>@return</para>
        /// </summary>
        public int SetAdjustDirection(int rotate = 0, bool mirror_h = false, bool mirror_v = false)
        {
            if (api_SetAdjustDirection == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetAdjustDirection(IRIP.GetID(), rotate, mirror_h, mirror_v);
        }

        /// <summary>
        /// <br>获取UVC相机图像参数</br>
        /// <para>@param iamConfigs 结构体数组</para>
        /// <para>@param count 数组长度</para>
        /// <para>@return</para>
        /// </summary>
        public int GetVideoProcAmpConfigs(ref IAMConfig iamConfigs, int count)
        {
            if (api_GetVideoProcAmpConfigs == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetVideoProcAmpConfigs(IRIP.GetID(), ref iamConfigs, count);
        }

        /// <summary>
        /// <br>获取UVC相机图像参数（单个参数）</br>
        /// <para>@param iamConfig 结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int GetVideoProcAmpConfig(ref IAMConfig iamConfig)
        {
            if (api_GetVideoProcAmpConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetVideoProcAmpConfig(IRIP.GetID(), ref iamConfig);
        }

        /// <summary>
        /// <br>设置UVC相机图像参数</br>
        /// <para>@param iamConfigs 结构体数组</para>
        /// <para>@param count 数组长度</para>
        /// <para>@return</para>
        /// </summary>
        public int SetVideoProcAmpConfigs(ref IAMConfig iamConfigs, int count)
        {
            if (api_SetVideoProcAmpConfigs == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetVideoProcAmpConfigs(IRIP.GetID(), ref iamConfigs, count);
        }

        /// <summary>
        /// <br>设置UVC相机图像参数（单个参数）</br>
        /// <para>@param iamConfig 结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int SetVideoProcAmpConfig(ref IAMConfig iamConfig)
        {
            if (api_SetVideoProcAmpConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetVideoProcAmpConfig(IRIP.GetID(), ref iamConfig);
        }

        /// <summary>
        /// <br>获取UVC相机控制参数</br>
        /// <para>@param iamConfigs 结构体数组</para>
        /// <para>@param count 数组长度</para>
        /// <para>@return</para>
        /// </summary>
        public int GetCameraControlConfigs(ref IAMConfig iamConfigs, int count)
        {
            if (api_GetCameraControlConfigs == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetCameraControlConfigs(IRIP.GetID(), ref iamConfigs, count);
        }

        /// <summary>
        /// <br>获取UVC相机控制参数（单个参数）</br>
        /// <para>@param iamConfig 结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int GetCameraControlConfig(ref IAMConfig iamConfig)
        {
            if (api_GetCameraControlConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetCameraControlConfig(IRIP.GetID(), ref iamConfig);
        }

        /// <summary>
        /// <br>设置UVC相机控制参数</br>
        /// <para>@param iamConfigs 结构体数组</para>
        /// <para>@param count 数组长度</para>
        /// <para>@return</para>
        /// </summary>
        public int SetCameraControlConfigs(ref IAMConfig iamConfigs, int count)
        {
            if (api_SetCameraControlConfigs == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetCameraControlConfigs(IRIP.GetID(), ref iamConfigs, count);
        }

        /// <summary>
        /// <br>设置UVC相机控制参数（单个参数）</br>
        /// <para>@param iamConfig 结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int SetCameraControlConfig(ref IAMConfig iamConfig)
        {
            if (api_SetCameraControlConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetCameraControlConfig(IRIP.GetID(), ref iamConfig);
        }

    }
}
