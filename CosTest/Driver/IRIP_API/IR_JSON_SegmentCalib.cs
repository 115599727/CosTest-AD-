using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_SegmentCalib
    {
        /// <summary>  
        /// 分段校正是否启用，如果校正数据存在则应用校正
        /// </summary>
        public bool enable { get; set; } 

        /// <summary>  
        /// 分段校准数使用数据组名称：NV/LOW/MED/HIGH
        /// </summary>
        public string group { get; set; }

        public IR_JSON_SegmentCalib()
        {
            enable = false;
            group = "NV";
        }
    }
}
