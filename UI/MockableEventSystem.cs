namespace Utilities
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	[ExecuteInEditMode]
	public class MockableEventSystem : EventSystem 
	{
		private void OnGUI()
		{
			Diag.Log("is focused " + isFocused);
		}
	}	
}
