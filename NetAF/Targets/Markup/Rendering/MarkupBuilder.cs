using System;
using System.Text;
using NetAF.Utilities;

namespace NetAF.Targets.Markup.Rendering
{
    /// <summary>
    /// Provides a class for building markup.
    /// </summary>
    public sealed class MarkupBuilder()
    {
        #region Fields

        private readonly StringBuilder builder = new();

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the default text style.
        /// </summary>
        public TextStyle TextStyle { get; set; } = TextStyle.Default;

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

            Raw($"{heading} {text}{MarkupSyntax.NewLine}");
        }

        /// <summary>
        /// Append text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        public void Write(string text)
        {
            Write(text, TextStyle);
        }

        /// <summary>
        /// Append text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="markers">The markers indicating which parts of the text to highlight.</param>
        public void Write(string text, StringMarker[] markers = null)
        {
            Write(text, TextStyle, markers);
        }

        /// <summary>
        /// Append text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="style">The style to use for the text.</param>
        /// <param name="markers">The markers indicating which parts of the text to highlight.</param>
        public void Write(string text, TextStyle style, StringMarker[] markers = null)
        {
            Write(Highlight(text, markers), style);
        }

        /// <summary>
        /// Append styled text.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="style">The style to use for the text.</param>
        public void Write(string text, TextStyle style)
        {
            var content = text;

            if (style.Bold)
                content = Format(MarkupSyntax.Bold, content);

            if (style.Italic)
                content = Format(MarkupSyntax.Italic, content);

            if (style.Strikethrough)
                content = Format(MarkupSyntax.Strikethrough, content);

            if (style.Underline)
                content = Format(MarkupSyntax.Underline, content);

            if (style.Monospace)
                content = Format(MarkupSyntax.Monospace, content);

            if (style.Highlight)
                content = Format(MarkupSyntax.Highlight, content);

            if (style.Foreground != null)
                content = FormatWithValue(MarkupSyntax.Foregound, style.Foreground.ToHtml(), content);

            if (style.Background != null)
                content = FormatWithValue(MarkupSyntax.Background, style.Background.ToHtml(), content);

            Raw(content);
        }

        /// <summary>
        /// Append text, terminating in a newline character.
        /// </summary>
        /// <param name="text">The text to append.</param>
        public void WriteLine(string text)
        {
            WriteLine(text, TextStyle);
        }

        /// <summary>
        /// Append text, terminating in a newline character, with highlighted words styled, terminating in a newline character.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="markers">The markers indicating which parts of the text to highlight.</param>
        public void WriteLine(string text, StringMarker[] markers = null)
        {
            Write(text, TextStyle, markers);
        }

        /// <summary>
        /// Append text, terminating in a newline character, with highlighted words styled, terminating in a newline character.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <param name="style">The style to use for the text.</param>
        /// <param name="markers">The markers indicating which parts of the text to highlight.</param>
        public void WriteLine(string text, TextStyle style, StringMarker[] markers = null)
        {
            Write(Highlight(text, markers) + MarkupSyntax.NewLine, style);
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

        /// <summary>
        /// Highlight words within a string by emboldening the sections indicated by a set of markers.
        /// </summary>
        /// <param name="text">The text to highlight.</param>
        /// <param name="markers">The markers indicating which parts of the text to embolden.</param>
        /// <returns>The text with the marked sections wrapped in bold tags.</returns>
        internal static string Highlight(string text, StringMarker[] markers)
        {
            if (string.IsNullOrEmpty(text) || markers == null || markers.Length == 0)
                return text;

            StringBuilder result = new();
            var highlights = StringUtilities.GetHighlightMask(text, markers);
            var open = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Highlight}{MarkupSyntax.CloseTag}";
            var close = $"{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Highlight}{MarkupSyntax.CloseTag}";

            for (var i = 0; i < text.Length; i++)
            {
                var isHighlighted = highlights[i];
                var wasHighlighted = i > 0 && highlights[i - 1];

                if (isHighlighted && !wasHighlighted)
                    result.Append(open);
                else if (!isHighlighted && wasHighlighted)
                    result.Append(close);

                result.Append(text[i]);
            }

            if (highlights[^1])
                result.Append(close);

            return result.ToString();
        }

        #endregion

        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            return builder.ToString();
        }

        #endregion
    }
}
