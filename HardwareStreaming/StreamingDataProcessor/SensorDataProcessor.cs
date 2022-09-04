using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Models;

namespace HardwareStreaming.StreamingDataProcessor;

public class SensorDataProcessor : ISensorDataProcessor
{
    public float ProcessTemperatureData(TemperatureSensorInfo temperatureSensorInfo)
    {
        switch (temperatureSensorInfo.temperatureUnit)
        {
            case TemperatureUnit.K:
                return temperatureSensorInfo.absoluteValue + 273.15f;
            case TemperatureUnit.F:
                return temperatureSensorInfo.absoluteValue * 1.8f + 32;
            //Default is Celcius
            case TemperatureUnit.C:
            default:
                return temperatureSensorInfo.absoluteValue;
        }
    }
}