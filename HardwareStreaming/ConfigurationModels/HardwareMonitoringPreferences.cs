using HardwareStreaming.Enums;

namespace HardwareStreaming.ConfigurationModels;

public class HardwareMonitoringPreferences
{
    public List<HardwareCatagory> hardwareMonitoring { get; set; }
    public TemperatureUnit temperatureUnit { get; set; }
}