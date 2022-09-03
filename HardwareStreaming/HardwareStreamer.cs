using Confluent.Kafka;
using HardwareStreaming.Domains;
using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.HardwareUtils;
using HardwareStreaming.HardwareLog;
using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming;

public class HardwareStreamer
{
    private Dictionary<HardwareCatagory, IComponentLog> _componentsLog { get; }
    private List<HardwareCatagory> _hardwareToStream { get; }
    private HardwareInfoExtractor _infoExtractor { get; }
    private KafkaDomain _domain { get; }
    private ILogger _logger { get; }

    public HardwareStreamer(ILogger logger, HardwareInfoExtractor infoExtractor,
        List<HardwareCatagory> hardwareToStream, KafkaDomain domain)
    {
        _logger = logger;
        _infoExtractor = infoExtractor;
        _hardwareToStream = hardwareToStream;
        _domain = domain;

        _componentsLog = new();
        InitComponentLoggers();
    }
    private void InitComponentLoggers()
    {
        foreach (HardwareCatagory hardwareCatagory in _hardwareToStream)
        {
            switch (hardwareCatagory)
            {
                case HardwareCatagory.Cpu:
                    _componentsLog.Add(HardwareCatagory.Cpu, new CpuLog());
                    break;
                case HardwareCatagory.Mainboard:
                    _componentsLog.Add(HardwareCatagory.Mainboard, new MainboardLog());
                    break;
                case HardwareCatagory.Gpu:
                    _componentsLog.Add(HardwareCatagory.Gpu, new GpuLog());
                    break;
                case HardwareCatagory.Network:
                    _componentsLog.Add(HardwareCatagory.Network, new NetworkLog());
                    break;
                case HardwareCatagory.FanController:
                    _componentsLog.Add(HardwareCatagory.FanController, new FanContollerLog());
                    break;
                case HardwareCatagory.Ram:
                    _componentsLog.Add(HardwareCatagory.Ram, new RamLog());
                    break;
                case HardwareCatagory.Hdd:
                    _componentsLog.Add(HardwareCatagory.Hdd, new HddLog());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }   
    }

    public void PulseStream()
    {
        using var producer = new ProducerBuilder<string, float>(_domain.producerConfig).Build();
        foreach (HardwareCatagory hardware in _hardwareToStream)
        {
            var sensorInfos = _infoExtractor.GetSensorInfos(hardware);
            
            _componentsLog[hardware].Log(_logger, sensorInfos);
            foreach (var sensorInfo in sensorInfos)
                _domain.StreamInfo(sensorInfo, in producer);
            
            producer.Flush();
        }
    }
}