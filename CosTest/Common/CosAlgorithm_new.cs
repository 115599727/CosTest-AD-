using CosTest;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    class CosAlgorithm
    {
        /// <summary>
        /// 平滑点数
        /// </summary>
        private static int SmoothCount = 1;




        /// <summary>
        /// 计算阈值电流（使用分段拟合优化）
        /// </summary>
        /// <param name="livList">原始LIV数据</param>
        /// <param name="pdOffset">PD零点偏移：I=0A时原始PD功率读数</param>
        /// <param name="ith">输出阈值电流(A)</param>
        /// <param name="slope">输出微分斜率(W/A)</param>
        /// <param name="res">输出微分电阻(Ω)</param>
        internal static void Calc(List<StGoPIV> livList, double pdOffset, out double ith, out double slope, out double res)
        {
            ith = TestSystemUtil.DEFAULT_TEST_VALUE;
            slope = TestSystemUtil.DEFAULT_TEST_VALUE;
            res = TestSystemUtil.DEFAULT_TEST_VALUE;

            if (livList == null || livList.Count < 10)
                return;

            // 复制数据，扣除PD偏移
            List<StGoPIV> tempLiv = new List<StGoPIV>(livList.Count);
            foreach (var src in livList)
            {
                var item = new StGoPIV
                {
                    LDI = src.LDI,
                    LDV = src.LDV,
                    PDI = src.PDI - pdOffset
                };
                if (item.PDI < 0)
                    item.PDI = 0;
                tempLiv.Add(item);
            }

            // 平滑处理
            List<double> smoothCurrent = new List<double>(tempLiv.Count);
            List<double> smoothPower = new List<double>(tempLiv.Count);
            List<double> smoothVoltage = new List<double>(tempLiv.Count);
            List<double> dLdI_1st = new List<double>(tempLiv.Count);
            List<double> dVdI_1st = new List<double>(tempLiv.Count);

            int halfSmoothCount = SmoothCount / 2;
            int smoothCount = halfSmoothCount * 2 + 1;

            for (int i = 0; i < tempLiv.Count; i++)
            {
                if (i - halfSmoothCount < 0 || i + halfSmoothCount >= tempLiv.Count)
                {
                    smoothCurrent.Add(tempLiv[i].LDI);
                    smoothPower.Add(tempLiv[i].PDI);
                    smoothVoltage.Add(tempLiv[i].LDV);
                }
                else
                {
                    double sumCurrent = 0.0;
                    double sumPower = 0.0;
                    double sumVoltage = 0.0;
                    for (int j = i - halfSmoothCount; j <= i + halfSmoothCount; j++)
                    {
                        sumCurrent += tempLiv[j].LDI;
                        sumPower += tempLiv[j].PDI;
                        sumVoltage += tempLiv[j].LDV;
                    }
                    smoothCurrent.Add(sumCurrent / smoothCount);
                    smoothPower.Add(sumPower / smoothCount);
                    smoothVoltage.Add(sumVoltage / smoothCount);
                }
            }

            // 计算 dP/dI 和 dV/dI
            for (int i = 0; i < tempLiv.Count; i++)
            {
                if (i < 1)
                {
                    dLdI_1st.Add(0.0);
                    dVdI_1st.Add(0.0);
                }
                else
                {
                    if (smoothCurrent[i] == smoothCurrent[i - 1])
                    {
                        dLdI_1st.Add(0.0);
                        dVdI_1st.Add(0.0);
                    }
                    else
                    {
                        dLdI_1st.Add((smoothPower[i] - smoothPower[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
                        dVdI_1st.Add((smoothVoltage[i] - smoothVoltage[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
                    }
                }
            }

            // ========== 分段拟合计算 Ith ==========
            ith = CalculateIthBySegmentedFitting(tempLiv, smoothCurrent, smoothPower);

            // Slope & Res（使用稳定后的高电流区）
            int indexStartBaseSlope = FindIndexByCurrent(smoothCurrent, ith + 1.0);
            int indexStopBaseSlope = tempLiv.Count - halfSmoothCount - 1;

            if (indexStartBaseSlope >= 0 && indexStopBaseSlope > indexStartBaseSlope)
            {
                double dLdI_1st_sum = 0.0;
                for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
                    dLdI_1st_sum += dLdI_1st[i];
                slope = dLdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);

                double dVdI_1st_sum = 0.0;
                for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
                    dVdI_1st_sum += dVdI_1st[i];
                res = dVdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
            }
        }

        /// <summary>
        /// 分段拟合法计算阈值电流
        /// </summary>
        /// <summary>
        /// 分段拟合法计算阈值电流 - 密集数据版
        /// </summary>
        private static double CalculateIthBySegmentedFitting(List<StGoPIV> tempLiv, List<double> smoothCurrent, List<double> smoothPower)
        {
            List<double> ithCandidates = new List<double>();
            List<double> slopeCandidates = new List<double>();
            List<double> rSquaredCandidates = new List<double>();

            // ========== 找到第一个功率显著增长的点 ==========
            int startIdx = -1;
            for (int i = 5; i < tempLiv.Count - 10; i++)  // 从第5个点开始搜索
            {
                // 找到功率开始明显超过0的点（扣除偏移后）
                if (smoothPower[i] > 0.05 && smoothPower[i] < 0.5)
                {
                    startIdx = i;
                    break;
                }
            }

            if (startIdx < 0)
            {
                ErrLog.WriteLine("  调试: 未找到合适的起始点");
                return TestSystemUtil.DEFAULT_TEST_VALUE;
            }

            ErrLog.WriteLine($"  调试: 起始点索引={startIdx}, 电流={smoothCurrent[startIdx]:F4}, 功率={smoothPower[startIdx]:F4}");

            // ========== 方法1：从起始点开始，取不同数量的点 ==========
            for (int pointCount = 8; pointCount <= 20; pointCount += 2)  // 8-20个点
            {
                if (startIdx + pointCount > tempLiv.Count) break;

                List<DPoint> points = new List<DPoint>();
                for (int i = startIdx; i < startIdx + pointCount; i++)
                {
                    points.Add(new DPoint(smoothCurrent[i], smoothPower[i]));
                }

                if (points.Count < 5) continue;

                double rca, rcb;
                double rSquared;
                FitLineWithRSquared(points, out rca, out rcb, out rSquared);

                if (rca > 0 && rca < 2.0 && rSquared > 0.95)
                {
                    double ithCandidate = (0 - rcb) / rca;
                    if (ithCandidate > 0.3 && ithCandidate < 2.0)
                    {
                        ithCandidates.Add(ithCandidate);
                        slopeCandidates.Add(rca);
                        rSquaredCandidates.Add(rSquared);
                        ErrLog.WriteLine($"  调试: count={pointCount}, Ith={ithCandidate:F4}, 斜率={rca:F4}, R²={rSquared:F4}");
                    }
                }
            }

            // ========== 方法2：从起始点往前移，取固定10个点 ==========
            for (int offset = -3; offset <= 3; offset++)
            {
                int idx = startIdx + offset;
                if (idx < 2 || idx + 10 > tempLiv.Count) continue;

                List<DPoint> points = new List<DPoint>();
                for (int i = idx; i < idx + 10; i++)
                {
                    points.Add(new DPoint(smoothCurrent[i], smoothPower[i]));
                }

                if (points.Count < 5) continue;

                double rca, rcb;
                double rSquared;
                FitLineWithRSquared(points, out rca, out rcb, out rSquared);

                if (rca > 0 && rca < 2.0 && rSquared > 0.95)
                {
                    double ithCandidate = (0 - rcb) / rca;
                    if (ithCandidate > 0.3 && ithCandidate < 2.0)
                    {
                        ithCandidates.Add(ithCandidate);
                        slopeCandidates.Add(rca);
                        rSquaredCandidates.Add(rSquared);
                        ErrLog.WriteLine($"  调试: idx={idx}, Ith={ithCandidate:F4}, 斜率={rca:F4}, R²={rSquared:F4}");
                    }
                }
            }

            // ========== 方法3：从不同起始点取不同点数 ==========
            for (int idx = Math.Max(2, startIdx - 5); idx <= Math.Min(startIdx + 5, tempLiv.Count - 10); idx++)
            {
                for (int count = 8; count <= 15; count += 2)
                {
                    if (idx + count > tempLiv.Count) break;

                    List<DPoint> points = new List<DPoint>();
                    for (int i = idx; i < idx + count; i++)
                    {
                        points.Add(new DPoint(smoothCurrent[i], smoothPower[i]));
                    }

                    if (points.Count < 5) continue;

                    double rca, rcb;
                    double rSquared;
                    FitLineWithRSquared(points, out rca, out rcb, out rSquared);

                    if (rca > 0 && rca < 2.0 && rSquared > 0.95)
                    {
                        double ithCandidate = (0 - rcb) / rca;
                        if (ithCandidate > 0.3 && ithCandidate < 2.0)
                        {
                            ithCandidates.Add(ithCandidate);
                            slopeCandidates.Add(rca);
                            rSquaredCandidates.Add(rSquared);
                        }
                    }
                }
            }

            // 如果没有候选，使用简单方法
            if (ithCandidates.Count < 3)
            {
                ErrLog.WriteLine("  调试: 候选太少，使用简单方法");
                List<DPoint> points = new List<DPoint>();
                for (int i = 10; i < Math.Min(25, tempLiv.Count); i++)  // 从0.9A开始取15个点
                {
                    points.Add(new DPoint(smoothCurrent[i], smoothPower[i]));
                }

                if (points.Count >= 5)
                {
                    double rca, rcb;
                    FitLine(points, out rca, out rcb);
                    if (rca > 0)
                    {
                        double ithCandidate = (0 - rcb) / rca;
                        if (ithCandidate > 0.3 && ithCandidate < 2.0)
                        {
                            ithCandidates.Add(ithCandidate);
                        }
                    }
                }
            }

            if (ithCandidates.Count == 0)
            {
                ErrLog.WriteLine("  调试: 没有有效的候选值");
                return TestSystemUtil.DEFAULT_TEST_VALUE;
            }

            // 去除异常值
            ithCandidates.Sort();
            int removeCount = Math.Max(1, ithCandidates.Count / 6);
            if (removeCount > 0 && ithCandidates.Count > 4)
            {
                ithCandidates.RemoveRange(0, removeCount);
                ithCandidates.RemoveRange(ithCandidates.Count - removeCount, removeCount);
            }

            // 取中位数
            double finalIth = ithCandidates[ithCandidates.Count / 2];

            ErrLog.WriteLine($"分段拟合结果: 共{ithCandidates.Count}个候选值");
            ErrLog.WriteLine($"  中位数: {finalIth:F4}, 平均值: {ithCandidates.Average():F4}");
            ErrLog.WriteLine($"  候选值范围: {ithCandidates.Min():F4} ~ {ithCandidates.Max():F4}");
            for (int i = 0; i < Math.Min(ithCandidates.Count, 10); i++)
            {
                ErrLog.WriteLine($"  候选{i + 1}: Ith={ithCandidates[i]:F4}");
            }

            return finalIth;
        }


        /// <summary>
        /// 传统方法计算Ith（作为备选）
        /// </summary>
        private static double CalculateIthTraditional(List<StGoPIV> tempLiv, List<double> smoothCurrent, List<double> smoothPower)
        {
            // 找到第一个功率 > 0.5W 的点
            int startIdx = -1;
            for (int i = 2; i < tempLiv.Count; i++)
            {
                if (smoothPower[i] > 0.5)
                {
                    startIdx = i;
                    break;
                }
            }

            if (startIdx < 0 || startIdx >= tempLiv.Count - 3)
                return TestSystemUtil.DEFAULT_TEST_VALUE;

            // 取从startIdx开始的10个点
            List<DPoint> points = new List<DPoint>();
            for (int i = startIdx; i < Math.Min(startIdx + 12, tempLiv.Count); i++)
            {
                points.Add(new DPoint(smoothCurrent[i], smoothPower[i]));
            }

            if (points.Count < 3)
                return TestSystemUtil.DEFAULT_TEST_VALUE;

            double rca, rcb;
            FitLine(points, out rca, out rcb);

            if (rca <= 0)
                return TestSystemUtil.DEFAULT_TEST_VALUE;

            double ith = (0 - rcb) / rca;
            return (ith > 0 && ith < 5.0) ? ith : TestSystemUtil.DEFAULT_TEST_VALUE;
        }

        /// <summary>
        /// 最小二乘法拟合直线（带R²计算）
        /// </summary>
        private static void FitLineWithRSquared(List<DPoint> ptList, out double rca, out double rcb, out double rSquared)
        {
            FitLine(ptList, out rca, out rcb);

            // 计算R²（拟合优度）
            if (ptList.Count < 3 || rca == 0)
            {
                rSquared = 0;
                return;
            }

            double meanY = ptList.Average(p => p.PDI);
            double ssTot = 0; // 总平方和
            double ssRes = 0; // 残差平方和

            foreach (var pt in ptList)
            {
                double yPred = rca * pt.LDI + rcb;
                ssTot += (pt.PDI - meanY) * (pt.PDI - meanY);
                ssRes += (pt.PDI - yPred) * (pt.PDI - yPred);
            }

            rSquared = 1 - (ssRes / ssTot);
            if (rSquared < 0) rSquared = 0;
        }

        /// <summary>
        /// 根据电流值查找索引
        /// </summary>
        private static int FindIndexByCurrent(List<double> currentList, double targetCurrent)
        {
            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i] >= targetCurrent)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 最小二乘法拟合直线（原有方法保留）
        /// </summary>
        private static void FitLine(List<DPoint> ptList, out double rca, out double rcb)
        {
            double averagex = 0, averagey = 0;
            foreach (var pt in ptList)
            {
                averagex += pt.LDI;
                averagey += pt.PDI;
            }
            averagex /= ptList.Count;
            averagey /= ptList.Count;

            double numerator = 0;
            double denominator = 0;
            for (int i = 0; i < ptList.Count; i++)
            {
                numerator += (ptList[i].LDI - averagex) * (ptList[i].PDI - averagey);
                denominator += (ptList[i].LDI - averagex) * (ptList[i].LDI - averagex);
            }

            if (Math.Abs(denominator) > 1.0e-8 && Math.Abs(denominator) < 1.0e8)
            {
                rca = numerator / denominator;
                rcb = averagey - rca * averagex;
            }
            else
            {
                rca = 0.0;
                rcb = 0.0;
            }
        }







        ///// <summary>
        ///// 计算阈值电流（增加PD零点偏移扣除）
        ///// </summary>
        ///// <param name="livList">原始LIV数据</param>
        ///// <param name="pdOffset">PD零点偏移：I=0A时原始PD功率读数</param>
        ///// <param name="ith">输出阈值电流(A)</param>
        ///// <param name="slope">输出微分斜率(W/A)</param>
        ///// <param name="res">输出微分电阻(Ω)</param>
        //internal static void Calc(List<StGoPIV> livList, double pdOffset, out double ith, out double slope, out double res)
        //{
        //    ith = TestSystemUtil.DEFAULT_TEST_VALUE;
        //    slope = TestSystemUtil.DEFAULT_TEST_VALUE;
        //    res = TestSystemUtil.DEFAULT_TEST_VALUE;

        //    if (livList == null || livList.Count < 10)
        //        return;

        //    // ========== 关键：复制一份数据，扣除PD偏移，不修改外部原始list ==========
        //    List<StGoPIV> tempLiv = new List<StGoPIV>(livList.Count);
        //    foreach (var src in livList)
        //    {
        //        var item = new StGoPIV
        //        {
        //            LDI = src.LDI,
        //            LDV = src.LDV,
        //            PDI = src.PDI - pdOffset // 扣除零点偏移

        //        };

        //        // 噪声导致功率为负，强制置0
        //        if (item.PDI < 0)
        //            item.PDI = 0;
        //        tempLiv.Add(item);
        //    }


        //    List<double> smoothCurrent = new List<double>(tempLiv.Count);
        //    List<double> smoothPower = new List<double>(tempLiv.Count);
        //    List<double> smoothVoltage = new List<double>(tempLiv.Count);
        //    List<double> dLdI_1st = new List<double>(tempLiv.Count);
        //    List<double> dVdI_1st = new List<double>(tempLiv.Count);

        //    int halfSmoothCount = SmoothCount / 2;
        //    int smoothCount = halfSmoothCount * 2 + 1;

        //    //滑动平均平滑，使用扣除偏移后的tempLiv
        //    for (int i = 0; i < tempLiv.Count; i++)
        //    {
        //        if (i - halfSmoothCount < 0 || i + halfSmoothCount >= tempLiv.Count)
        //        {
        //            smoothCurrent.Add(tempLiv[i].LDI);
        //            smoothPower.Add(tempLiv[i].PDI);
        //            smoothVoltage.Add(tempLiv[i].LDV);
        //        }
        //        else
        //        {
        //            double sumCurrent = 0.0;
        //            double sumPower = 0.0;
        //            double sumVoltage = 0.0;
        //            for (int j = i - halfSmoothCount; j <= i + halfSmoothCount; j++)
        //            {
        //                sumCurrent += tempLiv[j].LDI;
        //                sumPower += tempLiv[j].PDI;
        //                sumVoltage += tempLiv[j].LDV;
        //            }
        //            smoothCurrent.Add(sumCurrent / smoothCount);
        //            smoothPower.Add(sumPower / smoothCount);
        //            smoothVoltage.Add(sumVoltage / smoothCount);
        //        }
        //    }

        //    // 计算 DL/DI 1st 
        //    for (int i = 0; i < tempLiv.Count; i++)
        //    {
        //        if (i < 1)
        //        {
        //            dLdI_1st.Add(0.0);
        //            dVdI_1st.Add(0.0);
        //        }
        //        else
        //        {
        //            if (smoothCurrent[i] == smoothCurrent[i - 1])
        //            {
        //                dLdI_1st.Add(0.0);
        //                dVdI_1st.Add(0.0);
        //            }
        //            else
        //            {
        //                dLdI_1st.Add((smoothPower[i] - smoothPower[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
        //                dVdI_1st.Add((smoothVoltage[i] - smoothVoltage[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
        //            }
        //        }
        //    }

        //    // 计算ith
        //    int indexGrossThresh = 0;
        //    double zeroPower;
        //    if (tempLiv[1].LDI - tempLiv[0].LDI < 0.3)
        //        zeroPower = (tempLiv[0].PDI + tempLiv[1].PDI) / 2;
        //    else
        //        zeroPower = tempLiv[0].PDI;

        //    List<DPoint> postThreshPoints = new List<DPoint>();
        //    for (int i = 1; i < tempLiv.Count; i++)
        //    {
        //        if (tempLiv[i].PDI >= zeroPower + 0.3)
        //        {
        //            postThreshPoints.Add(new DPoint(tempLiv[i].LDI, tempLiv[i].PDI));
        //            if (indexGrossThresh == 0)
        //                indexGrossThresh = i;
        //            if (tempLiv[i].PDI >= zeroPower + 5)
        //            {
        //                //固定向后追加3个点
        //                for (int j = 0; j < 3; j++)
        //                {
        //                    int idx = i + j + 1;
        //                    if (idx >= tempLiv.Count)
        //                        break;
        //                    postThreshPoints.Add(new DPoint(tempLiv[idx].LDI, tempLiv[idx].PDI));
        //                }
        //                break;
        //            }
        //        }
        //    }

        //    int iw = 0;
        //    ErrLog.WriteLine("postThreshPoints count:" + postThreshPoints.Count());
        //    foreach (var item in postThreshPoints)
        //    {
        //        iw++;
        //        ErrLog.WriteLine($"postThreshPoints  LDI:{item.LDI}  PDI:{item.PDI}");
        //    }


        //    if (postThreshPoints.Count < 2)
        //    {
        //        ith = tempLiv[indexGrossThresh].LDI;
        //    }
        //    else
        //    {
        //        double rca, rcb;
        //        FitLine(postThreshPoints, out rca, out rcb);
        //        ErrLog.WriteLine($"rca={rca:F6}, rcb={rcb:F6}");
        //        if (rca == 0.0)
        //            ith = tempLiv[indexGrossThresh].LDI;
        //        else
        //        {
        //            ith = (zeroPower - rcb) / rca;
        //            ErrLog.WriteLine($"ith={ith:F4}");
        //        }
        //    }

        //    // 增加简单保护：Ith不能为负数，不能大于扫描最大电流
        //    double maxScanI = tempLiv.Last().LDI;
        //    if (ith < 0 || ith > maxScanI)
        //    {
        //        ith = TestSystemUtil.DEFAULT_TEST_VALUE;
        //    }

        //    // Slope & Kink & Voltage
        //    int indexStartBaseSlope = indexGrossThresh;
        //    for (; indexStartBaseSlope < smoothCurrent.Count; indexStartBaseSlope++)
        //    {
        //        if (smoothCurrent[indexStartBaseSlope] >= ith + 1.0)
        //            break;
        //    }
        //    int indexStopBaseSlope = tempLiv.Count - halfSmoothCount - 1;
        //    if (indexStopBaseSlope <= indexStartBaseSlope)
        //        return;

        //    // Slope
        //    double dLdI_1st_sum = 0.0;
        //    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
        //        dLdI_1st_sum += dLdI_1st[i];
        //    slope = dLdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);

        //    // Res
        //    double dVdI_1st_sum = 0.0;
        //    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
        //        dVdI_1st_sum += dVdI_1st[i];
        //    res = dVdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
        //}


        ///// <summary>
        ///// 最小二乘法拟合直线
        ///// </summary>
        //private static void FitLine(List<DPoint> ptList, out double rca, out double rcb)
        //{
        //    ErrLog.WriteLine($"FitLine入口 ptList[0]  LDI={ptList[0].LDI} , PDI={ptList[0].PDI}");
        //    ErrLog.WriteLine($"FitLine入口 ptList[14] LDI={ptList[14].LDI}, PDI={ptList[14].PDI}");

        //    double averagex = 0, averagey = 0;
        //    foreach (var pt in ptList)
        //    {
        //        averagex += pt.LDI;
        //        averagey += pt.PDI;
        //    }
        //    averagex /= ptList.Count;
        //    averagey /= ptList.Count;  // ← 修复：计算平均值
        //    ErrLog.WriteLine($"averagex={averagex:F4} averagey={averagey:F4}");

        //    double numerator = 0;
        //    double denominator = 0;
        //    for (int i = 0; i < ptList.Count; i++)
        //    {
        //        numerator += (ptList[i].LDI - averagex) * (ptList[i].PDI - averagey);
        //        denominator += (ptList[i].LDI - averagex) * (ptList[i].LDI - averagex);
        //    }
        //    ErrLog.WriteLine($"numerator={numerator:F4} denominator={denominator:F4}");

        //    if (Math.Abs(denominator) > 1.0e-8 && Math.Abs(denominator) < 1.0e8)
        //    {
        //        rca = numerator / denominator;
        //        rcb = averagey - rca * averagex;
        //    }
        //    else
        //    {
        //        rca = 0.0;
        //        rcb = 0.0;
        //    }
        //}

        /// <summary>
        /// 计算阈值电流
        /// </summary>
        /// <param name="livList"></param>
        /// <returns></returns>
        internal static bool CalcVoltKink(List<StGoPIV> livList)
        {
            if (livList == null || livList.Count < 10)
                return false;

            List<double> dVdI_1st = new List<double>(livList.Count);

            // 添加DV_DI,以0.5A为间隔
            int idxBaseEnd = -1;
            for (int i = 0; i < livList.Count; i++)
            {
                if (livList[i].LDI < 0.5)
                    continue;
                for (int j = i + 1; j < livList.Count; j++)
                {
                    if (livList[j].LDI - livList[i].LDI >= 0.5)
                    {
                        dVdI_1st.Add((livList[j].PDI - livList[i].PDI) / (livList[j].LDI - livList[i].LDI));
                        if (livList[i].LDI < 1.5)
                            idxBaseEnd++;
                        break;
                    }
                }
            }
            // 判断结果
            if (idxBaseEnd < 0)
                return false;

            double dVdISum = 0.0;
            for (int i = 0; i <= idxBaseEnd; i++)
                dVdISum += dVdI_1st[i];
            double dVDiBase = dVdISum / (idxBaseEnd + 1);

            for (int i = idxBaseEnd + 1; i < dVdI_1st.Count; i++)
            {
                if (dVdI_1st[i] > 3 * dVDiBase || dVdI_1st[i] < dVDiBase / 3)
                    return false;
            }

            return true;
        }
    }

    /// <summary>双精度点，用于LIV最小二乘，替代PointF</summary>
    public struct DPoint
    {
        public double LDI;
        public double PDI;
        public DPoint(double x, double y)
        {
            LDI = x;
            PDI = y;
        }
    }

}

///// <summary>
///// 计算阈值电流
///// </summary>
///// <param name="livList">每一点 = 驱动电流 LDI、光功率 PDI、LD 电压 LD</param>
///// <param name="ith">阈值电流</param>
///// <param name="slope">微分斜率</param>
///// <param name="res">微分电阻</param>
///// <returns></returns>
//internal static void Calc(List<StGoPIV> livList, out double ith, out double slope, out double res)
//{
//    ith = TestSystemUtil.DEFAULT_TEST_VALUE;
//    slope = TestSystemUtil.DEFAULT_TEST_VALUE;
//    res = TestSystemUtil.DEFAULT_TEST_VALUE;

//    if (livList == null || livList.Count < 10)
//        return;

//    List<double> smoothCurrent = new List<double>(livList.Count);//平滑电流
//    List<double> smoothPower = new List<double>(livList.Count);//平滑光功率
//    List<double> smoothVoltage = new List<double>(livList.Count);//平滑电压
//    List<double> dLdI_1st = new List<double>(livList.Count);//ΔP / ΔI 微分斜率
//    List<double> dVdI_1st = new List<double>(livList.Count);//ΔV / ΔI 微分电阻

//    // 平滑电流和功率（均值滤波）消除采集噪声，避免差分求导放大噪声
//    int halfSmoothCount = SmoothCount / 2;     //1
//    int smoothCount = halfSmoothCount * 2 + 1; //1 总窗口大小
//    for (int i = 0; i < livList.Count; i++)
//    {
//        if (i - halfSmoothCount < 0 || i + halfSmoothCount >= livList.Count)
//        {
//            smoothCurrent.Add(livList[i].LDI);
//            smoothPower.Add(livList[i].PDI);
//            smoothVoltage.Add(livList[i].LDV);
//        }
//        else
//        {
//            double sumCurrent = 0.0;
//            double sumPower = 0.0;
//            double sumVoltage = 0.0;
//            for (int j = i - halfSmoothCount; j <= i + halfSmoothCount; j++)
//            {
//                sumCurrent += (livList[j].LDI);
//                sumPower += (livList[j].PDI);
//                sumVoltage += (livList[j].LDV);
//            }
//            smoothCurrent.Add(sumCurrent / smoothCount);
//            smoothPower.Add(sumPower / smoothCount);
//            smoothVoltage.Add(sumVoltage / smoothCount);
//        }
//    }

//    // 计算 DL/DI 1st     计算一阶差分
//    for (int i = 0; i < livList.Count; i++)
//    {
//        if (i < 1)
//        {
//            dLdI_1st.Add(0.0);
//            dVdI_1st.Add(0.0);
//        }
//        else
//        {
//            if (smoothCurrent[i] == smoothCurrent[i - 1])
//            {
//                dLdI_1st.Add(0.0);
//                dVdI_1st.Add(0.0);
//            }
//            else
//            {
//                dLdI_1st.Add((smoothPower[i] - smoothPower[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
//                dVdI_1st.Add((smoothVoltage[i] - smoothVoltage[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
//            }  
//        }
//    }


//    // 计算ith
//    int indexGrossThresh = 0;
//    double zeroPower;//零电流附近基线光功率（自发辐射，没有激光输出）

//    //基线光功率 zeroPower（自发辐射基底）
//    if (livList[1].LDI - livList[0].LDI < 0.3)
//        zeroPower = (livList[0].PDI + livList[1].PDI) / 2;
//    else
//        zeroPower = livList[0].PDI;

//    //收集阈值之后线性段点
//    //光功率大于基线zeroPower+0.5，认为进入起振区域，开始收集拟合点；
//    //当光功率到达zeroPower + 5，再多追加 3 个点，停止收集；
//    List<PointF> postThreshPoints = new List<PointF>();
//    for (int i = 1; i < livList.Count; i++)
//    {
//        if(livList[i].PDI >= zeroPower + 0.5)
//        {
//            postThreshPoints.Add(new PointF((float)livList[i].LDI, (float)livList[i].PDI));
//            if (indexGrossThresh == 0)
//                indexGrossThresh = i;
//            if (livList[i].PDI >= zeroPower + 5)
//            {
//                int more = 3 - postThreshPoints.Count;
//                for (int j = 0; j<more;j++)
//                {
//                    if (i + j + 1 >= livList.Count)
//                        break;
//                    postThreshPoints.Add(new PointF((float)livList[i + j + 1].LDI, (float)livList[i + j + 1].PDI));
//                }
//                break;
//            }
//        }
//    }

//    //最小二乘直线拟合，外推法求 Ith
//    if (postThreshPoints.Count < 2)
//        ith = livList[indexGrossThresh].LDI;
//    else
//    {
//        double rca, rcb;
//        FitLine(postThreshPoints, out rca, out rcb);
//        ////直线方程 P = rca * I + rcb
//        ///   光功率 P = 斜率*电流I+截距
//        if (rca == 0.0)
//            ith = livList[indexGrossThresh].LDI;
//        else
//        {
//            ////令P = zeroPower，求I，就是阈值电流
//            ith = (zeroPower - rcb) / rca;
//        }
//    }


//    // Slope & Kink & Voltage
//    // Base Slope Range
//    //计算微分斜率 slope 和微分电阻 res
//    //从 ith+1.0mA 开始，避开阈值附近过渡区
//    int indexStartBaseSlope = indexGrossThresh;
//    for (; indexStartBaseSlope < smoothCurrent.Count; indexStartBaseSlope++)
//    {
//        if (smoothCurrent[indexStartBaseSlope] >= ith + 1.0)
//            break;
//    }
//    int indexStopBaseSlope = livList.Count - halfSmoothCount - 1;
//    if(indexStopBaseSlope <= indexStartBaseSlope)
//        return;
//    // Slope
//    //dL/dI求平均，区间内对一阶导数dLdI_1st求平均微分斜率，得到slope
//    double dLdI_1st_sum = 0.0;
//    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
//        dLdI_1st_sum += dLdI_1st[i];
//    slope = dLdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
//    // Res
//    //dV/dI求平均，区间内对dVdI_1st求平均得到微分电阻res
//    double dVdI_1st_sum = 0.0;
//    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
//        dVdI_1st_sum += dVdI_1st[i];
//    res = dVdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
//}


///// <summary>
///// 计算阈值电流（增加PD零点偏移扣除）
///// </summary>
///// <param name="livList">原始LIV数据</param>
///// <param name="pdOffset">PD零点偏移：I=0A时原始PD功率读数</param>
///// <param name="ith">输出阈值电流(A)</param>
///// <param name="slope">输出微分斜率(W/A)</param>
///// <param name="res">输出微分电阻(Ω)</param>
//internal static void Calc(List<StGoPIV> livList, double pdOffset, out double ith, out double slope, out double res)
//{
//    ith = TestSystemUtil.DEFAULT_TEST_VALUE;
//    slope = TestSystemUtil.DEFAULT_TEST_VALUE;
//    res = TestSystemUtil.DEFAULT_TEST_VALUE;

//    if (livList == null || livList.Count < 10)
//        return;

//    // ========== 关键：复制一份数据，扣除PD偏移，不修改外部原始list ==========
//    List<StGoPIV> tempLiv = new List<StGoPIV>(livList.Count);
//    foreach (var src in livList)
//    {
//        var item = new StGoPIV
//        {
//            LDI = src.LDI,
//            LDV = src.LDV,
//            PDI = src.PDI - pdOffset // 扣除零点偏移
//        };
//        // 噪声导致功率为负，强制置0
//        if (item.PDI < 0)
//            item.PDI = 0;
//        tempLiv.Add(item);
//    }

//    List<double> smoothCurrent = new List<double>(tempLiv.Count);
//    List<double> smoothPower = new List<double>(tempLiv.Count);
//    List<double> smoothVoltage = new List<double>(tempLiv.Count);
//    List<double> dLdI_1st = new List<double>(tempLiv.Count);
//    List<double> dVdI_1st = new List<double>(tempLiv.Count);

//    int halfSmoothCount = SmoothCount / 2;
//    int smoothCount = halfSmoothCount * 2 + 1;

//    //滑动平均平滑，使用扣除偏移后的tempLiv
//    for (int i = 0; i < tempLiv.Count; i++)
//    {
//        if (i - halfSmoothCount < 0 || i + halfSmoothCount >= tempLiv.Count)
//        {
//            smoothCurrent.Add(tempLiv[i].LDI);
//            smoothPower.Add(tempLiv[i].PDI);
//            smoothVoltage.Add(tempLiv[i].LDV);
//        }
//        else
//        {
//            double sumCurrent = 0.0;
//            double sumPower = 0.0;
//            double sumVoltage = 0.0;
//            for (int j = i - halfSmoothCount; j <= i + halfSmoothCount; j++)
//            {
//                sumCurrent += tempLiv[j].LDI;
//                sumPower += tempLiv[j].PDI;
//                sumVoltage += tempLiv[j].LDV;
//            }
//            smoothCurrent.Add(sumCurrent / smoothCount);
//            smoothPower.Add(sumPower / smoothCount);
//            smoothVoltage.Add(sumVoltage / smoothCount);
//        }
//    }

//    // 计算 DL/DI 1st 
//    for (int i = 0; i < tempLiv.Count; i++)
//    {
//        if (i < 1)
//        {
//            dLdI_1st.Add(0.0);
//            dVdI_1st.Add(0.0);
//        }
//        else
//        {
//            if (smoothCurrent[i] == smoothCurrent[i - 1])
//            {
//                dLdI_1st.Add(0.0);
//                dVdI_1st.Add(0.0);
//            }
//            else
//            {
//                dLdI_1st.Add((smoothPower[i] - smoothPower[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
//                dVdI_1st.Add((smoothVoltage[i] - smoothVoltage[i - 1]) / (smoothCurrent[i] - smoothCurrent[i - 1]));
//            }
//        }
//    }

//    // 计算ith
//    int indexGrossThresh = 0;
//    double zeroPower;
//    if (tempLiv[1].LDI - tempLiv[0].LDI < 0.3)
//        zeroPower = (tempLiv[0].PDI + tempLiv[1].PDI) / 2;
//    else
//        zeroPower = tempLiv[0].PDI;

//    List<PointF> postThreshPoints = new List<PointF>();
//    for (int i = 1; i < tempLiv.Count; i++)
//    {
//        if (tempLiv[i].PDI >= zeroPower + 0.5)
//        {
//            postThreshPoints.Add(new PointF((float)tempLiv[i].LDI, (float)tempLiv[i].PDI));
//            if (indexGrossThresh == 0)
//                indexGrossThresh = i;
//            if (tempLiv[i].PDI >= zeroPower + 5)
//            {
//                int more = 3 - postThreshPoints.Count;
//                for (int j = 0; j < more; j++)
//                {
//                    if (i + j + 1 >= tempLiv.Count)
//                        break;
//                    postThreshPoints.Add(new PointF((float)tempLiv[i + j + 1].LDI, (float)tempLiv[i + j + 1].PDI));
//                }
//                break;
//            }
//        }
//    }

//    if (postThreshPoints.Count < 2)
//    {
//        ith = tempLiv[indexGrossThresh].LDI;
//    }
//    else
//    {
//        double rca, rcb;
//        FitLine(postThreshPoints, out rca, out rcb);

//        if (rca == 0.0)
//            ith = tempLiv[indexGrossThresh].LDI;
//        else
//        {
//            ith = (zeroPower - rcb) / rca;
//        }
//    }

//    // 增加简单保护：Ith不能为负数，不能大于扫描最大电流
//    double maxScanI = tempLiv.Last().LDI;
//    if (ith < 0 || ith > maxScanI)
//    {
//        ith = TestSystemUtil.DEFAULT_TEST_VALUE;
//    }

//    // Slope & Kink & Voltage
//    int indexStartBaseSlope = indexGrossThresh;
//    for (; indexStartBaseSlope < smoothCurrent.Count; indexStartBaseSlope++)
//    {
//        if (smoothCurrent[indexStartBaseSlope] >= ith + 1.0)
//            break;
//    }
//    int indexStopBaseSlope = tempLiv.Count - halfSmoothCount - 1;
//    if (indexStopBaseSlope <= indexStartBaseSlope)
//        return;

//    // Slope
//    double dLdI_1st_sum = 0.0;
//    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
//        dLdI_1st_sum += dLdI_1st[i];
//    slope = dLdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);

//    // Res
//    double dVdI_1st_sum = 0.0;
//    for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
//        dVdI_1st_sum += dVdI_1st[i];
//    res = dVdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
//}

