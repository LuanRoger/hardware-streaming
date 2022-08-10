using HardwareBroadcast.ArgsHandling;
using HardwareBroadcast.ConfigurationModels;
using HardwareBroadcast.Domains;
using HardwareStreaming.Internals.ArgsParser;
using HardwareStreaming.Internals.Configuration;
using HardwareStreaming.Internals.Configuration.ConfigsFormaters.Yaml;
using HardwareStreaming.Internals.Loggin;
using HardwareStreaming.Internals.Loggin.LogginCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logger
Logger logger = new(new SerilogLogger());
builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger());

//ArgsParser
CmdArgsHandler argsHandler = new(args);
ArgsOptions? options = argsHandler.Parse<ArgsOptions>();
if(options is null)
{
    logger.LogFatal("There is no possible to execute the command." +
                    "\nArguments parsing failed.");
    Environment.Exit(1);
}

//Configuration
ConfigurationManager<ConfigurationPreferences> configFormater = 
    new(new YamlConfigFormater<ConfigurationPreferences>(), options.fileConfigPath);
ConfigurationPreferences? configurationPreferences = ConfigurationPreferences.FactoryCreateDefault();

if(options.createConfigFile)
    configFormater.CreateDefaultConfiguration(configurationPreferences);
else configurationPreferences = configFormater.LoadConfiguration();

if(configurationPreferences is null)
{
    logger.LogFatal($"It's no possible to load the configuration file in {options.fileConfigPath}");
    Environment.Exit(1);
}

//Domain
KafkaDomain kafkaDomain = new(logger, configurationPreferences.kafkaConfig.bootstrapServer, 
    configurationPreferences.kafkaConfig.groupId);

var app = builder.Build();

app.MapGet("/", () => Results.Ok());

CancellationTokenSource cancellationTokenSource = new();
CancellationToken token = cancellationTokenSource.Token;

Task kafkaConsumerTask = Task.Factory.StartNew(() => kafkaDomain.StartConsumeLoop(token));
Task appRunTask = app.RunAsync(token);

Task.WaitAll(kafkaConsumerTask, appRunTask);