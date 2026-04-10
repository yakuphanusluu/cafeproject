using System;

namespace CafeProject.API.Observers
{
    public class BaristaScreen : IObserver
    {
        public void Update(string message)
        {
            // Barista ekranına düşen log
            Console.WriteLine($"[BARİSTA] Bildirim alındı: {message}");
        }
    }
}