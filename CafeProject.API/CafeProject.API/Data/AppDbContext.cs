using Microsoft.EntityFrameworkCore;
using CafeProject.API.Models;

namespace CafeProject.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Örnek verileri buraya ekliyoruz (Senin istediğin o Karamel Latte burada olacak!)
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Filtre Kahve", Category = "Kahve", Price = 40 },
                new Product { Id = 2, Name = "Espresso", Category = "Kahve", Price = 45 },
                new Product { Id = 3, Name = "Latte", Category = "Kahve", Price = 50 },
                new Product { Id = 4, Name = "Karamel", Category = "Şurup", Price = 15 },
                new Product { Id = 5, Name = "Fıstık", Category = "Şurup", Price = 18 },
                new Product { Id = 6, Name = "Süt", Category = "Ekstra", Price = 10 }
            );
        }
    }
}