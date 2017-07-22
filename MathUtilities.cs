using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{    
    public static class MathExt
    {
        public static float RandomPlusMinus(float number)
        {
            return Random.Range(-number, number);
        }
    
        public static Quaternion GetRotationBetweenPoints(Vector3 p1, Vector3 p2)
        {
            Vector3 diff = p1 - p2;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            return q;
        }
    
    
        //Two non-parallel lines which may or may not touch each other have a point on each line which are closest
        //to each other. This function finds those two points. If the lines are not parallel, the function 
        //outputs true, otherwise false.
        public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2,
            Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
        {
    
            closestPointLine1 = Vector3.zero;
            closestPointLine2 = Vector3.zero;
    
            float a = Vector3.Dot(lineVec1, lineVec1);
            float b = Vector3.Dot(lineVec1, lineVec2);
            float e = Vector3.Dot(lineVec2, lineVec2);
    
            float d = a * e - b * b;
    
            //lines are not parallel
            if (d != 0.0f)
            {
    
                Vector3 r = linePoint1 - linePoint2;
                float c = Vector3.Dot(lineVec1, r);
                float f = Vector3.Dot(lineVec2, r);
    
                float s = (b * f - c * e) / d;
                float t = (a * f - c * b) / d;
    
                closestPointLine1 = linePoint1 + lineVec1 * s;
                closestPointLine2 = linePoint2 + lineVec2 * t;
    
                return true;
            }
    
            return false;
        }
    
        public static float GetSignedAngle(Vector3 vectorA, Vector3 vectorB)
        {
            float angle = Vector2.Angle(vectorA, vectorB);
            Vector3 cross = Vector3.Cross(vectorA, vectorB);
            if (cross.z < 0) angle = -angle;
            return angle;
        }
    
        public static void SortInCycle(Vector3[] vectors, CycleDirection direction)
        {
            Vector3 center = GetAverageVector(vectors);
    
            //clockwise
            Array.Sort(vectors, (v1, v2) =>
            {
                v1 -= center;
                v2 -= center;
                return Mathf.Atan2(v1.x, v1.y).CompareTo(Mathf.Atan2(v2.x, v2.y));
            });
        }
    
        public static Vector3 GetAverageVector(IEnumerable<Vector3> vectors)
        {
            Vector3 currentValue = default(Vector3);
    
            foreach (var vector in vectors)
            {
                currentValue += vector;
            }
    
            return currentValue / vectors.Count();
        }
    
        public static float GetCirclePoint(float radius, float xOrY)
        {
            return Mathf.Sqrt(radius * radius - xOrY * xOrY);
        }

        public static bool IsEven(int i)
        {
            return i % 2 == 0;
        }
    }   

}