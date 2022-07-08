using HardwareStreaming.Enums;
using HardwareStreaming.Hardware;
using LibreHardwareMonitor.Hardware;
using ILogger = HardwareStreaming.Loggin.ILogger;
// ReSharper disable InconsistentNaming

namespace HardwareStreaming;

public class Computer : IDisposable
{
    private LibreHardwareMonitor.Hardware.Computer _computer { get; }
    private ILogger _logger { get; }

    public CPU? cpu { get; set; }
    public Mainboard? mainboard { get; set; }
    public GPU? gpu { get; set; }
    public Network? network { get; set; }
    public EmbeddedController? fanController { get; set; }
    public RAM? ram { get; set; }
    public HDD? hdd { get; set; }

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
            else cpu = new(cpuHardware); 
        }
        
        //Mainboard
        if(_computer.IsMotherboardEnabled)
        {
            IHardware? mainboardHardware = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Motherboard);
            if(mainboardHardware is null)
                _logger.LogWarning($"{nameof(mainboardHardware)} can't be found, so it's null");
            else mainboard = new(mainboardHardware);
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
                    gpu = new(gpuHardwareNvidia, GpuType.Nvidia);
                else if(gpuHardwareIntel is not null)
                    gpu = new(gpuHardwareIntel, GpuType.Intel);
                else if(gpuHardwareAmd is not null) 
                    gpu = new(gpuHardwareAmd!, GpuType.Amd);
            }
        }
        
        //Network
        if(_computer.IsNetworkEnabled)
        {
            IHardware? networkHardware = _computer.Hardware
                .FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Network);
            
            if(networkHardware is null)
                _logger.LogWarning($"{nameof(networkHardware)} can't be found, so it's null");
            else network = new(networkHardware);
        }
        
        //FanController
        if(_computer.IsControllerEnabled)
        {
            IHardware? fanControllerHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.EmbeddedController);
            
            if(fanControllerHardware is null)
                _logger.LogWarning($"{nameof(fanControllerHardware)} can't be found, so it's null");
            else fanController = new(fanControllerHardware);
        }
        
        //RAM
        if(_computer.IsMemoryEnabled)
        {
            IHardware? ramHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Memory);
            
            if(ramHardware is null)
                _logger.LogWarning($"{nameof(ramHardware)} can't be found, so it's null");
            else ram = new(ramHardware);
        }
        
        //HDD
        if(_computer.IsStorageEnabled)
        {
            IHardware? hddHardware = _computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Storage);
            
            if(hddHardware is null)
                _logger.LogWarning($"{nameof(hddHardware)} can't be found, so it's null");
            else hdd = new(hddHardware);
        }
    }
    
    public void UpdateAllComponents()
    {
        cpu?.UpdateComponents();
        mainboard?.UpdateComponents();
        gpu?.UpdateComponents();
        network?.UpdateComponents();
        fanController?.UpdateComponents();
        ram?.UpdateComponents();
        hdd?.UpdateComponents();
    }
    
    public void Dispose()
    {
        _computer.Close();

        GC.SuppressFinalize(this);
    }
}