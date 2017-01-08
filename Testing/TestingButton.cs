using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;
using System;


public class TestingButton : DatumBehavior<DiagnosticsData>, ILayoutMember {

    Button button;

    [SerializeField]
    Text text;

    protected override void InitComponents ()
    {
        button = GetComponent<Button> ();
    }

    protected override void HandleDataUpdate (DiagnosticsData oldData, DiagnosticsData newData)
    {
        text.text = newData.DisplayName;
        button.onClick.AddListener(() => newData.Action());
    }

    public void OnLocalLayout (Vector2 idealPosition)
    {
        transform.localPosition = idealPosition;
    }
}
