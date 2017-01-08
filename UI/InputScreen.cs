using UnityEngine;
using System.Collections;

public abstract class InputScreen : GameBehavior {

    protected abstract bool WaitForInput {
        get;
    }

    public IEnumerator Show ()
    {
            
        gameObject.SetActive (true);
        WaitForInput:
            while (WaitForInput) {
                yield return null;
            }
            yield return StartCoroutine(HandleInput ());
            if (ValidInput ()) {
                yield return StartCoroutine (Hide ());
            } else {
                goto WaitForInput;
            }
    }

    IEnumerator Hide ()
    {

        yield return StartCoroutine(HideTransition());
        gameObject.SetActive (false);
    }

    protected virtual IEnumerator HandleInput ()
    {
        yield return null;
    }

    protected virtual bool ValidInput ()
    {
        return true;
    }

    protected virtual IEnumerator ShowTransition () 
    {
        yield return null;
    }

    protected virtual IEnumerator HideTransition () 
    {
        yield return null;
    }
}
