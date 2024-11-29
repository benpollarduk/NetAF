using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering.FrameBuilders;
using System;

namespace NetAF.Rendering.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of visual frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleVisualFrameBuilder(GridStringBuilder gridStringBuilder) : IVisualFrameBuilder
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

        #endregion

        #region Implementation of IVisualFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="gridVisualBuilder">The grid visual builder.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, GridVisualBuilder gridVisualBuilder, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawWrapped(description.EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 3;

            GridVisualBuilder finalBuilder = new(BackgroundColor, TitleColor);
            finalBuilder.Resize(size);

            var xOffset = Math.Max(leftMargin, (size.Width / 2) - (gridVisualBuilder.DisplaySize.Width / 2));
            var yOffset = Math.Max(lastY, (size.Height / 2) - (gridVisualBuilder.DisplaySize.Height / 2));

            finalBuilder.Overlay(0, 0, gridStringBuilder);
            finalBuilder.Overlay(xOffset, yOffset, gridVisualBuilder);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
