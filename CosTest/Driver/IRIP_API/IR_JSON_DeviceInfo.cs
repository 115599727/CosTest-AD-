using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_DeviceInfo
    {
        /// <summary>  
        /// 红外驱动器类型, 0:irCamera, 1:thCamera
        /// </summary> 
        public int iType { get; set; }

        /// <summary>  
        /// 可见光相机类型
        /// </summary> 
        public int vType { get; set; } 

        /// <summary>  
        /// 红外驱动器名称
        /// </summary> 
        public string iName { get; set; } 

        /// <summary>  
        /// 可见光相机名称
        /// </summary> 
        public string vName { get; set; } 

        /// <summary>  
        /// 文本序列号
        /// </summary> 
        public string iSerial { get; set; } 

        /// <summary>  
        /// 固件版本
        /// </summary> 
        public int firmwareNumber { get; set; } 

        /// <summary>  
        /// 硬件版本
        /// </summary> 
        public int hardwareNumber { get; set; } 

        /// <summary>  
        /// 主板序列号
        /// </summary> 
        public int modularSerialNumber { get; set; }

        public IR_JSON_DeviceInfo()
        {
            iName = "";
            vName = "";
            iSerial = "";
        }
    }
}
