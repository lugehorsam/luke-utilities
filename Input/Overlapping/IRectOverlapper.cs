
namespace Utilities.Overlap
{
	using System;
	using UnityEngine;

	public interface IRectOverlapper
	{
		event Action<IRectOverlapper> OnPositionChange;
		Rect Rect { get; }
	}
}
