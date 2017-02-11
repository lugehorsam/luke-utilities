/**
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ButtonSelectionScreen<TDatum, TBehavior> : InputScreen<TDatum, TBehavior>
    where TDatum : struct
    where TBehavior : ButtomDatumBehavior<TDatum>, IButton {


}
**/