namespace Utilities.UI
{
	using UnityEditor;
	
	[CustomEditor(typeof(Button))]
	public class ButtonEditor : UnityEditor.UI.ButtonEditor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty colorBlock = serializedObject.FindProperty("_colorBlockObject");
			EditorGUILayout.ObjectField(colorBlock);
			
			base.OnInspectorGUI();
			serializedObject.Update();
		}
	}	
}
