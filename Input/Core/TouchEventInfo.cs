using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo {

		public TouchDispatcher TouchDispatcher { get; }
		public ITouchState TouchState { get; }
		public RaycastHit[] Hits { get; }
		public Vector3 WorldPosition { get; }

		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic touchState, RaycastHit[] hits, Vector3 worldPosition)
		{
			TouchDispatcher = dispatcher;
			TouchState = touchState;
			Hits = hits;
			WorldPosition = worldPosition;
		}
	}
}

