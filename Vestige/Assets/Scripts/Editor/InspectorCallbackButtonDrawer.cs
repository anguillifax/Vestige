using UnityEditor;
using UnityEngine;

namespace Vestige
{
	[CustomPropertyDrawer(typeof(InspectorCallbackButton))]
	public class InspectorCallbackButtonDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(pos, label, prop);

			var r = EditorGUI.PrefixLabel(pos, label);
			var prevIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			string tooltip = prop.FindPropertyRelative("tooltip").stringValue;

			if (EditorApplication.isPlaying)
			{
				if (GUI.Button(r, tooltip))
				{
					// This hack only works if the member variable is a plain field inside the parent class.
					var obj = fieldInfo.GetValue(prop.serializedObject.targetObject);
					if (obj is InspectorCallbackButton data)
					{
						data.Invoke();
					}
					else
					{
						Debug.LogWarning("Internal Error: Unable to use reflection to access original object.");
					}
				}
			}
			else
			{
				GUI.enabled = false;
				GUI.Button(r, tooltip);
				GUI.enabled = true;
			}


			EditorGUI.indentLevel = prevIndent;
			EditorGUI.EndProperty();
		}
	}
}
