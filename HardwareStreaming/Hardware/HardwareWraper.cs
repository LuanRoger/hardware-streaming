using LibreHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware;

public abstract class HardwareWraper
{
    public abstract HardwareType HARDWARE_TYPE { get; }
    protected abstract IHardware _hardware { get; }

    public abstract string name { get; set; }
    public abstract Identifier identifier { get; set; }
    public abstract IHardware[] subHardware { get; set; }
    public abstract IHardware parent { get; set; }
    public abstract ISensor[] sensors { get; set; }
    
    public abstract void UpdateComponents();
}