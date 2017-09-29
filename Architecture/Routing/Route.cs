/**
namespace Utilities.Routing
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	[Serializable]
	public class Route : List<Route>, IState
	{
		[SerializeField] private string _fragment;

		private readonly IState _state;
	
		public string Fragment => _fragment;

		public Route(string fragment, IState state)
		{
			_fragment = fragment;
			_state = state;
		}

		public IEnumerator Exit()
		{
			return _state.Exit();
		}

		public IEnumerator Enter()
		{
			return _state.Enter();
		}

		public override string ToString()
		{
			return $"[{GetType()}] {_state}";
		}
	}
}

**/