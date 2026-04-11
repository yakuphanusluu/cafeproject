using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;

namespace CafeProject.API.Data // Kendi proje adına göre ayarla
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // YENİ EKLENEN MÜDAVİM TABLOSU
        public DbSet<Customer> Customers { get; set; }
    }
}