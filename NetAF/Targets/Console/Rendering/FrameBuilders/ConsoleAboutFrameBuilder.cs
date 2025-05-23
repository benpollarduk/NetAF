﻿using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of console about frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleAboutFrameBuilder(GridStringBuilder gridStringBuilder) : IAboutFrameBuilder
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
        /// Get or set the name color.
        /// </summary>
        public AnsiColor NameColor { get; set; } = NetAFPalette.NetAFGreen;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = NetAFPalette.NetAFYellow;

        /// <summary>
        /// Get or set the author color.
        /// </summary>
        public AnsiColor AuthorColor { get; set; } = AnsiColor.BrightWhite;

        #endregion

        #region Implementation of IAboutFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, Game game, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            gridStringBuilder.DrawWrapped(game.Info.Name, leftMargin, lastY + 3, availableWidth, NameColor, out _, out lastY);

            gridStringBuilder.DrawWrapped(game.Info.Description.EnsureFinishedSentence(), leftMargin, lastY + 2, availableWidth, DescriptionColor, out _, out lastY);

            if (!string.IsNullOrEmpty(game.Info.Author))
                gridStringBuilder.DrawWrapped($"Created by: {game.Info.Author}".EnsureFinishedSentence(), leftMargin, lastY + 2, availableWidth, AuthorColor, out _, out _);
            else
                gridStringBuilder.DrawWrapped(Info.AboutNetAF, leftMargin, lastY + 2, availableWidth, AuthorColor, out _, out _);

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
