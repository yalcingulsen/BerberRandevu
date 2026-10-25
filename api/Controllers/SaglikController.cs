using Microsoft.AspNetCore.Mvc;

namespace BerberRandevu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaglikController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { durum = "API çalışıyor", zaman = DateTime.Now });
    }
}
