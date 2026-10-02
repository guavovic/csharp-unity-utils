using System;
using System.Globalization;
using UnityEngine;

namespace GV.Extensions
{
    public static class DateRegionConverter
    {
        private const string AmericanFormat = "MM/dd/yyyy";
        private const string BrazilianFormat = "dd/MM/yyyy";

        /// <summary>
        /// Formata a data no padrão do idioma do aparelho: dd/MM/yyyy em português e MM/dd/yyyy nos demais.
        /// </summary>
        public static string GetLocalizedDate(DateTime date)
        {
            string format = Application.systemLanguage == SystemLanguage.Portuguese ? BrazilianFormat : AmericanFormat;
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Recebe a data como MM/dd/yyyy e devolve no padrão do aparelho. Se o texto não estiver nesse formato, devolve o texto original.
        /// </summary>
        public static string GetLocalizedDate(string americanDate)
        {
            return DateTime.TryParseExact(americanDate, AmericanFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                ? GetLocalizedDate(date)
                : americanDate;
        }
    }
}
