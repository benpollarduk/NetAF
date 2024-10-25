﻿using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color transition frames.
    /// </summary>
    public sealed class ColorTransitionFrameBuilder : ITransitionFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder;

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
        /// Get or set the message color.
        /// </summary>
        public AnsiColor MessageColor { get; set; } = AnsiColor.Yellow;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorTransitionFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public ColorTransitionFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
        }

        #endregion

        #region Implementation of ITransitionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, string message, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, leftMargin, lastY, availableWidth, TitleColor, out _, out lastY);

                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

                lastY += 3;
            }

            gridStringBuilder.DrawWrapped(message.EnsureFinishedSentence(), leftMargin, lastY, availableWidth, MessageColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
