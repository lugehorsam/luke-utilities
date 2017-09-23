using System;

namespace Utilities
{
	using UnityEngine;
	using Observable;
	using Assets;
	
	[ExecuteInEditMode]
	public abstract class Behavior<T> : MonoBehaviour where T : Bundle
	{
		private static readonly Observables<Behavior<T>> _behaviors = new Observables<Behavior<T>>();
		public static Observables<Behavior<T>> Behaviors => _behaviors;
		
		private T _bundle;

		public bool HasBundle => _bundle != null;

		private void Awake()
		{
			TryAddToBehaviors();
		}
		
		protected abstract void SetVisuals(T bundle);

		public void OnRenderObject()
		{
			TryAddToBehaviors();
			
			if (_bundle != null)
				SetVisuals(_bundle);
		}

		public void SetBundle(T bundle)
		{
			Diag.Log("set bundle called");
			_bundle = bundle;
		}

		private void TryAddToBehaviors()
		{
			if (!_behaviors.Contains(this))
				_behaviors.Add(this);
		}
	}
}