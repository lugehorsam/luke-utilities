using UnityEngine;
using System.Collections;

public static class MathUtils {
    public static float RandomPlusMinus(float number) {
        return Random.Range(-number, number);
    }

    public static Quaternion GetRotationBetweenPoints(Vector3 p1, Vector3 p2) {
        Vector3 diff = p1 - p2;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        return q;
    }
}
    