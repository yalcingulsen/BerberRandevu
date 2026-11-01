namespace BerberRandevu.Api.Models;

public class Berber
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = string.Empty;
    public string? Uzmanlik { get; set; }
    public string? Telefon { get; set; }
    public string? FotoUrl { get; set; }
    public bool AktifMi { get; set; } = true;

    // Bir berberin birden çok randevusu, yorumu ve çalışma saati olabilir (bire-çok)
    public List<Randevu> Randevular { get; set; } = new();
    public List<Yorum> Yorumlar { get; set; } = new();
    public List<CalismaSaati> CalismaSaatleri { get; set; } = new();
}
