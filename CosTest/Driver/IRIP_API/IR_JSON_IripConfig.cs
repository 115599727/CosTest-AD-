using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_IripConfig
    {
        /// <summary>  
        /// 高动态温度输出灰度图像压缩模式, hpfMode取值范围0: Agc, 1: Lut;
        /// </summary> 
        public ushort hpfMode { get; set; } 

        /// <summary>  
        /// 动态曝光控制，单幅输出模式应关闭，视频模式可开启

        /// </summary> 
        public bool isDynamicExposure { get; set; }

        /// <summary>  
        /// 夜视模式
        /// </summary> 
        public bool isNightVision { get; set; } 

        /// <summary>  
        /// 夜视模式强度 hpfAlpha取值范围0.01f - 0.05f
        /// </summary> 
        public float hpfAlpha { get; set; } 

        /// <summary>  
        /// 截幅，取值范围0.00f - 0.99f
        /// </summary> 
        public float ignoreRatio { get; set; } 

        /// <summary>  
        /// 高温自动阈值, 非夜视模式锁定有效
        /// </summary> 
        public bool isAutoTmax { get; set; }

        /// <summary>  
        /// 低温自动阈值, 非夜视模式锁定有效
        /// </summary> 
        public bool isAutoTmin { get; set; } 

        /// <summary>  
        /// 高温阈值
        /// </summary> 
        public int Tmax { get; set; } 

        /// <summary>  
        /// 低温阈值
        /// </summary> 
        public int Tmin { get; set; } 

        /// <summary>  
        /// 增益限制
        /// </summary> 
        public uint gainLimit { get; set; } 

        /// <summary>  
        /// 增益限制值，取值范围0.0 - 3.75;
        /// </summary> 
        public float gainValue { get; set; } 

        /// <summary>  
        /// 输出上下颠倒的图像
        /// </summary> 
        public bool isUpsidedown { get; set; } 

        /// <summary>  
        /// 锐化
        /// </summary> 
        public bool isSharpness { get; set; } 

        /// <summary>  
        /// 开启DDE数字增强
        /// </summary> 
        public uint DDEType { get; set; } 

        /// <summary>  
        /// 开启图像降噪
        /// </summary> 
        public bool isDenoise { get; set; } 

        public IR_JSON_IripConfig()
        {
            hpfMode = 1;
            isDynamicExposure = true;
            isNightVision = false;
            hpfAlpha = 0.05f;
            ignoreRatio = 0.25f;
            isAutoTmax = true;
            isAutoTmin = true;
            Tmax = 10000;
            Tmin = 1000;
            gainLimit = 1;
            gainValue = 0.0f;
            isUpsidedown = false;
            isSharpness = true;
            DDEType = 0;
            isDenoise = false;
        }
    }
}
