using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>红外相机API</br>
    /// </summary>
    public class IRIP_API_IRCamera
    {
        IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
            api_searchUsbCameraDevices = _IRIP_API_searchUsbCameraDevices;

            api_SetNucTablesPath = _IRIP_API_irCamera_SetNucTablesPath;
    api_GetSegmentCalibData = _IRIP_API_irCamera_GetSegmentCalibData;
    api_SetSegmentCalibDataForFile = _IRIP_API_irCamera_SetSegmentCalibDataForFile;
    api_SetSegmentCalibData = _IRIP_API_irCamera_SetSegmentCalibData;
    api_GetCalibrationDistance = _IRIP_API_irCamera_GetCalibrationDistance;
    api_SetCalibrationDistance = _IRIP_API_irCamera_SetCalibrationDistance;
    api_Start = _IRIP_API_irCamera_Start;
    api_Run = _IRIP_API_irCamera_Run;
    api_Stop = _IRIP_API_irCamera_Stop;
    api_Close = _IRIP_API_irCamera_Close;
    api_SetHotKeyCallback = _IRIP_API_irCamera_SetHotKeyCallback;
    api_SetRangFindingCallback = _IRIP_API_irCamera_SetRangFindingCallback;
    api_SetRelayControlCallback = _IRIP_API_irCamera_SetRelayControlCallback;
    api_SetBatteryStatusCallback = _IRIP_API_irCamera_SetBatteryStatusCallback;
    api_RelayControl = _IRIP_API_irCamera_RelayControl;
    api_RangFinding = _IRIP_API_irCamera_RangFinding;
    api_GetBatteryStatus = _IRIP_API_irCamera_GetBatteryStatus;
    api_SetFocusPosiCallback = _IRIP_API_irCamera_SetFocusPosiCallback;
    api_GetFocusErrCode = _IRIP_API_irCamera_GetFocusErrCode;
    api_GetFocusMaxPosi = _IRIP_API_irCamera_GetFocusMaxPosi;
    api_GetFocusCurrentPosi = _IRIP_API_irCamera_GetFocusCurrentPosi;
    api_SetFocusMode = _IRIP_API_irCamera_SetFocusMode;
    api_NearFarFocus = _IRIP_API_irCamera_NearFarFocus;
    api_AutoFocus = _IRIP_API_irCamera_AutoFocus;
    api_FindFocus = _IRIP_API_irCamera_FindFocus;
    api_GetDeviceInfo = _IRIP_API_irCamera_GetDeviceInfo;
    api_GetTempType = _IRIP_API_irCamera_GetTempType;
    api_SetTempType = _IRIP_API_irCamera_SetTempType;
    api_GetOutputType = _IRIP_API_irCamera_GetOutputType;
    api_SetOutputType = _IRIP_API_irCamera_SetOutputType;
    api_Refresh = _IRIP_API_irCamera_Refresh;
    api_GetAutoRefreshTime = _IRIP_API_irCamera_GetAutoRefreshTime;
    api_SetAutoRefreshTime = _IRIP_API_irCamera_SetAutoRefreshTime;
    api_GetDropFrameNum = _IRIP_API_irCamera_GetDropFrameNum;
    api_SetDropFrameNum = _IRIP_API_irCamera_SetDropFrameNum;
    api_GetOutMaxFps = _IRIP_API_irCamera_GetOutMaxFps;
    api_SetOutMaxFps = _IRIP_API_irCamera_SetOutMaxFps;
    api_GetReRunNumMax = _IRIP_API_irCamera_GetReRunNumMax;
    api_SetReRunNumMax = _IRIP_API_irCamera_SetReRunNumMax;
    api_GetDeviceTempCorrection = _IRIP_API_irCamera_GetDeviceTempCorrection;
    api_SetDeviceTempCorrection = _IRIP_API_irCamera_SetDeviceTempCorrection;
    api_SetFrameCallback = _IRIP_API_irCamera_SetFrameCallback;
    api_SelectPalette = _IRIP_API_irCamera_SelectPalette;
    api_GetSelectPalette = _IRIP_API_irCamera_GetSelectPalette;
    api_SetPalette = _IRIP_API_irCamera_SetPalette;
    api_SetFilterMode = _IRIP_API_irCamera_SetFilterMode;
    api_GetFilterMode = _IRIP_API_irCamera_GetFilterMode;
    api_GetIRIPConfig = _IRIP_API_irCamera_GetIRIPConfig;
    api_SetIRIPConfig = _IRIP_API_irCamera_SetIRIPConfig;
    api_SetAdjustDirection = _IRIP_API_irCamera_SetAdjustDirection;
    api_SetMeasurement = _IRIP_API_irCamera_SetMeasurement;
    api_SetTempISO = _IRIP_API_irCamera_SetTempISO;
    api_GetTempISO = _IRIP_API_irCamera_GetTempISO;
    api_SetTempNephogram = _IRIP_API_irCamera_SetTempNephogram;
    api_GetTempNephogram = _IRIP_API_irCamera_GetTempNephogram;
    api_TempNephogramEnable = _IRIP_API_irCamera_TempNephogramEnable;
    api_TempNephogramOverlay = _IRIP_API_irCamera_TempNephogramOverlay;
    api_TempNephogramDpi = _IRIP_API_irCamera_TempNephogramDpi;
    api_RefreshData = _IRIP_API_irCamera_RefreshData;
    api_SetGlobalMDParameters = _IRIP_API_irCamera_SetGlobalMDParameters;
    api_EnRefEmissivityTransmittanceUnit = _IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit;
    api_SetNUCStatesCallback = _IRIP_API_irCamera_SetNUCStatesCallback;
    api_NonUniformityCorrectionEnable = _IRIP_API_irCamera_NonUniformityCorrectionEnable;
    api_StartNonUniformityCorrection = _IRIP_API_irCamera_StartNonUniformityCorrection;
    api_SetExtendCMDCallback = _IRIP_API_irCamera_SetExtendCMDCallback;
    api_ExtendCMD = _IRIP_API_irCamera_ExtendCMD;
    api_SetExtendRangFindingDevice = _IRIP_API_irCamera_SetExtendRangFindingDevice;
#else
            api_searchUsbCameraDevices = (IRIP_API_searchUsbCameraDevices)dll.Invoke("IRIP_API_searchUsbCameraDevices", typeof(IRIP_API_searchUsbCameraDevices));

            api_SetNucTablesPath = (IRIP_API_irCamera_SetNucTablesPath)dll.Invoke("IRIP_API_irCamera_SetNucTablesPath", typeof(IRIP_API_irCamera_SetNucTablesPath));
            api_GetSegmentCalibData = (IRIP_API_irCamera_GetSegmentCalibData)dll.Invoke("IRIP_API_irCamera_GetSegmentCalibData", typeof(IRIP_API_irCamera_GetSegmentCalibData));
            api_SetSegmentCalibDataForFile = (IRIP_API_irCamera_SetSegmentCalibDataForFile)dll.Invoke("IRIP_API_irCamera_SetSegmentCalibDataForFile", typeof(IRIP_API_irCamera_SetSegmentCalibDataForFile));
            api_SetSegmentCalibData = (IRIP_API_irCamera_SetSegmentCalibData)dll.Invoke("IRIP_API_irCamera_SetSegmentCalibData", typeof(IRIP_API_irCamera_SetSegmentCalibData));
            api_GetCalibrationDistance = (IRIP_API_irCamera_GetCalibrationDistance)dll.Invoke("IRIP_API_irCamera_GetCalibrationDistance", typeof(IRIP_API_irCamera_GetCalibrationDistance));
            api_SetCalibrationDistance = (IRIP_API_irCamera_SetCalibrationDistance)dll.Invoke("IRIP_API_irCamera_SetCalibrationDistance", typeof(IRIP_API_irCamera_SetCalibrationDistance));
            api_Start = (IRIP_API_irCamera_Start)dll.Invoke("IRIP_API_irCamera_Start", typeof(IRIP_API_irCamera_Start));
            api_Run = (IRIP_API_irCamera_Run)dll.Invoke("IRIP_API_irCamera_Run", typeof(IRIP_API_irCamera_Run));
            api_Stop = (IRIP_API_irCamera_Stop)dll.Invoke("IRIP_API_irCamera_Stop", typeof(IRIP_API_irCamera_Stop));
            api_Close = (IRIP_API_irCamera_Close)dll.Invoke("IRIP_API_irCamera_Close", typeof(IRIP_API_irCamera_Close));
            api_SetHotKeyCallback = (IRIP_API_irCamera_SetHotKeyCallback)dll.Invoke("IRIP_API_irCamera_SetHotKeyCallback", typeof(IRIP_API_irCamera_SetHotKeyCallback));
            api_SetRangFindingCallback = (IRIP_API_irCamera_SetRangFindingCallback)dll.Invoke("IRIP_API_irCamera_SetRangFindingCallback", typeof(IRIP_API_irCamera_SetRangFindingCallback));
            api_SetRelayControlCallback = (IRIP_API_irCamera_SetRelayControlCallback)dll.Invoke("IRIP_API_irCamera_SetRelayControlCallback", typeof(IRIP_API_irCamera_SetRelayControlCallback));
            api_SetBatteryStatusCallback = (IRIP_API_irCamera_SetBatteryStatusCallback)dll.Invoke("IRIP_API_irCamera_SetBatteryStatusCallback", typeof(IRIP_API_irCamera_SetBatteryStatusCallback));
            api_RelayControl = (IRIP_API_irCamera_RelayControl)dll.Invoke("IRIP_API_irCamera_RelayControl", typeof(IRIP_API_irCamera_RelayControl));
            api_RangFinding = (IRIP_API_irCamera_RangFinding)dll.Invoke("IRIP_API_irCamera_RangFinding", typeof(IRIP_API_irCamera_RangFinding));
            api_GetBatteryStatus = (IRIP_API_irCamera_GetBatteryStatus)dll.Invoke("IRIP_API_irCamera_GetBatteryStatus", typeof(IRIP_API_irCamera_GetBatteryStatus));
            api_SetFocusPosiCallback = (IRIP_API_irCamera_SetFocusPosiCallback)dll.Invoke("IRIP_API_irCamera_SetFocusPosiCallback", typeof(IRIP_API_irCamera_SetFocusPosiCallback));
            api_GetFocusErrCode = (IRIP_API_irCamera_GetFocusErrCode)dll.Invoke("IRIP_API_irCamera_GetFocusErrCode", typeof(IRIP_API_irCamera_GetFocusErrCode));
            api_GetFocusMaxPosi = (IRIP_API_irCamera_GetFocusMaxPosi)dll.Invoke("IRIP_API_irCamera_GetFocusMaxPosi", typeof(IRIP_API_irCamera_GetFocusMaxPosi));
            api_GetFocusCurrentPosi = (IRIP_API_irCamera_GetFocusCurrentPosi)dll.Invoke("IRIP_API_irCamera_GetFocusCurrentPosi", typeof(IRIP_API_irCamera_GetFocusCurrentPosi));
            api_SetFocusMode = (IRIP_API_irCamera_SetFocusMode)dll.Invoke("IRIP_API_irCamera_SetFocusMode", typeof(IRIP_API_irCamera_SetFocusMode));
            api_NearFarFocus = (IRIP_API_irCamera_NearFarFocus)dll.Invoke("IRIP_API_irCamera_NearFarFocus", typeof(IRIP_API_irCamera_NearFarFocus));
            api_AutoFocus = (IRIP_API_irCamera_AutoFocus)dll.Invoke("IRIP_API_irCamera_AutoFocus", typeof(IRIP_API_irCamera_AutoFocus));
            api_FindFocus = (IRIP_API_irCamera_FindFocus)dll.Invoke("IRIP_API_irCamera_FindFocus", typeof(IRIP_API_irCamera_FindFocus));
            api_GetDeviceInfo = (IRIP_API_irCamera_GetDeviceInfo)dll.Invoke("IRIP_API_irCamera_GetDeviceInfo", typeof(IRIP_API_irCamera_GetDeviceInfo));
            api_GetTempType = (IRIP_API_irCamera_GetTempType)dll.Invoke("IRIP_API_irCamera_GetTempType", typeof(IRIP_API_irCamera_GetTempType));
            api_SetTempType = (IRIP_API_irCamera_SetTempType)dll.Invoke("IRIP_API_irCamera_SetTempType", typeof(IRIP_API_irCamera_SetTempType));
            api_GetOutputType = (IRIP_API_irCamera_GetOutputType)dll.Invoke("IRIP_API_irCamera_GetOutputType", typeof(IRIP_API_irCamera_GetOutputType));
            api_SetOutputType = (IRIP_API_irCamera_SetOutputType)dll.Invoke("IRIP_API_irCamera_SetOutputType", typeof(IRIP_API_irCamera_SetOutputType));
            api_Refresh = (IRIP_API_irCamera_Refresh)dll.Invoke("IRIP_API_irCamera_Refresh", typeof(IRIP_API_irCamera_Refresh));
            api_GetAutoRefreshTime = (IRIP_API_irCamera_GetAutoRefreshTime)dll.Invoke("IRIP_API_irCamera_GetAutoRefreshTime", typeof(IRIP_API_irCamera_GetAutoRefreshTime));
            api_SetAutoRefreshTime = (IRIP_API_irCamera_SetAutoRefreshTime)dll.Invoke("IRIP_API_irCamera_SetAutoRefreshTime", typeof(IRIP_API_irCamera_SetAutoRefreshTime));
            api_GetDropFrameNum = (IRIP_API_irCamera_GetDropFrameNum)dll.Invoke("IRIP_API_irCamera_GetDropFrameNum", typeof(IRIP_API_irCamera_GetDropFrameNum));
            api_SetDropFrameNum = (IRIP_API_irCamera_SetDropFrameNum)dll.Invoke("IRIP_API_irCamera_SetDropFrameNum", typeof(IRIP_API_irCamera_SetDropFrameNum));
            api_GetOutMaxFps = (IRIP_API_irCamera_GetOutMaxFps)dll.Invoke("IRIP_API_irCamera_GetOutMaxFps", typeof(IRIP_API_irCamera_GetOutMaxFps));
            api_SetOutMaxFps = (IRIP_API_irCamera_SetOutMaxFps)dll.Invoke("IRIP_API_irCamera_SetOutMaxFps", typeof(IRIP_API_irCamera_SetOutMaxFps));
            api_GetReRunNumMax = (IRIP_API_irCamera_GetReRunNumMax)dll.Invoke("IRIP_API_irCamera_GetReRunNumMax", typeof(IRIP_API_irCamera_GetReRunNumMax));
            api_SetReRunNumMax = (IRIP_API_irCamera_SetReRunNumMax)dll.Invoke("IRIP_API_irCamera_SetReRunNumMax", typeof(IRIP_API_irCamera_SetReRunNumMax));
            api_GetDeviceTempCorrection = (IRIP_API_irCamera_GetDeviceTempCorrection)dll.Invoke("IRIP_API_irCamera_GetDeviceTempCorrection", typeof(IRIP_API_irCamera_GetDeviceTempCorrection));
            api_SetDeviceTempCorrection = (IRIP_API_irCamera_SetDeviceTempCorrection)dll.Invoke("IRIP_API_irCamera_SetDeviceTempCorrection", typeof(IRIP_API_irCamera_SetDeviceTempCorrection));
            api_SetFrameCallback = (IRIP_API_irCamera_SetFrameCallback)dll.Invoke("IRIP_API_irCamera_SetFrameCallback", typeof(IRIP_API_irCamera_SetFrameCallback));
            api_SelectPalette = (IRIP_API_irCamera_SelectPalette)dll.Invoke("IRIP_API_irCamera_SelectPalette", typeof(IRIP_API_irCamera_SelectPalette));
            api_GetSelectPalette = (IRIP_API_irCamera_GetSelectPalette)dll.Invoke("IRIP_API_irCamera_GetSelectPalette", typeof(IRIP_API_irCamera_GetSelectPalette));
            api_SetPalette = (IRIP_API_irCamera_SetPalette)dll.Invoke("IRIP_API_irCamera_SetPalette", typeof(IRIP_API_irCamera_SetPalette));
            api_SetFilterMode = (IRIP_API_irCamera_SetFilterMode)dll.Invoke("IRIP_API_irCamera_SetFilterMode", typeof(IRIP_API_irCamera_SetFilterMode));
            api_GetFilterMode = (IRIP_API_irCamera_GetFilterMode)dll.Invoke("IRIP_API_irCamera_GetFilterMode", typeof(IRIP_API_irCamera_GetFilterMode));
            api_GetIRIPConfig = (IRIP_API_irCamera_GetIRIPConfig)dll.Invoke("IRIP_API_irCamera_GetIRIPConfig", typeof(IRIP_API_irCamera_GetIRIPConfig));
            api_SetIRIPConfig = (IRIP_API_irCamera_SetIRIPConfig)dll.Invoke("IRIP_API_irCamera_SetIRIPConfig", typeof(IRIP_API_irCamera_SetIRIPConfig));
            api_SetAdjustDirection = (IRIP_API_irCamera_SetAdjustDirection)dll.Invoke("IRIP_API_irCamera_SetAdjustDirection", typeof(IRIP_API_irCamera_SetAdjustDirection));
            api_SetMeasurement = (IRIP_API_irCamera_SetMeasurement)dll.Invoke("IRIP_API_irCamera_SetMeasurement", typeof(IRIP_API_irCamera_SetMeasurement));
            api_SetTempISO = (IRIP_API_irCamera_SetTempISO)dll.Invoke("IRIP_API_irCamera_SetTempISO", typeof(IRIP_API_irCamera_SetTempISO));
            api_GetTempISO = (IRIP_API_irCamera_GetTempISO)dll.Invoke("IRIP_API_irCamera_GetTempISO", typeof(IRIP_API_irCamera_GetTempISO));
            api_SetTempNephogram = (IRIP_API_irCamera_SetTempNephogram)dll.Invoke("IRIP_API_irCamera_SetTempNephogram", typeof(IRIP_API_irCamera_SetTempNephogram));
            api_GetTempNephogram = (IRIP_API_irCamera_GetTempNephogram)dll.Invoke("IRIP_API_irCamera_GetTempNephogram", typeof(IRIP_API_irCamera_GetTempNephogram));
            api_TempNephogramEnable = (IRIP_API_irCamera_TempNephogramEnable)dll.Invoke("IRIP_API_irCamera_TempNephogramEnable", typeof(IRIP_API_irCamera_TempNephogramEnable));
            api_TempNephogramOverlay = (IRIP_API_irCamera_TempNephogramOverlay)dll.Invoke("IRIP_API_irCamera_TempNephogramOverlay", typeof(IRIP_API_irCamera_TempNephogramOverlay));
            api_TempNephogramDpi = (IRIP_API_irCamera_TempNephogramDpi)dll.Invoke("IRIP_API_irCamera_TempNephogramDpi", typeof(IRIP_API_irCamera_TempNephogramDpi));
            api_RefreshData = (IRIP_API_irCamera_RefreshData)dll.Invoke("IRIP_API_irCamera_RefreshData", typeof(IRIP_API_irCamera_RefreshData));
            api_SetGlobalMDParameters = (IRIP_API_irCamera_SetGlobalMDParameters)dll.Invoke("IRIP_API_irCamera_SetGlobalMDParameters", typeof(IRIP_API_irCamera_SetGlobalMDParameters));
            api_EnRefEmissivityTransmittanceUnit = (IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit)dll.Invoke("IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit", typeof(IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit));
            api_SetNUCStatesCallback = (IRIP_API_irCamera_SetNUCStatesCallback)dll.Invoke("IRIP_API_irCamera_SetNUCStatesCallback", typeof(IRIP_API_irCamera_SetNUCStatesCallback));
            api_NonUniformityCorrectionEnable = (IRIP_API_irCamera_NonUniformityCorrectionEnable)dll.Invoke("IRIP_API_irCamera_NonUniformityCorrectionEnable", typeof(IRIP_API_irCamera_NonUniformityCorrectionEnable));
            api_StartNonUniformityCorrection = (IRIP_API_irCamera_StartNonUniformityCorrection)dll.Invoke("IRIP_API_irCamera_StartNonUniformityCorrection", typeof(IRIP_API_irCamera_StartNonUniformityCorrection));
            api_SetExtendCMDCallback = (IRIP_API_irCamera_SetExtendCMDCallback)dll.Invoke("IRIP_API_irCamera_SetExtendCMDCallback", typeof(IRIP_API_irCamera_SetExtendCMDCallback));
            api_ExtendCMD = (IRIP_API_irCamera_ExtendCMD)dll.Invoke("IRIP_API_irCamera_ExtendCMD", typeof(IRIP_API_irCamera_ExtendCMD));
            api_SetExtendRangFindingDevice = (IRIP_API_irCamera_SetExtendRangFindingDevice)dll.Invoke("IRIP_API_irCamera_SetExtendRangFindingDevice", typeof(IRIP_API_irCamera_SetExtendRangFindingDevice));
#endif
        }

