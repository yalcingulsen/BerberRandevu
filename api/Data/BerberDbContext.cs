using BerberRandevu.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevu.Api.Data;

public class BerberDbContext : DbContext
{
    public BerberDbContext(DbContextOptions<BerberDbContext> options) : base(options)
    {
    }

    // Her DbSet veritabanında bir tablo olur
    public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
    public DbSet<Berber> Berberler => Set<Berber>();
    public DbSet<Hizmet> Hizmetler => Set<Hizmet>();
    public DbSet<Randevu> Randevular => Set<Randevu>();
    public DbSet<Yorum> Yorumlar => Set<Yorum>();
    public DbSet<CalismaSaati> CalismaSaatleri => Set<CalismaSaati>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kullanıcılar
        modelBuilder.Entity<Kullanici>(e =>
        {
            e.Property(k => k.AdSoyad).HasMaxLength(100).IsRequired();
            e.Property(k => k.Email).HasMaxLength(150).IsRequired();
            e.HasIndex(k => k.Email).IsUnique();
            e.Property(k => k.Telefon).HasMaxLength(20);
            e.Property(k => k.Rol).HasConversion<string>().HasMaxLength(20);
        });

        // Berberler
        modelBuilder.Entity<Berber>(e =>
        {
            e.Property(b => b.AdSoyad).HasMaxLength(100).IsRequired();
            e.Property(b => b.Uzmanlik).HasMaxLength(100);
            e.Property(b => b.Telefon).HasMaxLength(20);
            e.Property(b => b.FotoUrl).HasMaxLength(300);
        });

        // Hizmetler
        modelBuilder.Entity<Hizmet>(e =>
        {
            e.Property(h => h.Ad).HasMaxLength(100).IsRequired();
            e.Property(h => h.Aciklama).HasMaxLength(300);
            e.Property(h => h.Fiyat).HasPrecision(10, 2);
        });

        // Randevular: kullanıcı, berber ve hizmete bağlı (çok-bir)
        modelBuilder.Entity<Randevu>(e =>
        {
            e.Property(r => r.Durum).HasConversion<string>().HasMaxLength(20);
            e.Property(r => r.Not).HasMaxLength(300);

            e.HasOne(r => r.Kullanici)
                .WithMany(k => k.Randevular)
                .HasForeignKey(r => r.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Berber)
                .WithMany(b => b.Randevular)
                .HasForeignKey(r => r.BerberId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Hizmet)
                .WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HizmetId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aynı berbere, aynı gün ve saatte iki randevu verilemez
            e.HasIndex(r => new { r.BerberId, r.Tarih, r.BaslangicSaati }).IsUnique();
        });

        // Yorumlar: kullanıcı ve berbere bağlı (çok-bir)
        modelBuilder.Entity<Yorum>(e =>
        {
            e.Property(y => y.Metin).HasMaxLength(500);
            e.ToTable(t => t.HasCheckConstraint("CK_Yorum_Puan", "[Puan] BETWEEN 1 AND 5"));

            e.HasOne(y => y.Kullanici)
                .WithMany(k => k.Yorumlar)
                .HasForeignKey(y => y.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(y => y.Berber)
                .WithMany(b => b.Yorumlar)
                .HasForeignKey(y => y.BerberId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Çalışma saatleri: berbere bağlı (çok-bir)
        modelBuilder.Entity<CalismaSaati>(e =>
        {
            e.HasOne(c => c.Berber)
                .WithMany(b => b.CalismaSaatleri)
                .HasForeignKey(c => c.BerberId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OrnekVerileriEkle(modelBuilder);
    }

    // Örnek (seed) veriler: migration ile tabloya otomatik eklenir
    private static void OrnekVerileriEkle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hizmet>().HasData(
            new Hizmet { Id = 1, Ad = "Saç Kesimi", Aciklama = "Yıkama dahil klasik kesim", SureDakika = 30, Fiyat = 300 },
            new Hizmet { Id = 2, Ad = "Sakal Tıraşı", Aciklama = "Ustura ile sakal tıraşı", SureDakika = 15, Fiyat = 150 },
            new Hizmet { Id = 3, Ad = "Saç + Sakal", Aciklama = "Saç kesimi ve sakal tıraşı birlikte", SureDakika = 45, Fiyat = 400 },
            new Hizmet { Id = 4, Ad = "Çocuk Saç Kesimi", Aciklama = "12 yaş altı", SureDakika = 20, Fiyat = 200 }
        );

        modelBuilder.Entity<Berber>().HasData(
            new Berber { Id = 1, AdSoyad = "Ahmet Usta", Uzmanlik = "Klasik kesim", Telefon = "0555 111 22 33", AktifMi = true },
            new Berber { Id = 2, AdSoyad = "Mehmet Usta", Uzmanlik = "Sakal ve ustura", Telefon = "0555 444 55 66", AktifMi = true },
            new Berber { Id = 3, AdSoyad = "Can Usta", Uzmanlik = "Modern kesim", Telefon = "0555 777 88 99", AktifMi = true }
        );

        // Her berber Pazartesi-Cumartesi 09:00-19:00 çalışıyor
        var saatler = new List<CalismaSaati>();
        var id = 1;
        for (var berberId = 1; berberId <= 3; berberId++)
        {
            for (var gun = 1; gun <= 6; gun++)
            {
                saatler.Add(new CalismaSaati
                {
                    Id = id++,
                    BerberId = berberId,
                    Gun = (DayOfWeek)gun,
                    BaslangicSaati = new TimeOnly(9, 0),
                    BitisSaati = new TimeOnly(19, 0)
                });
            }
        }
        modelBuilder.Entity<CalismaSaati>().HasData(saatler);
    }
}
