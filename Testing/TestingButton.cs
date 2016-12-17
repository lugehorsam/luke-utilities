using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;
using System;

[RequireComponent(typeof(UIButton))]

public class TestingButton : DatumBehavior<DiagnosticsData>, ILayoutMember {

    UIButton button;

    [SerializeField]
    Text text;

    protected override void InitComponents ()
    {
        button = GetComponent<UIButton> ();
    }

    protected override void HandleDataUpdate (DiagnosticsData oldData, DiagnosticsData newData)
    {
        text.text = newData.DisplayName;
        button.OnClick += newData.Action;
    }

    public void OnLocalLayout (Vector2 idealPosition)
    {
        transform.localPosition = idealPosition;
    }
}
