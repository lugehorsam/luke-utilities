using System;
using UnityEngine.UI;
using UnityEngine;

public class ButtomDatumBehavior<TDatum> : DatumBehavior<TDatum>, IButton
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
