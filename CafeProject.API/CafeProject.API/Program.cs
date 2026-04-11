using Microsoft.EntityFrameworkCore;
using CafeProject.API.Data; // Eğer AppDbContext farklı bir klasördeyse burayı düzenle

var builder = WebApplication.CreateBuilder(args);

// 1. MYSQL BAĞLANTI AYARI
// Bağlantı hatasını (Unable to connect) migration sırasında aşmak için versiyonu sabitliyoruz.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// 2. CORS AYARI (Frontend sitene izin veriyoruz)
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. OTOMATİK MİGRASYON VE TABLO OLUŞTURMA
// Bu blok Render'da uygulama ilk açıldığında Hostinger'da tabloları oluşturur.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        Console.WriteLine("--- VERİTABANI BAĞLANTISI VE MİGRASYON BAŞARILI ---");
    }
    catch (Exception ex)
    {
        Console.WriteLine("--- MİGRASYON HATASI: " + ex.Message);
    }
}

// 4. PIPELINE (SIRALAMA KRİTİK)
if (app.Environment.IsDevelopment() || true) // Render'da Swagger'ı görmek için true kalsın
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Hataları detaylı görmek için
}

app.UseHttpsRedirection();

app.UseCors("AllowEdaCafe"); // CORS, Auth'dan önce olmalı.

app.UseAuthorization();

app.MapControllers();

app.Run();