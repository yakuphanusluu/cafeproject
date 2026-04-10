using Microsoft.EntityFrameworkCore;
using global::CafeProject.API.Data; // Veritabanımızın adresi

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı (SQLite) Bağlantısını Ekliyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cafe.db"));

// 2. CORS Ayarı (Arayüzümüzün API ile hata vermeden konuşabilmesi için)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// 3. Standart API ve Swagger Servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Çalışma Zamanı (Middleware) Ayarları
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// CORS'u aktif ediyoruz (Sırası çok önemlidir, tam burada olmalı)
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();