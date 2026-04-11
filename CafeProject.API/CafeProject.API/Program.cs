using CafeProject.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CafeProject.API.Data;// Kendi projene göre AppDbContext'in olduğu yeri (örn: using CafeProject.API.Data;) buraya ekle:
// using CafeProject.API.Data; 

var builder = WebApplication.CreateBuilder(args);

// 1. VERİTABANI BAĞLANTISI (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    // Eğer veritabanı kurulumu yapmıyorsan EnableRetryOnFailure silebilirsin
    ));

// 2. CORS AYARI (Tarayıcı engeline takılmamak için)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// 3. JSON AYARLARI (İşte Barista ekranındaki sonsuz döngüyü kıran hayat kurtarıcı kod!)
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// 4. ÇALIŞTIRMA SIRASI (Sıralama çok kritik kanka, bozma)
app.UseRouting();

app.UseCors("AllowAll"); // CORS her zaman UseRouting'den SONRA gelir!

app.UseAuthorization();
app.MapControllers();

app.Run();