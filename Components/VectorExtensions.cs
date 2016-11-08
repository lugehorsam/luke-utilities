using UnityEngine;
using System.Collections;
using System;

public static class VectorExtensions {
    public static Vector2 ToVector2(this Vector3 thisVector) {
        return new Vector2(thisVector.x, thisVector.y);
    }

    public static Vector3 ToVector3(this Vector3 thisVector, Transform transform) {
        return new Vector3(thisVector.x, thisVector.y, transform.position.z);
    }

    public static Vector3 ToVector3(this Vector2 thisVector) {
        return new Vector3(thisVector.x, thisVector.y, 0f);
    }

    public static Vector3 GetRandomVectorWithinCamera(float minYBot = 0f, float maxYBot = 0f) {
        float screenX = Randomizer.Randomize(0.0f, Camera.main.pixelWidth);
        float screenY = UnityEngine.Random.Range(minYBot, maxYBot);
        float screenZ = UnityEngine.Random.Range(Camera.main.nearClipPlane, Camera.main.farClipPlane);
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, screenZ)).ToVector2();
        return point; 
    }

    public static Vector3 FromFloat (float value)
    {
        return new Vector3 (value, value, value);
    }

    public static IEnumerable GetEnumerator(this Vector3 thisVector)
    {
        yield return thisVector.x;
        yield return thisVector.y;
        yield return thisVector.z;
    }

    public static Vector3 Map(this Vector3 thisVector, Func<float, float> transformation)
    {
        Vector3 newVector = Vector3.zero;
        newVector.x = transformation(thisVector.x);
        newVector.y = transformation(thisVector.y);
        newVector.z = transformation(thisVector.z);
        return newVector;
    }
}
