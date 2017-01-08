using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ButtonSelectionScreen<TData, TBehavior> : InputScreen<TData, TBehavior>
    where TData : struct
    where TBehavior : DatumBehavior<TData> {

    List<Button> buttons = new List<Button>();

    bool madeSelection;

    protected sealed override bool WaitForInput {
        get {
            return !madeSelection;
        }
    }

    public void AddButton(Button button)
    {
        button.onClick.AddListener(TriggerMadeSelection);
        buttons.Add(button);
    }

    public void RemoveButton(Button button)
    {
        button.onClick.RemoveListener(TriggerMadeSelection);
        buttons.Remove(button);
    }

    void TriggerMadeSelection()
    {
        madeSelection = true;
    }
}
