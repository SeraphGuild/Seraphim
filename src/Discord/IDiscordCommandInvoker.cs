using LanguageExt;

namespace Discord;

public interface IDiscordCommandInvoker
{
    Task<Fin<TResult>> InvokeAsync<TResult>(ICommand<TResult> command);
}