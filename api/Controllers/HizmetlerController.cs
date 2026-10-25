using BerberRandevu.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HizmetlerController : ControllerBase
{
    private static readonly List<Hizmet> _hizmetler = new()
    {
        new Hizmet { Id = 1, Ad = "Saç Kesimi", SureDakika = 30, Fiyat = 300 },
        new Hizmet { Id = 2, Ad = "Sakal Tıraşı", SureDakika = 15, Fiyat = 150 },
        new Hizmet { Id = 3, Ad = "Saç + Sakal", SureDakika = 45, Fiyat = 400 },
    };

    [HttpGet]
    public ActionResult<List<Hizmet>> GetAll()
    {
        return Ok(_hizmetler);
    }

    [HttpGet("{id}")]
    public ActionResult<Hizmet> GetById(int id)
    {
        var hizmet = _hizmetler.FirstOrDefault(h => h.Id == id);
        if (hizmet == null)
        {
            return NotFound();
        }
        return Ok(hizmet);
    }
}
