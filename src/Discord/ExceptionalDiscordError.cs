using LanguageExt.Common;

namespace Discord;

public abstract record ExceptionalDiscordError(string Message, int Code): Exceptional(Message, Code)
{
    public record NoKnownCommandExecutorError() : ExceptionalDiscordError("Unable to resolve an appropriate command executor", 0);
}
