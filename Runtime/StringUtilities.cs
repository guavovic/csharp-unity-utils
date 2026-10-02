using System.Globalization;
using System.Text.RegularExpressions;

namespace GV.Extensions
{
    public static class StringUtilities
    {
        private static readonly Regex WordBoundary = new Regex(
            "(?<=[a-z0-9])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])",
            RegexOptions.Compiled);

        /// <summary>
        /// Transforma o nome de um campo em texto legível: "playerMaxHP" vira "Player Max HP" e "nome_do_campo" vira "Nome Do Campo".
        /// </summary>
        public static string FormatFieldName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                return string.Empty;

            string spaced = WordBoundary.Replace(fieldName.Trim('_').Replace('_', ' '), " ");
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(spaced);
        }
    }
}
