using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

/// <summary>
///     An existing discord application command
/// </summary>
public class ApplicationCommand
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApplicationCommand"/> class. Provides
    ///     default values for any optional fields
    /// </summary>
    /// <param name="id">unique id of the command</param>
    /// <param name="applicationId">Tte unique id of the parent application</param>
    /// <param name="name">the name of the command</param>
    /// <param name="description">the description shown for <see cref="ApplicationCommandType.CHAT_INPUT"/> commands</param>
    /// <param name="version">the command's version</param>
    /// <param name="type">the type of the command</param>
    /// <param name="options">the parameters for the command</param>
    /// <param name="guildId">guild id of the command</param>
    /// <param name="nameLocalizations">the localizations for the command's name</param>
    /// <param name="descriptionLocalizations">the localizations for the command's description</param>
    /// <param name="defaultPermissions">indicates if the command is enabled by default when being added to a guild</param>
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

    /// <summary>
    ///     The unique id of the command.
    /// </summary>
    public Snowflake Id { get; private set; }

    /// <summary>
    ///     The type of command. Defaults to <see cref="ApplicationCommandType.CHAT_INPUT"/> if not defined.
    /// </summary>
    public ApplicationCommandType? Type { get; private set; }

    /// <summary>
    ///     The unique id of the parent application.
    /// </summary>
    [JsonProperty("application_id")]
    public Snowflake ApplicationId { get; private set; }

    /// <summary>
    ///     The guild id of the command. Undefined for global commands.
    /// </summary>
    [JsonProperty("guild_id")]
    public Snowflake? GuildId { get; private set; }

    /// <summary>
    ///     The command's name. Has a maximum length of 32 characters.
    /// </summary>
    [MaxLength(32)]
    public string Name { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Name"/> field. 
    ///     The values follow the same restrictions as <see cref="Name"/>.
    /// </summary>
    [JsonProperty("name_localizations")]
    public IDictionary<string, string>? NameLocalizations { get; private set; }

    /// <summary>
    ///     The description for <see cref="ApplicationCommandType.CHAT_INPUT"/> commands. Has a maximum length of 100 characters.
    ///     Is is an empty string for <see cref="ApplicationCommandType.MESSAGE"/> and <see cref="ApplicationCommandType.USER"/> commands.
    /// </summary>
    [MaxLength(100)]
    public string Description { get; private set; }

    /// <summary>
    ///     The localization dictionary for the <see cref="Description"/> field.
    ///     The values follow the same restrictions as <see cref="Description"/>.
    /// </summary>
    [JsonProperty("description_localizations")]
    public IDictionary<string, string>? DescriptionLocalizations { get; private set; }

    /// <summary>
    ///     The parameters for the command. Up to 25 options may be defined.
    /// </summary>
    [MaxLength(25)]
    public IList<ApplicationCommandOption>? Options { get; private set; }

    /// <summary>
    ///     Indicates whether the command is enabled by default when
    ///     the app is added to a guild. Defaults to true when it is not
    ///     defined.
    /// </summary>
    [JsonProperty("default_permission")]
    public bool? DefaultPermissions { get; private set; }

    /// <summary>
    ///     The command's auto-incrementing version identifier updated
    ///     during record changes.
    /// </summary>
    public Snowflake Version { get; private set; }
}
