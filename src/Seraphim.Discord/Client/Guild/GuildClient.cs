using LanguageExt;
using Discord.Core;

namespace Discord.API;

internal class GuildClient : IGuildClient
{
    private readonly IDiscordRequestMessageFactory requestFactory;

    public GuildClient(IDiscordRequestMessageFactory requestFactory)
    {
        this.requestFactory = requestFactory;
    }

    public async Task<Fin<Guild>> GetGuildAsync(Snowflake guildId)
    {
        DiscordRequestMessage request = CreateBaseDiscordRequest(HttpMethod.Get, guildId.ToString());
        DiscordResponseMessage response = await request.SendAsync();
        return await response.ReadBody<Guild>();
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
            .SetPath($"guilds/{path}");
    }
}