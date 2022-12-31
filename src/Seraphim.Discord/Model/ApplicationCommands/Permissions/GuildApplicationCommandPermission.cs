using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Discord.API;

public class GuildApplicationCommandPermission
{
    public GuildApplicationCommandPermission(
       Snowflake id,
       Snowflake applicationId,
       Snowflake guildId,
       IEnumerable<ApplicationCommandPermission> permissions)
    {
        Id = id;
        ApplicationId = applicationId;
        GuildId = guildId;
    }

    public Snowflake Id { get; private set; }

    [JsonPropertyName("application_id")]
    public Snowflake ApplicationId { get; private set; }

    /// <remarks>
    ///     Undefined for global commands.
    /// </remarks>
    [JsonPropertyName("guild_id")]
    public Snowflake? GuildId { get; private set; }

    public IEnumerable<ApplicationCommandPermission> Permissions { get; private set; }
}
