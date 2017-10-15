namespace Utilities.Bindings
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class FontComponent : PropertyComponent<Font, TextMesh>
    {
        public override BindType BindType => BindType.Font;

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
