using System.Collections.Generic;

namespace CafeProject.API.Observers
{
    public class OrderStation
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            if (observer != null) _observers.Add(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}