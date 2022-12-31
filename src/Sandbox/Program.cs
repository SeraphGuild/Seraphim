using Discord.API;
using Discord.Core;
using Discord.Core.Commands;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.RegisterDiscordCoreServices();

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

IDiscordCommandInvoker commandInvoker = serviceProvider.GetRequiredService<IDiscordCommandInvoker>();

GetGuildCommand command = new(new Snowflake("535354246715932682"));
Fin<Guild> result = await commandInvoker.InvokeAsync(command);

if (result.IsSucc)
{
    Guild guild = (Guild)result;
    Console.WriteLine(guild.Name);
    return;
}

Console.WriteLine(((Error)result).Message);