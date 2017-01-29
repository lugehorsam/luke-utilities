using System.Collections;
using System;
using System.Collections.Generic;

public class EnumeratorQueue<T> : IEnumerator<T>
    where T : IEnumerator
{
    public event Action<T, T> OnNextEnumerator = (oldEnumerator, newEnumerator) => { };

    public int Count {
        get {
            return nextEnumerators.Count;
        }
    }

    public T Current
    {
        get { return currentEnumerator; }
    }

    object IEnumerator.Current
    {
        get { return currentEnumerator; }
    }

    T IEnumerator<T>.Current {
        get { return currentEnumerator; }
    }

    readonly LinkedList<T> nextEnumerators = new LinkedList<T>();
    readonly Stack<T> oldEnumerators = new Stack<T>();

    private T currentEnumerator;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool MoveNext ()
    {
        if (nextEnumerators.First == null)
            return false;

        currentEnumerator = nextEnumerators.First.Value;

        if (currentEnumerator == null) {
            return false;
        }

        if (!currentEnumerator.MoveNext ()) {
            MoveEnumeratorToStack (currentEnumerator);
            return false;
        }

        return true;
    }

    public void Reset ()
    {
        throw new NotImplementedException ();
    }

    public void Add (T enumerator)
    {
        nextEnumerators.AddLast (enumerator);
    }

    public void AddRange(IEnumerable<T> enumerators)
    {
        foreach (T enumerator in enumerators)
        {
            Add(enumerator);
        }
    }

    void MoveEnumeratorToStack (T enumerator)
    {
        nextEnumerators.RemoveFirst ();
        oldEnumerators.Push (enumerator);
    }

    public void StopCurrentEnumerator ()
    {
        if (nextEnumerators.First != null) {
            MoveEnumeratorToStack (nextEnumerators.First.Value);
        }
    }
}

public class EnumeratorQueue : EnumeratorQueue<IEnumerator>
{
}

