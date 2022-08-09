using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.HardwareLog;

public interface IComponentLog
{
    void Log(Logger logger, Dictionary<string, float> toStream);
}