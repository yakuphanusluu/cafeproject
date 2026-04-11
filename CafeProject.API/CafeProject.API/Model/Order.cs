using System.Collections.Generic;

// Kendi projendeki isme göre namespace'i düzeltmeyi unutma kanka (Model veya Models olabilir)
namespace CafeProject.API.Model
{
    public class Order
    {
        public int Id { get; set; }

        // JS'den gelen hayati bilgiler
        public string? CustomerName { get; set; }
        public decimal TotalPrice { get; set; } // Fiyatın 0 gelmesini engelleyen kolon!

        // Kahve detayları
        public string? CoffeeType { get; set; }
        public string? Size { get; set; }
        public string? Concept { get; set; }
        public string? SyrupName { get; set; }
        // Diğer özelliklerin arasına (SyrupName'in falan altına) şunu ekle:
        public string? Extras { get; set; }
        public string? PaymentMethod { get; set; }

        // Barista ekranı için durum (Waiting, Preparing, Ready vb.)
        public string? Status { get; set; }

        // Sepetteki ürünlerin listesi
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}