using System;

namespace Utilities.Input
{
	public interface IRectOverlapper : IRectOverlappee
	{
		event Action<IRectOverlapper> OnPositionChange;
	}	
}
