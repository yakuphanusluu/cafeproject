using System;

namespace CafeProject.API.Observers
{
    // Kırmızı çizgiyi bu tam isim (Namespace) söndürür
    public class CustomerScreen : CafeProject.API.Observers.IObserver
    {
        public void Update(string message)
        {
            // Burası artık 'Müşteri Ekranı Logu' değil, gerçek bir işlem yapıyor
            Console.WriteLine($"[MÜŞTERİ EKRANI BİLDİRİMİ]: {message}");
        }
    }
}