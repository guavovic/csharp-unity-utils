using UnityEngine;

namespace GV.Extensions
{
    /// <summary>
    /// Esconde o campo no inspector, a menos que o campo bool indicado tenha o valor esperado.
    /// </summary>
    public class ShowIfAttribute : PropertyAttribute
    {
        public string Condition { get; }
        public bool Expected { get; }

        public ShowIfAttribute(string condition, bool expected = true)
        {
            Condition = condition;
            Expected = expected;
        }
    }
}
