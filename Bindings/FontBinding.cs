using UnityEngine;

public class FontBinding : PropertyBinding<Font, TextMesh> {

    public FontBinding(GameObject gameObject, TextMesh textMesh) : base(gameObject, textMesh)
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
