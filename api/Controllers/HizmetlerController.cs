using BerberRandevu.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HizmetlerController : ControllerBase
{
    private readonly BerberDbContext _db;

    public HizmetlerController(BerberDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hizmetler = await _db.Hizmetler
            .Select(h => new { h.Id, h.Ad, h.Aciklama, h.SureDakika, h.Fiyat })
            .ToListAsync();
        return Ok(hizmetler);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hizmet = await _db.Hizmetler
            .Where(h => h.Id == id)
            .Select(h => new { h.Id, h.Ad, h.Aciklama, h.SureDakika, h.Fiyat })
            .FirstOrDefaultAsync();

        if (hizmet == null)
        {
            return NotFound();
        }
        return Ok(hizmet);
    }
}
