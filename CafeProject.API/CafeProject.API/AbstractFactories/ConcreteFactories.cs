using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.AbstractFactories
{
    // Sadece standart ürünler veren fabrika
    public class StandardIngredientsFactory : IIngredientsFactory
    {
        public IMilk CreateMilk() => new CowMilk();
        public ISyrupIngredient CreateSyrup() => new StandardSyrup();
    }

    // Sadece vegan ürünler veren fabrika (Hata yapıp inek sütü verme ihtimalini sıfıra indirir)
    public class VeganIngredientsFactory : IIngredientsFactory
    {
        public IMilk CreateMilk() => new OatMilk();
        public ISyrupIngredient CreateSyrup() => new VeganSyrup();
    }
}
