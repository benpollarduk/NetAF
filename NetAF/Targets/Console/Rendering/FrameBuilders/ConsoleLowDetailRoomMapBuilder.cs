using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;

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
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawWest(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            builder.SetCell(topLeft.X, topLeft.Y, VerticalBoundary, color);
        }

        /// <summary>
        /// Draw the east exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawEast(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            builder.SetCell(topLeft.X + 2, topLeft.Y, VerticalBoundary, color);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Get the rendered size of the room, excluding any keys.
        /// </summary>
        public Size RenderedSize => new(3, 1);

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key)
        {
            BuildRoomMap(room, viewPoint, key, new Point2D(0, 0), out _, out _);
        }

        #endregion

        #region Implementation of IConsoleRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key, Point2D startPosition, out int endX, out int endY)
        {
            /*
             * [O]
            */

            DrawWest(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawEast(gridStringBuilder, room, startPosition, BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, EmptySpace, BoundaryColor);
            
            endX = startPosition.X;
            endY = startPosition.Y;
        }

        #endregion
    }
}
