using UnityEngine;
using Utilities.Bindings;

namespace Utilities.UI
{
    [CreateAssetMenu]
    public class ColorObject : PropertyObject<Color>
    {
        public override BindType BindType => BindType.SpriteColor;
    }
}
