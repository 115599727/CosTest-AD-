using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace IRIP_API_LIB
{

    //C# IRIP_API
    //Ver: 1.0.0.6
    //Date: 2023/1/13

    //继承IRIP_API
    /// <summary>  
    /// <br>IRIP_API入口</br>
    /// </summary>
    public class API : IRIP_API
    {
    }

    /// <summary>  
    /// <br>IRIP_API入口</br>
    /// </summary>
    public class IRIP_API
    {

        /// <summary>  
        /// <br>库接口返回状态码</br>
        /// </summary>
        public static class RET
        {
            /// <summary>  
            /// <br>正常（需要直接返回数据的部分接口不适用）</br>
            /// </summary>
            public const int OK = 0;

            /// <summary>  
            /// <br>未许可</br>
            /// </summary>
            public const int UNAUTHORIZED = -2147483647;

            /// <summary>  
            /// <br>库实例ID未创建（需先使用IRIP_API_init接口创建实例）</br>
            /// </summary>
            public const int INSTANCE_NOT_CREATED = -2147483646;

            /// <summary>  
            /// <br>目标实例对象未初始化（不是所有接口函数调用都会初始化对象，有执行循序要求，比如某些需要先启动相机才能操作的接口）</br>
            /// </summary>
            public const int TARGET_NOT_CREATED = -2147483645;

            /// <summary>  
            /// <br>库函数对应接口不存在或加载失败（非DLL库返回结果）</br>
            /// </summary>
            public const int API_FUNCTION_NULL = -1000000000;
        }

        /// <summary>  
        /// <br>动态连接库文件</br>
        /// </summary>
        public const string LIB_FILE = "IRIP.dll";

        /// <summary>  
        /// <br>SDK许可证</br>
        /// </summary>
        public static string LICENCE_CODE = "";


        /// <summary>  
        /// <br>自动打快门时间数组(单位：秒)</br>
        /// </summary> 
        public static readonly int[] AUTO_REFRESH_TIME = { 0, 30, 60, 180, 300, 600, 1800, 3600 };

        /// <summary>  
        /// <br>红外数据帧申请内存配置项</br>
        /// </summary>
        public static class OPTION
        {
            /// <summary>  
            /// <br>图像数据</br>
            /// </summary>
            public const int IMGA = 0x00000001;
            /// <summary>  
            /// <br>JSON文本数据</br>
            /// </summary>
            public const int JSON = 0x00000002;
            /// <summary>  
            /// <br>IDAT8位灰度数据</br>
            /// </summary>
            public const int IDAT = 0x00000004;
            /// <summary>  
            /// <br>32位调色板数据</br>
            /// </summary>
            public const int PLTE = 0x00000008;
            /// <summary>  
            /// <br>8位动态调色板数据</br>
            /// </summary>
            public const int DPLT = 0x00000010;
            /// <summary>  
            /// <br>16位温度数据</br>
            /// </summary>
            public const int TEMP = 0x00000020;
            /// <summary>  
            /// <br>16位温度数据2(滤波)</br>
            /// </summary>
            public const int TEMP2 = 0x00000040;
            /// <summary>  
            /// <br>分段校准数据</br>
            /// </summary>
            public const int CALI = 0x00000080;
            /// <summary>  
            /// <br>动态调色板温度映射数据</br>
            /// </summary>
            public const int DTHR = 0x00000100;
        }

        /// <summary>  
        /// <br>红外图像输出选项</br>
        /// </summary>
        public class IR
        {
            /// <summary>  
            /// <br>ARGB（根据调色板决定，调色板可设置高低温透明度、透明度预乘、ARGB/ABGR反转）</br>
            /// </summary>
            public const uint ARGB = 0x00;
            /// <summary>  
            /// <br>BMP（带调色板的8位BMP图像）</br>
            /// </summary>
            public const uint BMP = 0x01;
            /// <summary>  
            /// <br>PNG（包含透明度的带调色板的8位PNG图像，PNG图像调色板为ARGB格式，透明度不预乘）</br>
            /// </summary>
            public const uint PNG = 0x02;
        }

        /// <summary>  
        /// <br>可见光图像输出选项</br>
        /// </summary>
        public static class VI
        {
            /// <summary>  
            /// <br>RAW（根据相机配置不同可能为YUV,RGB,RGB555等格式的数据）</br>
            /// </summary>
            public const uint RAW = 0x00;
            /// <summary>  
            /// <br>BMP（24位BMP图像）</br>
            /// </summary>
            public const uint BMP = 0x01;
            /// <summary>  
            /// <br>RGBA</br>
            /// </summary>
            public const uint RGBA = 0x02;
            /// <summary>  
            /// <br>BGRA</br>
            /// </summary>
            public const uint BGRA = 0x03;
            /// <summary>  
            /// <br>ARGB</br>
            /// </summary>
            public const uint ARGB = 0x04;
            /// <summary>  
            /// <br>ABGR</br>
            /// </summary>
            public const uint ABGR = 0x05;
        }


        /// <summary>  
        /// <br>温度单位选项</br>
        /// </summary>
        public static class UNIT
        {
            /// <summary>  
            /// <br>摄氏度（°C）</br>
            /// </summary>
            public const uint CENTIGRADE = 0x00;
            /// <summary>  
            /// <br>华氏度（°F）</br>
            /// </summary>
            public const uint FAHRENHEIT = 0x01;
            /// <summary>  
            /// <br>开尔文（K）</br>
            /// </summary>
            public const uint KELVIN = 0x02;
            /// <summary>  
            /// <br>温度符号数组，下标对应ID选择</br>
            /// </summary>
            public static readonly string[] UNIT_SYMBOLS = { "°C", "°F", "K" };
            /// <summary>  
            /// <br>中文名称数组</br>
            /// </summary>
            public static readonly string[] UNIT_NAME = { "摄氏度", "华氏度", "开尔文" };
        }


        /// <summary>  
        /// <br>温度单位选项</br>
        /// </summary>
        public static class TEMPTYPE
        {
            /// <summary>  
            /// <br>常温段</br>
            /// </summary>
            public const uint NORMOL = 0x00;
            /// <summary>  
            /// <br>中温段</br>
            /// </summary>
            public const uint MEDIUM = 0x01;
            /// <summary>  
            /// <br>高温段</br>
            /// </summary>
            public const uint HIGH = 0x01;
            /// <summary>  
            /// <br>中文名称数组</br>
            /// </summary>
            public static readonly string[] TEMPTYPE_NAME = { "常温段", "中温段", "高温段" };
        }

        /// <summary>  
        /// 图像旋转选项</br>
        /// </summary>
        public static class ROTATE
        {
            /// <summary>  
            /// <br>符号</br>
            /// </summary>
            public static string SYMBOLS = "°";
            /// <summary>  
            /// <br>旋转角度：0°</br>
            /// </summary>
            public const int ANGLE_0 = 0;
            /// <summary>  
            /// <br>旋转角度：90°</br>
            /// </summary>
            public const int ANGLE_90 = 90;
            /// <summary>  
            /// <br>旋转角度：180°</br>
            /// </summary>
            public const int ANGLE_180 = 180;
            /// <summary>  
            /// <br>旋转角度：270°</br>
            /// </summary>
            public const int ANGLE_270 = 270;
            /// <summary>  
            /// <br>图旋转角度选择列表数组</br>
            /// </summary>
            public static readonly int[] ROTATE_ANGLE = { ANGLE_0, ANGLE_90, ANGLE_180, ANGLE_270 };
        }

        /// <summary>  
        /// <br>图像插值选项</br>
        /// </summary>
        public static class INTERPOLATION
        {
            /// <summary>  
            /// <br>最近邻插值</br>
            /// </summary>
            public const int NEAREST_NEIGHBOR = 0;
            /// <summary>  
            /// <br>双线性插值</br>
            /// </summary>
            public const int BILINERA = 1;
        }

        /// <summary>  
        /// <br>温度测量模式选项</br>
        /// </summary>
        public static class MD
        {
            /// <summary>  
            /// <br>中心温度</br>
            /// </summary>
            public static class CENTER
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = -1;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "center";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "中心温度";
            }
            /// <summary>  
            /// <br>点测温</br>
            /// </summary>
            public static class SPOT
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = 0;
                /// <summary>  
                /// <br>SID JSON数据测量标记显示顺序ID</br>
                /// </summary>
                public const int SID = 1;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "spot";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "点";
            }
            /// <summary>  
            /// <br>全幅高低温</br>
            /// </summary>
            public static class HILO
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = 1;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "hilo";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "高低温";
            }
            /// <summary>  
            /// <br>矩形区域测温</br>
            /// </summary>
            public static class AREA
            {
                /// <summary>  
                /// <br>ID
                /// </summary>
                public const int ID = 2;
                /// <summary>  
                /// <br>SID JSON数据测量标记显示顺序ID</br>
                /// </summary>
                public const int SID = 3;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "area";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "矩形";
            }
            /// <summary>  
            /// <br>线测温</br>
            /// </summary>
            public static class LINE
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = 3;
                /// <br><summary>  
                /// <br>SID JSON数据测量标记显示顺序ID</br>
                /// </summary>
                public const int SID = 2;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "line";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "线";
            }
            /// <summary>  
            /// <br>多边形测温</br>
            /// </summary>
            public static class POLY
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = 6;
                /// <summary>  
                /// <br>SID JSON数据测量标记显示顺序ID</br>
                /// </summary>
                public const int SID = 4;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "poly";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "多边形";
            }
            /// <summary>  
            /// <br>圆,椭圆形测温</br>
            /// </summary>
            // （1.0.0.7新增）
            public static class CIRC
            {
                /// <summary>  
                /// <br>ID</br>
                /// </summary>
                public const int ID = 7;
                /// <summary>  
                /// <br>SID JSON数据测量标记显示顺序ID</br>
                /// </summary>
                public const int SID = 5;
                /// <summary>  
                /// <br>类型</br>
                /// </summary>
                public const string TYPE = "circ";
                /// <summary>  
                /// <br>名称</br>
                /// </summary>
                public const string NAME = "圆/椭圆";
            }
        }

        /// <summary>  
        /// <br>UVC图像参数调整类型标识</br>
        /// </summary>
        public static class VideoProcAmpProperty
        {
            /// <summary>  
            /// <br>亮度</br>
            /// </summary>
            public const int VideoProcAmp_Brightness = 0;
            /// <summary>  
            /// <br>对比度</br>
            /// </summary>
            public const int VideoProcAmp_Contrast = 1;
            /// <summary>  
            /// <br>色调</br>
            /// </summary>
            public const int VideoProcAmp_Hue = 2;
            /// <summary>  
            /// <br>饱和度</br>
            /// </summary>
            public const int VideoProcAmp_Saturation = 3;
            /// <summary>  
            /// <br>锐度</br>
            /// </summary>
            public const int VideoProcAmp_Sharpness = 4;
            /// <summary>  
            /// <br>伽马，灰度系数</br>
            /// </summary>
            public const int VideoProcAmp_Gamma = 5;
            /// <summary>  
            /// <br>颜色启用</br>
            /// </summary>
            public const int VideoProcAmp_ColorEnable = 6;
            /// <summary>  
            /// <br>白平衡</br>
            /// </summary>
            public const int VideoProcAmp_WhiteBalance = 7;
            /// <summary>  
            /// <br>背光补偿，逆光对比</br>
            /// </summary>
            public const int VideoProcAmp_BacklightCompensation = 8;
            /// <summary>  
            /// <br>增益</br>
            /// </summary>
            public const int VideoProcAmp_Gain = 9;
        }

        /// <summary>  
        /// <br>UVC图像自动手动参数判断标识</br>
        /// </summary>
        public static class VideoProcAmpFlags
        {
            /// <summary>  
            /// <br>自动</br>
            /// </summary>
            public const int CameraControl_Flags_Auto = 1;
            /// <summary>  
            /// <br>手动</br>
            /// </summary>
            public const int CameraControl_Flags_Manual = 2;
        }

        /// <summary>  
        /// <br>UVC相继控制类型标识</br>
        /// </summary>
        public static class CameraControlProperty
        {
            /// <summary>  
            /// <br>平移</br>
            /// </summary>
            public const int CameraControl_Pan = 1;
            /// <summary>  
            /// <br>倾斜</br>
            /// </summary>
            public const int CameraControl_Tilt = 2;
            /// <summary>  
            /// <br>滚动</br>
            /// </summary>
            public const int CameraControl_Roll = 3;
            /// <summary>  
            /// <br>缩放</br>
            /// </summary>
            public const int CameraControl_Zoom = 4;
            /// <summary>  
            /// <br>曝光</br>
            /// </summary>
            public const int CameraControl_Exposure = 5;
            /// <summary>  
            /// <br>光圈</br>
            /// </summary>
            public const int CameraControl_Iris = 6;
            /// <summary>  
            /// <br对焦</br>
            /// </summary>
            public const int CameraControl_Focus = 7;
        }

        /// <summary>  
        /// <br>UVC相继控制自动手动参数判断标识</br>
        /// </summary>
        public static class CameraControlFlags
        {
            /// <summary>  
            /// <br>自动</br>
            /// </summary>
            public const int CameraControl_Flags_Auto = 1;
            /// <summary>  
            /// <br>手动</br>
            /// </summary>
            public const int CameraControl_Flags_Manual = 2;
        }

        /// <summary>  
        /// <br>CopyIR帧结构体数据</br>
        /// </summary>  
        public static void CopyFrameData(IntPtr src, ref CallbackFrameData2 dest)
        {
            CallbackFrameData frame = (CallbackFrameData)Marshal.PtrToStructure(src, typeof(CallbackFrameData));
            dest.index = frame.index;
            dest.histo = frame.histo;
            dest.fpaTemp = frame.fpaTemp;
            dest.width = frame.width;
            dest.height = frame.height;
            dest.time = frame.time;
            dest.delay = frame.delay;
            if(frame.IMGA_MLen > 0)
            {
                if(dest.IMGA == null || dest.IMGA.Length != frame.IMGA_MLen)
                {
                    dest.IMGA = new byte[frame.IMGA_MLen];
                }
                if (frame.IMGA_Len > 0)
                {
                    Marshal.Copy(frame.IMGA, dest.IMGA, 0, (int)frame.IMGA_Len);
                }
            }
            dest.IMGA_MLen = frame.IMGA_MLen;
            dest.IMGA_Len = frame.IMGA_Len;
            dest.IMGA_W = frame.IMGA_W;
            dest.IMGA_H = frame.IMGA_H;
            dest.IMGA_Type = frame.IMGA_Type;
            dest.rotated = frame.rotated;
            if (frame.jSON_MLen > 0)
            {
                if (dest.jSON == null || dest.jSON.Length != frame.jSON_MLen)
                {
                    dest.jSON = new byte[frame.jSON_MLen];
                }
                if(frame.jSON_Len > 0)
                {
                    Marshal.Copy(frame.jSON, dest.jSON, 0, (int)frame.jSON_Len);
                    if (frame.jSON_Len < frame.jSON_MLen)
                    {
                        dest.jSON[frame.jSON_Len] = 0; //文本结束符
                    }
                }
            }
            dest.jSON_MLen = frame.jSON_MLen;
            dest.jSON_Len = frame.jSON_Len;
            if (frame.IDAT_MLen > 0)
            {
                if (dest.IDAT == null || dest.IDAT.Length != frame.IDAT_MLen)
                {
                    dest.IDAT = new byte[frame.IDAT_MLen];
                }
                if (frame.IDAT_Len > 0)
                {
                    Marshal.Copy(frame.IDAT, dest.IDAT, 0, (int)frame.IDAT_Len);
                }
            }
            dest.IDAT_MLen = frame.IDAT_MLen;
            dest.IDAT_Len = frame.IDAT_Len;
            if (frame.PLTE_MLen > 0)
            {
                if (dest.PLTE == null || dest.PLTE.Length != frame.PLTE_MLen)
                {
                    dest.PLTE = new uint[frame.PLTE_MLen];
                }
                if (frame.PLTE_Len > 0)
                {
                    //由于Marshal.Copy不支持拷贝uint[]类型，需要手动复制
                    IntPtr PLTE = frame.PLTE;
                    for (int i = 0; i < frame.PLTE_Len; i++)
                    {
                        dest.PLTE[i] = (uint)Marshal.ReadInt32(PLTE); // 读取一个uint的大小（4字节）
                        PLTE = IntPtr.Add(PLTE, sizeof(uint)); // 移动到下一个uint
                    }
                }
            }
            dest.PLTE_MLen = frame.PLTE_MLen;
            dest.PLTE_Len = frame.PLTE_Len;
            if (frame.dPLT_MLen > 0)
            {
                if (dest.dPLT == null || dest.dPLT.Length != frame.dPLT_MLen)
                {
                    dest.dPLT = new byte[frame.dPLT_MLen];
                }
                if (frame.dPLT_Len > 0)
                {
                    Marshal.Copy(frame.dPLT, dest.dPLT, 0, (int)frame.dPLT_Len);
                }
            }
            dest.dPLT_MLen = frame.dPLT_MLen;
            dest.dPLT_Len = frame.dPLT_Len;
            if (frame.tEMP_MLen > 0)
            {
                if (dest.tEMP == null || dest.tEMP.Length != frame.tEMP_MLen)
                {
                    dest.tEMP = new short[frame.tEMP_MLen];
                }
                if (frame.tEMP_Len > 0)
                {
                    Marshal.Copy(frame.tEMP, dest.tEMP, 0, (int)frame.tEMP_Len);
                }
            }
            dest.tEMP_MLen = frame.tEMP_MLen;
            dest.tEMP_Len = frame.tEMP_Len;
            if (frame.tEMP2_MLen > 0)
            {
                if (dest.tEMP2 == null || dest.tEMP2.Length != frame.tEMP2_MLen)
                {
                    dest.tEMP2 = new short[frame.tEMP2_MLen];
                }
                if (frame.tEMP2_Len > 0)
                {
                    Marshal.Copy(frame.tEMP2, dest.tEMP2, 0, (int)frame.tEMP2_Len);
                }
            }
            dest.tEMP2_MLen = frame.tEMP2_MLen;
            dest.tEMP2_Len = frame.tEMP2_Len;
            if (frame.cALI_MLen > 0)
            {
                if (dest.cALI == null || dest.cALI.Length != frame.cALI_MLen)
                {
                    dest.cALI = new byte[frame.cALI_MLen];
                }
                if (frame.cALI_Len > 0)
                {
                    Marshal.Copy(frame.cALI, dest.cALI, 0, (int)frame.cALI_Len);
                }
            }
            dest.cALI_MLen = frame.cALI_MLen;
            dest.cALI_Len = frame.cALI_Len;
            if (frame.dTHR_MLen > 0)
            {
                if (dest.dTHR == null || dest.dTHR.Length != frame.dTHR_MLen)
                {
                    dest.dTHR = new int[frame.dTHR_MLen];
                }
                if (frame.dTHR_Len > 0)
                {
                    Marshal.Copy(frame.dTHR, dest.dTHR, 0, (int)frame.dTHR_Len);
                }
            }
            dest.dTHR_MLen = frame.dTHR_MLen;
            dest.dTHR_Len = frame.dTHR_Len;
            dest.tempBits = frame.tempBits;
            dest.tempDiv = frame.tempDiv;
            dest.tempOffset = frame.tempOffset;
        }

        /// <summary>  
        /// <br>CopyVI帧结构体数据</br>
        /// </summary>  
        public static void CopyFrameData(IntPtr src, VICallbackFrameData2 dest)
        {
            VICallbackFrameData frame = (VICallbackFrameData)Marshal.PtrToStructure(src, typeof(VICallbackFrameData));
            dest.index = frame.index;
            dest.width = frame.width;
            dest.height = frame.height;
            dest.time = frame.time;
            if (frame.IMGA_MLen > 0)
            {
                if (dest.IMGA == null || dest.IMGA.Length != frame.IMGA_MLen)
                {
                    dest.IMGA = new byte[frame.IMGA_MLen];
                }
                if (frame.IMGA_Len > 0)
                {
                    Marshal.Copy(frame.IMGA, dest.IMGA, 0, (int)frame.IMGA_Len);
                }
            }
            dest.IMGA_MLen = frame.IMGA_MLen;
            dest.IMGA_Len = frame.IMGA_Len;
            dest.IMGA_W = frame.IMGA_W;
            dest.IMGA_H = frame.IMGA_H;
            dest.IMGA_Type = frame.IMGA_Type;
            dest.rotated = frame.rotated;
            dest.bitCount = frame.bitCount;
        }

        /// <summary>  
        /// <br>Copy Irip图像渲染配置参数 JSON => STRUCT</br>
        /// </summary>  
        public static void CopyIripConfig(IR_JSON_IripConfig src, ref IRIP_Config dest)
        {
            dest.hpfMode = src.hpfMode;
            dest.isDynamicExposure = src.isDynamicExposure;
            dest.isNightVision = src.isNightVision;
            dest.hpfAlpha = src.hpfAlpha;
            dest.ignoreRatio = src.ignoreRatio;
            dest.isAutoTmax = src.isAutoTmax;
            dest.isAutoTmin = src.isAutoTmin;
            dest.Tmax = src.Tmax;
            dest.Tmin = src.Tmin;
            dest.gainLimit = src.gainLimit;
            dest.gainValue = src.gainValue;
            dest.isUpsidedown = src.isUpsidedown;
            dest.isSharpness = src.isSharpness;
            dest.DDEType = src.DDEType;
            dest.isDenoise = src.isDenoise;
        }

        /// <summary>  
        /// <br>Copy Irip图像渲染配置参数 STRUCT => JSON</br>
        /// </summary>  
        public static void CopyIripConfig(ref IRIP_Config src, IR_JSON_IripConfig dest)
        {
            dest.hpfMode = src.hpfMode;
            dest.isDynamicExposure = src.isDynamicExposure;
            dest.isNightVision = src.isNightVision;
            dest.hpfAlpha = src.hpfAlpha;
            dest.ignoreRatio = src.ignoreRatio;
            dest.isAutoTmax = src.isAutoTmax;
            dest.isAutoTmin = src.isAutoTmin;
            dest.Tmax = src.Tmax;
            dest.Tmin = src.Tmin;
            dest.gainLimit = src.gainLimit;
            dest.gainValue = src.gainValue;
            dest.isUpsidedown = src.isUpsidedown;
            dest.isSharpness = src.isSharpness;
            dest.DDEType = src.DDEType;
            dest.isDenoise = src.isDenoise;
        }


        //继承相关工具类
        /// <summary>  
        /// <br>调色板类</br>
        /// </summary>
        public class Palette : IRIP_API_Palette { }
        /// <summary>  
        /// <br>图像渲染类</br>
        /// </summary>
        public class Reader : IRIP_API_Reader { }
        /// <summary>  
        /// <br>红外图像/视频类</br>
        /// </summary>
        public class IRImage : IRIP_API_IRImage { }
        /// <summary>  
        /// <br>红外相机类</br>
        /// </summary>
        public class IRCamera : IRIP_API_IRCamera { }
        /// <summary>  
        /// <br>NetUSB红外相机类</br>
        /// </summary>
        public class NetIRCamera : IRIP_API_NetIRCamera { }
        /// <summary>  
        /// <br>HJK网络红外相机类</br>
        /// </summary>
        public class HJKCamera : IRIP_API_HJKCamera { }
        /// <summary>  
        /// <br>可见光相机类</br>
        /// </summary>
        public class VICamera : IRIP_API_VICamera { }
        /// <summary>  
        /// <br>TH红外相机类</br>
        /// </summary>
        public class THCamera : IRIP_API_THCamera { }
        /// <summary>  
        /// <br>温度测量TCP服务器类</br>
        /// </summary>
        public class MDTcpServer : IRIP_API_MDTcpServer { }
        /// <summary>  
        /// <br>温度测量TCP服务器类</br>
        /// </summary>
        public class TcpTaskClient2 : IRIP_API_TcpTaskClient2 { }


        /// <summary>  
        /// <br>调色板</br>
        /// </summary>
        private Palette palette;
        /// <summary>  
        /// <br>图像渲染</br>
        /// </summary>
        private Reader reader;
        /// <summary>  
        /// <br>红外图像, 视频</br>
        /// </summary>
        private IRImage irImage;
        /// <summary>  
        /// <br>红外相机</br>
        /// </summary>
        private IRCamera irCamera;
        /// <summary>  
        /// <br>NetUSB红外相机</br>
        /// </summary>
        private NetIRCamera netIRCamera;
        /// <summary>  
        /// <br>HJK网络红外相机</br>
        /// </summary>
        private HJKCamera hjkCamera;
        /// <summary>  
        /// <br>可见光相机</br>
        /// </summary>
        private VICamera viCamera;
        /// <summary>  
        /// <br>TH红外相机</br>
        /// </summary>
        private THCamera thCamera;
        /// <summary>  
        /// <br>温度测量TCP服务器</br>
        /// </summary>
        private MDTcpServer mdTcpServer;
        /// <summary>  
        /// <br>TCP客户端通信接口2</br>
        /// </summary>
        private TcpTaskClient2 tcpTaskClient2;

        private int ID = -1;
        private int licenceState = -1;

        /// <summary>  
        /// <br>设置许可证</br>
        /// </summary>
        public void setLicenceCode(string code)
        {
            LICENCE_CODE = code;
        }

        /// <summary>  
        /// <br>实例ID</br>
        /// </summary>
        public int GetID()
        {
            return ID;
        }

        /// <summary>  
        /// <br>TH红外相机</br>
        /// </summary>
        public Palette getPalette()
        {
            if(palette == null && dll != null)
            {
                palette = new Palette();
                palette.DLL(ref dll);
            }
            return palette;
        }

        /// <summary>
        /// <br>图像渲染</br>
        /// </summary>
        public Reader getReader()
        {
            if (reader == null && dll != null)
            {
                reader = new Reader();
                reader.DLL(ref dll, this);
            }
            return reader;
        }

        /// <summary>
        /// <br>红外图像读取, 红外视频播放</br>
        /// </summary>
        public IRImage getIRImage()
        {
            if (irImage == null && dll != null)
            {
                irImage = new IRImage();
                irImage.DLL(ref dll, this);
            }
            return irImage;
        }


        /// <summary>
        /// <br>USB红外相机</br>
        /// </summary>
        public IRCamera getIRCamera()
        {
            if (irCamera == null && dll != null)
            {
                irCamera = new IRCamera();
                irCamera.DLL(ref dll, this);
            }
            return irCamera;
        }

        /// <summary>
        /// <br>NetUSB红外相机</br>
        /// </summary>
        public NetIRCamera getNetIRCamera()
        {
            if (netIRCamera == null && dll != null)
            {
                netIRCamera = new NetIRCamera();
                netIRCamera.DLL(ref dll, this);
            }
            return netIRCamera;
        }


        /// <summary>
        /// <br>HJK网络红外相机</br>
        /// </summary>
        public HJKCamera getHJKCamera()
        {
            if (hjkCamera == null && dll != null)
            {
                hjkCamera = new HJKCamera();
                hjkCamera.DLL(ref dll, this);
            }
            return hjkCamera;
        }

        /// <summary>
        /// <br>USB可见光相机</br>
        /// </summary>
        public VICamera getVICamera()
        {
            if (viCamera == null && dll != null)
            {
                viCamera = new VICamera();
                viCamera.DLL(ref dll, this);
            }
            return viCamera;
        }

        /// <summary>
        /// <br>TH红外相机</br>
        /// </summary>
        public THCamera getTHCamera()
        {
            if (thCamera == null && dll != null)
            {
                thCamera = new THCamera();
                thCamera.DLL(ref dll, this);
            }
            return thCamera;
        }

        /// <summary>
        /// <br>温度测量服务器</br>
        /// </summary>
        public MDTcpServer getMDTcpServer()
        {
            if (mdTcpServer == null && dll != null)
            {
                mdTcpServer = new MDTcpServer();
                mdTcpServer.DLL(ref dll, this);
            }
            return mdTcpServer;
        }

        /// <summary>  
        /// <br>TCP客户端通信接口2</br>
        /// </summary>
        public TcpTaskClient2 getTcpTaskClient2()
        {
            if (tcpTaskClient2 == null && dll != null)
            {
                tcpTaskClient2 = new TcpTaskClient2();
                tcpTaskClient2.DLL(ref dll, this);
            }
            return tcpTaskClient2;
        }


        private bool isCreate = false;

        private DllInvoke dll;
        /// <summary>
        /// <br>传入IRIP.DLL文件路径加载API函数
        /// </summary>
        public IRIP_API()
        {
        }

        ~IRIP_API()
        {
            //destroy();
            //destroySync();
            dll = null;
        }

        /// <summary>
        /// <br>传入IRIP.DLL文件路径加载API函数</br>
        /// <para>@param DLLPath dll文件路径，留空默认文件在软件根目录</para>
        /// <para>@return true/false</para>
        /// </summary>
        public bool DLL(String DLLPath = LIB_FILE)
        {
            dll = new DllInvoke(DLLPath);
            Create();

            return isCreate;
        }

        /// <summary>
        /// <br>检查库文件及相关API函数是否已加载</br>
        /// </summary>
        public bool dllIsCreate()
        {
            return isCreate;
        }

        private void Create()
        {
            if (!dll.IsCreate())
            {
                isCreate = false;
                return;
            }

            //if (Environment.Is64BitProcess)
            //{

            //}

#if _X86
    api_LicenceCode = _IRIP_API_LicenceCode;
    api_version = _IRIP_API_version;
    api_vercode = _IRIP_API_vercode;
    api_getMaxObj = _IRIP_API_getMaxObj;
    api_init = _IRIP_API_init;
    api_destroy = _IRIP_API_destroy;
    api_destroyAsync = _IRIP_API_destroyAsync;
    api_destroySync = _IRIP_API_destroySync;
    api_isInit = _IRIP_API_isInit;
    api_ipVersion = _IRIP_API_ipVersion;
    api_ipVercode = _IRIP_API_ipVercode;
    api_SetPathEncoding = _IRIP_API_SetPathEncoding;
    api_GetPathEncoding = _IRIP_API_GetPathEncoding;
    api_Temp16ToInt32 = _IRIP_API_Temp16ToInt32;
    api_Temp16ToFloat = _IRIP_API_Temp16ToFloat;
    api_To16BitsTemperatureFormat = _IRIP_API_To16BitsTemperatureFormat;
    api_To32BitsTemperatureFormat = _IRIP_API_To32BitsTemperatureFormat;
    api_Interpolation_UInt8 = _IRIP_API_Interpolation_UInt8;
    api_Cut_Int16Array = _IRIP_API_Cut_Int16Array;
    api_GetMinMaxAvgTemp_Int16 = _IRIP_API_GetMinMaxAvgTemp_Int16;
    api_GetMinMaxAvgTemp_Int32 = _IRIP_API_GetMinMaxAvgTemp_Int32;
    api_GetMinMaxAvgTemps_Int16 = _IRIP_API_GetMinMaxAvgTemps_Int16;
    api_GetMinMaxAvgTemps_Int32 = _IRIP_API_GetMinMaxAvgTemps_Int32;
    api_GetSpotTemp_Int16 = _IRIP_API_GetSpotTemp_Int16;
    api_GetSpotTemp_Int32 = _IRIP_API_GetSpotTemp_Int32;
    api_SetSegmentCalibData = _IRIP_API_SetSegmentCalibData;
    api_SegmentCalib = _IRIP_API_SegmentCalib;
    api_GetCalibrationDistance = _IRIP_API_GetCalibrationDistance;
    api_SetCalibrationDistance = _IRIP_API_SetCalibrationDistance;
    api_EnRefEmissivity = _IRIP_API_EnRefEmissivity;
    api_UnRefEmissivity = _IRIP_API_UnRefEmissivity;
    api_EnTransmittance = _IRIP_API_EnTransmittance;
    api_UnTransmittance = _IRIP_API_UnTransmittance;
    api_EnRefEmissivityTransmittanceUnit = _IRIP_API_EnRefEmissivityTransmittanceUnit;
    api_ConversionUnit = _IRIP_API_ConversionUnit;
    api_ConversionUnitRelative = _IRIP_API_ConversionUnitRelative;
    api_irFrameMemoryAllocation = _IRIP_API_irFrameMemoryAllocation;
    api_irFrameMemoryCopy = _IRIP_API_irFrameMemoryCopy;
    api_irFrameMemoryRelease = _IRIP_API_irFrameMemoryRelease;
    api_viFrameMemoryAllocation = _IRIP_API_viFrameMemoryAllocation;
    api_viFrameMemoryCopy = _IRIP_API_viFrameMemoryCopy;
    api_viFrameMemoryRelease = _IRIP_API_viFrameMemoryRelease;
    api_getSearchUsbCamerasMax = _IRIP_API_getSearchUsbCamerasMax;
    api_searchUsbCameraDevices = _IRIP_API_searchUsbCameraDevices;
    api_netHost_SerachStart = _IRIP_API_netHost_SerachStart;
    api_netHost_SerachStop = _IRIP_API_netHost_SerachStop;
    api_netHost_GetDevices = _IRIP_API_netHost_GetDevices;
#else
            api_LicenceCode = (IRIP_API_LicenceCode)dll.Invoke("IRIP_API_LicenceCode", typeof(IRIP_API_LicenceCode));
            api_version = (IRIP_API_version)dll.Invoke("IRIP_API_version", typeof(IRIP_API_version));
            api_vercode = (IRIP_API_vercode)dll.Invoke("IRIP_API_vercode", typeof(IRIP_API_vercode));
            api_getMaxObj = (IRIP_API_getMaxObj)dll.Invoke("IRIP_API_getMaxObj", typeof(IRIP_API_getMaxObj));
            api_init = (IRIP_API_init)dll.Invoke("IRIP_API_init", typeof(IRIP_API_init));
            api_destroy = (IRIP_API_destroy)dll.Invoke("IRIP_API_destroy", typeof(IRIP_API_destroy));
            api_destroyAsync = (IRIP_API_destroyAsync)dll.Invoke("IRIP_API_destroyAsync", typeof(IRIP_API_destroyAsync));
            api_destroySync = (IRIP_API_destroySync)dll.Invoke("IRIP_API_destroySync", typeof(IRIP_API_destroySync));
            api_isInit = (IRIP_API_isInit)dll.Invoke("IRIP_API_isInit", typeof(IRIP_API_isInit));
            api_ipVersion = (IRIP_API_ipVersion)dll.Invoke("IRIP_API_ipVersion", typeof(IRIP_API_ipVersion));
            api_ipVercode = (IRIP_API_ipVercode)dll.Invoke("IRIP_API_ipVercode", typeof(IRIP_API_ipVercode));
            api_SetPathEncoding = (IRIP_API_SetPathEncoding)dll.Invoke("IRIP_API_SetPathEncoding", typeof(IRIP_API_SetPathEncoding));
            api_GetPathEncoding = (IRIP_API_GetPathEncoding)dll.Invoke("IRIP_API_GetPathEncoding", typeof(IRIP_API_GetPathEncoding));
            api_Temp16ToInt32 = (IRIP_API_Temp16ToInt32)dll.Invoke("IRIP_API_Temp16ToInt32", typeof(IRIP_API_Temp16ToInt32));
            api_Temp16ToFloat = (IRIP_API_Temp16ToFloat)dll.Invoke("IRIP_API_Temp16ToFloat", typeof(IRIP_API_Temp16ToFloat));
            api_To16BitsTemperatureFormat = (IRIP_API_To16BitsTemperatureFormat)dll.Invoke("IRIP_API_To16BitsTemperatureFormat", typeof(IRIP_API_To16BitsTemperatureFormat));
            api_To32BitsTemperatureFormat = (IRIP_API_To32BitsTemperatureFormat)dll.Invoke("IRIP_API_To32BitsTemperatureFormat", typeof(IRIP_API_To32BitsTemperatureFormat));
            api_Interpolation_UInt8 = (IRIP_API_Interpolation_UInt8)dll.Invoke("IRIP_API_Interpolation_UInt8", typeof(IRIP_API_Interpolation_UInt8));
            api_Cut_Int16Array = (IRIP_API_Cut_Int16Array)dll.Invoke("IRIP_API_Cut_Int16Array", typeof(IRIP_API_Cut_Int16Array));
            api_GetMinMaxAvgTemp_Int16 = (IRIP_API_GetMinMaxAvgTemp_Int16)dll.Invoke("IRIP_API_GetMinMaxAvgTemp_Int16", typeof(IRIP_API_GetMinMaxAvgTemp_Int16));
            api_GetMinMaxAvgTemp_Int32 = (IRIP_API_GetMinMaxAvgTemp_Int32)dll.Invoke("IRIP_API_GetMinMaxAvgTemp_Int32", typeof(IRIP_API_GetMinMaxAvgTemp_Int32));
            api_GetMinMaxAvgTemps_Int16 = (IRIP_API_GetMinMaxAvgTemps_Int16)dll.Invoke("IRIP_API_GetMinMaxAvgTemps_Int16", typeof(IRIP_API_GetMinMaxAvgTemps_Int16));
            api_GetMinMaxAvgTemps_Int32 = (IRIP_API_GetMinMaxAvgTemps_Int32)dll.Invoke("IRIP_API_GetMinMaxAvgTemps_Int32", typeof(IRIP_API_GetMinMaxAvgTemps_Int32));
            api_GetSpotTemp_Int16 = (IRIP_API_GetSpotTemp_Int16)dll.Invoke("IRIP_API_GetSpotTemp_Int16", typeof(IRIP_API_GetSpotTemp_Int16));
            api_GetSpotTemp_Int32 = (IRIP_API_GetSpotTemp_Int32)dll.Invoke("IRIP_API_GetSpotTemp_Int32", typeof(IRIP_API_GetSpotTemp_Int32));
            api_SetSegmentCalibData = (IRIP_API_SetSegmentCalibData)dll.Invoke("IRIP_API_SetSegmentCalibData", typeof(IRIP_API_SetSegmentCalibData));
            api_SegmentCalib = (IRIP_API_SegmentCalib)dll.Invoke("IRIP_API_SegmentCalib", typeof(IRIP_API_SegmentCalib));
            api_GetCalibrationDistance = (IRIP_API_GetCalibrationDistance)dll.Invoke("IRIP_API_GetCalibrationDistance", typeof(IRIP_API_GetCalibrationDistance));
            api_SetCalibrationDistance = (IRIP_API_SetCalibrationDistance)dll.Invoke("IRIP_API_SetCalibrationDistance", typeof(IRIP_API_SetCalibrationDistance));
            api_EnRefEmissivity = (IRIP_API_EnRefEmissivity)dll.Invoke("IRIP_API_EnRefEmissivity", typeof(IRIP_API_EnRefEmissivity));
            api_UnRefEmissivity = (IRIP_API_UnRefEmissivity)dll.Invoke("IRIP_API_UnRefEmissivity", typeof(IRIP_API_UnRefEmissivity));
            api_EnTransmittance = (IRIP_API_EnTransmittance)dll.Invoke("IRIP_API_EnTransmittance", typeof(IRIP_API_EnTransmittance));
            api_UnTransmittance = (IRIP_API_UnTransmittance)dll.Invoke("IRIP_API_UnTransmittance", typeof(IRIP_API_UnTransmittance));
            api_EnRefEmissivityTransmittanceUnit = (IRIP_API_EnRefEmissivityTransmittanceUnit)dll.Invoke("IRIP_API_EnRefEmissivityTransmittanceUnit", typeof(IRIP_API_EnRefEmissivityTransmittanceUnit));
            api_ConversionUnit = (IRIP_API_ConversionUnit)dll.Invoke("IRIP_API_ConversionUnit", typeof(IRIP_API_ConversionUnit));
            api_ConversionUnitRelative = (IRIP_API_ConversionUnitRelative)dll.Invoke("IRIP_API_ConversionUnitRelative", typeof(IRIP_API_ConversionUnitRelative));
            api_irFrameMemoryAllocation = (IRIP_API_irFrameMemoryAllocation)dll.Invoke("IRIP_API_irFrameMemoryAllocation", typeof(IRIP_API_irFrameMemoryAllocation));
            api_irFrameMemoryCopy = (IRIP_API_irFrameMemoryCopy)dll.Invoke("IRIP_API_irFrameMemoryCopy", typeof(IRIP_API_irFrameMemoryCopy));
            api_irFrameMemoryRelease = (IRIP_API_irFrameMemoryRelease)dll.Invoke("IRIP_API_irFrameMemoryRelease", typeof(IRIP_API_irFrameMemoryRelease));
            api_viFrameMemoryAllocation = (IRIP_API_viFrameMemoryAllocation)dll.Invoke("IRIP_API_viFrameMemoryAllocation", typeof(IRIP_API_viFrameMemoryAllocation));
            api_viFrameMemoryCopy = (IRIP_API_viFrameMemoryCopy)dll.Invoke("IRIP_API_viFrameMemoryCopy", typeof(IRIP_API_viFrameMemoryCopy));
            api_viFrameMemoryRelease = (IRIP_API_viFrameMemoryRelease)dll.Invoke("IRIP_API_viFrameMemoryRelease", typeof(IRIP_API_viFrameMemoryRelease));
            api_getSearchUsbCamerasMax = (IRIP_API_getSearchUsbCamerasMax)dll.Invoke("IRIP_API_getSearchUsbCamerasMax", typeof(IRIP_API_getSearchUsbCamerasMax));
            api_searchUsbCameraDevices = (IRIP_API_searchUsbCameraDevices)dll.Invoke("IRIP_API_searchUsbCameraDevices", typeof(IRIP_API_searchUsbCameraDevices));
            api_netHost_SerachStart = (IRIP_API_netHost_SerachStart)dll.Invoke("IRIP_API_netHost_SerachStart", typeof(IRIP_API_netHost_SerachStart));
            api_netHost_SerachStop = (IRIP_API_netHost_SerachStop)dll.Invoke("IRIP_API_netHost_SerachStop", typeof(IRIP_API_netHost_SerachStop));
            api_netHost_GetDevices = (IRIP_API_netHost_GetDevices)dll.Invoke("IRIP_API_netHost_GetDevices", typeof(IRIP_API_netHost_GetDevices));
#endif

            //验证SDK证许可证
            LicenceCode(LICENCE_CODE);

            isCreate = true;
        }

