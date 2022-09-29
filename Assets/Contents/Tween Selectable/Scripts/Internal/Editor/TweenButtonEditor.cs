using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace UI.Tween
{
	[CustomEditor(typeof(UI.Tween.TweenButton), true)]
	[CanEditMultipleObjects]
	public class TweenButtonEditor : ButtonEditor
	{
		SerializedProperty m_TweenProperty;

		protected override void OnEnable()
		{
			base.OnEnable();
			m_TweenProperty = serializedObject.FindProperty("tweenInfo");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();

			serializedObject.Update();
			EditorGUILayout.PropertyField(m_TweenProperty);
			serializedObject.ApplyModifiedProperties();
		}
	}
}