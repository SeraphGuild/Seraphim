namespace Seraphim.Discord;

internal interface IDiscordClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}