using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    class OcOsa
    {
        /// <summary>
        /// 设备连接
        /// </summary>
        private bool connected;
        /// <summary>
        /// 设备连接
        /// </summary>
        internal bool CONNECTED { get { return this.connected; } }
        /// <summary>
        /// 设备索引
        /// </summary>
        private int SpectrometerIndex;
        /// <summary>
        /// 设备序列号
        /// </summary>
        internal string SerialNumber { get; set; }
        /// <summary>
        /// 像素数
        /// </summary>
        private int NumberOfPixels;
        /// <summary>
        /// 积分时间
        /// </summary>
        internal double IntegrationTimeMillisec { get; set; }
        /// <summary>
        /// 是否自动积分时间
        /// </summary>
        internal bool IsAutoIntegrationTime { get; set; }
        /// <summary>
        /// 平滑宽度
        /// </summary>
        internal int BoxcarWidth { get; set; }
        /// <summary>
        /// 波长OFFSET
        /// </summary>
        internal double WavelengthOffset { get; set; }
        /// <summary>
        /// 波长OFFSET
        /// </summary>
        internal double SpectrumNoise { get; set; }
        /// <summary>
        /// 波长OFFSET
        /// </summary>
        internal bool SpectrumDeNoiseEnable { get; set; }
        /// <summary>
        /// 扫描平均次数
        /// </summary>
        internal int NumberOfScansToAverage { get; set; }
        /// <summary>
        /// 起始波长
        /// </summary>
        internal double StartWavelength { get; set; }
        /// <summary>
        /// 结束波长
        /// </summary>
        internal double StopWavelength { get; set; }
        /// <summary>
        /// PIB总范围
        /// </summary>
        internal double PibTotalHalfSpan { get; set; }
        /// <summary>
        /// PIB中心范围
        /// </summary>
        internal double PibCenterHalfSpan { get; set; }
        /// <summary>
        /// 是否采用高斯拟合
        /// </summary>
        internal bool IsGaussFit { get; set; }

        /// <summary>
        /// 最大积分时间
        /// </summary>
        internal int MaximumIntegrationTime;
        /// <summary>
        /// 最小积分时间
        /// </summary>
        internal int MinimumIntegrationTime;
        /// <summary>
        /// 最大强度
        /// </summary>
        internal int MaximumIntensity;

        /// <summary>
        /// OmniDriver.CCoWrapper
        /// </summary>
        private OmniDriver.CCoWrapper wrapper;

        internal OcOsa()
        {
            this.SpectrometerIndex = 0;
            this.IntegrationTimeMillisec = 100;
            this.IsAutoIntegrationTime = false;
            this.BoxcarWidth = 10;
            this.WavelengthOffset = 0.0;
            this.SpectrumNoise = 0.0;
            this.SpectrumDeNoiseEnable = false;
            this.NumberOfScansToAverage = 1;
            this.StartWavelength = 700.0;
            this.StopWavelength = 1000.0;
            this.PibTotalHalfSpan = 10;
            this.PibCenterHalfSpan = 2;
            this.IsGaussFit = false;

            this.wrapper = new OmniDriver.CCoWrapper();

        }

        /// <summary>
        /// 打开设备
        /// </summary>
        internal void Open()
        {
            try
            {
                // Open All Spectrometers
                int numberOfSpectrometersFound = this.wrapper.openAllSpectrometers();
                if (numberOfSpectrometersFound < 1)
                {
                    this.connected = false;
                    return;
                }
                // Read Device Info
                this.SerialNumber = this.wrapper.getSerialNumber(this.SpectrometerIndex);
                this.NumberOfPixels = this.wrapper.getNumberOfPixels(this.SpectrometerIndex);
                this.MaximumIntegrationTime = this.wrapper.getMaximumIntegrationTime(this.SpectrometerIndex);
                this.MinimumIntegrationTime = this.wrapper.getMinimumIntegrationTime(this.SpectrometerIndex);
                this.MaximumIntensity = this.wrapper.getMaximumIntensity(this.SpectrometerIndex);
                this.connected = true;
            }
            catch(System.Exception ex)
            {
                this.connected = false;
                ErrLog.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        internal void Close()
        {
            try
            {
                // Close Spectrometers
                this.wrapper.closeAllSpectrometers();
                this.connected = false;
            }
            catch (System.Exception ex)
            {
                this.connected = false;
                ErrLog.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 读取序列号
        /// </summary>
        internal void GetSerialNumber()
        {
            this.wrapper.getSerialNumber(this.SpectrometerIndex);
        }

        /// <summary>
        /// 检查连接
        /// </summary>
        internal void CheckConnection()
        {
            try
            {
                if (this.connected)
                    this.wrapper.getSerialNumber(this.SpectrometerIndex);
            }
            catch
            {
                this.connected = false;
            }
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        internal void UpdateSetting()
        {
            // Setting
            this.wrapper.setScansToAverage(this.SpectrometerIndex, this.NumberOfScansToAverage);
            this.wrapper.setCorrectForElectricalDark(this.SpectrometerIndex, 1);
            this.wrapper.setCorrectForDetectorNonlinearity(this.SpectrometerIndex, 1);
            this.wrapper.setBoxcarWidth(this.SpectrometerIndex, this.BoxcarWidth);
            if(!this.IsAutoIntegrationTime)
                this.wrapper.setIntegrationTime(this.SpectrometerIndex, (int)(this.IntegrationTimeMillisec * 1000));
        }

        /// <summary>
        /// 获取光谱数据
        /// </summary>
        /// <returns></returns>
        internal StOcSpData GetSpectrumData()
        {
            #region 读取数据
            // Reading
            double[] spectrum_dev = this.wrapper.getSpectrum(this.SpectrometerIndex);
            double[] wavelength_dev = this.wrapper.getWavelengths(this.SpectrometerIndex);
            for (int i = 0; i < wavelength_dev.Length; i++)
                wavelength_dev[i] += this.WavelengthOffset;

            // Extract the desired wavelength
            int startWavelengthIndex = 0;
            for (int i = 0; i < wavelength_dev.Length; i++)
            {
                if (wavelength_dev[i] < this.StartWavelength)
                    startWavelengthIndex++;
                else
                    break;
            }
            int stopWavelengthIndex = wavelength_dev.Length - 1;
            for (int i = wavelength_dev.Length - 1; i >= 0; i--)
            {
                if (wavelength_dev[i] > this.StopWavelength)
                    stopWavelengthIndex--;
                else
                    break;
            }
            if (startWavelengthIndex >= stopWavelengthIndex)
                return new StOcSpData();
            // Copy Array
            double[] spectrum = new double[stopWavelengthIndex - startWavelengthIndex + 1];
            double[] wavelength = new double[stopWavelengthIndex - startWavelengthIndex + 1];
            for (int i = 0; i < spectrum.Length; i++)
            {
                spectrum[i] = Math.Round(spectrum_dev[i + startWavelengthIndex], 3);
                wavelength[i] = Math.Round(wavelength_dev[i + startWavelengthIndex], 3);
            }
            // De Noise
            if (this.SpectrumDeNoiseEnable)
            {
                List<double> bkNoise = new List<double>();
                // First & Last 5nm
                for (int i = 0; i < wavelength.Length; i++)
                {
                    if (wavelength[i] - wavelength[0] > 5 || spectrum[i] > 500)
                        break;
                    bkNoise.Add(spectrum[i]);
                }
                for (int i = wavelength.Length - 1; i >= 0; i--)
                {
                    if (wavelength[wavelength.Length - 1] - wavelength[i] > 5 || spectrum[i] > 500)
                        break;
                    bkNoise.Add(spectrum[i]);
                }

                double mean, stdev;
                this.StandardVariance(bkNoise, out mean, out stdev);

                double offset = mean + stdev * this.SpectrumNoise;
                for (int i = 0; i < spectrum.Length; i++)
                {
                    spectrum[i] = Math.Round(spectrum[i] - offset, 3);
                    if (spectrum[i] < 0)
                        spectrum[i] = 0;
                }
            }
            #endregion


            #region 高斯拟合
            if(this.IsGaussFit)
            {
                int idxSpanStart = 0;
                int idxSpanEnd = spectrum.Length - 1;
                //for (; idxSpanStart < spectrum.Length; idxSpanStart++)
                //{
                //    if (spectrum[idxSpanStart] - spectrum[0] >= 100.0)
                //        break;
                //}
                //for (; idxSpanEnd >= 0; idxSpanEnd--)
                //{
                //    if (spectrum[idxSpanEnd] - spectrum[spectrum.Length - 1] >= 100.0)
                //        break;
                //}

                if(idxSpanEnd - idxSpanStart > 10)
                {
                    idxSpanStart = idxSpanStart - 30 < 0 ? 0 : idxSpanStart - 30;
                    idxSpanEnd = idxSpanEnd + 30 > spectrum.Length - 1 ? spectrum.Length - 1 : idxSpanEnd + 30;

                    double[] spectrumSpan = new double[idxSpanEnd - idxSpanStart + 1];
                    double[] wavelengthSpan = new double[idxSpanEnd - idxSpanStart + 1];
                    for (int i = 0; i < spectrumSpan.Length; i++)
                    {
                        spectrumSpan[i] = spectrum[i + idxSpanStart];
                        wavelengthSpan[i] = wavelength[i + idxSpanStart];
                    }

                    double xc, w, a;
                    PyGaussCurve gsAmp = new PyGaussCurve();
                    gsAmp.Fit(wavelengthSpan, spectrumSpan, out xc, out w, out a);

                    for (int i = 0; i < wavelength.Length; i++)
                    {
                        spectrum[i] = a * Math.Exp(-(wavelength[i] - xc) * (wavelength[i] - xc) / (2 * w * w));
                        spectrum[i] = spectrum[i] < 0.0 ? 0.0 : spectrum[i];
                        spectrum[i] = Math.Round(spectrum[i], 3);
                    }
                }
            }
            #endregion


            #region 计算参数
            // Primary Peak — sliding window local maximum above baseline
            double baseline = 100.0;
            int minimumIndicesBetweenPeaks = 100;
            int startingIndex = 0;
            int primaryPeakIndex = 0;
            int secondPeakIndex = 0;
            int idx;
            do
            {
                idx = FindNextPeakIndex(spectrum, startingIndex, minimumIndicesBetweenPeaks, baseline);
                if (idx == -1)
                    break;

                if (spectrum[idx] > spectrum[primaryPeakIndex])
                    primaryPeakIndex = idx;
                else if (spectrum[idx] > spectrum[secondPeakIndex])
                    secondPeakIndex = idx;

                startingIndex = idx + 1;
            }
            while (true);


            // Peak Parameters
            double centerWavelength = wavelength[primaryPeakIndex];
            double centroid = ComputeCentroid(wavelength, spectrum, primaryPeakIndex, 5);
            double FWHM = ComputeFWHM(wavelength, spectrum, primaryPeakIndex);
            if (double.IsNaN(centerWavelength))
                centerWavelength = -9999.0;
            if (double.IsNaN(centroid))
                centroid = -9999.0;
            if (double.IsNaN(FWHM))
                FWHM = -9999.0;

            // Data Result
            StOcSpData spectrumData = new StOcSpData();
            spectrumData.Spectrum = spectrum;
            spectrumData.Wavelength = wavelength;
            spectrumData.FHWM = FWHM;
            double primaryPeakValue = spectrum[primaryPeakIndex] > 1 ? spectrum[primaryPeakIndex] : 1;
            double secondPeakValue = spectrum[secondPeakIndex] > 1 ? spectrum[secondPeakIndex] : 1;
            spectrumData.SNR = 10 * Math.Log10(primaryPeakValue / secondPeakValue);
            spectrumData.CenterWaveLength = centerWavelength;
            spectrumData.Centroid = centroid;
            spectrumData.PeakWaveLength = wavelength[primaryPeakIndex];
            spectrumData.PIB = this.CalcPIB(centerWavelength, wavelength, spectrum);
            return spectrumData;
            #endregion
        }

        /// <summary>
        /// 计算PIB
        /// </summary>
        /// <param name="centerWavelength"></param>
        /// <returns></returns>
        internal double CalcPIB(double centerWavelength, double[] wavelength, double[] spectrum)
        {
            double pib = 0.0;
            if (wavelength == null || wavelength.Length < 3)
                return pib;
            if (spectrum == null || spectrum.Length < 3)
                return pib;

            int idxSpanFullStart = wavelength.Length - 1, idxSpanFullEnd = 0;
            int idxSpanCenterStart = wavelength.Length - 1, idxSpanCenterEnd = 0;
            
            // 左边界
            int i = 0;
            for(;i<wavelength.Length;i++)
            {
                if(wavelength[i] > centerWavelength - this.PibTotalHalfSpan)
                {
                    idxSpanFullStart = i;
                    break;
                }
            }
            for (; i < wavelength.Length; i++)
            {
                if (wavelength[i] > centerWavelength - this.PibCenterHalfSpan)
                {
                    idxSpanCenterStart = i;
                    break;
                }
            }
            // 右边界
            i = wavelength.Length - 1;
            for (; i >= 0; i--)
            {
                if (wavelength[i] < centerWavelength + this.PibTotalHalfSpan)
                {
                    idxSpanFullEnd = i;
                    break;
                }
            }
            for (; i >= 0; i--)
            {
                if (wavelength[i] < centerWavelength + this.PibCenterHalfSpan)
                {
                    idxSpanCenterEnd = i;
                    break;
                }
            }

            // 判断边界是否正确
            if(idxSpanFullEnd <= idxSpanFullStart || idxSpanCenterEnd <= idxSpanCenterStart)
                return pib;

            // 积分
            double engFull = 0, engCenter = 0;
            for( i= idxSpanFullStart;i<= idxSpanFullEnd;i++)
            {
                if (spectrum[i] > 0)
                    engFull += spectrum[i];
            }
            if (engFull <= 0)
                return pib;
            for (i = idxSpanCenterStart; i <= idxSpanCenterEnd; i++)
            {
                if (spectrum[i] > 0)
                    engCenter += spectrum[i];
            }
            // 计算
            pib = engCenter / engFull * 100;
            return pib;
        }

        /// <summary>
        /// 自动积分时间
        /// </summary>
        /// <param name="TARGET_COUNTS"></param>
        /// <param name="TARGET_COUNT_ERROR_MARGIN"></param>
        /// <returns></returns>
        private int AutoIntegrationTimeMicrosec()
        {
            int MAX_TRIES = 15;
            double TARGET_COUNTS = this.MaximumIntensity * 0.5;
            double TARGET_COUNT_ERROR_MARGIN = 0.075;

            int MAX_SANE_INTEGRATION_TIME_MICROSEC = (int)(TestSystemUtil.MaxAutoIntegrationTimeMilliSec * 1000);
            int DEFAULT_INTEGRATION_TIME_MICROSEC = (int)(TestSystemUtil.RefIntegrationTimeMilliSec * 1000);
            int TARGET_COUNT_TOLERANCE = (int)(TARGET_COUNTS * TARGET_COUNT_ERROR_MARGIN);
            double SATURATION_COUNT = this.MaximumIntensity * 0.99;
            double THRESHOLD_COUNT = this.MaximumIntensity * 0.05;
            int lowerPowerCount = 0;

            // start search at 100 milliseconds
            int currentIntegrationTimeMicrosec = DEFAULT_INTEGRATION_TIME_MICROSEC;
            wrapper.setIntegrationTime(this.SpectrometerIndex, currentIntegrationTimeMicrosec);

            double maxCounts = 0;
            bool found = false;
            int prevIntegrationTimeMillisec = currentIntegrationTimeMicrosec;

            for (int pass = 0; !found && pass < MAX_TRIES; pass++)
            {
                double[] spectrum = wrapper.getSpectrum(this.SpectrometerIndex);

                maxCounts = 5;
                for (int i = 20; i < spectrum.Length - 20; i++) // igore the dummy pixels at 2 ends
                    maxCounts = spectrum[i] > maxCounts ? spectrum[i] : maxCounts;
                    

                if (maxCounts >= (TARGET_COUNTS - TARGET_COUNT_TOLERANCE) &&
                    maxCounts <= (TARGET_COUNTS + TARGET_COUNT_TOLERANCE))
                {
                    found = true;
                    break;
                }

                if(maxCounts < THRESHOLD_COUNT) // No Power
                {
                    lowerPowerCount++;
                    if (lowerPowerCount >= 3)
                        break;
                    else
                    currentIntegrationTimeMicrosec = (int)(currentIntegrationTimeMicrosec * 5);
                }
                //if (maxCounts > TARGET_COUNTS + TARGET_COUNT_TOLERANCE)
                else if(maxCounts >= SATURATION_COUNT)
                {
                    currentIntegrationTimeMicrosec = (int)(currentIntegrationTimeMicrosec * 0.3);
                    lowerPowerCount = 0;
                }
                else
                {
                    double ratio = 1.0 * TARGET_COUNTS / maxCounts;
                    currentIntegrationTimeMicrosec = (int)(currentIntegrationTimeMicrosec * ratio);
                    lowerPowerCount = 0;
                }

                // sanity-check 
                if (currentIntegrationTimeMicrosec > MAX_SANE_INTEGRATION_TIME_MICROSEC)
                {
                    currentIntegrationTimeMicrosec = MAX_SANE_INTEGRATION_TIME_MICROSEC;
                    if (currentIntegrationTimeMicrosec == prevIntegrationTimeMillisec)
                        break;
                }
                else if (currentIntegrationTimeMicrosec < 100)
                {
                    currentIntegrationTimeMicrosec = 100;
                    if (currentIntegrationTimeMicrosec == prevIntegrationTimeMillisec)
                        break;
                }

                prevIntegrationTimeMillisec = currentIntegrationTimeMicrosec;
                wrapper.setIntegrationTime(this.SpectrometerIndex, currentIntegrationTimeMicrosec);
            }

            //if (!found)
            //{
            //    // ERROR: unable to find good integration time for spectrometer {0} after {1} tries
            //    return DEFAULT_INTEGRATION_TIME_MILLISEC;
            //}
            //else
            //{
            //    // Optimal integration time for spectrometer {0} = {1} millisec
            //    return currentIntegrationTimeMillisec;
            //}

            //ErrLog.WriteLine("currentIntegrationTimeMicrosec=" + currentIntegrationTimeMicrosec + "; MaximumIntensity=" + MaximumIntensity + "; maxCounts=" + maxCounts + "; TARGET_COUNTS=" + TARGET_COUNTS + "; DEFAULT_INTEGRATION_TIME_MICROSEC=" + DEFAULT_INTEGRATION_TIME_MICROSEC + "; MaximumIntegrationTime=" + MaximumIntegrationTime + "; MinimumIntegrationTime=" + MinimumIntegrationTime);

            return currentIntegrationTimeMicrosec;
        }

        /// <summary>
        /// 设置积分时间
        /// </summary>
        internal void SetIntegrationTime()
        {
            if (this.IsAutoIntegrationTime)
            {
                // Clear ScansToAverage
                this.wrapper.setScansToAverage(this.SpectrometerIndex, 1);
                // Get Auto Integration Time
                int autoIntegrationTimeMicrosec = this.AutoIntegrationTimeMicrosec();
                // Set Integeration Time
                this.wrapper.setIntegrationTime(this.SpectrometerIndex, autoIntegrationTimeMicrosec);
                // Recover ScansToAverage
                this.wrapper.setScansToAverage(this.SpectrometerIndex, this.NumberOfScansToAverage);
            }
            else
            {
                // Set Integeration Time
                this.wrapper.setIntegrationTime(this.SpectrometerIndex, (int)(this.IntegrationTimeMillisec * 1000));
                // Recover ScansToAverage
                this.wrapper.setScansToAverage(this.SpectrometerIndex, this.NumberOfScansToAverage);
            }
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private void StandardVariance(IEnumerable<double> values, out double mean, out double stdev)
        {
            mean = 0;
            stdev = 0;
            if (values == null || !values.Any())
               return;

            double m = values.Average();
            mean = m;
            double var = values.Select(x => Math.Pow(x - m, 2)).Average();
            stdev = Math.Sqrt(var);
        }

        /// <summary>
        /// Sliding window local maximum finder. Returns index of next peak >= startingIndex
        /// whose value > baseline, or -1 if none found.
        /// </summary>
        private static int FindNextPeakIndex(double[] spectrum, int startingIndex, int minimumGap, double baseline)
        {
            if (spectrum == null || spectrum.Length < 3) return -1;
            int half = Math.Max(1, minimumGap / 2);
            for (int i = Math.Max(startingIndex, half); i < spectrum.Length - half; i++)
            {
                double v = spectrum[i];
                if (v <= baseline) continue;
                bool isPeak = true;
                for (int j = i - half; j <= i + half; j++)
                {
                    if (j != i && spectrum[j] > v) { isPeak = false; break; }
                }
                if (isPeak) return i;
            }
            return -1;
        }

        /// <summary>
        /// Weighted centroid: sum(wavelength * intensity) / sum(intensity),
        /// sampled in a ±window half-width around the peak.
        /// </summary>
        private static double ComputeCentroid(double[] wavelength, double[] spectrum, int peakIdx, int window)
        {
            double num = 0, den = 0;
            for (int i = Math.Max(0, peakIdx - window); i <= Math.Min(spectrum.Length - 1, peakIdx + window); i++)
            {
                num += wavelength[i] * spectrum[i];
                den += spectrum[i];
            }
            return den > 0 ? num / den : double.NaN;
        }

        /// <summary>
        /// FWHM: width (in wavelength units) at half of peak height, linear interpolation between bins.
        /// </summary>
        private static double ComputeFWHM(double[] wavelength, double[] spectrum, int peakIdx)
        {
            if (peakIdx < 0 || peakIdx >= spectrum.Length) return double.NaN;
            double half = spectrum[peakIdx] / 2.0;

            // Left side
            double leftWave = double.NaN;
            for (int i = peakIdx - 1; i >= 0; i--)
            {
                if (spectrum[i] <= half)
                {
                    // Linear interp between i and i+1
                    double x0 = spectrum[i], x1 = spectrum[i + 1];
                    double w0 = wavelength[i], w1 = wavelength[i + 1];
                    if (x1 - x0 > 0)
                        leftWave = w0 + (half - x0) * (w1 - w0) / (x1 - x0);
                    break;
                }
            }

            // Right side
            double rightWave = double.NaN;
            for (int i = peakIdx + 1; i < spectrum.Length; i++)
            {
                if (spectrum[i] <= half)
                {
                    double x0 = spectrum[i - 1], x1 = spectrum[i];
                    double w0 = wavelength[i - 1], w1 = wavelength[i];
                    if (x0 - x1 > 0)
                        rightWave = w1 - (half - x1) * (w1 - w0) / (x0 - x1);
                    break;
                }
            }

            if (double.IsNaN(leftWave) || double.IsNaN(rightWave)) return double.NaN;
            return rightWave - leftWave;
        }
    }
}
