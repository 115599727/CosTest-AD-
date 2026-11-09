using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CosTest
{
    class BeamCam
    {
        /// <summary>
        /// CONNECTION
        /// </summary>
        internal bool CONNECTED { get; private set; }
        /// <summary>
        /// 视频提取
        /// </summary>
        internal VideoCapture VideoCapt;

        /// <summary>
        /// 打开设备
        /// </summary>
        internal void Open()
        {
            // 检测相机
            for (int index = 0; index < 3; index++)
            {
                try
                {
                    //打开相机
                    this.VideoCapt = new VideoCapture(index);
                    this.VideoCapt.Set(3, 1920);   // 设置宽度为1920像素
                    this.VideoCapt.Set(4, 1080);   // 设置高度为1080像素
                    this.VideoCapt.Set(10, 70);    // 设置亮度为70
                    if (this.VideoCapt.IsOpened())
                    {
                        Mat mat = new Mat();
                        if(this.VideoCapt.Read(mat))
                        {
                            this.CONNECTED = true;
                            return;
                        }
                        else
                        {
                            this.VideoCapt.Release();
                        }
                    }
                }
                catch(Exception ex)
                {
                    ErrLog.WriteLine(ex.ToString());
                }
            }
            // 连接失败
            this.CONNECTED = false;
            return;


        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        internal void Close()
        {
            try
            {
                //this.VideoCapt.Dispose();
                this.CONNECTED = false;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLine(ex.ToString());
                this.CONNECTED = false;
            }
        }

        /// <summary>
        /// 抓取帧图像
        /// </summary>
        /// <returns></returns>
        internal Mat ReadFrame()
        {
            if (this.CONNECTED == false)
                return null;
            Mat frame = new Mat();
            if (this.VideoCapt.Read(frame))    // 抓取和解码，返回下一帧
                return frame;
            else
                return null;
        }
    }
}
