namespace Discord.API;

public class BatchEditApplicationCommandPermissionsRequest : EditApplicationCommandPermissionsRequest
{
    public BatchEditApplicationCommandPermissionsRequest(
        Snowflake id,
        IEnumerable<ApplicationCommandPermission> permissions) : base(permissions)
    {
        this.Id = id;
    }

    public Snowflake Id { get; private set; }
}
