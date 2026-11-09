using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CosTest
{
    class CosAlgorithm
    {
        /// <summary>
        /// 平滑点数
        /// </summary>
        private static int SmoothCount = 1;

        /// <summary>
        /// 计算阈值电流
        /// </summary>
        /// <param name="livList"></param>
        /// <returns></returns>
        internal static void Calc(List<StGoPIV> livList, out double ith, out double slope, out double res)
        {
            ith = TestSystemUtil.DEFAULT_TEST_VALUE;
            slope = TestSystemUtil.DEFAULT_TEST_VALUE;
            res = TestSystemUtil.DEFAULT_TEST_VALUE;

            if (livList == null || livList.Count < 10)
                return;

            List<double> smoothCurrent = new List<double>(livList.Count);
            List<double> smoothPower = new List<double>(livList.Count);
            List<double> smoothVoltage = new List<double>(livList.Count);
            List<double> dLdI_1st = new List<double>(livList.Count);
            List<double> dVdI_1st = new List<double>(livList.Count);

            // 平滑电流和功率
            int halfSmoothCount = SmoothCount / 2;
            int smoothCount = halfSmoothCount * 2 + 1;
            for (int i = 0; i < livList.Count; i++)
            {
                if (i - halfSmoothCount < 0 || i + halfSmoothCount >= livList.Count)
                {
                    smoothCurrent.Add(livList[i].LDI);
                    smoothPower.Add(livList[i].PDI);
                    smoothVoltage.Add(livList[i].LDV);
                }
                else
                {
                    double sumCurrent = 0.0;
                    double sumPower = 0.0;
                    double sumVoltage = 0.0;
                    for (int j = i - halfSmoothCount; j <= i + halfSmoothCount; j++)
                    {
                        sumCurrent += (livList[j].LDI);
                        sumPower += (livList[j].PDI);
                        sumVoltage += (livList[j].LDV);
                    }
                    smoothCurrent.Add(sumCurrent / smoothCount);
                    smoothPower.Add(sumPower / smoothCount);
                    smoothVoltage.Add(sumVoltage / smoothCount);
                }
            }

            // 计算 DL/DI 1st 
            for (int i = 0; i < livList.Count; i++)
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


            // 计算ith
            int indexGrossThresh = 0;
            double zeroPower;
            if (livList[1].LDI - livList[0].LDI < 0.3)
                zeroPower = (livList[0].PDI + livList[1].PDI) / 2;
            else
                zeroPower = livList[0].PDI;
            List<PointF> postThreshPoints = new List<PointF>();
            for (int i = 1; i < livList.Count; i++)
            {
                if(livList[i].PDI >= zeroPower + 0.5)
                {
                    postThreshPoints.Add(new PointF((float)livList[i].LDI, (float)livList[i].PDI));
                    if (indexGrossThresh == 0)
                        indexGrossThresh = i;
                    if (livList[i].PDI >= zeroPower + 5)
                    {
                        int more = 3 - postThreshPoints.Count;
                        for (int j = 0; j<more;j++)
                        {
                            if (i + j + 1 >= livList.Count)
                                break;
                            postThreshPoints.Add(new PointF((float)livList[i + j + 1].LDI, (float)livList[i + j + 1].PDI));
                        }
                        break;
                    }
                }
            }

            if (postThreshPoints.Count < 2)
                ith = livList[indexGrossThresh].LDI;
            else
            {
                double rca, rcb;
                FitLine(postThreshPoints, out rca, out rcb);

                if (rca == 0.0)
                    ith = livList[indexGrossThresh].LDI;
                else
                {
                    ith = (zeroPower - rcb) / rca;
                }
            }


            // Slope & Kink & Voltage
            // Base Slope Range
            int indexStartBaseSlope = indexGrossThresh;
            for (; indexStartBaseSlope < smoothCurrent.Count; indexStartBaseSlope++)
            {
                if (smoothCurrent[indexStartBaseSlope] >= ith + 1.0)
                    break;
            }
            int indexStopBaseSlope = livList.Count - halfSmoothCount - 1;
            if(indexStopBaseSlope <= indexStartBaseSlope)
                return;
            // Slope
            double dLdI_1st_sum = 0.0;
            for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
                dLdI_1st_sum += dLdI_1st[i];
            slope = dLdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
            // Res
            double dVdI_1st_sum = 0.0;
            for (int i = indexStartBaseSlope; i <= indexStopBaseSlope; i++)
                dVdI_1st_sum += dVdI_1st[i];
            res = dVdI_1st_sum / (indexStopBaseSlope - indexStartBaseSlope + 1);
        }

        /// <summary>
        /// 最小二乘法拟合直线
        /// </summary>
        private static void FitLine(List<PointF> ptList, out double rca, out double rcb)
        {
            double averagex = 0, averagey = 0;
            foreach (PointF pt in ptList)
            {
                averagex += pt.X;
                averagey += pt.Y;
            }
            averagex /= ptList.Count;        //  取X坐标的平均数
            averagey /= ptList.Count;        //  取Y坐标的平均数


            //经验回归系数的分子与分母
            double numerator = 0;
            double denominator = 0;
            for (int i = 0; i < ptList.Count; i++)
            {
                numerator += (ptList[i].X - averagex) * (ptList[i].Y - averagey);
                denominator += (ptList[i].X - averagex) * (ptList[i].X - averagex);
            }
            if(Math.Abs(denominator) > 1.0e-8 && Math.Abs(denominator) < 1.0e8)
            {
                //回归系数a（Regression Coefficient）   y = ax + b ;
                rca = numerator / denominator;
                //回归系数b
                rcb = averagey - rca * averagex;
            }
            else
            {
                rca = 0.0;
                rcb = 0.0;
            }
        }

        #region 光谱平滑算法 (显示层专用 — 计算层仍用原始数据)

        // ------------------------------------------------------------------
        // SG 卷积系数表 (多项式阶数 = 3)
        // 注: 窗口过大会产生 Gibb's ringing (主峰边缘假峰), 显示层上限 w=13
        // ------------------------------------------------------------------
        private static readonly Dictionary<int, double[]> SGCoefficients = new Dictionary<int, double[]>
        {
            { 5,  new double[] { -3, 12, 17, 12, -3 } },
            { 7,  new double[] { -21, 14, 39, 54, 39, 14, -21 } },
            { 9,  new double[] { -21, 14, 39, 54, 67, 54, 39, 14, -21 } },
            { 11, new double[] { -36, 9, 44, 69, 84, 89, 84, 69, 44, 9, -36 } },
            { 13, new double[] { -25, 0, 30, 60, 90, 115, 131, 115, 90, 60, 30, 0, -25 } }
        };
        private static readonly Dictionary<int, double> SGDivisor = new Dictionary<int, double>
        {
            { 5, 35 }, { 7, 231 }, { 9, 231 }, { 11, 231 }, { 13, 143 }
        };

        private static double[] BuildGaussianKernel(int windowSize, double sigma)
        {
            if (windowSize % 2 == 0) windowSize++;
            int half = windowSize / 2;
            double[] kernel = new double[windowSize];
            double sum = 0.0;
            for (int i = 0; i < windowSize; i++)
            {
                double x = i - half;
                kernel[i] = Math.Exp(-(x * x) / (2.0 * sigma * sigma));
                sum += kernel[i];
            }
            for (int i = 0; i < windowSize; i++) kernel[i] /= sum;
            return kernel;
        }

        /// <summary>
        /// 通用卷积 — 边界镜像对称 (避免端点截断伪影)
        /// </summary>
        private static double[] Convolve(double[] data, double[] kernel)
        {
            int half = kernel.Length / 2;
            int n = data.Length;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int k = -half; k <= half; k++)
                {
                    int j = i + k;
                    if (j < 0) j = -j;              // 左: 镜像
                    if (j >= n) j = 2 * n - 2 - j;  // 右: 镜像
                    sum += kernel[k + half] * data[j];
                }
                result[i] = sum < 0 ? 0 : sum;
            }
            return result;
        }

        /// <summary>
        /// 迭代高斯 (Iterated Gaussian): 3 次小窗口卷积 = 1 次超大窗口卷积,
        /// 但边界镜像处理更自然, 不容易在端点出鬼峰.
        /// 等价窗口 ≈ windowSize * iterationCount
        /// </summary>
        private static double[] IteratedGaussian(double[] data, int windowSize, double sigma, int iterations = 3)
        {
            double[] result = data;
            for (int it = 0; it < iterations; it++)
            {
                double[] kernel = BuildGaussianKernel(windowSize, sigma);
                result = Convolve(result, kernel);
            }
            return result;
        }

        // ============================================================
        // 对外公开方法
        // ============================================================

        /// <summary>
        /// Savitzky-Golay 平滑 — 保留峰位/峰形
        /// </summary>
        internal static double[] SavitzkyGolaySmooth(double[] data, int windowSize = 9)
        {
            if (data == null || data.Length == 0) return data;
            if (data.Length < windowSize) return (double[])data.Clone();
            if (windowSize % 2 == 0) windowSize++;

            if (!SGCoefficients.ContainsKey(windowSize))
                return GaussianSmooth(data, windowSize);

            double[] coeffs = SGCoefficients[windowSize];
            double divisor = SGDivisor[windowSize];
            double[] kernel = coeffs.Select(c => c / divisor).ToArray();
            return Convolve(data, kernel);
        }

        /// <summary>
        /// 高斯卷积平滑 — 视觉上最自然
        /// 默认 sigma = windowSize/4 (钟形集中, 只消高频, 主峰不变形)
        /// </summary>
        internal static double[] GaussianSmooth(double[] data, int windowSize = 21, double? sigma = null)
        {
            if (data == null || data.Length == 0) return data;
            if (windowSize % 2 == 0) windowSize++;
            if (!sigma.HasValue) sigma = windowSize / 4.0;
            double[] kernel = BuildGaussianKernel(windowSize, sigma.Value);
            return Convolve(data, kernel);
        }

        internal static double[] MovingAverageSmooth(double[] data, int windowSize = 15)
        {
            if (data == null || data.Length == 0) return data;
            if (windowSize % 2 == 0) windowSize++;
            int half = windowSize / 2;
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                double sum = 0.0;
                int count = 0;
                for (int j = Math.Max(0, i - half); j <= Math.Min(data.Length - 1, i + half); j++)
                {
                    sum += data[j]; count++;
                }
                result[i] = sum / count;
            }
            return result;
        }

        /// <summary>
        /// 三重平滑 — 显示层专用
        /// 1. SG(w=9 温和) — 锁定峰位/峰高, 不用大窗口防 ringing
        /// 2. 高斯(w=15, σ=w/4) — 第一轮压锯齿
        /// 3. 高斯(w=15, σ=w/4) — 第二轮彻底磨平 (σ适中, 主峰不变形)
        /// </summary>
        internal static double[] TriplePassSmooth(double[] spectrum, int sgWindow = 9, int gaussWindow = 15)
        {
            if (spectrum == null || spectrum.Length == 0) return spectrum;
            double[] pass1 = SavitzkyGolaySmooth(spectrum, sgWindow);
            double[] pass2 = GaussianSmooth(pass1, gaussWindow);   // 默认 σ=w/4, 主峰不变形
            double[] pass3 = GaussianSmooth(pass2, gaussWindow);   // 默认 σ=w/4
            return pass3;
        }

        /// <summary>
        /// 光谱一站式平滑入口 — 默认三重平滑 (峰位准 + 彻底无锯齿)
        /// </summary>
        /// <param name="wavelength">波长数组 (不变)</param>
        /// <param name="spectrum">幅值数组 (会被平滑)</param>
        /// <param name="mode">
        ///   0 = 三重平滑 (SG + 高斯 + 高斯) — 推荐
        ///   1 = 仅 SG (锁峰位, 锯齿还在)
        ///   2 = 仅高斯 (彻底平滑, 峰位略漂)
        ///   3 = 迭代高斯 (3 次小窗口, 边界效果好)
        ///   4 = 移动平均
        /// </param>
        /// <param name="strength">0=轻度 1=推荐 2=强力 (影响窗口大小)</param>
        internal static double[] SmoothSpectrum(double[] wavelength, double[] spectrum, int mode = 0, int strength = 1)
        {
            if (spectrum == null || spectrum.Length == 0) return spectrum;

            // 根据强度算窗口 (SG 始终温和, 高斯按强度放大)
            int sgW = 9;
            int gsW = strength == 0 ? 11 : strength == 1 ? 15 : 19;

            switch (mode)
            {
                case 1: return SavitzkyGolaySmooth(spectrum, sgW);
                case 2: return GaussianSmooth(spectrum, gsW);
                case 3: return IteratedGaussian(spectrum, 11, 11 / 4.0, iterations: strength == 2 ? 4 : 3);
                case 4: return MovingAverageSmooth(spectrum, gsW);
                default: return TriplePassSmooth(spectrum, sgW, gsW); // 0: 三重 (推荐)
            }
        }

        #endregion

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
            for(int i=0;i<livList.Count;i++)
            {
                if (livList[i].LDI < 0.5)
                    continue;
                for(int j=i+1;j<livList.Count;j++)
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

            for(int i=idxBaseEnd+1;i<dVdI_1st.Count;i++)
            {
                if (dVdI_1st[i] > 3 * dVDiBase || dVdI_1st[i] < dVDiBase / 3)
                    return false;
            }

            return true;
        }
    }
}
