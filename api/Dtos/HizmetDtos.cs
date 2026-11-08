using System.ComponentModel.DataAnnotations;

namespace BerberRandevu.Api.Dtos;

// API'nin dışarıya gönderdiği hizmet bilgisi
public class HizmetDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string? Aciklama { get; set; }
    public int SureDakika { get; set; }
    public decimal Fiyat { get; set; }
}

// Hizmet eklerken ve güncellerken API'ye gönderilen bilgi
public class HizmetKaydetDto
{
    [Required(ErrorMessage = "Hizmet adı boş olamaz.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Hizmet adı 2 ile 100 karakter arasında olmalı.")]
    public string Ad { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir.")]
    public string? Aciklama { get; set; }

    [Range(5, 240, ErrorMessage = "Süre 5 ile 240 dakika arasında olmalı.")]
    public int SureDakika { get; set; }

    [Range(typeof(decimal), "1", "10000", ErrorMessage = "Fiyat 1 ile 10000 TL arasında olmalı.")]
    public decimal Fiyat { get; set; }
}
