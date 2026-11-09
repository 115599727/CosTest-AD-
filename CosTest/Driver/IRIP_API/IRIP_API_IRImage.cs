using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>红外图像API， 解析红外图像和红外视频播放</br>
    /// </summary>
    public class IRIP_API_IRImage
    {
        private IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
        api_GetIRIPConfig = _IRIP_API_irImage_GetIRIPConfig;
        api_SetIRIPConfig = _IRIP_API_irImage_SetIRIPConfig;
        api_SelectPalette = _IRIP_API_irImage_SelectPalette;
        api_GetSelectPalette = _IRIP_API_irImage_GetSelectPalette;
        api_SetPalette = _IRIP_API_irImage_SetPalette;
        api_SetFilterMode = _IRIP_API_irImage_SetFilterMode;
        api_GetFilterMode = _IRIP_API_irImage_GetFilterMode;
        api_SetFrameCallback = _IRIP_API_irImage_SetFrameCallback;
        api_LoadFramesInfo = _IRIP_API_irImage_LoadFramesInfo;
        api_GetFrameData = _IRIP_API_irImage_GetFrameData;
        api_Open = _IRIP_API_irImage_Open;
        api_IsOpen = _IRIP_API_irImage_IsOpen;
        api_Close = _IRIP_API_irImage_Close;
        api_Speed = _IRIP_API_irImage_Speed;
        api_GetSpeed = _IRIP_API_irImage_GetSpeed;
        api_Play = _IRIP_API_irImage_Play;
        api_Seek = _IRIP_API_irImage_Seek;
        api_GetSeek = _IRIP_API_irImage_GetSeek;
        api_IsPlay = _IRIP_API_irImage_IsPlay;
        api_Pause = _IRIP_API_irImage_Pause;
        api_IsPause = _IRIP_API_irImage_IsPause;
        api_Stop = _IRIP_API_irImage_Stop;
        api_IsStop = _IRIP_API_irImage_IsStop;
        api_GetFrameDataCallback = _IRIP_API_irImage_GetFrameDataCallback;
        api_SaveIrImage = _IRIP_API_irImage_SaveIrImage;
        api_SaveIrAnimation = _IRIP_API_irImage_SaveIrAnimation;
        api_StopSaveIrAnimation = _IRIP_API_irImage_StopSaveIrAnimation;
        api_SetGlobalMDParameters = _IRIP_API_irImage_SetGlobalMDParameters;
        api_SetTempISO = _IRIP_API_irImage_SetTempISO;
        api_GetTempISO = _IRIP_API_irImage_GetTempISO;
        api_SetTempNephogram = _IRIP_API_irImage_SetTempNephogram;
        api_GetTempNephogram = _IRIP_API_irImage_GetTempNephogram;
        api_TempNephogramEnable = _IRIP_API_irImage_TempNephogramEnable;
        api_TempNephogramOverlay = _IRIP_API_irImage_TempNephogramOverlay;
        api_TempNephogramDpi = _IRIP_API_irImage_TempNephogramDpi;
#else
            api_GetIRIPConfig = (IRIP_API_irImage_GetIRIPConfig)dll.Invoke("IRIP_API_irImage_GetIRIPConfig", typeof(IRIP_API_irImage_GetIRIPConfig));
            api_SetIRIPConfig = (IRIP_API_irImage_SetIRIPConfig)dll.Invoke("IRIP_API_irImage_SetIRIPConfig", typeof(IRIP_API_irImage_SetIRIPConfig));
            api_SelectPalette = (IRIP_API_irImage_SelectPalette)dll.Invoke("IRIP_API_irImage_SelectPalette", typeof(IRIP_API_irImage_SelectPalette));
            api_GetSelectPalette = (IRIP_API_irImage_GetSelectPalette)dll.Invoke("IRIP_API_irImage_GetSelectPalette", typeof(IRIP_API_irImage_GetSelectPalette));
            api_SetPalette = (IRIP_API_irImage_SetPalette)dll.Invoke("IRIP_API_irImage_SetPalette", typeof(IRIP_API_irImage_SetPalette));
            api_SetFilterMode = (IRIP_API_irImage_SetFilterMode)dll.Invoke("IRIP_API_irImage_SetFilterMode", typeof(IRIP_API_irImage_SetFilterMode));
            api_GetFilterMode = (IRIP_API_irImage_GetFilterMode)dll.Invoke("IRIP_API_irImage_GetFilterMode", typeof(IRIP_API_irImage_GetFilterMode));
            api_SetFrameCallback = (IRIP_API_irImage_SetFrameCallback)dll.Invoke("IRIP_API_irImage_SetFrameCallback", typeof(IRIP_API_irImage_SetFrameCallback));
            api_LoadFramesInfo = (IRIP_API_irImage_LoadFramesInfo)dll.Invoke("IRIP_API_irImage_LoadFramesInfo", typeof(IRIP_API_irImage_LoadFramesInfo));
            api_GetFrameData = (IRIP_API_irImage_GetFrameData)dll.Invoke("IRIP_API_irImage_GetFrameData", typeof(IRIP_API_irImage_GetFrameData));
            api_Open = (IRIP_API_irImage_Open)dll.Invoke("IRIP_API_irImage_Open", typeof(IRIP_API_irImage_Open));
            api_IsOpen = (IRIP_API_irImage_IsOpen)dll.Invoke("IRIP_API_irImage_IsOpen", typeof(IRIP_API_irImage_IsOpen));
            api_Close = (IRIP_API_irImage_Close)dll.Invoke("IRIP_API_irImage_Close", typeof(IRIP_API_irImage_Close));
            api_Speed = (IRIP_API_irImage_Speed)dll.Invoke("IRIP_API_irImage_Speed", typeof(IRIP_API_irImage_Speed));
            api_GetSpeed = (IRIP_API_irImage_GetSpeed)dll.Invoke("IRIP_API_irImage_GetSpeed", typeof(IRIP_API_irImage_GetSpeed));
            api_Play = (IRIP_API_irImage_Play)dll.Invoke("IRIP_API_irImage_Play", typeof(IRIP_API_irImage_Play));
            api_Seek = (IRIP_API_irImage_Seek)dll.Invoke("IRIP_API_irImage_Seek", typeof(IRIP_API_irImage_Seek));
            api_GetSeek = (IRIP_API_irImage_GetSeek)dll.Invoke("IRIP_API_irImage_GetSeek", typeof(IRIP_API_irImage_GetSeek));
            api_IsPlay = (IRIP_API_irImage_IsPlay)dll.Invoke("IRIP_API_irImage_IsPlay", typeof(IRIP_API_irImage_IsPlay));
            api_Pause = (IRIP_API_irImage_Pause)dll.Invoke("IRIP_API_irImage_Pause", typeof(IRIP_API_irImage_Pause));
            api_IsPause = (IRIP_API_irImage_IsPause)dll.Invoke("IRIP_API_irImage_IsPause", typeof(IRIP_API_irImage_IsPause));
            api_Stop = (IRIP_API_irImage_Stop)dll.Invoke("IRIP_API_irImage_Stop", typeof(IRIP_API_irImage_Stop));
            api_IsStop = (IRIP_API_irImage_IsStop)dll.Invoke("IRIP_API_irImage_IsStop", typeof(IRIP_API_irImage_IsStop));
            api_GetFrameDataCallback = (IRIP_API_irImage_GetFrameDataCallback)dll.Invoke("IRIP_API_irImage_GetFrameDataCallback", typeof(IRIP_API_irImage_GetFrameDataCallback));
            api_SaveIrImage = (IRIP_API_irImage_SaveIrImage)dll.Invoke("IRIP_API_irImage_SaveIrImage", typeof(IRIP_API_irImage_SaveIrImage));
            api_SaveIrAnimation = (IRIP_API_irImage_SaveIrAnimation)dll.Invoke("IRIP_API_irImage_SaveIrAnimation", typeof(IRIP_API_irImage_SaveIrAnimation));
            api_StopSaveIrAnimation = (IRIP_API_irImage_StopSaveIrAnimation)dll.Invoke("IRIP_API_irImage_StopSaveIrAnimation", typeof(IRIP_API_irImage_StopSaveIrAnimation));
            api_SetGlobalMDParameters = (IRIP_API_irImage_SetGlobalMDParameters)dll.Invoke("IRIP_API_irImage_SetGlobalMDParameters", typeof(IRIP_API_irImage_SetGlobalMDParameters));
            api_SetTempISO = (IRIP_API_irImage_SetTempISO)dll.Invoke("IRIP_API_irImage_SetTempISO", typeof(IRIP_API_irImage_SetTempISO));
            api_GetTempISO = (IRIP_API_irImage_GetTempISO)dll.Invoke("IRIP_API_irImage_GetTempISO", typeof(IRIP_API_irImage_GetTempISO));
            api_SetTempNephogram = (IRIP_API_irImage_SetTempNephogram)dll.Invoke("IRIP_API_irImage_SetTempNephogram", typeof(IRIP_API_irImage_SetTempNephogram));
            api_GetTempNephogram = (IRIP_API_irImage_GetTempNephogram)dll.Invoke("IRIP_API_irImage_GetTempNephogram", typeof(IRIP_API_irImage_GetTempNephogram));
            api_TempNephogramEnable = (IRIP_API_irImage_TempNephogramEnable)dll.Invoke("IRIP_API_irImage_TempNephogramEnable", typeof(IRIP_API_irImage_TempNephogramEnable));
            api_TempNephogramOverlay = (IRIP_API_irImage_TempNephogramOverlay)dll.Invoke("IRIP_API_irImage_TempNephogramOverlay", typeof(IRIP_API_irImage_TempNephogramOverlay));
            api_TempNephogramDpi = (IRIP_API_irImage_TempNephogramDpi)dll.Invoke("IRIP_API_irImage_TempNephogramDpi", typeof(IRIP_API_irImage_TempNephogramDpi));
#endif
        }

