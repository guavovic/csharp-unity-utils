using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace GV.Extensions.Tests
{
    public class ExtensionsTests
    {
        [Test]
        public void VectorExtensions_TrocamSoOEixoPedido()
        {
            var vector = new Vector3(1f, 2f, 3f);

            Assert.AreEqual(new Vector3(9f, 2f, 3f), vector.WithX(9f));
            Assert.AreEqual(new Vector3(1f, 9f, 3f), vector.WithY(9f));
            Assert.AreEqual(new Vector3(1f, 2f, 9f), vector.WithZ(9f));
            Assert.AreEqual(new Vector3(1f, 0f, 3f), vector.Flat());
            Assert.AreEqual(new Vector2(9f, 2f), new Vector2(1f, 2f).WithX(9f));
            Assert.AreEqual(new Vector2(1f, 9f), new Vector2(1f, 2f).WithY(9f));
        }

        [Test]
        public void ColorExtensions_WithAlpha_MantemARgb()
        {
            Color result = Color.red.WithAlpha(0.25f);

            Assert.AreEqual(1f, result.r);
            Assert.AreEqual(0.25f, result.a);
        }

        [Test]
        public void LayerMaskExtensions_Contains_ConfereACamada()
        {
            LayerMask mask = (1 << 3) | (1 << 5);

            Assert.IsTrue(mask.Contains(3));
            Assert.IsTrue(mask.Contains(5));
            Assert.IsFalse(mask.Contains(4));
        }

        [Test]
        public void RandomElement_ListaVazia_LancaExcecao()
        {
            Assert.Throws<InvalidOperationException>(() => new List<int>().RandomElement());
        }

        [Test]
        public void RandomElement_DevolveUmItemDaLista()
        {
            var list = new List<int> { 1, 2, 3 };

            Assert.Contains(list.RandomElement(), list);
        }

        [Test]
        public void Shuffle_MantemOsMesmosElementos()
        {
            var list = Enumerable.Range(0, 20).ToList();

            list.Shuffle();

            CollectionAssert.AreEquivalent(Enumerable.Range(0, 20), list);
        }

        [Test]
        public void Transform_ResetLocal_VoltaAoPadrao()
        {
            var gameObject = new GameObject();
            gameObject.transform.localPosition = Vector3.one;
            gameObject.transform.localScale = Vector3.one * 3f;

            gameObject.transform.ResetLocal();

            Assert.AreEqual(Vector3.zero, gameObject.transform.localPosition);
            Assert.AreEqual(Vector3.one, gameObject.transform.localScale);
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void Transform_DestroyChildren_RemoveTodosOsFilhos()
        {
            var parent = new GameObject();
            for (int i = 0; i < 3; i++)
                new GameObject().transform.SetParent(parent.transform);

            parent.transform.DestroyChildren();

            Assert.AreEqual(0, parent.transform.childCount);
            UnityEngine.Object.DestroyImmediate(parent);
        }

        [Test]
        public void Transform_LookAt2D_ApontaOEixoXParaOAlvo()
        {
            var gameObject = new GameObject();

            gameObject.transform.LookAt2D(new Vector3(0f, 5f, 0f));

            Assert.AreEqual(90f, gameObject.transform.eulerAngles.z, 0.01f);
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
    }
}
