using UnityEngine;

public static class RectExt {

    public static Rect Subtract(Rect rect1, Rect rect2)
    {
        return new Rect(rect1.x - rect2.x, rect1.y - rect2.y, rect1.width - rect2.width, rect1.height - rect2.height);
    }

    public static Rect AddToOrigin(Rect sourceRect, Vector2 offset)
    {
        return new Rect(sourceRect.x + offset.x, sourceRect.y + offset.y,sourceRect.width, sourceRect.height);
    }
}
