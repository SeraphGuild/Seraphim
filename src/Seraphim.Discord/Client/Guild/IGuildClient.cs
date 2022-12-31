using Discord.Core;
using LanguageExt;

namespace Discord.API;

public interface IGuildClient
{
    Task<Fin<Guild>> GetGuildAsync(Snowflake guildId);
}