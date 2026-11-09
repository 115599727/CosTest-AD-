using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>红外图像/视频打开状态回调</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param jsonVerCode JSON文本信息版本</para>
    /// <para>@param isIRImage 是否为红外图像</para>
    /// <para>@param width 图像宽度</para>
    /// <para>@param height 图像高度</para>
    /// <para>@param fileSize 文件大小</para>
    /// <para>@param isAnimation 是否为红外视频</para>
    /// <para>@param frames_number 总帧数</para>
    /// <para>@param s_time 起始帧时间戳</para>
    /// <para>@param e_time 结束帧时间戳</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void OpenCallback(int ret, uint jsonVerCode, bool isIRImage, uint width, uint height, UInt64 fileSize, bool isAnimation, uint frames_number, Int64 s_time, Int64 e_time, IntPtr pUser);

    /// <summary>
    /// <br>红外视频加载帧信息回调</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param frames_number 总帧数</para>
    /// <para>@param index 已当前加载到的帧位置索引</para>
    /// <para>@param width 视频宽度</para>
    /// <para>@param height 视频高度</para>
    /// <para>@param s_time 起始帧时间戳</para>
    /// <para>@param e_time 结束帧时间戳</para>
    /// <para>@param delay 相对于上一帧的延时时间（ms）</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void LoadFrameCallback(int ret, uint frames_number, uint index, uint width, uint height, Int64 s_time, Int64 e_time, uint delay, IntPtr pUser);

    /// <summary>
    /// <br>公用回调函数定义</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void RetCallback(int ret, IntPtr pUser);

    /// <summary>
    /// <br>公用回调函数定义2</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param size 保存红外图像/视频回调文件大小，以及作为其它数据使用，具体含义参照根据实际接口说明</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void RetCallbackPlus(int ret, UInt64 size, IntPtr pUser);

    /// <summary>
    /// <br>thCamera相机打开回调</br>
    /// <para>@param width 宽度</para>
    /// <para>@param height 高度</para>
    /// <para>@param serialNumber 序列号</para>
    /// <para>@param hardwareNumber 硬件版本</para>
    /// <para>@param firmwarelNumber 固件版本</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void CameraConnectCallback(int width, int height, int serialNumber, int hardwareNumber, int firmwarelNumber, IntPtr pUser);

    /// <summary>
    /// <br>导出矩阵温度数据到CSV文件保存进度回调(行数)</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param line 已处理行数</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void SaveCSVFileCallback(int ret, uint line, IntPtr pUser);

    /// <summary>
    /// <br>TCP测量服务器设置测量信息状态回调</br>
    /// <para>@param action (char*)消息标识别</para>
    /// <para>@param ret 状态码</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void MdCallback(IntPtr action, int ret, IntPtr pUser);

    /// <summary>
    /// <br>TCP服务命令触发回调</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param action (char*)消息标识别</para>
    /// <para>@param msg1 (char*)文本消息1</para>
    /// <para>@param msg2 (char*)文本消息2</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void InfoCallback(int ret, IntPtr action, IntPtr msg1, IntPtr msg2, IntPtr pUser);

    /// <summary>
    /// <br>TCP服务拍照触发回调</br>
    /// <para>@param ret 状态码</para>
    /// <para>@param path (char*)保存路径</para>
    /// <para>@param file (char*)文件名</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void SaveImageCallback(int ret, IntPtr path, IntPtr file, IntPtr pUser);

    /// <summary>
    /// <br>消息回调/br>
    /// <para>@param ret 状态码</para>
    /// <para>@param msg (char*)文本消息</para>
    /// <para>@param pUser 自定义指针回调</para>
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public delegate void MsgCallback(int ret, IntPtr msg, IntPtr pUser);
}
