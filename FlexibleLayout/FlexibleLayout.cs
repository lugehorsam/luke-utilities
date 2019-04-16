namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Utilities.Grid;

	public class FlexibleLayout
	{
		private readonly LayoutOverflowChecker overflowChecker;
		private readonly Transform parent;
		private readonly float maxWidth;
		private readonly float ySpacing;

		public FlexibleLayout(Transform parent, float maxWidth, float ySpacing)
		{
			this.parent = parent;
			this.maxWidth = maxWidth;
			this.ySpacing = ySpacing;

			overflowChecker = new LayoutOverflowChecker(parent, maxWidth);
		}

		public void Layout()
		{
			Layout(row: 0);
		}

		private void Layout(int row)
		{
			List<OverflowData> overflowingElements = overflowChecker.GetOverflowingElements();

			for (int i = 0; i < overflowingElements.Count; i++)
			{
				overflowingElements[i].OverflowObject.transform.localPosition = GetNewOverflowElementPosition(overflowingElements, i, row);
			}

			if (overflowingElements.Any())
			{
				Layout(row++);
			}
		}

		private Vector3 GetNewOverflowElementPosition(IList<OverflowData> overflowingElements, int i, int row)
		{
			float newX = GetNewOverflowElementX(overflowingElements, i);
			return new Vector3(newX, row * ySpacing, 0f);
		}

		private float GetNewOverflowElementX(IList<OverflowData> overflowingElements, int i)
		{
			OverflowData elementData = overflowingElements[i];
			return i == 0 ? 0 : overflowingElements[i].OverflowAmount - overflowingElements[i - 1].OverflowAmount;
		}
	}
}
