using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Discord.API;

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

    [MaxLength(25)]
    public IList<ApplicationCommandOptionDoubleChoice>? Choices { get; private set; }

    [JsonPropertyName("min_value")]
    public double? MinValue { get; private set; }

    [JsonPropertyName("max_value")]
    public double? MaxValue { get; private set; }

    public bool? Autocomplete { get; private set; }
}