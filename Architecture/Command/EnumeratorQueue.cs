using System.Collections;
using System;
using System.Collections.Generic;

public class EnumeratorQueue<T> : IEnumerator<T>
    where T : IEnumerator
{
    public int Count {
        get {
            return nextEnumerators.Count;
        }
    }

    public T Current
    {
        get { return Current; }
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

