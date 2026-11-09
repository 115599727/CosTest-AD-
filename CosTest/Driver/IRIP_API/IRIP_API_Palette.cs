using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IRIP_API_LIB
{
    /// <summary>
    /// <br>调色板</br>
    /// </summary>
    public class IRIP_API_Palette
    {
        public void DLL(ref DllInvoke dll)
        {

#if _X86
            api_getIRPalettesNUM = _IRIP_API_getIRPalettesNUM;
            api_getIRPalettes = _IRIP_API_getIRPalettes;
#else
            api_getIRPalettesNUM = (IRIP_API_getIRPalettesNUM)dll.Invoke("IRIP_API_getIRPalettesNUM", typeof(IRIP_API_getIRPalettesNUM));
            api_getIRPalettes = (IRIP_API_getIRPalettes)dll.Invoke("IRIP_API_getIRPalettes", typeof(IRIP_API_getIRPalettes));
#endif

        }

#if _X86
        [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_getIRPalettesNUM", CallingConvention = CallingConvention.StdCall)]
        static private extern int _IRIP_API_getIRPalettesNUM();

        [DllImport(IRIP_API.LIB_FILE, EntryPoint = "IRIP_API_getIRPalettes", CallingConvention = CallingConvention.StdCall)]
        static private extern int _IRIP_API_getIRPalettes(ref IR_PALETTES palettes);
#endif

        private delegate int IRIP_API_getIRPalettesNUM();
        private IRIP_API_getIRPalettesNUM api_getIRPalettesNUM;
        /// <summary>
        ///  br>获取内置调色板数量</br>
        /// <para>@return 返回数量</para>
        /// </summary>
        public int getIRPalettesNUM()
        {
            if (api_getIRPalettesNUM == null)
            {
                return IRIP_API.RET.API_FUNCTION_NULL;
            }
            return api_getIRPalettesNUM();
        }

        private delegate int IRIP_API_getIRPalettes(ref IR_PALETTES palettes);
        private IRIP_API_getIRPalettes api_getIRPalettes;
        /// <summary>
        /// <br>获取内置调色板数据</br>
        /// <para>@param palettes</para>
        /// <para>@return 0</para>
        /// </summary>
        public int getIRPalettes(ref IR_PALETTES palettes)
        {
            if (api_getIRPalettes == null)
            {
                palettes.num = 0;
                return IRIP_API.RET.API_FUNCTION_NULL;
            }

            return api_getIRPalettes(ref palettes);
        }
    }
}
