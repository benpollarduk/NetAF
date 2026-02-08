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
    public sealed class ConsoleVisualFrameBuilder(GridStringBuilder gridStringBuilder, VisualFrameResizeMode resizeMode = VisualFrameResizeMode.Scale) : IVisualFrameBuilder
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
        public VisualFrameResizeMode ResizeMode { get; set; } = resizeMode;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Crop a grid visual builder to a specified size.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="renderSize">The render size.</param>
        /// <param name="background">The background color.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The cropped visual builder.</returns>
        private static GridVisualBuilder Crop(GridVisualBuilder visualBuilder, Size renderSize, AnsiColor background, AnsiColor foreground)
        {
            // can't crop if the source is smaller than the render size
            if (visualBuilder.DisplaySize.Width < renderSize.Width && visualBuilder.DisplaySize.Height < renderSize.Height)
                return visualBuilder;

            var newWidth = Math.Min(renderSize.Width, visualBuilder.DisplaySize.Width);
            var newHeight = Math.Min(renderSize.Height, visualBuilder.DisplaySize.Height);
            var newSize = new Size(newWidth, newHeight);
            var newBuilder = new GridVisualBuilder(background, foreground);
            newBuilder.Resize(newSize);

            for (var row = 0;  row < newHeight; row++)
            {
                for (var column = 0; column < newWidth; column++)
                {
                    var cellBackground = visualBuilder.GetCellBackgroundColor(column, row);
                    var cellForeground = visualBuilder.GetCellForegroundColor(column, row);
                    var character = visualBuilder.GetCharacter(column, row);
                    newBuilder.SetCell(column, row, character, cellForeground, cellBackground);
                }
            }

            return newBuilder;
        }

        /// <summary>
        /// Scale a grid visual builder to a specified size.
        /// </summary>
        /// <param name="visualBuilder">The visual builder.</param>
        /// <param name="renderSize">The render size.</param>
        /// <param name="background">The background color.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The scaled visual builder.</returns>
        private static GridVisualBuilder Scale(GridVisualBuilder visualBuilder, Size renderSize, AnsiColor background, AnsiColor foreground)
        {
            var newWidth = Math.Min(renderSize.Width, visualBuilder.DisplaySize.Width);
            var newHeight = Math.Min(renderSize.Height, visualBuilder.DisplaySize.Height);
            var newSize = new Size(newWidth, newHeight);
            var widthRatio = visualBuilder.DisplaySize.Width / (double)newWidth;
            var heightRatio = visualBuilder.DisplaySize.Height / (double)newHeight;
            var newBuilder = new GridVisualBuilder(background, foreground);
            newBuilder.Resize(newSize);

            for (var row = 0; row < newHeight; row++)
            {
                var sourceRow = (int)Math.Floor(heightRatio * row);

                for (var column = 0; column < newWidth; column++)
                {
                    var sourceColumn = (int)Math.Floor(widthRatio * column);
                    var cellBackground = visualBuilder.GetCellBackgroundColor(sourceColumn, sourceRow);
                    var cellForeground = visualBuilder.GetCellForegroundColor(sourceColumn, sourceRow);
                    var character = visualBuilder.GetCharacter(sourceColumn, sourceRow);
                    newBuilder.SetCell(column, row, character, cellForeground, cellBackground);
                }
            }

            return newBuilder;
        }

        #endregion

        #region Implementation of IVisualFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="gridVisualBuilder">The grid visual builder containing the visual.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, GridVisualBuilder gridVisualBuilder, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;
            
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawWrapped(description.EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 3;

            var availableHeight = size.Height - lastY - 1;

            GridVisualBuilder finalBuilder = new(BackgroundColor, TitleColor);
            finalBuilder.Resize(size);

            // determine the render size
            var renderSize = new Size(availableWidth, availableHeight);

            // check if resize of the visual is needed
            if (gridVisualBuilder.DisplaySize.Width != renderSize.Width || 
                gridVisualBuilder.DisplaySize.Height != renderSize.Height)
            {
                // perform resize
                gridVisualBuilder = ResizeMode switch
                {
                    VisualFrameResizeMode.Crop => Crop(gridVisualBuilder, renderSize, BackgroundColor, TitleColor),
                    VisualFrameResizeMode.Scale => Scale(gridVisualBuilder, renderSize, BackgroundColor, TitleColor),
                    _ => throw new NotImplementedException()
                };
            }

            var xOffset = Math.Max(leftMargin, size.Width / 2 - gridVisualBuilder.DisplaySize.Width / 2);
            var yOffset = Math.Max(lastY, size.Height / 2 - gridVisualBuilder.DisplaySize.Height / 2);

            finalBuilder.Overlay(0, 0, gridStringBuilder);
            finalBuilder.Overlay(xOffset, yOffset, gridVisualBuilder);

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridVisualFrame(finalBuilder) { ShowCursor = false };
        }

        #endregion
    }
}
