AspNetCoreIdentityApp.Web
ASP.NET Core Identity ile geliştirilen bu proje, kullanıcı kimlik doğrulama, yetkilendirme ve rol yönetimi işlemlerini destekleyen, çoklu dil desteğine sahip bir web uygulamasıdır.

🚀 Özellikler
ASP.NET Core Identity ile kullanıcı yönetimi

Kayıt, giriş, çıkış, e-posta doğrulama

Şifre sıfırlama ve doğrulama kodu gönderimi

Role dayalı yetkilendirme

Özel kullanıcı validasyonları

Çoklu dil desteği (Localization)

ViewModel tabanlı yapı ile temiz veri aktarımı

Gelişmiş hata yönetimi

Entity Framework Core ile veritabanı işlemleri

🧰 Kullanılan Teknolojiler
ASP.NET Core 8

Entity Framework Core

SQL Server (EF Migrations ile yapılandırılabilir)

ASP.NET Identity

Razor Pages / MVC

C#

Bootstrap

jQuery (Libman üzerinden yönetiliyor)

📂 Proje Yapısı
Controllers/: Account, Member ve Admin işlemleri için controller'lar

Areas/: Admin paneli için alan tanımı

Models/: Uygulama içi veri modelleri

ViewModels/: Kullanıcıdan alınan/verilen veriler için ViewModel'ler

Services/: Custom kullanıcı işlemleri (örneğin EmailService)

Localizations/: Dil dosyaları (Resource bazlı yapı)

Extensions/: Identity ve servisler için extension methodlar

CustomValidations/: Özel IUserValidator ve IPasswordValidator sınıfları

⚙️ Kurulum
1. Projeyi Klonlayın
git clone https://github.com/UmutSahinkaya/AspNetCoreIdentityApp.Web.git
cd AspNetCoreIdentityApp.Web
2. Bağımlılıkları Kurun
Visual Studio kullanıyorsanız otomatik restore edilir. Aksi takdirde:
dotnet restore
3. Veritabanını Oluşturun
   appsettings.json dosyasındaki connection string'e göre bir veritabanı tanımlayın ve ardından migrations'u uygulayın:
   dotnet ef database update
4. Uygulamayı Başlatın
dotnet run
Uygulama https://localhost:5001 veya http://localhost:5000 üzerinden çalışacaktır.

🔐 Kimlik Doğrulama
Uygulama, ASP.NET Identity yapısını kullanarak:

E-posta ile kayıt

Rol atama (Admin / Member)

Şifre sıfırlama

E-posta onayı

gibi işlemleri destekler.

🌐 Dil Desteği
Türkçe ve İngilizce dil desteği mevcut

Localizations/ klasöründeki .resx dosyalarıyla özelleştirilebilir

🧪 Test Kullanıcısı
Varsayılan bir admin kullanıcısı migration sırasında oluşturulabilir (yapılandırmaya bağlı olarak UserManager üzerinden seed işlemi yapılabilir).
