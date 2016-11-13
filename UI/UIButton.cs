using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class UIButton : UIElement {

    protected Button button;

    public event Action OnClick = () => { };
       
    protected override void AddEventHandlers()
    {
        button.onClick.AddListener(ClickListener);
    }

    void ClickListener()
    {
        HandleOnClick();
        OnClick();
    }

    protected override void RemoveEventHandlers()
    {
        button.onClick.RemoveListener(ClickListener);
    }

    protected override void InitComponents ()
    {
        button = GetComponent<Button> ();
    }
     
    protected virtual void HandleOnClick () {}
}
