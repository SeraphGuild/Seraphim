using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text.Json;

namespace Seraphim.Discord;

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
        this.uriBuilder.Path = path;
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

        this.uriBuilder.Query = this.queryParameters.ToString();
        HttpRequestMessage request = new HttpRequestMessage(this.method, this.uriBuilder.Uri);

        if (this.body != null)
        {
            request.Content = JsonContent.Create(this.body);
        }

        HttpResponseMessage response = await this.configuredClient.SendAsync(request);
        return new DiscordResponseMessage(response);
    }
}