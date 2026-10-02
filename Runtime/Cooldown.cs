using System;
using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// Tempo de recarga para habilidades, tiros e ações com intervalo. A duração aparece no inspector.
    /// </summary>
    [Serializable]
    public struct Cooldown
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private bool _useUnscaledTime;

        private float _readyAt;

        public Cooldown(float duration, bool useUnscaledTime = false)
        {
            _duration = duration;
            _useUnscaledTime = useUnscaledTime;
            _readyAt = 0f;
        }

        public float Duration => _duration;

        private float Now => _useUnscaledTime ? Time.unscaledTime : Time.time;

        public bool IsReady => Now >= _readyAt;

        public float Remaining => Mathf.Max(0f, _readyAt - Now);

        /// <summary>
        /// Se estiver pronto, inicia a recarga e devolve true. Se não, devolve false.
        /// </summary>
        public bool TryUse()
        {
            if (!IsReady)
                return false;

            _readyAt = Now + _duration;
            return true;
        }

        public void Reset()
        {
            _readyAt = 0f;
        }
    }
}
