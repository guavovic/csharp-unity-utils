using UnityEditor;
using UnityEngine;

namespace GV.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredDrawer : PropertyDrawer
    {
        private const float MessageHeight = 22f;

        private static bool IsMissing(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUI.GetPropertyHeight(property, label, true);
            return IsMissing(property) ? height + MessageHeight + EditorGUIUtility.standardVerticalSpacing : height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsMissing(property))
            {
                Rect message = new Rect(position.x, position.y, position.width, MessageHeight);
                EditorGUI.HelpBox(message, $"{label.text} é obrigatório.", MessageType.Error);

                float offset = MessageHeight + EditorGUIUtility.standardVerticalSpacing;
                position.y += offset;
                position.height -= offset;
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
