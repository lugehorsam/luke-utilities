namespace Utilities
{
    using UnityEngine;
    
    public static class SpriteExt
    {
        public static Vector2 NormalizedPivot(this Sprite thisSprite)
        {
            return new Vector2(thisSprite.pivot.x/thisSprite.bounds.max.x, thisSprite.pivot.y/thisSprite.bounds.max.y);
        }
    }
}
