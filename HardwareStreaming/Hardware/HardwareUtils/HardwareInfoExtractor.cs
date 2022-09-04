using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Models;
using HardwareStreaming.Internals.Loggin;
using LibreHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware.HardwareUtils;

public class HardwareInfoExtractor : IDisposable
{
    private Computer mainComputer { get; }
    private ILogger _logger { get; }
    private TemperatureUnit sensorDefaultTemperature { get; }

    public HardwareInfoExtractor(Computer mainComputer, TemperatureUnit sensorDefaultTemperature, ILogger logger)
    {
        this.mainComputer = mainComputer;
        this.sensorDefaultTemperature = sensorDefaultTemperature;
        _logger = logger;
    }
    
    public void UpdateComputerComponents() => mainComputer.UpdateAllComponents();
    
    public SensorInfo[] GetSensorInfos(HardwareCatagory hardwareCatagory)
    {
        List<SensorInfo> nameValueSensor = new();

        switch (hardwareCatagory)
        {
            case HardwareCatagory.Cpu:
                if(mainComputer.cpu is null) break;
                
                foreach (ISensor sensor in mainComputer.cpu.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            case HardwareCatagory.Mainboard:
                if(mainComputer.mainboard is null) break;
                
                foreach (ISensor sensor in mainComputer.mainboard.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            case HardwareCatagory.Gpu:
                if(mainComputer.gpu is null) break;
                
                foreach (ISensor sensor in mainComputer.gpu.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                break;
            case HardwareCatagory.Network:
                if(mainComputer.network is null) break;
                
                foreach (ISensor sensor in mainComputer.network.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            case HardwareCatagory.FanController:
                if(mainComputer.fanController is null) break;
                
                foreach (ISensor sensor in mainComputer.fanController.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            case HardwareCatagory.Ram:
                if(mainComputer.ram is null) break;
                
                foreach (ISensor sensor in mainComputer.ram.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            case HardwareCatagory.Hdd:
                if(mainComputer.hdd is null) break;
                
                foreach (ISensor sensor in mainComputer.hdd.sensors)
                    nameValueSensor.Add(CreateConcreateSensor(sensor));
                
                break;
            default:
                _logger.LogError($"{nameof(hardwareCatagory)} is null");
                break;
        }
        
        return nameValueSensor.ToArray(); 
    }
    
    private SensorInfo CreateConcreateSensor(ISensor sensor)
    {
        if(sensor.SensorType == SensorType.Temperature)
            return new TemperatureSensorInfo(sensor.Name, sensor.Value ?? -1.0f,
                (SensorDataType)(int)sensor.SensorType, sensorDefaultTemperature);
        
        //Default generic sensor
        return new(sensor.Name, sensor.Value ?? -1.0f,
            (SensorDataType)(int)sensor.SensorType);
    }
    
    public void Dispose()
    {
        mainComputer.Dispose();
    }
}