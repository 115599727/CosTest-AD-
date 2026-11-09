using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    //继承IR_JSON_Object
    /// <summary>  
    /// IR_JSON_Object入口
    /// </summary> 
    public class JSON : IR_JSON_Object { }

    /// <summary>  
    /// IR_JSON_Object入口
    /// </summary> 
    public class IR_JSON_Object
    {
        //继承相关子项类
        /// <summary>  
        /// 报警设置类
        /// </summary> 
        public class cAlert : IR_JSON_Alert { }
        /// <summary>  
        /// APP信息类
        /// </summary> 
        public class cAppInfo : IR_JSON_AppInfo { }
        /// <summary>  
        /// 红外驱动器信息类
        /// </summary> 
        public class cDeviceInfo : IR_JSON_DeviceInfo { }
        /// <summary>  
        /// 融合参数类
        /// </summary> 
        public class cFusion : IR_JSON_Fusion { }
        /// <summary>  
        /// GPS信息类
        /// </summary> 
        public class cGPSInfo : IR_JSON_GPSInfo { }
        /// <summary>  
        /// IRIP图像渲染配置信息类
        /// </summary> 
        public class cIripConfig : IR_JSON_IripConfig { }
        /// <summary>  
        /// ISO参数类
        /// </summary> 
        public class cISO : IR_JSON_ISO { }
        /// <summary>  
        /// 温度锁定信息类
        /// </summary> 
        public class cLockTemp : IR_JSON_LockTemp { }
        /// <summary>  
        /// 测量信息类
        /// </summary> 
        public class cMeasurement : IR_JSON_Measurement { }
        /// <summary>  
        /// 测距信息类
        /// </summary> 
        public class cRanging : IR_JSON_Ranging { }
        /// <summary>  
        /// 场景信息类
        /// </summary> 
        public class cScenes : IR_JSON_Scenes { }
        /// <summary>  
        /// 全局显示设置类
        /// </summary> 
        public class cShow : IR_JSON_Show { }

        /// <summary>  
        /// 红外相机设置类
        /// </summary> 
        public class cCamera
        {
            /// <summary>  
            /// 温度量程 0: 常温段 1: 宽温段
            /// </summary> 
            public int tempType
            {
                get
                {
                    return _tempType;
                }
                set
                {
                    _tempType = value < 0 ? 0 : value > 1 ? 1 : value;
                }
            }
            private int _tempType;

            /// <summary>  
            /// 打快门校正选择的时间序号
            /// </summary> 
            public int refreshIndex
            {
                get
                {
                    return _refreshIndex;
                }
                set
                {
                    _refreshIndex = value < 0 ? 0 : value;
                }
            }
            private int _refreshIndex;

            /// <summary>  
            /// 是否启用均匀度校正
            /// </summary> 
            public bool ucEnable { get; set; }

            public cCamera()
            {
                refreshIndex = 4;
            }
        }

        /// <summary>  
        /// 图像名称
        /// </summary> 
        [JsonProperty("Imager info")] //json实际key
        public string ImagerName { get; }

        /// <summary>  
        /// JSON数据版本名称
        /// </summary> 
        public string Version { get; }

        /// <summary>  
        /// JSON数据版本版本号
        /// </summary> 
        [JsonProperty("Version Code")] //json实际key
        public ushort VersionCode { get; }

        /// <summary>  
        /// 原始保存路径
        /// </summary> 
        public string path { get; set; }

        /// <summary>  
        /// 创建时间（1970-包含毫秒的时间戳）
        /// </summary> 
        public Int64 createTime { get; set; } 

        /// <summary>  
        /// 修改时间（1970-包含毫秒的时间戳）
        /// </summary> 
        public Int64 editTime { get; set; } 

        /// <summary>  
        /// 分辨率: width x height
        /// </summary> 
        public string resolution { get; set; }  

        /// <summary>  
        /// 红外图像宽度
        /// </summary> 
        public int imgW { get; set; } 

        /// <summary>  
        /// 红外图像高度
        /// </summary> 
        public int imgH { get; set; }

        /// <summary>  
        /// 红外相机设置
        /// </summary> 
        public cCamera Camera { get; set; }

        /// <summary>  
        /// APP信息
        /// </summary> 
        public cAppInfo AppInfo { get; set; } 

        /// <summary>  
        /// 红外驱动器信息
        /// </summary> 
        public cDeviceInfo DeviceInfo { get; set; } 

        /// <summary>  
        /// GPS信息
        /// </summary> 
        public cGPSInfo GPSInfo { get; set; } 

        /// <summary>  
        /// 全局显示设置
        /// </summary> 
        public cShow Show { get; set; } 

        /// <summary>  
        /// IRIP图像渲染配置信息
        /// </summary> 
        public cIripConfig IripConfig { get; set; } 

        /// <summary>  
        /// ISO参数
        /// </summary> 
        public cISO ISO { get; set; } 

        /// <summary>  
        /// 报警设置
        /// </summary> 
        public cAlert Alert { get; set; } 

        /// <summary>  
        /// 测距信息
        /// </summary> 
        public cRanging Ranging { get; set; }  

        /// <summary>  
        /// 融合参数
        /// </summary> 
        public cFusion Fusion { get; set; } 

        /// <summary>  
        /// 测量信息
        /// </summary> 
        public cMeasurement Measurement { get; set; } 

        /// <summary>  
        /// 场景信息
        /// </summary> 
        public cScenes Scenes { get; set; } 

        /// <summary>  
        /// 温度锁定信息
        /// </summary> 
        public cLockTemp LockTemp { get; set; } 

        public IR_JSON_Object()
        {
            ImagerName = "ThermViewer IR Data";
            Version = "1.0.0.7";
            VersionCode = 7;

            path = "";
            resolution = "";

            Camera = new cCamera();
            AppInfo = new cAppInfo();
            DeviceInfo = new cDeviceInfo();
            GPSInfo = new cGPSInfo();
            Show = new cShow();
            IripConfig = new cIripConfig();
            ISO = new cISO();
            Alert = new cAlert();
            Ranging = new cRanging();
            Fusion = new cFusion();
            Measurement = new cMeasurement();
            Scenes = new cScenes();
            LockTemp = new cLockTemp();
        }

    }
}
