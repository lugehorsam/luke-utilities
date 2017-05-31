using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Randomization class. Always inclusive.
/// </summary>
public static class Randomizer {
    public static Vector2 Randomize(Vector2 min, Vector2 max) {
        float randX = Randomize(min.x, max.x);
        float randY = Randomize(min.y, max.y);
        return new Vector2(randX, randY);
    }

    public static float Randomize(float min, float max) {        
        return UnityEngine.Random.Range(min, max);
    }

	public static int Randomize(int min, int max) { 
		return UnityEngine.Random.Range(min, max);
	}

	public static Color Randomize(Color color1, Color color2) {
		return Color.Lerp (color1, color2, UnityEngine.Random.value);
	}

	public static T[] RandomDistinct<T>(
		int numDistinct,
		IEnumerable<T> collection)
	{
		HashSet<T> distinctValues = new HashSet<T>();
		var collectionCopy = new List<T>(collection);

		while (distinctValues.Count < numDistinct && collectionCopy.Count > 0)
		{
			var randomElement = collectionCopy.ElementAt(Randomize(0, collectionCopy.Count() - 1));
			collectionCopy.Remove(randomElement);
			distinctValues.Add(randomElement);
		}

		return distinctValues.ToArray();
	}
}

