using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Fusion
    {
        /// <summary>  
        /// 是否启用
        /// </summary> 
        public bool enable { get; set; }

        /// <summary>  
        /// 可见光width
        /// </summary> 
        public int visibleW { get; set; }

        /// <summary>  
        /// 可见光height
        /// </summary> 
        public int visibleH { get; set; }

        /// <summary>  
        /// 放大倍数
        /// </summary> 
        public float zoom {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value < 0.1f ? 0.1f : value;
            }
        }
        private float _zoom;

        /// <summary>  
        /// 默认中心坐标对齐
        /// </summary> 
        public string align { get; set; } 

        /// <summary>  
        /// 偏移量倍数, 旧版本 默认倍数0.8, 新版默认倍数为1，无需换算;
        /// </summary> 
        public float offsetZ { get; set; } 

        /// <summary>  
        /// X轴对齐偏移量, offsetX * offsetZ
        /// </summary> 
        public float offsetX { get; set; }

        /// <summary>  
        /// Y轴对齐偏移量, offsetY * offsetZ
        /// </summary> 
        public float offsetY { get; set; } 

        /// <summary>  
        /// 整体透明度
        /// </summary> 
        public float alpha0 { get; set; } 

        /// <summary>  
        /// 低温透明度
        /// </summary> 
        public float alpha1 { get; set; } 

        /// <summary>  
        /// 融合时红外图像缩小显示倍数
        /// </summary> 
        public float irZoom {
            get
            {
                return _irZoom;
            }
            set
            {
                _irZoom = value < 0.1f ? 0.1f : value;
            }
        }
        private float _irZoom;

        /// <summary>  
        /// 融合时红外图像缩小显示倍数
        /// </summary> 
        public float irMin { get; set; }

        /// <summary>  
        /// /融合时红外图像缩小显示
        /// </summary> 
        public float irMax { get; set; }

        public IR_JSON_Fusion()
        {
            enable = false;
            visibleW = 768;
            visibleH = 1024;
            zoom = 1.0f;
            align = "center";
            offsetZ = 1.0f;
            offsetX = 0.0f;
            offsetY = 0.0f;
            alpha0 = 0.9f;
            alpha1 = 0.1f;

            irZoom = 0.8f;
            irMin = 0.8f;
            irMax = 1.0f;
        }
    }
}
