using System;
using System.Drawing;
using System.Text;

namespace NetAF.Targets.Markup.Rendering
{
    /// <summary>
    /// Provides a class for building markup.
    /// </summary>
    public class MarkupBuilder()
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
        /// Append a heading.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="headingLevel">The level of the heading.</param>
        public void Heading(string text, HeadingLevel headingLevel)
        {
            var heading = headingLevel switch
            {
                HeadingLevel.H1 => $"{MarkupSyntax.Heading}",
                HeadingLevel.H2 => $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}",
                HeadingLevel.H3 => $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}",
                HeadingLevel.H4 => $"{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}{MarkupSyntax.Heading}",
                _ => throw new NotImplementedException()
            };

            Raw($"{heading} {text}");
        }

        /// <summary>
        /// Append text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        public void Text(string text)
        {
            Text(text, TextStyle.Default);
        }

        /// <summary>
        /// Append styled text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="style">The style to use for the text.</param>
        public void Text(string text, TextStyle style)
        {
            var content = text;

            if (style.Bold)
                content = Format(MarkupSyntax.Bold, content);

            if (style.Italic)
                content = Format(MarkupSyntax.Italic, content);

            if (style.Strikethrough)
                content = Format(MarkupSyntax.Strikethrough, content);

            if (style.Foreground != null)
                content = FormatWithValue(MarkupSyntax.Foregound, ColorTranslator.ToHtml(style.Foreground.Value), content);

            if (style.Background != null)
                content = FormatWithValue(MarkupSyntax.Background, ColorTranslator.ToHtml(style.Background.Value), content);

            Raw(content);
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
        /// Append a newline.
        /// </summary>
        public void Newline()
        {
            Raw(MarkupSyntax.NewLine.ToString());
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Format a section of text.
        /// </summary>
        /// <param name="tag">The tag to append.</param>
        /// <param name="text">The text to format.</param>
        private static string Format(string tag, string text)
        {
            return $"{MarkupSyntax.OpenTag}{tag}{MarkupSyntax.CloseTag}{text}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{tag}{MarkupSyntax.CloseTag}";
        }

        /// <summary>
        /// Format a section of text.
        /// </summary>
        /// <param name="tag">The tag to append.</param>
        /// <param name="value">The value of the tag.</param>
        /// <param name="text">The text to format.</param>
        private static string FormatWithValue(string tag, string value, string text)
        {
            return $"{MarkupSyntax.OpenTag}{tag}{MarkupSyntax.Delimiter}{value}{MarkupSyntax.CloseTag}{text}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{tag}{MarkupSyntax.CloseTag}";
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return builder.ToString();
        }

        #endregion
    }
}
