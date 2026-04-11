using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
namespace CafeProject.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}