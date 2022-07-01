namespace HardwareStreaming.ArgsHandling;

public class ArgsHandler
{
    private readonly char flagIndicator = '-';
    
    public string[] args { get; }
    public string? configFilePathIndex { get; set; }
    public List<ArgsFlags>? flags { get; private set; }

    public ArgsHandler(string[] args)
    {
        this.args = args;
    }
    
    public void Parse()
    {
        List<ArgsFlags> flags = new();
        foreach (string argument in args)
        {
            if(argument[..2].Equals(new(flagIndicator, 2)))
                flags.Add(ParseFlag(argument));
            else configFilePathIndex = argument;
        }
        
        this.flags = flags;
    }
    private ArgsFlags ParseFlag(string flagText)
    {
        foreach (string availableFlags in SupportedFlags.AVAILABLE_ARGS_FLAGS.Values)
            if(availableFlags.Equals(flagText)) return SupportedFlags.AVAILABLE_ARGS_FLAGS
                .GetEnumerator().Current.Key;
        
        return ArgsFlags.UnknownFlag;
    }
}