namespace BerberRandevu.Api.Models;

public class Hizmet
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public int SureDakika { get; set; }
    public decimal Fiyat { get; set; }
}
