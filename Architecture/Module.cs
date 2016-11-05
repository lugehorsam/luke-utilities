using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public abstract class Module : MonoBehaviour
{

    public UIElement [] UIElements {
        get {
            return uiElements.ToArray ();
        }
    }

    List<UIElement> uiElements = new List<UIElement> ();

    void Awake ()
    {
        uiElements.AddRange (GetComponentsInChildren<UIElement> ());
    }

    IEnumerator AnimateEnter ()
    {
        gameObject.SetActive (true);
        foreach (UIElement element in uiElements) {
            yield return element.OnModuleEnter ();
        }
    }

    protected virtual void InitDataSubscriptions () { }
    protected virtual void InitEventListeners () { }
    protected virtual void AssignReferences () { }
    protected abstract IEnumerator RunLogic ();
    protected virtual void DestroyEventListeners () { }
    protected virtual void DestroyDataSubscriptions () { }

    IEnumerator AnimateExit ()
    {
        foreach (UIElement element in uiElements) {
            yield return StartCoroutine (element.OnModuleExit ());
        }
        gameObject.SetActive (false);
    }

    public IEnumerator Run ()
    {
        gameObject.SetActive (true);
        InitDataSubscriptions ();
        InitEventListeners ();
        AssignReferences ();
        yield return RunLogic ();
        DestroyEventListeners ();
        DestroyDataSubscriptions ();
    }
}
