using System.Text.Json;

namespace Seraphim.Discord;
internal class DiscordResponseMessage
{
    private readonly HttpResponseMessage response;

    internal DiscordResponseMessage(HttpResponseMessage httpResponseMessage)
    {
        this.response = httpResponseMessage;
    }

    public async Task<T> ReadBody<T>()
    {
        Stream bodyStreamContent = await this.response.Content.ReadAsStreamAsync();
        return (await JsonDocument.ParseAsync(bodyStreamContent)).Deserialize<T>();
    }
}