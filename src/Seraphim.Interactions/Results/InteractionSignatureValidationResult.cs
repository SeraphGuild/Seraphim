using Newtonsoft.Json.Linq;

namespace Seraphim.Interactions;

public abstract record InteractionSignatureValidationResult()
{
    public record Success(JObject MessageBody): InteractionSignatureValidationResult();

    public record Failure(): InteractionSignatureValidationResult();
}
