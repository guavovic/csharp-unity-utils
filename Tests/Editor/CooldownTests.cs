using NUnit.Framework;

namespace GV.Extensions.Tests
{
    public class CooldownTests
    {
        [Test]
        public void TryUse_NovoCooldown_EstaPronto()
        {
            var cooldown = new Cooldown(10f);

            Assert.IsTrue(cooldown.IsReady);
            Assert.AreEqual(0f, cooldown.Remaining);
        }

        [Test]
        public void TryUse_UsaUmaVezEBloqueiaAteORecarregar()
        {
            var cooldown = new Cooldown(10f, true);

            Assert.IsTrue(cooldown.TryUse());
            Assert.IsFalse(cooldown.TryUse());
            Assert.IsFalse(cooldown.IsReady);
            Assert.Greater(cooldown.Remaining, 0f);
        }

        [Test]
        public void Reset_LiberaDeNovo()
        {
            var cooldown = new Cooldown(10f, true);
            cooldown.TryUse();

            cooldown.Reset();

            Assert.IsTrue(cooldown.IsReady);
        }
    }
}
