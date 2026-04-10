using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Strategies
{
    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;

        // Kredi kartı stratejisi, çalışmak için kart numarasına ihtiyaç duyar
        public CreditCardPayment(string cardNumber)
        {
            _cardNumber = cardNumber;
        }

        public void Pay(double amount)
        {
            Console.WriteLine($"[ÖDEME] {amount} TL {_cardNumber} numaralı Kredi Kartı ile çekildi. Banka onayı alındı.");
        }
    }
}
