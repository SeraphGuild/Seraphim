using System.ComponentModel.DataAnnotations;

namespace Discord.API;

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

    [MaxLength(25)]
    public IList<ApplicationCommandOptionStringChoice>? Choices { get; private set; }

    public bool? Autocomplete { get; private set; }
}