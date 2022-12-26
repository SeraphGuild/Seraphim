using LanguageExt;

namespace Discord;

internal class DiscordCommandInvoker : IDiscordCommandInvoker
{
    private readonly IServiceProvider commandExecutorProvider;

    public DiscordCommandInvoker(IServiceProvider commandExecutorProvider)
    {
        this.commandExecutorProvider = commandExecutorProvider;
    }

    public Fin<TResult> Invoke<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        Type executorInterfaceType = typeof(ICommandExecutor<,>).MakeGenericType(typeof(TCommand), typeof(TResult));
        ICommandExecutor<TCommand, TResult>? commandExecutor =
            commandExecutorProvider.GetService(executorInterfaceType) as ICommandExecutor<TCommand, TResult>;

        if (commandExecutor == null)
        {
            return new ExceptionalDiscordErrors.NoKnownCommandExecutorError();
        }

        return commandExecutor.Execute(command);
    }
}
