using HardwareStreaming.Internals.Loggin;
using LibreHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware;

public class HardwareVisitor : IVisitor
{
    private Logger _logger { get; }
    
    public HardwareVisitor(Logger logger)
    {
        _logger = logger;
    }
    
    public void VisitComputer(IComputer computer)
    {
        _logger.LogInformation($"New computer visited: CPU: {computer.IsCpuEnabled}\nMainboard: {computer.IsMotherboardEnabled}\n" +
                               $"GPU: {computer.IsGpuEnabled}\nNetwork: {computer.IsNetworkEnabled}\nFan Controller: {computer.IsControllerEnabled}\n" +
                               $"HDD: {computer.IsStorageEnabled}\nRAM: {computer.IsMemoryEnabled}");
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