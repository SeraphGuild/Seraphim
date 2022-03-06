using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace Seraphim.Interactions;

public static class TestInteractionHandler
{
    const string DiscordApiBaseUrl = "https://discord.com/api/v9";
    const string BotClientIdEnvironmentVariableName = "BOT_CLIENT_ID";
    const string BotClientSecretEnvironmentVariableName = "BOT_CLIENT_SECRET";

    [FunctionName("TestInteractionHandler")]
    public static async Task Run(
        [ServiceBusTrigger(
            topicName: "interactions",
            subscriptionName: "ping",
            Connection = "SERAPHIM_SERVICE_BUS_CONNECTION_STRING")]
        string myQueueItem, 
        ILogger log)
    {
        HttpClient httpClient = new HttpClient();

        string seraphimApiMessage = await GetMessageContent(log, httpClient);
        string bearerToken = await GetBearerToken(httpClient);
        await RespondToInteraction(myQueueItem, httpClient, seraphimApiMessage, bearerToken);

        log.LogInformation("Successfully sent message to discord");
    }

    private static async Task RespondToInteraction(string myQueueItem, HttpClient httpClient, string seraphimApiMessage, string bearerToken)
    {
        JObject interaction = JObject.Parse(myQueueItem);

        string interactionResponseUrl = $"{DiscordApiBaseUrl}/interactions/{interaction["id"].Value<string>()}/{interaction["token"].Value<string>()}/callback";
        object interactionResponsePayload = new
        {
            type = 4,
            data = new
            {
                content = seraphimApiMessage
            }
        };

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, interactionResponseUrl)
        {
            Content = new ObjectContent(typeof(object), interactionResponsePayload, new JsonMediaTypeFormatter())
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        await httpClient.SendAsync(request);
    }

    private static async Task<string> GetBearerToken(HttpClient httpClient)
    {
        object oauthPayload = new
        {
            grant_type = "client_credentials",
            scope = "applications.commands applications.commands.update"
        };

        HttpRequestMessage bearerTokenRequest = new HttpRequestMessage(HttpMethod.Post, $"{DiscordApiBaseUrl}/oauth2/token")
        {
            Content = new ObjectContent(typeof(object), oauthPayload, new JsonMediaTypeFormatter())
        };

        bearerTokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Environment.GetEnvironmentVariable(BotClientIdEnvironmentVariableName)}:{Environment.GetEnvironmentVariable(BotClientSecretEnvironmentVariableName)}")));

        HttpResponseMessage bearerTokenRespone = await httpClient.SendAsync(bearerTokenRequest);
        return JObject.Parse(await bearerTokenRespone.Content.ReadAsStringAsync()).GetValue("access_token").Value<string>();
    }

    private static async Task<string> GetMessageContent(ILogger log, HttpClient httpClient)
    {
        string? SeraphimDataUrl = Environment.GetEnvironmentVariable("SERAPHIM_DATA_URL");
        string seraphimApiUrl = $"{SeraphimDataUrl}/Test";
        HttpRequestMessage seraphimApiRequest = new HttpRequestMessage(HttpMethod.Get, seraphimApiUrl);

        HttpResponseMessage seraphimApiResponse;

        try
        {
            seraphimApiResponse = await httpClient.SendAsync(seraphimApiRequest);
        }
        catch (Exception ex)
        {
            log.LogError($"Failed to submit request to data layer: {ex}");
            throw;
        }

        return await seraphimApiResponse.Content.ReadAsStringAsync();
    }
}
