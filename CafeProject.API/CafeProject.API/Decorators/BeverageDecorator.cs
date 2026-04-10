using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Decorators
{
    // Temel Süsleyici Sınıf
    public abstract class BeverageDecorator : IBeverage
    {
        // Protected yapıyoruz ki miras alan alt sınıflar (süt, şurup) erişebilsin
        protected IBeverage _beverage;

        public BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        // Temel içeceğin metotlarını çağırıyoruz (override edilebilir)
        public virtual string GetDescription() => _beverage.GetDescription();
        public virtual double GetCost() => _beverage.GetCost();
    }
}
