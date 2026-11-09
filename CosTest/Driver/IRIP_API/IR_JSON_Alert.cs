using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Alert
    {
        /// <summary>  
        /// 是否开启低温报警
        /// </summary> 
        public bool lowEnable { get; set; }

        /// <summary>  
        /// 是否开启高温报警
        /// </summary> 
        public bool highEnable { get; set; }

        /// <summary>  
        /// 低温报警温度 int类型，实际温度(摄氏度)=temp/100f
        /// </summary> 
        public int lowTemp { get; set; }

        /// <summary>  
        /// 高温报警温度 int类型，实际温度(摄氏度)=temp/100f
        /// </summary> 
        public int highTemp { get; set; }

        public IR_JSON_Alert()
        {
            lowEnable = false;
            highEnable = false;
            lowTemp = 0;
            highTemp = 5000;
        }
    }
}
