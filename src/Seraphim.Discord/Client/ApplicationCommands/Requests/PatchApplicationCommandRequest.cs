using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Discord.API;

public class PatchApplicationCommandRequest
{
    public PatchApplicationCommandRequest(
        string name,
        string description,
        IList<ApplicationCommandOption>? options = null,
        bool defaultPermissions = true)
    {
        Name = name;
        Description = description;
        Options = options;
        DefaultPermissions = defaultPermissions;
    }

    [MaxLength(32)]
    public string Name { get; private set; }

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
}
