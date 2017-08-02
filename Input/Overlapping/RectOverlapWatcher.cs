namespace Utilities.Input
{
	using System;
	using System.Collections.Generic;
	
	public class RectOverlapWatcher
	{
		public event Action<IRectOverlapper, IRectOverlappee> OnOverlap = (region, snapable) => {};
		
		public List<IRectOverlappee> Overlapees { get; } = new List<IRectOverlappee>();
		private readonly List<IRectOverlapper> _observedOverlappers = new List<IRectOverlapper>();
		
		public void Watch(IRectOverlapper rectOverlapper)
		{
			rectOverlapper.OnPositionChange += HandleOverlapperPositionChange;
			_observedOverlappers.Add(rectOverlapper);
		}

		public void Unwatch(IRectOverlapper rectOverlapper)
		{
			if (!_observedOverlappers.Contains(rectOverlapper))
			{
				Diagnostics.LogWarning("Trying to ignore an unwatched snapper.");
				return;
			}

			rectOverlapper.OnPositionChange -= HandleOverlapperPositionChange;
			_observedOverlappers.Remove(rectOverlapper);
		}		

		void HandleOverlapperPositionChange(IRectOverlapper changedRectOverlapper)
		{
			foreach (IRectOverlappee region in Overlapees)
			{
				if (region.Rect.Overlaps(changedRectOverlapper.Rect))
				{
					OnOverlap(changedRectOverlapper, region);
				}
			}
		}
	}	
}

