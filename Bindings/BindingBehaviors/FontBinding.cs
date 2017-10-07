using UnityEngine;

namespace Utilities
{
    public class FontBinding : PropertyBinding<Font, TextMesh> 
    {
        public override Font GetCurrentProperty ()
        {
            return _Component.font;
        }

        public override void SetProperty (Font property)
        {
            _Component.font = property;
        }
    }   
}
