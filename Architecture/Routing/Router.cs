namespace Utilities.Routing
{
	using System.Collections;
	using System.Collections.Generic;
	
	public class Router 
	{
		private List<Route> _routes = new List<Route>();
		private Route _currentRoute;
		
		public void AddRoute(Route route)
		{
			_routes.Add(route);
		}

		public IEnumerator RouteTo(Route route)
		{
			Diag.Crumb(this, $"Begin routing to {route}");
			
			if (_currentRoute != null)
				yield return _currentRoute.Exit();

			_currentRoute = route;

			if (route != null)
			{
				yield return route.Enter();
			}
			
			Diag.Crumb(this, $"Finish routing to {route}");
		}
	}
}
