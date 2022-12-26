using LanguageExt.Common;

namespace Discord;

public abstract record ExceptionalDiscordErrors(string Message, int Code): Exceptional(Message, Code)
{
    public record NoKnownCommandExecutorError() : ExceptionalDiscordErrors("Unable to resolve an appropriate command executor", 0);
}
