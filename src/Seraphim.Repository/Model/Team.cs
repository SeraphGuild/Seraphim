using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seraphim.Repository;

[Table("Team")]
public class Team
{
    public Team(string name, int id = default, List<TeamMember>? members = default)
    {
        this.Id = id;
        this.Name = name;
        this.Members = members ?? new List<TeamMember>();
    }

    /// <summary>
    ///     EF constructor
    /// </summary>
    private Team()
    {
        this.Name = string.Empty;
        this.Members = new List<TeamMember>();
    }

    [Column(Order = 0), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required, MinLength(2), MaxLength(24)]
    public string Name { get; private set; }

    public List<TeamMember> Members { get; private set; }
}
