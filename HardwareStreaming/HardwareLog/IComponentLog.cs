using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.HardwareLog;

public interface IComponentLog
{
    void Log(ILogger logger, Dictionary<string, float> toStream);
}