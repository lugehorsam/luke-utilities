namespace SpriteRendererExt
{
    using UnityEngine;
    
    public static class SpriteRendererExt 
    {
        public static float GetWorldHeight(this SpriteRenderer thisSpriteRenderer)
        {
            return thisSpriteRenderer.sprite.bounds.max.y - thisSpriteRenderer.sprite.bounds.min.y;
        }
        
        public static float GetWorldWidth(this SpriteRenderer thisSpriteRenderer)
        {
            return thisSpriteRenderer.sprite.bounds.max.x - thisSpriteRenderer.sprite.bounds.min.x;
        }
    }
}
