namespace Seraphim.Discord;

internal interface IDiscordClient
{
    Task<HttpResponseMessage> SendAsync<T>(string apiPath, HttpMethod method, T body);
}