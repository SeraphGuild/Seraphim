using LanguageExt;
using LanguageExt.Common;

namespace Discord;

internal class GetGuildCommandExecutor : ICommandExecutor<GetGuildCommand, Guild>
{
    public Task<Fin<Guild>> ExecuteAsync(GetGuildCommand command)
    {
        return Task.FromResult((Fin<Guild>)Error.New("test"));
    }
}
