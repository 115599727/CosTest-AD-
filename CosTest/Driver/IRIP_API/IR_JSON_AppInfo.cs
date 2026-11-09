using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_AppInfo
    {
        /// <summary>  
        /// 名称
        /// </summary> 
        public string name { get; set; }

        /// <summary>  
        /// 版本名称
        /// </summary> 
        public string version { get; set; }

        /// <summary>  
        /// 版本号
        /// </summary> 
        public int vercode { get; set; } //版本号

        public IR_JSON_AppInfo()
        {
            name = "IRIP_API";
            version = "2.0.0.51";
            vercode = 61;
        }
    }
}
