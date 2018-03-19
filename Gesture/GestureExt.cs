namespace Utilities.Gesture
{
    using UnityEngine;

    public static class GestureExt
    {
        /// <summary>
        ///     For each dimension, zero = no progress from origin to destination and 1 = full or greater progress
        /// </summary>
        public static Vector2 GetNormalizedProgress(this Gesture gesture, Vector2 origin, Vector2 destination)
        {
            Vector2 currPos = gesture.MousePositionCurrent;
            Vector2 progressOffset = currPos - origin;
            Vector2 totalOffset = destination - origin;
            var clampedProgress = new Vector2(Mathf.Clamp(progressOffset.x / totalOffset.y, 0, 1), Mathf.Clamp(progressOffset.y / totalOffset.y, 0, 1));
            return clampedProgress;
        }
    }
}