#if _X86
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetIRIPConfig(int id, ref IRIP_Config config);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetIRIPConfig(int id, ref IRIP_Config config);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SelectPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetSelectPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetFilterMode", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetFilterMode(int id, uint val);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetFilterMode", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetFilterMode(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetFrameCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_LoadFramesInfo", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_LoadFramesInfo(int id, LoadFrameCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetFrameData", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetFrameData(int id, uint index);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Open", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Open(int id, byte[] path, OpenCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_IsOpen", CallingConvention = CallingConvention.StdCall)]
    static private extern bool _IRIP_API_irImage_IsOpen(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Close", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Close(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Speed", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Speed(int id, float speed);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetSpeed", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetSpeed(int id, ref float speed);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Play", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Play(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Seek", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Seek(int id, uint index);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetSeek", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetSeek(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_IsPlay", CallingConvention = CallingConvention.StdCall)]
    static private extern bool _IRIP_API_irImage_IsPlay(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Pause", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Pause(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_IsPause", CallingConvention = CallingConvention.StdCall)]
    static private extern bool _IRIP_API_irImage_IsPause(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_Stop", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_Stop(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_IsStop", CallingConvention = CallingConvention.StdCall)]
    static private extern bool _IRIP_API_irImage_IsStop(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetFrameDataCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetFrameDataCallback(int id, uint index, IntPtr frame, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SaveIrImage", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SaveIrImage(int id, byte[] path, byte[] file, bool ropen, IntPtr frame, RetCallbackPlus callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SaveIrAnimation", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SaveIrAnimation(int id, byte[] path, byte[] file, IntPtr frame, RetCallbackPlus callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_StopSaveIrAnimation", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_StopSaveIrAnimation(int id, RetCallbackPlus callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetGlobalMDParameters", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetGlobalMDParameters(int id, ref MDParameters parameters);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetTempISO", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetTempISO(int id, ref TempISO iso);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetTempISO", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetTempISO(int id, ref TempISO iso);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_SetTempNephogram", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_GetTempNephogram", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_TempNephogramEnable", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_TempNephogramEnable(int id, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_TempNephogramOverlay", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_TempNephogramOverlay(int id, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irImage_TempNephogramDpi", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_irImage_TempNephogramDpi(int id, float dpi);
#endif

        private delegate int IRIP_API_irImage_GetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_irImage_GetIRIPConfig api_GetIRIPConfig;

        private delegate int IRIP_API_irImage_SetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_irImage_SetIRIPConfig api_SetIRIPConfig;

        private delegate int IRIP_API_irImage_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_irImage_SelectPalette api_SelectPalette;

        private delegate int IRIP_API_irImage_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
        private IRIP_API_irImage_GetSelectPalette api_GetSelectPalette;

        private delegate int IRIP_API_irImage_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_irImage_SetPalette api_SetPalette;

        private delegate int IRIP_API_irImage_SetFilterMode(int id, uint val);
        private IRIP_API_irImage_SetFilterMode api_SetFilterMode;

        private delegate int IRIP_API_irImage_GetFilterMode(int id);
        private IRIP_API_irImage_GetFilterMode api_GetFilterMode;

        private delegate int IRIP_API_irImage_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_irImage_SetFrameCallback api_SetFrameCallback;

        private delegate int IRIP_API_irImage_LoadFramesInfo(int id, LoadFrameCallback callback, IntPtr pUser);
        private IRIP_API_irImage_LoadFramesInfo api_LoadFramesInfo;

        private delegate int IRIP_API_irImage_GetFrameData(int id, uint index);
        private IRIP_API_irImage_GetFrameData api_GetFrameData;

        private delegate int IRIP_API_irImage_Open(int id, byte[] path, OpenCallback callback, IntPtr pUser);
        private IRIP_API_irImage_Open api_Open;

        private delegate bool IRIP_API_irImage_IsOpen(int id);
        private IRIP_API_irImage_IsOpen api_IsOpen;

        private delegate int IRIP_API_irImage_Close(int id);
        private IRIP_API_irImage_Close api_Close;

        private delegate int IRIP_API_irImage_Speed(int id, float speed);
        private IRIP_API_irImage_Speed api_Speed;

        private delegate int IRIP_API_irImage_GetSpeed(int id, ref float speed);
        private IRIP_API_irImage_GetSpeed api_GetSpeed;

        private delegate int IRIP_API_irImage_Play(int id);
        private IRIP_API_irImage_Play api_Play;

        private delegate int IRIP_API_irImage_Seek(int id, uint index);
        private IRIP_API_irImage_Seek api_Seek;

        private delegate int IRIP_API_irImage_GetSeek(int id);
        private IRIP_API_irImage_GetSeek api_GetSeek;

        private delegate bool IRIP_API_irImage_IsPlay(int id);
        private IRIP_API_irImage_IsPlay api_IsPlay;

        private delegate int IRIP_API_irImage_Pause(int id);
        private IRIP_API_irImage_Pause api_Pause;

        private delegate bool IRIP_API_irImage_IsPause(int id);
        private IRIP_API_irImage_IsPause api_IsPause;

        private delegate int IRIP_API_irImage_Stop(int id);
        private IRIP_API_irImage_Stop api_Stop;

        private delegate bool IRIP_API_irImage_IsStop(int id);
        private IRIP_API_irImage_IsStop api_IsStop;

        private delegate int IRIP_API_irImage_GetFrameDataCallback(int id, uint index, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_irImage_GetFrameDataCallback api_GetFrameDataCallback;

        private delegate int IRIP_API_irImage_SaveIrImage(int id, byte[] path, byte[] file, bool ropen, IntPtr frame, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irImage_SaveIrImage api_SaveIrImage;

        private delegate int IRIP_API_irImage_SaveIrAnimation(int id, byte[] path, byte[] file, IntPtr frame, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irImage_SaveIrAnimation api_SaveIrAnimation;

        private delegate int IRIP_API_irImage_StopSaveIrAnimation(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irImage_StopSaveIrAnimation api_StopSaveIrAnimation;

        private delegate int IRIP_API_irImage_SetGlobalMDParameters(int id, ref MDParameters parameters);
        private IRIP_API_irImage_SetGlobalMDParameters api_SetGlobalMDParameters;

        private delegate int IRIP_API_irImage_SetTempISO(int id, ref TempISO iso);
        private IRIP_API_irImage_SetTempISO api_SetTempISO;

        private delegate int IRIP_API_irImage_GetTempISO(int id, ref TempISO iso);
        private IRIP_API_irImage_GetTempISO api_GetTempISO;

        private delegate int IRIP_API_irImage_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_irImage_SetTempNephogram api_SetTempNephogram;

        private delegate int IRIP_API_irImage_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_irImage_GetTempNephogram api_GetTempNephogram;

        private delegate int IRIP_API_irImage_TempNephogramEnable(int id, bool enable);
        private IRIP_API_irImage_TempNephogramEnable api_TempNephogramEnable;

        private delegate int IRIP_API_irImage_TempNephogramOverlay(int id, bool enable);
        private IRIP_API_irImage_TempNephogramOverlay api_TempNephogramOverlay;

        private delegate int IRIP_API_irImage_TempNephogramDpi(int id, float dpi);
        private IRIP_API_irImage_TempNephogramDpi api_TempNephogramDpi;

        /// <summary>
        /// <br>获取图像渲染设数</br>
        /// <para>@param config 参数结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int GetIRIPConfig(ref IRIP_Config config)
        {
            if (api_GetIRIPConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetIRIPConfig(IRIP.GetID(), ref config);
        }

        /// <summary>
        /// <br>设置图像渲染参数</br>
        /// <para>@param config 参数结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int SetIRIPConfig(ref IRIP_Config config)
        {
            if (api_SetIRIPConfig == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetIRIPConfig(IRIP.GetID(), ref config);
        }

        /// <summary>
        /// <br>根据序号选择内置调色板</br>
        /// <para>@param index 调色板序号</para>
        /// <para>@param galpha 全局不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param lalpha 低温不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param preMultiplied 透明度预乘</para>
        /// <para>@param rgbInversion RGB反转</para>
        /// </summary>
        public int SelectPalette(uint index, float galpha = 1.0f, float lalpha = 1.0f, bool preMultiplied = false, bool rgbInversion = false)
        {
            if (api_SelectPalette == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SelectPalette(IRIP.GetID(), index, galpha, lalpha, preMultiplied, rgbInversion);
        }

        /// <summary>
        /// <br>获取选择的调色板序号和相关设置参数, 引用赋值index为负值表示当前设置的调色板为传入的自定义调色板</br>
        /// <para>@param index 调色板序号</para>
        /// <para>@param galpha 全局不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param lalpha 低温不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param preMultiplied 透明度预乘</para>
        /// <para>@param rgbInversion RGB反转</para>
        /// </summary>
        public int GetSelectPalette(ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion)
        {
            if (api_GetSelectPalette == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSelectPalette(IRIP.GetID(), ref index, ref galpha, ref lalpha, ref preMultiplied, ref rgbInversion);
        }

        /// <summary>
        /// <br>设置自定义调色板</br>
        /// <para>@param palette 32位256色阶调色板</para>
        /// <para>@param galpha 全局不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param lalpha 低温不透明度 0.0-1.0，不需透明值为1.0</para>
        /// <para>@param preMultiplied 透明度预乘</para>
        /// <para>@param rgbInversion RGB反转</para>
        /// </summary>
        public int SetPalette(uint[] palette, float galpha = 1.0f, float lalpha = 1.0f, bool preMultiplied = false, bool rgbInversion = false)
        {
            if (api_SetPalette == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetPalette(IRIP.GetID(), palette, galpha, lalpha, preMultiplied, rgbInversion);
        }

        /// <summary>
        /// <br>设置测温滤波模式</br>
        /// <para>@param val 滤波模式， 0：关闭， 1：均值， 2：增强</para>
        /// <para>@return</para>
        /// </summary>
        public int SetFilterMode(uint val)
        {
            if (api_SetFilterMode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetFilterMode(IRIP.GetID(), val);
        }

        /// <summary>
        /// <br>设置测温滤波模式</br>
        /// <para>@return 滤波模式， 0：关闭， 1：均值， 2：增强</para>
        /// </summary>
        public int GetFilterMode()
        {
            if (api_GetFilterMode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFilterMode(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置帧回调函数及数据内存地址</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
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
        /// <br>设置红外视频图像加载进度回调函数</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int LoadFramesInfo(LoadFrameCallback callback, IntPtr pUser)
        {
            if (api_LoadFramesInfo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_LoadFramesInfo(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>获取帧数据</br>
        /// <para>@param index 帧序号</para>
        /// </summary>
        public int GetFrameData(uint index)
        {
            if (api_GetFrameData == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFrameData(IRIP.GetID(), index);
        }

        /// <summary>
        /// <br>打开红外图像或红外视频</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int Open(string path, OpenCallback callback, IntPtr pUser)
        {
            if (api_Open == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Open(IRIP.GetID(), Encoding.UTF8.GetBytes(path), callback, pUser);
        }

        /// <summary>
        /// <br>红外图像或红外视频是否打开</br>
        /// </summary>
        public bool IsOpen()
        {
            if (api_IsOpen == null)
            {
            }
            return api_IsOpen(IRIP.GetID());
        }

        /// <summary>
        /// <br>关闭红外图像或红外视频</br>
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
        /// <br>设置播放速率</br>
        /// <para>@param speed 播放速率, 0.1x - 30x, 最高支持播放速度视实际加载速度而定</para>
        /// </summary>
        public int Speed(float speed)
        {
            if (api_Speed == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Speed(IRIP.GetID(), speed);
        }

        /// <summary>
        /// <br>获取设置的播放速率</br>
        /// <para>@param speed 播放速率</para>
        /// </summary>
        public int GetSpeed(ref float speed)
        {
            if (api_GetSpeed == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSpeed(IRIP.GetID(), ref speed);
        }

        /// <summary>
        /// <br>开始播放（红外视频有效）</br>
        /// </summary>
        public int Play()
        {
            if (api_Play == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Play(IRIP.GetID());
        }

        /// <summary>
        /// <br>跳转到帧（播放红外视频有效）</br>
        /// <para>@param index 帧序号</para>
        /// </summary>
        public int Seek(uint index)
        {
            if (api_Seek == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Seek(IRIP.GetID(), index);
        }

        /// <summary>
        /// <br>获取当前帧序号（播放红外视频有效）</br>
        /// </summary>
        public int GetSeek()
        {
            if (api_GetSeek == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSeek(IRIP.GetID());
        }

        /// <summary>
        /// <br>是否在播放状态（播放红外视频有效）</br>
        /// </summary>
        public bool IsPlay()
        {
            if (api_IsPlay == null)
            {
            }
            return api_IsPlay(IRIP.GetID());
        }

        /// <summary>
        /// <br>暂停播放（播放红外视频有效）</br>
        /// </summary>
        public int Pause()
        {
            if (api_Pause == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Pause(IRIP.GetID());
        }

        /// <summary>
        /// <br>是否暂停播放（播放红外视频有效）</br>
        /// </summary>
        public bool IsPause()
        {
            if (api_IsPause == null)
            {
            }
            return api_IsPause(IRIP.GetID());
        }

        /// <summary>
        /// <br>停止播放（播放红外视频有效）</br>
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
        /// <br>是否停止播放（播放红外视频有效）</br>
        /// </summary>
        public bool IsStop()
        {
            if (api_IsStop == null)
            {
            }
            return api_IsStop(IRIP.GetID());
        }

        /// <summary>
        /// <br>直接传入帧序号、回调函数和数据内存地址获取帧数据</br>
        /// <para>@param frame  帧数据结构体</para>
        /// <para>@param index 帧序号</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int GetFrameDataCallback(uint index, IntPtr frame, RetCallback callback, IntPtr pUser)
        {
            if (api_GetFrameDataCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFrameDataCallback(IRIP.GetID(), index, frame, callback, pUser);
        }

        /// <summary>
        /// <br>保存红外图像</br>
        /// <para>@param path  保存路径</para>
        /// <para>@param file  文件名</para>
        /// <para>@param ropen  如果文件已打开的保存后是否再次打开文件</para>
        /// <para>@param frame  帧数据结构体</para>
        /// <para>@param callback  回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int SaveIrImage(string path, string file, bool ropen, IntPtr frame, RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SaveIrImage == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SaveIrImage(IRIP.GetID(), Encoding.UTF8.GetBytes(path), Encoding.UTF8.GetBytes(file), ropen, frame, callback, pUser);
        }

        /// <summary>
        /// <br>保存/追加红外视频帧（动画）</br>
        /// <para>@param path  保存路径</para>
        /// <para>@param file  文件名</para>
        /// <para>@param frame  帧数据结构体</para>
        /// <para>@param callback  回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int SaveIrAnimation(string path, string file, IntPtr frame, RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SaveIrAnimation == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SaveIrAnimation(IRIP.GetID(), Encoding.UTF8.GetBytes(path), Encoding.UTF8.GetBytes(file), frame, callback, pUser);
        }

        /// <summary>
        /// <br>停止保存红外视频（动画），写入结束标志</br>
        /// <para>@param callback  回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int StopSaveIrAnimation(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_StopSaveIrAnimation == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_StopSaveIrAnimation(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>设置全局测量参数</br>
        /// <para>@param parameters 测量参数结构体指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetGlobalMDParameters(ref MDParameters parameters)
        {
            if (api_SetGlobalMDParameters == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetGlobalMDParameters(IRIP.GetID(), ref parameters);
        }

        /// <summary>
        /// <br>设置ISO</br>
        /// <para>@param iso 数据结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int SetTempISO(ref TempISO iso)
        {
            if (api_SetTempISO == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetTempISO(IRIP.GetID(), ref iso);
        }

        /// <summary>
        /// <br>获取ISO</br>
        /// <para>@param iso 数据结构体</para>
        /// <para>@return</para>
        /// </summary>
        public int GetTempISO(ref TempISO iso)
        {
            if (api_GetTempISO == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetTempISO(IRIP.GetID(), ref iso);
        }

        /// <summary>
        /// <br>设置温度云图</br>
        /// <para>@param nephogram 云图数据结构体</para>
        /// <para>@param enable 是否启用</para>
        /// <para>@param overlay 叠加(云图温度范围外保留原调色板背景,否则透明)</para>
        /// <para>@return</para>
        /// </summary>
        public int SetTempNephogram(ref TempNephogram nephogram, ref bool enable, ref bool overlay)
        {
            if (api_SetTempNephogram == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetTempNephogram(IRIP.GetID(), ref nephogram, ref enable, ref overlay);
        }

        /// <summary>
        /// <br>获取温度云图</br>
        /// <para>@param nephogram 云图数据结构体</para>
        /// <para>@param enable 是否启用</para>
        /// <para>@param overlay 颜色叠加(云图温度范围外保留原调色板背景,否则透明)</para>
        /// <para>@return</para>
        /// </summary>
        public int GetTempNephogram(ref TempNephogram nephogram, ref bool enable, ref bool overlay)
        {
            if (api_GetTempNephogram == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetTempNephogram(IRIP.GetID(), ref nephogram, ref enable, ref overlay);
        }

        /// <summary>
        /// <br>设置云图是否启用</br>
        /// <para>@param enable 是否启用</para>
        /// <para>@return</para>
        /// </summary>
        public int TempNephogramEnable(bool enable)
        {
            if (api_TempNephogramEnable == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_TempNephogramEnable(IRIP.GetID(), enable);
        }

        /// <summary>
        /// <br>设置云图是否启用颜色叠加模式</br>
        /// <para>@param enable 颜色叠加(云图温度范围外保留原调色板背景,否则透明)</para>
        /// <para>@return</para>
        /// </summary>
        public int TempNephogramOverlay(bool enable)
        {
            if (api_TempNephogramOverlay == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_TempNephogramOverlay(IRIP.GetID(), enable);
        }

        /// <summary>
        /// <br>设置云图分辨率</br>
        /// <para>@param dpi 分辨率dpi</para>
        /// <para>@return</para>
        /// </summary>
        public int TempNephogramDpi(float dpi)
        {
            if (api_TempNephogramDpi == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_TempNephogramDpi(IRIP.GetID(), dpi);
        }
    }
}
