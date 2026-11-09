using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosTest
{
    //Command for Static
    public enum MeerstetterTECCommands : int
    {
        GetDeviceType = 100,
        GetSerialNumber = 102,
        DeviceStatus = 104,
        OutputEnabled = 2010,
        SetTemperatureSetPoint = 3000,
        GetTemperatureSetPoint = 1010,
        GetTemperature = 1000,
        GetTECCurrent = 1020,
        GetTECVoltage = 1021,
        GetDeviceStatus = 104,
        Mode = 2000,
        GetStableStatus = 1200,
        CurrentProtectionLimit = 2030,
        VoltageProtectionLimit = 2031,
        PIDGain = 3010,
        PIDIntegral = 3011,
        PIDDerivative = 3012,
        TemperatureSourceMode = 50011,

        /// <summary>
        /// Object Temperature Stability Indicator Settings
        /// Temperature Deviation
        /// </summary>
        TemperatureDeviation = 4040,
        MinTimeInWindow = 4041,

        UpperErrorThreshold = 5011,
        LowerErrorThreshold = 5010,

        MaxStabilizationTime = 4042,
    }
}
