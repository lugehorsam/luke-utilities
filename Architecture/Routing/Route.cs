
namespace Utilities
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	
	[Serializable]
	public class Route : List<Route>, IState
	{
		[SerializeField] private string _fragment;

		private readonly IState _state;
	
		public Route(string fragment, IState state)
		{
			_fragment = fragment;
			_state = state;
		}

		public void OnExit()
		{
			_state.OnExit();
		}

		public void OnEnter()
		{
			_state.OnEnter();
		}
	}
}
