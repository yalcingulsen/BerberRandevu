namespace BerberRandevu.Api.Models;

public class Randevu
{
    public int Id { get; set; }

    public int KullaniciId { get; set; }
    public Kullanici? Kullanici { get; set; }

    public int BerberId { get; set; }
    public Berber? Berber { get; set; }

    public int HizmetId { get; set; }
    public Hizmet? Hizmet { get; set; }

    public DateOnly Tarih { get; set; }
    public TimeOnly BaslangicSaati { get; set; }
    public RandevuDurumu Durum { get; set; } = RandevuDurumu.Bekliyor;
    public string? Not { get; set; }
}
