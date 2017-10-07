using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo : ITouchEventInfo
	{
		public GameObject Owner { get; }
		public ITouchState TouchState { get; }
		public RaycastHit[] Hits { get; }
		public Vector3 WorldPosition { get; }

		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic touchState, RaycastHit[] hits, Vector3 worldPosition)
		{
			Owner = dispatcher.gameObject;
			TouchState = touchState;
			Hits = hits;
			WorldPosition = worldPosition;
		}
	}
}
