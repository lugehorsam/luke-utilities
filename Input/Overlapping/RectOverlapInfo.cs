namespace Utilities.Overlap
{
	using System.Linq;
	using System.Collections.Generic;

	public class RectOverlapInfo<T, K> where T : IRectOverlapper where K : IRectOverlapper
	{
		public T Overlapper { get; }
		public K Overlapee { get; }
		public IEnumerable<Quadrant> Quadrants { get; }
		
		public RectOverlapInfo(T overlapper, K overlapee, IEnumerable<Quadrant> quadrants)
		{
			Overlapper = overlapper;
			Overlapee = overlapee;
			Quadrants = quadrants;
		}		
		
		public bool IsEnteringFromBelow()
		{
			Diag.Log("quads " +  Quadrants.ToFormattedString());

			Diag.Log("returning " +  Quadrants.All(quadrant => Quadrant.Upper.HasFlag(quadrant)));
			return Quadrants.All(quadrant => Quadrant.Upper.HasFlag(quadrant));
		}

		public bool IsEnteringFromAbove()
		{
			return Quadrants.All(quadrant => Quadrant.Lower.HasFlag(quadrant));
		}
	}
}
