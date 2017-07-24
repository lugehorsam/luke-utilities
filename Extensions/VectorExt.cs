namespace Utilities
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class VectorExt {
        
        public static Vector2 ToVector2(this Vector3 thisVector) 
        {
            return new Vector2(thisVector.x, thisVector.y);
        }      
    
        public static Vector3 ToVector3(this Vector3 thisVector, Transform transform) 
        {
            return new Vector3(thisVector.x, thisVector.y, transform.position.z);
        }
    
        public static Vector3 ToVector3(this Vector2 thisVector) 
        {
            return new Vector3(thisVector.x, thisVector.y, 0f);
        }
    
        public static Vector3 GetRandomVectorWithinCamera(float minYBot = 0f, float maxYBot = 0f) 
        {
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
            return new []{thisVector.x, thisVector.y, thisVector.z}.Max();      
        }
    
        public static Axis DominantAxis(this Vector3 thisVector, List<Axis> excludedAxes = null)
        {
            var axisValues = new List<KeyValuePair<Axis, float>>();
            
            float xMag = Math.Abs(thisVector.x);
            float yMag = Math.Abs(thisVector.y);
            float zMag = Math.Abs(thisVector.z);
    
            axisValues.Add(new KeyValuePair<Axis, float>(Axis.X, xMag));
            axisValues.Add(new KeyValuePair<Axis, float>(Axis.Y, yMag));
            axisValues.Add(new KeyValuePair<Axis, float>(Axis.Z, zMag));
    
            var sortedAxisValues = axisValues.OrderByDescending(keyVal => keyVal.Value);
    
            foreach (var axisMagnitude in sortedAxisValues)
            {
                if (excludedAxes != null && excludedAxes.Contains(axisMagnitude.Key))
                {
                    continue;
                }
                
                return axisMagnitude.Key;
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
                    return thisVector.y > 0 ? Direction.Up : Direction.Down;
                case Axis.Z:
                    return thisVector.z > 0 ? Direction.Forward : Direction.Back;
            }
            return Direction.None;
        }
    
        public static Vector3 ExceptAxis(this Vector3 thisVector, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    thisVector.x = 0f;
                    break;
                case Axis.Y:
                    thisVector.y = 0f;
                    break;
                case Axis.Z:
                    thisVector.z = 0f;
                    break;
            }
            return thisVector;
        }
    
    
        public static bool ApproximatelyEquals(this Vector3 thisVector, Vector3 otherVector, float differenceCap)
        {
            return thisVector.x.ApproximatelyEquals(otherVector.x, differenceCap) &&
                   thisVector.y.ApproximatelyEquals(otherVector.y, differenceCap) &&
                   thisVector.z.ApproximatelyEquals(otherVector.z, differenceCap);
        }
    
        public static Vector2[] GetOffsetCombinations(this Vector2 thisVector2, float offsetX, float offsetY)
        {
            return new Vector2[]
            {
                thisVector2,
                new Vector2(thisVector2.x + offsetX, thisVector2.y),
                new Vector2(thisVector2.x, thisVector2.y + offsetY),
                new Vector2(thisVector2.x + offsetX, thisVector2.y + offsetY) 
            };
        }
        
        public static Vector3 SetX(this Vector3 thisVector, float x)
        {
            return new Vector3(x, thisVector.y, thisVector.z);
        }
    
        public static Vector3 SetY(this Vector3 thisVector, float y)
        {
            return new Vector3(thisVector.x, y, thisVector.z);
        }
        
        public static Vector3 SetZ(this Vector3 thisVector, float z)
        {
            return new Vector3(thisVector.x, thisVector.y, z);
        }
    
        public static Vector3 Add(Vector3 vector3, Vector2 vector2)
        {
            return new Vector3(vector2.x + vector2.x, vector2.y + vector3.y, vector3.z);        
        }
    
        public static Vector3 OntoAxis(this Vector2 thisVector2, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return new Vector3(0f, thisVector2.x, thisVector2.y);
                case Axis.Y:
                    return new Vector3(thisVector2.x, 0f, thisVector2.y);
                default:
                    return new Vector3(thisVector2.x, thisVector2.y, 0f);
            }
        }
    
        public static List<Axis> UnusedDimensions(this Vector3 thisVector3)
        {
            var unusedAxes = new List<Axis>();
            unusedAxes.Add(Axis.None);
         
            if (thisVector3.x == 0f)
            {
                unusedAxes.Add(Axis.X);
            }
    
            if (thisVector3.y == 0f)
            {
                unusedAxes.Add(Axis.Y);
            }
    
            if (thisVector3.z == 0f)
            {
                unusedAxes.Add(Axis.Z);
            }
    
            return unusedAxes;
        }
    
        public static IEnumerable<Axis> UsedDimensions(this Vector3 thisVector)
        {
            return EnumExt.GetValues<Axis>().Except(thisVector.UnusedDimensions());
        }
        
        
    }

}


