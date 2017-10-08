namespace Utilities.Bindings
{
    using System;
    using UnityEngine;
    
    public class SpriteColorBinding : LerpBinding<Color, SpriteRenderer> 
    {
        public override void SetProperty(Color color) 
        {
            _Component.color = color;
        }

        public override Color GetProperty()
        {
            return _Component.color;
        }        

        protected override Func<Color, Color, float, Color> GetLerpDelegate ()
        {
            return Color.Lerp;
        }

        public override Color AddProperty (Color color1, Color color2)
        {
            return color1 + color2;
        }

        public override Func<Color, Color, Color> GetRandomizeDelegate ()
        {
            return (c1, c2) => Color.Lerp (c1, c2, UnityEngine.Random.value);
        }
    }
}
