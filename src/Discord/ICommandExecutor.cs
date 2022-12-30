using LanguageExt;

namespace Discord;

internal interface ICommandExecutor<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<Fin<TResult>> ExecuteAsync(TCommand command);
}