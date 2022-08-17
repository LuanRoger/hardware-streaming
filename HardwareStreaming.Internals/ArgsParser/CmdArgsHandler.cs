using CommandLine;

namespace HardwareStreaming.Internals.ArgsParser;

public class CmdArgsHandler
{
    private string[] args { get; }
    public CmdArgsHandler(string[] args)
    {
        this.args = args;
    }
    
    public T? Parse<T>()
    {
        T? result = default;
        Parser.Default.ParseArguments<T>(args).WithParsed(valueParsed => result = valueParsed);
        
        return result;
    }
}