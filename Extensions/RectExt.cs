using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Overlap;

namespace Utilities
{
    public static class RectExt 
    {

        public static Rect Subtract(Rect rect1, Rect rect2)
        {
            return new Rect(rect1.x - rect2.x, rect1.y - rect2.y, rect1.width - rect2.width, rect1.height - rect2.height);
        }

        public static Rect AddToOrigin(Rect sourceRect, Vector2 offset)
        {
            return new Rect(sourceRect.x + offset.x, sourceRect.y + offset.y,sourceRect.width, sourceRect.height);
        }

        public static IEnumerable<Tuple<Quadrant, Rect>> DivideIntoQuadrants(this Rect thisRect)
        {
            float width = thisRect.width;
            float height = thisRect.height;
            float halfWidth = width / 2;
            float halfHeight = height / 2;
            
            return new List<Tuple<Quadrant, Rect>>
            {
                Tuple.Create
                (
                    Quadrant.UpperLeft, new Rect(0, 0, halfWidth, halfHeight)
                ),
                Tuple.Create
                (
                    Quadrant.UpperRight, new Rect(halfWidth, 0, halfWidth, halfHeight)
                ),
                Tuple.Create
                (
                    Quadrant.LowerRight, new Rect(0, halfHeight, halfWidth, halfHeight)
                ),
                Tuple.Create
                (
                    Quadrant.LowerLeft, new Rect(halfWidth, halfHeight, halfWidth, halfHeight)
                )
            };
        }
        
        public static bool Overlaps<T, K>(this T rect1, K rect2, out RectOverlapInfo<T, K> overlapInfo) where T : IRectOverlapper where K : IRectOverlapper
        {
            bool isOverlap = rect1.Rect.Overlaps(rect2.Rect);

            if (isOverlap)
            {
                IEnumerable<Tuple<Quadrant, Rect>> quadrantInfo = rect1.Rect.DivideIntoQuadrants();

                var overlappingQuadrants = quadrantInfo
                        .Where(info => info.Item2.Overlaps(rect2.Rect))
                        .Select(info => info.Item1);
                        
                overlapInfo = new RectOverlapInfo<T, K>(rect1, rect2, overlappingQuadrants);

                return true;

            }

            overlapInfo = null;
            return false;

        }
    }   
}
