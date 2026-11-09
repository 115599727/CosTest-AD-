using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IRIP_API_LIB
{
    /// <summary>
    /// 红外图像渲染
    /// </summary>
    public class IRIP_API_Reader
    {
        private IRIP_API IRIP;
        public void DLL(ref DllInvoke dll, IRIP_API irip)
        {
            IRIP = irip;
#if _X86
    api_GetIRIPConfig = _IRIP_API_GetIRIPConfig;
    api_SetIRIPConfig = _IRIP_API_SetIRIPConfig;
    api_HpfAgc_Int16 = _IRIP_API_HpfAgc_Int16;
    api_HpfLut_Int16 = _IRIP_API_HpfLut_Int16;
    api_Hpf_Int16 = _IRIP_API_Hpf_Int16;
    api_GetPseudoColorImage = _IRIP_API_GetPseudoColorImage;
#else
            api_GetIRIPConfig = (IRIP_API_GetIRIPConfig)dll.Invoke("IRIP_API_GetIRIPConfig", typeof(IRIP_API_GetIRIPConfig));
            api_SetIRIPConfig = (IRIP_API_SetIRIPConfig)dll.Invoke("IRIP_API_SetIRIPConfig", typeof(IRIP_API_SetIRIPConfig));
            api_HpfAgc_Int16 = (IRIP_API_HpfAgc_Int16)dll.Invoke("IRIP_API_HpfAgc_Int16", typeof(IRIP_API_HpfAgc_Int16));
            api_HpfLut_Int16 = (IRIP_API_HpfLut_Int16)dll.Invoke("IRIP_API_HpfLut_Int16", typeof(IRIP_API_HpfLut_Int16));
            api_Hpf_Int16 = (IRIP_API_Hpf_Int16)dll.Invoke("IRIP_API_Hpf_Int16", typeof(IRIP_API_Hpf_Int16));
            api_GetPseudoColorImage = (IRIP_API_GetPseudoColorImage)dll.Invoke("IRIP_API_GetPseudoColorImage", typeof(IRIP_API_GetPseudoColorImage));
#endif
        }

#if _X86
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_SetIRIPConfig", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_SetIRIPConfig(int id, ref IRIP_Config config);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_HpfAgc_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_HpfAgc_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_HpfLut_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_HpfLut_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_Hpf_Int16", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_Hpf_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
  
  [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_GetPseudoColorImage", CallingConvention = CallingConvention.StdCall)]
  static private extern int _IRIP_API_GetPseudoColorImage(int id, byte[] imageGrey, uint[] imageColor, int width, int height, uint[] palette);
#endif

        private delegate int IRIP_API_GetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_GetIRIPConfig api_GetIRIPConfig;

        private delegate int IRIP_API_SetIRIPConfig(int id, ref IRIP_Config config);
        private IRIP_API_SetIRIPConfig api_SetIRIPConfig;

        private delegate int IRIP_API_HpfAgc_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
        private IRIP_API_HpfAgc_Int16 api_HpfAgc_Int16;

        private delegate int IRIP_API_HpfLut_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
        private IRIP_API_HpfLut_Int16 api_HpfLut_Int16;

        private delegate int IRIP_API_Hpf_Int16(int id, short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette, int[] dThr);
        private IRIP_API_Hpf_Int16 api_Hpf_Int16;

        private delegate int IRIP_API_GetPseudoColorImage(int id, byte[] imageGrey, uint[] imageColor, int width, int height, uint[] palette);
        private IRIP_API_GetPseudoColorImage api_GetPseudoColorImage;

        /// <summary>
        /// <br>获取IRIP设置参数</br>
        /// <para>@param config 配置参数结构体</para>
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
        /// <br>设置IRIP参数</br>
        /// <para>@param config 配置参数结构体</para>
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
        /// <br>16位温度数据输出灰度图象(线性压缩)</br>
        /// <para>@param buffer 输入16位温度数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param width 图像宽度</para>
        /// <para>@param height 图像高度</para>
        /// <para>@param imageGrey 输出灰度图象矩阵数据(数组长度width * height)</para>
        /// <para>@param dPalette 输出动态调色板(数组长度256)</para>
        /// <para>@param dThr 输出渲染高低温度范围及调色板范围数组(数组长度4)</para>
        /// </summary>
        public int HpfAgc_Int16(short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette = null, int[] dThr = null)
        {
            if (api_HpfAgc_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_HpfAgc_Int16(IRIP.GetID(), buffer, tempDiv, tempOffset, width, height, imageGrey, dPalette, dThr);
        }

        /// <summary>
        /// <br>16位温度数据输出灰度图象(直方图增强)</br>
        /// <para>@param buffer 输入16位温度数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param width 图像宽度</para>
        /// <para>@param height 图像高度</para>
        /// <para>@param imageGrey 输出灰度图象矩阵数据(数组长度width * height)</para>
        /// <para>@param dPalette 输出动态调色板(数组长度256)</para>
        /// <para>@param dThr 输出渲染高低温度范围及调色板范围数组(数组长度4)</para>
        /// </summary>
        public int HpfLut_Int16(short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette = null, int[] dThr = null)
        {
            if (api_HpfLut_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_HpfLut_Int16(IRIP.GetID(), buffer, tempDiv, tempOffset, width, height, imageGrey, dPalette, dThr);
        }

        /// <summary>
        /// <br>16位温度数据输出灰度图象(根据IRIPConfig配置选择Agc/Lut)</br>
        /// <para>@param buffer 输入16位温度数据</para>
        /// <para>@param tempDiv</para>
        /// <para>@param tempOffset 温度偏移量</para>
        /// <para>@param width 图像宽度</para>
        /// <para>@param height 图像高度</para>
        /// <para>@param imageGrey 输出灰度图象矩阵数据(数组长度width * height)</para>
        /// <para>@param dPalette 输出动态调色板(数组长度256)</para>
        /// <para>@param dThr 输出渲染高低温度范围及调色板范围数组(数组长度4)</para>
        /// </summary>
        public int Hpf_Int16(short[] buffer, byte tempDiv, int tempOffset, int width, int height, byte[] imageGrey, byte[] dPalette = null, int[] dThr = null)
        {
            if (api_Hpf_Int16 == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_Hpf_Int16(IRIP.GetID(), buffer, tempDiv, tempOffset, width, height, imageGrey, dPalette, dThr);
        }

        /// <summary>
        /// <br>灰度图像+调色板映射彩色图像</br>
        /// <para>@param imageGrey 输入8位灰度图像</para>
        /// <para>@param imageColor 输出32位彩色图像</para>
        /// <para>@param width 图像宽度</para>
        /// <para>@param height 图像高度</para>
        /// <para>@param palette 32位调色板(数组长度256)</para>
        /// </summary>
        public int GetPseudoColorImage(byte[] imageGrey, uint[] imageColor, int width, int height, uint[] palette)
        {
            if (api_GetPseudoColorImage == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_GetPseudoColorImage(IRIP.GetID(), imageGrey, imageColor, width, height, palette);
        }

    }
}
