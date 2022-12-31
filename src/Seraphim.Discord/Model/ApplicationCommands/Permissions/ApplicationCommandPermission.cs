namespace Discord.API;

public class ApplicationCommandPermission
{
    public ApplicationCommandPermission(
       Snowflake id,
       ApplicationCommandPermissionType type,
       bool permission)
    {
        Id = id;
        Type = type;
        Permission = permission;
    }

    /// <summary>
    ///     The id of the user or role indicated by <see cref="Type"/>
    /// </summary>
    public Snowflake Id { get; private set; }

    /// <summary>
    ///     Indicates if the permissions is defined for a role or user
    /// </summary>
    public ApplicationCommandPermissionType Type { get; private set; }

    /// <summary>
    ///     True to allow, false to disallow
    /// </summary>
    public bool Permission { get; private set; }
}
