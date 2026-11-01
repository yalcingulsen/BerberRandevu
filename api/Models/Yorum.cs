namespace BerberRandevu.Api.Models;

public class Yorum
{
    public int Id { get; set; }

    public int KullaniciId { get; set; }
    public Kullanici? Kullanici { get; set; }

    public int BerberId { get; set; }
    public Berber? Berber { get; set; }

    public int Puan { get; set; }
    public string? Metin { get; set; }
    public DateTime Tarih { get; set; } = DateTime.Now;
}
