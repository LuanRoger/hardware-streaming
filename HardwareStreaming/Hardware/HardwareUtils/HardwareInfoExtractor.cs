using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Models;
using HardwareStreaming.Internals.Loggin;
using LibreHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware.HardwareUtils;

public class HardwareInfoExtractor : IDisposable
{
    private Computer mainComputer { get; }
    private ILogger _logger { get; }

    public HardwareInfoExtractor(Computer mainComputer, ILogger logger)
    {
        this.mainComputer = mainComputer;
        _logger = logger;
    }
    
    public void UpdateComputerComponents() => mainComputer.UpdateAllComponents();
    
    public Dictionary<string, float> GetSensorInfos(HardwareCatagory hardwareCatagory)
    {
        Dictionary<string, float> nameValueSensor = new();

        switch (hardwareCatagory)
        {
            case HardwareCatagory.Cpu:
                if(mainComputer.cpu is null) break;
                
                foreach (ISensor sensor in mainComputer.cpu.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -1.0f); }
                    catch(Exception e) { _logger.LogWarning(e.Message); }
                }
                break;
            case HardwareCatagory.Mainboard:
                if(mainComputer.mainboard is null) break;
                
                foreach (ISensor sensor in mainComputer.mainboard.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -1.0f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Gpu:
                if(mainComputer.gpu is null) break;
                
                foreach (ISensor sensor in mainComputer.gpu.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -1.0f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Network:
                if(mainComputer.network is null) break;
                
                foreach (ISensor sensor in mainComputer.network.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -1.0f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.FanController:
                if(mainComputer.fanController is null) break;
                
                foreach (ISensor sensor in mainComputer.fanController.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -1.0f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Ram:
                if(mainComputer.ram is null) break;
                
                foreach (ISensor sensor in mainComputer.ram.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -0.1f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Hdd:
                if(mainComputer.hdd is null) break;
                
                foreach (ISensor sensor in mainComputer.hdd.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, sensor.Value ?? -0.1f); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            default:
                _logger.LogError($"{nameof(hardwareCatagory)} is null");
                break;
        }
        
        return nameValueSensor; 
    }
    
    public void Dispose()
    {
        mainComputer.Dispose();
    }
}