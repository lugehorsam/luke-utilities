using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ButtonSelectionScreen<TData, TBehavior> : InputScreen<TData, TBehavior>
    where TData : struct
    where TBehavior : ButtomDatumBehavior<TData>, IButton {


}
