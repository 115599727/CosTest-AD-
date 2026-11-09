using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_TfpaCalib
    {
        /// <summary>  
        /// 配置数据类
        /// </summary>
        public class Mark
        {
            /// <summary>  
            /// Fpa温度
            /// </summary>
            int tfpa { get; set; }

            /// <summary>  
            /// 校准温度列表
            /// </summary>
            List<int> temp { get; set; }

            /// <summary>  
            /// 偏移量列表
            /// </summary>
            List<int> offset { get; set; }
        }

        /// <summary>  
        /// Fpa温度
        /// </summary>
        int tfpa { get; set; }

        /// <summary>  
        /// 低温段配置数据
        /// </summary>
        Mark minMark { get; set; }

        /// <summary>  
        /// 高温段配置数据
        /// </summary>
        Mark maxMark { get; set; }
    }

}
