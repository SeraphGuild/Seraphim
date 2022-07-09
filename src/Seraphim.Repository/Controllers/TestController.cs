using Microsoft.AspNetCore.Mvc;

namespace Seraphim.Repository;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok($"Hello From {Environment.UserName}");
    }
}