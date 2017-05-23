using System.Collections;

public static class EnumeratorExt {

    public static void Complete(this IEnumerator thisEnumerator)
    {
        while (thisEnumerator.MoveNext())
        {
            
        }        
    }
}
