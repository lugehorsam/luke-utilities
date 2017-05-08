using System;

public interface IObservable<T>
{
    event Action<T> OnAdd;
    event Func<T, bool> OnRemove;
}
