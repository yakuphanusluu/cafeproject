namespace CafeProject.API.Observers
{
    public interface ISubject
    {
        void Attach(IObserver observer); // Kendi IObserver'ın
        void Detach(IObserver observer);
        void Notify(string message);
    }
}