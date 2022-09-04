using HardwareStreaming.Hardware.Models;

namespace HardwareStreaming.StreamingDataProcessor;

public interface ISensorDataProcessor
{ 
    public float ProcessTemperatureData(TemperatureSensorInfo temperatureSensorInfo);
}