using Microsoft.Extensions.DependencyInjection;

namespace Discord;

public static class ServiceCollectionExtensions
{
    public static void RegisterDiscordCoreServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDiscordCommandInvoker, DiscordCommandInvoker>();
        serviceCollection.AddScoped<ICommandExecutor<CreateGuildCommand, Guild>, CreateGuildCommandExecutor>();
    }
}
