using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour {

    public virtual IEnumerator OnModuleEnter ()
    {
        yield return null;
    }

    public virtual IEnumerator OnModuleExit ()
    {
        yield return null;
    }

    protected void Awake ()
    {
        gameObject.SetActive (false);

    }
}
