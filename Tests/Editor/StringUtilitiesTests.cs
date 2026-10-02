using NUnit.Framework;

namespace GV.Extensions.Tests
{
    public class StringUtilitiesTests
    {
        [TestCase("playerMaxHP", "Player Max HP")]
        [TestCase("nome_do_campo", "Nome Do Campo")]
        [TestCase("_myField", "My Field")]
        [TestCase("Score", "Score")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void FormatFieldName_FormataONome(string input, string expected)
        {
            Assert.AreEqual(expected, StringUtilities.FormatFieldName(input));
        }
    }
}
