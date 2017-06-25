using UnityEngine;

public static class RectExt {

    public static Rect Subtract(Rect rect1, Rect rect2)
    {
        return new Rect(rect1.x - rect2.x, rect1.y - rect2.y, rect1.width - rect2.width, rect1.height - rect2.height);
    }
}
