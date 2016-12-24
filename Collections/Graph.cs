using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Graph<T> : IEnumerable<T> {

    Dictionary<T, List<T>> adjacencyLists = new Dictionary<T, List<T>>();

    public void Add(T element, T adjacentElement) {
        if (!adjacencyLists.ContainsKey (element)) {
            adjacencyLists [element] = new List<T> ();
        }
        adjacencyLists [element].Add (adjacentElement);
    }

    public T[] GetAllElements() {
        return GetAllTargets ().Concat (GetAllOrigins ()).ToArray();   
    }
        
    public T[] GetAllWithTarget(T element) {
        List<T> targetElements = new List<T> ();
        foreach (KeyValuePair<T, List<T>> adjacencyList in adjacencyLists) {
            if (adjacencyList.Value.Contains (element)) {
                targetElements.Add (adjacencyList.Key);
            }
        }
        return targetElements.ToArray ();
    }
                   
    public T[] GetAllWithOrigin(T element) {
        return adjacencyLists [element].ToArray ();
    }
        
    public T[] GetAllOrigins() {
        return adjacencyLists.Keys.ToArray ();
    }

    public T[] GetAllTargets() {
        List<T> targets = new List<T> ();
        IEnumerator<T> enumerator = this.GetEnumerator (includeOrigins: false);
        while (enumerator.Current != null) {
            targets.Add (enumerator.Current);
            enumerator.MoveNext ();
        }
        return targets.ToArray ();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator (includeOrigins: true);
    }
        
    public IEnumerator<T> GetEnumerator(bool includeOrigins) {
        HashSet<T> viewedElements = new HashSet<T> ();
        T[] origins = GetAllOrigins ();
        for (int i = 0; i < origins.Length; i++) {
            T origin = origins [i];
            if (includeOrigins && viewedElements.Add (origin)) {
                yield return origin;
            }
            T[] targetsForOrigin = GetAllWithOrigin(origin);
            for (int j = 0; j < targetsForOrigin.Length; j++) {
                T target = targetsForOrigin [j];
                if (viewedElements.Add(target)) {
                    yield return target;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
