using HardwareStreaming.Enums;

namespace HardwareStreaming.Hardware.Models;

public class SensorInfo
{
    public string name { get; }
    public virtual float value { get; }
    public SensorDataType sensorDataType { get; }

    public SensorInfo(string name, float value, SensorDataType sensorDataType)
    {
        this.name = name;
        this.value = value;
        this.sensorDataType = sensorDataType;
    }
}