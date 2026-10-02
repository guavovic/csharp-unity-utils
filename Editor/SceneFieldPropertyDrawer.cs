using UnityEditor;
using UnityEngine;

namespace GV.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty sceneAsset = property.FindPropertyRelative("_sceneAsset");
            SerializedProperty sceneName = property.FindPropertyRelative("_sceneName");
            SerializedProperty scenePath = property.FindPropertyRelative("_scenePath");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.BeginChangeCheck();
            Object scene = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

            if (EditorGUI.EndChangeCheck())
            {
                sceneAsset.objectReferenceValue = scene;
                sceneName.stringValue = scene != null ? scene.name : "";
                scenePath.stringValue = scene != null ? AssetDatabase.GetAssetPath(scene) : "";
            }

            EditorGUI.EndProperty();
        }
    }
}
