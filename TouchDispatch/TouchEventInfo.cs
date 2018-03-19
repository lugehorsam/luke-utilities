namespace TouchDispatch
{
    using UnityEngine;

    using Utilities;

    public class TouchEventInfo : ITouchEventInfo
    {
        public TouchEventInfo(TouchDispatch dispatch, TouchLogic touchState, RaycastHit[] hits, Vector3 worldPosition)
        {
            Owner = dispatch.gameObject;
            Hits = hits;
            WorldPosition = worldPosition;
            Dimension = Dimension.Three;
        }

        public TouchEventInfo(TouchDispatch dispatch, TouchLogic touchState, RaycastHit2D[] hits, Vector3 worldPosition)
        {
            Owner = dispatch.gameObject;
            Hits2D = hits;
            WorldPosition = worldPosition;
            Dimension = Dimension.Two;
        }

        public GameObject Owner { get; }
        public RaycastHit2D[] Hits2D { get; }
        public Dimension Dimension { get; }
        public ITouchState TouchState { get; }
        public RaycastHit[] Hits { get; }
        public Vector3 WorldPosition { get; }
    }
}
