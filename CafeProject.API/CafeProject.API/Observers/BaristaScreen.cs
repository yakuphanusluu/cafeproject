using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Observers
{
    public class BaristaScreen : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[BARİSTA EKRANI] Yeni Bildirim: {message} -> Hemen hazırlamaya başlıyorum!");
        }
    }
}
