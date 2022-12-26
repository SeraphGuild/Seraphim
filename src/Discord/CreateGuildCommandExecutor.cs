using LanguageExt;
using LanguageExt.Common;

namespace Discord;

internal class CreateGuildCommandExecutor : ICommandExecutor<CreateGuildCommand, Guild>
{
    public Fin<Guild> Execute(CreateGuildCommand command)
    {
        return Error.New("test");
    }
}
