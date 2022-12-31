using System.Text.Json;

namespace Discord.API;
internal class DiscordResponseMessage
{
    private readonly HttpResponseMessage response;

    internal DiscordResponseMessage(HttpResponseMessage httpResponseMessage)
    {
        this.response = httpResponseMessage;
    }

    public async Task<T> ReadBody<T>()
    {
        string bodyStreamContent = await this.response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(bodyStreamContent).Deserialize<T>()!;
    }
}