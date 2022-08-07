using HardwareBroadcast.Domains;
using Serilog;
using Logger = HardwareBroadcast.Logging.Logger;

var builder = WebApplication.CreateBuilder(args);

//Logger
Logger logger = new(new LoggerConfiguration().WriteTo.Console().CreateLogger());
builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger());

//Domain
KafkaDomain kafkaDomain = new(logger);

var app = builder.Build();

app.MapGet("/", () => Results.Ok());

CancellationTokenSource cancellationTokenSource = new();
CancellationToken token = cancellationTokenSource.Token;

Task kafkaConsumerTask = Task.Factory.StartNew(() => kafkaDomain.StartConsumeLoop(token));
Task appRunTask = app.RunAsync(token);

Task.WaitAll(kafkaConsumerTask, appRunTask);