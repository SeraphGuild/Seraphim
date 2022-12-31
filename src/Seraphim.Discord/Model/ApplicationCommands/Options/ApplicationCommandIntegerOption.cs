using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Discord.API;

public class ApplicationCommandIntegerOption : ApplicationCommandOption
{
    public ApplicationCommandIntegerOption(
        ApplicationCommandOptionType type,
        string name,
        string description,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool? required = null,
        IList<ApplicationCommandOptionIntegerChoice>? choices = null, 
        int? minValue = null,
        int? maxValue = null, 
        bool? autocomplete = null) : base(
            type,
            name,
            description,
            nameLocalizations,
            descriptionLocalizations,
            required)
    {
        Choices = choices;
        MinValue = minValue;
        MaxValue = maxValue;
        Autocomplete = autocomplete;
    }

    [MaxLength(25)]
    public IList<ApplicationCommandOptionIntegerChoice>? Choices { get; private set; }

    [JsonPropertyName("min_value")]
    public int? MinValue { get; private set; }

    [JsonPropertyName("max_value")]
    public int? MaxValue { get; private set; }

    public bool? Autocomplete { get; private set; }
}