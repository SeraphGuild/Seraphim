namespace Discord.API;

public class ApplicationCommandSubcommandOption : ApplicationCommandOption
{
    public ApplicationCommandSubcommandOption(
        ApplicationCommandOptionType type,
        string name,
        string description,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool? required = null, 
        IList<ApplicationCommandOption>? options = null) : base(
            type, 
            name,
            description,
            nameLocalizations,
            descriptionLocalizations,
            required)
    {
        Options = options;
    }

    public IList<ApplicationCommandOption>? Options { get; private set; }
}