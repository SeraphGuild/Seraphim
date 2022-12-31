using Discord.Core.Errors;
using LanguageExt;

namespace Discord.Core.Commands;

internal class DiscordCommandInvoker : IDiscordCommandInvoker
{
    private readonly IServiceProvider commandExecutorProvider;

    public DiscordCommandInvoker(IServiceProvider commandExecutorProvider)
    {
        this.commandExecutorProvider = commandExecutorProvider;
    }

    public Task<Fin<TResult>> InvokeAsync<TResult>(ICommand<TResult> command)
    {
        Type executorInterfaceType = typeof(ICommandExecutor<,>).MakeGenericType(command.GetType(), typeof(TResult));
        dynamic? commandExecutor = commandExecutorProvider.GetService(executorInterfaceType);

        if (commandExecutor == null)
        {
            return Task.FromResult((Fin<TResult>)new ExceptionalDiscordError.NoKnownCommandExecutorError());
        }

        return commandExecutor.ExecuteAsync((dynamic)command);
    }
}
