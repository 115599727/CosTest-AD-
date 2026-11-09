using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    public class IRIP_API_HJKCamera
    {
        IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
    api_SetNucTablesPath = _IRIP_API_hjkCamera_SetNucTablesPath;
    api_GetSegmentCalibData = _IRIP_API_hjkCamera_GetSegmentCalibData;
    api_SetSegmentCalibDataForFile = _IRIP_API_hjkCamera_SetSegmentCalibDataForFile;
    api_SetSegmentCalibData = _IRIP_API_hjkCamera_SetSegmentCalibData;
    api_GetCalibrationDistance = _IRIP_API_hjkCamera_GetCalibrationDistance;
    api_SetCalibrationDistance = _IRIP_API_hjkCamera_SetCalibrationDistance;
    api_Instance = _IRIP_API_hjkCamera_Instance;
    api_Destroy = _IRIP_API_hjkCamera_Destroy;
    api_InquireIP = _IRIP_API_hjkCamera_InquireIP;
    api_Create = _IRIP_API_hjkCamera_Create;
    api_InqureDeviceInfo = _IRIP_API_hjkCamera_InqureDeviceInfo;
    api_Connect = _IRIP_API_hjkCamera_Connect;
    api_SetOutMaxFps = _IRIP_API_hjkCamera_SetOutMaxFps;
    api_GetOutMaxFps = _IRIP_API_hjkCamera_GetOutMaxFps;
    api_Play = _IRIP_API_hjkCamera_Play;
    api_Stop = _IRIP_API_hjkCamera_Stop;
    api_NearFarFocus = _IRIP_API_hjkCamera_NearFarFocus;
    api_MoveCtrl = _IRIP_API_hjkCamera_MoveCtrl;
    api_CommSend = _IRIP_API_hjkCamera_CommSend;
    api_SetIP = _IRIP_API_hjkCamera_SetIP;
    api_GetTempType = _IRIP_API_hjkCamera_GetTempType;
    api_SetTempType = _IRIP_API_hjkCamera_SetTempType;
    api_Refresh = _IRIP_API_hjkCamera_Refresh;
    api_GetAutoRefreshTime = _IRIP_API_hjkCamera_GetAutoRefreshTime;
    api_SetAutoRefreshTime = _IRIP_API_hjkCamera_SetAutoRefreshTime;
    api_SetFrameCallback = _IRIP_API_hjkCamera_SetFrameCallback;
    api_SelectPalette = _IRIP_API_hjkCamera_SelectPalette;
    api_GetSelectPalette = _IRIP_API_hjkCamera_GetSelectPalette;
    api_SetPalette = _IRIP_API_hjkCamera_SetPalette;
    api_SetFilterMode = _IRIP_API_hjkCamera_SetFilterMode;
    api_GetFilterMode = _IRIP_API_hjkCamera_GetFilterMode;
    api_GetIRIPConfig = _IRIP_API_hjkCamera_GetIRIPConfig;
    api_SetIRIPConfig = _IRIP_API_hjkCamera_SetIRIPConfig;
    api_SetAdjustDirection = _IRIP_API_hjkCamera_SetAdjustDirection;
    api_SetMeasurement = _IRIP_API_hjkCamera_SetMeasurement;
    api_SetTempISO = _IRIP_API_hjkCamera_SetTempISO;
    api_GetTempISO = _IRIP_API_hjkCamera_GetTempISO;
    api_SetTempNephogram = _IRIP_API_hjkCamera_SetTempNephogram;
    api_GetTempNephogram = _IRIP_API_hjkCamera_GetTempNephogram;
    api_TempNephogramEnable = _IRIP_API_hjkCamera_TempNephogramEnable;
    api_TempNephogramOverlay = _IRIP_API_hjkCamera_TempNephogramOverlay;
    api_TempNephogramDpi = _IRIP_API_hjkCamera_TempNephogramDpi;
    api_RefreshData = _IRIP_API_hjkCamera_RefreshData;
    api_SetGlobalMDParameters = _IRIP_API_hjkCamera_SetGlobalMDParameters;
    api_EnRefEmissivityTransmittanceUnit = _IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit;
#else
            api_SetNucTablesPath = (IRIP_API_hjkCamera_SetNucTablesPath)dll.Invoke("IRIP_API_hjkCamera_SetNucTablesPath", typeof(IRIP_API_hjkCamera_SetNucTablesPath));
            api_GetSegmentCalibData = (IRIP_API_hjkCamera_GetSegmentCalibData)dll.Invoke("IRIP_API_hjkCamera_GetSegmentCalibData", typeof(IRIP_API_hjkCamera_GetSegmentCalibData));
            api_SetSegmentCalibDataForFile = (IRIP_API_hjkCamera_SetSegmentCalibDataForFile)dll.Invoke("IRIP_API_hjkCamera_SetSegmentCalibDataForFile", typeof(IRIP_API_hjkCamera_SetSegmentCalibDataForFile));
            api_SetSegmentCalibData = (IRIP_API_hjkCamera_SetSegmentCalibData)dll.Invoke("IRIP_API_hjkCamera_SetSegmentCalibData", typeof(IRIP_API_hjkCamera_SetSegmentCalibData));
            api_GetCalibrationDistance = (IRIP_API_hjkCamera_GetCalibrationDistance)dll.Invoke("IRIP_API_hjkCamera_GetCalibrationDistance", typeof(IRIP_API_hjkCamera_GetCalibrationDistance));
            api_SetCalibrationDistance = (IRIP_API_hjkCamera_SetCalibrationDistance)dll.Invoke("IRIP_API_hjkCamera_SetCalibrationDistance", typeof(IRIP_API_hjkCamera_SetCalibrationDistance));
            api_Instance = (IRIP_API_hjkCamera_Instance)dll.Invoke("IRIP_API_hjkCamera_Instance", typeof(IRIP_API_hjkCamera_Instance));
            api_Destroy = (IRIP_API_hjkCamera_Destroy)dll.Invoke("IRIP_API_hjkCamera_Destroy", typeof(IRIP_API_hjkCamera_Destroy));
            api_InquireIP = (IRIP_API_hjkCamera_InquireIP)dll.Invoke("IRIP_API_hjkCamera_InquireIP", typeof(IRIP_API_hjkCamera_InquireIP));
            api_Create = (IRIP_API_hjkCamera_Create)dll.Invoke("IRIP_API_hjkCamera_Create", typeof(IRIP_API_hjkCamera_Create));
            api_InqureDeviceInfo = (IRIP_API_hjkCamera_InqureDeviceInfo)dll.Invoke("IRIP_API_hjkCamera_InqureDeviceInfo", typeof(IRIP_API_hjkCamera_InqureDeviceInfo));
            api_Connect = (IRIP_API_hjkCamera_Connect)dll.Invoke("IRIP_API_hjkCamera_Connect", typeof(IRIP_API_hjkCamera_Connect));
            api_SetOutMaxFps = (IRIP_API_hjkCamera_SetOutMaxFps)dll.Invoke("IRIP_API_hjkCamera_SetOutMaxFps", typeof(IRIP_API_hjkCamera_SetOutMaxFps));
            api_GetOutMaxFps = (IRIP_API_hjkCamera_GetOutMaxFps)dll.Invoke("IRIP_API_hjkCamera_GetOutMaxFps", typeof(IRIP_API_hjkCamera_GetOutMaxFps));
            api_Play = (IRIP_API_hjkCamera_Play)dll.Invoke("IRIP_API_hjkCamera_Play", typeof(IRIP_API_hjkCamera_Play));
            api_Stop = (IRIP_API_hjkCamera_Stop)dll.Invoke("IRIP_API_hjkCamera_Stop", typeof(IRIP_API_hjkCamera_Stop));
            api_NearFarFocus = (IRIP_API_hjkCamera_NearFarFocus)dll.Invoke("IRIP_API_hjkCamera_NearFarFocus", typeof(IRIP_API_hjkCamera_NearFarFocus));
            api_MoveCtrl = (IRIP_API_hjkCamera_MoveCtrl)dll.Invoke("IRIP_API_hjkCamera_MoveCtrl", typeof(IRIP_API_hjkCamera_MoveCtrl));
            api_CommSend = (IRIP_API_hjkCamera_CommSend)dll.Invoke("IRIP_API_hjkCamera_CommSend", typeof(IRIP_API_hjkCamera_CommSend));
            api_SetIP = (IRIP_API_hjkCamera_SetIP)dll.Invoke("IRIP_API_hjkCamera_SetIP", typeof(IRIP_API_hjkCamera_SetIP));
            api_GetTempType = (IRIP_API_hjkCamera_GetTempType)dll.Invoke("IRIP_API_hjkCamera_GetTempType", typeof(IRIP_API_hjkCamera_GetTempType));
            api_SetTempType = (IRIP_API_hjkCamera_SetTempType)dll.Invoke("IRIP_API_hjkCamera_SetTempType", typeof(IRIP_API_hjkCamera_SetTempType));
            api_Refresh = (IRIP_API_hjkCamera_Refresh)dll.Invoke("IRIP_API_hjkCamera_Refresh", typeof(IRIP_API_hjkCamera_Refresh));
            api_GetAutoRefreshTime = (IRIP_API_hjkCamera_GetAutoRefreshTime)dll.Invoke("IRIP_API_hjkCamera_GetAutoRefreshTime", typeof(IRIP_API_hjkCamera_GetAutoRefreshTime));
            api_SetAutoRefreshTime = (IRIP_API_hjkCamera_SetAutoRefreshTime)dll.Invoke("IRIP_API_hjkCamera_SetAutoRefreshTime", typeof(IRIP_API_hjkCamera_SetAutoRefreshTime));
            api_SetFrameCallback = (IRIP_API_hjkCamera_SetFrameCallback)dll.Invoke("IRIP_API_hjkCamera_SetFrameCallback", typeof(IRIP_API_hjkCamera_SetFrameCallback));
            api_SelectPalette = (IRIP_API_hjkCamera_SelectPalette)dll.Invoke("IRIP_API_hjkCamera_SelectPalette", typeof(IRIP_API_hjkCamera_SelectPalette));
            api_GetSelectPalette = (IRIP_API_hjkCamera_GetSelectPalette)dll.Invoke("IRIP_API_hjkCamera_GetSelectPalette", typeof(IRIP_API_hjkCamera_GetSelectPalette));
            api_SetPalette = (IRIP_API_hjkCamera_SetPalette)dll.Invoke("IRIP_API_hjkCamera_SetPalette", typeof(IRIP_API_hjkCamera_SetPalette));
            api_SetFilterMode = (IRIP_API_hjkCamera_SetFilterMode)dll.Invoke("IRIP_API_hjkCamera_SetFilterMode", typeof(IRIP_API_hjkCamera_SetFilterMode));
            api_GetFilterMode = (IRIP_API_hjkCamera_GetFilterMode)dll.Invoke("IRIP_API_hjkCamera_GetFilterMode", typeof(IRIP_API_hjkCamera_GetFilterMode));
            api_GetIRIPConfig = (IRIP_API_hjkCamera_GetIRIPConfig)dll.Invoke("IRIP_API_hjkCamera_GetIRIPConfig", typeof(IRIP_API_hjkCamera_GetIRIPConfig));
            api_SetIRIPConfig = (IRIP_API_hjkCamera_SetIRIPConfig)dll.Invoke("IRIP_API_hjkCamera_SetIRIPConfig", typeof(IRIP_API_hjkCamera_SetIRIPConfig));
            api_SetAdjustDirection = (IRIP_API_hjkCamera_SetAdjustDirection)dll.Invoke("IRIP_API_hjkCamera_SetAdjustDirection", typeof(IRIP_API_hjkCamera_SetAdjustDirection));
            api_SetMeasurement = (IRIP_API_hjkCamera_SetMeasurement)dll.Invoke("IRIP_API_hjkCamera_SetMeasurement", typeof(IRIP_API_hjkCamera_SetMeasurement));
            api_SetTempISO = (IRIP_API_hjkCamera_SetTempISO)dll.Invoke("IRIP_API_hjkCamera_SetTempISO", typeof(IRIP_API_hjkCamera_SetTempISO));
            api_GetTempISO = (IRIP_API_hjkCamera_GetTempISO)dll.Invoke("IRIP_API_hjkCamera_GetTempISO", typeof(IRIP_API_hjkCamera_GetTempISO));
            api_SetTempNephogram = (IRIP_API_hjkCamera_SetTempNephogram)dll.Invoke("IRIP_API_hjkCamera_SetTempNephogram", typeof(IRIP_API_hjkCamera_SetTempNephogram));
            api_GetTempNephogram = (IRIP_API_hjkCamera_GetTempNephogram)dll.Invoke("IRIP_API_hjkCamera_GetTempNephogram", typeof(IRIP_API_hjkCamera_GetTempNephogram));
            api_TempNephogramEnable = (IRIP_API_hjkCamera_TempNephogramEnable)dll.Invoke("IRIP_API_hjkCamera_TempNephogramEnable", typeof(IRIP_API_hjkCamera_TempNephogramEnable));
            api_TempNephogramOverlay = (IRIP_API_hjkCamera_TempNephogramOverlay)dll.Invoke("IRIP_API_hjkCamera_TempNephogramOverlay", typeof(IRIP_API_hjkCamera_TempNephogramOverlay));
            api_TempNephogramDpi = (IRIP_API_hjkCamera_TempNephogramDpi)dll.Invoke("IRIP_API_hjkCamera_TempNephogramDpi", typeof(IRIP_API_hjkCamera_TempNephogramDpi));
            api_RefreshData = (IRIP_API_hjkCamera_RefreshData)dll.Invoke("IRIP_API_hjkCamera_RefreshData", typeof(IRIP_API_hjkCamera_RefreshData));
            api_SetGlobalMDParameters = (IRIP_API_hjkCamera_SetGlobalMDParameters)dll.Invoke("IRIP_API_hjkCamera_SetGlobalMDParameters", typeof(IRIP_API_hjkCamera_SetGlobalMDParameters));
            api_EnRefEmissivityTransmittanceUnit = (IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit)dll.Invoke("IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit", typeof(IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit));
#endif
        }

