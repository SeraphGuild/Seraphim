using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

public class ApplicationCommandOption
{
    public ApplicationCommandOption(
        ApplicationCommandOptionType type,
        string name,
        string description,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool? required = null)
    {
        Type = type;
        Name = name;
        NameLocalizations = nameLocalizations;
        Description = description;
        DescriptionLocalizations = descriptionLocalizations;
        Required = required;
    }

    public ApplicationCommandOptionType Type { get; private set; }

    [MaxLength(32)]
    public string Name { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Name"/> field.
    ///     Values follow the same restrictions and <see cref="Name"/>.
    /// </summary>
    [JsonProperty("name_localizations")]
    public IDictionary<string, string>? NameLocalizations { get; private set; }

    [MaxLength(100)]
    public string Description { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Description"/> field.
    ///     Values follow the same restrictions and <see cref="Description"/>.
    /// </summary>
    [JsonProperty("description_localizations")]
    public IDictionary<string, string>? DescriptionLocalizations { get; private set; }

    public bool? Required { get; private set; }
}