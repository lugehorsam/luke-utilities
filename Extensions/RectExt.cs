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
            float xMin = thisRect.xMin;
            float yMin = thisRect.yMin;

            float xMax = thisRect.xMax;
            float yMax = thisRect.yMax;

            float xHalf = (xMin + xMax) / 2;
            float yHalf = (yMin + yMax) / 2;
            
            return new List<Tuple<Quadrant, Rect>>
            {
                Tuple.Create
                (
                    Quadrant.UpperLeft, new Rect(xMin, yMin, xHalf, yHalf)
                ),
                Tuple.Create
                (
                    Quadrant.UpperRight, new Rect(xHalf, yMin, xMax, yHalf)
                ),
                Tuple.Create
                (
                    Quadrant.LowerRight, new Rect(xHalf, yHalf, xMax, yMax)
                ),
                Tuple.Create
                (
                    Quadrant.LowerLeft, new Rect(xMin, xHalf, yHalf, yMax)
                )
            };
        }
        
        public static bool Overlaps<T, K>(this T overlapper, K overlapee, out RectOverlapInfo<T, K> overlapInfo) where T : IRectOverlapper where K : IRectOverlapper
        {
            bool isOverlap = overlapper.Rect.Overlaps(overlapee.Rect);

            if (isOverlap)
            {
                IEnumerable<Tuple<Quadrant, Rect>> quadrantInfo = overlapper.Rect.DivideIntoQuadrants();

                var overlappingQuadrants = quadrantInfo
                        .Where(info => info.Item2.Overlaps(overlapee.Rect))
                        .Select(info => info.Item1);
                        
                overlapInfo = new RectOverlapInfo<T, K>(overlapper, overlapee, overlappingQuadrants);

                return true;

            }

            overlapInfo = null;
            return false;

        }
    }   
}
