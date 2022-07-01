using HardwareStreaming.Enums;
using HardwareStreaming.Hardware;
using LibreHardwareMonitor.Hardware;
using ILogger = HardwareStreaming.Loggin.ILogger;
// ReSharper disable InconsistentNaming

namespace HardwareStreaming;

public class Computer : IDisposable
{
    private LibreHardwareMonitor.Hardware.Computer _computer { get; }
    private ILogger _logger { get; set; }

    private CPU? _cpu { get; set; }
    private Mainboard? _mainboard { get; set; }
    private GPU? _gpu { get; set; }
    private Network? _network { get; set; }
    private EmbeddedController? _fanController { get; set; }
    private RAM? _ram { get; set; }
    private HDD? _hdd { get; set; }

    public Computer(ComputerConfiguration computerConfiguration, ILogger logger)
    {
        _computer = new()
        {
            IsCpuEnabled = computerConfiguration.initCpu,
            IsMotherboardEnabled = computerConfiguration.initMainboard,
            IsGpuEnabled = computerConfiguration.initGpu,
            IsNetworkEnabled = computerConfiguration.initNetwork,
            IsControllerEnabled = computerConfiguration.initFanController,
            IsStorageEnabled = computerConfiguration.initHdd,
            IsMemoryEnabled = computerConfiguration.initRam
        };
        _logger = logger;
        
        _computer.Open();
        HardwareVisitor hardwareVisitor = new(logger);
        _computer.Accept(hardwareVisitor);

        GetHardwares();
    }

    private void GetHardwares()
    {
        //CPU
        if(_computer.IsCpuEnabled)
        {
            IHardware? cpuHardware = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Cpu);
            if(cpuHardware is null)
                _logger.LogWarning($"{nameof(cpuHardware)} can't be found, so it's null");
            else _cpu = new(cpuHardware); 
        }
        
        //Mainboard
        if(_computer.IsMotherboardEnabled)
        {
            IHardware? mainboardHardware = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Motherboard);
            if(mainboardHardware is null)
                _logger.LogWarning($"{nameof(mainboardHardware)} can't be found, so it's null");
            else _mainboard = new(mainboardHardware);
        }
        
        //GPU
        if(_computer.IsGpuEnabled)
        {
            IHardware? gpuHardwareNvidia = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.GpuNvidia);
            IHardware? gpuHardwareIntel = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.GpuIntel);
            IHardware? gpuHardwareAmd = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.GpuAmd);

            if(gpuHardwareNvidia is null && gpuHardwareIntel is null && gpuHardwareAmd is null)
                _logger.LogWarning($"{nameof(gpuHardwareIntel)} or ${nameof(gpuHardwareAmd)} can't be found, so it's null");
            else
            {
                if(gpuHardwareNvidia is not null)
                    _gpu = new(gpuHardwareNvidia, GpuType.Nvidia);
                else if(gpuHardwareIntel is not null)
                    _gpu = new(gpuHardwareIntel, GpuType.Intel);
                else if(gpuHardwareAmd is not null) 
                    _gpu = new(gpuHardwareAmd!, GpuType.Amd);
            }
        }
        
        //Network
        if(_computer.IsNetworkEnabled)
        {
            IHardware? networkHardware = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Network);
            
            if(networkHardware is null)
                _logger.LogWarning($"{nameof(networkHardware)} can't be found, so it's null");
            else _network = new(networkHardware);
        }
        
        //FanController
        if(_computer.IsControllerEnabled)
        {
            IHardware? fanControllerHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.EmbeddedController);
            
            if(fanControllerHardware is null)
                _logger.LogWarning($"{nameof(fanControllerHardware)} can't be found, so it's null");
            else _fanController = new(fanControllerHardware);
        }
        
        //RAM
        if(_computer.IsMemoryEnabled)
        {
            IHardware? ramHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Memory);
            
            if(ramHardware is null)
                _logger.LogWarning($"{nameof(ramHardware)} can't be found, so it's null");
            else _ram = new(ramHardware);
        }
        
        //HDD
        if(_computer.IsStorageEnabled)
        {
            IHardware? hddHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Storage);
            
            if(hddHardware is null)
                _logger.LogWarning($"{nameof(hddHardware)} can't be found, so it's null");
            else _hdd = new(hddHardware);
        }
    }
    
    public void UpdateAllComponents()
    {
        _cpu?.UpdateComponents();
        _mainboard?.UpdateComponents();
        _gpu?.UpdateComponents();
        _network?.UpdateComponents();
        _fanController?.UpdateComponents();
        _ram?.UpdateComponents();
        _hdd?.UpdateComponents();
    }
    
    public Dictionary<string, double> GetSensorInfos(HardwareCatagory hardwareCatagory)
    {
        Dictionary<string, double> nameValueSensor = new();

        switch (hardwareCatagory)
        {
            case HardwareCatagory.Cpu:
                if(_cpu is null) break;
                
                foreach (ISensor sensor in _cpu.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch(Exception e) { _logger.LogWarning(e.Message); }
                }
                break;
            case HardwareCatagory.Mainboard:
                if(_mainboard is null) break;
                
                foreach (ISensor sensor in _mainboard.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Gpu:
                if(_gpu is null) break;
                
                foreach (ISensor sensor in _gpu.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Network:
                if(_network is null) break;
                
                foreach (ISensor sensor in _network.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.FanController:
                if(_fanController is null) break;
                
                foreach (ISensor sensor in _fanController.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Ram:
                if(_ram is null) break;
                
                foreach (ISensor sensor in _ram.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
                    catch (Exception e) { _logger.LogWarning(e.Message);}
                }
                break;
            case HardwareCatagory.Hdd:
                if(_hdd is null) break;
                
                foreach (ISensor sensor in _hdd.sensors)
                {
                    try { nameValueSensor.Add(sensor.Name, (double)sensor.Value!); }
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
        _computer.Close();

        GC.SuppressFinalize(this);
    }
}