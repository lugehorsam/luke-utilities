using UnityEngine;

namespace Utilities
{
	public class ControllerBinding : MonoBehaviour {

		public Controller Controller
		{
			get;
			set;
		}

		void Update()
		{
#if UNITY_EDITOR
			gameObject.name = Controller.GameObjectName;
#endif
		}
	}	
}
