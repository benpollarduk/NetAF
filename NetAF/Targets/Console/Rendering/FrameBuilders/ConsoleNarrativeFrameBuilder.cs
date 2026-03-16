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

            gridStringBuilder.DrawWrapped(builder.ToString().EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, NarrativeColor, out _, out lastY);

            // if a visual
            if (narrative.CurrentVisual != null)
            {
                lastY++;

                var visual = narrative.CurrentVisual;

                GridVisualBuilder finalBuilder = new(BackgroundColor, TitleColor);
                finalBuilder.Resize(size);

                // determine the render size - -2 because of boundary and space at bottom
                var renderSize = new Size(availableWidth, size.Height - lastY - 2);

                // check if resize of the visual is needed
                if (visual.VisualBuilder.DisplaySize.Width != renderSize.Width ||
                    visual.VisualBuilder.DisplaySize.Height != renderSize.Height)
                {
                    // perform resize
                    visual = ResizeMode switch
                    {
                        VisualResizeMode.Crop => visual.Crop(renderSize),
                        VisualResizeMode.Scale => visual.Scale(renderSize),
                        _ => throw new NotImplementedException()
                    };
                }

                var xOffset = Math.Max(leftMargin, size.Width / 2 - visual.VisualBuilder.DisplaySize.Width / 2);
                var yOffset = Math.Max(lastY, size.Height / 2 - visual.VisualBuilder.DisplaySize.Height / 2);

                finalBuilder.Overlay(0, 0, gridStringBuilder);
                finalBuilder.Overlay(xOffset, yOffset, visual.VisualBuilder);
            }

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
