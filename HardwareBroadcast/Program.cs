using HardwareBroadcast.ArgsHandling;
using HardwareBroadcast.Configuration;
using HardwareBroadcast.Configuration.Models;
using HardwareBroadcast.Domains;
using Serilog;
using Logger = HardwareBroadcast.Logging.Logger;

var builder = WebApplication.CreateBuilder(args);

//Logger
Logger logger = new(new LoggerConfiguration().WriteTo.Console().CreateLogger());
builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger());

//ArgsParser
CmdArgsHandler argsHandler = new(args);
ArgsOptions options = argsHandler.Parse();

//Configuration
ConfigurationLoader configurationLoader = new(logger);
YamlConfiguration? configuration = configurationLoader.LoadConfig(options.fileConfigPath);

if(configuration is null)
{
    logger.LogFatal($"It's no possible to load the configuration file in {options.fileConfigPath}");
    Environment.Exit(1);
}

//Domain
KafkaDomain kafkaDomain = new(logger, configuration.kafkaConfig.bootstrapServer, configuration.kafkaConfig.groupId);

var app = builder.Build();

app.MapGet("/", () => Results.Ok());

CancellationTokenSource cancellationTokenSource = new();
CancellationToken token = cancellationTokenSource.Token;

Task kafkaConsumerTask = Task.Factory.StartNew(() => kafkaDomain.StartConsumeLoop(token));
Task appRunTask = app.RunAsync(token);

Task.WaitAll(kafkaConsumerTask, appRunTask);