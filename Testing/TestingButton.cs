using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;
using System;

[RequireComponent(typeof(UIButton))]

public class TestingButton : DatumBehavior<TestingMethodData>, ILayoutMember {

    UIButton button;

    [SerializeField]
    Text text;

    public override void Init ()
    {
        button = GetComponent<UIButton> ();
    }

    protected override void HandleDataUpdate (TestingMethodData oldData, TestingMethodData newData)
    {
        text.text = newData.DisplayName;
        button.OnClick += newData.Action;
    }

    public void OnLocalLayout (Vector2 idealPosition)
    {
        transform.localPosition = idealPosition;
    }
}
