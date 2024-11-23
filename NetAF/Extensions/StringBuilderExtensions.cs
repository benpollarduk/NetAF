using System.Text;

namespace NetAF.Extensions
{
    /// <summary>
    /// Provides extension methods for StringBuilder.
    /// </summary>
    public static class StringBuilderExtensions
    {
        #region Extensions

        /// <summary>
        /// Ensure this string is a finished sentence, ending in either ?, ! or .
        /// </summary>
        /// <param name="value">The string to finish.</param>
        public static void EnsureFinishedSentence(this StringBuilder value)
        {
            if (value.Length == 0)
                return;

            var str = value.ToString();

            if (str.EndsWith('.') || str.EndsWith('!') || str.EndsWith('?'))
                return;

            if (str.EndsWith(','))
                value.Remove(value.Length - 1, 1);

            value.Append(".");
        }

        #endregion
    }
}