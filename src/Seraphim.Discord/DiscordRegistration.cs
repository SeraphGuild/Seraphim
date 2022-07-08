using Microsoft.Extensions.DependencyInjection;

namespace Seraphim.Discord;

public static class DiscordRegistration
{
    public static void Register(IServiceCollection container)
    {
        container.AddScoped<IDiscordClient, DiscordClient>();
    }
}
