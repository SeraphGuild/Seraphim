using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text.Json;

namespace Discord.API;

internal class DiscordRequestMessage
{
    private readonly IDiscordClient configuredClient;

    private readonly UriBuilder uriBuilder;

    private readonly NameValueCollection queryParameters;

    private HttpMethod? method;

    private JsonDocument? body;

    internal DiscordRequestMessage(IDiscordClient configuredClient)
    {
        this.configuredClient = configuredClient;
        this.uriBuilder = new UriBuilder();
        this.queryParameters = new NameValueCollection();
    }

    public DiscordRequestMessage SetMethod(HttpMethod method)
    {
        this.method = method;
        return this;
    }

    public DiscordRequestMessage SetPath(string path)
    {
        this.uriBuilder.Path = "api/v10/" + path;
        return this;
    }

    public DiscordRequestMessage SetQueryParameter(string parameterKey, string parameterValue)
    {
        this.queryParameters.Add(parameterKey, parameterValue);
        return this;
    }

    public DiscordRequestMessage SetJsonBody<T>(T body)
    {
        this.body = body as JsonDocument;
        return this;
    }

    public async Task<DiscordResponseMessage> SendAsync()
    {
        if (this.uriBuilder.Path.Length == 0 || this.method == null)
        {
            throw new InvalidOperationException("Cannot send request before HTTP method and Uri path have been set");
        }

        if (this.queryParameters.Count > 0)
        {
            this.uriBuilder.Query = this.queryParameters.ToString();
        }

        this.uriBuilder.Scheme = "https";
        this.uriBuilder.Host = "discord.com";
        HttpRequestMessage request = new(this.method, this.uriBuilder.Uri);

        if (this.body != null)
        {
            request.Content = JsonContent.Create(this.body);
        }

        HttpResponseMessage response = await this.configuredClient.SendAsync(request);
        return new DiscordResponseMessage(response);
    }
}