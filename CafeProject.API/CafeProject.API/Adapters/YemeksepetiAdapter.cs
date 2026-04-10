using CafeProject.API.Facades; // .API eklendi
using CafeProject.API.Model;

namespace CafeProject.API.Adapters
{
    public class YemeksepetiAdapter
    {
        // OrderFacade'in public ve static olduğundan emin ol
        public void Process(Order order)
        {
            OrderFacade.CalculateFinalPrice(order);
        }
    }
}