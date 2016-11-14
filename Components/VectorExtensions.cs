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

    public static float MaxDimension(this Vector3 thisVector)
    {
        if (thisVector.x > thisVector.y && thisVector.x > thisVector.z)
        {
            return thisVector.x;
        }

        if (thisVector.y > thisVector.x && thisVector.y > thisVector.z)
        {
            return thisVector.y;
        }

        if (thisVector.z > thisVector.y && thisVector.z > thisVector.x)
        {
            return thisVector.z;
        }

        return 0f;
    }


    public static Axis DominantAxis(this Vector3 thisVector)
    {
        float xMag = Math.Abs(thisVector.x);
        float yMag = Math.Abs(thisVector.y);
        float zMag = Math.Abs(thisVector.z);

        if (xMag > yMag && xMag > zMag)
        {
            return Axis.X;
        }

        if (yMag > xMag && yMag > zMag)
        {
            return Axis.Y;
        }

        if (zMag > yMag && zMag > xMag)
        {
            return Axis.Z;
        }

        return Axis.None;
    }

    public static Vector3 RestrictToAxis(this Vector3 thisVector, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return new Vector3(thisVector.x, 0f, 0f);
            case Axis.Y: 
                return new Vector3(0f, thisVector.y, 0f);
            case Axis.Z:
                return new Vector3(0f, 0f, thisVector.z);
            default:
                return thisVector;
        }
    }

    public static Direction DominantDirection(this Vector3 thisVector)
    {
        Axis dominantAxis = thisVector.DominantAxis();
        switch (dominantAxis)
        {
            case Axis.X:
                return thisVector.x > 0 ? Direction.Right : Direction.Left;
            case Axis.Y:
                return thisVector.y > 0 ? Direction.Top : Direction.Bottom;
            case Axis.Z:
                return thisVector.z > 0 ? Direction.Forward : Direction.Back;
        }
        return Direction.None;
    }
}
