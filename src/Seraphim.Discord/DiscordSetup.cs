using Microsoft.Extensions.DependencyInjection;

namespace Seraphim.Discord;

public static class DiscordSetup
{
    public static void Register(IServiceCollection container)
    {
        container.AddScoped<IDiscordClient, DiscordClient>();
    }
}