#if _X86
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_LicenceCode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_LicenceCode(byte[] str, int length);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_version", CallingConvention = CallingConvention.StdCall)]
  static private extern IntPtr _IRIP_API_version();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_vercode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_vercode();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_getMaxObj", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_getMaxObj();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_init", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_init(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_destroy", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_destroy(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_destroyAsync", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_destroyAsync(int id, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_destroySync", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_destroySync(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_isInit", CallingConvention = CallingConvention.StdCall)]
  static private extern bool _IRIP_API_isInit(int id);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_ipVersion", CallingConvention = CallingConvention.StdCall)]
  static private extern IntPtr _IRIP_API_ipVersion();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_ipVercode", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_ipVercode();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_SetPathEncoding", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_SetPathEncoding(int coding);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetPathEncoding", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetPathEncoding();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_Temp16ToInt32", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_Temp16ToInt32(short temp, byte div, int offset, ref int outTemp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_Temp16ToFloat", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_Temp16ToFloat(short temp, byte div, int offset, ref float outTemp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_To16BitsTemperatureFormat", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_To16BitsTemperatureFormat(int[] src, int length, ref Temperature16Bits dest);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_To32BitsTemperatureFormat", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_To32BitsTemperatureFormat(ref Temperature16Bits src, int[] dest, int length);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_Interpolation_UInt8", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_Interpolation_UInt8(byte[] src, int srcW, int srcH, byte[] dst, int dstW, int dstH, int type);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_Cut_Int16Array", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_Cut_Int16Array(short[] buffer, int width, int height, short[] outBuffer, ref int outWidth, ref int outHeight, int sx, int sy, int ex, int ey, ref int leftOffset, ref int topOffset, int mPixelNum);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetMinMaxAvgTemp_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetMinMaxAvgTemp_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, ref Measurement_Data md, bool filter);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetMinMaxAvgTemp_Int32", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetMinMaxAvgTemp_Int32(int id, int[] buffer, int W, int H, ref Measurement_Data md, bool filter);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetMinMaxAvgTemps_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetMinMaxAvgTemps_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetMinMaxAvgTemps_Int32", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetMinMaxAvgTemps_Int32(int id, int[] buffer, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetSpotTemp_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetSpotTemp_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, int x, int y, bool filter);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetSpotTemp_Int32", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetSpotTemp_Int32(int id, int[] buffer, int W, int H, int x, int y, bool filter);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_SetSegmentCalibData", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_SetSegmentCalibData(int id, byte[] data, int length, bool enable);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_SegmentCalib", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_SegmentCalib(int id, ref int temp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetCalibrationDistance(int id, ref float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_SetCalibrationDistance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_SetCalibrationDistance(int id, float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_EnRefEmissivity", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_EnRefEmissivity(int id, ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_UnRefEmissivity", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_UnRefEmissivity(int id, ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_EnTransmittance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_EnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_UnTransmittance", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_UnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_EnRefEmissivityTransmittanceUnit", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_EnRefEmissivityTransmittanceUnit(int id, ref float outTemperature, float inTemperature, uint unit, float compensation, float emissivity, float reflectTemp, float distance, float winTmte, float winTemp);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_ConversionUnit", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_ConversionUnit(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_ConversionUnitRelative", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_ConversionUnitRelative(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irFrameMemoryAllocation", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, uint options);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irFrameMemoryCopy", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, uint options, bool onlyCopy);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_irFrameMemoryRelease", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_irFrameMemoryRelease(IntPtr frame);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viFrameMemoryAllocation", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_viFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, ushort bitCount);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viFrameMemoryCopy", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_viFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, bool onlyCopy);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_viFrameMemoryRelease", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_viFrameMemoryRelease(IntPtr frame);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_getSearchUsbCamerasMax", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_getSearchUsbCamerasMax();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_searchUsbCameraDevices", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);

          [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_netHost_SerachStart", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_netHost_SerachStart(ushort port);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_netHost_SerachStop", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_netHost_SerachStop();
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_netHost_GetDevices", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_netHost_GetDevices(ref HostDevices devices, int length);

#endif

        private delegate int IRIP_API_LicenceCode(byte[] str, int length);
        private IRIP_API_LicenceCode api_LicenceCode;

        private delegate IntPtr IRIP_API_version();
        private IRIP_API_version api_version;

        private delegate int IRIP_API_vercode();
        private IRIP_API_vercode api_vercode;

        private delegate int IRIP_API_getMaxObj();
        private IRIP_API_getMaxObj api_getMaxObj;

        private delegate int IRIP_API_init(int id);
        private IRIP_API_init api_init;

        private delegate int IRIP_API_destroy(int id);
        private IRIP_API_destroy api_destroy;

        private delegate int IRIP_API_destroyAsync(int id, RetCallback callback, IntPtr pUser);
        private IRIP_API_destroyAsync api_destroyAsync;

        private delegate int IRIP_API_destroySync(int id);
        private IRIP_API_destroySync api_destroySync;

        private delegate bool IRIP_API_isInit(int id);
        private IRIP_API_isInit api_isInit;

        private delegate IntPtr IRIP_API_ipVersion();
        private IRIP_API_ipVersion api_ipVersion;

        private delegate int IRIP_API_ipVercode();
        private IRIP_API_ipVercode api_ipVercode;

        private delegate int IRIP_API_SetPathEncoding(int coding);
        private IRIP_API_SetPathEncoding api_SetPathEncoding;

        private delegate int IRIP_API_GetPathEncoding();
        private IRIP_API_GetPathEncoding api_GetPathEncoding;

        private delegate int IRIP_API_Temp16ToInt32(short temp, byte div, int offset, ref int outTemp);
        private IRIP_API_Temp16ToInt32 api_Temp16ToInt32;

        private delegate int IRIP_API_Temp16ToFloat(short temp, byte div, int offset, ref float outTemp);
        private IRIP_API_Temp16ToFloat api_Temp16ToFloat;

        private delegate int IRIP_API_To16BitsTemperatureFormat(int[] src, int length, ref Temperature16Bits dest);
        private IRIP_API_To16BitsTemperatureFormat api_To16BitsTemperatureFormat;

        private delegate int IRIP_API_To32BitsTemperatureFormat(ref Temperature16Bits src, int[] dest, int length);
        private IRIP_API_To32BitsTemperatureFormat api_To32BitsTemperatureFormat;

        private delegate int IRIP_API_Interpolation_UInt8(byte[] src, int srcW, int srcH, byte[] dst, int dstW, int dstH, int type);
        private IRIP_API_Interpolation_UInt8 api_Interpolation_UInt8;

        private delegate int IRIP_API_Cut_Int16Array(short[] buffer, int width, int height, short[] outBuffer, ref int outWidth, ref int outHeight, int sx, int sy, int ex, int ey, ref int leftOffset, ref int topOffset, int mPixelNum);
        private IRIP_API_Cut_Int16Array api_Cut_Int16Array;

        private delegate int IRIP_API_GetMinMaxAvgTemp_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, ref Measurement_Data md, bool filter);
        private IRIP_API_GetMinMaxAvgTemp_Int16 api_GetMinMaxAvgTemp_Int16;

        private delegate int IRIP_API_GetMinMaxAvgTemp_Int32(int id, int[] buffer, int W, int H, ref Measurement_Data md, bool filter);
        private IRIP_API_GetMinMaxAvgTemp_Int32 api_GetMinMaxAvgTemp_Int32;

        private delegate int IRIP_API_GetMinMaxAvgTemps_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser);
        private IRIP_API_GetMinMaxAvgTemps_Int16 api_GetMinMaxAvgTemps_Int16;

        private delegate int IRIP_API_GetMinMaxAvgTemps_Int32(int id, int[] buffer, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser);
        private IRIP_API_GetMinMaxAvgTemps_Int32 api_GetMinMaxAvgTemps_Int32;

        private delegate int IRIP_API_GetSpotTemp_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int W, int H, int x, int y, bool filter);
        private IRIP_API_GetSpotTemp_Int16 api_GetSpotTemp_Int16;

        private delegate int IRIP_API_GetSpotTemp_Int32(int id, int[] buffer, int W, int H, int x, int y, bool filter);
        private IRIP_API_GetSpotTemp_Int32 api_GetSpotTemp_Int32;

        private delegate int IRIP_API_SetSegmentCalibData(int id, byte[] data, int length, bool enable);
        private IRIP_API_SetSegmentCalibData api_SetSegmentCalibData;

        private delegate int IRIP_API_SegmentCalib(int id, ref int temp);
        private IRIP_API_SegmentCalib api_SegmentCalib;

        private delegate int IRIP_API_GetCalibrationDistance(int id, ref float distance);
        private IRIP_API_GetCalibrationDistance api_GetCalibrationDistance;

        private delegate int IRIP_API_SetCalibrationDistance(int id, float distance);
        private IRIP_API_SetCalibrationDistance api_SetCalibrationDistance;

        private delegate int IRIP_API_EnRefEmissivity(int id, ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance);
        private IRIP_API_EnRefEmissivity api_EnRefEmissivity;

        private delegate int IRIP_API_UnRefEmissivity(int id, ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance);
        private IRIP_API_UnRefEmissivity api_UnRefEmissivity;

        private delegate int IRIP_API_EnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp);
        private IRIP_API_EnTransmittance api_EnTransmittance;

        private delegate int IRIP_API_UnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp);
        private IRIP_API_UnTransmittance api_UnTransmittance;

        private delegate int IRIP_API_EnRefEmissivityTransmittanceUnit(int id, ref float outTemperature, float inTemperature, uint unit, float compensation, float emissivity, float reflectTemp, float distance, float winTmte, float winTemp);
        private IRIP_API_EnRefEmissivityTransmittanceUnit api_EnRefEmissivityTransmittanceUnit;

        private delegate int IRIP_API_ConversionUnit(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit);
        private IRIP_API_ConversionUnit api_ConversionUnit;

        private delegate int IRIP_API_ConversionUnitRelative(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit);
        private IRIP_API_ConversionUnitRelative api_ConversionUnitRelative;

        private delegate int IRIP_API_irFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, uint options);
        private IRIP_API_irFrameMemoryAllocation api_irFrameMemoryAllocation;

        private delegate int IRIP_API_irFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, uint options, bool onlyCopy);
        private IRIP_API_irFrameMemoryCopy api_irFrameMemoryCopy;

        private delegate int IRIP_API_irFrameMemoryRelease(IntPtr frame);
        private IRIP_API_irFrameMemoryRelease api_irFrameMemoryRelease;

        private delegate int IRIP_API_viFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, ushort bitCount);
        private IRIP_API_viFrameMemoryAllocation api_viFrameMemoryAllocation;

        private delegate int IRIP_API_viFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, bool onlyCopy);
        private IRIP_API_viFrameMemoryCopy api_viFrameMemoryCopy;

        private delegate int IRIP_API_viFrameMemoryRelease(IntPtr frame);
        private IRIP_API_viFrameMemoryRelease api_viFrameMemoryRelease;

        private delegate int IRIP_API_getSearchUsbCamerasMax();
        private IRIP_API_getSearchUsbCamerasMax api_getSearchUsbCamerasMax;

        private delegate int IRIP_API_searchUsbCameraDevices(ref CameraDevices devices);
        private IRIP_API_searchUsbCameraDevices api_searchUsbCameraDevices;

        private delegate int IRIP_API_netHost_SerachStart(ushort port);
        private IRIP_API_netHost_SerachStart api_netHost_SerachStart;

        private delegate int IRIP_API_netHost_SerachStop();
        private IRIP_API_netHost_SerachStop api_netHost_SerachStop;

        private delegate int IRIP_API_netHost_GetDevices(ref HostDevices devices, int length);
        private IRIP_API_netHost_GetDevices api_netHost_GetDevices;

        /// <summary>
        /// <br>证许可证状态</br>
        /// <para>@return 0: 通过验证</para>
        /// </summary>
        public int LicenceState()
        {
            return licenceState;
        }

        /// <summary>
        /// <br>提交SDK证许可证</br>
        /// <para>@param code 许可证文本字串</para>
        /// <para>@return 0: 通过验证</para>
        /// </summary>
        public int LicenceCode(string code)
        {
            if (api_LicenceCode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }

            byte[] bCode = System.Text.Encoding.UTF8.GetBytes(code.TrimStart().TrimEnd());
            licenceState = api_LicenceCode(bCode, bCode.Length);
            return licenceState;
        }

        /// <summary>
        /// <br>库接口版本名</br>
        /// </summary>
        public string version()
        {
            if (api_version == null)
            {
                return "";
            }
            return Marshal.PtrToStringAnsi(api_version());
        }

        /// <summary>
        /// <br>库接口版本号</br>
        /// </summary>
        public int vercode()
        {
            if (api_vercode == null)
            {
                return 0;
            }
            return api_vercode();
        }

        /// <summary>
        /// <br>获取API实例总数，内部定量值：512</br>
        /// <para>@return 实例总数</para>
        /// </summary>
        public int getMaxObj()
        {
            if (api_getMaxObj == null)
            {
                return 0;
            }
            return api_getMaxObj();
        }

        /// <summary>
        /// <br>初始化实例，值为负时返回自动分配的实例ID，值为正时自定义ID</br>
        /// <para>@param id 实例ID</para>
        /// <para>@return 返回实例ID(0-511), RET_UNAUTHORIZED(-2147483647): 许可验证未通过, -2: 要求创建的实例ID超出范围, -1: 没有可用资源，实例创建失败</para>
        /// </summary>
        public int init(int id = -1)
        {
            if (api_init == null)
            {
                return -1;
            }

            //检查是否已分配实例并且检查实例是否有效
            if(isInit())
            {
                return ID;
            }

            ID = api_init(id);

            return ID;
        }

        /// <summary>
        /// <br>注销实例（异步模式，非线程安全）</br>
        /// <br>注销实例应保证所使用相关接口无活动内容，如已开启相机等多线程处理任务，请先结束，建议延时一定时间后注销实例</br>
        /// <para>@return 注销的的实例ID或注销实例数量</para>
        /// </summary>
        public int destroy()
        {
            if (api_destroy == null)
            {
                return -1;
            }
            if(ID >= 0 && isInit())
            {
                int id = api_destroy(ID);
                ID = -1;
                return id;
            }
            return -1;
        }

        /// <summary>
        /// <br>注销实例(异步模式， 带回调)</br>
        /// <para>@param callback 回调函数， ret：实例ID</para>
        /// <para>@param pUser</para>
        /// <para>@return 返回该注销实例的ID，返回值为-2: 要求注销的实例ID超出范围</para>
        /// </summary>
        public int destroyAsync(RetCallback callback, IntPtr pUser)
        {
            if (api_destroyAsync == null)
            {
                return -1;
            }
            if (isInit() && ID >= 0)
            {
                int ret = api_destroyAsync(ID, callback, pUser);
                ID = -1;
                return ret;
            }
            return -1;
        }

        /// <summary>
        /// <br>注销实例(同步模式)，注销实例应保证所使用相关接口无活动内容</br>
        /// <para>@return 返回该注销实例的ID，返回值为-2: 要求注销的实例ID超出范围</para>
        /// </summary>
        public int destroySync()
        {
            if (api_destroy == null)
            {
                return -1;
            }
            if (ID >= 0 && isInit())
            {
                int ret = api_destroySync(ID); 
                ID = -1;
                return ret;
            }
            return -1;
        }

        /// <summary>
        /// <br>查询实例是否存在</br>
        /// <para>@return TRUE/FALSE</para>
        /// </summary>
        public bool isInit()
        {
            if (api_isInit == null)
            {
                return false;
            }
            if(ID < 0)
            {
                return false;
            }
            return api_isInit(ID);
        }

        /// <summary>
        /// <br>API版本名</br>
        /// <para>@return</para>
        /// </summary>
        public string ipVersion()
        {
            if (api_ipVersion == null)
            {
                return "";
            }
            return Marshal.PtrToStringAnsi(api_ipVersion());
        }

        /// <summary>
        /// <br>API版本号</br>
        /// <para>@return</para>
        /// </summary>
        public int ipVercode()
        {
            if (api_ipVercode == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_ipVercode();
        }

        /// <summary>
        /// <br>设置输入路径和文件名的字符编码，支持ASC和UTF-8，默认UTF8</br>
        /// <para>@param coding 编码：PATH_ENCODING_UTF8(0), PATH_ENCODING_ASC(1)</para>
        /// </summary>
        public int SetPathEncoding(int coding)
        {
            if (api_SetPathEncoding == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetPathEncoding(coding);
        }

        /// <summary>
        /// <br>获取当前设置的编码</br>
        /// <para>@return coding 编码：PATH_ENCODING_UTF8(0), PATH_ENCODING_ASC(1)</para>
        /// </summary>
        public int GetPathEncoding()
        {
            if (api_GetPathEncoding == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetPathEncoding();
        }

        /// <summary>
        /// <br>16位温度格式转实际温度，真实温度t = (temp + offset) &lt;&lt; div);</br>
        /// <para>@param temp 输入温度</para>
        /// <para>@param div 移位值</para>
        /// <para>@param offset 偏移值</para>
        /// <para>@param outTemp 输出真实温度（整形，摄氏度的100倍）</para>
        /// <para>@return</para>
        /// </summary>
        public int Temp16ToInt32(short temp, byte div, int offset, ref int outTemp)
        {
            if (api_Temp16ToInt32 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Temp16ToInt32(temp, div, offset, ref outTemp);
        }

        /// <summary>
        /// <br>16位温度格式转实际温度，真实温度t = (temp + offset) &lt;&lt; div) / 100.0f;</br>
        /// <para>@param temp 输入温度</para>
        /// <para>@param div 移位值</para>
        /// <para>@param offset 偏移值</para>
        /// <para>@param outTemp 输出真实温度（小数，摄氏度）</para>
        /// <para>@return</para>
        /// </summary>
        public int Temp16ToFloat(short temp, byte div, int offset, ref float outTemp)
        {
            if (api_Temp16ToFloat == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Temp16ToFloat(temp, div, offset, ref outTemp);
        }

        /// <summary>
        /// <br>32位温度数组转16位温度格式</br>
        /// <para>@param src 输入温度数组</para>
        /// <para>@param length 数组长度</para>
        /// <para>@param dest 输出结构体(结构体内buffer不能为NULL，且对应数据长度为length)</para>
        /// <para>@return</para>
        /// </summary>
        public int To16BitsTemperatureFormat(int[] src, int length, ref Temperature16Bits dest)
        {
            if (api_To16BitsTemperatureFormat == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_To16BitsTemperatureFormat(src, length, ref dest);
        }

        /// <summary>
        /// <br>16位温度格式转32位温度数组</br>
        /// <para>@param src 输入结构体(结构体内buffer不能为NULL，且对应数据长度为length)</para>
        /// <para>@param dest 输出温度数组</para>
        /// <para>@param length 数组长度</para>
        /// <para>@return</para>
        /// </summary>
        public int To32BitsTemperatureFormat(ref Temperature16Bits src, int[] dest, int length)
        {
            if (api_To32BitsTemperatureFormat == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_To32BitsTemperatureFormat(ref src, dest, length);
        }

        /// <summary>
        /// <br>灰度图象插值</br>
        /// <para>@param src 输入8位灰度图像</para>
        /// <para>@param srcW 图像宽度</para>
        /// <para>@param srcH 图像高度</para>
        /// <para>@param dst 输出8位灰度图像</para>
        /// <para>@param dstW 输出宽度</para>
        /// <para>@param dstH 输出高度</para>
        /// <para>@param type 插值类型 双线形：INTERPOLATION_BILINERA(1), 最邻近：INTERPOLATION_NEAREST_NEIGHBOR(0)</para>
        /// </summary>
        public int Interpolation_UInt8(byte[] src, int srcW, int srcH, byte[] dst, int dstW, int dstH, int type = INTERPOLATION.BILINERA)
        {
            if (api_Interpolation_UInt8 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Interpolation_UInt8(src, srcW, srcH, dst, dstW, dstH, type);
        }

        /// <summary>
        /// <br>剪裁16位温度矩阵数据</br>
        /// <para>@param buffer [IN]输入16位温度矩阵数据</para>
        /// <para>@param width [IN]图像宽度</para>
        /// <para>@param height [IN]图像高度</para>
        /// <para>@param outBuffer [IN][OUT]输出16位温度矩阵数据</para>
        /// <para>@param outWidth [IN][OUT]输出宽度</para>
        /// <para>@param outHeight [IN][OUT]输出高度</para>
        /// <para>@param sx [IN]x起始坐标</para>
        /// <para>@param xy [IN]y起始坐标</para>
        /// <para>@param ex [IN]x结束坐标</para>
        /// <para>@param ey [IN]y结束坐标</para>
        /// <para>@param leftOffset [OUT]超出边界x方向输出坐标偏移</para>
        /// <para>@param topOffset  [OUT]超出边界y方向输出坐标偏移</para>
        /// <para>@param mPixelNum [IN]要求裁减宽高像素值必须为mPixelNum的倍数</para>
        /// </summary>
        public int Cut_Int16Array(short[] buffer, int width, int height, short[] outBuffer, ref int outWidth, ref int outHeight, int sx, int sy, int ex, int ey, ref int leftOffset, ref int topOffset, int mPixelNum = 1)
        {
            if (api_Cut_Int16Array == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Cut_Int16Array(buffer, width, height, outBuffer, ref outWidth, ref outHeight, sx, sy, ex, ey, ref leftOffset, ref topOffset, mPixelNum);
        }

        /// <summary>
        /// <br>输入区域/线坐标获取 高低温和平均温度，点坐标获取点温度</br>
        /// <para>@param buffer 16位温度矩阵数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param md</para>
        /// <para>@param filter 是否滤波</para>
        /// </summary>
        public int GetMinMaxAvgTemp_Int16(short[] buffer, byte tempDiv, int tempOffset, int W, int H, ref Measurement_Data md, bool filter = false)
        {
            if (api_GetMinMaxAvgTemp_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetMinMaxAvgTemp_Int16(ID, buffer, tempDiv, tempOffset, W, H, ref md, filter);
        }

        /// <summary>
        /// <br>输入区域/线坐标获取 高低温和平均温度，点坐标获取点温度</br>
        /// <para>@param buffer 32位温度矩阵数据</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param md</para>
        /// <para>@param filter 是否滤波</para>
        /// </summary>
        public int GetMinMaxAvgTemp_Int32(int[] buffer, int W, int H, ref Measurement_Data md, bool filter = false)
        {
            if (api_GetMinMaxAvgTemp_Int32 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetMinMaxAvgTemp_Int32(ID, buffer, W, H, ref md, filter);
        }

        /// <summary>
        /// <br>输入区域/线坐标获取 高低温和平均温度，点坐标获取点温度</br>
        /// <para>@param buffer 16位温度矩阵数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param mds 测量数组指针</para>
        /// <para>@param size 数组大小</para>
        /// <para>@param filter 是否滤波</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int GetMinMaxAvgTemps_Int16(short[] buffer, byte tempDiv, int tempOffset, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser)
        {
            if (api_GetMinMaxAvgTemps_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetMinMaxAvgTemps_Int16(ID, buffer, tempDiv, tempOffset, W, H, mds, size, filter, callback, pUser);
        }

        /// <summary>
        /// <br>输入区域/线坐标获取 高低温和平均温度，点坐标获取点温度</br>
        /// <para>@param buffer 32位温度矩阵数据</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param mds; 测量数组指针</para>
        /// <para>@param size 数组大小</para>
        /// <para>@param filter 是否滤波</para>
        /// <para>@param callback 回调函数</para>
        /// <para>@param pUser</para>
        /// </summary>
        public int GetMinMaxAvgTemps_Int32(int[] buffer, int W, int H, Measurement_Data[] mds, int size, bool filter, RetCallback callback, IntPtr pUser)
        {
            if (api_GetMinMaxAvgTemps_Int32 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetMinMaxAvgTemps_Int32(ID, buffer, W, H, mds, size, filter, callback, pUser);
        }

        /// <summary>
        /// <br>获取坐标点温度</br>
        /// <para>@param buffer 16位温度矩阵数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param x x坐标</para>
        /// <para>@param y y坐标</para>
        /// <para>@param filter 是否滤波</para>
        /// </summary>
        public int GetSpotTemp_Int16(short[] buffer, byte tempDiv, int tempOffset, int W, int H, int x, int y, bool filter = false)
        {
            if (api_GetSpotTemp_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSpotTemp_Int16(ID, buffer, tempDiv, tempOffset, W, H, x, y, filter);
        }

        /// <summary>
        /// <br>获取坐标点温度</br>
        /// <para>@param buffer 32位温度矩阵数据</para>
        /// <para>@param W 矩阵宽度</para>
        /// <para>@param H 矩阵高度</para>
        /// <para>@param x x坐标</para>
        /// <para>@param y y坐标</para>
        /// <para>@param filter 是否滤波</para>
        /// </summary>
        public int GetSpotTemp_Int32(int[] buffer, int W, int H, int x, int y, bool filter = false)
        {
            if (api_GetSpotTemp_Int32 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetSpotTemp_Int32(ID, buffer, W, H, x, y, filter);
        }

        /// <summary>
        /// <br>设置温度修正数据</br>
        /// <para>@param data 修正数据</para>
        /// <para>@param length 数据长度</para>
        /// <para>@param enable 是否启用修正</para>
        /// <para>@return 0: 启用，-1：不启用，-2：数据长度无效，-3,-4：数据无效</para>
        /// </summary>
        public int SetSegmentCalibData(byte[] data, int length, bool enable)
        {
            if (api_SetSegmentCalibData == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SetSegmentCalibData(ID, data, length, enable);
        }

        /// <summary>
        /// <br>温度修正（设置温度修正数据并启用后有效）</br>
        /// <para>@param temp 输入/输出温度</para>
        /// <para>@return</para>
        /// </summary>
        public int SegmentCalib(ref int temp)
        {
            if (api_SegmentCalib == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_SegmentCalib(ID, ref temp);
        }

        /// <summary>
        /// <br>获取标定距离</br>
        /// <para>@param distance 输出标定距离（米）</para>
        /// <para>@return</para>
        /// </summary>
        public int GetCalibrationDistance(ref float distance)
        {
            if (api_GetCalibrationDistance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetCalibrationDistance(ID, ref distance);
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
            return api_SetCalibrationDistance(ID, distance);
        }

        /// <summary>
        /// <br>发射率修正</br>
        /// <para>@param outTemperature 输出修正温度（摄氏度）</para>
        /// <para>@param inTemperature 输入温度（摄氏度）</para>
        /// <para>@param emissivity 发射率（0.01f~1.00f）</para>
        /// <para>@param reflectTemp 反射温度</para>
        /// <para>@param distance 距离（米），值为0时忽略距离修正</para>
        /// <para>@return</para>
        /// </summary>
        public int EnRefEmissivity(ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance = 0.0f)
        {
            if (api_EnRefEmissivity == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_EnRefEmissivity(ID, ref outTemperature, inTemperature, emissivity, reflectTemp, distance);
        }

        /// <summary>
        /// <br>反算发射率修正</br>
        /// <para>@param outTemperature 输出反修正温度（摄氏度）</para>
        /// <para>@param inTemperature 输入温度（摄氏度）</para>
        /// <para>@param emissivity 发射率（0.01f~1.00f）</para>
        /// <para>@param reflectTemp 反射温度</para>
        /// <para>@param distance 距离（米），值为0时忽略距离修正</para>
        /// <para>@return </para>
        /// </summary>
        public int UnRefEmissivity(ref float outTemperature, float inTemperature, float emissivity, float reflectTemp, float distance = 0.0f)
        {
            if (api_UnRefEmissivity == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_UnRefEmissivity(ID, ref outTemperature, inTemperature, emissivity, reflectTemp, distance);
        }

        /// <summary>
        /// <br>*</br>
        /// <br>透射率修正</br>
        /// <para>@param outTemperature 输出修正温度（摄氏度）</para>
        /// <para>@param inTemperature 输入温度（摄氏度）</para>
        /// <para>@param winTmte 窗口透射率（0.01f~1.00f）</para>
        /// <para>@param winTemp 窗口温度（摄氏度）</para>
        /// <para>@return </para>
        /// </summary>
        public int EnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp)
        {
            if (api_EnTransmittance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_EnTransmittance(ref outTemperature, inTemperature, winTmte, winTemp);
        }

        /// <summary>
        /// <br>*</br>
        /// <br>反算透射率修正</br>
        /// <para>@param outTemperature 输出反修正温度（摄氏度）</para>
        /// <para>@param inTemperature 输入温度（摄氏度）</para>
        /// <para>@param winTmte 窗口透射率（0.01f~1.00f）</para>
        /// <para>@param winTemp 窗口温度（摄氏度）</para>
        /// <para>@return </para>
        /// </summary>
        public int UnTransmittance(ref float outTemperature, float inTemperature, float winTmte, float winTemp)
        {
            if (api_UnTransmittance == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_UnTransmittance(ref outTemperature, inTemperature, winTmte, winTemp);
        }

        /// <summary>
        /// <br>发射率修正+温度补偿+转换温度单位</br>
        /// <para>@param outTemperature 输出反修正温度</para>
        /// <para>@param inTemperature 输入温度(摄氏度)</para>
        /// <para>@param unit 转换到的温度单位(0: 摄氏度 1: 华氏度 2: 开尔文)</para>
        /// <para>@param compensation 补偿温度</para>
        /// <para>@param emissivity 发射率（0.01f~1.0f）</para>
        /// <para>@param reflectTemp 反射温度</para>
        /// <para>@param distance 距离（米），值为0时忽略距离修正</para>
        /// <para>@param winTmte 窗口透射率（0.1f~1.0f）</para>
        /// <para>@param winTemp 窗口温度（摄氏度）</para>
        /// <para>@return </para>
        /// </summary>
        public int EnRefEmissivityTransmittanceUnit(ref float outTemperature, float inTemperature, uint unit, float compensation, float emissivity, float reflectTemp, float distance = 0.0f, float winTmte = 1.0f, float winTemp = 20.0f)
        {
            if (api_EnRefEmissivityTransmittanceUnit == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_EnRefEmissivityTransmittanceUnit(ID, ref outTemperature, inTemperature, unit, compensation, emissivity, reflectTemp, distance, winTmte, winTemp);
        }

        /// <summary>
        /// <br>温度单位转换（绝对值）</br>
        /// <para>@param outTemperature 输出转换温度</para>
        /// <para>@param inTemperature 输入温度</para>
        /// <para>@param inUnit 输入单位(0: 摄氏度 1: 华氏度 2: 开尔文)</para>
        /// <para>@param outUnit 输出单位(0: 摄氏度 1: 华氏度 2: 开尔文)</para>
        /// <para>@return </para>
        /// </summary>
        public int ConversionUnit(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit = UNIT.CENTIGRADE)
        {
            if (api_ConversionUnit == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_ConversionUnit(ref outTemperature, inTemperature, inUnit, outUnit);
        }

        /// <summary>
        /// <br>温度单位转换（相对值）</br>
        /// <para>@param outTemperature 输出转换温度</para>
        /// <para>@param inTemperature 输入温度</para>
        /// <para>@param inUnit 输入单位(0: 摄氏度 1: 华氏度 2: 开尔文)</para>
        /// <para>@param outUnit 输出单位(0: 摄氏度 1: 华氏度 2: 开尔文)</para>
        /// <para>@return</para>
        /// </summary>
        public int ConversionUnitRelative(ref float outTemperature, float inTemperature, uint inUnit, uint outUnit = UNIT.CENTIGRADE)
        {
            if (api_ConversionUnitRelative == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_ConversionUnitRelative(ref outTemperature, inTemperature, inUnit, outUnit);
        }

        /// <summary>
        /// <br>红外数据帧分配内存</br>
        /// <para>@param frame 帧结构体</para>
        /// <para>@param width　图像宽度</para>
        /// <para>@param height　图像高度</para>
        /// <para>@param IMGA_W　设置输出的图像宽度(插值), 注意如输出BMP图像时输出宽度和高度尽可能设置为4的倍数，否则旋转图像时可能导致申请的IMGA内存长度不足导致输出失败（输出BMP格式时）</para>
        /// <para>@param IMGA_H　设置输出的图像高度(插值)</para>
        /// <para>@param IMGA_Type  IMAGE_DATA_TYPE_ARGB(0): ARGB(32位),  IMAGE_DATA_TYPE_BMP(1): BMP(8位), IMAGE_DATA_TYPE_PNG(2): PNG(8位)</para>
        /// <para>@param options 多选项参数指定需要申请内存的项 FR_OPTION_IMGA | FR_OPTION_tEMP |.....</para>
        /// <para>@return</para>
        /// <br>&lt;/&gt;</br>
        /// <br>options的设置：options = FR_OPTION_IMGA | FR_OPTION_tEMP |.....</br>
        /// <br>frame-&gt;IMGA  FR_OPTION_IMGA (输出图像ARGB/BMP/PNG，可选)</br>
        /// <br>frame-&gt;tEMP  FR_OPTION_tEMP (返回原始温度矩阵数据，必选)</br>
        /// <br>frame-&gt;tEMP2 FR_OPTION_tEMP2 (温度矩阵滤波，如果启用则使用滤波后的数据进行温度测量，可选)</br>
        /// <br>frame-&gt;jSON  FR_OPTION_JSON (红外文件/视频内置的文件信息和测量数据，可选)</br>
        /// <br>frame-&gt;IDAT  FR_OPTION_IDAT (图像灰度数据，可选)</br>
        /// <br>frame-&gt;PLTE  FR_OPTION_PLTE (文件内置调色板，可选)</br>
        /// <br>frame-&gt;cALA  FR_OPTION_CALI (文件内置校准数据，可选)</br>
        /// <br>frame-&gt;dPLT　FR_OPTION_DPLT (8位灰度动态调色板，可选)</br>
        /// <br>frame-&gt;dTHR  FR_OPTION_DTHR (动态调色板阈值，表示当前红外图像渲染调色板映射温度范围，可选）</br>
        /// </summary>
        public int irFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, uint options)
        {
            if (api_irFrameMemoryAllocation == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_irFrameMemoryAllocation(frame, width, height, IMGA_W, IMGA_H, IMGA_Type, options);
        }

        /// <summary>
        /// <br>红外数据帧Copy</br>
        /// <para>@param inFrame 源帧结构体</para>
        /// <para>@param outFrame 目标帧结构体</para>
        /// <para>@param options 多选项参数指定需要拷贝的内存的项，该参数为0时拷贝所有项目</para>
        /// <para>@param onlyCopy 是否只拷贝数据，是：仅在目标结构体数据项内存大小够用时拷贝数据，否：目标结构体数据项内存大小与输入不符将重新分配内存并拷贝数据</para>
        /// </summary>
        public int irFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, uint options = 0, bool onlyCopy = false)
        {
            if (api_irFrameMemoryCopy == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_irFrameMemoryCopy(inFrame, outFrame, options, onlyCopy);
        }

        /// <summary>
        /// <br>红外数据帧内存回收</br>
        /// <para>@param frame 帧结构体</para>
        /// </summary>
        public int irFrameMemoryRelease(IntPtr frame)
        {
            if (api_irFrameMemoryRelease == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_irFrameMemoryRelease(frame);
        }

        /// <summary>
        /// <br>可见光数据帧分配内存</br>
        /// <para>@param frame 帧结构体</para>
        /// <para>@param width　图像宽度</para>
        /// <para>@param height　图像高度</para>
        /// <para>@param IMGA_W　设置输出的图像宽度(插值), 注意如输出BMP图像时输出宽度和高度尽可能设置为4的倍数，否则旋转图像时可能导致申请的IMGA内存长度不足导致输出失败（输出BMP格式时）</para>
        /// <para>@param IMGA_H　设置输出的图像高度(插值)</para>
        /// <para>@param IMGA_Type </para>
        /// <br>VI_IMAGE_DATA_TYPE_RAW(0): RAW(如果相机支持，选择24位时即为RGB数据), </br>
        /// <br>VI_IMAGE_DATA_TYPE_BMP(1): BMP(16位/24位), </br>
        /// <br>VI_IMAGE_DATA_TYPE_RGBA(2): RGBA(数据, 目前原数据24位情况下支持), </br>
        /// <br>VI_IMAGE_DATA_TYPE_BGRA(3): BGRA(数据, 目前原数据24位情况下支持), </br>
        /// <br>VI_IMAGE_DATA_TYPE_ARGB(3): ARGB(数据, 目前原数据24位情况下支持), </br>
        /// <br>VI_IMAGE_DATA_TYPE_ABGR(4): ABGR(数据, 目前原数据24位情况下支持)</br>
        /// <para>@param bitCount 支持16位/24位/32位（RGBA/BGRA/ARGB/ABGR强制32位）</para>
        /// </summary>
        public int viFrameMemoryAllocation(IntPtr frame, ushort width, ushort height, ushort IMGA_W, ushort IMGA_H, uint IMGA_Type, ushort bitCount)
        {
            if (api_viFrameMemoryAllocation == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_viFrameMemoryAllocation(frame, width, height, IMGA_W, IMGA_H, IMGA_Type, bitCount);
        }

        /// <summary>
        /// <br>可见光数据帧Copy</br>
        /// <para>@param inFrame 源帧结构体</para>
        /// <para>@param outFrame 目标帧结构体</para>
        /// <para>@param onlyCopy 是否只拷贝数据，是：仅在目标结构体数据项内存大小够用时拷贝数据，否：目标结构体数据项内存大小与输入不符将重新分配内存并拷贝数据</para>
        /// </summary>
        public int viFrameMemoryCopy(IntPtr inFrame, IntPtr outFrame, bool onlyCopy = false)
        {
            if (api_viFrameMemoryCopy == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_viFrameMemoryCopy(inFrame, outFrame, onlyCopy);
        }

        /// <summary>
        /// <br>可见光数据帧内存回收</br>
        /// <para>@param frame 帧结构体</para>
        /// </summary>
        public int viFrameMemoryRelease(IntPtr frame)
        {
            if (api_viFrameMemoryRelease == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_viFrameMemoryRelease(frame);
        }

        /// <summary>
        /// <br>可检索USB相机的数量(内置常量, 用于结构体预审请内存设置大小)</br>
        /// <para>@return 数量</para>
        /// </summary>
        public int getSearchUsbCamerasMax()
        {
            if (api_getSearchUsbCamerasMax == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_getSearchUsbCamerasMax();
        }

        /// <summary>
        /// <br>检索USB相机</br>
        /// <para>@return 0：正常</para>
        /// </summary>
        public int searchUsbCameraDevices(ref CameraDevices devices)
        {
            if (api_searchUsbCameraDevices == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_searchUsbCameraDevices(ref devices);
        }


        /// <summary>
        /// <br>开启网络主机搜索服务</br>
        /// <para>@param port 端口</para>
        /// </summary>
        public int netHost_SerachStart(ushort port)
        {
            if (api_netHost_SerachStart == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_netHost_SerachStart(port);
        }

        /// <summary>
        /// <br>关闭网络主机搜索服务</br>
        /// </summary>
        public int netHost_SerachStop()
        {
            if (api_netHost_SerachStop == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_netHost_SerachStop();
        }

        /// <summary>
        /// <br>获取在线主机信息</br>
        /// <para>@param devices HostDevice 结构体数组</para>
        /// <para>@param length 数组长度</para>
        /// </summary>
        public int netHost_GetDevices(ref HostDevices devices, int length)
        {
            if (api_netHost_GetDevices == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_netHost_GetDevices(ref devices, length);
        }

    }
}
