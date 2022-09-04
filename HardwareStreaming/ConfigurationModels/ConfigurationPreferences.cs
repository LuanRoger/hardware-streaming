using HardwareStreaming.Enums;

namespace HardwareStreaming.ConfigurationModels;

public class ConfigurationPreferences
{
    public KafkaDomainConfiguration kafkaDomainConfiguration { get; set; }
    public HardwareMonitoringPreferences hardwarePreferences { get; set; }

    public static ConfigurationPreferences CreateConfigurationPreferencesDefaultFactory() => new()
    {
        kafkaDomainConfiguration = new()
        {
            bootstrapServer = "localhost:9092",
            clientId = "hardware_streaming",
            topic = "hardware_streaming"
        },
        hardwarePreferences = new()
        {
            hardwareMonitoring = new()
            {
                HardwareCatagory.Cpu,
                HardwareCatagory.Gpu,
                HardwareCatagory.Ram
            },
            temperatureUnit = TemperatureUnit.C
        }
    };
}