namespace Seraphim.Discord;

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

    /// <summary>
    ///     If the option is a <see cref="ApplicationCommandOptionType.SUB_COMMAND"/> or <see cref="ApplicationCommandOptionType.SUB_COMMAND_GROUP"/> type,
    ///     these nested options will be the parameters
    /// </summary>
    public IList<ApplicationCommandOption>? Options { get; private set; }
}