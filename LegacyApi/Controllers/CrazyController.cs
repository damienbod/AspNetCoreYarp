using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LegacyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrazyController : ControllerBase
{
    private readonly ILogger<CrazyController> _logger;

    public CrazyController(ILogger<CrazyController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new List<string> { "crazy data from API which cannot be updated", "not public" };
    }
}
