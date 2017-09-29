/**namespace Utilities.Routing
{
	public class RouteTransition : Command 
	{
		public RouteTransition(Route oldRoute, Route newRoute)
		{
			if (oldRoute != null)
				_queue.AddSerial(oldRoute.Exit());
			
			if (newRoute != null)
				_queue.AddSerial(newRoute.Enter());		
		}
	}	
}
**/