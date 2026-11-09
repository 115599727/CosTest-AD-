using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_Measurement
    {
        /// <summary>  
        /// 温差参数设置类
        /// </summary> 
        public class Delta : IR_JSON_Delta { }

        /// <summary>  
        /// 分段校准设置类
        /// </summary> 
        public class SegmentCalib : IR_JSON_SegmentCalib { }

        /// <summary>  
        /// Tfpa校准数据类
        /// </summary> 
        public class TfpaCalib : IR_JSON_TfpaCalib { }

        /// <summary>  
        /// 显示信息类
        /// </summary> 
        public class modeShow
        {

            /// <summary>  
            /// 测量类型，测量元素选择标记
            /// [0] 是否选中数据标示测量类型的最后一个元素是否被选中 0: 未选中，1: 已选中，已选中的元素用不同颜色表示，以方便编辑等操作
            /// [1] - [4] SID测量元素显示顺序，值为IRIP_API.MD.SPOT.SID、IRIP_API.MD.LINE.SID、IRIP_API.MD.AREA.SID、IRIP_API.MD.POLY.SID
            /// 当前版本支持着四个SID，顺序可任意，不能重复，倒序显示，显示时数组最后一个测量类型的测量元素显示在最前依次往后。
            /// </summary> 
            public List<int> order
            {
                get
                {
                    return _order;
                }
                set
                {
                    if (value == null)
                    {
                        value = new List<int>() {
                            0,
                            IRIP_API.MD.SPOT.SID,
                            IRIP_API.MD.LINE.SID,
                            IRIP_API.MD.AREA.SID,
                            IRIP_API.MD.POLY.SID,
                            IRIP_API.MD.CIRC.SID
                        };
                    }
                    else
                    {
                        if (value.Count == 4)
                        {
                            value.Add(IRIP_API.MD.POLY.SID);
                            value.Add(IRIP_API.MD.CIRC.SID);
                        }
                        else if (value.Count == 5)
                        {
                            value.Add(IRIP_API.MD.CIRC.SID);
                        }
                    }

                    _order = value;
                }
            }
            private List<int> _order;

            /// <summary>  
            /// 获取显示顺序标记
            /// <para>@return order</para>
            /// </summary> 
            public List<int> getOrder()
            {
                if(order == null)
                {
                    order = new List<int>() {
                        0,
                        IRIP_API.MD.SPOT.SID,
                        IRIP_API.MD.LINE.SID,
                        IRIP_API.MD.AREA.SID,
                        IRIP_API.MD.POLY.SID,
                        IRIP_API.MD.CIRC.SID
                    };
                }
                else
                {
                    if(order.Count == 4)
                    {
                        order.Add(IRIP_API.MD.POLY.SID);
                        order.Add(IRIP_API.MD.CIRC.SID);
                    }
                    else if (order.Count == 5)
                    {
                        order.Add(IRIP_API.MD.CIRC.SID);
                    }
                }

                return order;
            }

            /// <summary>  
            /// 设置显示顺序标记 按测量类型sid设置的顺序前后以倒序排
            /// <para>@param sel 测量元素选择标记 0: 未选中，1: 已选中</para>
            /// <para>@param sid1</para>
            /// <para>@param sid2</para>
            /// <para>@param sid3</para>
            /// <para>@param sid4</para>
            /// <para>@param sid5</para>
            /// </summary> 
            public void setOrder(int sel = 0, int sid1 = IRIP_API.MD.SPOT.SID, int sid2 = IRIP_API.MD.LINE.SID, int sid3 = IRIP_API.MD.AREA.SID, int sid4 = IRIP_API.MD.POLY.SID, int sid5 = IRIP_API.MD.CIRC.SID)
            {
                sid1 = sid1 < 1 ? 1 : sid1 > 5 ? 5 : sid1;
                sid2 = sid2 < 1 ? 1 : sid2 > 5 ? 5 : sid2;
                sid3 = sid3 < 1 ? 1 : sid3 > 5 ? 5 : sid3;
                sid4 = sid4 < 1 ? 1 : sid4 > 5 ? 5 : sid4;
                sid5 = sid5 < 1 ? 1 : sid5 > 5 ? 5 : sid5;

                int len = 6;

                int[] order0 = new int[len];
                int[] order1 = { sel == 0 ? 0 : 1, sid1, sid2, sid3, sid4, sid5 };
                bool isTrue = false;
                for (int e = 1; e < len; e++)
                {
                    if (order1[e] > 0)
                    {
                        for (int i = e + 1; i < len; i++)
                        {
                            if (order1[e] == order1[i])
                            {
                                order1[i] = 0;
                                isTrue = false;
                            }
                        }
                        order0[order1[e]] = 0x01;
                    }
                }
                if (!isTrue)
                {
                    for (int e = 2; e < len; e++)
                    {
                        if (order1[e] == 0)
                        {
                            for (int i = 2; i < len; i++)
                            {
                                if (order0[i] == 0)
                                {
                                    order1[e] = i;
                                    order0[i] = 0x01;
                                }
                            }
                        }
                    }
                }

                order = new List<int>();
                order.Add(order1[0]);
                order.Add(order1[1]);
                order.Add(order1[2]);
                order.Add(order1[3]);
                order.Add(order1[4]);
                order.Add(order1[5]);
            }

            /// <summary>  
            /// 显示高低温
            /// </summary> 
            public bool hiloShow { get; set; }

            /// <summary>  
            /// 显示点测温
            /// </summary> 
            public bool spotShow { get; set; }

            /// <summary>  
            /// 显示线测温
            /// </summary> 
            public bool lineShow { get; set; }

            /// <summary>  
            /// 显示区域/矩形测温
            /// </summary> 
            public bool areaShow { get; set; }

            /// <summary>  
            /// 显示多圆、椭圆形
            /// </summary> 
            public bool circShow { get; set; }

            /// <summary>  
            /// 显示多边形测温
            /// </summary> 
            public bool polyShow { get; set; }


            public modeShow()
            {
                hiloShow = true;
            }
        }

        /// <summary>  
        /// 测量数据类
        /// </summary> 
        public class mdPlus
        {

            /// <summary>  
            /// 测得的最低温度
            /// </summary> 
            public int min { get; set; }

            /// <summary>  
            /// 测得的最高温度
            /// </summary> 
            public int max { get; set; }

            /// <summary>  
            /// 低温点坐标X
            /// </summary> 
            public int minx { get; set; }

            /// <summary>  
            /// 低温点坐标Y
            /// </summary> 
            public int miny { get; set; }

            /// <summary>  
            /// 高温点坐标X
            /// </summary> 
            public int maxx { get; set; }

            /// <summary>  
            /// 高温点坐标Y
            /// </summary> 
            public int maxy { get; set; }

            /// <summary>  
            /// 测得的平均温度 
            /// </summary> 
            public int avg { get; set; }

            /// <summary>  
            /// 检查测量点数量，cnum>=1表示测量数据有效
            /// </summary> 
            public int cnum { get; set; }

        }

        /// <summary>  
        /// 点测量元素信息类
        /// </summary> 
        public class SpotPlus
        {
            /// <summary>  
            /// 元素ID
            /// </summary> 
            public uint id { get; set; }

            /// <summary>  
            /// 起始x坐标
            /// </summary> 
            public int sx { get; set; }

            /// <summary>  
            /// 起始y坐标
            /// </summary> 
            public int sy { get; set; }

            /// <summary>  
            /// 结束x坐标
            /// </summary> 
            public int ex { get; set; }

            /// <summary>  
            /// 结束y坐标
            /// </summary> 
            public int ey { get; set; }

            /// <summary>  
            /// 是否使用全局设置（发射率和报警温度）
            /// </summary> 
            public bool global { get; set; }

            /// <summary>  
            /// 发射率
            /// </summary> 
            public float emissivity
            {
                get
                {
                    return _emissivity;
                }
                set
                {
                    _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
                }
            }
            private float _emissivity;

            /// <summary>  
            /// alert-low-temp 低温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aLT { get; set; }

            /// <summary>  
            /// alert-high-temp 高温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aHT { get; set; }

            /// <summary>  
            /// 测点温度
            /// </summary> 、
            public int temp { get; set; }

            /// <summary>  
            /// 自定义名称
            /// </summary> 
            public string name { get; set; }

            /// <summary>  
            /// 获取显示名称
            /// </summary> 
            public string getName()
            {
                if (name == null || name == "")
                {
                    //如果自定义名称为空，将以前缀＋(id+1)作为显示名称，该id在该类测温类型中具有唯一性
                    return "Sp" + (id + 1);
                }
                return name;
            }
            /// <summary>  
            /// 获取类型
            /// </summary> 
            public string getType()
            {
                return "spot";
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// </summary>
            public void set(int x, int y)
            {
                set(x, y, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// <para>@param name 名称</para>
            /// </summary>
            public void set(int x, int y, string name)
            {
                set(x, y, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// </summary>
            public void set(int x, int y, string name, bool global)
            {
                set(x, y, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// </summary>
            public void set(int x, int y, string name, bool global, float emissivity)
            {
                set(x, y, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int x, int y, string name, bool global, float emissivity, int aHT)
            {
                set(x, y, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param x 起始x坐标</para>
            /// <para>@param y 起始y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int x, int y, string name, bool global, float emissivity, int aHT, int aLT)
            {
                this.sx = x;
                this.sy = y;
                this.name = name ?? "";
                this.global = global;
                this.emissivity = emissivity;
                this.aHT = aHT;
                this.aLT = aLT;
            }

            public SpotPlus()
            {
                global = true;
                emissivity = 0.95f;
                aLT = 0;
                aHT = 5000;

                name = "";

                //md = new IR_JSON_MD_mdPlus();
            }
        }
        /// <summary>  
        /// 线测量元素信息类
        /// </summary> 
        public class LinePlus
        {
            /// <summary>  
            /// 元素ID
            /// </summary> 
            public uint id { get; set; }

            /// <summary>  
            /// 起始x坐标
            /// </summary> 
            public int sx { get; set; }

            /// <summary>  
            /// 起始y坐标
            /// </summary> 
            public int sy { get; set; }

            /// <summary>  
            /// 结束x坐标
            /// </summary> 
            public int ex { get; set; }

            /// <summary>  
            /// 结束y坐标
            /// </summary> 
            public int ey { get; set; }

            /// <summary>  
            /// 是否使用全局设置（发射率和报警温度）
            /// </summary> 
            public bool global { get; set; }

            /// <summary>  
            /// 发射率
            /// </summary> 
            public float emissivity
            {
                get
                {
                    return _emissivity;
                }
                set
                {
                    _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
                }
            }
            private float _emissivity;

            /// <summary>  
            /// alert-low-temp 低温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aLT { get; set; }

            /// <summary>  
            /// alert-high-temp 高温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aHT { get; set; }

            /// <summary>  
            /// 自定义名称
            /// </summary> 
            public string name { get; set; }

            /// <summary>  
            /// 测量所得信息
            /// </summary> 
            public mdPlus md { get; set; }

            /// <summary>  
            /// 获取显示名称
            /// </summary> 
            public string getName()
            {
                if (name == null || name == "")
                {
                    //如果自定义名称为空，将以前缀＋(id+1)作为显示名称，该id在该类测温类型中具有唯一性
                    return "Li" + (id + 1);
                }
                return name;
            }
            /// <summary>  
            /// 获取类型
            /// </summary> 
            public string getType()
            {
                return "line";
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT, int aLT)
            {
                this.sx = sx;
                this.sy = sy;
                this.ex = ex;
                this.ey = ey;
                this.name = name ?? "";
                this.global = global;
                this.emissivity = emissivity;
                this.aHT = aHT;
                this.aLT = aLT;
                this.md = null;
            }

            public LinePlus()
            {
                global = true;
                emissivity = 0.95f;
                aLT = 0;
                aHT = 5000;

                name = "";

                //md = new IR_JSON_MD_mdPlus();
                md = null;
            }
        }

        /// <summary>  
        /// 矩形测量元素信息类
        /// </summary> 
        public class AreaPlus
        {

            /// <summary>  
            /// 元素ID
            /// </summary> 
            public uint id { get; set; }

            /// <summary>  
            /// 起始x坐标
            /// </summary> 
            public int sx { get; set; }

            /// <summary>  
            /// 起始y坐标
            /// </summary> 
            public int sy { get; set; }

            /// <summary>  
            /// 结束x坐标
            /// </summary> 
            public int ex { get; set; }

            /// <summary>  
            /// 结束y坐标
            /// </summary> 
            public int ey { get; set; }

            /// <summary>  
            /// 是否使用全局设置（发射率和报警温度）
            /// </summary> 
            public bool global { get; set; }

            /// <summary>  
            /// 发射率
            /// </summary> 
            public float emissivity
            {
                get
                {
                    return _emissivity;
                }
                set
                {
                    _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
                }
            }
            private float _emissivity;

            /// <summary>  
            /// alert-low-temp 低温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aLT { get; set; }

            /// <summary>  
            /// alert-high-temp 高温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aHT { get; set; }

            /// <summary>  
            /// 自定义名称
            /// </summary> 
            public string name { get; set; }

            /// <summary>  
            /// 测量所得信息
            /// </summary> 
            public mdPlus md { get; set; }

            /// <summary>  
            /// 获取显示名称
            /// </summary> 
            public string getName()
            {
                if (name == null || name == "")
                {
                    //如果自定义名称为空，将以前缀＋(id+1)作为显示名称，该id在该类测温类型中具有唯一性
                    return "Ar" + (id + 1);
                }
                return name;
            }
            /// <summary>  
            /// 获取类型
            /// </summary> 
            public string getType()
            {
                return "area";
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT, int aLT)
            {
                this.sx = sx;
                this.sy = sy;
                this.ex = ex;
                this.ey = ey;
                this.name = name ?? "";
                this.global = global;
                this.emissivity = emissivity;
                this.aHT = aHT;
                this.aLT = aLT;
                this.md = null;
            }


            public AreaPlus()
            {
                global = true;
                emissivity = 0.95f;
                aLT = 0;
                aHT = 5000;

                name = "";

                //md = new IR_JSON_MD_mdPlus();
                md = null;
            }
        }


        /// <summary>  
        /// 圆、椭圆测量元素信息类
        /// </summary> 
        public class CircPlus
        {

            /// <summary>  
            /// 元素ID
            /// </summary> 
            public uint id { get; set; }

            /// <summary>  
            /// 起始x坐标
            /// </summary> 
            public int sx { get; set; }

            /// <summary>  
            /// 起始y坐标
            /// </summary> 
            public int sy { get; set; }

            /// <summary>  
            /// 结束x坐标
            /// </summary> 
            public int ex { get; set; }

            /// <summary>  
            /// 结束y坐标
            /// </summary> 
            public int ey { get; set; }

            /// <summary>  
            /// 是否使用全局设置（发射率和报警温度）
            /// </summary> 
            public bool global { get; set; }

            /// <summary>  
            /// 发射率
            /// </summary> 
            public float emissivity
            {
                get
                {
                    return _emissivity;
                }
                set
                {
                    _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
                }
            }
            private float _emissivity;

            /// <summary>  
            /// alert-low-temp 低温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aLT { get; set; }

            /// <summary>  
            /// alert-high-temp 高温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aHT { get; set; }

            /// <summary>  
            /// 自定义名称
            /// </summary> 
            public string name { get; set; }

            /// <summary>  
            /// 测量所得信息
            /// </summary> 
            public mdPlus md { get; set; }

            /// <summary>  
            /// 获取显示名称
            /// </summary> 
            public string getName()
            {
                if (name == null || name == "")
                {
                    //如果自定义名称为空，将以前缀＋(id+1)作为显示名称，该id在该类测温类型中具有唯一性
                    return "Ci" + (id + 1);
                }
                return name;
            }
            /// <summary>  
            /// 获取类型
            /// </summary> 
            public string getType()
            {
                return "circ";
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT)
            {
                set(sx, sy, ex, ey, name, global, emissivity, aHT, aLT);
            }

            /// <summary>
            /// 修改元素
            /// <para>@param sx 起始x坐标</para>
            /// <para>@param sy 起始y坐标</para>
            /// <para>@param ey 结束x坐标</para>
            /// <para>@param ex 结束y坐标</para>
            /// <para>@param name 名称</para>
            /// <para>@param global 是否使用全局设置</para>
            /// <para>@param emissivity 发射率</para>
            /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
            /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
            /// </summary>
            public void set(int sx, int sy, int ex, int ey, string name, bool global, float emissivity, int aHT, int aLT)
            {
                this.sx = sx;
                this.sy = sy;
                this.ex = ex;
                this.ey = ey;
                this.name = name ?? "";
                this.global = global;
                this.emissivity = emissivity;
                this.aHT = aHT;
                this.aLT = aLT;
                this.md = null;
            }


            public CircPlus()
            {
                global = true;
                emissivity = 0.95f;
                aLT = 0;
                aHT = 5000;

                name = "";

                //md = new IR_JSON_MD_mdPlus();
                md = null;
            }
        }

        /// <summary>  
        /// 节点
        /// </summary> 
        public class Point
        {
            public int x { get; set; }
            public int y { get; set; }
        };

        /// <summary>  
        /// 多边形节点数据
        /// </summary> 
        public class PointStruct
        {
            /// <summary>  
            /// 节点List，一个有效的多边形至少包含三个节点
            /// </summary> 
            public List<Point> nodes { get; set; }

            /// <summary>  
            /// 标记线段是否已闭合，如已闭合，连接第一个和最后一个作为一个多边形，温度测量函数才会正常返回测量结果
            /// </summary> 
            public bool closed { get; set; }
        };

        /// <summary>  
        /// 多边形测量元素信息类
        /// </summary> 
        public class PolyPlus
        {

            /// <summary>  
            /// 元素ID
            /// </summary> 
            public uint id { get; set; }

            /// <summary>  
            /// 起始x坐标
            /// </summary> 
            public int sx { get; set; }

            /// <summary>  
            /// 起始y坐标
            /// </summary> 
            public int sy { get; set; }

            /// <summary>  
            /// 结束x坐标
            /// </summary> 
            public int ex { get; set; }

            /// <summary>  
            /// 结束y坐标
            /// </summary> 
            public int ey { get; set; }

            /// <summary>  
            /// 是否使用全局设置（发射率和报警温度）
            /// </summary> 
            public bool global { get; set; }

            /// <summary>  
            /// 发射率
            /// </summary> 
            public float emissivity
            {
                get
                {
                    return _emissivity;
                }
                set
                {
                    _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
                }
            }
            private float _emissivity;

            /// <summary>  
            /// alert-low-temp 低温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aLT { get; set; }

            /// <summary>  
            /// alert-high-temp 高温报警阈值 实际摄氏度*100
            /// </summary> 
            public int aHT { get; set; }

            /// <summary>  
            /// 自定义名称
            /// </summary> 
            public string name { get; set; }

            /// <summary>  
            /// 多边形坐标
            /// </summary> 
            public PointStruct point {
                get
                {
                    return _point;
                }
                set
                {
                    _point = value == null ? new PointStruct() : value;
                }
            }
            private PointStruct _point;

            /// <summary>  
            /// 测量所得信息
            /// </summary> 
            public mdPlus md { get; set; }

            /// <summary>  
            /// 获取显示名称
            /// </summary> 
            public string getName()
            {
                if (name == null || name == "")
                {
                    //如果自定义名称为空，将以前缀＋(id+1)作为显示名称，该id在该类测温类型中具有唯一性
                    return "Po" + (id + 1);
                }
                return name;
            }
            /// <summary>  
            /// 获取类型
            /// </summary> 
            public string getType()
            {
                return "poly";
            }

            /// <summary>
            /// 更新坐标范围，计算多边形整体坐标范围（sx, sy, ex, ey），从IRIP库回调的测量数据会自动计算，如果之前用不到这个数据可以不计算
            /// </summary>
            public void refreshCoordinates()
            {
                sx = 0;
                sy = 0;
                ex = 0;
                ey = 0;
                if(point == null)
                {
                    return;
                }
                for (int i = 0; i < point.nodes.Count; i++)
                {
                    int x = point.nodes[i].x;
                    int y = point.nodes[i].y;
                    if (i == 0)
                    {
                        sx = ex = x;
                        sy = ey = y;
                    }
                    else
                    {
                        if (x < sx)
                        {
                            sx = x;
                        }
                        else if (x > ex)
                        {
                            ex = x;
                        }
                        if (y < sy)
                        {
                            sy = y;
                        }
                        else if (y > ey)
                        {
                            ey = y;
                        }
                    }
                }
            }


            public PolyPlus()
            {
                global = true;
                emissivity = 0.95f;
                aLT = 0;
                aHT = 5000;

                name = "";

                point = new PointStruct();

                //md = new IR_JSON_MD_mdPlus();
                md = null;
            }
        }

        /// <summary>  
        /// 全幅温度数据偏移量（兼容老版，已弃用）
        /// </summary> 
        //public int offsetTemp { get; set; } 

        /// <summary>  
        /// 发射率 0.01-1
        /// </summary> 
        public float emissivity {
            get {
                return _emissivity;
            }
            set {
                _emissivity = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
            }
        }
        private float _emissivity;

        /// <summary>  
        /// 反射温度
        /// </summary> 
        public float reflected { get; set; }

        /// <summary>  
        /// 湿度
        /// </summary> 
        public uint humidity
        {
            get
            {
                return _humidity;
            }
            set
            {
                _humidity = value > 100 ? 100 : value;
            }
        }
        public uint _humidity;

        /// <summary>  
        /// 红外窗口透射率
        /// </summary> 
        public float winTmte
        {
            get
            {
                return _winTmte;
            }
            set
            {
                _winTmte = value < 0.001f ? 0.001f : value > 1.0f ? 1.0f : value;
            }
        }
        private float _winTmte;

        /// <summary>  
        /// 红外窗口温度
        /// </summary> 
        public float winTemp { get; set; }

        /// <summary>  
        /// 截幅（已弃用，图像渲染相关已转移至IRIP设置）
        /// </summary> 
        //public float truncation { get; set; } 

        /// <summary>  
        /// 补偿温度
        /// </summary> 
        public int compensation { get; set; }

        /// <summary>  
        /// 测量距离（单位：m），如果设置距离为0.0将忽略距离修正（默认标定距离一般为1.0m）
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
        /// 温度单位 0：°C， 1：°F，2：K 
        /// </summary> 
        public uint unit {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value < 0 ? 0 : value > 2 ? 2 : value;
            }
        }
        private uint _unit;

        /// <summary>  
        /// 滤波模式 0：关闭， 1：均值， 2：增强
        /// </summary> 
        public uint filterMode { get; set; } 

        /// <summary>  
        /// 显示设置
        /// </summary> 
        public modeShow show { get; set; }

        /// <summary>  
        /// 分段校准设置
        /// </summary> 
        public SegmentCalib segmentCalib { get; set; } 

        /// <summary>  
        /// Tfpa校准数据
        /// </summary> 
        public TfpaCalib tfpaCalib { get; set; } 

        /// <summary>  
        /// 全局最低温度
        /// </summary> 
        public int min { get; set; } 

        /// <summary>  
        /// 全局最高温度
        /// </summary> 
        public int max { get; set; } 

        /// <summary>  
        /// 全局最低温度x坐标
        /// </summary> 
        public int minx { get; set; } 

        /// <summary>  
        /// 全局最低温度y坐标
        /// </summary> 
        public int miny { get; set; } 

        /// <summary>  
        /// 全局最高温度x坐标
        /// </summary> 
        public int maxx { get; set; } 

        /// <summary>  
        /// 全局最高温度y坐标
        /// </summary> 
        public int maxy { get; set; } 

        /// <summary>  
        /// 中心温度
        /// </summary> 
        public int center { get; set; }

        /// <summary>  
        /// 温差参数设置
        /// </summary> 
        public Delta delta { get; set; }

        /// <summary>  
        /// 测量模式（已弃用，由show对象多选模式取代）
        /// </summary> 
        //public string mode { get; set; } 

        /// <summary>  
        /// 全幅高低温测量数据
        /// </summary> 
        public mdPlus hilo { get; set; } 

        /// <summary>  
        /// 点测温测量数据
        /// </summary> 
        public List<SpotPlus> spot { get; set; } 

        /// <summary>  
        /// 线测温测量数据
        /// </summary> 
        public List<LinePlus> line { get; set; } 

        /// <summary>  
        /// 区域测温测量数据
        /// </summary> 
        public List<AreaPlus> area { get; set; }

        /// <summary>  
        /// 圆、椭圆测温测量数据
        /// </summary> 
        public List<CircPlus> circ { get; set; }

        /// <summary>  
        /// 多边形测温测量数据
        /// </summary> 
        public List<PolyPlus> poly { get; set; }

        private int getNewId(List<SpotPlus> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int id = -1;
                int[] ids = new int[64];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id < 64)
                    {
                        ids[list[i].id] = 1;
                    }
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] == 0)
                    {
                        id = i;
                        break;
                    }
                }
                return id;
            }
        }

        private int getNewId(List<LinePlus> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int id = -1;
                int[] ids = new int[64];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id < 64)
                    {
                        ids[list[i].id] = 1;
                    }
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] == 0)
                    {
                        id = i;
                        break;
                    }
                }
                return id;
            }
        }

        private int getNewId(List<AreaPlus> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int id = -1;
                int[] ids = new int[64];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id < 64)
                    {
                        ids[list[i].id] = 1;
                    }
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] == 0)
                    {
                        id = i;
                        break;
                    }
                }
                return id;
            }
        }

        private int getNewId(List<CircPlus> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int id = -1;
                int[] ids = new int[64];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id < 64)
                    {
                        ids[list[i].id] = 1;
                    }
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] == 0)
                    {
                        id = i;
                        break;
                    }
                }
                return id;
            }
        }

        private int getNewId(List<PolyPlus> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int id = -1;
                int[] ids = new int[64];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id < 64)
                    {
                        ids[list[i].id] = 1;
                    }
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] == 0)
                    {
                        id = i;
                        break;
                    }
                }
                return id;
            }
        }

        /// <summary>
        /// 添加一个点测温元素并分配所属ID
        /// <para>@param x 起始x坐标 对应sx</para>
        /// <para>@param y 起始y坐标 对应sy</para>
        /// <para>@param name 名称</para>
        /// <para>@param global 是否使用全局设置</para>
        /// <para>@param emissivity 发射率</para>
        /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
        /// <para>@return 返回新建元素对象，最大容量64个，如果超出，则新建失败返回null</para>
        /// </summary>
        public SpotPlus addSpot(int x, int y, string name = "", bool global = true, float emissivity = 0.95f, int aHT = 5000, int aLT = 0)
        {
            int id = getNewId(spot);
            if (id == -1)
            {
                return null;
            }
            else
            {
                SpotPlus plus = new SpotPlus();
                plus.id = (uint)id;
                plus.sx = x;
                plus.sy = y;
                plus.name = name ?? "";
                plus.global = global;
                plus.emissivity = emissivity < 0.001f ? 0.001f : emissivity > 1.0f ? 1.0f : emissivity;
                plus.aHT = aHT;
                plus.aLT = aLT;
                spot.Add(plus);
                return plus;
            }
        }

        /// <summary>
        /// 删除一个点测温元素
        /// <para>@param id 元素ID</para>
        /// </summary>
        public bool delSpot(uint id)
        {
            for (int i = 0; i < spot.Count; i++)
            {
                if (spot[i].id == id)
                {
                    spot.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个线测温元素并分配所属ID
        /// <para>@param sx 起始x坐标</para>
        /// <para>@param sy 起始y坐标</para>
        /// <para>@param ey 结束x坐标</para>
        /// <para>@param ex 结束y坐标</para>
        /// <para>@param name 名称</para>
        /// <para>@param global 是否使用全局设置</para>
        /// <para>@param emissivity 发射率</para>
        /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
        /// <para>@return 返回新建元素对象，最大容量64个，如果超出，则新建失败返回null</para>
        /// </summary>
        public LinePlus addLine(int sx, int sy, int ex, int ey, string name = "", bool global = true, float emissivity = 0.95f, int aHT = 5000, int aLT = 0)
        {
            int id = getNewId(line);
            if (id == -1)
            {
                return null;
            }
            else
            {
                LinePlus plus = new LinePlus();
                plus.id = (uint)id;
                plus.sx = sx;
                plus.sy = sy;
                plus.ex = ex;
                plus.ey = ey;
                plus.name = name ?? "";
                plus.global = global;
                plus.emissivity = emissivity < 0.001f ? 0.001f : emissivity > 1.0f ? 1.0f : emissivity;
                plus.aHT = aHT;
                plus.aLT = aLT;
                line.Add(plus);
                return plus;
            }
        }

        /// <summary>
        /// 删除一个线测温元素
        /// <para>@param id 元素ID</para>
        /// </summary>
        public bool delLine(uint id)
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].id == id)
                {
                    line.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个矩形区域测温元素并分配所属ID
        /// <para>@param sx 起始x坐标</para>
        /// <para>@param sy 起始y坐标</para>
        /// <para>@param ey 结束x坐标</para>
        /// <para>@param ex 结束y坐标</para>
        /// <para>@param name 名称</para>
        /// <para>@param global 是否使用全局设置</para>
        /// <para>@param emissivity 发射率</para>
        /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
        /// <para>@return 返回新建元素对象，最大容量64个，如果超出，则新建失败返回null</para>
        /// </summary>
        public AreaPlus addArea(int sx, int sy, int ex, int ey, string name = "", bool global = true, float emissivity = 0.95f, int aHT = 5000, int aLT = 0)
        {
            int id = getNewId(area);
            if (id == -1)
            {
                return null;
            }
            else
            {
                AreaPlus plus = new AreaPlus();
                plus.id = (uint)id;
                plus.sx = sx;
                plus.sy = sy;
                plus.ex = ex;
                plus.ey = ey;
                plus.name = name ?? "";
                plus.global = global;
                plus.emissivity = emissivity < 0.001f ? 0.001f : emissivity > 1.0f ? 1.0f : emissivity;
                plus.aHT = aHT;
                plus.aLT = aLT;
                area.Add(plus);
                return plus;
            }
        }

        /// <summary>
        /// 删除一个矩形区域测温元素
        /// <para>@param id 元素ID</para>
        /// </summary>
        public bool delArea(uint id)
        {
            for (int i = 0; i < area.Count; i++)
            {
                if (area[i].id == id)
                {
                    area.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个圆或椭圆形区域测温元素并分配所属ID
        /// <para>@param sx 起始x坐标</para>
        /// <para>@param sy 起始y坐标</para>
        /// <para>@param ey 结束x坐标</para>
        /// <para>@param ex 结束y坐标</para>
        /// <para>@param name 名称</para>
        /// <para>@param global 是否使用全局设置</para>
        /// <para>@param emissivity 发射率</para>
        /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
        /// <para>@return 返回新建元素对象，最大容量64个，如果超出，则新建失败返回null</para>
        /// </summary>
        public CircPlus addCirc(int sx, int sy, int ex, int ey, string name = "", bool global = true, float emissivity = 0.95f, int aHT = 5000, int aLT = 0)
        {
            int id = getNewId(circ);
            if (id == -1)
            {
                return null;
            }
            else
            {
                CircPlus plus = new CircPlus();
                plus.id = (uint)id;
                plus.sx = sx;
                plus.sy = sy;
                plus.ex = ex;
                plus.ey = ey;
                plus.name = name ?? "";
                plus.global = global;
                plus.emissivity = emissivity < 0.001f ? 0.001f : emissivity > 1.0f ? 1.0f : emissivity;
                plus.aHT = aHT;
                plus.aLT = aLT;
                circ.Add(plus);
                return plus;
            }
        }

        /// <summary>
        /// 删除一个矩形区域测温元素
        /// <para>@param id 元素ID</para>
        /// </summary>
        public bool delCirc(uint id)
        {
            for (int i = 0; i < circ.Count; i++)
            {
                if (circ[i].id == id)
                {
                    circ.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个多边形测温元素并分配所属ID
        /// <para>@param name 名称</para>
        /// <para>@param global 是否使用全局设置</para>
        /// <para>@param emissivity 发射率</para>
        /// <para>@param aHT 高温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param aLT 低温报警温度(摄氏度实际温度*100)</para>
        /// <para>@param point 多变型节点数据</para>
        /// <para>@return 返回新建元素对象，最大容量64个，如果超出，则新建失败返回null</para>
        /// </summary>
        public PolyPlus addPoly(string name = "", bool global = true, float emissivity = 0.95f, int aHT = 5000, int aLT = 0, PointStruct point = null)
        {
            int id = getNewId(poly);
            if (id == -1)
            {
                return null;
            }
            else
            {
                PolyPlus plus = new PolyPlus();
                plus.id = (uint)id;
                plus.name = name ?? "";
                plus.global = global;
                plus.emissivity = emissivity < 0.001f ? 0.001f : emissivity > 1.0f ? 1.0f : emissivity;
                plus.aHT = aHT;
                plus.aLT = aLT;

                if(point != null && point.nodes != null)
                {
                    if(plus.point == null)
                    {
                        plus.point = new PointStruct();
                    }

                    int count = 0;
                    for (int i = 0; i < point.nodes.Count; i++)
                    {
                        Point p = new Point();
                        p.x = point.nodes[i].x;
                        p.y = point.nodes[i].y;
                        plus.point.nodes.Add(p);
                        count++;
                    }
                    if (count > 0)
                    {
                        plus.point.closed = point.closed;
                    }
                    else
                    {
                        plus.point.closed = false;
                    }
                }

                poly.Add(plus);
                return plus;
            }
        }

        /// <summary>
        /// 删除一个多边形测温元素
        /// <para>@param id 元素ID</para>
        /// </summary>
        public bool delPoly(uint id)
        {
            for (int i = 0; i < poly.Count; i++)
            {
                if (poly[i].id == id)
                {
                    poly.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public IR_JSON_Measurement()
        {
            //offsetTemp = 27767;
            emissivity = 0.95f;
            reflected = 25.0f;
            humidity = 45;
            winTmte = 1.0f;
            winTemp = 20.0f;
            //truncation = 0.25f;
            distance = 1.0f;
            filterMode = 0;

            show = new modeShow();
            segmentCalib = null;// new SegmentCalib();
            tfpaCalib = null;
            delta = null;// new Delta();

            //mode = "NONE";

            min = 0; //最低温度
            max = 0; //最高温度
            center = 0; //中心温度
            minx = -1;
            miny = -1;
            maxx = -1;
            maxy = -1;

            hilo = null;// new mdPlus();
            spot = new List<SpotPlus>();
            line = new List<LinePlus>();
            area = new List<AreaPlus>();
            circ = new List<CircPlus>();
            poly = new List<PolyPlus>();
        }
    }


}
