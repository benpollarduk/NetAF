﻿using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color about frames.
    /// </summary>
    public sealed class ColorAboutFrameBuilder : IAboutFrameBuilder
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
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the name color.
        /// </summary>
        public AnsiColor NameColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.Yellow;

        /// <summary>
        /// Get or set the author color.
        /// </summary>
        public AnsiColor AuthorColor { get; set; } = AnsiColor.BrightWhite;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorAboutFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public ColorAboutFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
        }

        #endregion

        #region Implementation of IAboutFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, Game game, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            gridStringBuilder.DrawWrapped(game.Name, leftMargin, lastY + 3, availableWidth, NameColor, out _, out lastY);

            gridStringBuilder.DrawWrapped(game.Description.EnsureFinishedSentence(), leftMargin, lastY + 2, availableWidth, DescriptionColor, out _, out lastY);

            if (!string.IsNullOrEmpty(game.Author))
                gridStringBuilder.DrawWrapped($"Created by: {game.Author}.", leftMargin, lastY + 2, availableWidth, AuthorColor, out _, out _);
            else
                gridStringBuilder.DrawWrapped("NetAF by Ben Pollard 2011 - 2023.", leftMargin, lastY + 2, availableWidth, AuthorColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
