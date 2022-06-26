using System.ComponentModel.DataAnnotations;

namespace Seraphim.Discord;

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
