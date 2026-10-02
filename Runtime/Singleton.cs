using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// Base para MonoBehaviours com uma única instância na cena. Herde com o próprio tipo: <c>class GameManager : Singleton&lt;GameManager&gt;</c>.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        protected virtual bool PersistAcrossScenes => true;

        /// <summary>
        /// Instância da cena, ou null (com aviso) quando não existe nenhuma.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_2023_1_OR_NEWER
                    _instance = FindAnyObjectByType<T>();
#else
                    _instance = FindObjectOfType<T>();
#endif
                    if (_instance == null)
                        Debug.LogWarning($"Nenhum {typeof(T).Name} foi encontrado na cena.");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = (T)this;

            if (PersistAcrossScenes && transform.parent == null)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
