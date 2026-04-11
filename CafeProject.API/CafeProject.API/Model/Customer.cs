using System;

namespace CafeProject.API.Model // Kendi namespace'ine göre ayarla kanka
{
    public class Customer
    {
        public int Id { get; set; }

        // Müşteriyi tanımak için en sağlam yol telefon numarasıdır
        public string PhoneNumber { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        // Biriken puanlar (Örn: Her kahve siparişinde 1 puan)
        public int LoyaltyPoints { get; set; } = 0;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}