#if _X86
        [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_searchUsbCameraDevices", CallingConvention = CallingConvention.StdCall)]
        static private extern int _IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);

        [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetNucTablesPath", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetNucTablesPath(int id, byte[] path, byte[] sn);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetSegmentCalibDataForFile", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetCalibrationDistance(int id, ref float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetCalibrationDistance(int id, float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_Start", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_Start(int id, ref CameraDevice device);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_Run", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_Run(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_Stop", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_Stop(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_Close", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_Close(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetHotKeyCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetHotKeyCallback(int id, RetCallbackPlus callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetRangFindingCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetRangFindingCallback(int id, RetCallbackPlus callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetRelayControlCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetRelayControlCallback(int id, RetCallbackPlus callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetBatteryStatusCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetBatteryStatusCallback(int id, RetCallbackPlus callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_RelayControl", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_RelayControl(int id, ushort port, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_RangFinding", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_RangFinding(int id, int command);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetBatteryStatus", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetBatteryStatus(int id, int command);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetFocusPosiCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetFocusPosiCallback(int id, RetCallbackPlus callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetFocusErrCode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetFocusErrCode(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetFocusMaxPosi", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetFocusMaxPosi(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetFocusCurrentPosi", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetFocusCurrentPosi(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetFocusMode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetFocusMode(int id, int mode);

  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_NearFarFocus", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_NearFarFocus(int id, int command, int posi);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_AutoFocus", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_AutoFocus(int id, ref CInt32Rect rect);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_FindFocus", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_FindFocus(int id, int command, ref CInt32Point range, ref CInt32Rect rect);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetDeviceInfo", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetDeviceInfo(int id, ref CameraDevice device);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetTempType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetTempType(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetTempType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetTempType(int id, uint type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetOutputType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetOutputType(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetOutputType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetOutputType(int id, uint type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_Refresh", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_Refresh(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetAutoRefreshTime", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetAutoRefreshTime(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetAutoRefreshTime", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetAutoRefreshTime(int id, uint time);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetDropFrameNum", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetDropFrameNum(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetDropFrameNum", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetDropFrameNum(int id, int num);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetOutMaxFps(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetOutMaxFps(int id, int fps);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetReRunNumMax", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetReRunNumMax(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetReRunNumMax", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetReRunNumMax(int id, int num);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetDeviceTempCorrection", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetDeviceTempCorrection(int id, ref float val);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetDeviceTempCorrection", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetDeviceTempCorrection(int id, float val);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetFrameCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SelectPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetSelectPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetFilterMode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetFilterMode(int id, uint val);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetFilterMode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetFilterMode(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetAdjustDirection", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetMeasurement", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetTempISO", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetTempISO(int id, ref TempISO iso);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetTempISO", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetTempISO(int id, ref TempISO iso);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetTempNephogram", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_GetTempNephogram", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_TempNephogramEnable", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_TempNephogramEnable(int id, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_TempNephogramOverlay", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_TempNephogramOverlay(int id, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_TempNephogramDpi", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_TempNephogramDpi(int id, float dpi);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_RefreshData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_RefreshData(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetGlobalMDParameters", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetNUCStatesCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetNUCStatesCallback(int id, IntPtr states, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_NonUniformityCorrectionEnable", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_NonUniformityCorrectionEnable(int id, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_StartNonUniformityCorrection", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_StartNonUniformityCorrection(int id, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetExtendCMDCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetExtendCMDCallback(int id, MsgCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_ExtendCMD", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_ExtendCMD(int id, int type, byte[] msg);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irCamera_SetExtendRangFindingDevice", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irCamera_SetExtendRangFindingDevice(int id, int type, IntPtr devStruct, uint devSize);
#endif

        private delegate int IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);
        private IRIP_API_searchUsbCameraDevices api_searchUsbCameraDevices;

        private delegate int IRIP_API_irCamera_SetNucTablesPath(int id, byte[] path, byte[] sn);
        private IRIP_API_irCamera_SetNucTablesPath api_SetNucTablesPath;

        private delegate int IRIP_API_irCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
        private IRIP_API_irCamera_GetSegmentCalibData api_GetSegmentCalibData;

        private delegate int IRIP_API_irCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
        private IRIP_API_irCamera_SetSegmentCalibDataForFile api_SetSegmentCalibDataForFile;

        private delegate int IRIP_API_irCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
        private IRIP_API_irCamera_SetSegmentCalibData api_SetSegmentCalibData;

        private delegate int IRIP_API_irCamera_GetCalibrationDistance(int id, ref float distance);
        private IRIP_API_irCamera_GetCalibrationDistance api_GetCalibrationDistance;

        private delegate int IRIP_API_irCamera_SetCalibrationDistance(int id, float distance);
        private IRIP_API_irCamera_SetCalibrationDistance api_SetCalibrationDistance;

        private delegate int IRIP_API_irCamera_Start(int id, ref CameraDevice device);
        private IRIP_API_irCamera_Start api_Start;

        private delegate int IRIP_API_irCamera_Run(int id);
        private IRIP_API_irCamera_Run api_Run;

        private delegate int IRIP_API_irCamera_Stop(int id);
        private IRIP_API_irCamera_Stop api_Stop;

        private delegate int IRIP_API_irCamera_Close(int id);
        private IRIP_API_irCamera_Close api_Close;

        private delegate int IRIP_API_irCamera_SetHotKeyCallback(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irCamera_SetHotKeyCallback api_SetHotKeyCallback;

        private delegate int IRIP_API_irCamera_SetRangFindingCallback(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irCamera_SetRangFindingCallback api_SetRangFindingCallback;

        private delegate int IRIP_API_irCamera_SetRelayControlCallback(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irCamera_SetRelayControlCallback api_SetRelayControlCallback;

        private delegate int IRIP_API_irCamera_SetBatteryStatusCallback(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irCamera_SetBatteryStatusCallback api_SetBatteryStatusCallback;

        private delegate int IRIP_API_irCamera_RelayControl(int id, ushort port, bool enable);
        private IRIP_API_irCamera_RelayControl api_RelayControl;

        private delegate int IRIP_API_irCamera_RangFinding(int id, int command);
        private IRIP_API_irCamera_RangFinding api_RangFinding;

        private delegate int IRIP_API_irCamera_GetBatteryStatus(int id, int command);
        private IRIP_API_irCamera_GetBatteryStatus api_GetBatteryStatus;

        private delegate int IRIP_API_irCamera_SetFocusPosiCallback(int id, RetCallbackPlus callback, IntPtr pUser);
        private IRIP_API_irCamera_SetFocusPosiCallback api_SetFocusPosiCallback;

        private delegate int IRIP_API_irCamera_GetFocusErrCode(int id);
        private IRIP_API_irCamera_GetFocusErrCode api_GetFocusErrCode;

        private delegate int IRIP_API_irCamera_GetFocusMaxPosi(int id);
        private IRIP_API_irCamera_GetFocusMaxPosi api_GetFocusMaxPosi;

        private delegate int IRIP_API_irCamera_GetFocusCurrentPosi(int id);
        private IRIP_API_irCamera_GetFocusCurrentPosi api_GetFocusCurrentPosi;

        private delegate int IRIP_API_irCamera_SetFocusMode(int id, int mode);
        private IRIP_API_irCamera_SetFocusMode api_SetFocusMode;

        private delegate int IRIP_API_irCamera_NearFarFocus(int id, int command, int posi);
        private IRIP_API_irCamera_NearFarFocus api_NearFarFocus;

        private delegate int IRIP_API_irCamera_AutoFocus(int id, ref CInt32Rect rect);
        private IRIP_API_irCamera_AutoFocus api_AutoFocus;

        private delegate int IRIP_API_irCamera_FindFocus(int id, int command, ref CInt32Point range, ref CInt32Rect rect);
        private IRIP_API_irCamera_FindFocus api_FindFocus;

        private delegate int IRIP_API_irCamera_GetDeviceInfo(int id, ref CameraDevice device);
        private IRIP_API_irCamera_GetDeviceInfo api_GetDeviceInfo;

        private delegate int IRIP_API_irCamera_GetTempType(int id);
        private IRIP_API_irCamera_GetTempType api_GetTempType;

        private delegate int IRIP_API_irCamera_SetTempType(int id, uint type);
        private IRIP_API_irCamera_SetTempType api_SetTempType;

        private delegate int IRIP_API_irCamera_GetOutputType(int id);
        private IRIP_API_irCamera_GetOutputType api_GetOutputType;

        private delegate int IRIP_API_irCamera_SetOutputType(int id, uint type);
        private IRIP_API_irCamera_SetOutputType api_SetOutputType;

        private delegate int IRIP_API_irCamera_Refresh(int id);
        private IRIP_API_irCamera_Refresh api_Refresh;

        private delegate int IRIP_API_irCamera_GetAutoRefreshTime(int id);
        private IRIP_API_irCamera_GetAutoRefreshTime api_GetAutoRefreshTime;

        private delegate int IRIP_API_irCamera_SetAutoRefreshTime(int id, uint time);
        private IRIP_API_irCamera_SetAutoRefreshTime api_SetAutoRefreshTime;

        private delegate int IRIP_API_irCamera_GetDropFrameNum(int id);
        private IRIP_API_irCamera_GetDropFrameNum api_GetDropFrameNum;

        private delegate int IRIP_API_irCamera_SetDropFrameNum(int id, int num);
        private IRIP_API_irCamera_SetDropFrameNum api_SetDropFrameNum;

        private delegate int IRIP_API_irCamera_GetOutMaxFps(int id);
        private IRIP_API_irCamera_GetOutMaxFps api_GetOutMaxFps;

        private delegate int IRIP_API_irCamera_SetOutMaxFps(int id, int fps);
        private IRIP_API_irCamera_SetOutMaxFps api_SetOutMaxFps;

        private delegate int IRIP_API_irCamera_GetReRunNumMax(int id);
        private IRIP_API_irCamera_GetReRunNumMax api_GetReRunNumMax;

        private delegate int IRIP_API_irCamera_SetReRunNumMax(int id, int num);
        private IRIP_API_irCamera_SetReRunNumMax api_SetReRunNumMax;

        private delegate int IRIP_API_irCamera_GetDeviceTempCorrection(int id, ref float val);
        private IRIP_API_irCamera_GetDeviceTempCorrection api_GetDeviceTempCorrection;

        private delegate int IRIP_API_irCamera_SetDeviceTempCorrection(int id, float val);
        private IRIP_API_irCamera_SetDeviceTempCorrection api_SetDeviceTempCorrection;

        private delegate int IRIP_API_irCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_irCamera_SetFrameCallback api_SetFrameCallback;

        private delegate int IRIP_API_irCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_irCamera_SelectPalette api_SelectPalette;

        private delegate int IRIP_API_irCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
        private IRIP_API_irCamera_GetSelectPalette api_GetSelectPalette;

        private delegate int IRIP_API_irCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_irCamera_SetPalette api_SetPalette;

        private delegate int IRIP_API_irCamera_SetFilterMode(int id, uint val);
        private IRIP_API_irCamera_SetFilterMode api_SetFilterMode;

        private delegate int IRIP_API_irCamera_GetFilterMode(int id);
        private IRIP_API_irCamera_GetFilterMode api_GetFilterMode;

        private delegate int IRIP_API_irCamera_GetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_irCamera_GetIRIPConfig api_GetIRIPConfig;

        private delegate int IRIP_API_irCamera_SetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_irCamera_SetIRIPConfig api_SetIRIPConfig;

        private delegate int IRIP_API_irCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
        private IRIP_API_irCamera_SetAdjustDirection api_SetAdjustDirection;

        private delegate int IRIP_API_irCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
        private IRIP_API_irCamera_SetMeasurement api_SetMeasurement;

        private delegate int IRIP_API_irCamera_SetTempISO(int id, ref TempISO iso);
        private IRIP_API_irCamera_SetTempISO api_SetTempISO;

        private delegate int IRIP_API_irCamera_GetTempISO(int id, ref TempISO iso);
        private IRIP_API_irCamera_GetTempISO api_GetTempISO;

        private delegate int IRIP_API_irCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_irCamera_SetTempNephogram api_SetTempNephogram;

        private delegate int IRIP_API_irCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_irCamera_GetTempNephogram api_GetTempNephogram;

        private delegate int IRIP_API_irCamera_TempNephogramEnable(int id, bool enable);
        private IRIP_API_irCamera_TempNephogramEnable api_TempNephogramEnable;

        private delegate int IRIP_API_irCamera_TempNephogramOverlay(int id, bool enable);
        private IRIP_API_irCamera_TempNephogramOverlay api_TempNephogramOverlay;

        private delegate int IRIP_API_irCamera_TempNephogramDpi(int id, float dpi);
        private IRIP_API_irCamera_TempNephogramDpi api_TempNephogramDpi;

        private delegate int IRIP_API_irCamera_RefreshData(int id);
        private IRIP_API_irCamera_RefreshData api_RefreshData;

        private delegate int IRIP_API_irCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
        private IRIP_API_irCamera_SetGlobalMDParameters api_SetGlobalMDParameters;

        private delegate int IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
        private IRIP_API_irCamera_EnRefEmissivityTransmittanceUnit api_EnRefEmissivityTransmittanceUnit;

        private delegate int IRIP_API_irCamera_SetNUCStatesCallback(int id, IntPtr states, RetCallback callback, IntPtr pUser);
        private IRIP_API_irCamera_SetNUCStatesCallback api_SetNUCStatesCallback;

        private delegate int IRIP_API_irCamera_NonUniformityCorrectionEnable(int id, bool enable);
        private IRIP_API_irCamera_NonUniformityCorrectionEnable api_NonUniformityCorrectionEnable;

        private delegate int IRIP_API_irCamera_StartNonUniformityCorrection(int id, RetCallback callback, IntPtr pUser);
        private IRIP_API_irCamera_StartNonUniformityCorrection api_StartNonUniformityCorrection;

        private delegate int IRIP_API_irCamera_SetExtendCMDCallback(int id, MsgCallback callback, IntPtr pUser);
        private IRIP_API_irCamera_SetExtendCMDCallback api_SetExtendCMDCallback;

        private delegate int IRIP_API_irCamera_ExtendCMD(int id, int type, byte[] msg);
        private IRIP_API_irCamera_ExtendCMD api_ExtendCMD;

        private delegate int IRIP_API_irCamera_SetExtendRangFindingDevice(int id, int type, IntPtr devStruct, uint devSize);
        private IRIP_API_irCamera_SetExtendRangFindingDevice api_SetExtendRangFindingDevice;

        /// <summary>
        /// <br>检索USB相机</br>
        /// <para>@param devices 结构体CameraDevices</para>
        /// <para>@return</para>
        /// <summary>
        public int SearchCameraDevices(ref CameraDevices devices)
        {
            if (api_searchUsbCameraDevices == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }

            devices.vid = 0x1514;
            devices.pid = 0x0001;

            return api_searchUsbCameraDevices(ref devices);
        }

        /// <summary>
        /// <br>设置相机配置文件主目录</br>
        /// <para>@param path 文件路径（不包含序列号目录）</para>
        /// <para>@param sn 序列号目录名称，如不指定默认使用内部检测到的序列号</para>
        /// </summary>
        public int SetNucTablesPath(string path, string sn = null)
        {
            if (api_SetNucTablesPath == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetNucTablesPath(IRIP.GetID(), Encoding.UTF8.GetBytes(path), sn == null ? null : Encoding.UTF8.GetBytes(sn));
        }

        /// <summary>
        /// <br>获取相机校准数据（设置相机配置文件主目录并启动后获取，常温段/宽温段切换后需要重新获取）</br>
        /// <para>@param data 数据</para>
        /// <para>@param length &amp;length 返回实际长度</para>
        /// <para>@param type 0: 常温段, 1: 中温段</para>
        /// <para>@return 0, 1: 已读取，-5：未确定序列号，-6, -7：读取数据失败</para>
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
        /// <br>从文件读取分段标定数据并设置（设置相机配置文件主目录并启动后获取，常温段/宽温段切换后需要重新获取）</br>
        /// <para>@param type 0: 常温段, 1: 中温段</para>
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
        /// <br>启动相机，并返回分辨率</br>
        /// <para>@param device 要启动的相机结构体，启动成功返回包含应用的实际分辨率</para>
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
        /// <br>启动相机并获得分辨率后，开始输出数据（如相机已被占用，则无法输出并回调数据）</br>
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
        /// <br>热键回调设置</br>
        /// <para>@param callback 回调函数，ret为为热键ID, val为热键状态</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetHotKeyCallback(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SetHotKeyCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetHotKeyCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>测距回调设置</br>
        /// <para>@param callback 回调函数</para>
        /// <br>回调定义：ret: 0x00 取消连续测距设置返回, ret: 0x01 单次测距设置返回, ret: 0x02 连续测距设置返回, ret: 0x03 激光指示设置返回,</br>
        /// <br>　　　　 ret: 0x81 val为返回的单次测距结果, ret: 0x82 val为返回的连续测距结果, ret: 0x83 val为返回的激光指示状态</br>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetRangFindingCallback(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SetRangFindingCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetRangFindingCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>控制端口上电回调设置</br>
        /// <para>@param callback 回调函数</para>
        /// <br>回调定义：ret: 端口号 val: 使能状态,</br>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetRelayControlCallback(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SetRelayControlCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetRelayControlCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>电池状态回调设置</br>
        /// <para>@param callback 回调函数</para>
        /// <br>回调定义：ret: 0x00 电量（%），0x01 电压（/1000=实际值）， 0x02 电流（/1000=实际值，值为正时表示充电， 值为负时表示放电）；  val: 回调值；</br>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetBatteryStatusCallback(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SetBatteryStatusCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetBatteryStatusCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>查询和控制端口上电，结果使用回调函数获取， 如查询和控制所有通道，先回调0x00或0x01，val为通道数，接着各通道结果依次回调</br>
        /// <para>@param port 端口 All: 0x00(查询所有) / 0x01（控制所有）；单路控制 1: 0x81, 2: 0x82, 2: 0x83, 4: 0x84;</para>
        /// <para>@param enable 使能状态</para>
        /// <para>@return</para>
        /// </summary>
        public int RelayControl(ushort port, bool enable)
        {
            if (api_RelayControl == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_RelayControl(IRIP.GetID(), port, enable);
        }

        /// <summary>
        /// <br>测距模组控制，测距结果使用回调函数获取</br>
        /// <para>@param command</para>
        /// <br>    1: 单次测距,</br>
        /// <br>    2: 连续测距,</br>
        /// <br>    3: 激光指示（仅串口模组支持）,</br>
        /// <br>    0: 停止连续测距</br>
        /// <para>@return</para>
        /// </summary>
        public int RangFinding(int command)
        {
            if (api_RangFinding == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_RangFinding(IRIP.GetID(), command);
        }

        /// <summary>
        /// <br>获取电池状态信息</br>
        /// <para>@param command</para>
        /// <br>    0: 电量,</br>
        /// <br>    1: 电压,</br>
        /// <br>    2: 电流</br>
        /// <para>@return　输出值，当电流值为正值时表示充电，负值表示放电</para>
        /// </summary>
        public int GetBatteryStatus(int command)
        {
            if (api_GetBatteryStatus == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetBatteryStatus(IRIP.GetID(), command);
        }

        /// <summary>
        /// <br>对焦位置回调设置</br>
        /// <para>@param callback 回调函数，val为回调位置</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetFocusPosiCallback(RetCallbackPlus callback, IntPtr pUser)
        {
            if (api_SetFocusPosiCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetFocusPosiCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>获取对焦错误码(判断相机是否支持电动对焦)</br>
        /// <para>@return 0: 正常无错误, -1: 相机类型不支持电动对焦，-2: 对焦通信端口未连接（可能不支持电动对焦），-11: 电动对焦故障</para>
        /// </summary>
        public int GetFocusErrCode()
        {
            if (api_GetFocusErrCode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFocusErrCode(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取对焦可用最大位置</br>
        /// <para>@return 正常返回一个&gt;0的整数，否则表示不支持对焦控制</para>
        /// </summary>
        public int GetFocusMaxPosi()
        {
            if (api_GetFocusMaxPosi == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFocusMaxPosi(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取当前对焦位置</br>
        /// <para>@return　正常返回一个&gt;=0的整数</para>
        /// </summary>
        public int GetFocusCurrentPosi()
        {
            if (api_GetFocusCurrentPosi == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetFocusCurrentPosi(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置自动对焦模式</br>
        /// <para>@param mode 模式</para>
        /// <br>     FOCUS_MODE_GRADIENT(0): 默认，以图像梯度值作为清晰度检测依据（图像清晰度）</br>
        /// <br>     FOCUS_MODE_HIGHTEMP(1): 以最高测量温度值作为清晰度检测依据</br>
        /// <br>     FOCUS_MODE_LOWTEMP(2):  以最低测量温度值作为清晰度检测依据 </br>
        /// <para>@return</para>
        /// </summary>
        public int SetFocusMode(int mode)
        {
            if (api_SetFocusMode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetFocusMode(IRIP.GetID(), mode);
        }

        /// <summary>
        /// <br>指定参数对焦</br>
        /// <para>@param command 命令参数</para>
        /// <br>     FOCUSZERO_TRY(-2), 忽略错误强制重试置零(posi&lt;=0时置零后移动到内置保存指定位置, posi&gt;0时为临时指定置零后移动到的位置)</br>
        /// <br>     FOCUSZERO(-1): 置零位(posi&lt;=0时置零后移动到内置保存指定位置, posi&gt;0时为临时指定置零后移动到的位置)</br>
        /// <br>     STOPFOCUS(0): 停止对焦 </br>
        /// <br>     FARFOCUS(1): 远焦触发, posi&lt;=0自动加速度, posi&gt;0连续对焦速度, 最大32</br>
        /// <br>     NEARFOCUS(2): 近焦触发, posi&lt;=0自动加速度, posi&gt;0连续对焦速度, 最大32</br>
        /// <br>     AUTOFOCUS(3): 快速自动对焦, 默认以全图像区域检测对焦</br>
        /// <br>     FOCUSSTATUS(4): 查询自动对焦状态</br>
        /// <br>     FOCUSPOSI(5): 对焦到指定位置, posi为目标位置</br>
        /// <br>     FOCUSMOVE(6): 前后移动聚焦位置, posi为移动步数，&lt;0表示向近焦位置移动，&gt;0表示向远焦位置移动</br>
        /// <para>@param posi 步长/对焦位置</para>
        /// <para>@return 查询聚焦状态时返回1 表示正在自动聚焦，返回0 表示未开始或者已完成自动聚焦</para>
        /// </summary>
        public int NearFarFocus(int command, int posi = 0)
        {
            if (api_NearFarFocus == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_NearFarFocus(IRIP.GetID(), command, posi);
        }

        /// <summary>
        /// <br>快速自动对焦</br>
        /// <para>@param rect 对焦检测区域, 该参数为NULL或width或height&lt;=0时默认以全图像区域检测对焦</para>
        /// <para>@return</para>
        /// </summary>
        public int AutoFocus(ref CInt32Rect rect)
        {
            if (api_AutoFocus == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_AutoFocus(IRIP.GetID(), ref rect);
        }

        /// <summary>
        /// <br>全域自动对焦（全程/定程寻焦）</br>
        /// <para>@param command 0: 全域自动对焦, 1: 自适应（当检测到寻焦过程锐度明显变小时，取最大锐度位置对焦）</para>
        /// <para>@param range 对焦位置搜索行程, range.x, range.y: 对应起始和结束对焦位置范围，若设置值均range.x = range.y时则为全程搜索寻焦</para>
        /// <para>@param rect 对焦检测区域, 该参数为NULL或width或height&lt;=0时默认以全图像区域检测对焦</para>
        /// <para>@return</para>
        /// </summary>
        public int FindFocus(int command, ref CInt32Point range, ref CInt32Rect rect)
        {
            if (api_FindFocus == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_FindFocus(IRIP.GetID(), command, ref range, ref rect);
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
        /// <br>获取当前相机的测温量程设置</br>
        /// <para>@return 0: 常温段, 1: 中温段</para>
        /// </summary>
        public int GetTempType()
        {
            if (api_GetTempType == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetTempType(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置相机的测温量程</br>
        /// <para>@param type 0: 常温段, 1: 中温段</para>
        /// <para>@return</para>
        /// </summary>
        public int SetTempType(uint type)
        {
            if (api_SetTempType == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetTempType(IRIP.GetID(), type);
        }

        /// <summary>
        /// <br>获取相机图像输出类型</br>
        /// <para>@return 0: RAW, 1: YUV，温度输出固定使用RAW类型（默认）</para>
        /// </summary>
        public int GetOutputType()
        {
            if (api_GetOutputType == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetOutputType(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置相机图像输出类型</br>
        /// <para>@param type 0: RAW, 1: YUV，温度输出固定使用RAW类型（默认）</para>
        /// </summary>
        public int SetOutputType(uint type)
        {
            if (api_SetOutputType == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetOutputType(IRIP.GetID(), type);
        }

        /// <summary>
        /// <br>打快门校正</br>
        /// </summary>
        public int Refresh()
        {
            if (api_Refresh == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Refresh(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取定时自动打快门校正时间(单位：秒)，手动出发或相机自动触发后会重新计时</br>
        /// <para>@return 0: 不自动打快门校正, &gt;=30: 自动打快门校正时间</para>
        /// </summary>
        public int GetAutoRefreshTime()
        {
            if (api_GetAutoRefreshTime == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetAutoRefreshTime(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取定时自动打快门校正时间(单位：秒)，手动出发或相机自动触发后会重新计时</br>
        /// <para>@param time 0: 不自动打快门校正, &gt;=30，默认值300, 机芯温度变化0.5摄氏度自动打快门，如果设置值为0时关闭自动打快门</para>
        /// </summary>
        public int SetAutoRefreshTime(uint time)
        {
            if (api_SetAutoRefreshTime == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetAutoRefreshTime(IRIP.GetID(), time);
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
        /// <br>获取相机温度修正参数</br>
        /// <para>@param val 修正参数</para>
        /// <para>@return </para>
        /// </summary>
        public int GetDeviceTempCorrection(ref float val)
        {
            if (api_GetDeviceTempCorrection == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetDeviceTempCorrection(IRIP.GetID(), ref val);
        }

        /// <summary>
        /// <br>获取相机温度修正参数</br>
        /// <para>@param val 参数值: -12.7~12.7</para>
        /// </summary>
        public int SetDeviceTempCorrection(float val)
        {
            if (api_SetDeviceTempCorrection == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetDeviceTempCorrection(IRIP.GetID(), val);
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

        /// <summary>
        /// <br>设置均匀度校正状态回调</br>
        /// <para>@param states NUCStates状态结构体指针，包含相机温度测量段改变/均匀度校正是否开启/当前校准数据组均匀度校正数据是否存在。</para>
        /// <para>@param callback 回调函数，状态改变后回调</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// <para>@return</para>
        /// </summary>
        public int SetNUCStatesCallback(IntPtr states, RetCallback callback, IntPtr pUser)
        {
            if (api_SetNUCStatesCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetNUCStatesCallback(IRIP.GetID(), states, callback, pUser);
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
        /// <br>设置扩展指令回调</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int SetExtendCMDCallback(MsgCallback callback, IntPtr pUser)
        {
            if (api_SetExtendCMDCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetExtendCMDCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>扩展指令</br>
        /// <para>@param type 类型</para>
        /// <para>@param msg 文本消息</para>
        /// </summary>
        public int ExtendCMD(int type, string msg)
        {
            if (api_ExtendCMD == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_ExtendCMD(IRIP.GetID(), type, Encoding.UTF8.GetBytes(msg));
        }

        /// <summary>
        /// <br>设置扩展测距驱动器（模块）</br>
        /// <para>@param type 类型</para>
        /// <para>@param devStruct 驱动器信息结构体指针</para>
        /// <para>@param devSize 结构体大小</para>
        /// </summary>
        public int SetExtendRangFindingDevice(int type, IntPtr devStruct, uint devSize)
        {
            if (api_SetExtendRangFindingDevice == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetExtendRangFindingDevice(IRIP.GetID(), type, devStruct, devSize);
        }

    }
}
