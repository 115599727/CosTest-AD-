using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Show
    {
        /// <summary>  
        /// 显示模式 0：红外，1：可见光，2：融合
        /// </summary>
        public int mode { get; set; } 

        /// <summary>  
        /// 调色板palette名称
        /// </summary>
        public string pName { get; set; } 

        /// <summary>  
        /// 调色板index
        /// </summary>
        public int pIndex { get; set; }

        /// <summary>  
        /// 图像增益限制（已弃用，图像渲染相关已转移至IRIP设置）
        /// </summary>
        //public bool gainLimit { get; set; }

        /// <summary>  
        /// 图像增益限制值（已弃用，图像渲染相关已转移至IRIP设置）
        /// </summary>
        //public float gainValue { get; set; } 

        /// <summary>  
        /// 红外图像旋转角度: 0,90,180,270
        /// </summary>
        public int rotate
        {
            get
            {
                return _rotate;
            }
            set
            {
                switch (value)
                {
                    case 90:
                        _rotate = 90;
                        break;
                    case 180:
                        _rotate = 180;
                        break;
                    case 270:
                        _rotate = 270;
                        break;
                    default:
                        _rotate = 0;
                        break;
                }
            }
        }
        private int _rotate;

        /// <summary>  
        /// 红外图像水平镜像
        /// </summary>
        public bool mirrorHorizontal { get; set; } 

        /// <summary>  
        /// 红外图像垂直镜像
        /// </summary>
        public bool mirrorVertically { get; set; } 

        /// <summary>  
        /// 显示放大倍数, 默认值1.0f
        /// </summary>
        public float zoom { get; set; } 

        /// <summary>  
        /// 超分辨率
        /// </summary>
        public bool superResolution { get; set; } 

        /// <summary>  
        /// 显示日期水印
        /// </summary>
        public bool dateWatermark { get; set; } 

        /// <summary>  
        /// logo水印
        /// </summary>
        public bool logoWatermark { get; set; } 

        /// <summary>  
        /// 显示GPS水印
        /// </summary>
        public bool gpsWatermark { get; set; } 

        /// <summary>  
        /// 温度信息跟随测点显示following
        /// </summary>
        public bool fSpot { get; set; } 

        /// <summary>  
        /// 显示低温测点
        /// </summary>
        public bool showMin { get; set; } 

        /// <summary>  
        /// 显示高温测点
        /// </summary>
        public bool showMax { get; set; } 

        /// <summary>  
        /// 显示平均温度
        /// </summary>
        public bool showAvg { get; set; } 

        /// <summary>  
        /// 显示中心温度点, SPOT模式有效，默认显示
        /// </summary>
        public bool showCenter { get; set; } 

        /// <summary>  
        /// 测点文字显示倍数
        /// </summary>
        public uint textZoom { get; set; } 

        public IR_JSON_Show()
        {
            showCenter = true;
            pIndex = 3;
            pName = "彩虹";
            //gainLimit = true;
            //gainValue = 1.0f;
            zoom = 1.0f;
            fSpot = true;
            textZoom = 1;
        }
    }
}
