using System.Net.Http.Headers;

namespace Discord.API;

internal class DiscordClient : IDiscordClient
{
    private readonly HttpClient httpClient;

    private readonly string botToken;

    public DiscordClient(HttpClient httpClient, short apiVersion, string botToken)
    {
        this.httpClient = httpClient;
        this.botToken = botToken;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
    {
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bot", this.botToken);
        return await this.httpClient.SendAsync(httpRequestMessage);
    }
}
