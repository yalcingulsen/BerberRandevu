namespace BerberRandevu.Api.Models;

public class CalismaSaati
{
    public int Id { get; set; }

    public int BerberId { get; set; }
    public Berber? Berber { get; set; }

    public DayOfWeek Gun { get; set; }
    public TimeOnly BaslangicSaati { get; set; }
    public TimeOnly BitisSaati { get; set; }
}
