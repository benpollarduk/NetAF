using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="resizeMode">The mode to use when the design size and the render size differ and the content needs to be resized.</param>
    public sealed class ConsoleVisualFrameBuilder(GridStringBuilder gridStringBuilder, VisualResizeMode resizeMode = VisualResizeMode.Scale) : IVisualFrameBuilder
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
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the mode to use when the design size and the render size differ and the content needs to be resized.
        /// </summary>
        public VisualResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region Implementation of IVisualFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Visual visual, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;
            
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(visual.Name, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, visual.Name.Length, TitleColor);

            if (!string.IsNullOrEmpty(visual.Description))
                gridStringBuilder.DrawWrapped(visual.Description.EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 3;

            var availableHeight = size.Height - lastY - 1;

            GridVisualBuilder finalBuilder = new(BackgroundColor, TitleColor);
            finalBuilder.Resize(size);

            // determine the render size
            var renderSize = new Size(availableWidth, availableHeight);

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

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
