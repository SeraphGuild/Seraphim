using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace Seraphim.Interactions;

public static class PingHandler
{
    const string DiscordApiBaseUrl = "https://discord.com";
    const string DiscordApiBasePath = "api/v9";
    const string BotClientIdEnvironmentVariableName = "BOT_CLIENT_ID";
    const string BotClientSecretEnvironmentVariableName = "BOT_CLIENT_SECRET";

    [FunctionName("Ping")]
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
        await RespondToInteraction(httpClient, log, myQueueItem, seraphimApiMessage, bearerToken);
    }

    private static async Task RespondToInteraction(HttpClient httpClient, ILogger log, string myQueueItem, string seraphimApiMessage, string bearerToken)
    {
        JObject interaction = JObject.Parse(myQueueItem);

        log.LogInformation($"requst: {JsonConvert.SerializeObject(myQueueItem)}");

        string interactionResponseUrl = $"{DiscordApiBaseUrl}/{DiscordApiBasePath}/webhooks/{interaction["application_id"].Value<string>()}/{interaction["token"].Value<string>()}/messages/@original";
        object interactionResponsePayload = new
        {
            content = seraphimApiMessage
        };

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, interactionResponseUrl)
        {
            Content = new ObjectContent(typeof(object), interactionResponsePayload, new JsonMediaTypeFormatter())
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        HttpResponseMessage replyResponse;

        try
        {
            replyResponse = await httpClient.SendAsync(request);
        }
        catch (Exception ex)
        {
            log.LogError($"Failed to send interaction response to discord. ex: {ex}");
            throw;
        }

        log.LogInformation($"response: {JsonConvert.SerializeObject(replyResponse)}");

        if (replyResponse.Content.Headers.ContentLength != 0)
        {
            JObject responseContent = JObject.Parse(await replyResponse.Content.ReadAsStringAsync());
            log.LogInformation($"response content: {JsonConvert.SerializeObject(responseContent)}");
        }

        if (replyResponse.IsSuccessStatusCode == false)
        {
            log.LogError("Failed to send message to discord");
        }
        else
        {
            log.LogInformation("Successfully sent message to discord");
        }
    }

    private static async Task<string> GetBearerToken(HttpClient httpClient)
    {
        UriBuilder uriBuilder = new UriBuilder(DiscordApiBaseUrl)
        {
            Path = $"{DiscordApiBasePath}/oauth2/token",
            Query = "grant_type=client_credentials&scope=applications.commands applications.commands.update"
        };

        HttpRequestMessage bearerTokenRequest = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
        bearerTokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Environment.GetEnvironmentVariable(BotClientIdEnvironmentVariableName)}:{Environment.GetEnvironmentVariable(BotClientSecretEnvironmentVariableName)}")));

        HttpResponseMessage bearerTokenRespone = await httpClient.SendAsync(bearerTokenRequest);
        string content = await bearerTokenRespone.Content.ReadAsStringAsync();
        return JObject.Parse(content).GetValue("access_token").Value<string>();
    }

    private static async Task<string> GetMessageContent(ILogger log, HttpClient httpClient)
    {
        string? SeraphimRepoUrl = Environment.GetEnvironmentVariable("SERAPHIM_REPO_URL");
        string seraphimApiUrl = $"{SeraphimRepoUrl}/Test";
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
