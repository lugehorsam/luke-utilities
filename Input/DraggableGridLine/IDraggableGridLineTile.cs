using UnityEngine;

namespace Utilities.Input
{
	public interface IDraggableGridLineTile : IGridMember, ILayoutMember, ITouchDispatcher
	{
		bool IsInputWithinConnectBounds(Vector3 mousePosition);
	}
}
