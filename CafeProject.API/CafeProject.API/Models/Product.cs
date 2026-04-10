namespace CafeProject.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } // Örn: Karamel Latte, Filtre Kahve
        public string Category { get; set; } // Örn: Kahve, Şurup, Ekstra
        public double Price { get; set; }
    }
}
