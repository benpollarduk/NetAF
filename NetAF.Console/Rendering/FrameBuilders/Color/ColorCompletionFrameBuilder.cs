using NetAF.Extensions;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.Frames;

namespace NetAF.Console.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color completion frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ColorCompletionFrameBuilder(GridStringBuilder gridStringBuilder) : ICompletionFrameBuilder
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
        public AnsiColor TitleColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.White;

        #endregion

        #region Implementation of ICompletionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string message, string reason, int width, int height)
        {
            gridStringBuilder.Resize(new(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(message, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, message.Length, TitleColor);

            gridStringBuilder.DrawWrapped(reason.EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, DescriptionColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
