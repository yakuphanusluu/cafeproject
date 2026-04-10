using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.AbstractFactories
{
    // Fabrikaların sözleşmesi: Her fabrika bir süt ve bir şurup üretmek zorundadır.
    public interface IIngredientsFactory
    {
        IMilk CreateMilk();
        ISyrupIngredient CreateSyrup();
    }
}
