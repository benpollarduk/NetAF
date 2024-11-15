using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleReactionFrameBuilder(GridStringBuilder gridStringBuilder) : IReactionFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

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

        #endregion

        #region Implementation of IReactionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="size">The size of the frame.</param>
        public IFrame Build(string title, string message, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, leftMargin, lastY, availableWidth, TitleColor, out _, out lastY);

                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

                lastY += 3;
            }

            gridStringBuilder.DrawWrapped(message.EnsureFinishedSentence(), leftMargin, lastY, availableWidth, MessageColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
