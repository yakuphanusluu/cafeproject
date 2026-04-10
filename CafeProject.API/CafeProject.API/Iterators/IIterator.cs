using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Iterators
{
    // Standart dolaşma aracı
    public interface IIterator
    {
        bool HasNext(); // Sırada başka eleman var mı?
        MenuItem Next(); // Varsa getir ve bir sonrakine geç
    }
}
