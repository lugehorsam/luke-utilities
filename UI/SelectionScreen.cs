using UnityEngine;
using System.Collections.Generic;

public class SelectionScreen : InputScreen {

    List<UIButton> buttons = new List<UIButton>();

    bool madeSelection = false;

    protected sealed override bool WaitForInput {
        get {
            return !madeSelection;
        }
    }

    void HandleNewButton(UIButton button)
    {
    }

    public void AddButton(UIButton button)
    {
        button.OnClick += TriggerMadeSelection;
        buttons.Add(button);
    }

    public void RemoveButton(UIButton button)
    {
        button.OnClick -= TriggerMadeSelection;
        buttons.Remove(button);
    }

    void TriggerMadeSelection()
    {
        madeSelection = true;
    }
}
