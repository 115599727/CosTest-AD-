using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstStream;

namespace CosTest
{
    class TestScriptUtil
    {
        #region 参数区

        /// <summary>
        /// ROUTINE集合
        /// </summary>
        public static Dictionary<string, StRoutine> ROUTINE_DICT { get; set; }

        #endregion


        #region 读取和保存

        /// <summary>
        /// 解析程序配置文件
        /// </summary>
        private static VstIniParser RoutineIniParser;

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
                ROUTINE_DICT = new Dictionary<string, StRoutine>();
                RoutineIniParser = new VstIniParser(StationUtil.GetRoutineFilePath());
                string[] routineNames = RoutineIniParser.EnumSectionNames();

                for (int i = 0; i < routineNames.Length; i++)
                {
                    StRoutine routine = new StRoutine();

                    // Routine Name
                    routine.Name = routineNames[i];

                    // Liv Scan Condition
                    routine.LivScanCfg = new StLivScanCfg();
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_PRELOADING_VOLTAGE") == "")
                        routine.LivScanCfg.PreloadingVoltage = 0.0;
                    else
                        routine.LivScanCfg.PreloadingVoltage = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_PRELOADING_VOLTAGE"));
                    routine.LivScanCfg.Start = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_START"));
                    routine.LivScanCfg.Stop = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_STOP"));
                    routine.LivScanCfg.Step = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_STEP"));
                    routine.LivScanCfg.Delay = Convert.ToInt32(RoutineIniParser.GetSetting(routineNames[i], "LIV_DELAY"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_IS_SHOW_VF") == "")
                        routine.LivScanCfg.IsShowVF = false;
                    else
                        routine.LivScanCfg.IsShowVF = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], "LIV_IS_SHOW_VF"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_IS_SHOW_EFF") == "")
                        routine.LivScanCfg.IsShowEFF = false;
                    else
                        routine.LivScanCfg.IsShowEFF = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], "LIV_IS_SHOW_EFF"));

                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_PRELOADING_CURRENT") == "")
                        routine.LivScanCfg.PreloadingCurrent = 0.0;
                    else
                        routine.LivScanCfg.PreloadingCurrent = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_PRELOADING_CURRENT"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_SHORT_VOLT_THRESH") == "")
                        routine.LivScanCfg.ShortVoltThresh = 0.0;
                    else
                        routine.LivScanCfg.ShortVoltThresh = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_SHORT_VOLT_THRESH"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_OPEN_CURRENT_THRESH") == "")
                        routine.LivScanCfg.OpenCurrentThresh = 0.0;
                    else
                        routine.LivScanCfg.OpenCurrentThresh = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_OPEN_CURRENT_THRESH"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "LIV_CURRENT_TOL") == "")
                        routine.LivScanCfg.CurrentTol = 0.0;
                    else
                        routine.LivScanCfg.CurrentTol = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "LIV_CURRENT_TOL"));

                    // Spectrum Scan Condition
                    routine.SpScanCfg.WavelengthBase = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "SP_WAVELENGTH_BASE"));
                    routine.SpScanCfg.WavelengthScopePositive = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "SP_WAVELENGTH_SCOPE_P"));
                    routine.SpScanCfg.WavelengthScopeNegative = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "SP_WAVELENGTH_SCOPE_N"));
                    routine.SpScanCfg.IsAutoIntegrationTime = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], "SP_IS_AUTO_INTEGRATION_TIME"));
                    routine.SpScanCfg.IntegrationTime = Convert.ToInt32(RoutineIniParser.GetSetting(routineNames[i], "SP_INTEGRATION_TIME"));
                    routine.SpScanCfg.BoxcarWidth = Convert.ToInt32(RoutineIniParser.GetSetting(routineNames[i], "SP_BOXCAR_WIDTH"));
                    routine.SpScanCfg.NumberOfScansToAverage = Convert.ToInt32(RoutineIniParser.GetSetting(routineNames[i], "SP_NUMBER_OF_SCANS_TO_AVERAGE"));
                    routine.SpScanCfg.PibTotalHalfSpan = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "SP_PIB_TOTAL_HALF_SPAN"));
                    routine.SpScanCfg.PibCenterHalfSpan = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], "SP_PIB_CENTER_HALF_SPAN"));
                    if (RoutineIniParser.GetSetting(routineNames[i], "SP_IS_GAUSS_FIT") == "")
                        routine.SpScanCfg.IsGaussFit = false;
                    else
                        routine.SpScanCfg.IsGaussFit = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], "SP_IS_GAUSS_FIT"));

                    // ThermMeasName
                    routine.ThermMeasName = RoutineIniParser.GetSetting(routineNames[i], "THERM_MEAS_NAME");

                    // Setting List
                    string[] settingNames = RoutineIniParser.EnumSection(routineNames[i]);

                    routine.LivTestParams = new List<StCosParam>();
                    List<int> pivIds = new List<int>();
                    routine.SpTestParams = new List<StCosParam>();
                    List<int> spIds = new List<int>();
                    routine.NaTestParams = new List<StCosParam>();
                    List<int> naIds = new List<int>();
                    routine.DeltaParams = new List<StCosParam>();
                    List<int> deltaIds = new List<int>();
                    routine.BeamTestParams = new List<StCosParam>();
                    List<int> bmIds = new List<int>();
                    routine.ThermTestParams = new List<StCosParam>();
                    List<int> thmIds = new List<int>();
                    routine.SpDrvs = new List<StCosParam>();
                    List<int> spdrvIds = new List<int>();
                    routine.TempDrvs = new List<double>();
                    List<int> tempdrvIds = new List<int>();
                    routine.NaDrvs = new List<StCosParam>();
                    List<int> nadrvIds = new List<int>();
                    routine.BeamCamDrvs = new List<StCosParam>();
                    List<int> bmdrvIds = new List<int>();
                    routine.ThermCamDrvs = new List<StCosParam>();
                    List<int> thmdrvIds = new List<int>();

                    foreach (string settingName in settingNames)
                    {
                        // Drive Temperature
                        if (settingName.StartsWith("TEMPDRV#"))
                        {
                            routine.TempDrvs.Add(Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName)));
                        }
                        // Drive Condition for SP Scan
                        else if (settingName.StartsWith("SPDRV#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(6, settingName.IndexOf('_') - 6));
                            int index = spdrvIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.SpDrvs.Add(new StCosParam());
                                spdrvIds.Add(id);
                                index = spdrvIds.Count - 1;
                            }

                            if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.SpDrvs[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpDrvs[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.SpDrvs[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpDrvs[index] = tmp;
                            }
                        }
                        // Drive Condition for NA Measurement
                        else if (settingName.StartsWith("NADRV#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(6, settingName.IndexOf('_') - 6));
                            int index = nadrvIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.NaDrvs.Add(new StCosParam());
                                nadrvIds.Add(id);
                                index = nadrvIds.Count - 1;
                            }

                            if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.NaDrvs[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaDrvs[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.NaDrvs[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaDrvs[index] = tmp;
                            }
                        }
                        // Drive Condition for Beam Camera Scan
                        else if (settingName.StartsWith("BMDRV#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(6, settingName.IndexOf('_') - 6));
                            int index = bmdrvIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.BeamCamDrvs.Add(new StCosParam());
                                bmdrvIds.Add(id);
                                index = bmdrvIds.Count - 1;
                            }

                            if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.BeamCamDrvs[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.BeamCamDrvs[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.BeamCamDrvs[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.BeamCamDrvs[index] = tmp;
                            }
                        }
                        // Drive Condition for Thermal Camera Scan
                        else if (settingName.StartsWith("TMDRV#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(6, settingName.IndexOf('_') - 6));
                            int index = thmdrvIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.ThermCamDrvs.Add(new StCosParam());
                                thmdrvIds.Add(id);
                                index = thmdrvIds.Count - 1;
                            }

                            if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.ThermCamDrvs[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermCamDrvs[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.ThermCamDrvs[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermCamDrvs[index] = tmp;
                            }
                        }
                        // PARAM -- PIV
                        else if (settingName.StartsWith("PIV#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(4, settingName.IndexOf('_') - 4));
                            int index = pivIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.LivTestParams.Add(new StCosParam());
                                pivIds.Add(id);
                                index = pivIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                tmp.Name = RoutineIniParser.GetSetting(routineNames[i], settingName);
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("TEMP"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                tmp.DriveTemp = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("REPORT"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                tmp.IsReport = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("UCL"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "ITH")
                                    tmp.ParamType = "Ith(A)";
                                else if (segs[1] == "SLOPE")
                                    tmp.ParamType = "Slope(W/A)";
                                else if (segs[1] == "RES")
                                    tmp.ParamType = "Res(Ω)";
                                else if (segs[1] == "POWER")
                                    tmp.ParamType = "Po(W)";
                                else if (segs[1] == "CURRENT")
                                    tmp.ParamType = "If(A)";
                                else if (segs[1] == "VOLTAGE")
                                    tmp.ParamType = "Vf(V)";
                                else if (segs[1] == "EFFICIENCY")
                                    tmp.ParamType = "Eff(%)";
                                tmp.ParamUCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("LCL"))
                            {
                                StCosParam tmp = routine.LivTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "ITH")
                                    tmp.ParamType = "Ith(A)";
                                else if (segs[1] == "SLOPE")
                                    tmp.ParamType = "Slope(W/A)";
                                else if (segs[1] == "RES")
                                    tmp.ParamType = "Res(Ω)";
                                else if (segs[1] == "POWER")
                                    tmp.ParamType = "Po(W)";
                                else if (segs[1] == "CURRENT")
                                    tmp.ParamType = "If(A)";
                                else if (segs[1] == "VOLTAGE")
                                    tmp.ParamType = "Vf(V)";
                                else if (segs[1] == "EFFICIENCY")
                                    tmp.ParamType = "Eff(%)";
                                tmp.ParamLCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.LivTestParams[index] = tmp;
                            }
                        }
                        // PARAM -- SP
                        else if (settingName.StartsWith("SP#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(3, settingName.IndexOf('_') - 3));
                            int index = spIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.SpTestParams.Add(new StCosParam());
                                spIds.Add(id);
                                index = spIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                tmp.Name = RoutineIniParser.GetSetting(routineNames[i], settingName);
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("TEMP"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                tmp.DriveTemp = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("REPORT"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                tmp.IsReport = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("UCL"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "PEAK")
                                    tmp.ParamType = "Peak(nm)";
                                else if (segs[1] == "CENTER")
                                    tmp.ParamType = "Center(nm)";
                                else if (segs[1] == "CENTROID")
                                    tmp.ParamType = "Centroid(nm)";
                                else if (segs[1] == "FWHM")
                                    tmp.ParamType = "FWHM(nm)";
                                else if (segs[1] == "SNR")
                                    tmp.ParamType = "SNR(dB)";
                                else if (segs[1] == "PIB")
                                    tmp.ParamType = "PIB(%)";
                                tmp.ParamUCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("LCL"))
                            {
                                StCosParam tmp = routine.SpTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "PEAK")
                                    tmp.ParamType = "Peak(nm)";
                                else if (segs[1] == "CENTER")
                                    tmp.ParamType = "Center(nm)";
                                else if (segs[1] == "CENTROID")
                                    tmp.ParamType = "Centroid(nm)";
                                else if (segs[1] == "FWHM")
                                    tmp.ParamType = "FWHM(nm)";
                                else if (segs[1] == "SNR")
                                    tmp.ParamType = "SNR(dB)";
                                else if (segs[1] == "PIB")
                                    tmp.ParamType = "PIB(%)";
                                tmp.ParamLCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.SpTestParams[index] = tmp;
                            }
                        }
                        // PARAM -- NA
                        else if (settingName.StartsWith("NA#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(3, settingName.IndexOf('_') - 3));
                            int index = naIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.NaTestParams.Add(new StCosParam());
                                naIds.Add(id);
                                index = naIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                tmp.Name = RoutineIniParser.GetSetting(routineNames[i], settingName);
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("TEMP"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                tmp.DriveTemp = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("REPORT"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                tmp.IsReport = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("UCL"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "NA")
                                    tmp.ParamType = "N.A.";
                                tmp.ParamUCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("LCL"))
                            {
                                StCosParam tmp = routine.NaTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "NA")
                                    tmp.ParamType = "N.A.";
                                tmp.ParamLCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.NaTestParams[index] = tmp;
                            }
                        }
                        // PARAM -- THM
                        else if (settingName.StartsWith("THM#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(4, settingName.IndexOf('_') - 4));
                            int index = thmIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.ThermTestParams.Add(new StCosParam());
                                thmIds.Add(id);
                                index = thmIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                tmp.Name = RoutineIniParser.GetSetting(routineNames[i], settingName);
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("CURRENT"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                tmp.DriveType = "If(A)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("POWER"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                tmp.DriveType = "Po(W)";
                                tmp.DriveValue = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("TEMP"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                tmp.DriveTemp = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("REPORT"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                tmp.IsReport = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("UCL"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                tmp.ParamType = segs[1];
                                tmp.ParamUCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("LCL"))
                            {
                                StCosParam tmp = routine.ThermTestParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                tmp.ParamType = segs[1];
                                tmp.ParamLCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.ThermTestParams[index] = tmp;
                            }
                        }
                        // PARAM -- DELTA
                        else if (settingName.StartsWith("DELTA#"))
                        {
                            int id = Convert.ToInt32(settingName.Substring(6, settingName.IndexOf('_') - 6));
                            int index = deltaIds.IndexOf(id);
                            if (index < 0)
                            {
                                routine.DeltaParams.Add(new StCosParam());
                                deltaIds.Add(id);
                                index = deltaIds.Count - 1;
                            }

                            if (settingName.EndsWith("NAME"))
                            {
                                StCosParam tmp = routine.DeltaParams[index];
                                tmp.Name = RoutineIniParser.GetSetting(routineNames[i], settingName);
                                routine.DeltaParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("REPORT"))
                            {
                                StCosParam tmp = routine.DeltaParams[index];
                                tmp.IsReport = Convert.ToBoolean(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.DeltaParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("UCL"))
                            {
                                StCosParam tmp = routine.DeltaParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "REAL")
                                    tmp.ParamType = "";
                                else if (segs[1] == "PERCENT")
                                    tmp.ParamType = "%";
                                tmp.ParamUCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.DeltaParams[index] = tmp;
                            }
                            else if (settingName.EndsWith("LCL"))
                            {
                                StCosParam tmp = routine.DeltaParams[index];
                                string[] segs = settingName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                if (segs[1] == "REAL")
                                    tmp.ParamType = "";
                                else if (segs[1] == "PERCENT")
                                    tmp.ParamType = "%";
                                tmp.ParamLCL = Convert.ToDouble(RoutineIniParser.GetSetting(routineNames[i], settingName));
                                routine.DeltaParams[index] = tmp;
                            }
                        }
                    }

                    // 检查Routine参数
                    for (int j = routine.LivTestParams.Count - 1; j >= 0; j--)
                    {
                        // ITH和Slope和Res免检
                        if (routine.LivTestParams[j].ParamType == "Ith(A)" || routine.LivTestParams[j].ParamType == "Slope(W/A)" || routine.LivTestParams[j].ParamType == "Res(Ω)")
                            continue;
                        // DriveType和ParamType不能为空
                        if (routine.LivTestParams[j].DriveType == null || routine.LivTestParams[j].ParamType == null)
                            routine.LivTestParams.RemoveAt(j);
                    }
                    for (int j = routine.SpTestParams.Count - 1; j >= 0; j--)
                    {
                        // DriveType和ParamType不能为空
                        if (routine.SpTestParams[j].DriveType == null || routine.SpTestParams[j].ParamType == null)
                            routine.SpTestParams.RemoveAt(j);
                    }
                    for (int j = routine.NaTestParams.Count - 1; j >= 0; j--)
                    {
                        // DriveType和ParamType不能为空
                        if (routine.NaTestParams[j].DriveType == null || routine.NaTestParams[j].ParamType == null)
                            routine.NaTestParams.RemoveAt(j);
                    }
                    for (int j = routine.ThermTestParams.Count - 1; j >= 0; j--)
                    {
                        // DriveType和ParamType不能为空
                        if (routine.ThermTestParams[j].DriveType == null || routine.ThermTestParams[j].ParamType == null)
                            routine.ThermTestParams.RemoveAt(j);
                    }
                    for (int j = routine.BeamTestParams.Count - 1; j >= 0; j--)
                    {
                        // DriveType和ParamType不能为空
                        if (routine.BeamTestParams[j].DriveType == null || routine.BeamTestParams[j].ParamType == null)
                            routine.BeamTestParams.RemoveAt(j);
                    }
                    for (int j = routine.DeltaParams.Count - 1; j >= 0; j--)
                    {
                        // ParamType不能为空
                        if (routine.DeltaParams[j].ParamType == null)
                            routine.DeltaParams.RemoveAt(j);
                    }

                    // 添加
                    ROUTINE_DICT.Add(routineNames[i], routine);
                }

                LOADED = true;
            }
            catch (Exception ex)
            {
                LOADED = false;
                ErrLog.WriteLine(ex.ToString());
                WarnWindow.ShowMsg("读取配置信息失败！");
            }
        }

        /// <summary>
        /// 保存配置数据
        /// </summary>
        public static void SaveConfig()
        {
            // 删除
            string[] routineNames = RoutineIniParser.EnumSectionNames();
            foreach (string rtName in routineNames)
            {
                RoutineIniParser.DeleteSetting(rtName);
            }

            // 更新
            foreach (var rt in ROUTINE_DICT)
            {
                string rtName = rt.Key;
                StRoutine routine = rt.Value;

                // Liv Scan Condition
                RoutineIniParser.AddSetting(rtName, "LIV_PRELOADING_VOLTAGE", routine.LivScanCfg.PreloadingVoltage.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_START", routine.LivScanCfg.Start.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_STOP", routine.LivScanCfg.Stop.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_STEP", routine.LivScanCfg.Step.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_DELAY", routine.LivScanCfg.Delay.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_IS_SHOW_VF", routine.LivScanCfg.IsShowVF.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_IS_SHOW_EFF", routine.LivScanCfg.IsShowEFF.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_PRELOADING_CURRENT", routine.LivScanCfg.PreloadingCurrent.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_SHORT_VOLT_THRESH", routine.LivScanCfg.ShortVoltThresh.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_OPEN_CURRENT_THRESH", routine.LivScanCfg.OpenCurrentThresh.ToString());
                RoutineIniParser.AddSetting(rtName, "LIV_CURRENT_TOL", routine.LivScanCfg.CurrentTol.ToString());

                // Spectrum Scan Condation
                RoutineIniParser.AddSetting(rtName, "SP_WAVELENGTH_BASE", routine.SpScanCfg.WavelengthBase.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_WAVELENGTH_SCOPE_P", routine.SpScanCfg.WavelengthScopePositive.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_WAVELENGTH_SCOPE_N", routine.SpScanCfg.WavelengthScopeNegative.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_IS_AUTO_INTEGRATION_TIME", routine.SpScanCfg.IsAutoIntegrationTime.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_INTEGRATION_TIME", routine.SpScanCfg.IntegrationTime.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_BOXCAR_WIDTH", routine.SpScanCfg.BoxcarWidth.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_NUMBER_OF_SCANS_TO_AVERAGE", routine.SpScanCfg.NumberOfScansToAverage.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_PIB_TOTAL_HALF_SPAN", routine.SpScanCfg.PibTotalHalfSpan.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_PIB_CENTER_HALF_SPAN", routine.SpScanCfg.PibCenterHalfSpan.ToString());
                RoutineIniParser.AddSetting(rtName, "SP_IS_GAUSS_FIT", routine.SpScanCfg.IsGaussFit.ToString());

                // ThermMeasName
                RoutineIniParser.AddSetting(rtName, "THERM_MEAS_NAME", routine.ThermMeasName);

                // TEMPDRV#
                for (int i = 0; i < routine.TempDrvs.Count; i++)
                {
                    RoutineIniParser.AddSetting(rtName, "TEMPDRV#" + (i + 1).ToString() + "_TEMP", routine.TempDrvs[i].ToString());
                }

                // SPDRV#
                for (int i = 0; i < routine.SpDrvs.Count; i++)
                {
                    if (routine.SpDrvs[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "SPDRV#" + (i + 1).ToString() + "_POWER", routine.SpDrvs[i].DriveValue.ToString());
                    else if (routine.SpDrvs[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "SPDRV#" + (i + 1).ToString() + "_CURRENT", routine.SpDrvs[i].DriveValue.ToString());
                }

                // NADRV#
                for (int i = 0; i < routine.NaDrvs.Count; i++)
                {
                    if (routine.NaDrvs[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "NADRV#" + (i + 1).ToString() + "_POWER", routine.NaDrvs[i].DriveValue.ToString());
                    else if (routine.NaDrvs[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "NADRV#" + (i + 1).ToString() + "_CURRENT", routine.NaDrvs[i].DriveValue.ToString());
                }

                // BMDRV#
                for (int i = 0; i < routine.BeamCamDrvs.Count; i++)
                {
                    if (routine.BeamCamDrvs[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "BMDRV#" + (i + 1).ToString() + "_POWER", routine.BeamCamDrvs[i].DriveValue.ToString());
                    else if (routine.BeamCamDrvs[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "BMDRV#" + (i + 1).ToString() + "_CURRENT", routine.BeamCamDrvs[i].DriveValue.ToString());
                }

                // TMDRV#
                for (int i = 0; i < routine.ThermCamDrvs.Count; i++)
                {
                    if (routine.ThermCamDrvs[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "TMDRV#" + (i + 1).ToString() + "_POWER", routine.ThermCamDrvs[i].DriveValue.ToString());
                    else if (routine.ThermCamDrvs[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "TMDRV#" + (i + 1).ToString() + "_CURRENT", routine.ThermCamDrvs[i].DriveValue.ToString());
                }

                // PIV#
                for (int i = 0; i < routine.LivTestParams.Count; i++)
                {
                    // NAME
                    RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_NAME", routine.LivTestParams[i].Name);
                    
                    // Drive Value
                    if (routine.LivTestParams[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_POWER", routine.LivTestParams[i].DriveValue.ToString());
                    else if (routine.LivTestParams[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_CURRENT", routine.LivTestParams[i].DriveValue.ToString());
                    
                    // Drive Temperature
                    RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_TEMP", routine.LivTestParams[i].DriveTemp.ToString());

                    // Report
                    RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_REPORT", routine.LivTestParams[i].IsReport.ToString());

                    // Param UCL & LCL
                    if (routine.LivTestParams[i].ParamType == "Ith(A)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_ITH_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_ITH_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "Slope(W/A)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_SLOPE_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_SLOPE_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "Res(Ω)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_RES_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_RES_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "Po(W)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_POWER_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_POWER_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "If(A)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_CURRENT_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1 ).ToString() + "_CURRENT_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "Vf(V)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_VOLTAGE_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_VOLTAGE_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.LivTestParams[i].ParamType == "Eff(%)")
                    {
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_EFFICIENCY_UCL", routine.LivTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "PIV#" + (i + 1).ToString() + "_EFFICIENCY_LCL", routine.LivTestParams[i].ParamLCL.ToString());
                    }
                }



                // SP#
                for (int i = 0; i < routine.SpTestParams.Count; i++)
                {
                    // NAME
                    RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_NAME", routine.SpTestParams[i].Name);
                    
                    // Drive Value
                    if (routine.SpTestParams[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_POWER", routine.SpTestParams[i].DriveValue.ToString());
                    else if (routine.SpTestParams[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_CURRENT", routine.SpTestParams[i].DriveValue.ToString());
                    
                    // Drive Temperature
                    RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_TEMP", routine.SpTestParams[i].DriveTemp.ToString());

                    // Report
                    RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_REPORT", routine.SpTestParams[i].IsReport.ToString());

                    // Param UCL & LCL
                    if (routine.SpTestParams[i].ParamType == "Peak(nm)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_PEAK_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_PEAK_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.SpTestParams[i].ParamType == "Center(nm)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_CENTER_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_CENTER_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.SpTestParams[i].ParamType == "Centroid(nm)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_CENTROID_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_CENTROID_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.SpTestParams[i].ParamType == "FWHM(nm)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_FWHM_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_FWHM_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.SpTestParams[i].ParamType == "SNR(dB)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_SNR_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_SNR_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                    else if (routine.SpTestParams[i].ParamType == "PIB(%)")
                    {
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_PIB_UCL", routine.SpTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "SP#" + (i + 1).ToString() + "_PIB_LCL", routine.SpTestParams[i].ParamLCL.ToString());
                    }
                }

                // NA#
                for (int i = 0; i < routine.NaTestParams.Count; i++)
                {
                    // NAME
                    RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_NAME", routine.NaTestParams[i].Name);

                    // Drive Value
                    if (routine.NaTestParams[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_POWER", routine.NaTestParams[i].DriveValue.ToString());
                    else if (routine.NaTestParams[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_CURRENT", routine.NaTestParams[i].DriveValue.ToString());

                    // Drive Temperature
                    RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_TEMP", routine.NaTestParams[i].DriveTemp.ToString());

                    // Report
                    RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_REPORT", routine.NaTestParams[i].IsReport.ToString());

                    // Param UCL & LCL
                    if (routine.NaTestParams[i].ParamType == "N.A.")
                    {
                        RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_NA_UCL", routine.NaTestParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "NA#" + (i + 1).ToString() + "_NA_LCL", routine.NaTestParams[i].ParamLCL.ToString());
                    }
                }

                // THM#
                for (int i = 0; i < routine.ThermTestParams.Count; i++)
                {
                    // NAME
                    RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_NAME", routine.ThermTestParams[i].Name);

                    // Drive Value
                    if (routine.ThermTestParams[i].DriveType == "Po(W)")
                        RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_POWER", routine.ThermTestParams[i].DriveValue.ToString());
                    else if (routine.ThermTestParams[i].DriveType == "If(A)")
                        RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_CURRENT", routine.ThermTestParams[i].DriveValue.ToString());

                    // Drive Temperature
                    RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_TEMP", routine.ThermTestParams[i].DriveTemp.ToString());

                    // Report
                    RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_REPORT", routine.ThermTestParams[i].IsReport.ToString());

                    // Param UCL & LCL
                    RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_" + routine.ThermTestParams[i].ParamType + "_UCL", routine.ThermTestParams[i].ParamUCL.ToString());
                    RoutineIniParser.AddSetting(rtName, "THM#" + (i + 1).ToString() + "_" + routine.ThermTestParams[i].ParamType + "_LCL", routine.ThermTestParams[i].ParamLCL.ToString());
                }

                // DELTA#
                for (int i = 0; i < routine.DeltaParams.Count; i++)
                {
                    // NAME
                    RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_NAME", routine.DeltaParams[i].Name);

                    // Report
                    RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_REPORT", routine.DeltaParams[i].IsReport.ToString());

                    // Param UCL & LCL
                    if (routine.DeltaParams[i].ParamType == "" || routine.DeltaParams[i].ParamType == null)
                    {
                        RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_REAL_UCL", routine.DeltaParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_REAL_LCL", routine.DeltaParams[i].ParamLCL.ToString());
                    }
                    else if (routine.DeltaParams[i].ParamType == "%")
                    {
                        RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_PERCENT_UCL", routine.DeltaParams[i].ParamUCL.ToString());
                        RoutineIniParser.AddSetting(rtName, "DELTA#" + (i + 1).ToString() + "_PERCENT_LCL", routine.DeltaParams[i].ParamLCL.ToString());
                    }
                }
            }

            //保存到文件
            try
            {
                RoutineIniParser.SaveSettings();
            }
            catch
            {
                ErrLog.WriteLine("更新机台信息文件失败！请确认检查配置文件路径是否有效");
                WarnWindow.ShowMsg("更新机台信息文件失败！请确认检查配置文件路径是否有效");
            }
        }

        public static List<string> GetRoutineNameList()
        {
            List<string> routineNameList = new List<string>();
            // Search
            foreach (string rtName in ROUTINE_DICT.Keys)
                routineNameList.Add(rtName);
            // Sort
            routineNameList.Sort();
            // return
            return routineNameList;
        }

        //public static List<string> GetRoutineNameList(bool withNA = false)
        //{
        //    List<string> routineNameList = new List<string>();
        //    // Search
        //    foreach (string rtName in ROUTINE_DICT.Keys)
        //        routineNameList.Add(rtName);
        //    // Sort
        //    routineNameList.Sort();
        //    if (withNA)
        //        routineNameList.Insert(0, "N/A");//空白记录

        //    return routineNameList;
        //}

        #endregion
    }
}
