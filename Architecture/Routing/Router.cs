using System.Collections;

namespace Utilities.Routing
{
	using System.Collections.Generic;
	
	public class Router : Command
	{		
		private List<Route> _routes = new List<Route>();
		private Route _currentRoute;
		
		public void AddRoute(Route route)
		{
			_routes.Add(route);
		}

		public void RouteTo(Route route)
		{
			if (_currentRoute != null)
				_queue.AddSerial(_currentRoute.Exit());
			
			_currentRoute = route;

			if (_currentRoute != null)
				_queue.AddSerial(_currentRoute.Enter());			
		}
	}
}
