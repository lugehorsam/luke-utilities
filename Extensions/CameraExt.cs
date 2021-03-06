﻿namespace Utilities
{
    using UnityEngine;

    public static class CameraExt
    {
        public static Vector3 GetWorldPositionForViewport(this Camera thisCamera, Transform referenceTransform, Vector3 desiredViewportPos)
        {
            Vector3 otherViewportPoint = thisCamera.WorldToViewportPoint(referenceTransform.position);
            Vector3 viewPortDifference = otherViewportPoint - desiredViewportPos;
            Vector3 newCameraPosition = thisCamera.ViewportToWorldPoint(viewPortDifference + new Vector3(.5f, .5f, thisCamera.farClipPlane));
            return newCameraPosition;
        }

        public static bool TransformIsInView(this Camera thisCamera, Transform transform)
        {
            Vector2 viewportPoint = thisCamera.WorldToViewportPoint(transform.position);
            return thisCamera.rect.Contains(viewportPoint);
        }

        public static Rect GetWorldRect(this Camera thisCamera)
        {
            Vector3 upperLeft = thisCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 lowerRight = thisCamera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

            return new Rect(upperLeft, lowerRight - upperLeft);
        }
    }
}
