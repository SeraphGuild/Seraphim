using Microsoft.EntityFrameworkCore;

namespace Seraphim.Data;

public class SeraphimContext : DbContext
{
    public DbSet<Team> Teams => Set<Team>();

    public SeraphimContext(DbContextOptions<SeraphimContext> contextOptions)
        : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(teamBuilder =>
        {
            teamBuilder.OwnsMany(t => t.Members, teamMemberBuilder =>
            {
                teamMemberBuilder.HasKey(tm => new { tm.TeamId, tm.DiscordId });
            });
        });
    }
}
