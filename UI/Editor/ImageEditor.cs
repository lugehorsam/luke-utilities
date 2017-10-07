namespace Utilities.UI
{
	using UnityEditor;
	
	[CustomEditor(typeof(Image))]
	public class ImageEditor : UnityEditor.UI.ImageEditor
	{
		public override void OnInspectorGUI()
		{      
			base.OnInspectorGUI();

			EditorGUI.BeginChangeCheck();

			SerializedProperty color = serializedObject.FindProperty("_colorObject");
			EditorGUILayout.ObjectField(color);

			if (!EditorGUI.EndChangeCheck())
			{
				return;
			}

			serializedObject.ApplyModifiedProperties();
			
			serializedObject.Update();
		}
	}	
}
