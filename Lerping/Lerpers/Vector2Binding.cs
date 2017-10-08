namespace Utilities.Bindings
{
	using System;
	using UnityEngine;

	public abstract class Vector2Binding<TComponent> : LerpBinding<Vector2, TComponent>
		where TComponent : Component {

		protected sealed override Func<Vector2, Vector2, float, Vector2> GetLerpDelegate() 
		{
			return Vector2.Lerp;
		}

		public override Vector2 AddProperty(Vector2 v1, Vector2 v2) {

			return v1 + v2;
		}

		public sealed override Func<Vector2, Vector2, Vector2> GetRandomizeDelegate ()
		{
			return (v1, v2) => Vector2.Lerp(v1, v2, UnityEngine.Random.value);
		}
	}	
}
