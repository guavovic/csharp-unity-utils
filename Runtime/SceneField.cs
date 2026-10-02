using UnityEngine;

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
}
