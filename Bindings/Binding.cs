namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class Binding<T> : MonoBehaviour
	{
		[SerializeField] private ScriptableObject _object;
				
		public void OnGUI()
		{
			if (_object == null)
				return;
			
			//BindingObject bindingObject = _object as BindingObject;
			
			//if (bindingObject == null)
				//throw new NullReferenceException($"Object {_object} on binding {this} could not be casted to a {typeof(BindingObject)}.");
			
		}
	}
}
