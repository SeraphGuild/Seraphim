using Discord.API;
using Discord.Core.Commands;
using LanguageExt;

namespace Discord.Core;

internal class GetGuildCommandExecutor : ICommandExecutor<GetGuildCommand, Guild>
{
    private readonly IGuildClient guildClient;
    
    public GetGuildCommandExecutor(IGuildClient guildClient)
    {
        this.guildClient = guildClient;
    }

    public async Task<Fin<Guild>> ExecuteAsync(GetGuildCommand command)
    {
        return await this.guildClient.GetGuildAsync(command.Id);
    }
}
