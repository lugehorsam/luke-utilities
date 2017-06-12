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
			gameObject.name = View.Name;
		}
	}	
}
