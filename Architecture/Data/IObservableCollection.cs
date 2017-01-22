using System.Collections.Generic;

public interface IObservableCollection<T>
{
    void RegisterObserver(ICollection<T> observer);
}
