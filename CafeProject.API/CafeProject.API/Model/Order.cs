using System.Collections.Generic;

namespace CafeProject.API.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerUsername { get; set; } // YENİ: Hesaba puan işlemek için
        public decimal TotalPrice { get; set; }

        public string? CoffeeType { get; set; }
        public string? Size { get; set; }
        public string? Concept { get; set; }
        public string? SyrupName { get; set; }
        public string? Extras { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool UsedPoints { get; set; } = false;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}