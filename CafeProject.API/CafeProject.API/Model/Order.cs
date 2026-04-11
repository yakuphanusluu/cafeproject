using System.Collections.Generic;

namespace CafeProject.API.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerUsername { get; set; }
        public decimal TotalPrice { get; set; }

        // Hata veren alanlar ve diğer detaylar:
        public string? CoffeeType { get; set; }
        public string? Size { get; set; }      // İşte build'i bozan kahraman!
        public string? Concept { get; set; }
        public string? SyrupName { get; set; }
        public string? Extras { get; set; }

        public string? Status { get; set; }    // Waiting, Preparing, Ready, Delivered
        public string? PaymentMethod { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool UsedPoints { get; set; } = false;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}