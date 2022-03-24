namespace Seraphim.Discord;

/// <summary>
///     Enumerates the possible type's a application commands that an application
///     can define
/// </summary>
public enum ApplicationCommandType
{
    /// <summary>
    ///     Indicates the command is executed from chat input
    /// </summary>
    CHAT_INPUT = 1,

    /// <summary>
    ///     Indicates the command is executed from a user's right-click context menu
    /// </summary>
    USER = 2,
    
    /// <summary>
    ///     Indicates the command is executed from a message's context menu
    /// </summary>
    MESSAGE = 3
}