using Discord;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.RegisterDiscordCoreServices();

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

IDiscordCommandInvoker commandInvoker = serviceProvider.GetRequiredService<IDiscordCommandInvoker>();

commandInvoker.Invoke<CreateGuildCommand, Guild>(new CreateGuildCommand());