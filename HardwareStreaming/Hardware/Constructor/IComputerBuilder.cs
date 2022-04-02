namespace HardwareStreaming.Hardware.Constructor;

public interface IComputerBuilder
{
    public IComputerBuilder InitCPU();
    public IComputerBuilder InitMainboard();
    public IComputerBuilder InitGPU();
    public IComputerBuilder InitFanController();
    public IComputerBuilder InitHDD();
    public IComputerBuilder InitRAM();
    public IComputerBuilder InitNetwork();
    public ComputerConfiguration Build();
}