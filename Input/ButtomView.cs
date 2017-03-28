using System;
using UnityEngine.UI;
using UnityEngine;
using Utilities;

/**
public class ButtomView<TDatum> : View<TDatum>, IButton
    where TDatum : struct {

    public event Action<IInput> OnInput
    {
        add
        {
            Button.onClick.AddListener(() => value(this));
        }

        remove
        {
            Button.onClick.RemoveListener(() => value(this));
        }
    }
    public Button Button
    {
        get { return button; }
    }

    [SerializeField] private Button button;
}
**/