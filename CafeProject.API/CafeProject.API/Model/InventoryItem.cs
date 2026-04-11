using System;

namespace CafeProject.API.Model // Kendi namespace'ine göre ayarla kanka
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Örn: Espresso Çekirdeği, Karamel Şurubu
        public string Unit { get; set; } = string.Empty; // Örn: Paket, Şişe, Litre
        public decimal Quantity { get; set; } // Örn: 15 (Adet/Litre)
        public DateTime LastUpdated { get; set; } = DateTime.Now; // Baristanın sayım yaptığı tarih
    }
}