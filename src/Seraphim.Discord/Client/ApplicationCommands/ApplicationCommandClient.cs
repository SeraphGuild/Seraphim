namespace Discord.API;

internal class ApplicationCommandClient : IApplicationCommandClient
{
    private readonly IDiscordRequestMessageFactory requestFactory;

    private readonly Snowflake applicationId;

    public ApplicationCommandClient(IDiscordRequestMessageFactory requestFactory, Snowflake applicationId)
    {
        this.requestFactory = requestFactory;
        this.applicationId = applicationId;
    }

    public async Task<GuildApplicationCommandPermission> BatchEditApplicationCommandPermissions(Snowflake guildId, IEnumerable<BatchEditApplicationCommandPermissionsRequest> commandPermissions)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Put, $"guilds/{guildId}/commands/permissions", commandPermissions);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<GuildApplicationCommandPermission>();
    }

    public async Task<IEnumerable<ApplicationCommand>> BulkOverwriteGlobalApplicationCommands(IEnumerable<CreateApplicationCommandRequest> createCommands)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Put, $"commands", createCommands);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<ApplicationCommand>>();
    }

    public async Task<IEnumerable<ApplicationCommand>> BulkOverwriteGuildApplicationCommands(Snowflake guildId, IEnumerable<CreateApplicationCommandRequest> createCommands)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Put, $"guilds/{guildId}/commands", createCommands);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<ApplicationCommand>>();
    }

    public async Task<ApplicationCommand> CreateGlobalApplicationCommand(CreateApplicationCommandRequest createCommand)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Post, $"commands", createCommand);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<ApplicationCommand>();
    }

    public async Task<ApplicationCommand> CreateGuildApplicationCommand(Snowflake guildId, CreateApplicationCommandRequest createCommand)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Post, $"guilds/{guildId}/commands", createCommand);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<ApplicationCommand>();
    }

    public async Task DeleteGlobalApplicationCommand(Snowflake commandId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Delete, $"commands/{commandId}");
        await request.SendAsync();
    }

    public async Task DeleteGuildApplicationCommand(Snowflake guildId, Snowflake commandId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Delete, $"guilds/{guildId}/commands/{commandId}");
        await request.SendAsync();
    }

    public async Task<GuildApplicationCommandPermission> EditApplicationCommandPermissions(Snowflake guildId, Snowflake commandId, IEnumerable<EditApplicationCommandPermissionsRequest> permissions)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Put, $"guilds/{guildId}/commands/{commandId}/permissions", permissions);
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<GuildApplicationCommandPermission>();
    }

    public async Task<IEnumerable<GuildApplicationCommandPermission>> GetApplicationCommandPermissions(Snowflake guildId, Snowflake commandId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, $"guilds/{guildId}/commands/{commandId}/permissions");
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<GuildApplicationCommandPermission>>();
    }

    public async Task<ApplicationCommand> GetGlobalApplicationCommand(Snowflake commandId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, $"commands/{commandId}");
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<ApplicationCommand>();
    }

    public async Task<IEnumerable<ApplicationCommand>> GetGlobalApplicationCommands(bool? includeLocalizations = null)
    {
        DiscordRequestMessage request = this.CreateBaseDiscordRequest(HttpMethod.Get, "commands");

        if (includeLocalizations.HasValue && includeLocalizations.Value)
        {
            request.SetQueryParameter("with_localizations", includeLocalizations.Value.ToString());
        }

        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<ApplicationCommand>>();
    }

    public async Task<ApplicationCommand> GetGuildApplicationCommand(Snowflake guildId, Snowflake commandId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, $"guilds/{guildId}/commands/{commandId}");
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<ApplicationCommand>();
    }

    public async Task<IEnumerable<GuildApplicationCommandPermission>> GetGuildApplicationCommandPermissions(Snowflake guildId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, $"guilds/{guildId}/commands/permissions");
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<GuildApplicationCommandPermission>>();
    }

    public async Task<IEnumerable<ApplicationCommand>> GetGuildApplicationCommands(Snowflake guildId, bool? includeLocalizations = null)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, $"guilds/{guildId}/commands");

        if (includeLocalizations.HasValue && includeLocalizations.Value)
        {
            request.SetQueryParameter("with_localizations", includeLocalizations.Value.ToString());
        }

        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<IEnumerable<ApplicationCommand>>();
    }

    private DiscordRequestMessage CreateBaseDiscordRequest<T>(HttpMethod httpMethod, string path, T body)
    {
        return this.CreateBaseDiscordRequest(httpMethod, path).SetJsonBody(body);
    }

    private DiscordRequestMessage CreateBaseDiscordRequest(HttpMethod httpMethod, string path)
    {
        return this.requestFactory
            .CreateRequestMessage()
            .SetMethod(httpMethod)
            .SetPath($"applications/{this.applicationId}/{path}");
    }
}
