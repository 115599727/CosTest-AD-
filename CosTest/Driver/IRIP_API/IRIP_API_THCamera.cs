using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>TH红外相机</br>
    /// </summary>
    public class IRIP_API_THCamera
    {
        private IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
        api_GetSearchCamerasMax = _IRIP_API_thCamera_GetSearchCamerasMax;
        api_SearchCameraDevices = _IRIP_API_thCamera_SearchCameraDevices;
        api_SetNucTablesPath = _IRIP_API_thCamera_SetNucTablesPath;
        api_GetSegmentCalibData = _IRIP_API_thCamera_GetSegmentCalibData;
        api_SetSegmentCalibDataForFile = _IRIP_API_thCamera_SetSegmentCalibDataForFile;
        api_SetSegmentCalibData = _IRIP_API_thCamera_SetSegmentCalibData;
        api_GetCalibrationDistance = _IRIP_API_thCamera_GetCalibrationDistance;
        api_SetCalibrationDistance = _IRIP_API_thCamera_SetCalibrationDistance;
        api_SetStatesCallback = _IRIP_API_thCamera_SetStatesCallback;
        api_ConnectToDevice = _IRIP_API_thCamera_ConnectToDevice;
        api_SetFrameCallback = _IRIP_API_thCamera_SetFrameCallback;
        api_AfterConnect = _IRIP_API_thCamera_AfterConnect;
        api_GetMissingFilesJson = _IRIP_API_thCamera_GetMissingFilesJson;
        api_StartVideo = _IRIP_API_thCamera_StartVideo;
        api_StopVideo = _IRIP_API_thCamera_StopVideo;
        api_PauseVideo = _IRIP_API_thCamera_PauseVideo;
        api_NonUniformityCorrectionEnable = _IRIP_API_thCamera_NonUniformityCorrectionEnable;
        api_StartNonUniformityCorrection = _IRIP_API_thCamera_StartNonUniformityCorrection;
        api_SelectPalette = _IRIP_API_thCamera_SelectPalette;
        api_GetSelectPalette = _IRIP_API_thCamera_GetSelectPalette;
        api_SetPalette = _IRIP_API_thCamera_SetPalette;
        api_SetFilterMode = _IRIP_API_thCamera_SetFilterMode;
        api_GetFilterMode = _IRIP_API_thCamera_GetFilterMode;
        api_GetIRIPConfig = _IRIP_API_thCamera_GetIRIPConfig;
        api_SetIRIPConfig = _IRIP_API_thCamera_SetIRIPConfig;
        api_SetAdjustDirection = _IRIP_API_thCamera_SetAdjustDirection;
        api_SetMeasurement = _IRIP_API_thCamera_SetMeasurement;
        api_SetTempISO = _IRIP_API_thCamera_SetTempISO;
        api_GetTempISO = _IRIP_API_thCamera_GetTempISO;
        api_SetTempNephogram = _IRIP_API_thCamera_SetTempNephogram;
        api_GetTempNephogram = _IRIP_API_thCamera_GetTempNephogram;
        api_TempNephogramEnable = _IRIP_API_thCamera_TempNephogramEnable;
        api_TempNephogramOverlay = _IRIP_API_thCamera_TempNephogramOverlay;
        api_TempNephogramDpi = _IRIP_API_thCamera_TempNephogramDpi;
        api_RefreshData = _IRIP_API_thCamera_RefreshData;
        api_SetGlobalMDParameters = _IRIP_API_thCamera_SetGlobalMDParameters;
        api_EnRefEmissivityTransmittanceUnit = _IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit;
#else
            api_GetSearchCamerasMax = (IRIP_API_thCamera_GetSearchCamerasMax)dll.Invoke("IRIP_API_thCamera_GetSearchCamerasMax", typeof(IRIP_API_thCamera_GetSearchCamerasMax));
            api_SearchCameraDevices = (IRIP_API_thCamera_SearchCameraDevices)dll.Invoke("IRIP_API_thCamera_SearchCameraDevices", typeof(IRIP_API_thCamera_SearchCameraDevices));
            api_SetNucTablesPath = (IRIP_API_thCamera_SetNucTablesPath)dll.Invoke("IRIP_API_thCamera_SetNucTablesPath", typeof(IRIP_API_thCamera_SetNucTablesPath));
            api_GetSegmentCalibData = (IRIP_API_thCamera_GetSegmentCalibData)dll.Invoke("IRIP_API_thCamera_GetSegmentCalibData", typeof(IRIP_API_thCamera_GetSegmentCalibData));
            api_SetSegmentCalibDataForFile = (IRIP_API_thCamera_SetSegmentCalibDataForFile)dll.Invoke("IRIP_API_thCamera_SetSegmentCalibDataForFile", typeof(IRIP_API_thCamera_SetSegmentCalibDataForFile));
            api_SetSegmentCalibData = (IRIP_API_thCamera_SetSegmentCalibData)dll.Invoke("IRIP_API_thCamera_SetSegmentCalibData", typeof(IRIP_API_thCamera_SetSegmentCalibData));
            api_GetCalibrationDistance = (IRIP_API_thCamera_GetCalibrationDistance)dll.Invoke("IRIP_API_thCamera_GetCalibrationDistance", typeof(IRIP_API_thCamera_GetCalibrationDistance));
            api_SetCalibrationDistance = (IRIP_API_thCamera_SetCalibrationDistance)dll.Invoke("IRIP_API_thCamera_SetCalibrationDistance", typeof(IRIP_API_thCamera_SetCalibrationDistance));
            api_SetStatesCallback = (IRIP_API_thCamera_SetStatesCallback)dll.Invoke("IRIP_API_thCamera_SetStatesCallback", typeof(IRIP_API_thCamera_SetStatesCallback));
            api_ConnectToDevice = (IRIP_API_thCamera_ConnectToDevice)dll.Invoke("IRIP_API_thCamera_ConnectToDevice", typeof(IRIP_API_thCamera_ConnectToDevice));
            api_SetFrameCallback = (IRIP_API_thCamera_SetFrameCallback)dll.Invoke("IRIP_API_thCamera_SetFrameCallback", typeof(IRIP_API_thCamera_SetFrameCallback));
            api_AfterConnect = (IRIP_API_thCamera_AfterConnect)dll.Invoke("IRIP_API_thCamera_AfterConnect", typeof(IRIP_API_thCamera_AfterConnect));
            api_GetMissingFilesJson = (IRIP_API_thCamera_GetMissingFilesJson)dll.Invoke("IRIP_API_thCamera_GetMissingFilesJson", typeof(IRIP_API_thCamera_GetMissingFilesJson));
            api_StartVideo = (IRIP_API_thCamera_StartVideo)dll.Invoke("IRIP_API_thCamera_StartVideo", typeof(IRIP_API_thCamera_StartVideo));
            api_StopVideo = (IRIP_API_thCamera_StopVideo)dll.Invoke("IRIP_API_thCamera_StopVideo", typeof(IRIP_API_thCamera_StopVideo));
            api_PauseVideo = (IRIP_API_thCamera_PauseVideo)dll.Invoke("IRIP_API_thCamera_PauseVideo", typeof(IRIP_API_thCamera_PauseVideo));
            api_NonUniformityCorrectionEnable = (IRIP_API_thCamera_NonUniformityCorrectionEnable)dll.Invoke("IRIP_API_thCamera_NonUniformityCorrectionEnable", typeof(IRIP_API_thCamera_NonUniformityCorrectionEnable));
            api_StartNonUniformityCorrection = (IRIP_API_thCamera_StartNonUniformityCorrection)dll.Invoke("IRIP_API_thCamera_StartNonUniformityCorrection", typeof(IRIP_API_thCamera_StartNonUniformityCorrection));
            api_SelectPalette = (IRIP_API_thCamera_SelectPalette)dll.Invoke("IRIP_API_thCamera_SelectPalette", typeof(IRIP_API_thCamera_SelectPalette));
            api_GetSelectPalette = (IRIP_API_thCamera_GetSelectPalette)dll.Invoke("IRIP_API_thCamera_GetSelectPalette", typeof(IRIP_API_thCamera_GetSelectPalette));
            api_SetPalette = (IRIP_API_thCamera_SetPalette)dll.Invoke("IRIP_API_thCamera_SetPalette", typeof(IRIP_API_thCamera_SetPalette));
            api_SetFilterMode = (IRIP_API_thCamera_SetFilterMode)dll.Invoke("IRIP_API_thCamera_SetFilterMode", typeof(IRIP_API_thCamera_SetFilterMode));
            api_GetFilterMode = (IRIP_API_thCamera_GetFilterMode)dll.Invoke("IRIP_API_thCamera_GetFilterMode", typeof(IRIP_API_thCamera_GetFilterMode));
            api_GetIRIPConfig = (IRIP_API_thCamera_GetIRIPConfig)dll.Invoke("IRIP_API_thCamera_GetIRIPConfig", typeof(IRIP_API_thCamera_GetIRIPConfig));
            api_SetIRIPConfig = (IRIP_API_thCamera_SetIRIPConfig)dll.Invoke("IRIP_API_thCamera_SetIRIPConfig", typeof(IRIP_API_thCamera_SetIRIPConfig));
            api_SetAdjustDirection = (IRIP_API_thCamera_SetAdjustDirection)dll.Invoke("IRIP_API_thCamera_SetAdjustDirection", typeof(IRIP_API_thCamera_SetAdjustDirection));
            api_SetMeasurement = (IRIP_API_thCamera_SetMeasurement)dll.Invoke("IRIP_API_thCamera_SetMeasurement", typeof(IRIP_API_thCamera_SetMeasurement));
            api_SetTempISO = (IRIP_API_thCamera_SetTempISO)dll.Invoke("IRIP_API_thCamera_SetTempISO", typeof(IRIP_API_thCamera_SetTempISO));
            api_GetTempISO = (IRIP_API_thCamera_GetTempISO)dll.Invoke("IRIP_API_thCamera_GetTempISO", typeof(IRIP_API_thCamera_GetTempISO));
            api_SetTempNephogram = (IRIP_API_thCamera_SetTempNephogram)dll.Invoke("IRIP_API_thCamera_SetTempNephogram", typeof(IRIP_API_thCamera_SetTempNephogram));
            api_GetTempNephogram = (IRIP_API_thCamera_GetTempNephogram)dll.Invoke("IRIP_API_thCamera_GetTempNephogram", typeof(IRIP_API_thCamera_GetTempNephogram));
            api_TempNephogramEnable = (IRIP_API_thCamera_TempNephogramEnable)dll.Invoke("IRIP_API_thCamera_TempNephogramEnable", typeof(IRIP_API_thCamera_TempNephogramEnable));
            api_TempNephogramOverlay = (IRIP_API_thCamera_TempNephogramOverlay)dll.Invoke("IRIP_API_thCamera_TempNephogramOverlay", typeof(IRIP_API_thCamera_TempNephogramOverlay));
            api_TempNephogramDpi = (IRIP_API_thCamera_TempNephogramDpi)dll.Invoke("IRIP_API_thCamera_TempNephogramDpi", typeof(IRIP_API_thCamera_TempNephogramDpi));
            api_RefreshData = (IRIP_API_thCamera_RefreshData)dll.Invoke("IRIP_API_thCamera_RefreshData", typeof(IRIP_API_thCamera_RefreshData));
            api_SetGlobalMDParameters = (IRIP_API_thCamera_SetGlobalMDParameters)dll.Invoke("IRIP_API_thCamera_SetGlobalMDParameters", typeof(IRIP_API_thCamera_SetGlobalMDParameters));
            api_EnRefEmissivityTransmittanceUnit = (IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit)dll.Invoke("IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit", typeof(IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit));
#endif
        }

