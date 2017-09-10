namespace Utilities 
{
    using System.Collections;
    
    public static class EnumeratorExt 
    {
        public static void Run(this IEnumerator thisEnumerator)
        {
            while (thisEnumerator.MoveNext())
            {
                
            }        
        }
    }
}
