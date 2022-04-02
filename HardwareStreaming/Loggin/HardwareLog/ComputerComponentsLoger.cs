using HardwareStreaming.Enums;

namespace HardwareStreaming.Loggin.HardwareLog;

public class CpuLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===CPU======================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Cpu))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
        logger.LogInformation("============================");
    }
}
public class MainboardLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===CPU======================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Mainboard))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
        logger.LogInformation("============================");
    }
}
public class GpuLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===GPU======================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Gpu))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
        logger.LogInformation("============================");
    }
}
public class NetworkLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===Network==================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Network))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
            
        logger.LogInformation("============================");
    }
}
public class FanContollerLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===Fan Controller===========");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.FanController))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
            
        logger.LogInformation("============================");
    }
}
public class RamLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===RAM======================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Ram))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
        logger.LogInformation("============================");
    }
}
public class HddLog : IComponentLog
{
    public void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos)
    {
        logedInfos = new();
        
        logger.LogInformation("===HDD======================");
        foreach ((string key, double value) in computer.GetSensorInfos(HardwareCatagory.Hdd))
        {
            logger.LogInformation($"{key}: {value}");
            logedInfos.Add(key, value);
        }
            
        logger.LogInformation("============================");
    }
}