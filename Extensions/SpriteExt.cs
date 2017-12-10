namespace Utilities
{
    using UnityEngine;
    
    public static class SpriteExt
    {
        public static Vector2 NormalizedPivot(this Sprite thisSprite)
        {
            return new Vector2(thisSprite.pivot.x/thisSprite.rect.max.x, thisSprite.pivot.y/thisSprite.rect.max.y);
        }
    }
}
