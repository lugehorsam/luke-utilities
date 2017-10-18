namespace Utilities.Bindings
{
    using UnityEngine;
    
    public class SpriteColorProperty : ColorProperty<SpriteRenderer> 
    {
        protected override void Set(SpriteRenderer component, Color color) 
        {
            component.color = color;
        }

        protected override Color Get(SpriteRenderer component)
        {
            return component.color;
        }      
    }
}
