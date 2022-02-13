using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seraphim.Data;

[Table("TeamMember")]
[Owned]
public class TeamMember
{
    public TeamMember(long discordId, TeamRank rank, Team team)
    {
        this.DiscordId = discordId;
        this.Rank = rank;
        this.Team = team;
        this.TeamId = team.Id;
    }

    /// <summary>
    ///     EF constructor
    /// </summary>
    private TeamMember()
    {
        this.Team = null!;
    }

    [Column(Order = 0), Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long DiscordId { get; private set; }

    [Column(Order = 1), ForeignKey("Team"), Required]
    public int TeamId { get; private set; }

    public TeamRank Rank { get; private set; }

    [InverseProperty("Members")]
    public Team Team { get; private set; }
}