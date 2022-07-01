using Confluent.Kafka;
using HardwareStreaming.Configuration;
using HardwareStreaming.Configuration.Models;
using HardwareStreaming.Domains;
using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Constructor;
using HardwareStreaming.Loggin;
using HardwareStreaming.Loggin.HardwareLog;

namespace HardwareStreaming;

static class Program
{
    private static KafkaDomain kafkaDomain { get; set; } = null!;
    private static YamlConfigurationFile configurationFile { get; set; } = null!;

    public static void Main(string[] args)
    {
        Logger logger = new();
        logger.InitGlobalLogger();
        
        string configFilepath = args.Length > 0 ? args[0] : Consts.DEFAULT_CONFIGURATION_PATH;
        YamlConfigurationFile? yamlConfigurationFile = YamlConfigurationManager
            .LoadFromFile(configFilepath, logger);
        
        if(yamlConfigurationFile is null)
        {
            logger.LogFatal("The configuration file don't exist in " + configFilepath);
            Environment.Exit(1);   
        }
        else configurationFile = yamlConfigurationFile;
        
        List<HardwareCatagory> monitoringHardware = configurationFile.hardwareMonitoring;

        #region Components Builder
        ComputerBuilder computerBuilder = new();
        List<IComponentLog> componentLogs = new();
        if(monitoringHardware.Contains(HardwareCatagory.Cpu))
        {
            computerBuilder.InitCPU();
            componentLogs.Add(new CpuLog());
        }
        if(monitoringHardware.Contains(HardwareCatagory.Mainboard))
        {
            computerBuilder.InitMainboard();
            componentLogs.Add(new MainboardLog());
        }
            
        if(monitoringHardware.Contains(HardwareCatagory.Gpu))
        {
            computerBuilder.InitGPU();
            componentLogs.Add(new GpuLog());
        }
            
        if(monitoringHardware.Contains(HardwareCatagory.Network))
        {
            computerBuilder.InitNetwork();
            componentLogs.Add(new NetworkLog());
        }
        if(monitoringHardware.Contains(HardwareCatagory.FanController))
        {
            computerBuilder.InitFanController();
            componentLogs.Add(new FanContollerLog());
        }
            
        if(monitoringHardware.Contains(HardwareCatagory.Ram))
        {
            computerBuilder.InitRAM();
            componentLogs.Add(new RamLog());
        }
            
        if(monitoringHardware.Contains(HardwareCatagory.Hdd))
        {
            computerBuilder.InitHDD();
            componentLogs.Add(new HddLog());
        }
        #endregion
        
        kafkaDomain = new(new()
        {
            BootstrapServers = configurationFile.kafkaDomainConfiguration.bootstrapServer,
            ClientId = configurationFile.kafkaDomainConfiguration.clientId,
            Partitioner = Partitioner.Consistent
        }, logger, configurationFile.kafkaDomainConfiguration.topic);
        
        ComputerConfiguration computerConfiguration = computerBuilder.Build();
        Computer computer = new(computerConfiguration, logger);
        HardwareStreamer hardwareStreamer = new(logger, computer, componentLogs, kafkaDomain);

        bool cancel = false;
        bool paused = false;
        Console.CancelKeyPress += (_, e) =>
        {
            paused = true;
            
            Console.WriteLine("Do you want to exit? [y/n] (default: n)");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            cancel = keyInfo.Key == ConsoleKey.Y;
            e.Cancel = cancel;

            if (e.Cancel)
            {
                paused = false;
                return;
            }
            
            computer.Dispose();
            Environment.Exit(0);
        };
        
        while(!cancel)
        {
            while (!paused)
            {
                computer.UpdateAllComponents();
                hardwareStreamer.PulseStream(yamlConfigurationFile.delayStreamTime);   
            }
        }
    }
}