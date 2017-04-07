using System;

public class State<T>
{
    public event Action<T, T> OnStateChanged = (arg1, arg2) => { };

    public T Property
    {
        get { return property; }
    }

    private T property;

    public static implicit operator T(State<T> thisState)
    {
        return thisState.property;
    }

    public void Set(T newProperty)
    {
        if (newProperty.Equals(property))
        {
            return;
        }

        T oldProperty = property;
        property = newProperty;
        OnStateChanged(oldProperty, newProperty);
    }
}
