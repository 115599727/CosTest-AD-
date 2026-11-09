using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using OpenCvSharp;

namespace CosTest
{
    class TestResult
    {
        /// <summary>
        /// TestDatatFile
        /// </summary>
        private static string TestDatatFile;
        /// <summary>
        /// TestSummaryFile
        /// </summary>
        private static string TestSummaryFile;
        /// <summary>
        /// TestReportFolder
        /// </summary>
        internal static string TestReportFolder { get; set; }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestDataList"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        internal static bool Write(string batchNumber, string serialNumber, string partNumber, string testStage, StRoutine rt, StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            #region 检查或创建文件夹
            // 工位缩写
            string stageAbbr = (testStage == TestSystemUtil.POST_TEST) ? TestSystemUtil.POST_TEST_ABBR : TestSystemUtil.PRE_TEST_ABBR;

            // 检查文件夹是否存在
            string resultParentFolder = StationUtil.GetOutFolder();
            if (Directory.Exists(resultParentFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(resultParentFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + resultParentFolder + "\r\n" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + resultParentFolder));
                    return false;
                }
            }

            // 检查批次文件夹是否存在
            string batchNoFolder = resultParentFolder + @"\" + batchNumber;
            if (Directory.Exists(batchNoFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(batchNoFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + batchNoFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + batchNoFolder));
                    return false;
                }
            }

            // 检查工序文件夹是否存在
            string testStageFolder = batchNoFolder + @"\" + testStage;
            if (Directory.Exists(testStageFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(testStageFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + testStageFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + testStageFolder));
                    return false;
                }
            }

            // 检查Data文件夹是否存在
            string testDataFolder = testStageFolder + @"\Data";
            if (Directory.Exists(testDataFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(testDataFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + testStageFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + testStageFolder));
                    return false;
                }
            }

            // 检查Report文件夹是否存在
            string reportFolder = testStageFolder + @"\Report";
            if (Directory.Exists(reportFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(reportFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + reportFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + reportFolder));
                    return false;
                }
            }

            // 检查Summary文件夹是否存在
            string summaryFolder = testStageFolder + @"\Summary";
            if (Directory.Exists(summaryFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(summaryFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + summaryFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + summaryFolder));
                    return false;
                }
            }

            // 检查History文件夹是否存在
            string historyFolder = testStageFolder + @"\History";
            if (Directory.Exists(historyFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(historyFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + historyFolder + @"\" + ex.ToString());
                    if (testWarn != null)
                        testWarn(null, new TestWarnEventArgs("无法打开或创建输出文件夹 " + historyFolder));
                    return false;
                }
            }

            // 更新TestReportFolder
            TestReportFolder = reportFolder;
            #endregion

            #region 读取历史数据
            // 读取本批次产品的测试数据
            List<StCosTestData> cosTestDataList;
            Read(batchNumber, testStage, out cosTestDataList);
            #endregion

            #region 移动历史文件
            // 移动历史文件
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string srcDataFile = testDataFolder + @"\" + serialNumber + "_" + stageAbbr + @".csv";
                string dstDataFile = historyFolder + @"\" + serialNumber + "_" + stageAbbr + @"_" + dt + @".csv";
                string srcReportBmp = reportFolder + @"\" + serialNumber + "_" + stageAbbr + @"_Report_" + cosTestData.SerialNumber + @".bmp";
                string dstReportBmp = historyFolder + @"\" + serialNumber + "_" + stageAbbr + @"_Report_" + cosTestData.SerialNumber + @"_" + dt + @".bmp";
                string srcReportXps = reportFolder + @"\" + serialNumber + "_" + stageAbbr + @"_Report_" + cosTestData.SerialNumber + @".xps";
                string dstReportXps = historyFolder + @"\" + serialNumber + "_" + stageAbbr + @"_Report_" + cosTestData.SerialNumber + @"_" + dt + @".xps";
                if (File.Exists(srcDataFile))
                    File.Move(srcDataFile, dstDataFile);
                if (File.Exists(srcReportBmp))
                    File.Move(srcReportBmp, dstReportBmp);
                if (File.Exists(srcReportXps))
                    File.Move(srcReportXps, dstReportXps);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("移动文件时发送错误 " + "\r\n" + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("移动文件时发送错误 "));
                return false;
            }
            #endregion

            #region 输出结果文件

            cosTestData.BatchNumber = batchNumber;
            cosTestData.SerialNumber = serialNumber;
            cosTestData.PartNumber = partNumber;
            cosTestData.TestStage = testStage;
            cosTestData.RoutineName = rt.Name;

            // 更新到数据列表
            bool isExist = false;
            for (int i= 0; i < cosTestDataList.Count;i++)
            {
                if(cosTestDataList[i].SerialNumber == serialNumber)
                {
                    isExist = true;
                    cosTestDataList[i] = cosTestData;
                    break;
                }
            }
            if (!isExist)
                cosTestDataList.Add(cosTestData);

            // 输出文件
            TestDatatFile = testDataFolder + @"\" + serialNumber + "_" + stageAbbr + @".csv";
            TestSummaryFile = summaryFolder + @"\" + batchNumber + "_" + stageAbbr + @"_Summary.csv";
            if (WriteDataFile(TestDatatFile, batchNumber, serialNumber, partNumber, testStage, rt, cosTestData, testWarn) == false)
                return false;
            if(WriteSummaryFile(TestSummaryFile, testStage, rt, cosTestDataList, testWarn) == false)
                return false;
            if (WriteBeamImage(testDataFolder, batchNumber, serialNumber, partNumber, testStage, rt, cosTestData, testWarn) == false)
                return false;
            if (WriteThermalImage(testDataFolder, batchNumber, serialNumber, partNumber, testStage, rt, cosTestData, testWarn) == false)
                return false;

            #endregion



            return true;
        }


        /// <summary>
        /// 写DataFile数据
        /// </summary>
        /// <param name="dataFile"></param>
        /// <param name="batchNumber"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="jigNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        private static bool WriteDataFile(string dataFile, string batchNumber, string serialNumber, string partNumber, string testStage, StRoutine rt, StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            // File
            FileStream fsDataFile;
            StreamWriter swDataFile;

            // 创建Data文件
            try
            {
                fsDataFile = new FileStream(dataFile, FileMode.Create);
                swDataFile = new StreamWriter(fsDataFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("无法创建文件 " + dataFile + "\r\n" + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("无法创建文件 " + dataFile + "\r\n" + ex.ToString()));
                return false;
            }

            // 输出Result数据
            try
            {
                // 基本测试信息
                swDataFile.WriteLine("批次号,序列号,产品型号,工序,测试程序");
                swDataFile.WriteLine(batchNumber + "," + serialNumber + "," + partNumber + "," + testStage + "," + rt.Name);
                swDataFile.WriteLine("机台名称,机台编号");
                swDataFile.WriteLine(StationUtil.RackName + "," + StationUtil.RackID);
                swDataFile.WriteLine("测试用户,测试时间,测试结果");
                swDataFile.WriteLine(cosTestData.TestUserName + "," + cosTestData.TestDateTime + "," + cosTestData.RESULT);
                swDataFile.WriteLine();

                // 具体参数
                swDataFile.WriteLine("参数名,参数下限,参数上限,测试值,测试结果");
                foreach (StCosParam param in cosTestData.LivTestData)
                {
                    swDataFile.WriteLine("\"" + param.Name + "\"," +
                        param.ParamLCL.ToString() + "," +
                        param.ParamUCL.ToString() + "," +
                        param.TestValue.ToString() + "," +
                        param.TestResult);
                }
                foreach (StCosParam param in cosTestData.SpTestData)
                {
                    swDataFile.WriteLine("\"" + param.Name + "\"," +
                        param.ParamLCL.ToString() + "," +
                        param.ParamUCL.ToString() + "," +
                        param.TestValue.ToString() + "," +
                        param.TestResult);
                }
                foreach (StCosParam param in cosTestData.NaTestData)
                {
                    swDataFile.WriteLine("\"" + param.Name + "\"," +
                        param.ParamLCL.ToString() + "," +
                        param.ParamUCL.ToString() + "," +
                        param.TestValue.ToString() + "," +
                        param.TestResult);
                }
                foreach (StCosParam param in cosTestData.BmTestData)
                {
                    swDataFile.WriteLine("\"" + param.Name + "\"," +
                        param.ParamLCL.ToString() + "," +
                        param.ParamUCL.ToString() + "," +
                        param.TestValue.ToString() + "," +
                        param.TestResult);
                }
                foreach (StCosParam param in cosTestData.ThmTestData)
                {
                    swDataFile.WriteLine("\"" + param.Name + "\"," +
                        param.ParamLCL.ToString() + "," +
                        param.ParamUCL.ToString() + "," +
                        param.TestValue.ToString() + "," +
                        param.TestResult);
                }
                if (testStage == TestSystemUtil.POST_TEST)
                {
                    foreach (StCosParam param in cosTestData.DeltaTestData)
                    {
                        swDataFile.WriteLine("\"" + "DELTA-" + param.Name + "\"," +
                            param.ParamLCL.ToString() + "," +
                            param.ParamUCL.ToString() + "," +
                            param.TestValue.ToString() + "," +
                            param.TestResult);
                    }
                }
                swDataFile.WriteLine();

                // COS原始数据
                swDataFile.WriteLine();
                swDataFile.WriteLine();
                swDataFile.WriteLine("<测试原始数据>");
                // 不同温度下的LIV数据
                for (int i = 0; i < cosTestData.LivRawData.Count; i++)
                {
                    double temperature = cosTestData.TestTemps[i];
                    swDataFile.WriteLine("\"" + "<LIV数据@" + temperature + "C>" + "\"");
                    swDataFile.WriteLine("序号,电流(A),电压(V),功率(W)");
                    for (int j = 0; j < cosTestData.LivRawData[i].Count; j++)
                    {
                        swDataFile.WriteLine((j + 1).ToString() + "," +
                            cosTestData.LivRawData[i][j].LDI.ToString() + "," +
                            cosTestData.LivRawData[i][j].LDV.ToString() + "," +
                            cosTestData.LivRawData[i][j].PDI.ToString());
                    }
                }
                // 不同驱动电流和温度下的光谱数据
                for (int i = 0; i < cosTestData.SpRawData.Count; i++)
                {
                    double temperature = cosTestData.TestTemps[i];
                    foreach (StOcSpData ocSpData in cosTestData.SpRawData[i])
                    {
                        int idx = ocSpData.DRV.LastIndexOf('@') + 1;
                        string drv = ocSpData.DRV.Substring(idx, ocSpData.DRV.Length - idx).Trim();
                        swDataFile.WriteLine("\"" + "<光谱数据@" + drv + "," + temperature + "C>" + "\"");
                        swDataFile.WriteLine("序号,波长(nm),幅值");
                        for (int j = 0; j < ocSpData.Wavelength.Length; j++)
                        {
                            swDataFile.WriteLine((j + 1).ToString() + "," +
                                         ocSpData.Wavelength[j].ToString() + "," +
                                         ocSpData.Spectrum[j].ToString());
                        }
                    }
                }
                // 空白行           
                swDataFile.WriteLine();

                // 关闭文件
                swDataFile.Close();
                fsDataFile.Close();
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("输出数据时发生异常"));
                return false;
            }

            return true;
        }


        /// <summary>
        /// 写光斑图像
        /// </summary>
        /// <param name="dataFolder"></param>
        /// <param name="batchNumber"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        private static bool WriteBeamImage(string dataFolder, string batchNumber, string serialNumber, string partNumber, string testStage, StRoutine rt, StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            try
            {
               // ErrLog.WriteLine("SCAN BEAM5");

                if (cosTestData.BmRawData != null)
                {
                    //ErrLog.WriteLine("SCAN BEAM16");
                    // 工位缩写
                    string stageAbbr = (testStage == TestSystemUtil.POST_TEST) ? TestSystemUtil.POST_TEST_ABBR : TestSystemUtil.PRE_TEST_ABBR;
                    // 输出图像
                    for (int i=0; i< cosTestData.BmRawData.Count;i++)
                    {
                        for(int j=0; j< cosTestData.BmRawData[i].Count; j++)
                        {
                            if (cosTestData.BmRawData[i][j].BeamImage == null || cosTestData.BmRawData[i][j].BeamImage.Width == 0)
                                continue;
                            string fileName = dataFolder + @"\" + batchNumber + "_" + serialNumber + "_" + stageAbbr + "_BEAM_" + cosTestData.BmRawData[i][j].DRV + ".jpg";
                            Cv2.ImWrite(fileName, cosTestData.BmRawData[i][j].BeamImage);
                        }
                    }
                }
               // ErrLog.WriteLine("SCAN BEAM7");
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出光斑图像时发生异常 " + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("输出光斑图像时发生异常"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 写热成像图像
        /// </summary>
        /// <param name="dataFolder"></param>
        /// <param name="batchNumber"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        private static bool WriteThermalImage(string dataFolder, string batchNumber, string serialNumber, string partNumber, string testStage, StRoutine rt, StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            try
            {
                if (cosTestData.ThmRawData != null)
                {
                    // 工位缩写
                    string stageAbbr = (testStage == TestSystemUtil.POST_TEST) ? TestSystemUtil.POST_TEST_ABBR : TestSystemUtil.PRE_TEST_ABBR;
                    // 输出图像
                    for (int i = 0; i < cosTestData.ThmRawData.Count; i++)
                    {
                        for (int j = 0; j < cosTestData.ThmRawData[i].Count; j++)
                        {
                            if (cosTestData.ThmRawData[i][j].ThermalImage == null || cosTestData.ThmRawData[i][j].ThermalImage.Width == 0)
                                continue;
                            string fileName = dataFolder + @"\" + batchNumber + "_" + serialNumber + "_" + stageAbbr + "_THERMAL_" + cosTestData.ThmRawData[i][j].DRV + ".bmp";
                            cosTestData.ThmRawData[i][j].ThermalImage.Save(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出热成像图像时发生异常 " + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("输出热成像图像时发生异常"));
                return false;
            }

            return true;
        }


        /// <summary>
        /// 写Summary数据
        /// </summary>
        /// <param name="summaryFile"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        private static bool WriteSummaryFile(string summaryFile, string testStage, StRoutine rt, List<StCosTestData> cosTestDataList, TestWarnHandler testWarn)
        {
            // File
            FileStream fsSummaryFile;
            StreamWriter swSummaryFile;

            // 创建Summary文件
            try
            {
                fsSummaryFile = new FileStream(summaryFile, FileMode.Create);
                swSummaryFile = new StreamWriter(fsSummaryFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("无法创建文件 " + summaryFile + "\r\n" + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("无法创建文件 " + summaryFile + "\r\n" + ex.ToString()));
                return false;
            }

            // 输出Summary数据
            try
            {
                string strLine = "批次号,序列号,产品型号,工序,测试程序,测试用户,测试时间,机台名称,机台编号";
                foreach (StCosParam param in rt.LivTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                foreach (StCosParam param in rt.SpTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                foreach (StCosParam param in rt.NaTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                foreach (StCosParam param in rt.ThermTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                if (testStage == TestSystemUtil.POST_TEST)
                {
                    foreach (StCosParam param in rt.DeltaParams)
                    {
                        //string unit = "";
                        //if (param.ParamType != null)
                        //{
                        //    if (param.ParamType.Contains("("))
                        //        unit = param.ParamType.Substring(param.ParamType.IndexOf("("), param.ParamType.Length - param.ParamType.IndexOf("("));
                        //    else if (param.ParamType != "")
                        //        unit = "(" + param.ParamType + ")";
                        //}
                        strLine += ("," + "\"" + "DELTA-" + param.Name + "\"");
                    }
                }
                strLine += (",NG参数,测试结果");
                swSummaryFile.WriteLine(strLine);

                // 输出结果数据
                foreach(StCosTestData cosTestData in cosTestDataList)
                {
                    // COS结果
                    strLine = cosTestData.BatchNumber;
                    strLine += "," + cosTestData.SerialNumber;
                    strLine += "," + cosTestData.PartNumber;
                    strLine += "," + cosTestData.TestStage;
                    strLine += "," + cosTestData.RoutineName;
                    strLine += "," + cosTestData.TestUserName;
                    strLine += "," + cosTestData.TestDateTime;
                    strLine += "," + StationUtil.RackName;
                    strLine += "," + StationUtil.RackID;

                    string ngCode = "";

                    foreach (StCosParam param in rt.LivTestParams)
                    {
                        string paramTestData = "";
                        foreach (StCosParam param_data in cosTestData.LivTestData)
                        {
                            if (param.Name == param_data.Name)
                            {
                                paramTestData = param_data.TestValue.ToString();
                                if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                    ngCode += param.Name + " ";
                                break;
                            }
                        }
                        strLine += ("," + paramTestData);
                    }

                    foreach (StCosParam param in rt.SpTestParams)
                    {
                        string paramTestData = "";
                        if (cosTestData.SpTestData.Count > 0)
                        {
                            foreach (StCosParam param_data in cosTestData.SpTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // 某些情况下，所有的参数会汇总到LivTestData里面
                            foreach (StCosParam param_data in cosTestData.LivTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        strLine += ("," + paramTestData);
                    }

                    foreach (StCosParam param in rt.NaTestParams)
                    {
                        string paramTestData = "";
                        if (cosTestData.NaTestData.Count > 0)
                        {
                            foreach (StCosParam param_data in cosTestData.NaTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (StCosParam param_data in cosTestData.LivTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        strLine += ("," + paramTestData);
                    }

                    foreach (StCosParam param in rt.ThermTestParams)
                    {
                        string paramTestData = "";
                        if (cosTestData.ThmTestData.Count > 0)
                        {
                            foreach (StCosParam param_data in cosTestData.ThmTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (StCosParam param_data in cosTestData.LivTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        strLine += ("," + paramTestData);
                    }

                    if (testStage == TestSystemUtil.POST_TEST)
                    {
                        foreach (StCosParam param in rt.DeltaParams)
                        {
                            string paramTestData = "";
                            if (cosTestData.DeltaTestData.Count > 0)
                            {
                                foreach (StCosParam param_data in cosTestData.DeltaTestData)
                                {
                                    if (param.Name == param_data.Name)
                                    {
                                        paramTestData = param_data.TestValue.ToString();
                                        if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                            ngCode += param.Name + " ";
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                foreach (StCosParam param_data in cosTestData.LivTestData)
                                {
                                    if (param.Name == param_data.Name)
                                    {
                                        paramTestData = param_data.TestValue.ToString();
                                        if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                            ngCode += param.Name + " ";
                                        break;
                                    }
                                }
                            }
                            strLine += ("," + paramTestData);
                        }
                    }

                    // NG参数
                    if (ngCode == "")
                        strLine += ",OK";
                    else
                        strLine += ",\"" + ngCode.TrimEnd() + "\"";

                    // 测试结果
                    strLine += ("," + cosTestData.RESULT);

                    // 输出
                    swSummaryFile.WriteLine(strLine);
                }

                // 关闭文件
                swSummaryFile.Close();
                fsSummaryFile.Close();
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("输出数据时发生异常"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 写Summary数据
        /// </summary>
        /// <param name="summaryFile"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="rt"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        /// <returns></returns>
        private static bool WriteSummaryFile(string summaryFile, string batchNumber, string serialNumber, string partNumber, string testStage, StRoutine rt, StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            // File
            FileStream fsSummaryFile;
            StreamWriter swSummaryFile;

            // 创建Summary文件
            try
            {
                fsSummaryFile = new FileStream(summaryFile, FileMode.Create);
                swSummaryFile = new StreamWriter(fsSummaryFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("无法创建文件 " + summaryFile + "\r\n" + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("无法创建文件 " + summaryFile + "\r\n" + ex.ToString()));
                return false;
            }

            // 输出Summary数据
            try
            {
                string strLine = "批次号,序列号,产品型号,工序,测试程序,测试用户,测试时间,机台名称,机台编号";
                foreach (StCosParam param in rt.LivTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                foreach (StCosParam param in rt.SpTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                foreach (StCosParam param in rt.NaTestParams)
                {
                    strLine += ("," + "\"" + param.Name + "\"");
                }
                if (testStage == TestSystemUtil.POST_TEST)
                {
                    foreach (StCosParam param in rt.DeltaParams)
                    {
                        //string unit = "";
                        //if (param.ParamType != null)
                        //{
                        //    if (param.ParamType.Contains("("))
                        //        unit = param.ParamType.Substring(param.ParamType.IndexOf("("), param.ParamType.Length - param.ParamType.IndexOf("("));
                        //    else if (param.ParamType != "")
                        //        unit = "(" + param.ParamType + ")";
                        //}
                        strLine += ("," + "\"" + "DELTA-" + param.Name + "\"");
                    }
                }
                strLine += (",NG参数,测试结果");
                swSummaryFile.WriteLine(strLine);

                // COS结果
                strLine = batchNumber;
                strLine += "," + serialNumber;
                strLine += "," + partNumber;
                strLine += "," + testStage;
                strLine += "," + rt.Name;
                strLine += "," + cosTestData.TestUserName;
                strLine += "," + cosTestData.TestDateTime;
                strLine += "," + StationUtil.RackName;
                strLine += "," + StationUtil.RackID;

                string ngCode = "";

                foreach (StCosParam param in rt.LivTestParams)
                {
                    string paramTestData = "";
                    foreach (StCosParam param_data in cosTestData.LivTestData)
                    {
                        if (param.Name == param_data.Name)
                        {
                            paramTestData = param_data.TestValue.ToString();
                            if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                ngCode += param.Name + " ";
                            break;
                        }
                    }
                    strLine += ("," + paramTestData);
                }

                foreach (StCosParam param in rt.SpTestParams)
                {
                    string paramTestData = "";
                    if (cosTestData.SpTestData.Count > 0)
                    {
                        foreach (StCosParam param_data in cosTestData.SpTestData)
                        {
                            if (param.Name == param_data.Name)
                            {
                                paramTestData = param_data.TestValue.ToString();
                                if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                    ngCode += param.Name + " ";
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (StCosParam param_data in cosTestData.LivTestData)
                        {
                            if (param.Name == param_data.Name)
                            {
                                paramTestData = param_data.TestValue.ToString();
                                if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                    ngCode += param.Name + " ";
                                break;
                            }
                        }
                    }
                    strLine += ("," + paramTestData);
                }

                foreach (StCosParam param in rt.NaTestParams)
                {
                    string paramTestData = "";
                    if (cosTestData.NaTestData.Count > 0)
                    {
                        foreach (StCosParam param_data in cosTestData.NaTestData)
                        {
                            if (param.Name == param_data.Name)
                            {
                                paramTestData = param_data.TestValue.ToString();
                                if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                    ngCode += param.Name + " ";
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (StCosParam param_data in cosTestData.LivTestData)
                        {
                            if (param.Name == param_data.Name)
                            {
                                paramTestData = param_data.TestValue.ToString();
                                if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                    ngCode += param.Name + " ";
                                break;
                            }
                        }
                    }
                    strLine += ("," + paramTestData);
                }

                if (testStage == TestSystemUtil.POST_TEST)
                {
                    foreach (StCosParam param in rt.DeltaParams)
                    {
                        string paramTestData = "";
                        if (cosTestData.DeltaTestData.Count > 0)
                        {
                            foreach (StCosParam param_data in cosTestData.DeltaTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (StCosParam param_data in cosTestData.LivTestData)
                            {
                                if (param.Name == param_data.Name)
                                {
                                    paramTestData = param_data.TestValue.ToString();
                                    if (param_data.TestValue > param.ParamUCL || param_data.TestValue < param.ParamLCL)
                                        ngCode += param.Name + " ";
                                    break;
                                }
                            }
                        }
                        strLine += ("," + paramTestData);
                    }
                }

                // NG参数
                if (ngCode == "")
                    strLine += ",OK";
                else
                    strLine += ",\"" + ngCode.TrimEnd() + "\"";

                // 测试结果
                strLine += ("," + cosTestData.RESULT);

                // 输出
                swSummaryFile.WriteLine(strLine);

                // 关闭文件
                swSummaryFile.Close();
                fsSummaryFile.Close();
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("输出数据时发生异常"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取指定BatchNo对应的新后缀
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        private static int GetNewSuffix(string resultFolder, string testStage, string batchNo)
        {
            //检查文件夹是否存在
            if (Directory.Exists(resultFolder) == false)
            {
                return 0;
            }

            //提取文件夹内的文件名，并进行对比
            string[] files = Directory.GetFiles(resultFolder, "*.csv");

            int suffix = -1; // -1表示该SN没有测试记录
            string stage = (testStage == TestSystemUtil.POST_TEST) ? TestSystemUtil.POST_TEST_ABBR : TestSystemUtil.PRE_TEST_ABBR;
            foreach (string file in files)
            {
                string fileName = file.Substring(resultFolder.Length + 1);
                //分割文件名 A001.txt 或者 A001_1.txt
                string[] strFileNameSegs = fileName.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (strFileNameSegs.Length < 3)
                    continue;
                if (strFileNameSegs[1].ToUpper() != stage)
                    continue;
                //检查是否存在相同的序列号
                if (strFileNameSegs[0].ToUpper() == batchNo.ToUpper())
                {
                    if (strFileNameSegs.Length == 4)
                    {
                        //计算已存在的后缀
                        int tmpSuffix;
                        try
                        {
                            tmpSuffix = Convert.ToInt32(strFileNameSegs[2]);
                            suffix = (tmpSuffix > suffix) ? tmpSuffix : suffix;
                        }
                        catch
                        {
                            
                        }
                    }
                    else
                    {
                        suffix = suffix < 0 ? 0 : suffix;
                    }
                }
            }

            return (suffix + 1);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <param name="testStage"></param>
        /// <param name="cosTestDataList"></param>
        private static void Read(string batchNumber, string testStage, out List<StCosTestData> cosTestDataList)
        {
            cosTestDataList = new List<StCosTestData>();

            // 检查文件是否存在
            string resultFolder = StationUtil.GetOutFolder();
            string testDataFolder = "";
            if (testStage == TestSystemUtil.POST_TEST)
                testDataFolder = resultFolder + @"\" + batchNumber + @"\" + TestSystemUtil.POST_TEST + @"\Data\";
            else
                testDataFolder = resultFolder + @"\" + batchNumber + @"\" + TestSystemUtil.PRE_TEST + @"\Data\";
            if (!Directory.Exists(testDataFolder))
                return;

            // 遍历所有的文件
            string[] testDataFiles = Directory.GetFiles(testDataFolder);
            if (testDataFiles == null || testDataFiles.Length == 0)
                return;

            foreach(string testDataFile in testDataFiles)
            {
                // 读取数据
                string batchNo,serialNo, partNo, stage, rtName;
                StCosTestData cosTestData;
                Read(testDataFile, out batchNo, out serialNo, out partNo, out stage, out rtName, out cosTestData, null);
                if(batchNo == batchNumber && stage == testStage)
                    cosTestDataList.Add(cosTestData);
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="batchNumber"></param>
        /// <param name="serialNumber"></param>
        /// <param name="partNumber"></param>
        /// <param name="rtName"></param>
        /// <param name="cosTestData"></param>
        /// <param name="testWarn"></param>
        internal static void Read(string filePath, out string batchNumber, out string serialNumber, out string partNumber, out string testStage, out string rtName, out StCosTestData cosTestData, TestWarnHandler testWarn)
        {
            // 初始化结果
            batchNumber = "";
            serialNumber = "";
            partNumber = "";
            testStage = "";
            rtName = "";
            cosTestData = new StCosTestData();

            try
            {
                //按行读取文件所有内容
                FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Position = 0;//移动到开头
                string strLine = "";

                // 批次号,序列号,产品型号,测试用户,测试时间,测试程序
                // BN1,SN1,PN1....
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (strLine.StartsWith("批次号"))
                        break;
                }
                if (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    string[] strSegs = strLine.Split(',');
                    if (strSegs.Length >= 5)
                    {
                        batchNumber = strSegs[0];
                        serialNumber = strSegs[1];
                        partNumber = strSegs[2];
                        testStage = strSegs[3];
                        rtName = strSegs[4];
                    }
                }

                // 机台名称,机台编号
                // HansCosBi001,1001
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (strLine.StartsWith("机台名称"))
                        break;
                }
                if (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    string[] strSegs = strLine.Split(',');
                    if (strSegs.Length >= 2)
                    {
                        //equipName = strSegs[0];
                        //equipId = strSegs[1];
                    }
                }

                string testDateTime = "";
                string testUserName = "";
                string result = "";
                // 测试用户,测试时间,测试结果
                // ww,2025/4/13 4:05:11,FAIL
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (strLine.StartsWith("测试用户"))
                        break;
                }
                if (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    string[] strSegs = strLine.Split(',');
                    if (strSegs.Length >= 3)
                    {
                        testUserName = strSegs[0];
                        testDateTime = strSegs[1];
                        result = strSegs[2];
                    }
                }


                /*  Cos Param Data   */
                while (!sr.EndOfStream)
                {
                    while (!sr.EndOfStream && strLine == "")
                    {
                        strLine = sr.ReadLine();
                    }

                    if (strLine.StartsWith("<测试原始数据>"))
                        break;

                    // 参数名,参数下限,参数上限,测试值,测试结果
                    // ITH,-9999,9999,0,
                    // PO@3W,-9999,9999,0.0030326296109706163,PASS
                    List<StCosParam> testData = new List<StCosParam>();
                    List<StCosParam> deltaData = new List<StCosParam>();
                    while (!sr.EndOfStream)
                    {
                        strLine = sr.ReadLine();
                        if (strLine.StartsWith("参数名"))
                            break;
                    }
                    while (!sr.EndOfStream)
                    {
                        strLine = sr.ReadLine();
                        if (strLine == "")
                            break;

                        bool withQuote = false;
                        int i1 = 0, i2 = 0;
                        if (strLine.Contains("\""))
                        {
                            i1 = strLine.IndexOf("\"");
                            i2 = strLine.IndexOf("\"", i1 + 1);
                            if (i1 >= 0 && i2 > i1)
                                withQuote = true;
                        }
                        if (withQuote)
                        {
                            string strName = strLine.Substring(i1 + 1, i2 - i1 - 1);
                            //if (strName.Contains("("))
                            //    strName = strName.Substring(0, strName.LastIndexOf("(")); 
                            string strRest = strLine.Substring(i2 + 1, strLine.Length - i2 - 1);
                            string[] strSegs = strRest.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (strSegs.Length >= 4)
                            {
                                if (strName.StartsWith("DELTA-"))
                                {
                                    strName = strName.Replace("DELTA-", "");
                                    StCosParam cosParam = new StCosParam()
                                    {
                                        Name = strName,
                                        ParamLCL = Convert.ToDouble(strSegs[0]),
                                        ParamUCL = Convert.ToDouble(strSegs[1]),
                                        TestValue = Convert.ToDouble(strSegs[2]),
                                        TestResult = strSegs[3]
                                    };
                                    deltaData.Add(cosParam);
                                }
                                else
                                {
                                    StCosParam cosParam = new StCosParam()
                                    {
                                        Name = strName,
                                        ParamLCL = Convert.ToDouble(strSegs[0]),
                                        ParamUCL = Convert.ToDouble(strSegs[1]),
                                        TestValue = Convert.ToDouble(strSegs[2]),
                                        TestResult = strSegs[3]
                                    };
                                    testData.Add(cosParam);
                                }
                            }
                        }
                        else
                        {
                            string[] strSegs = strLine.Split(',');
                            string strName = strSegs[0];
                            //if (strName.Contains("("))
                            //    strName = strName.Substring(0, strName.LastIndexOf("("));
                            if (strSegs.Length >= 5)
                            {
                                StCosParam cosParam = new StCosParam()
                                {
                                    Name = strName,
                                    ParamLCL = Convert.ToDouble(strSegs[1]),
                                    ParamUCL = Convert.ToDouble(strSegs[2]),
                                    TestValue = Convert.ToDouble(strSegs[3]),
                                    TestResult = strSegs[4]
                                };
                                testData.Add(cosParam);
                            }
                        }
                    }

                    // 添加到类别中来
                    cosTestData.BatchNumber = batchNumber;
                    cosTestData.PartNumber = partNumber;
                    cosTestData.SerialNumber = serialNumber;
                    cosTestData.TestStage = testStage;
                    cosTestData.RoutineName = rtName;
                    cosTestData.TestDateTime = testDateTime;
                    cosTestData.TestUserName = testUserName;
                    cosTestData.RESULT = result;
                    cosTestData.LivTestData = testData;// Combine LIV & SP & NA
                    cosTestData.SpTestData = new List<StCosParam>();
                    cosTestData.NaTestData = new List<StCosParam>();
                    cosTestData.BmTestData = new List<StCosParam>();
                    cosTestData.ThmTestData = new List<StCosParam>();
                    cosTestData.DeltaTestData = deltaData;
                    cosTestData.TestTemps = new List<double>();
                    cosTestData.LivRawData = new List<List<StGoPIV>>();
                    cosTestData.SpRawData = new List<List<StOcSpData>>();
                    cosTestData.NaRawData = new List<List<StNaData>>();
                    cosTestData.BmRawData = new List<List<StBeamData>>();
                    cosTestData.ThmRawData = new List<List<StThmData>>();
                }

                /*  Raw Data  */
                while (!sr.EndOfStream)
                {
                    strLine = strLine.Replace("\"", "");
                    if (strLine.StartsWith("<LIV数据"))
                    {
                        // <LIV数据@25C>
                        // 序号,电流(A),电压(V),功率(W)
                        // 1,0.03388333,0.00019514933,0.00012250032
                        // 2,0.033165134,0.00019514933,0.00012383037
                        // 3,0.03388333,0.00019514933,0.00012383037
                        strLine = strLine.Replace("<LIV数据@", "").Replace("C>", "");
                        double temparature = Convert.ToDouble(strLine);
                        // 跳过 "序号, ..."
                        if (!sr.EndOfStream)
                            strLine = sr.ReadLine();
                        // 提取数据
                        List<StGoPIV> livRawData = new List<StGoPIV>();
                        while (!sr.EndOfStream)
                        {
                            strLine = sr.ReadLine();
                            strLine = strLine.Replace("\"", "");
                            if (strLine.StartsWith("<"))
                                break;
                            if (strLine == "")
                                break;
                            string[] strSegs = strLine.Split(',');
                            if (strSegs.Length >= 4)
                            {
                                StGoPIV goPIV = new StGoPIV()
                                {
                                    LDI = Convert.ToSingle(strSegs[1]),
                                    LDV = Convert.ToSingle(strSegs[2]),
                                    PDI = Convert.ToSingle(strSegs[3])
                                };
                                livRawData.Add(goPIV);
                            }
                        }
                        // 保存数据
                        if (cosTestData.TestTemps == null)
                        {
                            cosTestData.TestTemps = new List<double>();
                            cosTestData.TestTemps.Add(temparature);
                            cosTestData.LivRawData = new List<List<StGoPIV>>();
                            cosTestData.SpRawData = new List<List<StOcSpData>>();
                            cosTestData.NaRawData = new List<List<StNaData>>();
                            cosTestData.LivRawData.Add(livRawData);
                        }
                        else if (cosTestData.TestTemps.Contains(temparature) == false)
                        {
                            cosTestData.TestTemps.Add(temparature);
                            cosTestData.LivRawData.Add(livRawData);
                        }
                        else
                        {
                            cosTestData.LivRawData.Add(livRawData);
                        }
                    }
                    else if (strLine.StartsWith("<光谱数据"))
                    {
                        // <光谱数据@0.2W,25C>
                        // 序号,波长(nm),幅值
                        // 1,712.05,1374.455
                        // 2,712.15,-197.644
                        strLine = strLine.Replace("<光谱数据@", "").Replace(">", "");
                        string[] strSegs = strLine.Split(',');
                        string drv = strSegs[0];
                        double temperature = Convert.ToDouble(strSegs[1].Replace("C", ""));
                        // 跳过 "序号, ..."
                        if (!sr.EndOfStream)
                            strLine = sr.ReadLine();
                        // 提取数据
                        List<double> wavelengthList = new List<double>();
                        List<double> spectrumList = new List<double>();
                        while (!sr.EndOfStream)
                        {
                            strLine = sr.ReadLine();
                            strLine = strLine.Replace("\"", "");
                            if (strLine.StartsWith("<"))
                                break;
                            if (strLine == "")
                                break;

                            strSegs = strLine.Split(',');
                            if (strSegs.Length >= 3)
                            {
                                double wavelength = Convert.ToDouble(strSegs[1]);
                                double spectrum = Convert.ToDouble(strSegs[2]);
                                wavelengthList.Add(wavelength);
                                spectrumList.Add(spectrum);
                            }
                        }
                        // 保存数据
                        StOcSpData osSpData = new StOcSpData();
                        osSpData.DRV = drv;
                        osSpData.Wavelength = wavelengthList.ToArray();
                        osSpData.Spectrum = spectrumList.ToArray();

                        // 保存数据
                        if (cosTestData.TestTemps == null)
                        {
                            cosTestData.TestTemps = new List<double>();
                            cosTestData.TestTemps.Add(temperature);
                            cosTestData.LivRawData = new List<List<StGoPIV>>();
                            cosTestData.SpRawData = new List<List<StOcSpData>>();
                            cosTestData.SpRawData.Add(new List<StOcSpData>() { osSpData });
                            cosTestData.NaRawData = new List<List<StNaData>>();
                        }
                        else if (cosTestData.TestTemps.Contains(temperature) == false)
                        {
                            cosTestData.TestTemps.Add(temperature);
                            cosTestData.SpRawData.Add(new List<StOcSpData>() { osSpData });
                        }
                        else
                        {
                            int idx = cosTestData.TestTemps.IndexOf(temperature);
                            if (idx >= cosTestData.SpRawData.Count)
                            {
                                int count = idx - cosTestData.SpRawData.Count;
                                for (int j = 0; j <= count; j++)
                                    cosTestData.SpRawData.Add(new List<StOcSpData>());
                            }
                            cosTestData.SpRawData[idx].Add(osSpData);
                        }
                    }
                    else
                    {
                        strLine = sr.ReadLine();
                    }
                }
                // 关闭文件
                sr.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("读取数据时发生异常"));
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
            }
        }

        /// <summary>
        /// 打开测试文件
        /// </summary>
        /// <param name="testWarn"></param>
        internal static void OpenTestFiles(TestWarnHandler testWarn)
        {
            if(TestSummaryFile == null || TestSummaryFile == "" || TestDatatFile == null || TestDatatFile == "")
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("当前无测试数据!"));
                return;
            }

            if(!File.Exists(TestSummaryFile))
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("找不到文件 " + TestSummaryFile));
                return;
            }

            if (!File.Exists(TestDatatFile))
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("找不到文件 " + TestDatatFile));
                return;
            }

            // 打开 TestSummaryFile
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(TestSummaryFile);
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch(Exception ex)
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("读取文件时发生异常(Summary)"));
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
            }

            // 打开 TestResultFile
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(TestDatatFile);
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch (Exception ex)
            {
                if (testWarn != null)
                    testWarn(null, new TestWarnEventArgs("读取文件时发生异常(Raw)"));
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
            }
        }

        /// <summary>
        /// 输出LIV数据
        /// </summary>
        /// <param name="livTestData"></param>
        /// <returns></returns>
        internal static bool WriteToolLivData(List<StGoPIV> livTestData)
        {
            // 检查文件夹是否存在
            string resultParentFolder = StationUtil.GetOutFolder();
            if (Directory.Exists(resultParentFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(resultParentFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + resultParentFolder + "\r\n" + ex.ToString());
                    return false;
                }
            }

            // 检查月份文件夹是否存在
            string resultFolder = resultParentFolder + @"\" + DateTime.Now.ToString("yyyyMM");
            if (Directory.Exists(resultFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(resultFolder);
                }
                catch (Exception ex)
                {
                    ErrLog.WriteLine("无法打开输出文件夹 " + resultFolder + @"\" + ex.ToString());
                    return false;
                }
            }

            // file
            FileStream fsResult;
            StreamWriter swResult;
            // 结果文件名
            TestDatatFile = resultFolder + @"\LIV-" + DateTime.Now.ToString("yyyyMMddhhmmss") + @".csv";

            // 创建Result文件
            try
            {
                fsResult = new FileStream(TestDatatFile, FileMode.Create);
                swResult = new StreamWriter(fsResult, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("无法创建文件 " + TestDatatFile + "\r\n" + ex.ToString());
                return false;
            }

            // 输出Result数据
            try
            {
                swResult.WriteLine("<LIV数据>");
                swResult.WriteLine("序号,电流(A),电压(V),功率(W)");
                for (int i = 0; i < livTestData.Count; i++)
                {
                    swResult.WriteLine((i + 1).ToString() + "," +
                        livTestData[i].LDI.ToString() + "," +
                        livTestData[i].LDV.ToString() + "," +
                        livTestData[i].PDI.ToString());
                }

                // 关闭文件
                swResult.Close();
                fsResult.Close();
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine("输出数据时发生异常 " + ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        internal static string GetPreTestFile(string batchNo, string serialNumber)
        {
            string resultFolder = StationUtil.GetOutFolder();
            if (Directory.Exists(resultFolder) == false)
                return "";
            string batchNoFolder = resultFolder + @"\" + batchNo;
            if (Directory.Exists(batchNoFolder) == false)
                return "";
            string testStageFolder = batchNoFolder + @"\" + TestSystemUtil.PRE_TEST;
            if (Directory.Exists(testStageFolder) == false)
                return "";
            string testDataFolder = testStageFolder + @"\Data";
            if (Directory.Exists(testDataFolder) == false)
                return "";
            string preTestDataFileName = testDataFolder + @"\" + serialNumber + "_" + TestSystemUtil.PRE_TEST_ABBR + @".csv";
            if (!File.Exists(preTestDataFileName))
                return "";
            else
                return preTestDataFileName;
        }
    }
}

