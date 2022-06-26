namespace Seraphim.Discord;

internal class DiscordRequestMessageFactory : IDiscordRequestMessageFactory
{
    private readonly IDiscordClient configuredClient;

    public DiscordRequestMessageFactory(IDiscordClient client)
    {
        this.configuredClient = client;
    }

    public DiscordRequestMessage CreateRequestMessage()
    {
        return new DiscordRequestMessage(configuredClient);
    }
}
