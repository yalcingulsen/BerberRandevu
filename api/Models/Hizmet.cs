namespace BerberRandevu.Api.Models;

public class Hizmet
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string? Aciklama { get; set; }
    public int SureDakika { get; set; }
    public decimal Fiyat { get; set; }

    // Bir hizmet birden çok randevuda seçilebilir (bire-çok)
    public List<Randevu> Randevular { get; set; } = new();
}
