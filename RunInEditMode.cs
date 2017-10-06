using UnityEngine;

namespace Utilities
{
	[ExecuteInEditMode]
	public class RunInEditMode : MonoBehaviour 
	{
		private void Update () 
		{
			foreach (var comp in GetComponents<MonoBehaviour>())
			{
				comp.runInEditMode = true;
			}
		}
	}	
}
