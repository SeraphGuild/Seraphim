using Discord;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.RegisterDiscordCoreServices();

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

IDiscordCommandInvoker commandInvoker = serviceProvider.GetRequiredService<IDiscordCommandInvoker>();

GetGuildCommand command = new(new Snowflake("1234567890"));
Fin<Guild> result = await commandInvoker.InvokeAsync(command);

if (result.IsSucc)
{
    Console.WriteLine("Hello World");
    return;
}

Console.WriteLine(((Error)result).Message);