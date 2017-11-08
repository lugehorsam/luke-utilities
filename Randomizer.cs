using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Utilities;

/// <summary>
///     Randomization class. Always inclusive.
/// </summary>
public static class Randomizer
{
    public static Vector2 Randomize(Vector2 min, Vector2 max)
    {
        float randX = Randomize(min.x, max.x);
        float randY = Randomize(min.y, max.y);
        return new Vector2(randX, randY);
    }

    public static float Randomize(float min, float max)
    {
        return Random.Range(min, max);
    }

    public static int Randomize(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static Color Randomize(Color color1, Color color2)
    {
        return Color.Lerp(color1, color2, Random.value);
    }   
}
