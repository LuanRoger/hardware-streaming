using HardwareStreaming.Enums;

namespace HardwareStreaming.Configuration.Models;

public class YamlConfigurationFile
{
    public KafkaDomainConfiguration kafkaDomainConfiguration { get; set; }
    public List<HardwareCatagory> hardwareMonitoring { get; set; }
    public int delayStreamTime { get; set; } = 1000;
}