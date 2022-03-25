using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

public class ApplicationCommandStringOption : ApplicationCommandOption
{
    public ApplicationCommandStringOption(
        ApplicationCommandOptionType type,
        string name,
        string description,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool? required = null,
        IList<ApplicationCommandOptionStringChoice>? choices = null, 
        bool? autocomplete = null) : base(
            type, 
            name,
            description,
            nameLocalizations,
            descriptionLocalizations,
            required)
    {
        Choices = choices;
        Autocomplete = autocomplete;
    }

    /// <summary>
    ///     Choices for <see cref="ApplicationCommandOptionType.STRING"/>, <see cref="ApplicationCommandOptionType.INTEGER"/>,
    ///     or <see cref="ApplicationCommandOptionType.NUMBER"/> type options.
    /// </summary>
    [MaxLength(25)]
    public IList<ApplicationCommandOptionStringChoice>? Choices { get; private set; }

    /// <summary>
    ///     Indicates if autocomplete interactions are enabled for this
    ///     <see cref="ApplicationCommandOptionType.STRING"/>, <see cref="ApplicationCommandOptionType.INTEGER"/>,
    ///     or <see cref="ApplicationCommandOptionType.NUMBER"/> type option.
    /// </summary>
    public bool? Autocomplete { get; private set; }
}