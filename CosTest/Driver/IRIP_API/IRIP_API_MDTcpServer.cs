using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>温度测量TCP交互接口</br>
    /// </summary>
    public class IRIP_API_MDTcpServer
    {
        private IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
        api_Set = _IRIP_API_mdTcpServer_Set;
        api_Run = _IRIP_API_mdTcpServer_Run;
        api_Close = _IRIP_API_mdTcpServer_Close;
        api_SetInfoCallback = _IRIP_API_mdTcpServer_SetInfoCallback;
        api_InfoCallbackNotice = _IRIP_API_mdTcpServer_InfoCallbackNotice;
#else
            api_Set = (IRIP_API_mdTcpServer_Set)dll.Invoke("IRIP_API_mdTcpServer_Set", typeof(IRIP_API_mdTcpServer_Set));
            api_Run = (IRIP_API_mdTcpServer_Run)dll.Invoke("IRIP_API_mdTcpServer_Run", typeof(IRIP_API_mdTcpServer_Run));
            api_Close = (IRIP_API_mdTcpServer_Close)dll.Invoke("IRIP_API_mdTcpServer_Close", typeof(IRIP_API_mdTcpServer_Close));
            api_SetInfoCallback = (IRIP_API_mdTcpServer_SetInfoCallback)dll.Invoke("IRIP_API_mdTcpServer_SetInfoCallback", typeof(IRIP_API_mdTcpServer_SetInfoCallback));
            api_InfoCallbackNotice = (IRIP_API_mdTcpServer_InfoCallbackNotice)dll.Invoke("IRIP_API_mdTcpServer_InfoCallbackNotice", typeof(IRIP_API_mdTcpServer_InfoCallbackNotice));
#endif
        }

#if _X86
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_mdTcpServer_Set", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_mdTcpServer_Set(int id, int camType, byte[] appVername, int appVercode);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_mdTcpServer_Run", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_mdTcpServer_Run(int id, ushort port, RetCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_mdTcpServer_Close", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_mdTcpServer_Close(int id);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_mdTcpServer_SetInfoCallback", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_mdTcpServer_SetInfoCallback(int id, InfoCallback callback, IntPtr pUser);
    
    [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_mdTcpServer_InfoCallbackNotice", CallingConvention = CallingConvention.StdCall)]
    static private extern int _IRIP_API_mdTcpServer_InfoCallbackNotice(int id, int ret, byte[] action, byte[] content, byte[] content2);
#endif

        private delegate int IRIP_API_mdTcpServer_Set(int id, int camType, byte[] appVername, int appVercode);
        private IRIP_API_mdTcpServer_Set api_Set;

        private delegate int IRIP_API_mdTcpServer_Run(int id, ushort port, RetCallback callback, IntPtr pUser);
        private IRIP_API_mdTcpServer_Run api_Run;

        private delegate int IRIP_API_mdTcpServer_Close(int id);
        private IRIP_API_mdTcpServer_Close api_Close;

        private delegate int IRIP_API_mdTcpServer_SetInfoCallback(int id, InfoCallback callback, IntPtr pUser);
        private IRIP_API_mdTcpServer_SetInfoCallback api_SetInfoCallback;

        private delegate int IRIP_API_mdTcpServer_InfoCallbackNotice(int id, int ret, byte[] action, byte[] content, byte[] content2);
        private IRIP_API_mdTcpServer_InfoCallbackNotice api_InfoCallbackNotice;

        /// <summary>
        /// <br>设置温度测量服务器, 需获取相机空间指针，请在启动相机后执行</br>
        /// <para>@param camType 相机类型 0: irCamera, 1: thCamera, 2: hjkCamera, 3: netIRCamera</para>
        /// <para>@param appVername 程序版本名</para>
        /// <para>@param appVercode 程序版本号</para>
        /// <para>@return 1: 设置成功, 0: 实例未初始化, -1: 相机未启动, -2: 相机类型错误或相机实例未初始化</para>
        /// </summary>
        public int Set(int camType, string appVername, int appVercode)
        {
            if (api_Set == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Set(IRIP.GetID(), camType, Encoding.UTF8.GetBytes(appVername), appVercode);
        }

        /// <summary>
        /// <br>启动温度测量服务器</br>
        /// <para>@param callback 回调函数指针，ret: 端口号/错误码</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int Run(ushort port, RetCallback callback, IntPtr pUser)
        {
            if (api_Run == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Run(IRIP.GetID(), port, callback, pUser);
        }

        /// <summary>
        /// <br>关闭服务器</br>
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
        /// <br>设置消息回调</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int SetInfoCallback(InfoCallback callback, IntPtr pUser)
        {
            if (api_SetInfoCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetInfoCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>发送回复消息</br>
        /// <para>@param ret 状态码</para>
        /// <para>@param action 消息标识</para>
        /// <para>@param content 消息1</para>
        /// <para>@param content2 消息2</para>
        /// </summary>
        public int InfoCallbackNotice(int ret, string action, string content, string content2)
        {
            if (api_InfoCallbackNotice == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_InfoCallbackNotice(IRIP.GetID(), ret, Encoding.UTF8.GetBytes(action), Encoding.UTF8.GetBytes(content), Encoding.UTF8.GetBytes(content2));
        }
    }
}
