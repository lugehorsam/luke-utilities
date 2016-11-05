using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour {

    public Transform Anchoree {
        get {
            return anchoree;
        }
        set {
            TranslateToSelf (value);
            anchoree = value;
        }
    }

    void TranslateToSelf(Transform anchoree) {
        anchoree.transform.position = transform.position;
    }

    Transform anchoree;
}
