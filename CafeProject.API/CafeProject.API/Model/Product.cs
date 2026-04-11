namespace CafeProject.API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        // YENİ: Stok özelliği (Varsayılan olarak dükkana 50 adet eklenir)
        public int Stock { get; set; } = 50;
    }
}