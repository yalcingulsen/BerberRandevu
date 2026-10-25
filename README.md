# Berber Randevu Uygulaması

BLM4587 iOS ile Mobil Uygulama Geliştirme I (Flutter) dönem projesi. Müşterilerin telefonlarından berber randevusu almasını sağlayan bir mobil uygulama ve bu uygulamaya veri sağlayan Web API.

Hazırlayan: Yalçın Gülşen (bireysel) · GitHub: [yalcingulsen](https://github.com/yalcingulsen)

## Klasör yapısı

|Klasör|İçerik|
|-|-|
|`/api`|ASP.NET Core Web API (C#)|
|`/mobile`|Flutter mobil uygulama (Dart)|

## Kullanılan teknolojiler

* Mobil: Flutter (Dart), Android
* Web API: ASP.NET Core (.NET 10), Swagger
* Veritabanı: Microsoft SQL Server, Entity Framework Core (2. haftada eklenecek)

## Web API'yi çalıştırma

Gereken: .NET 10 SDK

```bash
cd api
dotnet restore
dotnet run --urls "http://localhost:5000"
```

Swagger arayüzü: http://localhost:5000/swagger

> Veritabanı kurulumu ve migration adımları 2. haftada bu bölüme eklenecek. Bağlantı dizesi gibi gizli bilgiler depoya konmaz.

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

* \[x] 1. hafta: Depo yapısı, Flutter karşılama ekranı, Web API iskeleti ve Swagger
* \[ ] 2. hafta: Veritabanı tasarımı, EF Core modelleri ve migration

