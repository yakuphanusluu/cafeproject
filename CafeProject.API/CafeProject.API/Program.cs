using Microsoft.EntityFrameworkCore;
using CafeProject.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. MYSQL VERİTABANI BAĞLANTISI (Pomelo Paketi Gerektirir)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. CORS POLİTİKASI (Frontend Erişimi)
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

// 3. SWAGGER AYARLARI (Models Çakışmasını Önlemek İçin Sade Bırakıldı)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. OTOMATİK MİGRASYON (MySQL Tablolarını Otomatik Oluşturur)
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
        Console.WriteLine("Veritabanı Hazır veya Hata: " + ex.Message);
    }
}

// 5. MIDDLEWARE SIRALAMASI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// CORS, Authorization'dan Önce Gelmeli!
app.UseCors("AllowEdaCafe");

app.UseAuthorization();

app.MapControllers();

app.Run();