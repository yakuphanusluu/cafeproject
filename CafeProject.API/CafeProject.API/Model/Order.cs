using System.Collections.Generic;

namespace CafeProject.API.Model // Kendi projene göre namespace'i düzelt kanka
{
    public class Order
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }
        public decimal TotalPrice { get; set; }

        // Sipariş Detayları
        public string? CoffeeType { get; set; }
        public string? Size { get; set; }
        public string? Concept { get; set; }
        public string? SyrupName { get; set; }
        public string? Extras { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }

        // YENİ EKLENEN: Global Z Raporu için kilit özellik!
        public bool IsClosed { get; set; } = false;

        // Sepet listesi
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}