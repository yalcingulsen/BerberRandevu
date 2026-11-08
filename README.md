# Berber Randevu Uygulaması

BLM4587 iOS ile Mobil Uygulama Geliştirme I (Flutter) dönem projesi. Müşterilerin telefonlarından berber randevusu almasını sağlayan bir mobil uygulama ve bu uygulamaya veri sağlayan Web API.

Hazırlayan: Yalçın Gülşen (bireysel) · GitHub: [yalcingulsen](https://github.com/yalcingulsen)

## Klasör yapısı

| Klasör | İçerik |
|---|---|
| `/api` | ASP.NET Core Web API (C#) |
| `/mobile` | Flutter mobil uygulama (Dart) |

## Kullanılan teknolojiler

- Mobil: Flutter (Dart), Android
- Web API: ASP.NET Core (.NET 10), Swagger
- Veritabanı: Microsoft SQL Server (geliştirmede LocalDB), Entity Framework Core 10 (code-first, migration)

## Veritabanı tabloları

| Tablo | İlişki |
|---|---|
| Kullanicilar | Randevular ve Yorumlar ile bire-çok |
| Berberler | Randevular, Yorumlar ve CalismaSaatleri ile bire-çok |
| Hizmetler | Randevular ile bire-çok |
| Randevular | Kullanici, Berber ve Hizmet ile çok-bir |
| Yorumlar | Kullanici ve Berber ile çok-bir |
| CalismaSaatleri | Berber ile çok-bir |

Örnek (seed) veriler: 4 hizmet, 3 berber ve her berber için Pazartesi-Cumartesi 09:00-19:00 çalışma saatleri.

## Veritabanı kurulumu

Gereken: .NET 10 SDK ve SQL Server (Express veya LocalDB).

1. EF Core komut aracını kur (bir kez):

```bash
dotnet tool install --global dotnet-ef
```

2. Bağlantı dizesini **user-secrets** ile ayarla. Bağlantı dizesi depoya konmaz, sadece geliştiricinin bilgisayarında saklanır:

```bash
cd api
dotnet user-secrets set "ConnectionStrings:BerberDb" "Server=(localdb)\MSSQLLocalDB;Database=BerberRandevuDb;Trusted_Connection=True;TrustServerCertificate=True"
```

SQL Server Express kullanılıyorsa `Server=(localdb)\MSSQLLocalDB` yerine `Server=.\SQLEXPRESS` yazılır.

3. Migration'ları veritabanına uygula (tablolar ve örnek veriler oluşur):

```bash
dotnet ef database update
```

## Web API'yi çalıştırma

```bash
cd api
dotnet run --urls "http://localhost:5000"
```

Swagger arayüzü: http://localhost:5000/swagger

Veritabanı kontrolü: `GET /api/Saglik/veritabani` her tablodaki kayıt sayısını gösterir.

### Uç noktalar (endpoints)

| Yöntem | Adres | Ne yapar |
|---|---|---|
| GET | `/api/Berberler` | Aktif berberleri listeler (`?tumu=true` ile pasifler de gelir) |
| GET | `/api/Berberler/{id}` | Tek berberi çalışma saatleriyle getirir |
| POST | `/api/Berberler` | Berber ekler (Pzt-Cmt 09:00-19:00 çalışma saatleri otomatik eklenir) |
| PUT | `/api/Berberler/{id}` | Berberi günceller |
| DELETE | `/api/Berberler/{id}` | Berberi siler (randevusu varsa 409 döner) |
| GET | `/api/Hizmetler` | Hizmetleri listeler |
| GET | `/api/Hizmetler/{id}` | Tek hizmeti getirir |
| POST | `/api/Hizmetler` | Hizmet ekler (aynı adla ikinci hizmet eklenemez) |
| PUT | `/api/Hizmetler/{id}` | Hizmeti günceller |
| DELETE | `/api/Hizmetler/{id}` | Hizmeti siler (randevuda kullanıldıysa 409 döner) |
| GET | `/api/Saglik` | API çalışıyor mu |
| GET | `/api/Saglik/veritabani` | Veritabanı bağlantısı ve kayıt sayıları |

Ekleme ve güncellemede veriler DTO sınıflarıyla alınır (`api/Dtos`) ve DataAnnotations ile doğrulanır. Hatalı veri gelirse API `400 Bad Request` ile hangi alanın neden hatalı olduğunu Türkçe olarak döner.

Örnek doğrulama kuralları:

- Berber: ad soyad zorunlu (3-100 karakter), telefon `0555 111 22 33` biçiminde, fotoğraf adresi geçerli bir bağlantı
- Hizmet: ad zorunlu (2-100 karakter), süre 5-240 dakika, fiyat 1-10000 TL

## Mobil uygulamayı çalıştırma

Gereken: Flutter SDK (geliştirmede kullanılan sürüm: `3.47.8`), Android Studio ve bir Android emülatörü

```bash
cd mobile
flutter pub get
flutter run
```

### API adresinin ayarlanması

Android emülatörü bilgisayardaki API'ye `localhost` yerine `10.0.2.2` adresiyle ulaşır. API adresi, mobil uygulamaya API bağlantısı eklendiğinde (4. hafta) tek bir ayar dosyasından değiştirilebilecek: `http://10.0.2.2:5000`

## Haftalık ilerleme

- [x] 1. hafta: Depo yapısı, Flutter karşılama ekranı, Web API iskeleti ve Swagger
- [x] 2. hafta: Veritabanı tasarımı, EF Core varlık sınıfları ve ilişkiler, ilk migration, örnek veriler, user-secrets
- [x] 3. hafta: Berber ve Hizmet için tam CRUD, DTO ve veri doğrulama
- [ ] 4. hafta: Flutter klasör yapısı, model sınıfları, API servis katmanı (dio), go_router, berber listesi ve detay ekranları
