using UnityEngine;
using System.Collections;
using System;

public static class MonoBehaviourExtensions {
    public static IEnumerator DoAfterDelay (this MonoBehaviour thisBehavior, float interval, Action doAfterDelay, bool repeat) {
        yield return new WaitForSeconds(interval);    
        doAfterDelay();
        thisBehavior.StartCoroutine(DoAfterDelay(thisBehavior, interval, doAfterDelay, repeat));
    }
}
