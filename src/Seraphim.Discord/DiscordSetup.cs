using Microsoft.Extensions.DependencyInjection;

namespace Discord.API;

public static class DiscordSetup
{
    public static void Register(IServiceCollection container)
    {
        container.AddHttpClient<DiscordClient>().ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(string.Format("https://www.discord.com/api/v{0}/", 10));
        });
        container.AddScoped<IDiscordClient, DiscordClient>(sp =>
        {
            HttpClient httpClient = sp.GetRequiredService<HttpClient>();
            return new DiscordClient(httpClient, 10, "OTI1MTIzNDUxMjM1MjEzNDEy.GxNSSZ.bQdMJN343FkgPDg6rVGkKtna6OQSTlZFrikNKE");
        });
        container.AddScoped<IGuildClient, GuildClient>();
        container.AddScoped<IDiscordRequestMessageFactory, DiscordRequestMessageFactory>();
    }
}
