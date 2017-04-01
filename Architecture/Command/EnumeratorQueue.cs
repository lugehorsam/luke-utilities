using System.Collections;
using System;
using System.Collections.Generic;

public class EnumeratorQueue : IEnumerator
{
    public event Action<IEnumerator, IEnumerator> OnNextEnumerator = (oldEnumerator, newEnumerator) => { };

    public int Count {
        get {
            return nextEnumerators.Count;
        }
    }

    public object Current
    {
        get { return currentEnumerator; }
    }

    readonly LinkedList<IEnumerator> nextEnumerators = new LinkedList<IEnumerator>();
    readonly Stack<IEnumerator> oldEnumerators = new Stack<IEnumerator>();

    private IEnumerator currentEnumerator;

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
            return MoveNext();
        } 
            
        return true;
    }

    public void Reset ()
    {
        throw new NotImplementedException ();
    }

    public void Add (IEnumerator enumerator)
    {
        nextEnumerators.AddLast (enumerator);
    }

    public void Add(Action action)
    {
        nextEnumerators.AddLast
        (
            ActionWrapper(action)
        );
    }

    IEnumerator ActionWrapper(Action action)
    {
        action();
        yield return null;
    }

    public void AddRange(IEnumerable<IEnumerator> enumerators)
    {
        foreach (IEnumerator enumerator in enumerators)
        {
            Add(enumerator);
        }
    }

    void MoveEnumeratorToStack (IEnumerator enumerator)
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

    public EnumeratorQueue(IEnumerable<IEnumerator> collection)
    {
        foreach (IEnumerator item in collection)
        {
            nextEnumerators.AddLast(item);
        }
    }

    public EnumeratorQueue()
    {

    }
}
