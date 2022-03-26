using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Seraphim.Discord;

[JsonConverter(typeof(ApplicationCommandJsonConverter))]
public class ApplicationCommand
{
    public ApplicationCommand(
        Snowflake id,
        Snowflake applicationId,
        string name,
        string description,
        Snowflake version,
        ApplicationCommandType? type = ApplicationCommandType.CHAT_INPUT,
        IList<ApplicationCommandOption>? options = null,
        Snowflake? guildId = null,
        IDictionary<string, string>? nameLocalizations = null,
        IDictionary<string, string>? descriptionLocalizations = null,
        bool defaultPermissions = true)
    {
        Id = id;
        Type = type;
        ApplicationId = applicationId;
        GuildId = guildId;
        Name = name;
        NameLocalizations = nameLocalizations;
        Description = description;
        DescriptionLocalizations = descriptionLocalizations;
        Options = options;
        DefaultPermissions = defaultPermissions;
        Version = version;
    }

    public Snowflake Id { get; private set; }

    /// <remarks>
    ///     Defaults to <see cref="ApplicationCommandType.CHAT_INPUT"/> if not defined.
    /// </remarks>
    public ApplicationCommandType? Type { get; private set; }

    [JsonPropertyName("application_id")]
    public Snowflake ApplicationId { get; private set; }

    /// <remarks>
    ///     Undefined for global commands.
    /// </remarks>
    [JsonPropertyName("guild_id")]
    public Snowflake? GuildId { get; private set; }

    [MaxLength(32)]
    public string Name { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Name"/> field. 
    ///     The values follow the same restrictions as <see cref="Name"/>.
    /// </summary>
    [JsonPropertyName("name_localizations")]
    public IDictionary<string, string>? NameLocalizations { get; private set; }

    /// <summary>
    ///     The description for <see cref="ApplicationCommandType.CHAT_INPUT"/> commands.
    /// </summary>
    /// <remarks>
    ///     Is is an empty string for <see cref="ApplicationCommandType.MESSAGE"/> and 
    ///     <see cref="ApplicationCommandType.USER"/> commands.
    /// </remarks>
    [MaxLength(100)]
    public string Description { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Description"/> field.
    ///     The values follow the same restrictions as <see cref="Description"/>.
    /// </summary>
    [JsonPropertyName("description_localizations")]
    public IDictionary<string, string>? DescriptionLocalizations { get; private set; }

    /// <summary>
    ///     The commands parameters
    /// </summary>
    [MaxLength(25)]
    public IList<ApplicationCommandOption>? Options { get; private set; }

    /// <summary>
    ///     Indicates whether the command is enabled by default when the app is added to a guild. 
    ///     Defaults to true when it is not defined.
    /// </summary>
    [JsonPropertyName("default_permission")]
    public bool? DefaultPermissions { get; private set; }

    public Snowflake Version { get; private set; }
}
