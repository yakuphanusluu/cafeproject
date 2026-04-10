using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Observers
{
    // Haber bekleyen ekranlar bu şablona uyacak
    public interface IObserver
    {
        void Update(string message);
    }
}
