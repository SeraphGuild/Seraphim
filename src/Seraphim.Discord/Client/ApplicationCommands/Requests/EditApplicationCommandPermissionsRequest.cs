using System.ComponentModel.DataAnnotations;

namespace Discord.API;

public class EditApplicationCommandPermissionsRequest
{
    public EditApplicationCommandPermissionsRequest(
        IEnumerable<ApplicationCommandPermission> permissions)
    {
        this.Permissions = permissions;
    }

    [MaxLength(10)]
    public IEnumerable<ApplicationCommandPermission> Permissions { get; private set; }
}
