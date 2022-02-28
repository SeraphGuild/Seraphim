using System.Text;
using Azure.Core;
using Azure.Messaging.ServiceBus;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

using Newtonsoft.Json.Linq;
using Sodium;

namespace Seraphim.Interactions;

public static class InteractionGateway
{
    const string InteractionsTopicName = "interactions";

    [FunctionName("Gateway")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger logger)
    {
        static async Task<IActionResult> OnValid(JObject request, ILogger logger)
        {
            await DispatchRequest(request, logger);
            return new AcceptedResult();
        }

        return await ValidateRequest(req) switch
        {
            InteractionSignatureValidationResult.Success success => await OnValid(success.MessageBody, logger),
            InteractionSignatureValidationResult.Failure => new UnauthorizedResult(),
            _ => new UnauthorizedResult()
        };
    }

    private static async Task<InteractionSignatureValidationResult> ValidateRequest(HttpRequest req)
    {
        string BotPublicKey = Environment.GetEnvironmentVariable("BOT_PUBLIC_KEY") ?? string.Empty;

        if (req.Headers.TryGetValue("X-Signature-Ed25519", out StringValues signature) &&
            req.Headers.TryGetValue("X-Signature-Timestamp", out StringValues timestamp))
        {
            string requestBody;
            bool isValid;

            try
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                isValid = req.Query.Keys.Contains("test") || PublicKeyAuth.VerifyDetached(
                    Convert.FromHexString(signature),
                    Encoding.UTF8.GetBytes(timestamp + requestBody),
                    Convert.FromHexString(BotPublicKey));
            }
            catch
            {
                return new InteractionSignatureValidationResult.Failure();
            }

            if (isValid)
            {
                return new InteractionSignatureValidationResult.Success(JObject.Parse(requestBody));
            }
        }

        return new InteractionSignatureValidationResult.Failure();
    }

    private static async Task DispatchRequest(JObject messageBody, ILogger logger)
    {

        if (!messageBody.ContainsKey("Data"))
        {
            logger.LogInformation($"Payload does not contain application command data. Cannot proceed. Payload: {messageBody}");
            return;
        }

        string namespaceConnectionString = Environment.GetEnvironmentVariable("SERAPHIM_SERVICE_BUS_CONNECTION_STRING") ?? string.Empty;
        ServiceBusSender serviceBusSender = new ServiceBusClient(namespaceConnectionString).CreateSender(InteractionsTopicName);
        ServiceBusMessage serviceBusMessage = GetTopicMessage(messageBody);

        try
        {
            await serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to push message to service bus topic: {ex}");
            return;
        }
    }

    private static ServiceBusMessage GetTopicMessage(JObject messageBody)
    {
        BinaryData payload = BinaryData.FromString(messageBody.ToString());
        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(payload)
        {
            ContentType = ContentType.ApplicationJson.ToString(),
        };

        serviceBusMessage.ApplicationProperties.Add("Name", messageBody["data"]["name"].Value<string>());
        return serviceBusMessage;
    }
}
