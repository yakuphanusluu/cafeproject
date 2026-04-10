using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Strategies
{
    public class CashPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"[ÖDEME] {amount} TL Nakit olarak alındı. Para üstü hesaplanıyor...");
        }
    }
}
