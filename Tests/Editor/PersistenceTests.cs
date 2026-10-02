using System;
using NUnit.Framework;
using UnityEngine;

namespace GV.Extensions.Tests
{
    public class PersistenceTests
    {
        private enum Difficulty { Easy, Hard = 5 }

        [Serializable]
        private class Settings
        {
            public int volume;
            public string name;
        }

        private const string Key = "GV.Extensions.Tests.Key";

        [SetUp]
        [TearDown]
        public void LimparChaves()
        {
            PlayerPrefs.DeleteKey(Key);
            PlayerPrefs.DeleteKey(ApplicationLanguageIdentifier.LanguageKey);
        }

        [Test]
        public void Bool_GuardaERecupera()
        {
            Assert.IsTrue(TypedPlayerPrefs.GetBool(Key, true));

            TypedPlayerPrefs.SetBool(Key, false);

            Assert.IsFalse(TypedPlayerPrefs.GetBool(Key, true));
        }

        [Test]
        public void Enum_GuardaERecupera_ValorInvalidoVoltaAoPadrao()
        {
            TypedPlayerPrefs.SetEnum(Key, Difficulty.Hard);
            Assert.AreEqual(Difficulty.Hard, TypedPlayerPrefs.GetEnum(Key, Difficulty.Easy));

            PlayerPrefs.SetInt(Key, 99);
            Assert.AreEqual(Difficulty.Easy, TypedPlayerPrefs.GetEnum(Key, Difficulty.Easy));
        }

        [Test]
        public void Object_GuardaERecupera_SemChaveDevolveOPadrao()
        {
            Assert.IsNull(TypedPlayerPrefs.GetObject<Settings>(Key));

            TypedPlayerPrefs.SetObject(Key, new Settings { volume = 7, name = "teste" });
            Settings loaded = TypedPlayerPrefs.GetObject<Settings>(Key);

            Assert.AreEqual(7, loaded.volume);
            Assert.AreEqual("teste", loaded.name);
        }

        [Test]
        public void Object_JsonInvalido_DevolveOPadrao()
        {
            PlayerPrefs.SetString(Key, "{ isso nao e json");

            Assert.IsNull(TypedPlayerPrefs.GetObject<Settings>(Key));
        }

        [Test]
        public void Idioma_SalvoTemPrioridadeSobreOIdiomaDoAparelho()
        {
            ApplicationLanguageIdentifier.SaveLanguage(Languages.Portuguese);
            Assert.AreEqual(Languages.Portuguese, ApplicationLanguageIdentifier.GetCurrentLanguage());

            ApplicationLanguageIdentifier.SaveLanguage(Languages.English);
            Assert.AreEqual(Languages.English, ApplicationLanguageIdentifier.GetCurrentLanguage());
        }

        [Test]
        public void Idioma_SemSalvo_UsaOIdiomaDoAparelho()
        {
            Assert.AreEqual(ApplicationLanguageIdentifier.GetSystemLanguage(), ApplicationLanguageIdentifier.GetCurrentLanguage());
        }

        [Test]
        public void Data_FormatoInvalido_DevolveOTextoOriginal()
        {
            Assert.AreEqual("31/12/2025 e outra coisa", DateRegionConverter.GetLocalizedDate("31/12/2025 e outra coisa"));
        }

        [Test]
        public void Data_FormatoAmericano_ViraUmDosDoisPadroes()
        {
            string result = DateRegionConverter.GetLocalizedDate("12/25/2025");

            Assert.That(result, Is.EqualTo("12/25/2025").Or.EqualTo("25/12/2025"));
        }
    }
}
