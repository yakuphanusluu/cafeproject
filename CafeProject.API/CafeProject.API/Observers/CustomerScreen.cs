using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Observers
{
    public class CustomerScreen : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[MÜŞTERİ NUMARATÖRÜ] Ding Dong! Bildirim: {message} -> Lütfen teslim alınız.");
        }
    }
}
