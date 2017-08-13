using UnityEngine;

public class FontBinding : PropertyBinding<Font, TextMesh> {

    public FontBinding(TextMesh textMesh) : base(textMesh)
    {
    }
    
	public override Font GetProperty ()
    {
        return Component.font;
    }

    public override void SetProperty (Font property)
    {
        Component.font = property;
    }
}
