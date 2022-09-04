using HardwareStreaming.Hardware.Models;
using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.HardwareLog;

public interface IComponentLog
{
    void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos);
}