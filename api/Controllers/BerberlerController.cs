using BerberRandevu.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BerberlerController : ControllerBase
{
    private readonly BerberDbContext _db;

    public BerberlerController(BerberDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var berberler = await _db.Berberler
            .Where(b => b.AktifMi)
            .Select(b => new { b.Id, b.AdSoyad, b.Uzmanlik, b.Telefon })
            .ToListAsync();
        return Ok(berberler);
    }

    private static readonly string[] GunAdlari =
        { "Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi" };

    // Berberi çalışma saatleriyle birlikte getirir (bire-çok ilişki)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var berber = await _db.Berberler
            .Include(b => b.CalismaSaatleri)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (berber == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            berber.Id,
            berber.AdSoyad,
            berber.Uzmanlik,
            berber.Telefon,
            berber.AktifMi,
            CalismaSaatleri = berber.CalismaSaatleri
                .OrderBy(c => c.Gun)
                .Select(c => new
                {
                    Gun = GunAdlari[(int)c.Gun],
                    Baslangic = c.BaslangicSaati.ToString("HH:mm"),
                    Bitis = c.BitisSaati.ToString("HH:mm")
                })
        });
    }
}
