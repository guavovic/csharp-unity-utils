using UnityEngine;

namespace GV.Extensions
{
    public static class TransformExtensions
    {
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Destrói todos os filhos: Destroy durante o jogo e DestroyImmediate no editor.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = transform.GetChild(i).gameObject;

                if (Application.isPlaying)
                    Object.Destroy(child);
                else
                    Object.DestroyImmediate(child);
            }
        }

        /// <summary>
        /// Gira no eixo Z para o sprite apontar (eixo X) na direção do alvo, em jogos 2D.
        /// </summary>
        public static void LookAt2D(this Transform transform, Vector3 target)
        {
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