#if _X86
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetSearchCamerasMax", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetSearchCamerasMax();
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SearchCameraDevices", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SearchCameraDevices(ref thDevices devices);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetNucTablesPath", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetNucTablesPath(int id, byte[] path);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetSegmentCalibDataForFile", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetCalibrationDistance(int id, ref float distance);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetCalibrationDistance(int id, float distance);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetStatesCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetStatesCallback(int id, ref thCameraStates states, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_ConnectToDevice", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_ConnectToDevice(int id, ref thDevice device, CameraConnectCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetFrameCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_AfterConnect", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_AfterConnect(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetMissingFilesJson", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetMissingFilesJson(int id, byte[] data, int length);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_StartVideo", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_StartVideo(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_StopVideo", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_StopVideo(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_PauseVideo", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_PauseVideo(int id, bool pause);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_NonUniformityCorrectionEnable", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_NonUniformityCorrectionEnable(int id, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_StartNonUniformityCorrection", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_StartNonUniformityCorrection(int id, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SelectPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetSelectPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetPalette", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetFilterMode", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetFilterMode(int id, uint val);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetFilterMode", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetFilterMode(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetIRIPConfig(int id, ref IRIP_Config config);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetIRIPConfig(int id, ref IRIP_Config config);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetAdjustDirection", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetMeasurement", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetTempISO", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetTempISO(int id, ref TempISO iso);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetTempISO", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetTempISO(int id, ref TempISO iso);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetTempNephogram", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_GetTempNephogram", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_TempNephogramEnable", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_TempNephogramEnable(int id, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_TempNephogramOverlay", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_TempNephogramOverlay(int id, bool enable);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_TempNephogramDpi", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_TempNephogramDpi(int id, float dpi);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_RefreshData", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_RefreshData(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_SetGlobalMDParameters", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
#endif

        private delegate int IRIP_API_thCamera_GetSearchCamerasMax();
        private IRIP_API_thCamera_GetSearchCamerasMax api_GetSearchCamerasMax;

        private delegate int IRIP_API_thCamera_SearchCameraDevices(ref thDevices devices);
        private IRIP_API_thCamera_SearchCameraDevices api_SearchCameraDevices;

        private delegate int IRIP_API_thCamera_SetNucTablesPath(int id, byte[] path);
        private IRIP_API_thCamera_SetNucTablesPath api_SetNucTablesPath;

        private delegate int IRIP_API_thCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
        private IRIP_API_thCamera_GetSegmentCalibData api_GetSegmentCalibData;

        private delegate int IRIP_API_thCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
        private IRIP_API_thCamera_SetSegmentCalibDataForFile api_SetSegmentCalibDataForFile;

        private delegate int IRIP_API_thCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
        private IRIP_API_thCamera_SetSegmentCalibData api_SetSegmentCalibData;

        private delegate int IRIP_API_thCamera_GetCalibrationDistance(int id, ref float distance);
        private IRIP_API_thCamera_GetCalibrationDistance api_GetCalibrationDistance;

        private delegate int IRIP_API_thCamera_SetCalibrationDistance(int id, float distance);
        private IRIP_API_thCamera_SetCalibrationDistance api_SetCalibrationDistance;

        private delegate int IRIP_API_thCamera_SetStatesCallback(int id, ref thCameraStates states, RetCallback callback, IntPtr pUser);
        private IRIP_API_thCamera_SetStatesCallback api_SetStatesCallback;

        private delegate int IRIP_API_thCamera_ConnectToDevice(int id, ref thDevice device, CameraConnectCallback callback, IntPtr pUser);
        private IRIP_API_thCamera_ConnectToDevice api_ConnectToDevice;

        private delegate int IRIP_API_thCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_thCamera_SetFrameCallback api_SetFrameCallback;

        private delegate int IRIP_API_thCamera_AfterConnect(int id);
        private IRIP_API_thCamera_AfterConnect api_AfterConnect;

        private delegate int IRIP_API_thCamera_GetMissingFilesJson(int id, byte[] data, int length);
        private IRIP_API_thCamera_GetMissingFilesJson api_GetMissingFilesJson;

        private delegate int IRIP_API_thCamera_StartVideo(int id);
        private IRIP_API_thCamera_StartVideo api_StartVideo;

        private delegate int IRIP_API_thCamera_StopVideo(int id);
        private IRIP_API_thCamera_StopVideo api_StopVideo;

        private delegate int IRIP_API_thCamera_PauseVideo(int id, bool pause);
        private IRIP_API_thCamera_PauseVideo api_PauseVideo;

        private delegate int IRIP_API_thCamera_NonUniformityCorrectionEnable(int id, bool enable);
        private IRIP_API_thCamera_NonUniformityCorrectionEnable api_NonUniformityCorrectionEnable;

        private delegate int IRIP_API_thCamera_StartNonUniformityCorrection(int id, RetCallback callback, IntPtr pUser);
        private IRIP_API_thCamera_StartNonUniformityCorrection api_StartNonUniformityCorrection;

        private delegate int IRIP_API_thCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_thCamera_SelectPalette api_SelectPalette;

        private delegate int IRIP_API_thCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
        private IRIP_API_thCamera_GetSelectPalette api_GetSelectPalette;

        private delegate int IRIP_API_thCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_thCamera_SetPalette api_SetPalette;

        private delegate int IRIP_API_thCamera_SetFilterMode(int id, uint val);
        private IRIP_API_thCamera_SetFilterMode api_SetFilterMode;

        private delegate int IRIP_API_thCamera_GetFilterMode(int id);
        private IRIP_API_thCamera_GetFilterMode api_GetFilterMode;

        private delegate int IRIP_API_thCamera_GetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_thCamera_GetIRIPConfig api_GetIRIPConfig;

        private delegate int IRIP_API_thCamera_SetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_thCamera_SetIRIPConfig api_SetIRIPConfig;

        private delegate int IRIP_API_thCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
        private IRIP_API_thCamera_SetAdjustDirection api_SetAdjustDirection;

        private delegate int IRIP_API_thCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
        private IRIP_API_thCamera_SetMeasurement api_SetMeasurement;

        private delegate int IRIP_API_thCamera_SetTempISO(int id, ref TempISO iso);
        private IRIP_API_thCamera_SetTempISO api_SetTempISO;

        private delegate int IRIP_API_thCamera_GetTempISO(int id, ref TempISO iso);
        private IRIP_API_thCamera_GetTempISO api_GetTempISO;

        private delegate int IRIP_API_thCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_thCamera_SetTempNephogram api_SetTempNephogram;

        private delegate int IRIP_API_thCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_thCamera_GetTempNephogram api_GetTempNephogram;

        private delegate int IRIP_API_thCamera_TempNephogramEnable(int id, bool enable);
        private IRIP_API_thCamera_TempNephogramEnable api_TempNephogramEnable;

        private delegate int IRIP_API_thCamera_TempNephogramOverlay(int id, bool enable);
        private IRIP_API_thCamera_TempNephogramOverlay api_TempNephogramOverlay;

        private delegate int IRIP_API_thCamera_TempNephogramDpi(int id, float dpi);
        private IRIP_API_thCamera_TempNephogramDpi api_TempNephogramDpi;

        private delegate int IRIP_API_thCamera_RefreshData(int id);
        private IRIP_API_thCamera_RefreshData api_RefreshData;

        private delegate int IRIP_API_thCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
        private IRIP_API_thCamera_SetGlobalMDParameters api_SetGlobalMDParameters;

        private delegate int IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
        private IRIP_API_thCamera_EnRefEmissivityTransmittanceUnit api_EnRefEmissivityTransmittanceUnit;

        /// <summary>
        /// <br>可检索TH红外相机的数量（内部常量）</br>
        /// <para>@return 数量</para>
        /// </summary>
        public int GetSearchCamerasMax()
        {
            if (api_GetSearchCamerasMax == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSearchCamerasMax();
        }

        /// <summary>
        /// <br>检索TH红外相机</br>
        /// <para>@param devices</para>
        /// </summary>
        public int SearchCameraDevices(ref thDevices devices)
        {
            if (api_SearchCameraDevices == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SearchCameraDevices(ref devices);
        }

        /// <summary>
        /// <br>设置相机配置文件主目录</br>
        /// <para>@param path 文件路径（不包含序列号目录）</para>
        /// </summary>
        public int SetNucTablesPath(string path)
        {
            if (api_SetNucTablesPath == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetNucTablesPath(IRIP.GetID(), Encoding.UTF8.GetBytes(path));
        }

        /// <summary>
        /// <br>获取相机校准数据（设置相机配置文件主目录并启动后获取，相机返回的切换校准数据组变更后需要重新获取）</br>
        /// <para>@param data 数据</para>
        /// <para>@param length &amp;length 返回实际长度</para>
        /// <para>@param type 0: Calibration_NV, 1: Calibration_LOW, 2: Calibration_MID, 3: Calibration_HIGH //相机自动返回的校准数据序号</para>
        /// <para>@return 0: 已读取，-5：未确定序列号，-6：读取数据失败</para>
        /// </summary>
        public int GetSegmentCalibData(byte[] data, ref int length, int type)
        {
            if (api_GetSegmentCalibData == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSegmentCalibData(IRIP.GetID(), data, ref length, type);
        }

        /// <summary>
        /// <br>从文件读取分段标定数据并设置（设置相机配置文件主目录并启动后获取，相机返回的切换校准数据组变更后需要重新获取）</br>
        /// <para>@param type 0: Calibration_NV, 1: Calibration_LOW, 2: Calibration_MID, 3: Calibration_HIGH //相机自动返回的校准数据序号</para>
        /// <para>@param enable 是否启用修正</para>
        /// <para>@return 0: 启用，-1：不启用，-2：数据长度无效，-3,-4：数据无效，-5：未确定序列号，-6：读取数据失败</para>
        /// </summary>
        public int SetSegmentCalibDataForFile(int type, bool enable)
        {
            if (api_SetSegmentCalibDataForFile == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetSegmentCalibDataForFile(IRIP.GetID(), type, enable);
        }

        /// <summary>
        /// <br>设置对应相机的温度修正数据</br>
        /// <para>@param data 修正数据</para>
        /// <para>@param length 数据长度</para>
        /// <para>@param type 0: 常温段, 1: 中温段</para>
        /// <para>@param enable 是否启用修正</para>
        /// <para>@return 0: 启用，-1：不启用，-2：数据长度无效，-3,-4：数据无效</para>
        /// </summary>
        public int SetSegmentCalibData(byte[] data, int length, int type, bool enable)
        {
            if (api_SetSegmentCalibData == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetSegmentCalibData(IRIP.GetID(), data, length, type, enable);
        }

        /// <summary>
        /// <br>获取标定距离</br>
        /// <para>@param distance 标定距离（米）</para>
        /// <para>@return </para>
        /// </summary>
        public int GetCalibrationDistance(ref float distance)
        {
            if (api_GetCalibrationDistance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetCalibrationDistance(IRIP.GetID(), ref distance);
        }

        /// <summary>
        /// <br>设置默认标定距离(红外相机的标定距离，距离修正的参考值，默认1.0米)</br>
        /// <para>@param distance 标定距离（米）</para>
        /// <para>@return 0: 设置成功</para>
        /// </summary>
        public int SetCalibrationDistance(float distance)
        {
            if (api_SetCalibrationDistance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetCalibrationDistance(IRIP.GetID(), distance);
        }

        /// <summary>
        /// <br>设置相机状态回调</br>
        /// <para>@param states 状态结构体指针，包含相机开启/暂停/断开/校准数据组改变/均匀度校正是否开启/当前校准数据组均匀度校正数据是否存在等。</para>
        /// <para>@param callback 相机状态改变后回调</para>
        /// </summary>
        public int SetStatesCallback(ref thCameraStates states, RetCallback callback, IntPtr pUser)
        {
            if (api_SetStatesCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetStatesCallback(IRIP.GetID(), ref states, callback, pUser);
        }

        /// <summary>
        /// <br>启动相机</br>
        /// <para>@param device 要启动的相机结构体</para>
        /// <para>@param callback 回调函数，包含相机图像的宽度、高度、序列号、硬版本和固件版本</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int ConnectToDevice(ref thDevice device, CameraConnectCallback callback, IntPtr pUser)
        {
            if (api_ConnectToDevice == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_ConnectToDevice(IRIP.GetID(), ref device, callback, pUser);
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
        /// <br>继续启动相机</br>
        /// <para>@return 0|1: 正常启动, -1: 启动失败, -2: 未设置配置文件目录, -3: 没找到配置文件, -4: 缺少配置文件</para>
        /// </summary>
        public int AfterConnect()
        {
            if (api_AfterConnect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_AfterConnect(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取缺少的配置文件</br>
        /// <para>@param data 输出的JSON文本</para>
        /// <para>@param length</para>
        /// <para>@return 长度</para>
        /// </summary>
        public int GetMissingFilesJson(string data, int length)
        {
            if (api_GetMissingFilesJson == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetMissingFilesJson(IRIP.GetID(), Encoding.UTF8.GetBytes(data), length);
        }

        /// <summary>
        /// <br>开始输出数据</br>
        /// </summary>
        public int StartVideo()
        {
            if (api_StartVideo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_StartVideo(IRIP.GetID());
        }

        /// <summary>
        /// <br>停止输出数据</br>
        /// </summary>
        public int StopVideo()
        {
            if (api_StopVideo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_StopVideo(IRIP.GetID());
        }

        /// <summary>
        /// <br>暂停/继续输出数据</br>
        /// <para>@param pause</para>
        /// </summary>
        public int PauseVideo(bool pause)
        {
            if (api_PauseVideo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_PauseVideo(IRIP.GetID(), pause);
        }

        /// <summary>
        /// <br>是否开启均匀度校正（启用后如果均匀度校正文件存在则会应用）</br>
        /// <para>@param enable</para>
        /// </summary>
        public int NonUniformityCorrectionEnable(bool enable)
        {
            if (api_NonUniformityCorrectionEnable == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_NonUniformityCorrectionEnable(IRIP.GetID(), enable);
        }

        /// <summary>
        /// <br>执行均匀度校正，需盖好镜头盖或将相机视野覆盖对准一格温度均匀的靶面</br>
        /// <para>@param callback 回调函数，ret: -4/-3:未初始化，必须在初始化实例和开启相机后执行； 回调当前采集的数据帧数默认25帧数；0/1：采集完毕并开启校正；-1：视频暂停或停止，中断采集，需要开启后再次执行，-2：相机应用的校正数据组已改变，中断采集，需要再次执行；</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int StartNonUniformityCorrection(RetCallback callback, IntPtr pUser)
        {
            if (api_StartNonUniformityCorrection == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_StartNonUniformityCorrection(IRIP.GetID(), callback, pUser);
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
        /// <br>获取图像渲染参数</br>
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
        /// <br>设置测量数据</br>
        /// <br>使用API内部测量工具，支持json文本输入或从磁盘读取json测量模板文件（格式请参考模板文件），</br>
        /// <br>支持全局高低温、点、线、矩形（支持倒角）、圆/椭圆、多边形测量，</br>
        /// <br>结果输出到回调帧的jSON项，设置帧输出项时需设置申请该项内存</br>
        /// <para>@param action JSON/PATH</para>
        /// <para>@param text json文件路径 或 json序列字符串</para>
        /// <para>@return</para>
        /// </summary>
        public int SetMeasurement(string action, string text, MdCallback callback, IntPtr pUser)
        {
            if (api_SetMeasurement == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetMeasurement(IRIP.GetID(), Encoding.UTF8.GetBytes(action), Encoding.UTF8.GetBytes(text), callback, pUser);
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

        /// <summary>
        /// <br>刷新数据（可在暂停时修改测量数据后刷新）</br>
        /// <para>@return</para>
        /// </summary>
        public int RefreshData()
        {
            if (api_RefreshData == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_RefreshData(IRIP.GetID());
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
        /// <br>原始温度应用发射率修正并转换温度单位（使用全局参数）</br>
        /// <para>@param temp 输入温度</para>
        /// <para>@return 输出温度</para>
        /// </summary>
        public int EnRefEmissivityTransmittanceUnit(int temp)
        {
            if (api_EnRefEmissivityTransmittanceUnit == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_EnRefEmissivityTransmittanceUnit(IRIP.GetID(), temp);
        }

    }

}
