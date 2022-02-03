using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Sodium;
using System.Text;

namespace Seraphim.Interactions;

public static class InteractionGateway
{
    [FunctionName("Gateway")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        return await ValidateEd25519Signature(req, log) switch
        {
            true => new OkObjectResult(new { type = 1 }),
            false => new UnauthorizedResult()
        };
    }

    private static async Task<bool> ValidateEd25519Signature(HttpRequest req, ILogger log)
    {
        string BotPublicKey = Environment.GetEnvironmentVariable("BOT_PUBLIC_KEY") ?? string.Empty;

        if (req.Headers.TryGetValue("X-Signature-Ed25519", out StringValues signature) &&
            req.Headers.TryGetValue("X-Signature-Timestamp", out StringValues timestamp))
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            bool isValid = PublicKeyAuth.VerifyDetached(
                Convert.FromHexString(signature),
                Encoding.UTF8.GetBytes(timestamp + requestBody),
                Convert.FromHexString(BotPublicKey));

            if (isValid)
            {
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                if (data.type == 1)
                {
                    log.LogInformation("acknowing discord validation request");
                    return true;
                }
            }
        }

        log.LogWarning("Unable to validate result");
        return false;
    }
}
