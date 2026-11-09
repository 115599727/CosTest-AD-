using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    /// <summary>
    /// Meerstetter TEC source function mode.
    /// </summary>
    public enum MeerstetterTECSourceFunction : int
    {
        StaticMode = 0,
        LiveMode = 1,
        TemperatureControllerMode = 2
    }
}
