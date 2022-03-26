using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Seraphim.Discord;

internal class DiscordClient : IDiscordClient
{
    private const string BaseUrlFormat = "https://www.discord.com/api/v{0}";

    private readonly HttpClient httpClient;

    private readonly string clientId;

    private readonly string clientSecret;

    private readonly string baseUrl;

    private OAuthToken oAuthToken;

    public DiscordClient(HttpClient httpClient, short apiVersion, string clientId, string clientSecret)
    {
        this.httpClient = httpClient;
        this.clientId = clientId;
        this.clientSecret = clientSecret;
        this.baseUrl = string.Format(BaseUrlFormat, apiVersion);
        this.oAuthToken = new OAuthToken();
    }

    public async Task<HttpResponseMessage> SendAsync<T>(string apiPath, HttpMethod method, T body)
    {
        if (this.oAuthToken.IsValid)
        {
            this.oAuthToken = await FetchOAuthToken();
        }

        HttpRequestMessage httpRequestMessage = BuildRequest(apiPath, method, body);

        return await this.httpClient.SendAsync(httpRequestMessage);
    }

    private HttpRequestMessage BuildRequest<T>(string apiPath, HttpMethod method, T body)
    {
        UriBuilder uriBuilder = new UriBuilder(baseUrl)
        {
            Path = apiPath
        };

        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            RequestUri = uriBuilder.Uri,
            Method = method,
            Content = new StringContent(JsonSerializer.Serialize<T>(body)),
        };

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(this.oAuthToken.TokenType, this.oAuthToken.AccessToken);
        return httpRequestMessage;
    }

    private async Task<OAuthToken> FetchOAuthToken()
    {
        UriBuilder uriBuilder = new UriBuilder(baseUrl)
        {
            Path = "oauth2/token"
        };

        IDictionary<string, string> oAuthFormData = new Dictionary<string, string>()
        {
            { "grant_type", "client_credential" },
            { "scope", "application.commands application.commands.upate" }
        };

        HttpRequestMessage bearerTokenRequest = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
        bearerTokenRequest.Content = new FormUrlEncodedContent(oAuthFormData);
        bearerTokenRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.clientId}:{this.clientSecret}")));

        HttpResponseMessage responseMessage = await this.httpClient.SendAsync(bearerTokenRequest);

        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OAuthToken>(responseContent) ?? new OAuthToken();
    }
}
