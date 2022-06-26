using System.Text.Json.Serialization;

namespace Seraphim.Discord;

public class BulkCreateApplicationCommandRequest : CreateApplicationCommandRequest
{
    public BulkCreateApplicationCommandRequest(
        Snowflake applicationId,
        string name,
        string description,
        ApplicationCommandType? type = ApplicationCommandType.CHAT_INPUT,
        IList<ApplicationCommandOption>? options = null,
        bool defaultPermissions = true) : base(name, description, type, options, defaultPermissions)
    {
        this.ApplicationId = applicationId;
    }

    [JsonPropertyName("id")]
    public Snowflake ApplicationId { get; private set; }
}
