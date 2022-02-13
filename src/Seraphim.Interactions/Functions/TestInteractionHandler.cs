using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;

namespace Seraphim.Interactions;

public static class TestInteractionHandler
{
    const string DiscordApiBaseUrl = "https://discord.com/api/v9";

    [FunctionName("TestInteractionHandler")]
    public static async Task Run(
        [ServiceBusTrigger(
            topicName: "interactions",
            subscriptionName: "Test",
            Connection = "SERAPHIM_SERVICE_BUS_CONNECTION_STRING")]
        string myQueueItem, 
        ILogger log)
    {
        JObject interaction = JObject.Parse(myQueueItem);

        HttpClient httpClient = new HttpClient();

        string seraphimApiUrl = "http://seraphimdata/Test";
        HttpRequestMessage seraphimApiRequest = new HttpRequestMessage(HttpMethod.Get, seraphimApiUrl);

        HttpResponseMessage seraphimApiResponse;

        try
        {
            seraphimApiResponse = await httpClient.SendAsync(seraphimApiRequest);
        }
        catch (Exception ex)
        {
            log.LogError($"Failed to submit request to stroage layer: {ex}");
            throw;
        }

        string seraphimApiMessage = await seraphimApiResponse.Content.ReadAsStringAsync();

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

        await httpClient.SendAsync(request);
        log.LogInformation("Successfully sent message to discord");
    }
}
