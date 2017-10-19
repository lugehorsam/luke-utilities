namespace Utilities.Bindings
{
    using System;
    using UnityEngine;
    
    public abstract class ColorProperty<K> : LerpableProperty<Color, K> where K : Component
    {
        protected sealed override Func<Color, Color, float, Color> GetLerpDelegate ()
        {
            return Color.Lerp;
        }

        public sealed override Color Add (Color color1, Color color2)
        {
            return color1 + color2;
        }

        public sealed override Func<Color, Color, Color> GetRandomizeDelegate ()
        {
            return (c1, c2) => Color.Lerp (c1, c2, UnityEngine.Random.value);
        }
    }
}
