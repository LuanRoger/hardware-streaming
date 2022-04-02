using HardwareStreaming.Domains;
using HardwareStreaming.Loggin;
using HardwareStreaming.Loggin.HardwareLog;

namespace HardwareStreaming;

public class HardwareStreamer
{
    public List<IComponentLog> componentsLog { get; }
    private Computer _computer { get; }
    private IDomain _domain { get; }
    private ILogger _logger { get; }

    public HardwareStreamer(ILogger logger, Computer computer, List<IComponentLog> componentsLog, IDomain domain)
    {
        _logger = logger;
        _computer = computer;
        this.componentsLog = componentsLog;
        _domain = domain;
    }

    public void PulseStream(int flushTimeout)
    {
        foreach (IComponentLog component in componentsLog)
        {
            component.Log(_logger, _computer, out var toStream);
            foreach (var sensorInfos in toStream)
                _domain.StreamInfo(sensorInfos, _logger, flushTimeout);
        }
    }
}