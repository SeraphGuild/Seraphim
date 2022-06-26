namespace Seraphim.Discord;

public interface IApplicationCommandClient
{
    Task<IEnumerable<ApplicationCommand>> GetGlobalApplicationCommands(
        bool? includeLocalizations = null);

    Task<ApplicationCommand> CreateGlobalApplicationCommand(
        CreateApplicationCommandRequest createCommand);

    Task<ApplicationCommand> GetGlobalApplicationCommand(
        Snowflake commandId);

    Task DeleteGlobalApplicationCommand(
        Snowflake commandId);

    Task<IEnumerable<ApplicationCommand>> BulkOverwriteGlobalApplicationCommands(
        IEnumerable<CreateApplicationCommandRequest> createCommands);

    Task<IEnumerable<ApplicationCommand>> GetGuildApplicationCommands(
        Snowflake guildId,
        bool? includeLocalizations = null);

    Task<ApplicationCommand> CreateGuildApplicationCommand(
        Snowflake guildId,
        CreateApplicationCommandRequest createCommand);

    Task<ApplicationCommand> GetGuildApplicationCommand(
        Snowflake guildId,
        Snowflake commandId);

    Task DeleteGuildApplicationCommand(
        Snowflake guildId,
        Snowflake commandId);

    Task<IEnumerable<ApplicationCommand>> BulkOverwriteGuildApplicationCommands(
        Snowflake guildId,
        IEnumerable<CreateApplicationCommandRequest> createCommands);

    Task<IEnumerable<GuildApplicationCommandPermission>> GetGuildApplicationCommandPermissions(
        Snowflake guildId);

    Task<IEnumerable<GuildApplicationCommandPermission>> GetApplicationCommandPermissions(
        Snowflake guildId,
        Snowflake commandId);

    Task<GuildApplicationCommandPermission> EditApplicationCommandPermissions(
        Snowflake guildId, 
        Snowflake commandId, 
        IEnumerable<EditApplicationCommandPermissionsRequest> permissions);

    Task<GuildApplicationCommandPermission> BatchEditApplicationCommandPermissions(
        Snowflake guildId, 
        IEnumerable<BatchEditApplicationCommandPermissionsRequest> commandPermissions);

}