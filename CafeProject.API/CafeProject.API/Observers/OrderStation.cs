using CafeProject.Observers;
using System;
using System.Collections.Generic;

namespace CafeProject.Observers
{
    public class OrderStation : ISubject
    {
        // Kendisine bağlı olan ekranların listesi
        private List<IObserver> _observers = new List<IObserver>();
        private string _lastOrderState;

        // Ekrani sisteme bağla
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        // Ekrani sistemden çıkar (Mesela ekran bozulduysa)
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Listedenki tüm ekranlara aynı anda mesajı fırlat!
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_lastOrderState);
            }
        }

        // Sistemin durumunu değiştiren ve bildirimleri tetikleyen ana metot
        public void SetOrderState(string orderDetails)
        {
            _lastOrderState = orderDetails;
            Console.WriteLine($"\n*** İSTASYON: '{orderDetails}' durumu sisteme işlendi. Herkese haber veriliyor... ***");
            Notify();
        }
    }
}