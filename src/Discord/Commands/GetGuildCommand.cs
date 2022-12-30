namespace Discord;

public class GetGuildCommand : ICommand<Guild>
{
    public GetGuildCommand(Snowflake id)
    {
        Id = id;
    }

    public Snowflake Id { get; }
}