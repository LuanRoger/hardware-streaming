using HardwareStreaming.Enums;
using OpenHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware;

public sealed class GPU : HardwareWraper
{
    public override HardwareType HARDWARE_TYPE { get; }
    protected override IHardware _hardware { get; }
    public override string name { get; set; }
    public override Identifier identifier { get; set; }
    public override IHardware[] subHardware { get; set; }
    public override IHardware parent { get; set; }
    public override ISensor[] sensors { get; set; }

    public GPU(IHardware hardware, GpuType gpuType)
    {
        _hardware = hardware;
        HARDWARE_TYPE = gpuType switch
        {
            GpuType.Nvidia => HardwareType.GpuNvidia,
            GpuType.ATI => HardwareType.GpuAti,
        };

        UpdateComponents();
    }
    
    public override void UpdateComponents()
    {
        _hardware.Update();
        
        name = _hardware.Name;
        identifier = _hardware.Identifier;
        subHardware = _hardware.SubHardware;
        parent = _hardware.Parent;
        sensors = _hardware.Sensors;
    }
}