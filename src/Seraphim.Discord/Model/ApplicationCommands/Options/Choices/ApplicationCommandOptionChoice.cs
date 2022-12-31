using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Discord.API;

public class ApplicationCommandOptionChoice
{
    public ApplicationCommandOptionChoice(
        string name,
        IDictionary<string, string>? nameLocalizations = null)
    {
        this.Name = name;
        this.NameLocalizations = nameLocalizations;
    }

    [MaxLength(100)]
    public string Name { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Name"/> field.
    ///     Values follow the same restrictions as <see cref="Name"/>.
    /// </summary>
    [JsonPropertyName("name_localizations")]
    public IDictionary<string, string>? NameLocalizations { get; private set; }
}