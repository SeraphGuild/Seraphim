using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Seraphim.Repository;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly SeraphimContext SeraphimContext;

    public TeamController(SeraphimContext seraphimContext)
    {
        this.SeraphimContext = seraphimContext;
    }

    [HttpGet]
    public ActionResult<List<Team>> Get()
    {
        return Ok(this.SeraphimContext.Teams.ToList());
    }

    [HttpGet("{name}")]
    public ActionResult<Team> Fetch(string name)
    {
        IQueryable<Team> filteredTeams = this.SeraphimContext.Teams
            .Where(t => t.Name == name);

        return filteredTeams.Any() ? Ok(filteredTeams.First()) : NoContent();
    }

    [HttpPost("{name}")]
    public async Task<ActionResult<Team>> Post(string name)
    {
        EntityEntry<Team> team = this.SeraphimContext.Teams.Add(new Team(name));
        await this.SeraphimContext.SaveChangesAsync();
        return team.Entity;
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult<Team>> Delete(string name)
    {
        IQueryable<Team> filteredTeams = this.SeraphimContext.Teams
            .Where(t => t.Name == name);

        if (filteredTeams.Any())
        {
            Team team = this.SeraphimContext.Remove(filteredTeams.First()).Entity;
            await this.SeraphimContext.SaveChangesAsync();
            return team;
        }

        return NoContent();
    }
}