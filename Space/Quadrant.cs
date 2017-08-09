namespace Utilities
{
	public enum Quadrant
	{
		None = 0,
		UpperLeft = 1,
		UpperRight = 2,
		LowerRight = 4,
		LowerLeft = 8,
		Upper = UpperLeft | UpperRight,
		Lower = LowerLeft | LowerRight
		
	}
}
