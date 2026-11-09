using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Windows;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>TCP客户端通信接口2</br>
    /// </summary>
    public class IRIP_API_TcpTaskClient2
    {
        ~IRIP_API_TcpTaskClient2(){
            if(IRIP != null)
            {
                Disconnect();
            }
        }

        public class JSONClass
        {
            /// <summary>  
            /// <br>数据标识和状态码</br>
            /// </summary> 
            public class Action
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }
            }

            /// <summary>  
            /// <br>通讯消息协议类型</br>
            /// </summary> 
            public class MSG_TYPE
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>协议类型 0：简要字符串命令，1：JSON格式命令</br>
                /// </summary> 
                public int type { get; set; }
            }

            /// <summary>  
            /// <br>通讯消息编码类型</br>
            /// </summary> 
            public class CODING
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>编码类型 gbk 或 utf-8</br>
                /// </summary> 
                public string coding { get; set; }
            }

            /// <summary>  
            /// <br>基本信息</br>
            /// </summary> 
            public class INFO
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>协议类型 gbk 或 utf-8</br>
                /// </summary> 
                public string coding { get; set; }

                /// <summary>  
                /// <br>基本信息</br>
                /// </summary> 
                public cInfo info { get; set; }
            }

            /// <summary>  
            /// <br>基本信息类</br>
            /// </summary> 
            public class cInfo
            {
                /// <summary>  
                /// <br>API版本</br>
                /// </summary> 
                public string apiVersion { get; set; }

                /// <summary>  
                /// <br>API版本号</br>
                /// </summary> 
                public int apiVercode { get; set; }

                /// <summary>  
                /// <br>软件版本</br>
                /// </summary> 
                public string appVersion { get; set; }

                /// <summary>  
                /// <br>软件版本号</br>
                /// </summary> 
                public int appVercode { get; set; }

                /// <summary>  
                /// <br>相机类型</br>
                /// </summary> 
                public int cameraType { get; set; }

                /// <summary>  
                /// <br>相机序列号</br>
                /// </summary> 
                public string cameraSN { get; set; }

                /// <summary>  
                /// <br>相机分辨率像素宽度</br>
                /// </summary> 
                public int width { get; set; }

                /// <summary>  
                /// <br>相机分辨率像素高度</br>
                /// </summary> 
                public int height { get; set; }

                /// <summary>  
                /// <br>扩展服务器连接信息</br>
                /// </summary> 
                public cExtendServer extend_server { get; set; }

            }

            /// <summary>  
            /// <br>扩展服务器连接信息类</br>
            /// </summary> 
            public class cExtendServer
            {
                /// <summary>  
                /// <br>端口号</br>
                /// </summary> 
                public ushort port { get; set; }

                /// <summary>  
                /// <br>等待连接超时时间（秒）</br>
                /// </summary> 
                public int timeout { get; set; }
            }

            /// <summary>  
            /// <br>保存图像</br>
            /// </summary> 
            public class IMG
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>保存路径</br>
                /// </summary> 
                public string path { get; set; }

                /// <summary>  
                /// <br>文件名</br>
                /// </summary> 
                public string file { get; set; }
            }

            /// <summary>  
            /// <br>测量信息</br>
            /// </summary> 
            public class GET
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>时间戳</br>
                /// </summary> 
                public Int64 time { get; set; }

                /// <summary>  
                /// <br>测量信息对象</br>
                /// </summary> 
                public JSON.cMeasurement mds { get; set; }
            }

            /// <summary>  
            /// <br>测量信息</br>
            /// </summary> 
            public class SGET
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>间隔帧数</br>
                /// </summary> 
                public int interrupted { get; set; }

            }

            /// <summary>  
            /// <br>测量信息</br>
            /// </summary> 
            public class RSGET
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>间隔帧数</br>
                /// </summary> 
                public int interrupted { get; set; }

                /// <summary>  
                /// <br>时间戳</br>
                /// </summary> 
                public Int64 time { get; set; }

                /// <summary>  
                /// <br>测量信息对象</br>
                /// </summary> 
                public JSON.cMeasurement mds { get; set; }
            }

            /// <summary>  
            /// <br>最大对焦位置</br>
            /// </summary> 
            public class FocusPosi
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>最大对焦位置值</br>
                /// </summary> 
                public int posi { get; set; }
            }

            /// <summary>  
            /// <br>对焦焦命令</br>
            /// </summary> 
            public class NearFarFocus
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>对焦焦命令参数值</br>
                /// </summary> 
                public int command { get; set; }
            }

            /// <summary>  
            /// <br>对焦焦命令</br>
            /// </summary> 
            public class REFRESH
            {
                /// <summary>  
                /// <br>返回状态码</br>
                /// </summary> 
                public int ret { get; set; }

                /// <summary>  
                /// <br>数据标识</br>
                /// </summary> 
                public string action { get; set; }

                /// <summary>  
                /// <br>对焦焦命令参数值</br>
                /// </summary> 
                public int delayed { get; set; }
            }

        }

        /// <summary>  
        /// <br>解析JSON数据类</br>
        /// </summary> 
        public class TcpMsgParse
        {
            /// <summary>  
            /// <br>数据标识和状态码</br>
            /// </summary> 
            public JSONClass.Action Action = null;// new JSONClass.Action();

            /// <summary>  
            /// <br>通讯消息协议类型</br>
            /// </summary> 
            public JSONClass.MSG_TYPE MSG_TYPE = null;//new JSONClass.MSG_TYPE();

            /// <summary>  
            /// <br>通讯消息编码类型</br>
            /// </summary> 
            public JSONClass.CODING CODING = null;//new JSONClass.CODING();

            /// <summary>  
            /// <br>基本信息</br>
            /// </summary> 
            public JSONClass.INFO INFO = null;//new JSONClass.INFO();

            /// <summary>  
            /// <br>设置测量模板文件</br>
            /// </summary> 
            public JSONClass.Action PATH = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>设置测量模板JSON数据</br>
            /// </summary> 
            public JSONClass.Action JSON = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>单次获取测量结果</br>
            /// </summary> 
            public JSONClass.GET GET = null;//new JSONClass.GET();

            /// <summary>  
            /// <br>连续获取测量结果</br>
            /// </summary> 
            public JSONClass.SGET SGET = null;//new JSONClass.SGET();

            /// <summary>  
            /// <br>取消连续获取测量结果</br>
            /// </summary> 
            public JSONClass.Action nSGET = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>保存测量结果图像</br>
            /// </summary> 
            public JSONClass.IMG IMG = null;//new JSONClass.IMG();

            /// <summary>  
            /// <br>保存红外图像</br>
            /// </summary> 
            public JSONClass.IMG IRIMG = null;//new JSONClass.IMG();

            /// <summary>  
            /// <br>连续获取测量结果</br>
            /// </summary> 
            public JSONClass.RSGET RSGET = null;//new JSONClass.RSGET();

            /// <summary>  
            /// <br>快门校正</br>
            /// </summary> 
            public JSONClass.REFRESH REFRESH = null;//new JSONClass.REFRESH();

            /// <summary>  
            /// <br>最大对焦位置</br>
            /// </summary> 
            public JSONClass.FocusPosi FocusMaxPosi = null;//new JSONClass.FocusPosi();

            /// <summary>  
            /// <br>当前对焦位置</br>
            /// </summary> 
            public JSONClass.FocusPosi FocusCurrentPosi = null;//new JSONClass.FocusPosi();

            /// <summary>  
            /// <br>远近焦控制</br>
            /// </summary> 
            public JSONClass.NearFarFocus NearFarFocus = null;//new JSONClass.NearFarFocus();

            /// <summary>  
            /// <br>自动对焦</br>
            /// </summary> 
            public JSONClass.Action AutoFocus = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>全程寻焦</br>
            /// </summary> 
            public JSONClass.Action FindFocus = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>设置窗口大小和位置命令</br>
            /// </summary> 
            public JSONClass.Action SetWindowSize = null;//new JSONClass.Action();

            /// <summary>  
            /// <br>退出程序命令</br>
            /// </summary> 
            public JSONClass.Action EXIT_APP = null;//new JSONClass.Action();


            /// <summary>  
            /// <br>解析JSON</br>
            /// <para>@param str JSON文本</para>
            /// <para>@return 返回解析的标识action，根据该标识使用相应子对象，注意：未解析的对象为null</para>
            /// </summary> 
            public string Parse(string str)
            {
                try
                {
                    //先解析action和ret，然后根据action解析其它内容到相应对象
                    Action = JsonConvert.DeserializeObject<JSONClass.Action>(str);

                    if(Action == null)
                    {
                        return null;
                    }

                    switch (Action.action)
                    {
                        case "SET_MSG_TYPE":
                            MSG_TYPE = JsonConvert.DeserializeObject<JSONClass.MSG_TYPE>(str);
                            break;
                        case "SET_CODING":
                            CODING = JsonConvert.DeserializeObject<JSONClass.CODING>(str);
                            break;
                        case "GET_INFO":
                            INFO = JsonConvert.DeserializeObject<JSONClass.INFO>(str);
                            break;
                        case "PATH":
                            if (PATH == null)
                            {
                                PATH = new JSONClass.Action();
                            }
                            PATH.action = Action.action;
                            PATH.ret = Action.ret;
                            break;
                        case "JSON":
                            if (JSON == null)
                            {
                                JSON = new JSONClass.Action();
                            }
                            JSON.action = Action.action;
                            JSON.ret = Action.ret;
                            break;
                        case "GET":
                            GET = JsonConvert.DeserializeObject<JSONClass.GET>(str);
                            break;
                        case "SGET":
                            SGET = JsonConvert.DeserializeObject<JSONClass.SGET>(str);
                            break;
                        case "!SGET":
                            if (nSGET == null)
                            {
                                nSGET = new JSONClass.Action();
                            }
                            nSGET.action = Action.action;
                            nSGET.ret = Action.ret;
                            break;
                        case "IMG":
                            IMG = JsonConvert.DeserializeObject<JSONClass.IMG>(str);
                            break;
                        case "IRIMG":
                            IRIMG = JsonConvert.DeserializeObject<JSONClass.IMG>(str);
                            break;
                        case "RSGET":
                            RSGET = JsonConvert.DeserializeObject<JSONClass.RSGET>(str);
                            break;
                        case "REFRESH":
                            REFRESH = JsonConvert.DeserializeObject<JSONClass.REFRESH>(str);
                            break;
                        case "CetFocusMaxPosi":
                            FocusMaxPosi = JsonConvert.DeserializeObject<JSONClass.FocusPosi>(str);
                            break;
                        case "GetFocusCurrentPosi":
                            FocusCurrentPosi = JsonConvert.DeserializeObject<JSONClass.FocusPosi>(str);
                            break;
                        case "NearFarFocus":
                            NearFarFocus = JsonConvert.DeserializeObject<JSONClass.NearFarFocus>(str);
                            break;
                        case "AutoFocus":
                            if (AutoFocus == null)
                            {
                                AutoFocus = new JSONClass.Action();
                            }
                            AutoFocus.action = Action.action;
                            AutoFocus.ret = Action.ret;
                            break;
                        case "FindFocus":
                            if (FindFocus == null)
                            {
                                FindFocus = new JSONClass.Action();
                            }
                            FindFocus.action = Action.action;
                            FindFocus.ret = Action.ret;
                            break;
                        case "SetWindowSize":
                            if (SetWindowSize == null)
                            {
                                SetWindowSize = new JSONClass.Action();
                            }
                            SetWindowSize.action = Action.action;
                            SetWindowSize.ret = Action.ret;
                            break;
                        case "EXIT_APP":
                            if (EXIT_APP == null)
                            {
                                EXIT_APP = new JSONClass.Action();
                            }
                            EXIT_APP.action = Action.action;
                            EXIT_APP.ret = Action.ret;
                            break;
                        default:
                            break;
                    }

                    return Action.action;
                }
                catch(Exception e)
                {
                    Console.WriteLine("解析JSON出现错误, " + e.ToString());
                }
                return null;
            }

            /// <summary>  
            /// <br>清理所有解析的JSON子对象</br>
            /// </summary> 
            public void Clear()
            {
                Action = null;
                MSG_TYPE = null;
                CODING = null;
                INFO = null;
                PATH = null;
                JSON = null;
                GET = null;
                SGET = null;
                nSGET = null;
                IMG = null;
                IRIMG = null;
                RSGET = null;
                REFRESH = null;
                FocusMaxPosi = null;
                FocusCurrentPosi = null;
                NearFarFocus = null;
                AutoFocus = null;
                FindFocus = null;
                SetWindowSize = null;
                EXIT_APP = null;
            }
        }

        public class TcpMsgSend
        {
            private IRIP_API_TcpTaskClient2 tcpObj;

            public TcpMsgSend(IRIP_API_TcpTaskClient2 obj)
            {
                tcpObj = obj;
            }

            /// <summary>  
            /// <br>设置通讯消息协议类型</br>
            /// <br>回复消息解析到MsgParse.MSG_TYPE对象</br>
            /// <para>@param type 协议类型: 0：简要字符串命令，1：JSON格式命令（使用包含数据头的消息协议）</para>
            /// </summary> 
            public int SET_MSG_TYPE(int type, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "SET_MSG_TYPE[" + type.ToString() + "]";
                    return tcpObj.SendMsgNoProtocol(msg);
                }

                msg = "{\"action\":\"SET_MSG_TYPE\",\"type\":" + type.ToString() + "}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>设置通讯消息编码类型</br>
            /// <br>回复消息解析到MsgParse.CODING对象</br>
            /// <para>@param coding 编码类型: "gbk", "utf-8"</para>
            /// </summary> 
            public int SET_CODING(string coding, bool noProtocol = false)
            {
                if (coding == null)
                {
                    return -1;
                }
                coding = coding.ToLower();
                if (coding != "gbk" && coding != "utf-8")
                {
                    coding = "gbk";
                }
                string msg;
                if (noProtocol)
                {
                    msg = "SET_CODING[" + coding + "]";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"SET_CODING\",\"coding\":\"" + coding + "\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>获取基本信息</br>
            /// <br>回复消息解析到MsgParse.INFO对象</br>
            /// </summary> 
            public int GET_INFO(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "GET_INFO";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"GET_INFO\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>设置测量模板文件</br>
            /// <br>回复消息解析到MsgParse.PATH对象</br>
            /// <para>@param path 模板文件完整路径</para>
            /// <para>@param offsetX 加载模板测量元素水平方向偏移像素</para>
            /// <para>@param offsetY 加载模板测量元素垂直方向偏移像素</para>
            /// </summary> 
            public int PATH(string path, int offsetX = 0, int offsetY = 0, bool noProtocol = false)
            {
                if (path == null)
                {
                    return -1;
                }
                string msg;
                if (noProtocol)
                {
                    msg = "PATH" + path;
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"PATH\",\"str\":\"" + path.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\",\"offsetX\":" + offsetX.ToString() + ",\"offsetY\":" + offsetY.ToString() + "}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>设置测量模板JSON数据</br>
            /// <br>回复消息解析到MsgParse.JSON对象</br>
            /// <para>@param jsonStr JSON字符串数据</para>
            /// </summary> 
            public int JSON(string jsonStr, bool noProtocol = false)
            {
                if (jsonStr == null || jsonStr.Length < 2)
                {
                    return -1;
                }
                string msg;
                if (noProtocol)
                {
                    msg = "JSON" + jsonStr;
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"JSON\",\"obj\":" + jsonStr + "}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>获取测量结果</br>
            /// <br>回复消息包含测量信息解析到MsgParse.GET对象</br>
            /// </summary> 
            public int GET(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "GET";
                    return tcpObj.SendMsgNoProtocol(msg);
                }

                msg = "{\"action\":\"GET\"}";
                return tcpObj.SendMsg(msg);

            }

            /// <summary>  
            /// <br>连续获取测量结果</br>
            /// <br>回复消息解析到MsgParse.SGET对象</br>
            /// <br>回复测量信息解析到MsgParse.Measurement对象，action: RSGET</br>
            /// <para>@param interrupted 间隔N帧回传一次数据，取值范围：0~999</para>
            /// </summary> 
            public int SGET(int interrupted = 0, bool noProtocol = false)
            {
                interrupted = interrupted < 0 ? 0 : interrupted > 999 ? 999 : interrupted;
                string msg;
                if (noProtocol)
                {
                    msg = "SGET" + interrupted.ToString();
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"SGET\",\"type\":" + interrupted.ToString() + "}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>取消连续获取测量结果</br>
            /// <br>回复消息解析到MsgParse.nSGET对象</br>
            /// </summary> 
            public int nSGET(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "!SGET";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"!SGET\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>保存测量结果图像</br>
            /// <br>回复消息解析到MsgParse.IMG对象</br>
            /// <para>@param path 图像保存路径，若不指定保存到软件默认相册跟目录</para>
            /// <para>@param file 文件名，若不指定默认为"img.jpg"</para>
            /// </summary> 
            public int IMG(string path = "", string file = "", bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "IMG";
                    if (path.Length > 0)
                    {
                        msg += path;
                        if (file.Length > 0)
                        {
                            msg += "|" + file;

                        }
                    }
                    else
                    {
                        if (file.Length > 0)
                        {
                            msg += file;

                        }
                    }
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"IMG\",\"path\":\"" + path.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\",\"file\":\"" + file.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>保存红外图像</br>
            /// <br>回复消息解析到MsgParse.IRIMG对象</br>
            /// <para>@param path 图像保存路径，若不指定保存到软件默认相册目录</para>
            /// </summary> 
            public int IRIMG(string path = null, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "IRIMG";
                    if (path != null && path.Length > 0)
                    {
                        msg += path;
                    }
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                if (path == null || path == "")
                {
                    msg = "{\"action\":\"IRIMG\"}";
                }
                else
                {
                    msg = "{\"action\":\"IRIMG\",\"path\":\"" + path.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"}";
                }
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>打快门校正、设置定时打快门校正时间</br>
            /// <br>回复消息解析到MsgParse.REFRESH对象</br>
            /// <para>@param delayed 参数值&lt;0时执行打快门校正，&gt;=30时为设置自动打快门时间，0~29时关闭自动打快门校正</para>
            /// </summary> 
            public int REFRESH(int delayed = -1, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    if (delayed < 0)
                    {
                        msg = "REFRESH";
                    }
                    else
                    {
                        msg = "REFRESH[" + delayed.ToString() + "]";
                    }
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                if (delayed < 0)
                {
                    msg = "{\"action\":\"REFRESH\"}";
                }
                else
                {
                    msg = "{\"action\":\"REFRESH\",\"delayed\":" + delayed.ToString() + "}";
                }
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>获取最大对焦位置</br>
            /// <br>回复消息解析到MsgParse.FocusMaxPosi对象</br>
            /// </summary> 
            public int GetFocusMaxPosi(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "GetFocusMaxPosi";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"GetFocusMaxPosi\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>获取当前对焦位置</br>
            /// <br>回复消息解析到MsgParse.FocusCurrentPosi对象</br>
            /// </summary> 
            public int GetFocusCurrentPosi(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "GetFocusCurrentPosi";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"GetFocusCurrentPosi\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>远近焦控制</br>
            /// <br>回复消息解析到MsgParse.NearFarFocus对象</br>
            /// <para>@param command 聚焦参数</para>
            /// <br>     FOCUSZERO_TRY(-2), 忽略错误强制重试置零(posi&lt;=0时置零后移动到内置保存指定位置, posi&gt;0时为临时指定置零后移动到的位置)</br>
            /// <br>     FOCUSZERO(-1): 置零位(posi&lt;=0时置零后移动到内置保存指定位置, posi&gt;0时为临时指定置零后移动到的位置)</br>
            /// <br>     STOPFOCUS(0): 停止对焦 </br>
            /// <br>     FARFOCUS(1): 远焦触发, posi&lt;=0自动加速度, posi&gt;0连续对焦速度, 最大32</br>
            /// <br>     NEARFOCUS(2): 近焦触发, posi&lt;=0自动加速度, posi&gt;0连续对焦速度, 最大32</br>
            /// <br>     AUTOFOCUS(3): 自动对焦, 取画面宽高的3/4矩形形居中图像区域作为对焦检测区域</br>
            /// <br>     FOCUSSTATUS(4): 查询自动对焦状态</br>
            /// <br>     FOCUSPOSI(5): 对焦到指定位置, posi为目标位置</br>
            /// <br>     FOCUSMOVE(6): 前后移动聚焦位置, posi为移动步数，&lt;0表示向近焦位置移动，&gt;0表示向远焦位置移动</br>
            /// <para>@param posi 位置/步数</para>
            /// </summary> 
            public int NearFarFocus(int command, int posi = 0, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "NearFarFocus[" + command.ToString() + "," + posi.ToString() + "]";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"NearFarFocus\",\"command\":" + command.ToString() + ",\"posi\":" + posi.ToString() + "}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>远近焦控制</br>
            /// <br>回复消息解析到MsgParse.AutoFocus对象</br>
            /// </summary> 
            public int AutoFocus(bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "AutoFocus";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"AutoFocus\"}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>自动对焦</br>
            /// <br>回复消息解析到MsgParse.AutoFocus对象</br>
            /// <para>@param rect 对焦检测区域, 该参数为NULL或width或height&lt;=0时取画面宽高的3/4矩形形居中图像区域</para>
            /// </summary> 
            public int AutoFocus(Int32Rect rect, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    if (rect.Width <= 0 || rect.Height <= 0)
                    {
                        msg = "AutoFocus";
                    }
                    else
                    {
                        msg = "AutoFocus[" + rect.X.ToString() + "," + rect.Y.ToString() + "," + rect.Width.ToString() + "," + rect.Height.ToString() + "]";
                    }
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                if (rect.Width <= 0 || rect.Height <= 0)
                {
                    msg = "{\"action\":\"AutoFocus\"}";
                }
                else
                {
                    msg = "{\"action\":\"AutoFocus\",\"rect\":{\"x\":" + rect.X.ToString() + ",\"y\":" + rect.Y.ToString() + ",\"width\":" + rect.Width.ToString() + ",\"height\":" + rect.Height.ToString() + "}}";
                }
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>全程寻焦</br>
            /// <br>回复消息解析到MsgParse.FindFocus</br>
            /// <para>@param command 0: 全程寻焦, 1: 自适应（当检测到寻焦过程锐度明显变小时，取最大锐度位置对焦）</para>
            /// <para>@param range 对焦位置搜索行程, range.x, range.y: 对应起始和结束对焦位置范围，若设置值range.x = range.y时则为全程搜索寻焦</para>
            /// <para>@param rect 对焦检测区域, 该参数为NULL或width或height&lt;=0时取画面宽高的3/4矩形形居中图像区域</para>
            /// </summary> 
            public int FindFocus(int command, System.Windows.Point range, Int32Rect rect, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    //简易命令不支持参数，默认command = 0, range = null, rect = null
                    msg = "FindFocus";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                if (rect.Width <= 0 || rect.Height <= 0)
                {
                    msg = "{\"action\":\"FindFocus\",\"command\":" + command.ToString() + ",\"range\":{\"x\":" + range.X.ToString() + ",\"y\":" + range.Y.ToString() + "}}";
                }
                else
                {
                    msg = "{\"action\":\"FindFocus\",\"command\":" + command.ToString() + ",\"range\":{\"x\":" + range.X.ToString() + ",\"y\":" + range.Y.ToString() + "},\"rect\":{\"x\":" + rect.X.ToString() + ",\"y\":" + rect.Y.ToString() + ",\"width\":" + rect.Width.ToString() + ",\"height\":" + rect.Height.ToString() + "}}";
                }
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>设置窗口大小和位置</br>
            /// <br>回复消息解析到MsgParse.SetWindowSize对象</br>
            /// <para>@param rect 窗口坐标</para>
            /// </summary> 
            public int SetWindowSize(Int32Rect rect, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "SetWindowSize[" + rect.X.ToString() + "," + rect.Y.ToString() + "," + rect.Width.ToString() + "," + rect.Height.ToString() + "]";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"SetWindowSize\",\"rect\":{\"x\":" + rect.X.ToString() + ",\"y\":" + rect.Y.ToString() + ",\"width\":" + rect.Width.ToString() + ",\"height\":" + rect.Height.ToString() + "}}";
                return tcpObj.SendMsg(msg);
            }

            /// <summary>  
            /// <br>回复消息解析到MsgParse.EXIT_APP对象</br>
            /// <br>发送退出程序命令</br>
            /// <para>@param type 1: 则忽略未保存的设置强行退出程序</para>
            /// </summary> 
            public int EXIT_APP(int type = 0, bool noProtocol = false)
            {
                string msg;
                if (noProtocol)
                {
                    msg = "REFRESH[" + type.ToString() + "]";
                    return tcpObj.SendMsgNoProtocol(msg);
                }
                msg = "{\"action\":\"EXIT_APP\",\"type\":" + type.ToString() + "}";
                return tcpObj.SendMsg(msg);
            }
        }

        public TcpMsgParse GetTcpMsgParse()
        {
            return tcpMsgParse;
        }

        public TcpMsgSend GetTcpMsgSend()
        {
            return tcpMsgSend;
        }


        private TcpMsgParse tcpMsgParse;
        private TcpMsgSend tcpMsgSend;

        private IRIP_API IRIP;

        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;

            tcpMsgParse = new TcpMsgParse();
            tcpMsgSend = new TcpMsgSend(this);
#if _X86
    api_Connect = _IRIP_API_TcpTaskClient2_Connect;
    api_Disconnect = _IRIP_API_TcpTaskClient2_Disconnect;
    api_IsConnect = _IRIP_API_TcpTaskClient2_IsConnect;
    api_GetConnectIp = _IRIP_API_TcpTaskClient2_GetConnectIp;
    api_GetConnectPort = _IRIP_API_TcpTaskClient2_GetConnectPort;
    api_SetRecvCallback = _IRIP_API_TcpTaskClient2_SetRecvCallback;
    api_SendMsg = _IRIP_API_TcpTaskClient2_SendMsg;
    api_SendMsgNoProtocol = _IRIP_API_TcpTaskClient2_SendMsgNoProtocol;
    api_Extend_Connect = _IRIP_API_TcpTaskClient2_Extend_Connect;
    api_Extend_Disconnect = _IRIP_API_TcpTaskClient2_Extend_Disconnect;
    api_Extend_IsConnect = _IRIP_API_TcpTaskClient2_Extend_IsConnect;
    api_Extend_GetConnectIp = _IRIP_API_TcpTaskClient2_Extend_GetConnectIp;
    api_Extend_GetConnectPort = _IRIP_API_TcpTaskClient2_Extend_GetConnectPort;
    api_Extend_SetRecvCallback = _IRIP_API_TcpTaskClient2_Extend_SetRecvCallback;
    api_Extend_SendACK = _IRIP_API_TcpTaskClient2_Extend_SendACK;
#else
            api_Connect = (IRIP_API_TcpTaskClient2_Connect)dll.Invoke("IRIP_API_TcpTaskClient2_Connect", typeof(IRIP_API_TcpTaskClient2_Connect));
            api_Disconnect = (IRIP_API_TcpTaskClient2_Disconnect)dll.Invoke("IRIP_API_TcpTaskClient2_Disconnect", typeof(IRIP_API_TcpTaskClient2_Disconnect));
            api_IsConnect = (IRIP_API_TcpTaskClient2_IsConnect)dll.Invoke("IRIP_API_TcpTaskClient2_IsConnect", typeof(IRIP_API_TcpTaskClient2_IsConnect));
            api_GetConnectIp = (IRIP_API_TcpTaskClient2_GetConnectIp)dll.Invoke("IRIP_API_TcpTaskClient2_GetConnectIp", typeof(IRIP_API_TcpTaskClient2_GetConnectIp));
            api_GetConnectPort = (IRIP_API_TcpTaskClient2_GetConnectPort)dll.Invoke("IRIP_API_TcpTaskClient2_GetConnectPort", typeof(IRIP_API_TcpTaskClient2_GetConnectPort));
            api_SetRecvCallback = (IRIP_API_TcpTaskClient2_SetRecvCallback)dll.Invoke("IRIP_API_TcpTaskClient2_SetRecvCallback", typeof(IRIP_API_TcpTaskClient2_SetRecvCallback));
            api_SendMsg = (IRIP_API_TcpTaskClient2_SendMsg)dll.Invoke("IRIP_API_TcpTaskClient2_SendMsg", typeof(IRIP_API_TcpTaskClient2_SendMsg));
            api_SendMsgNoProtocol = (IRIP_API_TcpTaskClient2_SendMsgNoProtocol)dll.Invoke("IRIP_API_TcpTaskClient2_SendMsgNoProtocol", typeof(IRIP_API_TcpTaskClient2_SendMsgNoProtocol));
            api_Extend_Connect = (IRIP_API_TcpTaskClient2_Extend_Connect)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_Connect", typeof(IRIP_API_TcpTaskClient2_Extend_Connect));
            api_Extend_Disconnect = (IRIP_API_TcpTaskClient2_Extend_Disconnect)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_Disconnect", typeof(IRIP_API_TcpTaskClient2_Extend_Disconnect));
            api_Extend_IsConnect = (IRIP_API_TcpTaskClient2_Extend_IsConnect)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_IsConnect", typeof(IRIP_API_TcpTaskClient2_Extend_IsConnect));
            api_Extend_GetConnectIp = (IRIP_API_TcpTaskClient2_Extend_GetConnectIp)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_GetConnectIp", typeof(IRIP_API_TcpTaskClient2_Extend_GetConnectIp));
            api_Extend_GetConnectPort = (IRIP_API_TcpTaskClient2_Extend_GetConnectPort)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_GetConnectPort", typeof(IRIP_API_TcpTaskClient2_Extend_GetConnectPort));
            api_Extend_SetRecvCallback = (IRIP_API_TcpTaskClient2_Extend_SetRecvCallback)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_SetRecvCallback", typeof(IRIP_API_TcpTaskClient2_Extend_SetRecvCallback));
            api_Extend_SendACK = (IRIP_API_TcpTaskClient2_Extend_SendACK)dll.Invoke("IRIP_API_TcpTaskClient2_Extend_SendACK", typeof(IRIP_API_TcpTaskClient2_Extend_SendACK));
#endif
        }

#if _X86
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Connect", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Connect(int id, byte[] ip, ushort port, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Disconnect", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Disconnect(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_IsConnect", CallingConvention = CallingConvention.StdCall)]
  static private extern bool _IRIP_API_TcpTaskClient2_IsConnect(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_GetConnectIp", CallingConvention = CallingConvention.StdCall)]
  static private extern IntPtr _IRIP_API_TcpTaskClient2_GetConnectIp(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_GetConnectPort", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_GetConnectPort(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_SetRecvCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_SetRecvCallback(int id, MsgCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_SendMsg", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_SendMsg(int id, byte[] msg);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_SendMsgNoProtocol", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_SendMsgNoProtocol(int id, byte[] msg);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_Connect", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Extend_Connect(int id, byte[] ip, ushort port, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_Disconnect", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Extend_Disconnect(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_IsConnect", CallingConvention = CallingConvention.StdCall)]
  static private extern bool _IRIP_API_TcpTaskClient2_Extend_IsConnect(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_GetConnectIp", CallingConvention = CallingConvention.StdCall)]
  static private extern IntPtr _IRIP_API_TcpTaskClient2_Extend_GetConnectIp(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_GetConnectPort", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Extend_GetConnectPort(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_SetRecvCallback", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Extend_SetRecvCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_TcpTaskClient2_Extend_SendACK", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_TcpTaskClient2_Extend_SendACK(int id);
#endif

        private delegate int IRIP_API_TcpTaskClient2_Connect(int id, byte[] ip, ushort port, RetCallback callback, IntPtr pUser);
        private IRIP_API_TcpTaskClient2_Connect api_Connect;

        private delegate int IRIP_API_TcpTaskClient2_Disconnect(int id);
        private IRIP_API_TcpTaskClient2_Disconnect api_Disconnect;

        private delegate bool IRIP_API_TcpTaskClient2_IsConnect(int id);
        private IRIP_API_TcpTaskClient2_IsConnect api_IsConnect;

        private delegate IntPtr IRIP_API_TcpTaskClient2_GetConnectIp(int id);
        private IRIP_API_TcpTaskClient2_GetConnectIp api_GetConnectIp;

        private delegate int IRIP_API_TcpTaskClient2_GetConnectPort(int id);
        private IRIP_API_TcpTaskClient2_GetConnectPort api_GetConnectPort;

        private delegate int IRIP_API_TcpTaskClient2_SetRecvCallback(int id, MsgCallback callback, IntPtr pUser);
        private IRIP_API_TcpTaskClient2_SetRecvCallback api_SetRecvCallback;

        private delegate int IRIP_API_TcpTaskClient2_SendMsg(int id, byte[] msg);
        private IRIP_API_TcpTaskClient2_SendMsg api_SendMsg;

        private delegate int IRIP_API_TcpTaskClient2_SendMsgNoProtocol(int id, byte[] msg);
        private IRIP_API_TcpTaskClient2_SendMsgNoProtocol api_SendMsgNoProtocol;

        private delegate int IRIP_API_TcpTaskClient2_Extend_Connect(int id, byte[] ip, ushort port, RetCallback callback, IntPtr pUser);
        private IRIP_API_TcpTaskClient2_Extend_Connect api_Extend_Connect;

        private delegate int IRIP_API_TcpTaskClient2_Extend_Disconnect(int id);
        private IRIP_API_TcpTaskClient2_Extend_Disconnect api_Extend_Disconnect;

        private delegate bool IRIP_API_TcpTaskClient2_Extend_IsConnect(int id);
        private IRIP_API_TcpTaskClient2_Extend_IsConnect api_Extend_IsConnect;

        private delegate IntPtr IRIP_API_TcpTaskClient2_Extend_GetConnectIp(int id);
        private IRIP_API_TcpTaskClient2_Extend_GetConnectIp api_Extend_GetConnectIp;

        private delegate int IRIP_API_TcpTaskClient2_Extend_GetConnectPort(int id);
        private IRIP_API_TcpTaskClient2_Extend_GetConnectPort api_Extend_GetConnectPort;

        private delegate int IRIP_API_TcpTaskClient2_Extend_SetRecvCallback(int id, IntPtr frame, RetCallback callback, IntPtr pUser);
        private IRIP_API_TcpTaskClient2_Extend_SetRecvCallback api_Extend_SetRecvCallback;

        private delegate int IRIP_API_TcpTaskClient2_Extend_SendACK(int id);
        private IRIP_API_TcpTaskClient2_Extend_SendACK api_Extend_SendACK;

        /// <summary>
        /// <br>连接TCP测量服务器</br>
        /// <para>@param ip IP地址</para>
        /// <para>@param port 端口号</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int Connect(string ip, ushort port, RetCallback callback, IntPtr pUser)
        {
            if (api_Connect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Connect(IRIP.GetID(), Encoding.UTF8.GetBytes(ip), port, callback, pUser);
        }

        /// <summary>
        /// <br>断开服务器连接</br>
        /// </summary>
        public int Disconnect()
        {
            if (api_Disconnect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Disconnect(IRIP.GetID());
        }

        /// <summary>
        /// <br>是否已连接服务器</br>
        /// </summary>
        public bool IsConnect()
        {
            if (api_IsConnect == null)
            {
            }
            return api_IsConnect(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取已连接的ip地址</br>
        /// <para>@return ip地址</para>
        /// </summary>
        public string GetConnectIp()
        {
            if (api_GetConnectIp == null)
            {
                return "";
            }
            return Marshal.PtrToStringAnsi(api_GetConnectIp(IRIP.GetID()));
        }

        /// <summary>
        /// <br>获取已连接的端口号</br>
        /// <para>@return 端口号</para>
        /// </summary>
        public int GetConnectPort()
        {
            if (api_GetConnectPort == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetConnectPort(IRIP.GetID());
        }

        /// <summary>
        /// <br>设置接收消息回调函数</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int SetRecvCallback(MsgCallback callback, IntPtr pUser)
        {
            if (api_SetRecvCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetRecvCallback(IRIP.GetID(), callback, pUser);
        }

        /// <summary>
        /// <br>发送消息</br>
        /// <para>@param data 发送的消息（文本）</para>
        /// </summary>
        public int SendMsg(string msg)
        {
            if (api_SendMsg == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SendMsg(IRIP.GetID(), Encoding.UTF8.GetBytes(msg));
        }

        /// <summary>
        /// <br>无打包协议发送文本消息裸数据</br>
        /// <para>@param data 发送的消息（文本）</para>
        /// </summary>
        public int SendMsgNoProtocol(string msg)
        {
            if (api_SendMsgNoProtocol == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SendMsgNoProtocol(IRIP.GetID(), Encoding.UTF8.GetBytes(msg));
        }

        /// <summary>
        /// <br>连接TCP测量扩展服务器</br>
        /// <para>@param ip IP地址</para>
        /// <para>@param port 端口号</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int Extend_Connect(string ip, ushort port, RetCallback callback, IntPtr pUser)
        {
            if (api_Extend_Connect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Extend_Connect(IRIP.GetID(), Encoding.UTF8.GetBytes(ip), port, callback, pUser);
        }

        /// <summary>
        /// <br>断开扩展服务器连接</br>
        /// </summary>
        public int Extend_Disconnect()
        {
            if (api_Extend_Disconnect == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Extend_Disconnect(IRIP.GetID());
        }

        /// <summary>
        /// <br>是否已连接扩展服务器</br>
        /// </summary>
        public bool Extend_IsConnect()
        {
            if (api_Extend_IsConnect == null)
            {
            }
            return api_Extend_IsConnect(IRIP.GetID());
        }

        /// <summary>
        /// <br>获取扩展服务器已连接的ip地址</br>
        /// </summary>
        public string Extend_GetConnectIp()
        {
            if (api_Extend_GetConnectIp == null)
            {
                return "";
            }
            return Marshal.PtrToStringAnsi(api_Extend_GetConnectIp(IRIP.GetID()));
        }

        /// <summary>
        /// <br>获取扩展服务器已连接的端口</br>
        /// </summary>
        public int Extend_GetConnectPort()
        {
            if (api_Extend_GetConnectPort == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Extend_GetConnectPort(IRIP.GetID());
        }

        /// <summary>
        /// <br>接收扩展服务器数据回调</br>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser 自定义回调指针</para>
        /// </summary>
        public int Extend_SetRecvCallback(IntPtr frame, RetCallback callback, IntPtr pUser)
        {
            if (api_Extend_SetRecvCallback == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Extend_SetRecvCallback(IRIP.GetID(), frame, callback, pUser);
        }

        /// <summary>
        /// <br>向扩展服务器发送应答包</br>
        /// </summary>
        public int Extend_SendACK()
        {
            if (api_Extend_SendACK == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Extend_SendACK(IRIP.GetID());
        }

    }
}
