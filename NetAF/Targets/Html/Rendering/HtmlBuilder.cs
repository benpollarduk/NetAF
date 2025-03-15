using System.Text;
using System.Text.RegularExpressions;

namespace NetAF.Targets.Html.Rendering
{
    /// <summary>
    /// Provides a class for building HTML.
    /// </summary>
    public class HtmlBuilder()
    {
        #region Fields

        private readonly StringBuilder builder = new();

        #endregion

        #region Methods

        /// <summary>
        /// Clear the contents of this builder.
        /// </summary>
        public void Clear()
        {
            builder.Clear();
        }

        /// <summary>
        /// Append a header.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void H1(string content)
        {
            Append("h1", content);
        }

        /// <summary>
        /// Append a header.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void H2(string content)
        {
            Append("h2", content);
        }

        /// <summary>
        /// Append a header.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void H3(string content)
        {
            Append("h3", content);
        }

        /// <summary>
        /// Append a header.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void H4(string content)
        {
            Append("h4", content);
        }

        /// <summary>
        /// Append a paragraph.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void P(string content)
        {
            Append("p", content);
        }

        /// <summary>
        /// Append raw content.
        /// </summary>
        /// <param name="content">The content to append.</param>
        public void Raw(string content)
        {
            builder.Append(content);
        }

        /// <summary>
        /// Append a break.
        /// </summary>
        public void Br()
        {
            builder.Append("<br>");
        }

        /// <summary>
        /// Append a paragraph.
        /// </summary>
        /// <param name="tag">The tag to append.</param>
        /// <param name="content">The content to append.</param>
        private void Append(string tag, string content)
        {
            builder.Append($"<{tag}>{content}</{tag}>");
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var frame = builder.ToString();
            return Regex.Replace(frame, @"\r\n|\n", "<br>");
        }

        #endregion
    }
}
