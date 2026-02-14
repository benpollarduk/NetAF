using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using System;

namespace NetAF.Rendering
{
    /// <summary>
    /// Represents a visual.
    /// </summary>
    /// <param name="Name">The name of the visual.</param>
    /// <param name="Description">A description of the visual.</param>
    /// <param name="VisualBuilder">The builder that creates the visual.</param>
    public record Visual(string Name, string Description, GridVisualBuilder VisualBuilder)
    {
        #region Methods

        /// <summary>
        /// Crop this visual to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The cropped visual.</returns>
        public Visual Crop(Size newSize) => new(Name, Description, Crop(VisualBuilder, newSize, AnsiColor.Black, AnsiColor.White));

        /// <summary>
        /// Scale this visual to a new size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <returns>The scaled visual.</returns>
        public Visual Scale(Size newSize) => new(Name, Description, Scale(VisualBuilder, newSize, AnsiColor.Black, AnsiColor.White));

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

            for (var row = 0; row < newHeight; row++)
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
            var widthRatio = visualBuilder.DisplaySize.Width / (double)renderSize.Width;
            var heightRatio = visualBuilder.DisplaySize.Height / (double)renderSize.Height;
            var newBuilder = new GridVisualBuilder(background, foreground);
            newBuilder.Resize(renderSize);

            for (var row = 0; row < renderSize.Height; row++)
            {
                var sourceRow = (int)Math.Floor(heightRatio * row);

                for (var column = 0; column < renderSize.Width; column++)
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
    }
}
