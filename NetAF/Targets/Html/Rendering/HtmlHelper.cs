using NetAF.Targets.Console.Rendering;
using System.Text;

namespace NetAF.Targets.Html.Rendering
{
    /// <summary>
    /// Provides helper functions for Html
    /// </summary>
    internal class HtmlHelper
    {
        #region StaticMethods

        /// <summary>
        /// Convert the contents of a GridStringBuilder to HTML.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <returns>A HTML string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToHtmlString(GridStringBuilder builder, bool padEmptyCharacters = true)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var character = builder.GetCharacter(column, row);

                    if (padEmptyCharacters && character == 0)
                        character = ' ';

                    stringBuilder.Append(character);
                }

                stringBuilder.Append("<br>");
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
