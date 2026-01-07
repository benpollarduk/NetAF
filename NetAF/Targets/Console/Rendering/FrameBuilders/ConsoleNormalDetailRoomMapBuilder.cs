using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a normal detail room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    public sealed class ConsoleNormalDetailRoomMapBuilder(GridStringBuilder gridStringBuilder) : IConsoleRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing an unlocked exit.
        /// </summary>
        public char UnLockedExit { get; set; } = ' ';

        /// <summary>
        /// Get or set the character used for representing an empty space.
        /// </summary>
        public char EmptySpace { get; set; } = ' ';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '-';

        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public AnsiColor BoundaryColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; } = NetAFPalette.NetAFRed;

        #endregion

        #region Methods

        /// <summary>
        /// Draw the north exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawNorth(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            if (room.HasLockedExitInDirection(Direction.North))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y, LockedExit, LockedExitColor);
                builder.SetCell(topLeft.X + 2, topLeft.Y, LockedExit, LockedExitColor);
                builder.SetCell(topLeft.X + 3, topLeft.Y, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y, UnLockedExit, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y, UnLockedExit, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y, UnLockedExit, color);
            }
            else
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y, HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y, HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y, HorizontalBoundary, color);
            }
        }

        /// <summary>
        /// Draw the west exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawWest(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            if (room.HasLockedExitInDirection(Direction.West))
                builder.SetCell(topLeft.X, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.West))
                builder.SetCell(topLeft.X, topLeft.Y + 1, UnLockedExit, color);
            else
                builder.SetCell(topLeft.X, topLeft.Y + 1, VerticalBoundary, color);
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
            if (room.HasLockedExitInDirection(Direction.East))
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.East))
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, UnLockedExit, color);
            else
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, VerticalBoundary, color);
        }

        /// <summary>
        /// Draw the south exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawSouth(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            if (room.HasLockedExitInDirection(Direction.South))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, LockedExit, LockedExitColor);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, LockedExit, LockedExitColor);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, UnLockedExit, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, UnLockedExit, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, UnLockedExit, color);
            }
            else
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, HorizontalBoundary, color);
            }
        }

        /// <summary>
        /// Draw the up exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawUp(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            if (room.HasLockedExitInDirection(Direction.Up))
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Up))
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, '^', color);
            else
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, EmptySpace, color);
        }

        /// <summary>
        /// Draw the down exit.
        /// </summary>
        /// <param name="builder">The builder to draw with</param>
        /// <param name="room">The room.</param>
        /// <param name="topLeft">The top left cell of the room.</param>
        /// <param name="color">The color</param>
        private void DrawDown(GridStringBuilder builder, Room room, Point2D topLeft, AnsiColor color)
        {
            if (room.HasLockedExitInDirection(Direction.Down))
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Down))
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, 'v', color);
            else
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, EmptySpace, color);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Get the rendered size of the room, excluding any keys.
        /// </summary>
        public Size RenderedSize => new(5, 3);

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
             * |   |
             *  ^Ov|
             * |---|
            */

            DrawNorth(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawWest(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawUp(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawDown(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawEast(gridStringBuilder, room, startPosition, BoundaryColor);
            DrawSouth(gridStringBuilder, room, startPosition, BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 1, EmptySpace, BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, VerticalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, VerticalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, VerticalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 2, VerticalBoundary, BoundaryColor);

            endX = startPosition.X + 4;
            endY = startPosition.Y + 2;
        }

        #endregion
    }
}
