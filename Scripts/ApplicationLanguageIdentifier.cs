using System;
using UnityEngine;

namespace GV.Extensions
{
    public enum Languages
    {
        English,
        Portuguese
    }

    public static class ApplicationLanguageIdentifier
    {
        public const string LanguageKey = "Language";

        /// <summary>
        /// Idioma do aparelho, ou inglês quando ele não está na lista de <see cref="Languages"/>.
        /// </summary>
        public static Languages GetSystemLanguage()
        {
            return Enum.TryParse(Application.systemLanguage.ToString(), out Languages language)
                ? language
                : Languages.English;
        }

        /// <summary>
        /// Idioma salvo pelo jogador, ou o do aparelho quando ainda não há um salvo.
        /// </summary>
        public static Languages GetCurrentLanguage()
        {
            if (!PlayerPrefs.HasKey(LanguageKey))
                return GetSystemLanguage();

            int saved = PlayerPrefs.GetInt(LanguageKey);
            return Enum.IsDefined(typeof(Languages), saved) ? (Languages)saved : GetSystemLanguage();
        }

        public static void SaveLanguage(Languages language)
        {
            PlayerPrefs.SetInt(LanguageKey, (int)language);
            PlayerPrefs.Save();
        }
    }
}
