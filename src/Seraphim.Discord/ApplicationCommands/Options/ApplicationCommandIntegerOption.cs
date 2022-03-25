using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

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

    /// <summary>
    ///     Choices for <see cref="ApplicationCommandOptionType.STRING"/>, <see cref="ApplicationCommandOptionType.INTEGER"/>,
    ///     or <see cref="ApplicationCommandOptionType.NUMBER"/> type options.
    /// </summary>
    [MaxLength(25)]
    public IList<ApplicationCommandOptionIntegerChoice>? Choices { get; private set; }

    /// <summary>
    ///     The minimum value permitted for the option if it's types is either
    ///     <see cref="ApplicationCommandOptionType.INTEGER"/> or <see cref="ApplicationCommandOptionType.DOUBLE"/>
    /// </summary>
    [JsonProperty("min_value")]
    public int? MinValue { get; private set; }

    /// <summary>
    ///     The maximum value permitted for the option if it's types is either
    ///     <see cref="ApplicationCommandOptionType.INTEGER"/> or <see cref="ApplicationCommandOptionType.DOUBLE"/>
    /// </summary>
    [JsonProperty("max_value")]
    public int? MaxValue { get; private set; }

    /// <summary>
    ///     Indicates if autocomplete interactions are enabled for this
    ///     <see cref="ApplicationCommandOptionType.STRING"/>, <see cref="ApplicationCommandOptionType.INTEGER"/>,
    ///     or <see cref="ApplicationCommandOptionType.NUMBER"/> type option.
    /// </summary>
    public bool? Autocomplete { get; private set; }
}