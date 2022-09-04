using HardwareStreaming.CmdArgs;
using HardwareStreaming.ConfigurationModels;
using HardwareStreaming.Domains;
using HardwareStreaming.Enums;
using HardwareStreaming.Hardware.Constructor;
using HardwareStreaming.Hardware.HardwareUtils;
using HardwareStreaming.Internals.ArgsParser;
using HardwareStreaming.Internals.Configuration;
using HardwareStreaming.Internals.Configuration.ConfigsFormaters.Yaml;
using HardwareStreaming.Internals.Loggin;
using HardwareStreaming.Internals.Loggin.LogginCore;
using Logger = HardwareStreaming.Internals.Loggin.Logger;

namespace HardwareStreaming;

static class Program
{
    private static ConfigurationPreferences? configurationFile { get; set; }

    public static void Main(string[] args)
    {
        //Logger
        ILogger logger = new Logger(new SerilogLogger());
        
        //CMD args parsing
        CmdArgsHandler cmdArgsHandler = new(args);
        ArgsOptions? argsOptions = cmdArgsHandler.Parse<ArgsOptions>();
        if(argsOptions is null)
        {
            logger.LogFatal("There is no possible to execute the command." +
                            "\nArguments parsing failed.");
            Environment.Exit(1);
        }
        
        //Configuration
        ConfigurationManager<ConfigurationPreferences> configFormater = 
            new(new YamlConfigFormater<ConfigurationPreferences>(), argsOptions.fileConfigPath);

        ConfigurationPreferences? yamlConfigurationFile = 
            ConfigurationPreferences.CreateConfigurationPreferencesDefaultFactory();
        
        if(argsOptions.createConfigFile)
            configFormater.CreateDefaultConfiguration(yamlConfigurationFile);
        else yamlConfigurationFile = configFormater.LoadConfiguration();
        
        if(yamlConfigurationFile is null)
        {
            logger.LogFatal($"The configuration file don't exist in {argsOptions.fileConfigPath}.");
            Environment.Exit(1);
        }
        configurationFile = yamlConfigurationFile;

        List<HardwareCatagory> monitoringHardware = configurationFile.hardwarePreferences.hardwareMonitoring;
        #region Components Builder
        ComputerBuilder computerBuilder = new();
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
        
        KafkaDomain kafkaDomain = new(new()
        {
            BootstrapServers = configurationFile.kafkaDomainConfiguration.bootstrapServer,
            ClientId = configurationFile.kafkaDomainConfiguration.clientId,
        }, logger, configurationFile.kafkaDomainConfiguration.topic);
        
        ComputerConfiguration computerConfiguration = computerBuilder.Build();
        HardwareInfoExtractor hardwareInfoExtractor = new(new(computerConfiguration, logger), 
            configurationFile.hardwarePreferences.temperatureUnit, logger);
        HardwareStreamer hardwareStreamer = new(logger, hardwareInfoExtractor, monitoringHardware, kafkaDomain);

        bool cancel = false;
        bool paused = false;
        Console.CancelKeyPress += (_, e) =>
        {
            paused = true;
            
            Console.WriteLine("Do you want to exit? [y/n] (default: n)");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            cancel = keyInfo.Key == ConsoleKey.Y;
            e.Cancel = cancel;

            if (!e.Cancel)
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