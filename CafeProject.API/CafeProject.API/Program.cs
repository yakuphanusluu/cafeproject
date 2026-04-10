using Microsoft.EntityFrameworkCore;
using CafeProject.API.Data;
// DİKKAT: Burada OpenApi veya Models ile ilgili HİÇBİR ŞEY YOK!

var builder = WebApplication.CreateBuilder(args);

// 1. VERİTABANI BAĞLANTISI (EF CORE)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. CORS POLİTİKASI (Frontend Köprüsü)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEdaCafe", policy =>
    {
        policy.WithOrigins("https://yakuphanuslu.com", "http://yakuphanuslu.com", "https://www.yakuphanuslu.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// 3. SWAGGER AYARLARI (Sıfır Models, Sıfır Baş Ağrısı)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // İÇİ BOMBOŞ! Kendi varsayılan ayarlarını kullanacak.

var app = builder.Build();

// 4. OTOMATİK MİGRASYON (500 Hatasını Önlemek İçin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Veritabanı Hazır: " + ex.Message);
    }
}

// 5. ARA YAZILIM (MIDDLEWARE) SIRALAMASI
app.UseSwagger();
app.UseSwaggerUI(); // İçi boş, standart arayüzü hatasız açar.

app.UseHttpsRedirection();

// KRİTİK: CORS her zaman Authorization'dan önce gelmeli!
app.UseCors("AllowEdaCafe");

app.UseAuthorization();

app.MapControllers();

app.Run();