using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo {

		public TouchDispatcher TouchDispatcher { get; }
		public TouchLogic Logic { get; }
		public RaycastHit[] Hits { get; }
		public Vector3 WorldPosition { get; }

		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic logic, RaycastHit[] hits, Vector3 worldPosition)
		{
			TouchDispatcher = dispatcher;
			Logic = logic;
			Hits = hits;
			WorldPosition = worldPosition;
		}
	}
}

