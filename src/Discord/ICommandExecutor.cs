using LanguageExt;

namespace Discord;

internal interface ICommandExecutor<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Fin<TResult> Execute(TCommand command);
}