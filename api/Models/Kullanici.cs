namespace BerberRandevu.Api.Models;

public class Kullanici
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SifreHash { get; set; } = string.Empty;
    public string? Telefon { get; set; }
    public Rol Rol { get; set; } = Rol.Musteri;
    public DateTime KayitTarihi { get; set; } = DateTime.Now;

    // Bir kullanıcının birden çok randevusu ve yorumu olabilir (bire-çok)
    public List<Randevu> Randevular { get; set; } = new();
    public List<Yorum> Yorumlar { get; set; } = new();
}
