using System;
using System.Collections.Generic;

namespace GV.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Sorteia um elemento. Lança InvalidOperationException se a lista estiver vazia.
        /// </summary>
        public static T RandomElement<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("A lista está vazia.");

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Embaralha a própria lista (Fisher-Yates).
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
