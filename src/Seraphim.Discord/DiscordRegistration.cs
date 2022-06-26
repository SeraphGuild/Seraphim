using SimpleInjector;

namespace Seraphim.Discord;

public static class DiscordRegistration
{
    public static void Register(Container container)
    {
        container.Register<IDiscordClient>().As<DiscordClient>();
    }
}
