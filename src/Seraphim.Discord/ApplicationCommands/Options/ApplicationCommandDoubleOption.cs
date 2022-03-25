using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

public class ApplicationCommandDoubleOption
{
    public ApplicationCommandDoubleOption(
        IList<ApplicationCommandOptionDoubleChoice>? choices = null, 
        double? minValue = null,
        double? maxValue = null, 
        bool? autocomplete = null)
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
    public IList<ApplicationCommandOptionDoubleChoice>? Choices { get; private set; }

    /// <summary>
    ///     The minimum value permitted for the option if it's types is either
    ///     <see cref="ApplicationCommandOptionType.INTEGER"/> or <see cref="ApplicationCommandOptionType.DOUBLE"/>
    /// </summary>
    [JsonProperty("min_value")]
    public double? MinValue { get; private set; }

    /// <summary>
    ///     The maximum value permitted for the option if it's types is either
    ///     <see cref="ApplicationCommandOptionType.INTEGER"/> or <see cref="ApplicationCommandOptionType.DOUBLE"/>
    /// </summary>
    [JsonProperty("max_value")]
    public double? MaxValue { get; private set; }

    /// <summary>
    ///     Indicates if autocomplete interactions are enabled for this
    ///     <see cref="ApplicationCommandOptionType.STRING"/>, <see cref="ApplicationCommandOptionType.INTEGER"/>,
    ///     or <see cref="ApplicationCommandOptionType.NUMBER"/> type option.
    /// </summary>
    public bool? Autocomplete { get; private set; }
}