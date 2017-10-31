namespace Command
{
    using UnityEngine;

    using UnityEditor;

    using UnityEditorInternal;

    [CustomEditor(typeof(CommandBehaviour))] public class CommandBehaviourEditor : Editor
    {
        private ReorderableList _list;

        private void OnEnable()
        {
            var commandsProperty = serializedObject.FindProperty("_commands");
            _list = new ReorderableList(serializedObject, commandsProperty, true, true, true, true);

            _list.drawHeaderCallback = delegate(Rect rect)
            {
                EditorGUI.LabelField(rect, "Commands");
            };

            _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                                        element.FindPropertyRelative("_commandMode"), GUIContent.none);

                EditorGUI.ObjectField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                                        element.FindPropertyRelative("_commandObject"), GUIContent.none);
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();
            _list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
