using HardwareStreaming.Enums;
using HardwareStreaming.StreamingDataProcessor;

namespace HardwareStreaming.Hardware.Models;

public class TemperatureSensorInfo : SensorInfo, ISensorProcessableValue
{
    public TemperatureUnit temperatureUnit { get; }
    public float absoluteValue { get; }

    public override float value => ProcessValue(new SensorDataProcessor());

    public TemperatureSensorInfo(string name, float value, SensorDataType sensorDataType,
        TemperatureUnit temperatureUnit) : base(name, value, sensorDataType)
    {
        this.temperatureUnit = temperatureUnit;
        absoluteValue = value;
    }

    public float ProcessValue(ISensorDataProcessor dataProcessor) => dataProcessor.ProcessTemperatureData(this);
}