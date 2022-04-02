namespace HardwareStreaming.Hardware.Constructor;

public class ComputerBuilder : IComputerBuilder
{
    private ComputerConfiguration _computerConfiguration { get; }

    public ComputerBuilder()
    {
        _computerConfiguration = new();
    }
    
    public IComputerBuilder InitCPU()
    {
        _computerConfiguration.initCpu = true;
        return this;
    }

    public IComputerBuilder InitMainboard()
    {
        _computerConfiguration.initMainboard = true;
        return this;
    }

    public IComputerBuilder InitGPU()
    {
        _computerConfiguration.initGpu = true;
        return this;
    }

    public IComputerBuilder InitFanController()
    {
        _computerConfiguration.initFanController = true;
        return this;
    }

    public IComputerBuilder InitHDD()
    {
        _computerConfiguration.initHdd = true;
        return this;
    }

    public IComputerBuilder InitRAM()
    {
        _computerConfiguration.initRam = true;
        return this;
    }

    public IComputerBuilder InitNetwork()
    {
        _computerConfiguration.initNetwork = true;
        return this;
    }

    public ComputerConfiguration Build() => _computerConfiguration;
}