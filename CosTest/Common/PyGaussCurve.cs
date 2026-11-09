using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CosTest
{
    class PyGaussCurve
    {
        /// <summary>
        /// 原始数据文件
        /// </summary>
        private const string DataFile = @"Fit\data.csv";
        /// <summary>
        /// 高斯拟合数据文件
        /// </summary>
        private const string SpGsFile = @"Fit\spgs.csv";
        /// <summary>
        /// py文件
        /// </summary>
        private const string SpFitFile = @"Fit\SpFit.py";

        public void Fit(double[] x, double[] y, out double xc, out double w, out double a)
        {
            xc = 0.0;
            w = 0.0;
            a = 0.0;

            if (x == null || x.Length == 0 || y == null || y.Length == 0)
                return;

            try
            {
                //获取程序的基目录 
                string baseDir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string fitDir = baseDir + "Fit";
                string dataFilePath = baseDir + DataFile;
                string spGsFilePath = baseDir + SpGsFile;
                string spFitFilePath = baseDir + SpFitFile;

                // 删除结果文件
                if (File.Exists(spGsFilePath))
                    File.Delete(spGsFilePath);

                // 输出数据文件
                FileStream fsData = new FileStream(dataFilePath, FileMode.Create);
                StreamWriter swData = new StreamWriter(fsData, Encoding.ASCII);

                string strLine = x[0].ToString();
                for (int i = 1; i < x.Length; i++)
                    strLine += "," + x[i];
                swData.WriteLine(strLine);

                strLine = y[0].ToString();
                for (int i = 1; i < y.Length; i++)
                    strLine += "," + y[i];
                swData.WriteLine(strLine);

                swData.Flush();
                swData.Close();
                fsData.Close();



                //调用py文件
                ProcessStartInfo processStartInfo = new ProcessStartInfo(spFitFilePath);
                processStartInfo.UseShellExecute = true;
                processStartInfo.WorkingDirectory = fitDir;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit(30000); // 等待进程结束

                //未找到文件
                if (File.Exists(spGsFilePath) == false)
                    return;

                //按行结果文件
                FileStream fsSpgs = new FileStream(spGsFilePath, FileMode.Open);//打开文件
                StreamReader srSpgs = new StreamReader(fsSpgs);
                srSpgs.BaseStream.Position = 0;//移动到开头
                a = Convert.ToDouble(srSpgs.ReadLine());
                xc = Convert.ToDouble(srSpgs.ReadLine());
                w = Convert.ToDouble(srSpgs.ReadLine());
                srSpgs.Close();
                fsSpgs.Close();
            }
            catch
            {
                xc = 0.0;
                w = 0.0;
                a = 0.0;
            }
        }
    }

    class GaussAmpTest
    {
        public void Do(string filePath, out double[] wavelength, out double[] spectrum)
        {
            wavelength = null;
            spectrum = null;

            //按行读取文件所有内容
            FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Position = 0;//移动到开头
            string strLine = "";

            //读取内容
            List<double> wlList = new List<double>();
            List<double> spList = new List<double>();
            while (!sr.EndOfStream)
            {
                strLine = sr.ReadLine();
                string[] strSegs = strLine.Split(new char[] { ',' });
                if(strSegs.Length >= 2)
                {
                    wlList.Add(Convert.ToDouble(strSegs[0]));
                    spList.Add(Convert.ToDouble(strSegs[1]));
                }
            }

            sr.Close();
            fs.Close();


            wavelength = wlList.ToArray();
            spectrum = spList.ToArray();


            if (true)
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

                if (idxSpanEnd - idxSpanStart > 10)
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

                    double xc,  w, a;
                    PyGaussCurve gsAmp = new PyGaussCurve();
                    gsAmp.Fit(wavelengthSpan, spectrumSpan, out xc, out w, out a);

                    for (int i = 0; i < wavelength.Length; i++)
                    {
                        spectrum[i] = a * Math.Exp(-(wavelength[i] - xc) * (wavelength[i] - xc) / (2 * w * w));
                        spectrum[i] = spectrum[i] < 0.0 ? 0.0 : spectrum[i];
                    }
                }
            }
            else
            {
                
            }

            //double xc, y0, w, A, fwhm, area, reducedChisq, adjRSq;
            //GaussAmp gsAmp = new GaussAmp();
            //gsAmp.Fit(wavelength.ToArray(), spectrum.ToArray(), out xc, out y0, out w, out A, out fwhm, out area, out reducedChisq, out adjRSq);

            //if (y0 > Math.Min(spectrum.First(), spectrum.Last()))
            //    y0 = Math.Min(spectrum.First(), spectrum.Last());

            //for (int i = 0; i < wavelength.Count; i++)
            //{
            //    spectrum[i] = y0 + A * Math.Exp(-(wavelength[i] - xc) * (wavelength[i] - xc) / (2 * w * w));
            //    spectrum[i] = spectrum[i] < 0.0 ? 0.0 : spectrum[i];

            //}
        }
    }

}
