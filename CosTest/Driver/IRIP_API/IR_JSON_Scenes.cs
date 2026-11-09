using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_pathList
    {
        /// <summary>  
        /// 目录ID
        /// </summary>
        public int id { get; set; }

        /// <summary>  
        /// 目录类型
        /// </summary>
        public int type { get; set; }

        /// <summary>  
        /// 目录名称
        /// </summary>
        public string name { get; set; }

        /// <summary>  
        /// 备注
        /// </summary>
        public string rem { get; set; }
    }

    public class IR_JSON_Scenes
    {
        /// <summary>  
        /// 场景类型　0: 自定义模板, 1: 高铁版
        /// </summary>
        public int type { get; set; }

        /// <summary>  
        /// 场景名称 :SCENES/TRAIN_MODELS
        /// </summary>
        public string name { get; set; }

        /// <summary>  
        /// 场景目录
        /// </summary>
        public List<IR_JSON_pathList> list { get; set; } 

        public IR_JSON_Scenes()
        {
            name = "SCENES";
            list = new List<IR_JSON_pathList>();
        }
    }
}
