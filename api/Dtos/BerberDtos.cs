using System.ComponentModel.DataAnnotations;

namespace BerberRandevu.Api.Dtos;

// API'nin dışarıya gönderdiği berber bilgisi
public class BerberDto
{
    public int Id { get; set; }
    public string AdSoyad { get; set; } = string.Empty;
    public string? Uzmanlik { get; set; }
    public string? Telefon { get; set; }
    public string? FotoUrl { get; set; }
    public bool AktifMi { get; set; }
}

// Berber eklerken ve güncellerken API'ye gönderilen bilgi
public class BerberKaydetDto
{
    [Required(ErrorMessage = "Ad soyad boş olamaz.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad soyad 3 ile 100 karakter arasında olmalı.")]
    public string AdSoyad { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Uzmanlık en fazla 100 karakter olabilir.")]
    public string? Uzmanlik { get; set; }

    [RegularExpression(@"^0\d{3} ?\d{3} ?\d{2} ?\d{2}$", ErrorMessage = "Telefon 0555 111 22 33 biçiminde olmalı.")]
    public string? Telefon { get; set; }

    [Url(ErrorMessage = "Fotoğraf adresi geçerli bir bağlantı olmalı.")]
    [StringLength(300, ErrorMessage = "Fotoğraf adresi en fazla 300 karakter olabilir.")]
    public string? FotoUrl { get; set; }

    public bool AktifMi { get; set; } = true;
}
