using Discord.API;
using Discord.Core.Commands;

namespace Discord.Core;

public class GetGuildCommand : ICommand<Guild>
{
    public GetGuildCommand(Snowflake id)
    {
        Id = id;
    }

    public Snowflake Id { get; }
}