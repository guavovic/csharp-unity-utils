using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GV.Extensions.Tests
{
    public class TestSingleton : Singleton<TestSingleton>
    {
        protected override bool PersistAcrossScenes => false;
    }

    public class SingletonTests
    {
        private static TestSingleton Create()
        {
            return new GameObject("TestSingleton").AddComponent<TestSingleton>();
        }

        [UnityTest]
        public IEnumerator Instance_DevolveOComponenteDaCena()
        {
            TestSingleton created = Create();
            yield return null;

            Assert.AreSame(created, TestSingleton.Instance);

            Object.Destroy(created.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator SegundaInstancia_EDestruida()
        {
            TestSingleton first = Create();
            TestSingleton second = Create();
            yield return null;

            Assert.IsTrue(first != null);
            Assert.IsTrue(second == null);
            Assert.AreSame(first, TestSingleton.Instance);

            Object.Destroy(first.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Instance_AposDestruir_AvisaQueNaoHaNenhuma()
        {
            TestSingleton created = Create();
            yield return null;
            Object.Destroy(created.gameObject);
            yield return null;

            LogAssert.Expect(LogType.Warning, "Nenhum TestSingleton foi encontrado na cena.");
            Assert.IsNull(TestSingleton.Instance);
        }
    }

    public class CoroutineRunnerTests
    {
        [UnityTest]
        public IEnumerator RunAfter_ChamaAcaoDepoisDoTempo()
        {
            bool called = false;

            CoroutineRunner.RunAfter(0.1f, () => called = true);
            Assert.IsFalse(called);

            yield return new WaitForSeconds(0.3f);

            Assert.IsTrue(called);
        }

        [UnityTest]
        public IEnumerator Stop_CancelaAAcao()
        {
            bool called = false;

            Coroutine coroutine = CoroutineRunner.RunAfter(0.1f, () => called = true);
            CoroutineRunner.Stop(coroutine);

            yield return new WaitForSeconds(0.3f);

            Assert.IsFalse(called);
        }

        [UnityTest]
        public IEnumerator Run_ExecutaARotina()
        {
            int steps = 0;

            CoroutineRunner.Run(CountTwice(() => steps++));
            yield return null;
            yield return null;
            yield return null;

            Assert.AreEqual(2, steps);
        }

        private static IEnumerator CountTwice(System.Action step)
        {
            step();
            yield return null;
            step();
        }
    }

    public class CooldownPlayModeTests
    {
        [UnityTest]
        public IEnumerator TryUse_LiberaDepoisDaDuracao()
        {
            var cooldown = new Cooldown(0.2f);

            Assert.IsTrue(cooldown.TryUse());
            Assert.IsFalse(cooldown.IsReady);

            yield return new WaitForSeconds(0.3f);

            Assert.IsTrue(cooldown.IsReady);
            Assert.AreEqual(0f, cooldown.Remaining);
            Assert.IsTrue(cooldown.TryUse());
        }
    }

    public class SafeAreaFitterTests
    {
        [UnityTest]
        public IEnumerator SemNotch_PreencheATelaToda()
        {
            if (Screen.width == 0 || Screen.height == 0)
                Assert.Ignore("O jogo está sem tela neste ambiente.");

            var canvas = new GameObject("Canvas", typeof(Canvas));
            var panel = new GameObject("Panel", typeof(RectTransform), typeof(SafeAreaFitter));
            panel.transform.SetParent(canvas.transform, false);

            yield return null;

            var rect = (RectTransform)panel.transform;
            Rect safeArea = Screen.safeArea;

            Assert.AreEqual(safeArea.xMin / Screen.width, rect.anchorMin.x, 0.0001f);
            Assert.AreEqual(safeArea.yMin / Screen.height, rect.anchorMin.y, 0.0001f);
            Assert.AreEqual(safeArea.xMax / Screen.width, rect.anchorMax.x, 0.0001f);
            Assert.AreEqual(safeArea.yMax / Screen.height, rect.anchorMax.y, 0.0001f);
            Assert.AreEqual(Vector2.zero, rect.offsetMin);
            Assert.AreEqual(Vector2.zero, rect.offsetMax);

            Object.Destroy(canvas);
        }
    }
}
