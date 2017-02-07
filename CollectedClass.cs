using System.Collections.Generic;

public class CollectedClass<T> where T : CollectedClass<T>
{

    public static List<T> Collection {
        get {
            return collection;
        }
    }

    private static readonly List<T> collection = new List<T>();

    public CollectedClass()
    {
         collection.Add(this as T);
    }
}
