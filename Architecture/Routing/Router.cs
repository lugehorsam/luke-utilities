using System.Collections;
using System.Linq;

namespace Utilities
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable]
	public abstract class Router : ISerializationCallbackReceiver
	{
		[SerializeField] private string _fragment;
		private IEnumerable<Route> _routes;
		
		private StateMachine<Route> _stateMachine = new StateMachine<Route>();
		public StateMachine<Route> StateMachine => _stateMachine;
		
		protected abstract Route _DefaultRoute { get; }

		public void OnBeforeSerialize()
		{
			_fragment = _stateMachine.State?.Fragment;
		}

		public void OnAfterDeserialize()
		{
		}

		public IEnumerator Initialize()
		{
			_routes = _InitRoutes();
			var cachedRoute = _routes.FirstOrDefault(route => route.Fragment == _fragment);
			return _stateMachine.SetState(cachedRoute ?? _DefaultRoute);
		}

		protected abstract IEnumerable<Route> _InitRoutes();		
	}
}
