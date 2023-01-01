using Azure.Core;
using Azure.Messaging.ServiceBus;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Sodium;
using System.Text;

namespace Seraphim.InteractionGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class InteractionsController : ControllerBase
{
    private readonly ILogger<InteractionsController> logger;

    const string InteractionsTopicName = "interactions";

    public InteractionsController(ILogger<InteractionsController> logger)
    {
        this.logger = logger;
    }

    [HttpGet(Name = "Gateway")]
    public async Task<IActionResult> Get(HttpRequest req)
    {
        Fin<JObject> result = await ValidateRequest(req);

        if (result.IsFail)
        {
            return new UnauthorizedResult();
        }

        JObject validationRequestBody = (JObject)result;

        logger.LogInformation("Request validated. Prcoeeding.");

        if (validationRequestBody["type"].Value<int>() == 1)
        {
            logger.LogInformation("Payload identified as a PING message. Short circuiting reply.");
            return new OkObjectResult(new
            {
                type = 1
            });
        }

        await DispatchRequest(validationRequestBody, logger);

        logger.LogInformation("Successfully delegated application command to service bus topic");
        return new OkObjectResult(new
        {
            type = 5
        });
    }

    private static async Task<Fin<JObject>> ValidateRequest(HttpRequest req)
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
            catch(Exception ex)
            {
                return Error.New(ex);
            }

            if (isValid)
            {
                return JObject.Parse(requestBody);
            }
        }

        return new Expected($"The given cryptographic signatures could not be verified", 0);
    }

    private static async Task DispatchRequest(JObject messageBody, ILogger logger)
    {
        string namespaceConnectionString = Environment.GetEnvironmentVariable("SERAPHIM_SERVICE_BUS_CONNECTION_STRING") ?? string.Empty;
        ServiceBusSender serviceBusSender = new ServiceBusClient(namespaceConnectionString).CreateSender(InteractionsTopicName);
        ServiceBusMessage serviceBusMessage = GetTopicMessage(messageBody);

        try
        {
            logger.LogInformation($"Pushing application command into service bus topic. Command Name: {serviceBusMessage.ApplicationProperties["name"]}");
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
        ServiceBusMessage serviceBusMessage = new(payload)
        {
            ContentType = ContentType.ApplicationJson.ToString(),
        };

        serviceBusMessage.ApplicationProperties.Add("name", messageBody["data"]["name"].Value<string>());
        return serviceBusMessage;
    }
}