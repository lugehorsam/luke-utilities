namespace TouchDispatch
{
    using UnityEngine;

    public interface ITouchEventInfo
    {
        ITouchState TouchState { get; }
        RaycastHit[] Hits { get; }
        Vector3 WorldPosition { get; }
    }
}
