using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>红外图像渲染参数设置结构体</br>
    /// <br>使用API.GetIRIPConfig函数获取默认设置, API.SetIRIPConfig应用设置</br>
    /// <br>对于红外图像、红外相机分别使用API内相关的函数获取和设置</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct IRIP_Config
    {
        /// <summary>
        /// <br>高动态温度输出灰度图像压缩模式, hpfMode取值范围0: Agc, 1: Lut</br>
        /// </summary>
        public ushort hpfMode;
        /// <summary>
        /// <br>动态曝光控制，单幅输出模式应关闭，视频模式可开启</br>
        /// </summary>
        public bool isDynamicExposure;
        /// <summary>
        /// <br>夜视模式</br>
        /// </summary>
        public bool isNightVision;
        /// <summary>
        /// <br>夜视模式强度 hpfAlpha取值范围0.01f - 0.05f</br>
        /// </summary>
        public float hpfAlpha;
        /// <summary>
        /// <br>截幅，取值范围0.00f - 0.99f</br>
        /// </summary>
        public float ignoreRatio;
        /// <summary>
        /// <br>高温自动阈值, 非夜视模式锁定有效</br>
        /// </summary>
        public bool isAutoTmax;
        /// <summary>
        /// <br>低温自动阈值, 非夜视模式锁定有效</br>
        /// </summary>
        public bool isAutoTmin;
        /// <summary>
        /// <br>高温阈值;
        /// </summary>
        public int Tmax;
        /// <summary>
        /// <br>低温阈值</br>
        /// </summary>
        public int Tmin;
        /// <summary>
        /// <br>增益限制模式 0: 禁用, 1: 向中温收敛, 2: 向低温收敛, 3: 向高温收敛</br>
        /// </summary>
        public uint gainLimit;  //（1.0.0.7修改变量名称isGainLimit为gainLimit，类型由bool改为uint）
        /// <summary>
        /// <br>增益限制值，取值范围0.0 - 3.75;</br>
        /// </summary>
        public float gainValue;
        /// <summary>
        /// <br>输出上下颠倒的图像</br>
        /// </summary>
        public bool isUpsidedown;
        /// <summary>
        /// <br>锐化</br>
        /// </summary>
        public bool isSharpness;
        /// <summary>
        /// <br>DDE数字增强类型, 0: 不启用, 1: 类型1, 2: 类型2, 3: 类型3</br>
        /// </summary>
        public uint DDEType;  //（1.0.0.7修改变量名称isDDEOpen为DDEType，类型由bool改为uint）
        /// <summary>
        /// <br>开启图像降噪</br>
        /// </summary>
        public bool isDenoise;
    }

    /// <summary>
    /// <br>内置调色板信息，调色板名称为utf8编码，可将序号传给相继选择调色板接口来选择调色板</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IR_PALETTE
    {
        /// <summary>
        /// <br>调色板序号</br>
        /// </summary>
        public int index;

        /// <summary>
        /// <br>调色板中文名称</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] 
        public byte[]  name_cn;

        /// <summary>
        /// <br>调色板英文名称</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] 
        public byte[]  name_en;

        /// <summary>
        /// <br>调色板数据，32位，长度256</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] 
        public uint[]  palette;
    }

    /// <summary>
    /// <br>获取所有内置调色板结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct IR_PALETTES
    {
        /// <summary>
        /// <br>返回的调色板数量（获取时必须指定需要返回的数量，否则返回0个）</br>
        /// </summary>
        public int num;

        /// <summary>
        /// 调色板数组</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 12)] //要获取的数量不能大于该项设置值，否则可能会引发异常
        public IR_PALETTE[] palettes;

        /// <summary>
        /// <br>调色板结构体初始化</br>
        /// <para>@param num 要获取的调色板数量</para>
        /// </summary>
        public IR_PALETTES(int num)
        {
            this.num = num > 12 ? 12 : num < 0 ? 0 : num;

            ///新建数量必须与SizeConst声明一致
            this.palettes = new IR_PALETTE[12]; 
        }
    }

    /// <summary>
    /// <br>UVC相机结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct CameraDevice
    {
        /// <summary>
        /// <br>序号</br>
        /// </summary>
        public ushort index;
        /// <summary>
        /// <br>相机名称</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] 
        public string  name;
        /// <summary>
        /// <br>VID</br>
        /// </summary>
        public ushort vid;
        /// <summary>
        /// <br>PID</br>
        /// </summary>
        public ushort pid;
        /// <summary>
        /// <br>设备地址</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] 
        public string path;
        /// <summary>
        /// <br>USB端口地址</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string loactionIDs;
        /// <summary>
        /// <br>设备序列号</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] 
        public string sn;
        /// <summary>
        /// <br>设备固件号</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] 
        public string firmware;
        /// <summary>
        /// <br>机分辨率 宽度</br>
        /// </summary>
        public ushort width;
        /// <summary>
        /// <br>相机分辨率 高度</br>
        /// </summary>
        public ushort height;
        /// <summary>
        /// <br>数据位数</br>
        /// </summary>
        public ushort bitCount;
    }

    /// <summary>
    /// <br>检索UVC相机返回结构体数据</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct CameraDevices
    {
        /// <summary>
        /// <br>检索到的相机数量</br>
        /// </summary>
        public ushort devNum;
        /// <summary>
        /// <br>UVC相机结构体</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 16)] 
        public CameraDevice[]  devs;
        /// <summary>
        /// <br>0: 检索所有设备，>0: 指定检索VID; USB红外相机VID：USB_IR_CAMERA_VID(0x1514)</br>
        /// </summary>
        public ushort vid;
        /// <summary>
        /// <br>0: 检索所有设备，>0: 指定检索PID; USB红外相机PID：USB_IR_CAMERA_PID(0x0001)</br>
        /// </summary>
        public ushort pid;
    }

    /// <summary>
    /// <br>TH红外相机结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct thDevice
    {
        /// <summary>
        /// <br>相机序号</br>
        /// </summary>
        public ushort index;
        /// <summary>
        /// <br>VID</br>
        /// </summary>
        public ushort vid;
        /// <summary>
        /// <br>PID</br>
        /// </summary>
        public ushort pid;
        /// <summary>
        /// <br>设备路径</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)] 
        public byte[] filename;
    }

    /// <summary>
    /// <br>检索TH红外相机返回结构体数据</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct thDevices
    {
        /// <summary>
        /// <br>检索到的相机数量</br>
        /// </summary>
        public ushort devNum;
        /// <summary>
        /// <br>TH红外相机结构体</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)] 
        public thDevice[]  devs;
    }

    /// <summary>
    /// <br>TH红外相机状态回调结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct thCameraStates
    {
        /// <summary>
        /// <br>相机是否意外断开</br>
        /// </summary>
        public bool isAlive;
        /// <summary>
        /// <br>相机是否已启动</br>
        /// </summary>
        public bool isStart;
        /// <summary>
        /// <br>相机是否已暂停</br>
        /// </summary>
        public bool isPause;
        /// <summary>
        /// <br>当前使用的校准数据组 Calibration_NV(0) / Calibration_LOW(1) / Calibration_MED(2) / Calibration_HIGH(3)</br>
        /// </summary>
        public int calibrationTable;
        /// <summary>
        /// <br>是否已启用均匀度校正</br>
        /// </summary>
        public bool ucEnable;
        /// <summary>
        /// <br>当前使用的校准数据组均匀度较真数据是否存在</br>
        /// </summary>
        public bool ucDataTrue;
    }

    /// <summary>
    /// <br>红外图像/红外视频回调帧数据结构体（需先申请内存）</br>
    /// <br>申请内存：IRIP_API_irFrameMemoryAllocation()</br>
    /// <br>释放内存：IRIP_API_irFrameMemoryRelease()</br>
    /// <br>将结构体指针传给相应接口，回调后使用</br>
    /// <br>数据数组除[tEMP] 外其它数组均为可选项，申请内存时通过opttons参数配置</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct CallbackFrameData
    {
        /// <summary>
        /// <br>[OUT] 帧序号</br>
        /// </summary>
        public uint index;
        /// <summary>
        /// <br>[OUT] 当前帧是否是打快门(相机模式)</br>
        /// </summary>
        public bool histo;
        /// <summary>
        /// <br>[OUT] Fpa温度
        /// </summary>
        public int fpaTemp;
        /// <summary>
        /// <br>[OUT] 返回相机温度矩阵宽度</br>
        /// </summary>
        public ushort width;
        /// <summary>
        /// <br>[OUT] 返回相机温度矩阵高度</br>
        /// </summary>
        public ushort height;
        /// <summary>
        /// <br>[OUT] 时间戳</br>
        /// </summary>
        public long time;
        /// <summary>
        /// <br>[OUT] 延时时间（红外视频帧间隔时间, 相机模式和图片模式忽略此值）</br>
        /// </summary>
        public uint delay;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像数据数组(必选项OPTION.IMGA) ARGB/BMP/PNG</br>
        /// </summary>
        public IntPtr IMGA;
        /// <summary>
        /// <br>[IN] 图像数据可用数组长度</br>
        /// </summary>
        public uint IMGA_MLen;
        /// <summary>
        /// <br>[OUT] 图像数据返回数组长度</br>
        /// </summary>
        public uint IMGA_Len;
        /// <summary>
        /// <br>[IN] 设定返回的图像宽度（与原始温度数据不符将插值到设定值）</br>
        /// </summary>
        public ushort IMGA_W;
        /// <summary>
        /// <br>[IN] 设定返回的图像高度（与原始温度数据不符将插值到设定值）</br>
        /// </summary>
        public ushort IMGA_H;
        /// <summary>
        /// <br>[IN] 返回的图像数据类型（IR.ARGB(0): ARGB数据, IR.BMP(1): BMP, IR.TYPE_PNG(2): PNG)</br>
        /// </summary>
        public uint IMGA_Type;
        /// <summary>
        /// <br>[OUT] 图像是否已旋转方向(90°|270°)，旋转后输出的IMGA图像实际长宽比对调，IMGA_W、IMGA_H为输入参数保持不变，使用时通过rotated参数判断</br>
        /// </summary>
        public bool rotated;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像或视频内置配置JSON信息文本</br>
        /// </summary>
        public IntPtr jSON;
        /// <summary>
        /// <br>[IN] JSON数据可用数组长度（默认8192）</br>
        /// </summary>
        public uint jSON_MLen;
        /// <summary>
        /// <br>[OUT] JSON数据返回数组长度</br>
        /// </summary>
        public uint jSON_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])灰度图像数据数组</br>
        /// </summary>
        public IntPtr IDAT;
        /// <summary>
        /// <br>[IN] 灰度图像数据可用数组长度（width * height）</br>
        /// </summary>
        public uint IDAT_MLen;
        /// <summary>
        /// <br>[OUT] 灰度图像数据返回数组长度</br>
        /// </summary>
        public uint IDAT_Len;
        /// <summary>
        /// <br>[SET/OUT] (uint[])图像或视频内置32位256阶调色板数据</br>
        /// </summary>
        public IntPtr PLTE;
        /// <summary>
        /// <br>[IN] 调色板数据可用数组长度（256）</br>
        /// </summary>
        public uint PLTE_MLen;
        /// <summary>
        /// <br>[OUT] 调色板数据返回数组长度</br>
        /// </summary>
        public uint PLTE_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])8位动态灰度调色板数据</br>
        /// </summary>
        public IntPtr dPLT;
        /// <summary>
        /// <br>[IN] 动态灰度调色板可用数组长度（256）</br>
        /// </summary>
        public uint dPLT_MLen;
        /// <summary>
        /// <br>[OUT] 动态灰度调色板返回数组长度</br>
        /// </summary>
        public uint dPLT_Len;
        /// <summary>
        /// <br>[SET/OUT] (short[])原始16位温度数据数组(必选项)</br>
        /// </summary>
        public IntPtr tEMP;
        /// <summary>
        /// <br>[IN] 原始16位温度数据可用数组长度（width * height）</br>
        /// </summary>
        public uint tEMP_MLen;
        /// <summary>
        /// <br>[OUT] 原始16位温度数据返回数组长度</br>
        /// </summary>
        public uint tEMP_Len;
        /// <summary>
        /// <br>[SET/OUT] (short[])滤波后的16位温度数据数组（如果滤波启用）</br>
        /// </summary>
        public IntPtr tEMP2;
        /// <summary>
        /// <br>[IN] 滤波后的16位温度数据数组可用数组长度（width * height）</br>
        /// </summary>
        public uint tEMP2_MLen;
        /// <summary>
        /// <br>[OUT] 滤波后的16位温度数据数组返回数组长度</br>
        /// </summary>
        public uint tEMP2_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像或视频内置的矫正数据</br>
        /// </summary>
        public IntPtr cALI;
        /// <summary>
        /// <br>[IN] 图像或视频内置的矫正数据可用数组长度（固定长度180）</br>
        /// </summary>
        public uint cALI_MLen;
        /// <summary>
        /// <br>[OUT] 图像或视频内置的矫正数据返回数组长度</br>
        /// </summary>
        public uint cALI_Len;
        /// <summary>
        /// <br>[SET/OUT] (int[])红外图像动态调色板温度映射范围（长度4，[0]：最低温， [1]：最高温，[2]：调色板低位对应颜色index， [3]：调色板高位对应颜色index）</br>
        /// </summary>
        public IntPtr dTHR;
        /// <summary>
        /// <br>[IN] 红外图像动态调色板温度映射范围数据可用数组长度（4）</br>
        /// </summary>
        public uint dTHR_MLen;
        /// <summary>
        /// <br>[OUT] 红外图像动态调色板温度映射范围数据返回数组长度</br>
        /// </summary>
        public uint dTHR_Len;
        /// <summary>
        /// <br>[OUT] 温度数据位数</br>
        /// </summary>
        public byte tempBits;
        /// <summary>
        /// <br>[OUT] 实际温度数据换算移位值</br>
        /// </summary>
        public byte tempDiv;
        /// <summary>
        /// <br>[OUT] 实际温度数据换算偏移值</br>
        /// </summary>
        public int tempOffset;
    }

    /// <summary>
    /// <br>红外图像/红外视频回调帧数据结构体，从CallbackFrameData拷贝数据使用</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CallbackFrameData2
    {
        /// <summary>
        /// <br>[OUT] 帧序号</br>
        /// </summary>
        public uint index;
        /// <summary>
        /// <br>[OUT] 当前帧是否是打快门(相机模式)</br>
        /// </summary>
        public bool histo;
        /// <summary>
        /// <br>[OUT] Fpa温度
        /// </summary>
        public int fpaTemp;
        /// <summary>
        /// <br>[OUT] 返回相机温度矩阵宽度</br>
        /// </summary>
        public ushort width;
        /// <summary>
        /// <br>[OUT] 返回相机温度矩阵高度</br>
        /// </summary>
        public ushort height;
        /// <summary>
        /// <br>[OUT] 时间戳</br>
        /// </summary>
        public long time;
        /// <summary>
        /// <br>[OUT] 延时时间（红外视频帧间隔时间, 相机模式和图片模式忽略此值）</br>
        /// </summary>
        public uint delay;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像数据数组(必选项OPTION.IMGA) ARGB/BMP/PNG</br>
        /// </summary>
        public byte[] IMGA;
        /// <summary>
        /// <br>[IN] 图像数据可用数组长度</br>
        /// </summary>
        public uint IMGA_MLen;
        /// <summary>
        /// <br>[OUT] 图像数据返回数组长度</br>
        /// </summary>
        public uint IMGA_Len;
        /// <summary>
        /// <br>[IN] 设定返回的图像宽度（与原始温度数据不符将插值到设定值）</br>
        /// </summary>
        public ushort IMGA_W;
        /// <summary>
        /// <br>[IN] 设定返回的图像高度（与原始温度数据不符将插值到设定值）</br>
        /// </summary>
        public ushort IMGA_H;
        /// <summary>
        /// <br>[IN] 返回的图像数据类型（IR.ARGB(0): ARGB数据, IR.BMP(1): BMP, IR.TYPE_PNG(2): PNG)</br>
        /// </summary>
        public uint IMGA_Type;
        /// <summary>
        /// <br>[OUT] 图像是否已旋转方向(90°|270°)，旋转后输出的IMGA图像实际长宽比对调，IMGA_W、IMGA_H为输入参数保持不变，使用时通过rotated参数判断</br>
        /// </summary>
        public bool rotated;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像或视频内置配置JSON信息文本</br>
        /// </summary>
        public byte[] jSON;
        /// <summary>
        /// <br>[IN] JSON数据可用数组长度（默认8192）</br>
        /// </summary>
        public uint jSON_MLen;
        /// <summary>
        /// <br>[OUT] JSON数据返回数组长度</br>
        /// </summary>
        public uint jSON_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])灰度图像数据数组</br>
        /// </summary>
        public byte[] IDAT;
        /// <summary>
        /// <br>[IN] 灰度图像数据可用数组长度（width * height）</br>
        /// </summary>
        public uint IDAT_MLen;
        /// <summary>
        /// <br>[OUT] 灰度图像数据返回数组长度</br>
        /// </summary>
        public uint IDAT_Len;
        /// <summary>
        /// <br>[SET/OUT] (uint[])图像或视频内置32位256阶调色板数据</br>
        /// </summary>
        public uint[] PLTE;
        /// <summary>
        /// <br>[IN] 调色板数据可用数组长度（256）</br>
        /// </summary>
        public uint PLTE_MLen;
        /// <summary>
        /// <br>[OUT] 调色板数据返回数组长度</br>
        /// </summary>
        public uint PLTE_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])8位动态灰度调色板数据</br>
        /// </summary>
        public byte[] dPLT;
        /// <summary>
        /// <br>[IN] 动态灰度调色板可用数组长度（256）</br>
        /// </summary>
        public uint dPLT_MLen;
        /// <summary>
        /// <br>[OUT] 动态灰度调色板返回数组长度</br>
        /// </summary>
        public uint dPLT_Len;
        /// <summary>
        /// <br>[SET/OUT] (short[])原始16位温度数据数组(必选项)</br>
        /// </summary>
        public short[] tEMP;
        /// <summary>
        /// <br>[IN] 原始16位温度数据可用数组长度（width * height）</br>
        /// </summary>
        public uint tEMP_MLen;
        /// <summary>
        /// <br>[OUT] 原始16位温度数据返回数组长度</br>
        /// </summary>
        public uint tEMP_Len;
        /// <summary>
        /// <br>[SET/OUT] (short[])滤波后的16位温度数据数组（如果滤波启用）</br>
        /// </summary>
        public short[] tEMP2;
        /// <summary>
        /// <br>[IN] 滤波后的16位温度数据数组可用数组长度（width * height）</br>
        /// </summary>
        public uint tEMP2_MLen;
        /// <summary>
        /// <br>[OUT] 滤波后的16位温度数据数组返回数组长度</br>
        /// </summary>
        public uint tEMP2_Len;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像或视频内置的矫正数据</br>
        /// </summary>
        public byte[] cALI;
        /// <summary>
        /// <br>[IN] 图像或视频内置的矫正数据可用数组长度（固定长度180）</br>
        /// </summary>
        public uint cALI_MLen;
        /// <summary>
        /// <br>[OUT] 图像或视频内置的矫正数据返回数组长度</br>
        /// </summary>
        public uint cALI_Len;
        /// <summary>
        /// <br>[SET/OUT] (int[])红外图像动态调色板温度映射范围（长度4，[0]：最低温， [1]：最高温，[2]：调色板低位对应颜色index， [3]：调色板高位对应颜色index）</br>
        /// </summary>
        public int[] dTHR;
        /// <summary>
        /// <br>[IN] 红外图像动态调色板温度映射范围数据可用数组长度（4）</br>
        /// </summary>
        public uint dTHR_MLen;
        /// <summary>
        /// <br>[OUT] 红外图像动态调色板温度映射范围数据返回数组长度</br>
        /// </summary>
        public uint dTHR_Len;
        /// <summary>
        /// <br>[OUT] 温度数据位数</br>
        /// </summary>
        public byte tempBits;
        /// <summary>
        /// <br>[OUT] 实际温度数据换算移位值</br>
        /// </summary>
        public byte tempDiv;
        /// <summary>
        /// <br>[OUT] 实际温度数据换算偏移值</br>
        /// </summary>
        public int tempOffset;
    }

    /// <summary>
    /// <br>16位温度矩阵格式化结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    //
    public struct Temperature16Bits
    {
        Temperature16Bits(uint minIndex = 0, uint maxIndex = 0, byte tempDiv = 0, int tempOffset = 0, short[] buffer = null, uint length = 0)
        {
            this.minIndex = minIndex;
            this.maxIndex = maxIndex;
            this.tempDiv = tempDiv;
            this.tempOffset = tempOffset;
            this.length = length;
            this.buffer = buffer;
        }
        /// <summary>
        /// <br>最低温度索引</br>
        /// </summary>
        public uint minIndex;
        /// <summary>
        /// <br>最高温度索引</br>
        /// </summary>
        public uint maxIndex;
        /// <summary>
        /// <br>移位值</br>
        /// </summary>
        public byte tempDiv;
        /// <summary>
        /// <br>补偿值</br>
        /// </summary>
        public int tempOffset;
        /// <summary>
        /// <br>数据数组长度</br>
        /// </summary>
        public uint length;
        /// <summary>
        /// <br>数据数组</br>
        /// </summary>
        public short[] buffer;
    }
    ;

    /// <summary>
    /// <br>可见光图相机回调帧数据结构体（需先申请内存）</br>
    /// <br>申请内存：IRIP_API_viFrameMemoryAllocation()</br>
    /// <br>释放内存：IRIP_API_viFrameMemoryRelease()</br>
    /// <br>将结构体指针传给相应接口，回调后使用</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct VICallbackFrameData
    {
        /// <summary>
        /// <br>[OUT] 帧序号</br>
        /// </summary>
        public uint index;
        /// <summary>
        /// <br>[OUT] 图像高度</br>
        /// </summary>
        public ushort width;
        /// <summary>
        /// <br>[OUT] 图像宽度</br>
        /// </summary>
        public ushort height;
        /// <summary>
        /// <br>[OUT] 时间戳</br>
        /// </summary>
        public long time;
        /// <summary>
        /// <br>[SET/OUT] (byte[])图像数据数组 RAW/BMP</br>
        /// </summary>
        public IntPtr IMGA;
        /// <summary>
        /// <br>[IN] 图像数据可用数组长度</br>
        /// </summary>
        public uint IMGA_MLen;
        /// <summary>
        /// <br>[OUT] 图像数据返回数组长度</br>
        /// </summary>
        public uint IMGA_Len;
        /// <summary>
        /// <br>[IN] 设定返回的图像宽度，暂时不支持插值，请与原始尺寸保持一致</br>
        /// </summary>
        public ushort IMGA_W;
        /// <summary>
        /// <br>[IN] 设定返回的图像高度，暂时不支持插值，请与原始尺寸保持一致</br>
        /// </summary>
        public ushort IMGA_H;
        /// <summary>
        /// <br>[IN] 设定返回的图像类型（VI.RAW(0): RAW/RGB(如果相机输出24位图像), VI.BMP(1): BMP, VI.RGBA(2), VI.BGRA(3), VI.ARGB(4), VI.ABGR(5))</br>
        /// </summary>
        public uint IMGA_Type;
        /// <summary>
        /// <br>[OUT] 图像是否已旋转方向(90°|270°)，旋转后输出的IMGA图像实际长宽比对调，IMGA_W、IMGA_H为输入参数保持不变，使用时通过rotated参数判断</br>
        /// </summary>
        public bool rotated;
        /// <summary>
        /// <br>[OUT] 返回的图像位数</br>
        /// </summary>
        public ushort bitCount;

    }

    /// <summary>
    /// <br>可见光图相机回调帧数据结构体，从VICallbackFrameData拷贝数据使用</br>
    /// <br>释放内存：IRIP_API_viFrameMemoryRelease()</br>
    /// <br>将结构体指针传给相应接口，回调后使用</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VICallbackFrameData2
    {
        /// <summary>
        /// <br>[OUT] 帧序号</br>
        /// </summary>
        public uint index;
        /// <summary>
        /// <br>[OUT] 图像高度</br>
        /// </summary>
        public ushort width;
        /// <summary>
        /// <br>[OUT] 图像宽度</br>
        /// </summary>
        public ushort height;
        /// <summary>
        /// <br>[OUT] 时间戳</br>
        /// </summary>
        public long time;
        /// <summary>
        /// <br>[SET/OUT] 图像数据数组 RAW/BMP</br>
        /// </summary>
        public byte[] IMGA;
        /// <summary>
        /// <br>[IN] 图像数据可用数组长度</br>
        /// </summary>
        public uint IMGA_MLen;
        /// <summary>
        /// <br>[OUT] 图像数据返回数组长度</br>
        /// </summary>
        public uint IMGA_Len;
        /// <summary>
        /// <br>[IN] 设定返回的图像宽度，暂时不支持插值，请与原始尺寸保持一致</br>
        /// </summary>
        public ushort IMGA_W;
        /// <summary>
        /// <br>[IN] 设定返回的图像高度，暂时不支持插值，请与原始尺寸保持一致</br>
        /// </summary>
        public ushort IMGA_H;
        /// <summary>
        /// <br>[IN] 设定返回的图像类型（VI.RAW(0): RAW/RGB(如果相机输出24位图像), VI.BMP(1): BMP, VI.RGBA(2), VI.BGRA(3), VI.ARGB(4), VI.ABGR(5))</br>
        /// </summary>
        public uint IMGA_Type;
        /// <summary>
        /// <br>[OUT] 图像是否已旋转方向(90°|270°)，旋转后输出的IMGA图像实际长宽比对调，IMGA_W、IMGA_H为输入参数保持不变，使用时通过rotated参数判断</br>
        /// </summary>
        public bool rotated;
        /// <summary>
        /// <br>[OUT] 返回的图像位数</br>
        /// </summary>
        public ushort bitCount;

    }

    /// <summary>
    /// <br>MDMap数据行坐标结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct xIndex
    {
        public xIndex(int y = 0, int l = 0, int r = 0)
        {
            this.y = y;
            this.l = l;
            this.r = r;
        }
        /// <summary>
        /// <br>测量y向坐标/br>
        /// </summary>
        public int y;
        /// <summary>
        /// <br>测量x向起始坐标/br>
        /// </summary>
        public int l;
        /// <summary>
        /// <br>测量x向结束坐标/br>
        /// </summary>
        public int r;
    }

    /// <summary>
    /// <br>MDMap数据结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct MDMapStruct
    {
        public MDMapStruct(int sx = 0, int sy = 0, int ex = 0, int ey = 0)
        {
            this.sx = sx;
            this.sy = sy;
            this.ex = ex;
            this.ey = ey;
            // 创建一个vector
            var vec = new xIndex[0];
            // 将数组转换为IntPtr，传递给C++函数
            GCHandle handle = GCHandle.Alloc(vec, GCHandleType.Pinned);
            this.xIndexs  = handle.AddrOfPinnedObject();
            this.crc = 0;
        }
        /// <summary>
        /// <br>测量元素起始x坐标</br>
        /// </summary>
        public int sx;
        /// <summary>
        /// <br>测量元素起始y坐标</br>
        /// </summary>
        public int sy;
        /// <summary>
        /// <br>测量元素结束x坐标</br>
        /// </summary>
        public int ex;
        /// <summary>
        /// <br>测量元素结束y坐标</br>
        /// </summary>
        public int ey;
        /// <summary>
        /// <br>vector结构体xIndex指针</br>
        /// <br>[SET]矩形区域、椭圆和任意多边形测量模式预计算测量坐标信息，预计算数据</br>
        /// <br>如矩形区域不包含倒角等扩展参数不使用此数据，椭圆和多边形必须使用，否则无法获得测量结果</br>
        /// </summary>
        public IntPtr xIndexs;
        /// <summary>
        /// <br>测量数据校验值</br>
        /// </summary>
        public uint crc;
    }

    /// <summary>
    /// <br>测量数据结构体</br>
    /// <br>注意：自IRIP库2.0.0.48版本后sx、sy、ex、ey由16位有符号类型改为32位无符号类型，以适应坐标可以设置超出图像范围的问题</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] 
    public struct Measurement_Data
    {
        /// <summary>
        /// <br>[IN] 0 - 127，测点粘滞功能数组最大支持128个结构体数据，超出可正常测温但无粘滞功能</br>
        /// </summary>
        public ushort id;
        /// <summary>
        /// <br>[IN] MD.CENTER.ID(-1) / MD.SPOT.ID(0) / MD.HILO.ID(1) / MD.AREA.ID(2) / MD.LINE.ID(3) / MD.POLY.ID(6) / MD.CIRC.ID(7)</br>
        /// </summary>
        public short mode;
        /// <summary>
        /// <br>[IN] 测点黏滞防止测温点快速跳变，温度阈值(摄氏度*100)，0-100, 0: 关闭，>100时为0.1摄氏度</br>
        /// </summary>
        public ushort thr;
        /// <summary>
        /// <br>[IN] 起始x坐标（点测温/矩形区域测温/线测温）</br>
        /// <br>椭圆和多边形时仅表示其所占矩形区域的坐标，不作为实际测量坐标使用</br>
        /// </summary>
        public int sx;
        /// <summary>
        /// <br>[IN] 起始y坐标（点测温/矩形区域测温/线测温）</br>
        /// <br>椭圆和多边形时仅表示其所占矩形区域的坐标，不作为实际测量坐标使用</br>
        /// </summary>
        public int sy;
        /// <summary>
        /// <br>[IN] 结束x坐标（矩形区域测温/线测温）</br>
        /// <br>椭圆和多边形时仅表示其所占矩形区域的坐标，不作为实际测量坐标使用</br>
        /// </summary>
        public int ex;
        /// <summary>
        /// <br>[IN] 结束y坐标（矩形区域测温/线测温）</br>
        /// <br>椭圆和多边形时仅表示其所占矩形区域的坐标，不作为实际测量坐标使用</br>
        /// </summary>
        public int ey;
        /// <summary>
        /// <br>[OUT] 测量温度检查的像素数, 0表示超出图像区域，测量结果无效</br>
        /// </summary>
        public int cnum;
        /// <summary>
        /// <br>[OUT] 最低温度（中心/点测温/高低温/矩形区域测温/线测温）</br>
        /// </summary>
        public int min;
        /// <summary>
        /// <br>[OUT] 最高温度（高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int max;
        /// <summary>
        /// <br>[OUT] 最低温x坐标（中心/点测温/高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int minx;
        /// <summary>
        /// <br>[OUT] 最低温y坐标（中心/点测温/高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int miny;
        /// <summary>
        /// <br>[OUT] 最高温x坐标（中心/高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int maxx;
        /// <summary>
        /// <br>[OUT] 最高温y坐标（高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int maxy;
        /// <summary>
        /// <br>[OUT] 平均温度（高低温/矩形区域测温/线测温/椭圆/多边形）</br>
        /// </summary>
        public int avg;
        /// <summary>
        /// <br>结构体MDMapStruct指针</br>
        /// <br>[SET]矩形区域、椭圆和任意多边形测量模式预计算测量坐标信息结构体，该数据需预计算，使用时引用</br>
        /// <br>如矩形区域不包含倒角等扩展参数不使用此数据，椭圆和多边形必须使用，否则无法获得测量结果</br>
        /// </summary>
        public MDMapStruct[] mdMap;
    };

    /// <summary>
    /// <br>Int32Point结构体</br>
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    public struct CInt32Point
    {
        public CInt32Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// <br>坐标x</br>
        /// </summary>
        public int x;
        /// <summary>
        /// <br>坐标y</br>
        /// </summary>
        public int y;
    }

    /// <summary>
    /// <br>Int32Rect结构体</br>
    /// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    public struct CInt32Rect
    {
        public CInt32Rect(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// <br>起始坐标x</br>
        /// </summary>
        public int x;
        /// <summary>
        /// <br>起始坐标y</br>
        /// </summary>
        public int y;
        /// <summary>
        /// <br>宽度</br>
        /// </summary>
        public int width;
        /// <summary>
        /// <br>高度</br>
        /// </summary>
        public int height;
    }

    /// <summary>
    /// <br>ISO模式数据结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TempISO
    {
        public TempISO(bool lowEnable = false, bool highEnable = false, int lowTemp = 1500, int highTemp = 5000, int lowPIndex = -1, int highPIndex = 256, int param1 = 0, int param2 = 0)
        {
            this.lowEnable = lowEnable;
            this.highEnable = highEnable;
            this.lowTemp = lowTemp;
            this.highTemp = highTemp;
            this.lowPIndex = lowPIndex;
            this.highPIndex = highPIndex;
            this.param1 = param1;
            this.param2 = param2;
        }
        /// <summary>
        /// <br>低温是否开启</br>
        /// </summary>
        public bool lowEnable;
        /// <summary>
        /// <br>高温是否开启</br>
        /// </summary>
        public bool highEnable;
        /// <summary>
        /// <br>低温阈值</br>
        /// </summary>
        public int lowTemp;
        /// <summary>
        /// <br>高温阈值</br>
        /// </summary>
        public int highTemp;
        /// <summary>
        /// <br>低温调色板映射上限0-255</br>
        /// </summary>
        public int lowPIndex;
        /// <summary>
        /// <br>高温调色板映射下限0-255/br>
        /// </summary>
        public int highPIndex;
        /// <summary>
        /// <br>预留参数1/br>
        /// </summary>
        public int param1;
        /// <summary>
        /// <br>预留参数2/br>
        /// </summary>
        public int param2;
    }

    /// <summary>
    /// <br>温度云图结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TempNephogram
    {
        public TempNephogram(uint count = 0, int[] min = null, int[] max = null, uint[] color = null, int[] ishide = null, uint[] cnum = null, float rate = 1.0f, float dpi = 72.0f)
        {
            this.count = count;
            this.min = min;
            this.max = max;
            this.color = color;
            this.ishide = ishide;
            this.cnum = cnum;
            this.rate = rate;
            this.dpi = dpi;
        }
        /// <summary>
        /// <br>[IN] 温度颜色段数量</br>
        /// </summary>
        public uint count;
        /// <summary>
        /// <br>[IN] 低温温度数组</br>
        /// </summary>
        public int[] min;
        /// <summary>
        /// <br>[IN] 高温温度数组</br>
        /// </summary>
        public int[] max;
        /// <summary>
        /// <br>[IN] 对应颜色数组</br>
        /// </summary>
        public uint[] color;
        /// <summary>
        /// <br>[IN] 隐藏显示颜色</br>
        /// </summary>
        public int[] ishide;
        /// <summary>
        /// <br>[OUT] 对应颜色像素数数组</br>
        /// </summary>
        public uint[] cnum;
        /// <summary>
        /// <br>[OUT] 插值比（实际检测图像可能存在插值），计算像素数或面积应乘以此值</br>
        /// </summary>
        public float rate;
        /// <summary>
        /// <br>[IN] 分辨率DPI</br>
        /// </summary>
        public float dpi;
    }

    /// <summary>
    /// <br>全局测量参数结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MDParameters
    {
        public MDParameters(uint unit = 0, float compensation = 0.0f, float emissivity = 1.0f, float reflected = 20.0f, float distance = 1.0f, float winTmte = 1.0f, float winTemp = 20.0f)
        {
            this.unit = unit < 0 ? 0 : unit > 2 ? 2 : unit;
            this.compensation = compensation;
            this.emissivity = emissivity > 1.0f ? 1.0f : emissivity < 0.001f ? 0.001f : emissivity;
            this.reflected = reflected;
            this.distance = distance < 0.0f ? 0.0f : distance;
            this.winTmte = winTmte > 1.0f ? 1.0f : winTmte < 0.001f ? 0.001f : winTmte;
            this.winTemp = winTemp;
        }
        /// <summary>
        /// <br>温度单位, 0: 摄氏度, 1: 华氏度, 2: 开尔文</br>
        /// </summary>
        public uint unit;
        /// <summary>
        /// <br>温度补偿</br>
        /// </summary>
        public float compensation;
        /// <summary>
        /// <br>发射率, 0.001f ~ 1.0f</br>
        /// </summary>
        public float emissivity;
        /// <summary>
        /// <br>反射温度</br>
        /// </summary>
        public float reflected;
        /// <summary>
        /// <br>测量距离（米）</br>
        /// </summary>
        public float distance;
        /// <summary>
        /// <br>透射率, 0.001f ~ 1.0f</br>
        /// </summary>
        public float winTmte;
        /// <summary>
        /// <br>窗口温度</br>
        /// </summary>
        public float winTemp;
    }

    /// <summary>
    /// <br>非均匀校正回调状态结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NUCStates
    {
        public NUCStates(int tempType = 0, bool nucEnable = false, bool nucDataTrue = false)
        {
            this.tempType = tempType;
            this.nucEnable = nucEnable;
            this.nucDataTrue = nucDataTrue;
        }

        /// <summary>
        /// <br>温度类型, 0: 120, 1: 400</br>
        /// </summary>
        public int tempType;
        /// <summary>
        /// <br>是否已启用均匀度校正</br>
        /// </summary>
        public bool nucEnable;
        /// <summary>
        /// <br>当前使用的校准数据组均匀度校正数据是否存在</br>
        /// </summary>
        public bool nucDataTrue;
    }

    /// <summary>
    /// <br>UVC相机参数设置结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IAMConfig
    {
        public IAMConfig(int id = 0, bool enable = false, long min = 0, long max = 0, long step = 0, long def = 0, long capsFlags = 0, long val = 0, long flags = 0)
        {
            this.id = id;
            this.enable = enable;
            this.min = min;
            this.max = max;
            this.step = step;
            this.def = def;
            this.capsFlags = capsFlags;
            this.val = val;
            this.flags = flags;
        }

        /// <summary>
        /// <br>属性ID</br>
        /// <br>IRIP_API.VideoProcAmpProperty 参数, value: 0-9</br>
        /// <br>或 IRIP_API.CameraControlProperty 参数, value: 0-6</br>
        /// </summary>
        public int id;
        /// <summary>
        /// <br>是否支持该属性</br>
        /// </summary>
        public bool enable;
        /// <summary>
        /// <br>最小调整范围</br>
        /// </summary>
        public long min;
        /// <summary>
        /// <br>最大调整范围</br>
        /// </summary>
        public long max;
        /// <summary>
        /// <br>调整步长</br>
        /// </summary>
        public long step;
        /// <summary>
        /// <br>默认值</br>
        /// </summary>
        public long def;
        /// <summary>
        /// <br>是否可自动调整(2: 只能手动, 3:可自动)</br>
        /// </summary>
        public long capsFlags;
        /// <summary>
        /// <br>设置值，当设置值为0xFFFFFFFF时应用默认值</br>
        /// </summary>
        public long val;
        /// <summary>
        /// <br>手动/自动调整(1: 自动, 2:手动)</br>
        /// <br>IRIP_API.VideoProcAmpFlags参数</br>
        /// <br>或 IRIP_API.CameraControlFlags参数</br>
        /// </summary>
        public long flags;
    }

    /// <summary>
    /// <br>HJK网络相机地址结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct T_IPADDR
    {
        /// <summary>
        /// <br>IP</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string IPAddr;
        /// <summary>
        /// <br>保留</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved;
        /// <summary>
        /// <br>Port</br>
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int DataPort;
        /// <summary>
        /// <br>是否在线</br>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public byte isValid;
        /// <summary>
        /// <br>在线个数</br>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public byte totalOnline;
        /// <summary>
        /// <br>在列表中的索引</br>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public byte Index;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct T_IPADDRS
    {
        /// <summary>
        /// <br>IP</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)] //要获取的数量不能大于该项设置值，否则可能会引发异常
        public T_IPADDR[] CamDevics;

        /// <summary>
        /// <para>@param empty 可为任意值</para>
        /// </summary>
        public T_IPADDRS(int empty)
        {
            this.CamDevics = new T_IPADDR[8];
        }
    }

    /// <summary>
    /// <br>HJK网络相机设备信息，均以字符串形式存储，该空间已经开辟，不需要再申请空间</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct T_DEVICE_INFO
    {
        /// <summary>
        /// <br>设备名称</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;
        /// <summary>
        /// <br>设备型号</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Model;
        /// <summary>
        /// <br>设备序列号</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string SerialNum;
        /// <summary>
        /// <br>头</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Lens;
        /// <summary>
        /// <br>出厂时间</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FactoryTime;
        /// <summary>
        /// <br>工作时间</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string WorkTime;
        /// <summary>
        /// <br>MAC</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Mac;
        /// <summary>
        /// <br>IP</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string IP;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved2;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved3;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved4;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved5;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved6;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved7;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Reserved8;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HostDevices
    {
        /// <summary>
        /// <br>IP</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)] //要获取的数量不能大于该项设置值，否则可能会引发异常
        public HostDevice[] devices;

        /// <summary>
        /// <para>@param empty 可为任意值</para>
        /// </summary>
        public HostDevices(int empty)
        {
            this.devices = new HostDevice[8];
        }
    }

    /// <summary>
    /// <br>红外相机主机搜索结构体</br>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HostDevice
    {
        /// <summary>
        /// <br>SID</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sid;

        /// <summary>
        /// <br>HOST</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string hostName;

        /// <summary>
        /// <br>服务器别名</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string alias;

        /// <summary>
        /// <br>IP地址</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ip;

        /// <summary>
        /// <br>IPv6地址</br>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string ipv6;

        /// <summary>
        /// <br>连接端口号</br>
        /// </summary>
        public ushort port;
    }
}
