using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRIP_API_LIB
{
    public class IR_JSON_LockTemp
    {
        public bool min_enable { get; set; } //开启低温锁定
        public bool max_enable { get; set; } //开启高温锁定
        public int type { get; set; } //锁定类型  0：平铺， 1：拉伸
        public bool auto_adjust { get; set; } //超限自适应
        public bool alert { get; set; } //超限报警
        public int lmin { get; set; } //低温锁定值
        public int lmax { get; set; } //高温锁定值
        public int tmin { get; set; } //低温输出
        public int tmax { get; set; } //高温输出
        public int pmin { get; set; } //调色板低温指示位置，0-255
        public int pmax { get; set; } //调色板高温指示位置，0-255

        public IR_JSON_LockTemp()
        {
            min_enable = false;
            max_enable = false;
            type = 0;
            auto_adjust = false;
            alert = false;
            lmin = 1500;
            lmax = 5000;
            tmin = 1500;
            tmax = 5000;
            pmin = 0;
            pmax = 0;
        }
    }
}
