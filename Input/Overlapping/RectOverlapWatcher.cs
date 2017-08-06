using System.Linq;

namespace Utilities.Input
{
	using System;
	using System.Collections.Generic;
	
	public class RectOverlapWatcher
	{
		public event Action<IRectOverlapper, IRectOverlappee> OnOverlap = (overlapper, overlappee) => {};
		public event Action<IRectOverlapper, IRectOverlappee> OnOverlapLeave = (overlapper, overlappee) => {};

		public List<IRectOverlappee> Overlapees { get; } = new List<IRectOverlappee>();
		private readonly List<IRectOverlapper> _observedOverlappers = new List<IRectOverlapper>();
		
		private readonly List<OverlapData> _currentOverlaps = new List<OverlapData>();
		
		public void Watch(IRectOverlapper rectOverlapper)
		{
			rectOverlapper.OnPositionChange += HandleOverlapperPositionChange;
			_observedOverlappers.Add(rectOverlapper);
		}

		public void Unwatch(IRectOverlapper rectOverlapper)
		{
			if (rectOverlapper == null)
			{
				Diag.Report("Trying to unwatch a null rect overlapper");
				return;
			}
			
			if (!_observedOverlappers.Contains(rectOverlapper))
			{
				Diag.Warn("Trying to unwatch an already unwatched overlapper.");
				return;
			}

			rectOverlapper.OnPositionChange -= HandleOverlapperPositionChange;
			_observedOverlappers.Remove(rectOverlapper);
		}		

		void HandleOverlapperPositionChange(IRectOverlapper overlapper)
		{
			foreach (IRectOverlappee overlapee in Overlapees)
			{
				bool isOverlap = overlapee.Rect.Overlaps(overlapper.Rect);
				bool hasBegunOverlapping = isOverlap && !IsTrackedOverlap(overlapper, overlapee);
				bool hasStoppedOverlapping = !isOverlap && IsTrackedOverlap(overlapper, overlapee);
					
				if (hasBegunOverlapping)
				{
					Diag.Log(UtilitiesFeature.Overlap, "Has begun overlapping " + overlapper + " ,  " + overlapee);
					OnOverlap(overlapper, overlapee);
					_currentOverlaps.Add(new OverlapData(overlapper, overlapee));
				}
				else if (hasStoppedOverlapping)
				{
					Diag.Log(UtilitiesFeature.Overlap, "Has stopped overlapping " + overlapper + " ,  " + overlapee);
					OnOverlapLeave(overlapper, overlapee);
					_currentOverlaps.Remove(GetTrackedOverlap(overlapper, overlapee));
				}
			}
		}	

		bool IsTrackedOverlap(IRectOverlapper overlapper, IRectOverlappee overlapee)
		{
			return GetTrackedOverlap(overlapper, overlapee) != null;
		}

		OverlapData GetTrackedOverlap(IRectOverlapper overlapper, IRectOverlappee overlapee)
		{
			return _currentOverlaps.FirstOrDefault
			(
				overlap => overlap.RectOverlapper == overlapper && overlap.RectOverlappee == overlapee
			);
		}

		private class OverlapData
		{
			public IRectOverlapper RectOverlapper { get; }
			public IRectOverlappee RectOverlappee { get; }

			public OverlapData(IRectOverlapper overlapper, IRectOverlappee overlapee)
			{
				RectOverlapper = overlapper;
				RectOverlappee = overlapee;
			}
		}
	}	
}

