using HardwareStreaming.Hardware.Models;
using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.HardwareLog;

public class CpuLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===CPU======================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class MainboardLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===Mainboard================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class GpuLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===GPU======================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class NetworkLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===Network==================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class FanContollerLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===Fan Controller===========");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class RamLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===RAM======================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");
        logger.LogInformation("============================");
    }
}
public class HddLog : IComponentLog
{
    public void Log(ILogger logger, IEnumerable<SensorInfo> sensorInfos)
    {
        logger.LogInformation("===HDD======================");
        foreach (SensorInfo sensorInfo in sensorInfos)
            logger.LogInformation($"{sensorInfo.name}: {sensorInfo.value} ({sensorInfo.sensorDataType})");

        logger.LogInformation("============================");
    }
}