using Microsoft.AspNetCore.Mvc;

namespace Seraphim.Data;

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