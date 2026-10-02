using UnityEngine;

namespace GV.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer) => (mask.value & (1 << layer)) != 0;

        public static bool Contains(this LayerMask mask, GameObject gameObject) => mask.Contains(gameObject.layer);
    }
}
