namespace HardwareStreaming.Loggin.HardwareLog;

public interface IComponentLog
{
    void Log(ILogger logger, Dictionary<string, float> toStream);
}