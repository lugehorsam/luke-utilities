using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ButtonSelectionScreen<TData, TBehavior> : InputScreen<TData, TBehavior>
    where TData : struct
    where TBehavior : DatumBehavior<TData>, IButton {

    List<Button> buttons = new List<Button>();

    bool madeSelection;

    void TriggerMadeSelection(TBehavior behavior)
    {
        madeSelection = true;
    }
}
