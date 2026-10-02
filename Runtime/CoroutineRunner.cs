using System;
using System.Collections;
using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// Roda coroutines a partir de classes que não são MonoBehaviour. Funciona só com o jogo rodando.
    /// </summary>
    public static class CoroutineRunner
    {
        private sealed class Host : MonoBehaviour { }

        private static Host _host;

        private static Host GetHost()
        {
            if (_host == null)
            {
                var gameObject = new GameObject("CoroutineRunner") { hideFlags = HideFlags.HideInHierarchy };
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                _host = gameObject.AddComponent<Host>();
            }

            return _host;
        }

        public static Coroutine Run(IEnumerator routine)
        {
            return GetHost().StartCoroutine(routine);
        }

        public static Coroutine RunAfter(float seconds, Action action)
        {
            return Run(WaitThen(seconds, action));
        }

        public static void Stop(Coroutine coroutine)
        {
            if (_host != null && coroutine != null)
                _host.StopCoroutine(coroutine);
        }

        private static IEnumerator WaitThen(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }
    }
}
