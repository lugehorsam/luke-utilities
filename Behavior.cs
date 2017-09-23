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
		protected T _Bundle => _bundle;

		public bool HasBundle => _bundle != null;

		private void Awake()
		{
			_behaviors.Add(this);
		}
		
		protected abstract void SetVisuals();

		public void OnRenderObject()
		{
			SetVisuals();
		}

		public void SetBundle(T bundle)
		{
			_bundle = bundle;
		}
	}
}