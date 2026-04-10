using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Observers
{
    // Haber verecek olan merkez (İstasyon) bu şablona uyacak
    public interface ISubject
    {
        void Attach(IObserver observer); // Abone ekle
        void Detach(IObserver observer); // Abone çıkar
        void Notify();                   // Herkese haber ver!
    }
}
