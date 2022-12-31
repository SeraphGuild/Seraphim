using LanguageExt;

namespace Discord.Core.Commands;

public interface IDiscordCommandInvoker
{
    Task<Fin<TResult>> InvokeAsync<TResult>(ICommand<TResult> command);
}