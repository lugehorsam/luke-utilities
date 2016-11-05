using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnumeratorQueue : IEnumerator
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

    public object Current {
        get {
            return null;
        }
    }

    LinkedList<IEnumerator> nextEnumerators = new LinkedList<IEnumerator>();
    Stack<IEnumerator> oldEnumerators = new Stack<IEnumerator>();

    public bool MoveNext ()
    {
        LinkedListNode<IEnumerator> currentEnumerator = nextEnumerators.First;

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

    public void Enqueue (IEnumerator enumerator)
    {
        Diagnostics.Log ("Enqueuing enumerator", LogType.Tweening);
        nextEnumerators.AddLast (enumerator);
    }

    void MoveEnumeratorToStack (IEnumerator enumerator)
    {
        Diagnostics.Log ("Moving iterator to stack", LogType.Tweening);
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
