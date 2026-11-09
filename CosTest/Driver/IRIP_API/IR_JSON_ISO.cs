using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_ISO
    {
        /// <summary>  
        /// 低温模式是否启用
        /// </summary> 
        public bool lowEnable { get; set; }

        /// <summary>  
        /// 高温模式是否启用
        /// </summary> 
        public bool highEnable { get; set; }

        /// <summary>  
        /// 低温
        /// </summary> 
        public int lowTemp { get; set; }

        /// <summary>  
        /// 高温
        /// </summary> 
        public int highTemp { get; set; }

        /// <summary>  
        /// 低温调色板映射上限0-255
        /// </summary> 
        public int lowPIndex { get; set; }

        /// <summary>  
        /// 高温调色板映射下限0-255
        /// </summary> 

        public int highPIndex { get; set; }

        public IR_JSON_ISO()
        {
            lowEnable = false;
            highEnable = false;
            lowTemp = 1500;
            highTemp = 5000;
            lowPIndex = -1;
            highPIndex = -1;
        }
    }
}
