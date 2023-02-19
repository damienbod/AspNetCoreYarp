using Microsoft.AspNetCore.Mvc;

namespace LegacyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrazyController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new List<string> { "crazy data from API which cannot be updated", "not public" };
    }
}
