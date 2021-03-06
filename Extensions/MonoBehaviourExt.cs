﻿namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public static class MonoBehaviourExt
    {
        public static IEnumerator DoAfterDelay(this MonoBehaviour thisBehavior, float interval, Action doAfterDelay, bool repeat)
        {
            yield return new WaitForSeconds(interval);

            doAfterDelay();
            thisBehavior.StartCoroutine(DoAfterDelay(thisBehavior, interval, doAfterDelay, repeat));
        }

        public static IEnumerator StartParallelCoroutines(this MonoBehaviour thisMonoBehavior, params IEnumerator[] enumerators)
        {
            var activeCoroutines = new Queue<Coroutine>(enumerators.Length - 1);

            foreach (IEnumerator enumerator in enumerators)
            {
                Coroutine coroutine = thisMonoBehavior.StartCoroutine(enumerator);
                activeCoroutines.Enqueue(coroutine);
            }

            while (activeCoroutines.Count > 0)
            {
                yield return activeCoroutines.Dequeue();
            }
        }
    }
}
