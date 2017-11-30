namespace Utilities
{
    using System;
    using System.Collections;

    public static class DelegateExt
    {
        public static IEnumerator AsEnumerator(this Action thisAction)
        {
            var hasInvoked = false;

            thisAction += () =>
            {
                hasInvoked = true;
            };

            while (!hasInvoked)
            {
                yield return null;
            }
        }
    }
}
