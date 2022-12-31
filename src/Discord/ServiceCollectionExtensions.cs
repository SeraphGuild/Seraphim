using Discord.API;
using Discord.Core.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Discord.Core;

public static class ServiceCollectionExtensions
{
    public static void RegisterDiscordCoreServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDiscordCommandInvoker, DiscordCommandInvoker>();
        serviceCollection.AddScoped<ICommandExecutor<GetGuildCommand, Guild>, GetGuildCommandExecutor>();

        DiscordSetup.Register(serviceCollection);
    }
}
