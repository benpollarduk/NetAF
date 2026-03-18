using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System;
using System.Text;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="resizeMode">The mode to use for the visual when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class ConsoleNarrativeFrameBuilder(GridStringBuilder gridStringBuilder, VisualResizeMode resizeMode = VisualResizeMode.Scale) : INarrativeFrameBuilder
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

        /// <summary>
        /// Get or set the mode to use for the visual when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Methods

        private IFrame BuildVisualFrame(Narrative narrative, Size size)
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

            lastY += 3;

            // resize if needed - -2 because of boundary and space at bottom
            var visual = narrative.CurrentVisual.ResizeIfNeeded(new Size(availableWidth, size.Height - lastY - 2), ResizeMode);

            var xOffset = Math.Max(leftMargin, size.Width / 2 - visual.VisualBuilder.DisplaySize.Width / 2);
            var yOffset = lastY;

            lastY += visual.VisualBuilder.DisplaySize.Height + 2;

            StringBuilder builder = new();

            foreach (var line in narrative.AllUntilCurrent())
                builder.AppendLine(line + StringUtilities.Newline);

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), leftMargin, lastY, availableWidth, NarrativeColor, out _, out lastY);

            gridStringBuilder.DrawBoundary(BorderColor);

            GridVisualBuilder finalBuilder = new(BackgroundColor, TitleColor);
            finalBuilder.Resize(size);
            finalBuilder.Overlay(0, 0, gridStringBuilder);
            finalBuilder.Overlay(xOffset, lastY, visual.VisualBuilder);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        private IFrame BuildTextFrame(Narrative narrative, Size size)
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

            lastY += 3;

            StringBuilder builder = new();

            foreach (var line in narrative.AllUntilCurrent())
                builder.AppendLine(line + StringUtilities.Newline);

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), leftMargin, lastY, availableWidth, NarrativeColor, out _, out lastY);

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

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
            if (narrative.CurrentVisual != null)
                return BuildVisualFrame(narrative, size);
            else
                return BuildTextFrame(narrative, size);
        }

        #endregion
    }
}
