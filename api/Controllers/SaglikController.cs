using BerberRandevu.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaglikController : ControllerBase
{
    private readonly BerberDbContext _db;

    public SaglikController(BerberDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { durum = "API çalışıyor", zaman = DateTime.Now });
    }

    // Veritabanına bağlanabiliyor muyuz ve tablolarda kaç kayıt var?
    [HttpGet("veritabani")]
    public async Task<IActionResult> Veritabani()
    {
        var baglandi = await _db.Database.CanConnectAsync();
        if (!baglandi)
        {
            return StatusCode(503, new { durum = "Veritabanına bağlanılamadı" });
        }

        return Ok(new
        {
            durum = "Veritabanı bağlantısı başarılı",
            veritabani = _db.Database.GetDbConnection().Database,
            kayitSayilari = new
            {
                kullanicilar = await _db.Kullanicilar.CountAsync(),
                berberler = await _db.Berberler.CountAsync(),
                hizmetler = await _db.Hizmetler.CountAsync(),
                calismaSaatleri = await _db.CalismaSaatleri.CountAsync(),
                randevular = await _db.Randevular.CountAsync(),
                yorumlar = await _db.Yorumlar.CountAsync()
            }
        });
    }
}
