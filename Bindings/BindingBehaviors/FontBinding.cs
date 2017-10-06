using UnityEngine;

namespace Utilities
{
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
