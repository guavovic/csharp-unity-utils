using TMPro;
using UnityEngine;

namespace GV.Extensions
{
    public class FrameRateDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField, Min(0.1f)] private float _pollingTime = 0.5f;

        private float _time;
        private int _frameCount;

        private void Update()
        {
            _time += Time.unscaledDeltaTime;
            _frameCount++;

            if (_time < _pollingTime)
                return;

            _text.text = $"{Mathf.RoundToInt(_frameCount / _time)} fps";
            _time = 0f;
            _frameCount = 0;
        }
    }
}
