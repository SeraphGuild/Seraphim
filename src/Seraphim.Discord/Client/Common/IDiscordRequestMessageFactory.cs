namespace Discord.API
{
    internal interface IDiscordRequestMessageFactory
    {
        DiscordRequestMessage CreateRequestMessage();
    }
}