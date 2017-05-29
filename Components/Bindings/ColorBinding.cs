using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class ColorBinding : LerpBinding<Color, Renderer> {
    
        public ColorBinding(MonoBehaviour coroutineRunner, GameObject gameObject) : base(coroutineRunner, gameObject)
        {
        }   
        public override void SetProperty(Color color) {
            Component.material.color = color;
        }

        public override Color GetProperty() {
            return Component.material.color;
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
