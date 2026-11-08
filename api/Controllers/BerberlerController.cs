using BerberRandevu.Api.Data;
using BerberRandevu.Api.Dtos;
using BerberRandevu.Api.Models;
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

    private static readonly string[] GunAdlari =
        { "Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi" };

    private static BerberDto DtoyaCevir(Berber b) => new()
    {
        Id = b.Id,
        AdSoyad = b.AdSoyad,
        Uzmanlik = b.Uzmanlik,
        Telefon = b.Telefon,
        FotoUrl = b.FotoUrl,
        AktifMi = b.AktifMi
    };

    // LİSTELE: varsayılan olarak sadece aktif berberler, ?tumu=true ile hepsi
    [HttpGet]
    public async Task<ActionResult<List<BerberDto>>> GetAll([FromQuery] bool tumu = false)
    {
        var sorgu = _db.Berberler.AsQueryable();
        if (!tumu)
        {
            sorgu = sorgu.Where(b => b.AktifMi);
        }

        var berberler = await sorgu.OrderBy(b => b.Id).ToListAsync();
        return Ok(berberler.Select(DtoyaCevir));
    }

    // TEK BERBER: çalışma saatleriyle birlikte (bire-çok ilişki)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var berber = await _db.Berberler
            .Include(b => b.CalismaSaatleri)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (berber == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı berber bulunamadı." });
        }

        return Ok(new
        {
            berber.Id,
            berber.AdSoyad,
            berber.Uzmanlik,
            berber.Telefon,
            berber.FotoUrl,
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

    // EKLE: yeni berbere Pazartesi-Cumartesi 09:00-19:00 çalışma saati de eklenir
    [HttpPost]
    public async Task<ActionResult<BerberDto>> Create(BerberKaydetDto dto)
    {
        var berber = new Berber
        {
            AdSoyad = dto.AdSoyad.Trim(),
            Uzmanlik = dto.Uzmanlik?.Trim(),
            Telefon = dto.Telefon?.Trim(),
            FotoUrl = dto.FotoUrl?.Trim(),
            AktifMi = dto.AktifMi
        };

        for (var gun = 1; gun <= 6; gun++)
        {
            berber.CalismaSaatleri.Add(new CalismaSaati
            {
                Gun = (DayOfWeek)gun,
                BaslangicSaati = new TimeOnly(9, 0),
                BitisSaati = new TimeOnly(19, 0)
            });
        }

        _db.Berberler.Add(berber);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = berber.Id }, DtoyaCevir(berber));
    }

    // GÜNCELLE
    [HttpPut("{id}")]
    public async Task<ActionResult<BerberDto>> Update(int id, BerberKaydetDto dto)
    {
        var berber = await _db.Berberler.FindAsync(id);
        if (berber == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı berber bulunamadı." });
        }

        berber.AdSoyad = dto.AdSoyad.Trim();
        berber.Uzmanlik = dto.Uzmanlik?.Trim();
        berber.Telefon = dto.Telefon?.Trim();
        berber.FotoUrl = dto.FotoUrl?.Trim();
        berber.AktifMi = dto.AktifMi;

        await _db.SaveChangesAsync();
        return Ok(DtoyaCevir(berber));
    }

    // SİL: randevusu olan berber silinmez, randevu geçmişi kaybolmasın
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var berber = await _db.Berberler.FindAsync(id);
        if (berber == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı berber bulunamadı." });
        }

        var randevusuVar = await _db.Randevular.AnyAsync(r => r.BerberId == id);
        if (randevusuVar)
        {
            return Conflict(new { mesaj = "Bu berberin randevuları var, silinemez. Bunun yerine pasif yapabilirsiniz (AktifMi = false)." });
        }

        _db.Berberler.Remove(berber);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
