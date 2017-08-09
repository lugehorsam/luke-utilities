namespace Utilities.Overlap
{
	using System.Linq;
	using System.Collections.Generic;

	public class RectOverlapInfo<T, K> where T : IRectOverlapper where K : IRectOverlapper
	{
		public T OverlappingRect { get; }
		public K OverlapeeRect { get; }
		public IEnumerable<Quadrant> Quadrants { get; }
		
		public RectOverlapInfo(T overlappingRect, K overlapeeRect, IEnumerable<Quadrant> quadrants)
		{
			OverlappingRect = overlappingRect;
			OverlapeeRect = overlapeeRect;
			Quadrants = quadrants;
		}		
		
		public bool IsEnteringFromBelow()
		{
			return Quadrants.Any(quadrant => quadrant.HasFlag(Quadrant.Lower));
		}

		public bool IsEnteringFromAbove()
		{
			return Quadrants.Any(quadrant => quadrant.HasFlag(Quadrant.Upper));
		}
	}
}
