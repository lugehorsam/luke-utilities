using System.Collections;
using System;
using System.Collections.Generic;

public class EnumeratorQueue<T> : IEnumerator<T>
    where T : IEnumerator
{
    public bool Enumerating {
        get;
        private set;
    }
    
    public int Count {
        get {
            return nextEnumerators.Count;
        }
    }

    object IEnumerator.Current
    {
        get { return null; }
    }

     T IEnumerator<T>.Current {
        get {
            return default (T);
        }
    }

    readonly LinkedList<T> nextEnumerators = new LinkedList<T>();
    readonly Stack<T> oldEnumerators = new Stack<T>();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool MoveNext ()
    {
        LinkedListNode<T> currentEnumerator = nextEnumerators.First;

        if (currentEnumerator == null) {
            Enumerating = false;
            return false;
        }

        if (!currentEnumerator.Value.MoveNext ()) {
            MoveEnumeratorToStack (currentEnumerator.Value);
            Enumerating = false;
            return false;
        }

        Enumerating = true;
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
