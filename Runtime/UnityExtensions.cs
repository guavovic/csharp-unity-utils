using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GV.Extensions
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Chama o método com o parâmetro depois do tempo informado. Devolve a coroutine para poder cancelar com StopCoroutine.
        /// </summary>
        public static Coroutine InvokeWithParameter<T>(this MonoBehaviour monoBehaviour, Action<T> method, T parameter, float time)
        {
            return monoBehaviour.StartCoroutine(InvokeAfter(method, parameter, time));
        }

        private static IEnumerator InvokeAfter<T>(Action<T> method, T parameter, float time)
        {
            yield return new WaitForSeconds(time);
            method(parameter);
        }

        /// <summary>
        /// Procura um objeto do tipo na cena e avisa no console quando não encontra.
        /// </summary>
        public static T FindOrWarn<T>(this MonoBehaviour monoBehaviour, bool includeInactive = false) where T : Object
        {
#if UNITY_2023_1_OR_NEWER
            T result = Object.FindAnyObjectByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
#else
            T result = Object.FindObjectOfType<T>(includeInactive);
#endif

            if (result == null)
                Debug.LogWarning($"Objeto do tipo {typeof(T)} não foi encontrado na cena.", monoBehaviour);

            return result;
        }

        /// <summary>
        /// Remove os componentes: Destroy durante o jogo e DestroyImmediate no editor.
        /// </summary>
        public static void RemoveComponents(this MonoBehaviour monoBehaviour, params Object[] components)
        {
            foreach (Object component in components)
            {
                if (component == null)
                    continue;

                if (Application.isPlaying)
                    Object.Destroy(component);
                else
                    Object.DestroyImmediate(component);
            }
        }

        /// <summary>
        /// Escreve no console em negrito e colorido. A cor só aparece no editor.
        /// </summary>
        public static void DebugLogColored(string message, Color color)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}><b>{message}</b></color>");
#else
            Debug.Log(message);
#endif
        }
    }
}
