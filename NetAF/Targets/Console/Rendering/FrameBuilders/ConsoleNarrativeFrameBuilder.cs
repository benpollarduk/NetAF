using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Text;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleNarrativeFrameBuilder(GridStringBuilder gridStringBuilder) : INarrativeFrameBuilder
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
        /// Get or set the narrative color.
        /// </summary>
        public AnsiColor NarrativeColor { get; set; } = AnsiColor.White;

        #endregion

        #region Implementation of INarrativeFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="narrative">The narrative.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Narrative narrative, Size size)
        {
            gridStringBuilder.Resize(size);

            var lastY = 0;
            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            if (!string.IsNullOrEmpty(narrative.Title))
            {
                gridStringBuilder.DrawWrapped(narrative.Title, leftMargin, 2, availableWidth, TitleColor, out _, out lastY);

                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, narrative.Title.Length, TitleColor);
            }

            StringBuilder builder = new();

            foreach (var line in narrative.AllUntilCurrent())
                builder.AppendLine(line + StringUtilities.Newline);

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, NarrativeColor, out _, out _);

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
