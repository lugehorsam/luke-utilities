using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo<T> : ITouchEventInfo
	{
		public T Owner { get; }
		public ITouchState TouchState { get; }
		public RaycastHit[] Hits { get; }
		public Vector3 WorldPosition { get; }

		public TouchEventInfo(TouchDispatcher<T> dispatcher, TouchLogic touchState, RaycastHit[] hits, Vector3 worldPosition)
		{
			Owner = dispatcher.Owner;
			TouchState = touchState;
			Hits = hits;
			WorldPosition = worldPosition;
		}
	}
}
