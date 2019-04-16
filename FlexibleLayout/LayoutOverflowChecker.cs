namespace Utilities
{
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public class LayoutOverflowChecker
	{
		private float maxWidth;
		private Transform parent;

		public LayoutOverflowChecker(Transform parent, float width)
		{
			this.maxWidth = width;
			this.parent = parent;
		}

		public bool IsOverflowX()
		{
			return GetOverflowingElements().Any();
		}

		public List<OverflowData> GetOverflowingElements()
		{
			var overflowingElements = new List<OverflowData>();

			for (int i = 0; i < parent.childCount; i++)
				AddOverflowingElement(overflowingElements, i);

			return overflowingElements;
		}

		private void AddOverflowingElement(List<OverflowData> overflowingElements, int i)
		{
			Bounds bounds = GetChildBounds(i);
			Transform child = parent.GetChild(i);

			float childX = bounds.size.x + child.localPosition.x;
			float overflow = maxWidth - childX;

			if (childX > maxWidth)
				overflowingElements.Add(new OverflowData(child.gameObject, overflow));
		}

		private Renderer GetChildRenderer(int i)
		{
			Transform child = parent.GetChild(i);
			return child.GetComponent<Renderer>();
		}

		private Bounds GetChildBounds(int i)
		{
			return GetChildRenderer(i).bounds;
		}
	}
}
