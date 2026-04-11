using System;

namespace CafeProject.API.Model // Kendi namespace'ine göre ayarla
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int LoyaltyPoints { get; set; } = 0;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}