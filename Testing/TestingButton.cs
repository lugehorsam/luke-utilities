using UnityEngine;
using UnityEngine.UI;

public class TestingButton : DatumBehavior<DiagnosticsData>, ILayoutMember {

    Button button;

    [SerializeField]
    Text text;

    protected void Awake ()
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
