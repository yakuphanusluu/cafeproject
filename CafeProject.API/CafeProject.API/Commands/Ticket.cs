using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Commands
{
    public class Ticket
    {
        private List<string> _orders = new List<string>();

        // Ürün ekleme
        public void AddOrder(string item)
        {
            _orders.Add(item);
            Console.WriteLine($"[ADİSYON] '{item}' sepete eklendi.");
        }

        // Ürün çıkarma
        public void RemoveOrder(string item)
        {
            if (_orders.Contains(item))
            {
                _orders.Remove(item);
                Console.WriteLine($"[ADİSYON İPTAL] '{item}' sepetten çıkarıldı.");
            }
        }

        // Fişi ekrana yazdırma
        public void PrintTicket()
        {
            Console.WriteLine("\n--- GÜNCEL ADİSYON ---");
            foreach (var order in _orders)
            {
                Console.WriteLine($"- {order}");
            }
            Console.WriteLine("----------------------\n");
        }
    }
}
