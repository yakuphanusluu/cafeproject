using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Adapters
{
    // Adaptee (Dış Sistem - Kendi formatı var)
    public class YemeksepetiService
    {
        // Temsili olarak dışarıdan gelen garip formatlı veri
        public string FetchOrderFromApi()
        {
            return "espresso-18-sutsuz-vanilya";
        }
    }
}
