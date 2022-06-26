namespace Seraphim.Discord;

public class CreateApplicationCommandRequest : PatchApplicationCommandRequest
{
    public CreateApplicationCommandRequest(
        string name,
        string description,
        ApplicationCommandType? type = ApplicationCommandType.CHAT_INPUT,
        IList<ApplicationCommandOption>? options = null,
        bool defaultPermissions = true) : base(name, description, options, defaultPermissions)
    {
        Type = type;
    }

    /// <remarks>
    ///     Defaults to <see cref="ApplicationCommandType.CHAT_INPUT"/> if not defined.
    /// </remarks>
    public ApplicationCommandType? Type { get; private set; }
}
