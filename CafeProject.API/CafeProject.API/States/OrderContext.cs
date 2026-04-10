using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.States
{
    public class OrderContext
    {
        // Siparişin o anki durumu
        private IOrderState _currentState;

        // Sipariş ilk açıldığında varsayılan bir durumla başlar
        public OrderContext(IOrderState initialState)
        {
            _currentState = initialState;
        }

        // Durumu dışarıdan değiştirmek için (State sınıfları kullanacak)
        public void SetState(IOrderState newState)
        {
            _currentState = newState;
        }

        // Aksiyonları o anki duruma (State nesnesine) havale ediyoruz
        public void NextState()
        {
            _currentState.Next(this);
        }

        public void CancelOrder()
        {
            _currentState.Cancel(this);
        }
    }
}
