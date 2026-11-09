using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Delta
    {
        /// <summary>  
        /// 是否启用
        /// </summary> 
        public bool enable { get; set; }

        /// <summary>  
        /// 0: 高温温差, 1:低温温差, 2:平均温差, 只适用LINE和AREA模式
        /// </summary> 
        public int mode { get; set; }

        /// <summary>  
        /// 基准温度
        /// </summary> 
        public int ratedTemp { get; set; }

        /// <summary>  
        /// 基准温度选择, -2: 使用基准温度值, >=0为选点index
        /// </summary> 
        public int selectHi { get; set; } 

        /// <summary>  
        /// 基准温度选择, -2: 使用基准温度值, -1: 以中心点温度为基准, >=0为测点index
        /// </summary> 
        public int selectSp { get; set; } 

        /// <summary>  
        /// 基准温度选择, -2: 以中心点温度为基准, >=0为线index
        /// </summary> 
        public int selectLi { get; set; } 

        /// <summary>  
        /// 基准温度选择, -2: 以中心点温度为基准, >=0为区域index
        /// </summary> 
        public int selectAr { get; set; } 

        public IR_JSON_Delta()
        {
            ratedTemp = 5000;
            selectHi = -2;
            selectSp = -2;
            selectLi = -2;
            selectAr = -2;
        }
    }
}
