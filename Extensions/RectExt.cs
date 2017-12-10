namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    using Random = UnityEngine.Random;

    public static class RectExt
    {
        public static Rect Subtract(Rect rect1, Rect rect2)
        {
            return new Rect(rect1.x - rect2.x, rect1.y - rect2.y, rect1.width - rect2.width,
                            rect1.height - rect2.height);
        }

        public static Rect AddToOrigin(Rect sourceRect, Vector2 offset)
        {
            return new Rect(sourceRect.x + offset.x, sourceRect.y + offset.y, sourceRect.width, sourceRect.height);
        }

        public static IEnumerable<Tuple<Quadrant, Rect>> CreateQuadrantMap(this Rect thisRect)
        {
            float xMin = thisRect.xMin;
            float yMin = thisRect.yMin;

            float halfWidth = thisRect.width / 2;
            float halfHeight = thisRect.height / 2;
            
            return new List<Tuple<Quadrant, Rect>>
            {
                Tuple.Create(Quadrant.UpperLeft, new Rect(xMin, yMin, halfWidth, halfHeight)),
                Tuple.Create(Quadrant.UpperRight, new Rect(xMin + halfWidth, yMin, halfWidth, halfHeight)),
                Tuple.Create(Quadrant.LowerRight, new Rect(xMin + halfWidth, yMin + halfHeight, halfWidth, halfHeight)),
                Tuple.Create(Quadrant.LowerLeft, new Rect(xMin, yMin + halfHeight, halfWidth, halfHeight))
            };
        }

        public static IEnumerable<Rect> CreateQuadrants(this Rect thisRect)
        {
            return thisRect.CreateQuadrantMap().Select(tuple => tuple.Item2);
        }

        public static List<Quadrant> OverlappingQuadrants(this Rect rect1, Rect rect2)
        {
            var overlappingQuadrants = new List<Quadrant>();

            IEnumerable<Tuple<Quadrant, Rect>> quadrants = rect1.CreateQuadrantMap();

            overlappingQuadrants.AddRange(quadrants.Where(tuple =>
            {
                Rect quadRect = tuple.Item2;

                return quadRect.Overlaps(rect1) && quadRect.Overlaps(rect1);
            }).Select(info => info.Item1));

            return overlappingQuadrants;
        }

        public static Vector2 GetRandomPoint(this Rect thisRect)
        {
            float randX = Random.Range(0f, thisRect.width);
            float randY = Random.Range(0f, thisRect.height);
            
            return new Vector2(thisRect.xMin + randX, thisRect.yMin + randY);
        }

        public static Rect Shrink(this Rect thisRect, float magnitude)
        {
            return new Rect(thisRect.xMin + magnitude/2, thisRect.yMin + magnitude/2, thisRect.width - magnitude,
                            thisRect.height - magnitude);
        }                
    }
}
