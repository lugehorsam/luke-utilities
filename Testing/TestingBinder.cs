using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
[RequireComponent (typeof (Canvas))]
[RequireComponent (typeof (CanvasRenderer))]
[RequireComponent (typeof (GraphicRaycaster))]

public class TestingBinder : DataBinder<DiagnosticsData, TestingButton> {

    [SerializeField]
    RectTransform buttonHolder;

    protected override ObservableList<DiagnosticsData> GetOverrideData ()
    {
        var data = new ObservableList<DiagnosticsData>();
        data.Add (new DiagnosticsData (TogglePanel, "Open Console", KeyCode.C));
        data.Add (new DiagnosticsData (() => PlayerPrefs.DeleteAll (), "Clear Cache"));
        LogType[] logTypes = Enum.GetValues (typeof(LogType)) as LogType[];
        foreach (LogType logType in logTypes) {
            LogType type = logType;
            data.Add (new DiagnosticsData (() => Diagnostics.CurrentLogType = type, "Toggle Log To " + logType));
        }
        return data;
    }

    void Update ()
    {
        foreach (DiagnosticsData datum in Data)
        {
            if (datum.Key.HasValue && Input.GetKeyDown (datum.Key.Value)) 
            {
                datum.Action ();
            }
        }
    }

    protected override void HandleNewBehavior (TestingButton behavior)
    {
        behavior.transform.SetParent (buttonHolder.transform);
    }

    protected override void HandleRemovedBehavior (TestingButton behaviors)
    {
    }

    void TogglePanel ()
    {
        buttonHolder.gameObject.SetActive (!buttonHolder.gameObject.activeSelf);
    }
}
