using System.Linq;

namespace Utilities.Overlap
{
	using System;
	using System.Collections.Generic;
	
	public class RectOverlapWatcher<T, K> where T : class, IRectOverlapper where K : class, IRectOverlapper
	{
		public event Action<RectOverlapInfo<T, K>> OnOverlap = overlapInfo => {};
		public event Action<T, K> OnOverlapLeave = (overlapper, overlappee) => {};

		public List<K> Overlapees { get; } = new List<K>();
		private readonly List<T> _observedOverlappers = new List<T>();
		
		private readonly List<OverlapData<T, K>> _currentOverlaps = new List<OverlapData<T, K>>();
		
		public void Watch(T rectOverlapper)
		{
			rectOverlapper.OnPositionChange += HandleOverlapperPositionChange;
			_observedOverlappers.Add(rectOverlapper);
		}

		public void Unwatch(T rectOverlapper)
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
			T typedOverlapper = overlapper as T;
			
			foreach (K overlapee in Overlapees)
			{
				RectOverlapInfo<T, K> overlapInfo;
				
				bool isOverlap = typedOverlapper.Overlaps(overlapee, out overlapInfo);				
				bool hasBegunOverlapping = isOverlap && !IsTrackedOverlap(overlapper as T, overlapee);
				bool hasStoppedOverlapping = !isOverlap && IsTrackedOverlap(overlapper as T, overlapee);
					
				if (hasBegunOverlapping)
				{
					Diag.Log(UtilitiesFeature.Overlap, "Has begun overlapping " + overlapper + " ,  " + overlapee);
					
					OnOverlap(overlapInfo);
					_currentOverlaps.Add(new OverlapData<T, K>(overlapper as T, overlapee));
				}
				else if (hasStoppedOverlapping)
				{
					Diag.Log(UtilitiesFeature.Overlap, "Has stopped overlapping " + overlapper + " ,  " + overlapee);
					OnOverlapLeave(overlapper as T, overlapee);
					_currentOverlaps.Remove(GetTrackedOverlap(overlapper as T, overlapee));
				}
			}
		}	

		bool IsTrackedOverlap(T overlapper, K overlapee)
		{
			return GetTrackedOverlap(overlapper, overlapee) != null;
		}

		OverlapData<T, K> GetTrackedOverlap(T overlapper, K overlapee)
		{
			return _currentOverlaps.FirstOrDefault
			(
				overlap => overlap.RectOverlapper == overlapper && overlap.RectOverlappee == overlapee
			);
		}

		private class OverlapData<T, K> where T : class, IRectOverlapper where K : class, IRectOverlapper
		{
			public T RectOverlapper { get; }
			public K RectOverlappee { get; }

			public OverlapData(T overlapper, K overlapee)
			{
				RectOverlapper = overlapper;
				RectOverlappee = overlapee;
			}
		}
	}	
}

