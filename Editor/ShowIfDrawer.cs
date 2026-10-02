using UnityEditor;
using UnityEngine;

namespace GV.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        private bool IsVisible(SerializedProperty property)
        {
            var showIf = (ShowIfAttribute)attribute;
            string path = property.propertyPath;
            int separator = path.LastIndexOf('.');
            string conditionPath = separator < 0 ? showIf.Condition : path.Substring(0, separator + 1) + showIf.Condition;

            SerializedProperty condition = property.serializedObject.FindProperty(conditionPath);

            if (condition == null || condition.propertyType != SerializedPropertyType.Boolean)
                return true;

            return condition.boolValue == showIf.Expected;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsVisible(property) ? EditorGUI.GetPropertyHeight(property, label, true) : -EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsVisible(property))
                EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
