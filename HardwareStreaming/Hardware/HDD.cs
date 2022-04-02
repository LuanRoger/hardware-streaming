﻿using OpenHardwareMonitor.Hardware;

namespace HardwareStreaming.Hardware;

public sealed class HDD : HardwareWraper
{
    public override HardwareType HARDWARE_TYPE { get; }
    protected override IHardware _hardware { get; }
    public override string name { get; set; }
    public override Identifier identifier { get; set; }
    public override IHardware[] subHardware { get; set; }
    public override IHardware parent { get; set; }
    public override ISensor[] sensors { get; set; }

    public HDD(IHardware hardware)
    {
        _hardware = hardware;
        
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