using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    /// <summary>
    /// 控制器状态
    /// </summary>
    public enum DeviceStatus
    {
        Init = 0,
        Ready = 1,
        Run = 2,
        Error = 3,
        Bootloader = 4,
        UnderReset = 5,
    }
}
