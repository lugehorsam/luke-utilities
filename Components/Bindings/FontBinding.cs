using UnityEngine;
using System.Collections;

public class FontBinding : PropertyBinding<Font, TextMesh> {

	public override Font GetProperty ()
    {
        return Component.font;
    }

    public override void SetProperty (Font property)
    {
        Component.font = property;
    }
}
