using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.HardwareLog;

public class CpuLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===CPU======================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class MainboardLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===Mainboard================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class GpuLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===GPU======================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class NetworkLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===Network==================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class FanContollerLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===Fan Controller===========");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class RamLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===RAM======================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}
public class HddLog : IComponentLog
{
    public void Log(ILogger logger, Dictionary<string, float> toStream)
    {
        logger.LogInformation("===HDD======================");
        foreach ((string key, double value) in toStream)
            logger.LogInformation($"{key}: {value}");
        logger.LogInformation("============================");
    }
}