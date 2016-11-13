using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionPopup : UIPopup {

    [SerializeField]
    UIButton [] buttons;

    bool madeSelection = false;

    void Start()
    {
        foreach (UIButton button in buttons)
        {
            button.OnClick += () => madeSelection = true;
        }
    }

    protected sealed override bool WaitForInput {
        get {
            return !madeSelection;
        }
    }
}
