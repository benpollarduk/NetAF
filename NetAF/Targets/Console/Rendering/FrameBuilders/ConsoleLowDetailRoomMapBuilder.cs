using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using System;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a low detail room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    public sealed class ConsoleLowDetailRoomMapBuilder(GridStringBuilder gridStringBuilder) : IConsoleRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public AnsiColor BoundaryColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the character used for representing an empty space.
        /// </summary>
        public char EmptySpace { get; set; } = ' ';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        #endregion

        #region Methods

        /// <summary>
        /// Draw the west exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawWest(GridStringBuilder builder, Point2D topLeft, AnsiColor color)
        {
            builder.SetCell(topLeft.X, topLeft.Y, VerticalBoundary, color);
        }

        /// <summary>
        /// Draw the east exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawEast(GridStringBuilder builder, Point2D topLeft, AnsiColor color)
        {
            builder.SetCell(topLeft.X + 2, topLeft.Y, VerticalBoundary, color);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(3, 1);

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            BuildRoomMap(room, viewPoint, options, new Point2D(0, 0), out _, out _);
        }

        #endregion

        #region Implementation of IConsoleRoomMapBuilder

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options, Point2D startPosition, out int endX, out int endY)
        {
            /*
             * [O]
            */

            DrawWest(gridStringBuilder, startPosition, BoundaryColor);
            DrawEast(gridStringBuilder, startPosition, BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, EmptySpace, BoundaryColor);
            
            endX = startPosition.X;
            endY = startPosition.Y;
        }

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            return RenderedSize;
        }

        #endregion
    }
}
