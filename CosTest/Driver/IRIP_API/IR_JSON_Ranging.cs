using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Ranging
    {
        /// <summary>  
        /// 是否启用
        /// </summary>
        public bool enable { get; set; }

        /// <summary>  
        /// 距离, 单位m
        /// </summary>
        public float distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value < 0.00f ? 0.00f : value;
            }
        }
        private float _distance;

        /// <summary>  
        /// 距离调整系数0-0.1;
        /// </summary>
        public float adjustment { get; set; } 

        public IR_JSON_Ranging()
        {
            enable = false;
            distance = 1.0f;
            adjustment = 0.0f;
        }
    }
}
