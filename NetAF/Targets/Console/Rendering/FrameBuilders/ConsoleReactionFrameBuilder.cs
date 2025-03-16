using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleReactionFrameBuilder(GridStringBuilder gridStringBuilder) : IReactionFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the message color.
        /// </summary>
        public AnsiColor MessageColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the error message color.
        /// </summary>
        public AnsiColor ErrorMessageColor { get; set; } = AnsiColor.White;

        #endregion

        #region Implementation of IReactionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="isError">If the message is an error.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string message, bool isError, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, leftMargin, lastY, availableWidth, TitleColor, out _, out lastY);

                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

                lastY += 3;
            }

            gridStringBuilder.DrawWrapped(message.EnsureFinishedSentence(), leftMargin, lastY, availableWidth, isError ? ErrorMessageColor : MessageColor, out _, out _);

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
