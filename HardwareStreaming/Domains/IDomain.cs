using HardwareStreaming.Loggin;

namespace HardwareStreaming.Domains;

public interface IDomain
{
    void StreamInfo(KeyValuePair<string, double> idValuePair, ILogger logger, int flushTimeout);
}