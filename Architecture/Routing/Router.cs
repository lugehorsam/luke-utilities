/**
namespace Utilities.Routing
{
	using System.Linq;
	using System.Collections.Generic;
	
	public class Router
	{		
		private List<Route> _routes = new List<Route>();
		private Route _currentRoute;
		
		public void AddRoute(Route route)
		{
			_routes.Add(route);
		}

		public void AddRoute(string route, IState state)
		{
			_routes.Add(new Route(route, state));
		}

		public RouteTransition RouteTo(Route route)
		{
			Route oldRoute = _currentRoute;
			_currentRoute = route;
			return new RouteTransition(oldRoute, route);	
		}

		public RouteTransition RouteTo(string uri)
		{
			var chosenRoute = _routes.First(route)
		}
	}
}
**/