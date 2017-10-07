namespace Utilities
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	[ExecuteInEditMode]
	public class MockableInputModule : StandaloneInputModule 
	{
		private void OnGUI()
		{
			m_InputOverride = m_InputOverride ?? 
			                  GetComponent<MockableInput>() ?? 
			                  gameObject.AddComponent<MockableInput>();
			Process();
		}
	}
}
