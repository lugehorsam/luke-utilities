using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
[RequireComponent (typeof (Canvas))]
[RequireComponent (typeof (CanvasRenderer))]
[RequireComponent (typeof (GraphicRaycaster))]

public class TestingManager : DataManager<TestingMethodData, TestingButton> {

    [SerializeField]
    RectTransform panelHolder;

    protected override void AddLocalData ()
    {
        data.Add (new TestingMethodData (TogglePanel, "Open Console", KeyCode.C));
        data.Add (new TestingMethodData (() => DataSource.EnableCache = false, "Disable Cache"));
        data.Add (new TestingMethodData (() => PlayerPrefs.DeleteAll (), "Clear Cache"));
        LogType[] logTypes = Enum.GetValues (typeof(LogType)) as LogType[];
        foreach (LogType logType in logTypes) {
            LogType type = logType;
            data.Add (new TestingMethodData (() => Diagnostics.CurrentLogType = type, "Toggle Log To " + logType));
        }
    }

    void Update ()
    {
        foreach (TestingMethodData datum in data) {
            if (datum.Key.HasValue && Input.GetKeyDown (datum.Key.Value)) {
                datum.Action ();
            }
        }
    }

    protected override void HandleNewBehavior (TestingButton behavior)
    {
        behavior.transform.SetParent (panelHolder.transform);
    }

    protected override void HandleRemovedBehavior (TestingButton behaviors)
    {
    }

    void TogglePanel ()
    {
        Diagnostics.Log ("Toggling panel");
        panelHolder.gameObject.SetActive (!panelHolder.gameObject.activeSelf);
    }
}
