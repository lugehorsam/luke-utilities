namespace Utilities
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

        public static Rect GetSlicedWorldRect(this SpriteRenderer thisSpriteRenderer)
        {
            Vector2 pivot = thisSpriteRenderer.sprite.NormalizedPivot();
            Vector2 worldPosition = thisSpriteRenderer.transform.position;
            Vector2 size = thisSpriteRenderer.size;
            var lowerLeft = new Vector2(worldPosition.x - size.x * pivot.x, worldPosition.y - size.y * pivot.y);
            return new Rect(lowerLeft.x, lowerLeft.y, size.x, size.y);
        }
    }
}
