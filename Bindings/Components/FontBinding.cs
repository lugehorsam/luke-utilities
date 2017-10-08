namespace Utilities.Bindings
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class FontBinding : PropertyBinding<Font, TextMesh> 
    {
        public override Font GetProperty ()
        {
            return _Component.font;
        }

        public override void SetProperty (Font property)
        {
            _Component.font = property;
        }
    }   
}
