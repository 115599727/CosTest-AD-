using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstStream;

namespace CosTest
{
    class ThermMeasUtil
    {
        #region 参数区

        /// <summary>
        /// THERM_AREA
        /// </summary>
        public static Dictionary<string, StThermAreaDef> THERM_AREA_DICT { get; set; }

        #endregion


        #region 读取和保存

        /// <summary>
        /// 解析程序配置文件
        /// </summary>
        private static VstIniParser ThermAreaIniParser;

        /// <summary>
        /// 是否成功加载配置文件
        /// </summary>
        public static bool LOADED { get; set; }

        /// <summary>
        /// 读取配置数据
        /// </summary>
        public static void ReadConfig()
        {
            try
            {
                THERM_AREA_DICT = new Dictionary<string, StThermAreaDef>();
                ThermAreaIniParser = new VstIniParser(StationUtil.GetThermMeasFilePath());
                string[] areaDefNames = ThermAreaIniParser.EnumSectionNames();

                for (int i = 0; i < areaDefNames.Length; i++)
                {
                    StThermAreaDef areadef = new StThermAreaDef();

                    // Setting List
                    string[] settingNames = ThermAreaIniParser.EnumSection(areaDefNames[i]);
                    areadef.AreaList = new List<StThermAreaParam>();
                    List<int> areaIds = new List<int>();


                    foreach (string settingName in settingNames)
                    {
                        // PARAM -- AREA
                        if (settingName.StartsWith("AREA#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(5, settingName.IndexOf('_') - 5));

                            int index = areaIds.IndexOf(id);
                            if (index < 0)
                            {
                                areadef.AreaList.Add(new StThermAreaParam());
                                areaIds.Add(id);
                                index = areaIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.Name = ThermAreaIniParser.GetSetting(areaDefNames[i], settingName);
                                areadef.AreaList[index] = tmp;
                            }
                            else if (settingName.EndsWith("SX"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.SX = Convert.ToInt32(ThermAreaIniParser.GetSetting(areaDefNames[i], settingName));
                                areadef.AreaList[index] = tmp;
                            }
                            else if (settingName.EndsWith("SY"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.SY = Convert.ToInt32(ThermAreaIniParser.GetSetting(areaDefNames[i], settingName));
                                areadef.AreaList[index] = tmp;
                            }
                            else if (settingName.EndsWith("EX"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.EX = Convert.ToInt32(ThermAreaIniParser.GetSetting(areaDefNames[i], settingName));
                                areadef.AreaList[index] = tmp;
                            }
                            else if (settingName.EndsWith("EY"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.EY = Convert.ToInt32(ThermAreaIniParser.GetSetting(areaDefNames[i], settingName));
                                areadef.AreaList[index] = tmp;
                            }
                            else if (settingName.EndsWith("EMISSIVITY"))
                            {
                                StThermAreaParam tmp = areadef.AreaList[index];
                                tmp.Emissivity = Convert.ToDouble(ThermAreaIniParser.GetSetting(areaDefNames[i], settingName));
                                areadef.AreaList[index] = tmp;
                            }
                        }
                    }

                    // 添加
                    THERM_AREA_DICT.Add(areaDefNames[i], areadef);
                }

                LOADED = true;
            }
            catch (Exception ex)
            {
                LOADED = false;
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("读取热像测量信息失败！");
            }
        }

        /// <summary>
        /// 保存配置数据
        /// </summary>
        public static void SaveConfig()
        {
            // 删除
            string[] routineNames = ThermAreaIniParser.EnumSectionNames();
            foreach (string rtName in routineNames)
            {
                ThermAreaIniParser.DeleteSetting(rtName);
            }

            // 更新
            foreach (var rt in THERM_AREA_DICT)
            {
                string areDefName = rt.Key;
                StThermAreaDef areDef = rt.Value;

                // AREA#
                for (int i = 0; i < areDef.AreaList.Count; i++)
                {
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_NAME", areDef.AreaList[i].Name);
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_SX", areDef.AreaList[i].SX.ToString());
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_SY", areDef.AreaList[i].SY.ToString());
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_EX", areDef.AreaList[i].EX.ToString());
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_EY", areDef.AreaList[i].EY.ToString());
                    ThermAreaIniParser.AddSetting(areDefName, "AREA#" + (i + 1).ToString() + "_EMISSIVITY", areDef.AreaList[i].Emissivity.ToString());
                }
            }

            //保存到文件
            try
            {
                ThermAreaIniParser.SaveSettings();
            }
            catch
            {
                ErrLog.WriteLine("更新热像测量信息文件失败！请确认检查文件路径是否有效");
                WarnWindow.ShowMsg("更新热像测量信息文件失败！请确认文件路径是否有效");
            }
        }

        public static List<string> GetAreaNameList()
        {
            List<string> areaNameList = new List<string>();
            // Search
            foreach (string rtName in THERM_AREA_DICT.Keys)
                areaNameList.Add(rtName);
            // Sort
            areaNameList.Sort();
            // return
            return areaNameList;
        }

        #endregion
    }

    /// <summary>
    /// 热测量区域定义
    /// </summary>
    struct StThermAreaDef
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal string Name;

        /// <summary>
        /// 名称
        /// </summary>
        internal List<StThermAreaParam> AreaList;
    }

    /// <summary>
    /// 热测量区域定义
    /// </summary>
    struct StThermAreaParam
    {
        /// <summary>
        /// 名称
        /// </summary>
        internal string Name;
        /// <summary>
        /// SX
        /// </summary>
        internal int SX;
        /// <summary>
        /// SY
        /// </summary>
        internal int SY;
        /// <summary>
        /// EX
        /// </summary>
        internal int EX;
        /// <summary>
        /// EY
        /// </summary>
        internal int EY;
        /// <summary>
        /// 发射率
        /// </summary>
        internal double Emissivity;
    }
}
