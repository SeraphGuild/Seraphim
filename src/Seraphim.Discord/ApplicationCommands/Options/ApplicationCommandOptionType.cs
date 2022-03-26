namespace Seraphim.Discord;

public enum ApplicationCommandOptionType
{
    SUB_COMMAND = 1,

    SUB_COMMAND_GROUP = 2,

    STRING = 3,

    /// <summary>
    ///     Any 64-bit integer
    /// </summary>
    INTEGER = 4,

    BOOLEAN = 5,

    USER = 6,

    /// <summary>
    /// 	Includes all channel types + categories
    /// </summary>
    CHANNEL = 7,

    ROLE = 8,

    MENTIONABLE = 9,

    /// <summary>
    ///     Any 64-bit double
    /// </summary>
    NUMBER = 10,

    ATTACHMENT = 11
}