namespace Seraphim.InteractionAcknowledgement
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using Sodium;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class Functions
    {
        [FunctionName("Acknowledge")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string BotPublicKey = Environment.GetEnvironmentVariable("BOT_PUBLIC_KEY") ?? string.Empty;

            if (req.Headers.TryGetValue("X-Signature-Ed25519", out StringValues signature) &&
                req.Headers.TryGetValue("X-Signature-Timestamp", out StringValues timestamp))
            {
                if (req.Body != null && req.Body.CanRead)
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
                            return new OkObjectResult(new
                            {
                                type = 1
                            });
                        }
                    }

                    log.LogWarning("Unable to validate result");
                    return new UnauthorizedResult();
                }

                log.LogWarning("No body provided in the given request.");
                return new BadRequestResult();
            }

            log.LogWarning("'X-Signature-Ed25519' and 'X-Signature-Timestamp' headers must be provided");
            return new UnauthorizedResult();
        }
    }
}