#if _X86
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetNucTablesPath", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetNucTablesPath(int id, byte[] path, byte[] sn);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetSegmentCalibDataForFile", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetCalibrationDistance(int id, ref float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetCalibrationDistance(int id, float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Instance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Instance(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Destroy", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Destroy(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_InquireIP", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_InquireIP(int id, ref T_IPADDRS ipaddrs, int length);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Create", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Create(int id, ref T_IPADDR ipaddr);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_InqureDeviceInfo", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_InqureDeviceInfo(int id, ref T_DEVICE_INFO devInfo);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Connect", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Connect(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetOutMaxFps(int id, float fps);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetOutMaxFps", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetOutMaxFps(int id, ref float fps);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Play", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Play(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Stop", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Stop(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_NearFarFocus", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_NearFarFocus(int id, int command);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_MoveCtrl", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_MoveCtrl(int id, int protocol, int type, uint param);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_CommSend", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_CommSend(int id, byte[] data, int length);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetIP", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetIP(int id, byte[] pIp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetTempType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetTempType(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetTempType", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetTempType(int id, uint type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_Refresh", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_Refresh(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetAutoRefreshTime", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetAutoRefreshTime(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetAutoRefreshTime", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetAutoRefreshTime(int id, uint time);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetFrameCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SelectPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetSelectPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetPalette", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetFilterMode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetFilterMode(int id, uint val);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetFilterMode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetFilterMode(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetAdjustDirection", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetMeasurement", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetTempISO", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetTempISO(int id, ref TempISO iso);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetTempISO", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetTempISO(int id, ref TempISO iso);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetTempNephogram", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_GetTempNephogram", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_TempNephogramEnable", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_TempNephogramEnable(int id, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_TempNephogramOverlay", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_TempNephogramOverlay(int id, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_TempNephogramDpi", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_TempNephogramDpi(int id, float dpi);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_RefreshData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_RefreshData(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_SetGlobalMDParameters", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
#endif

        private delegate int IRIP_API_hjkCamera_SetNucTablesPath(int id, byte[] path, byte[] sn);
        private IRIP_API_hjkCamera_SetNucTablesPath api_SetNucTablesPath;

        private delegate int IRIP_API_hjkCamera_GetSegmentCalibData(int id, byte[] data, ref int length, int type);
        private IRIP_API_hjkCamera_GetSegmentCalibData api_GetSegmentCalibData;

        private delegate int IRIP_API_hjkCamera_SetSegmentCalibDataForFile(int id, int type, bool enable);
        private IRIP_API_hjkCamera_SetSegmentCalibDataForFile api_SetSegmentCalibDataForFile;

        private delegate int IRIP_API_hjkCamera_SetSegmentCalibData(int id, byte[] data, int length, int type, bool enable);
        private IRIP_API_hjkCamera_SetSegmentCalibData api_SetSegmentCalibData;

        private delegate int IRIP_API_hjkCamera_GetCalibrationDistance(int id, ref float distance);
        private IRIP_API_hjkCamera_GetCalibrationDistance api_GetCalibrationDistance;

        private delegate int IRIP_API_hjkCamera_SetCalibrationDistance(int id, float distance);
        private IRIP_API_hjkCamera_SetCalibrationDistance api_SetCalibrationDistance;

        private delegate int IRIP_API_hjkCamera_Instance(int id);
        private IRIP_API_hjkCamera_Instance api_Instance;

        private delegate int IRIP_API_hjkCamera_Destroy(int id);
        private IRIP_API_hjkCamera_Destroy api_Destroy;

        private delegate int IRIP_API_hjkCamera_InquireIP(int id, ref T_IPADDRS ipaddrs, int length);
        private IRIP_API_hjkCamera_InquireIP api_InquireIP;

        private delegate int IRIP_API_hjkCamera_Create(int id, ref T_IPADDR ipaddr);
        private IRIP_API_hjkCamera_Create api_Create;

        private delegate int IRIP_API_hjkCamera_InqureDeviceInfo(int id, ref T_DEVICE_INFO devInfo);
        private IRIP_API_hjkCamera_InqureDeviceInfo api_InqureDeviceInfo;

        private delegate int IRIP_API_hjkCamera_Connect(int id);
        private IRIP_API_hjkCamera_Connect api_Connect;

        private delegate int IRIP_API_hjkCamera_SetOutMaxFps(int id, float fps);
        private IRIP_API_hjkCamera_SetOutMaxFps api_SetOutMaxFps;

        private delegate int IRIP_API_hjkCamera_GetOutMaxFps(int id, ref float fps);
        private IRIP_API_hjkCamera_GetOutMaxFps api_GetOutMaxFps;

        private delegate int IRIP_API_hjkCamera_Play(int id);
        private IRIP_API_hjkCamera_Play api_Play;

        private delegate int IRIP_API_hjkCamera_Stop(int id);
        private IRIP_API_hjkCamera_Stop api_Stop;

        private delegate int IRIP_API_hjkCamera_NearFarFocus(int id, int command);
        private IRIP_API_hjkCamera_NearFarFocus api_NearFarFocus;

        private delegate int IRIP_API_hjkCamera_MoveCtrl(int id, int protocol, int type, uint param);
        private IRIP_API_hjkCamera_MoveCtrl api_MoveCtrl;

        private delegate int IRIP_API_hjkCamera_CommSend(int id, byte[] data, int length);
        private IRIP_API_hjkCamera_CommSend api_CommSend;

        private delegate int IRIP_API_hjkCamera_SetIP(int id, byte[] pIp);
        private IRIP_API_hjkCamera_SetIP api_SetIP;

        private delegate int IRIP_API_hjkCamera_GetTempType(int id);
        private IRIP_API_hjkCamera_GetTempType api_GetTempType;

        private delegate int IRIP_API_hjkCamera_SetTempType(int id, uint type);
        private IRIP_API_hjkCamera_SetTempType api_SetTempType;

        private delegate int IRIP_API_hjkCamera_Refresh(int id);
        private IRIP_API_hjkCamera_Refresh api_Refresh;

        private delegate int IRIP_API_hjkCamera_GetAutoRefreshTime(int id);
        private IRIP_API_hjkCamera_GetAutoRefreshTime api_GetAutoRefreshTime;

        private delegate int IRIP_API_hjkCamera_SetAutoRefreshTime(int id, uint time);
        private IRIP_API_hjkCamera_SetAutoRefreshTime api_SetAutoRefreshTime;

        private delegate int IRIP_API_hjkCamera_SetFrameCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_hjkCamera_SetFrameCallback api_SetFrameCallback;

        private delegate int IRIP_API_hjkCamera_SelectPalette(int id, uint index, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_hjkCamera_SelectPalette api_SelectPalette;

        private delegate int IRIP_API_hjkCamera_GetSelectPalette(int id, ref int index, ref float galpha, ref float lalpha, ref bool preMultiplied, ref bool rgbInversion);
        private IRIP_API_hjkCamera_GetSelectPalette api_GetSelectPalette;

        private delegate int IRIP_API_hjkCamera_SetPalette(int id, uint[] palette, float galpha, float lalpha, bool preMultiplied, bool rgbInversion);
        private IRIP_API_hjkCamera_SetPalette api_SetPalette;

        private delegate int IRIP_API_hjkCamera_SetFilterMode(int id, uint val);
        private IRIP_API_hjkCamera_SetFilterMode api_SetFilterMode;

        private delegate int IRIP_API_hjkCamera_GetFilterMode(int id);
        private IRIP_API_hjkCamera_GetFilterMode api_GetFilterMode;

        private delegate int IRIP_API_hjkCamera_GetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_hjkCamera_GetIRIPConfig api_GetIRIPConfig;

        private delegate int IRIP_API_hjkCamera_SetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_hjkCamera_SetIRIPConfig api_SetIRIPConfig;

        private delegate int IRIP_API_hjkCamera_SetAdjustDirection(int id, int rotate, bool mirror_h, bool mirror_v);
        private IRIP_API_hjkCamera_SetAdjustDirection api_SetAdjustDirection;

        private delegate int IRIP_API_hjkCamera_SetMeasurement(int id, byte[] action, byte[] text, MdCallback callback, IntPtr pUser);
        private IRIP_API_hjkCamera_SetMeasurement api_SetMeasurement;

        private delegate int IRIP_API_hjkCamera_SetTempISO(int id, ref TempISO iso);
        private IRIP_API_hjkCamera_SetTempISO api_SetTempISO;

        private delegate int IRIP_API_hjkCamera_GetTempISO(int id, ref TempISO iso);
        private IRIP_API_hjkCamera_GetTempISO api_GetTempISO;

        private delegate int IRIP_API_hjkCamera_SetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_hjkCamera_SetTempNephogram api_SetTempNephogram;

        private delegate int IRIP_API_hjkCamera_GetTempNephogram(int id, ref TempNephogram nephogram, ref bool enable, ref bool overlay);
        private IRIP_API_hjkCamera_GetTempNephogram api_GetTempNephogram;

        private delegate int IRIP_API_hjkCamera_TempNephogramEnable(int id, bool enable);
        private IRIP_API_hjkCamera_TempNephogramEnable api_TempNephogramEnable;

        private delegate int IRIP_API_hjkCamera_TempNephogramOverlay(int id, bool enable);
        private IRIP_API_hjkCamera_TempNephogramOverlay api_TempNephogramOverlay;

        private delegate int IRIP_API_hjkCamera_TempNephogramDpi(int id, float dpi);
        private IRIP_API_hjkCamera_TempNephogramDpi api_TempNephogramDpi;

        private delegate int IRIP_API_hjkCamera_RefreshData(int id);
        private IRIP_API_hjkCamera_RefreshData api_RefreshData;

        private delegate int IRIP_API_hjkCamera_SetGlobalMDParameters(int id, ref MDParameters parameters);
        private IRIP_API_hjkCamera_SetGlobalMDParameters api_SetGlobalMDParameters;

        private delegate int IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit(int id, int temp);
        private IRIP_API_hjkCamera_EnRefEmissivityTransmittanceUnit api_EnRefEmissivityTransmittanceUnit;

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
        /// <br>创建相机实例</br>
        /// </summary>
        public int Instance()
        {
            if (api_Instance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Instance(IRIP.GetID());
        }

        /// <summary>
        /// <br>销毁相机实例</br>
        /// </summary>
        public int Destroy()
        {
            if (api_Destroy == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Destroy(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取相机IP</br>
        /// <para>@param ipaddrs ip信息结构体数组</para>
        /// <para>@param length 数组大小</para>
        /// </summary>
        public int InquireIP(ref T_IPADDRS ipaddrs, int length)
        {
            if (api_InquireIP == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_InquireIP(IRIP.GetID(), ref ipaddrs, length);
        }

        /// <summary>
        /// <br>创建相机连接实例</br>
        /// <para>@param ipaddr ip信息结构体</para>
        /// <para>@return 0：创建失败，1：创建成功，2：销毁已存在并重新创建</para>
        /// </summary>
        public int Create(ref T_IPADDR ipaddr)
        {
            if (api_Create == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Create(IRIP.GetID(), ref ipaddr);
        }

        /// <summary>
        /// <br>获取设备信息</br>
        /// <para>@param devInfo 设备信息结构体</para>
        /// <para>@return 0：成功 其他： 失败</para>
        /// </summary>
        public int InqureDeviceInfo(ref T_DEVICE_INFO devInfo)
        {
            if (api_InqureDeviceInfo == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_InqureDeviceInfo(IRIP.GetID(), ref devInfo);
        }

        /// <summary>
        /// <br>建立连接</br>
        /// <para>@return 0：成功 其他： 失败</para>
        /// </summary>
        public int Connect()
        {
            if (api_Connect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Connect(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置帧率</br>
        /// <para>@param fps 帧率，50(50.0f), 25(25.0f), 12(12.5f), 6(6.25f), 3(3.125f), 1(1.5625f)</para>
        /// <para>@return 0：成功 其他： 失败</para>
        /// </summary>
        public int SetOutMaxFps(float fps)
        {
            if (api_SetOutMaxFps == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetOutMaxFps(IRIP.GetID(), fps);
        }

        /// <summary>
        /// <br>HJKCamera 获取帧率（当前SDK设置的帧率，如果相机已被其它接口设置，获取到的值可能是不准确的）</br>
        /// <para>@param fps 返回的帧率值</para>
        /// <para>@return 0：成功 其他： 失败</para>
        /// </summary>
        public int GetOutMaxFps(ref float fps)
        {
            if (api_GetOutMaxFps == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetOutMaxFps(IRIP.GetID(), ref fps);
        }

        /// <summary>
        /// <br>开始获取数据</br>
        /// <para>@return 0：成功 其他： 失败</para>
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
        /// <br>停止获取数据</br>
        /// <para>@return 0：成功 其他： 失败</para>
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
        /// <br>指定参数对焦</br>
        /// <para>@param command 0: 停止对焦, 1: 远焦触发, 2: 近焦触发, 3: 自动对焦, 4: 查询自动对焦状态</para>
        /// <para>@return 查询聚焦状态时返回1 表示正在自动聚焦，返回0 表示未开始或者已完成自动聚焦</para>
        /// </summary>
        public int NearFarFocus(int command)
        {
            if (api_NearFarFocus == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_NearFarFocus(IRIP.GetID(), command);
        }

        /// <summary>
        /// <br>云台控制</br>
        /// <para>@param protocol 控制协议 0：Pelco-D，1：自定义1（升降杆），2：自定义2（舵机），3：自定义3（转台），4：自定义4（舵机马达模式）</para>
        /// <para>@param type 4: 上, 5: 下, 6: 左, 7: 右, 8: 停, 9: 波特率</para>
        /// <para>@param param 当选择pelco-D 协议时，该参数为速度，高16 位为垂直方向速度，低16 位为水平方向速，参数范围分别为1~64</para>
        /// <para>@return</para>
        /// </summary>
        public int MoveCtrl(int protocol, int type, uint param)
        {
            if (api_MoveCtrl == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_MoveCtrl(IRIP.GetID(), protocol, type, param);
        }

        /// <summary>
        /// <br>透传RS485数据</br>
        /// <para>@param data 数据</para>
        /// <para>@param len 数据长度</para>
        /// </summary>
        public int CommSend(string data, int length)
        {
            if (api_CommSend == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_CommSend(IRIP.GetID(), Encoding.UTF8.GetBytes(data), length);
        }

        /// <summary>
        /// <br>修改IP</br>
        /// <para>@param pIp 新的IP地址</para>
        /// <para>@return 0：成功 其他： 失败</para>
        /// </summary>
        public int SetIP(string pIp)
        {
            if (api_SetIP == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetIP(IRIP.GetID(), Encoding.UTF8.GetBytes(pIp));
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
        /// <br>获取测温滤波模式</br>
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
