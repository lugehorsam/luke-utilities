namespace Utilities.Gesture
{
    using UnityEngine;

    public struct GestureFrame
    {
        /// <summary>
        ///     Position of the gesture frame in screen space.
        /// </summary>
        public Vector2 Position { get; }

        public RaycastHit? HitInfo { get; }

        public float Time { get; }

        public GestureFrame(Vector2 position, RaycastHit? hitInfo = null)
        {
            Position = position;
            HitInfo = hitInfo;
            Time = UnityEngine.Time.timeSinceLevelLoad;
        }

        public RaycastHit? HitForCollider(Collider otherCollider)
        {
            return HitInfo.HasValue && (HitInfo.Value.collider == otherCollider) ? HitInfo : null;
        }

        public RaycastHit? HitForCollider(Collider2D otherCollider)
        {
            return HitInfo.HasValue && (HitInfo.Value.collider == otherCollider) ? HitInfo : null;
        }

        public override string ToString()
        {
            return $"[{typeof(GestureFrame)}: Position={Position}, Time={Time}, HitInfo={HitInfo}]";
        }
    }
}
