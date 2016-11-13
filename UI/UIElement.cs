using UnityEngine;
using System.Collections;

public class UIElement : GameBehavior {

    public virtual IEnumerator OnModuleEnter ()
    {
        yield return null;
    }

    public virtual IEnumerator OnModuleExit ()
    {
        yield return null;
    }

    protected sealed override void OnAwake ()
    {
        gameObject.SetActive (false);
        OnUIElementAwake();
    }

    protected virtual void OnUIElementAwake()
    {

    }
}
