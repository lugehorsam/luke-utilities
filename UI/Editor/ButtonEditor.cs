namespace Utilities
{
	using UnityEditor;
	
	[CustomEditor(typeof(Button))]
	public class ButtonEditor : UnityEditor.UI.ButtonEditor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty serializedTintState = serializedObject.FindProperty("m_CurrentSelectionState");
			TintState tintState = EnumExt.GetAtIndex<TintState>(serializedTintState.enumValueIndex);
			var popup = EditorGUILayout.EnumPopup("Current Display", tintState);
			
			base.OnInspectorGUI();
			
			serializedObject.Update();
		}

		private enum TintState
		{
			Normal,
			Highlighted,
			Pressed,
			Disabled
		}
	}	
}
