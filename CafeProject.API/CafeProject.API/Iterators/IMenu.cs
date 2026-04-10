using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Iterators
{
    // Tüm menüler bu kurala uyup kendi iterator'ını verecek
    public interface IMenu
    {
        IIterator CreateIterator();
    }
}
