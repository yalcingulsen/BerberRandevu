using BerberRandevu.Api.Data;
using BerberRandevu.Api.Dtos;
using BerberRandevu.Api.Models;
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

    private static HizmetDto DtoyaCevir(Hizmet h) => new()
    {
        Id = h.Id,
        Ad = h.Ad,
        Aciklama = h.Aciklama,
        SureDakika = h.SureDakika,
        Fiyat = h.Fiyat
    };

    // LİSTELE
    [HttpGet]
    public async Task<ActionResult<List<HizmetDto>>> GetAll()
    {
        var hizmetler = await _db.Hizmetler.OrderBy(h => h.Id).ToListAsync();
        return Ok(hizmetler.Select(DtoyaCevir));
    }

    // TEK HİZMET
    [HttpGet("{id}")]
    public async Task<ActionResult<HizmetDto>> GetById(int id)
    {
        var hizmet = await _db.Hizmetler.FindAsync(id);
        if (hizmet == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı hizmet bulunamadı." });
        }
        return Ok(DtoyaCevir(hizmet));
    }

    // EKLE: aynı adla ikinci hizmet eklenemez
    [HttpPost]
    public async Task<ActionResult<HizmetDto>> Create(HizmetKaydetDto dto)
    {
        var ad = dto.Ad.Trim();
        if (await _db.Hizmetler.AnyAsync(h => h.Ad == ad))
        {
            return Conflict(new { mesaj = $"\"{ad}\" adında bir hizmet zaten var." });
        }

        var hizmet = new Hizmet
        {
            Ad = ad,
            Aciklama = dto.Aciklama?.Trim(),
            SureDakika = dto.SureDakika,
            Fiyat = dto.Fiyat
        };

        _db.Hizmetler.Add(hizmet);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = hizmet.Id }, DtoyaCevir(hizmet));
    }

    // GÜNCELLE
    [HttpPut("{id}")]
    public async Task<ActionResult<HizmetDto>> Update(int id, HizmetKaydetDto dto)
    {
        var hizmet = await _db.Hizmetler.FindAsync(id);
        if (hizmet == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı hizmet bulunamadı." });
        }

        var ad = dto.Ad.Trim();
        if (await _db.Hizmetler.AnyAsync(h => h.Ad == ad && h.Id != id))
        {
            return Conflict(new { mesaj = $"\"{ad}\" adında başka bir hizmet zaten var." });
        }

        hizmet.Ad = ad;
        hizmet.Aciklama = dto.Aciklama?.Trim();
        hizmet.SureDakika = dto.SureDakika;
        hizmet.Fiyat = dto.Fiyat;

        await _db.SaveChangesAsync();
        return Ok(DtoyaCevir(hizmet));
    }

    // SİL: randevularda kullanılmış hizmet silinmez
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var hizmet = await _db.Hizmetler.FindAsync(id);
        if (hizmet == null)
        {
            return NotFound(new { mesaj = $"{id} numaralı hizmet bulunamadı." });
        }

        if (await _db.Randevular.AnyAsync(r => r.HizmetId == id))
        {
            return Conflict(new { mesaj = "Bu hizmet randevularda kullanılmış, silinemez." });
        }

        _db.Hizmetler.Remove(hizmet);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
