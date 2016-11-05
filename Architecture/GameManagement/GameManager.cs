using UnityEngine;
using System.Collections;

public abstract class GameManager : MonoBehaviour {

    void Start ()
    {
        StartCoroutine(RunGame ());
    }

    protected abstract IEnumerator RunGame ();
}
