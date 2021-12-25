using Microsoft.AspNetCore.Mvc;
using Seraphim.Model;
using Seraphim.Storage;

namespace Seraphim.Controllers;

[ApiController]
[Route("[controller]")]
public class GuildController : ControllerBase
{
    private readonly SeraphimContext SeraphimContext;

    public GuildController(SeraphimContext seraphimContext)
    {
        this.SeraphimContext = seraphimContext;
    }

    [HttpGet(Name = "Guild")]
    public List<Team> Get()
    {
        return this.SeraphimContext.Team?.ToList() ?? new List<Team>();
    }
}