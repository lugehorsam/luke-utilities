namespace Enact
{
	using UnityEngine;

	using UnityEditor;

	using UnityEditorInternal;

	[CustomPropertyDrawer(typeof(Mixins))] public class MixinsDrawer : PropertyDrawer
	{
		private ReorderableList _list;
		
	    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
		    if (_list == null)
		    {
			    var commandsProperty = property.FindPropertyRelative("_mixins");
			    _list = new ReorderableList(commandsProperty.serializedObject, commandsProperty, true, true, true, true);

			    _list.drawHeaderCallback = delegate(Rect rect)
			    {
				    EditorGUI.LabelField(rect, "Mixins");
			    };
		    }
		    
		    _list.DoList(position);
	    }

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (_list == null)
			{
				return 0f;
			}
			
			return _list.GetHeight();
		}
	}
}
