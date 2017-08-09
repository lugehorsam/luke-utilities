using UnityEngine;

namespace Utilities.Input
{
	public interface ITouchEventInfo
	{
		ITouchState TouchState { get; }
		RaycastHit[] Hits { get; }
		Vector3 WorldPosition { get; }
	}
}


