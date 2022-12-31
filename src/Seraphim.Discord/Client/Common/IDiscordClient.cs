namespace Discord.API;

internal interface IDiscordClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}