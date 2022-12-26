using LanguageExt;

namespace Discord;

public interface IDiscordCommandInvoker
{
    Fin<TResult> Invoke<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
}