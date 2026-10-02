using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GV.Extensions
{
    /// <summary>
    /// Campo de cena para o inspector: arraste o asset da cena e use o nome ou o caminho em runtime.
    /// </summary>
    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private Object _sceneAsset;
        [SerializeField] private string _sceneName = "";
        [SerializeField] private string _scenePath = "";

        public string SceneName => _sceneName;
        public string ScenePath => _scenePath;

        public static implicit operator string(SceneField sceneField)
        {
            return sceneField?.SceneName;
        }
    }

#if UNITY_EDITOR
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
#endif
}
