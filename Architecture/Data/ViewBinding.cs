using UnityEngine;

namespace Utilities
{
	public class ViewBinding : MonoBehaviour {

		public View View
		{
			get;
			set;
		}

		void Update()
		{
#if UNITY_EDITOR
			gameObject.name = View.GameObjectName;
#endif
		}
	}	
}
