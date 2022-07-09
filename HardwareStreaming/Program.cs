using HardwareStreaming.ArgsHandling;
using HardwareStreaming.Configuration;
using HardwareStreaming.Configuration.Models;
using HardwareStreaming.Domains;
using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Constructor;
using HardwareStreaming.Hardware.HardwareUtils;
using HardwareStreaming.Loggin;
using HardwareStreaming.Loggin.HardwareLog;

namespace HardwareStreaming;

static class Program
{
    private static KafkaDomain kafkaDomain { get; set; } = null!;
    private static YamlConfigurationFile? configurationFile { get; set; }

    public static void Main(string[] args)
    {
        Logger logger = new();
        logger.InitGlobalLogger();
        CmdArgsHandler cmdArgsHandler = new(args);
        ArgsOptions argsOptions = cmdArgsHandler.Parse();

        YamlConfigurationFile? yamlConfigurationFile = YamlConfigurationManager
            .LoadConfigFile(argsOptions.fileConfigPath, logger, argsOptions.createConfigFile);
        if(yamlConfigurationFile is null)
        {
            logger.LogFatal($"The configuration file don't exist in {argsOptions.fileConfigPath}");
            Environment.Exit(1);   
        }
        configurationFile = yamlConfigurationFile;

        List<HardwareCatagory> monitoringHardware = configurationFile.hardwareMonitoring;
        #region Components Builder
        ComputerBuilder computerBuilder = new();
        List<IComponentLog> componentLogs = new();
        if(monitoringHardware.Contains(HardwareCatagory.Cpu))
            computerBuilder.InitCPU();
        if(monitoringHardware.Contains(HardwareCatagory.Mainboard))
            computerBuilder.InitMainboard();
        if(monitoringHardware.Contains(HardwareCatagory.Gpu))
            computerBuilder.InitGPU();
        if(monitoringHardware.Contains(HardwareCatagory.Network))
            computerBuilder.InitNetwork();
        if(monitoringHardware.Contains(HardwareCatagory.FanController))
            computerBuilder.InitFanController();
        if(monitoringHardware.Contains(HardwareCatagory.Ram))
            computerBuilder.InitRAM();
        if(monitoringHardware.Contains(HardwareCatagory.Hdd))
            computerBuilder.InitHDD();
        #endregion
        
        kafkaDomain = new(new()
        {
            BootstrapServers = configurationFile.kafkaDomainConfiguration.bootstrapServer,
            ClientId = configurationFile.kafkaDomainConfiguration.clientId,
        }, logger, configurationFile.kafkaDomainConfiguration.topic);
        
        ComputerConfiguration computerConfiguration = computerBuilder.Build();
        HardwareInfoExtractor hardwareInfoExtractor = new(new(computerConfiguration, logger), logger);
        HardwareStreamer hardwareStreamer = new(logger, hardwareInfoExtractor, monitoringHardware, kafkaDomain);

        bool cancel = false;
        bool paused = false;
        Console.CancelKeyPress += (_, e) =>
        {
            paused = true;
            
            Console.WriteLine("Do you want to exit? [y/n] (default: n)");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            cancel = keyInfo.Key != ConsoleKey.Y;
            e.Cancel = cancel;

            if (e.Cancel)
            {
                paused = false;
                return;
            }
            
            hardwareInfoExtractor.Dispose();
            Environment.Exit(0);
        };
        
        while(!cancel)
        {
            while (!paused)
            {
                hardwareInfoExtractor.UpdateComputerComponents();
                hardwareStreamer.PulseStream();   
            }
        }
    }
}