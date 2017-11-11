namespace Utilities
{
    using System;
    using System.Collections;

    public static class LTTweenExt 
    {
        public static IEnumerator AsEnumerator(this LTDescr thisDescr)
        {
            bool hasFinished = false;
            thisDescr.setOnComplete(() => hasFinished = true);
            
            while (!hasFinished)
            {
                yield return null;
            }
        }
    }
}