using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// Ajusta o RectTransform à área segura da tela (notch, cantos arredondados e barra de gestos). Coloque num painel que preenche o Canvas.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;

        private void OnEnable()
        {
            Apply();
        }

        private void Update()
        {
            if (Screen.safeArea != _lastSafeArea || Screen.width != _lastScreenSize.x || Screen.height != _lastScreenSize.y)
                Apply();
        }

        private void Apply()
        {
            if (Screen.width == 0 || Screen.height == 0)
                return;

            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            Rect safeArea = Screen.safeArea;
            _lastSafeArea = safeArea;
            _lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            _rectTransform.anchorMin = new Vector2(safeArea.xMin / Screen.width, safeArea.yMin / Screen.height);
            _rectTransform.anchorMax = new Vector2(safeArea.xMax / Screen.width, safeArea.yMax / Screen.height);
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }
    }
}
