using CommandLine;

namespace HardwareBroadcast.ArgsHandling;

public class CmdArgsHandler
{
    private string[] args { get; }
    public CmdArgsHandler(string[] args)
    {
        this.args = args;
    }
    
    public ArgsOptions Parse()
    {
        ArgsOptions argsOptions = new();
        
        Parser.Default.ParseArguments<ArgsOptions>(args).WithParsed(options =>
        {
            argsOptions.fileConfigPath = options.fileConfigPath;
        });
        
        return argsOptions;
    }
}