using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class UIButton : UIElement {

    protected Button button;

    public event Action OnClick = () => { };
       

    void Awake ()
    {
        button = GetComponent<Button> ();
        button.onClick.AddListener (() => {
            HandleOnClick ();
            OnClick ();
        });
    }

    protected virtual void HandleOnClick () {}
}
