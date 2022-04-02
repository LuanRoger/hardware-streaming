using HardwareStreaming.Loggin;
using OpenHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware;

public class HardwareVisitor : IVisitor
{
    private ILogger _logger { get; }
    
    public HardwareVisitor(ILogger logger)
    {
        _logger = logger;
    }
    
    public void VisitComputer(IComputer computer)
    {
        _logger.LogInformation($"New computer visited: CPU: {computer.CPUEnabled}\nMainboard: {computer.MainboardEnabled}\n" +
                               $"GPU: {computer.GPUEnabled}\nNetwork: {computer.NetworkEnabled}\nFan Controller: {computer.FanControllerEnabled}\n" +
                               $"HDD: {computer.HDDEnabled}\nRAM: {computer.RAMEnabled}");
        computer.Traverse(this);
    }

    public void VisitHardware(IHardware hardware)
    {
        hardware.Update();
        
        _logger.LogInformation($"Hardware inited: {hardware.Name} - {hardware.Identifier}");
        
        hardware.Traverse(this);
    }

    public void VisitSensor(ISensor sensor)
    {
        _logger.LogInformation($"Sensor inited: {sensor.Name} - {sensor.Identifier}");
        
        sensor.Traverse(this);
    }

    public void VisitParameter(IParameter parameter)
    {
        _logger.LogInformation($"Parameter inited: {parameter.Name} - {parameter.Identifier} ({parameter.Description})");
        
        parameter.Traverse(this);
    }
}