using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Common
{
    /// <summary>
    /// Provides a normal detail room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleNormalDetailRoomMapBuilder(GridStringBuilder gridStringBuilder, ConsoleMapRenderProperties properties) : IConsoleRoomMapBuilder
    {
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
                builder.SetCell(topLeft.X + 1, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
                builder.SetCell(topLeft.X + 2, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
                builder.SetCell(topLeft.X + 3, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y, properties.UnLockedExit, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y, properties.UnLockedExit, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y, properties.UnLockedExit, color);
            }
            else
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y, properties.HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y, properties.HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y, properties.HorizontalBoundary, color);
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
                builder.SetCell(topLeft.X, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.West))
                builder.SetCell(topLeft.X, topLeft.Y + 1, properties.UnLockedExit, color);
            else
                builder.SetCell(topLeft.X, topLeft.Y + 1, properties.VerticalBoundary, color);
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
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.East))
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.UnLockedExit, color);
            else
                builder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.VerticalBoundary, color);
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
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.UnLockedExit, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.UnLockedExit, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.UnLockedExit, color);
            }
            else
            {
                builder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.HorizontalBoundary, color);
                builder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.HorizontalBoundary, color);
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
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Up))
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, '^', color);
            else
                builder.SetCell(topLeft.X + 1, topLeft.Y + 1, properties.EmptySpace, color);
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
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Down))
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, 'v', color);
            else
                builder.SetCell(topLeft.X + 3, topLeft.Y + 1, properties.EmptySpace, color);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(5, 3);

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
             * |   |
             *  ^Ov|
             * |---|
            */

            DrawNorth(gridStringBuilder, room, startPosition, properties.BoundaryColor);
            DrawWest(gridStringBuilder, room, startPosition, properties.BoundaryColor);
            DrawUp(gridStringBuilder, room, startPosition, properties.BoundaryColor);
            DrawDown(gridStringBuilder, room, startPosition, properties.BoundaryColor);
            DrawEast(gridStringBuilder, room, startPosition, properties.BoundaryColor);
            DrawSouth(gridStringBuilder, room, startPosition, properties.BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 1, properties.EmptySpace, properties.BoundaryColor);

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, properties.VerticalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, properties.VerticalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);

            endX = startPosition.X + 4;
            endY = startPosition.Y + 2;
        }

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            return RenderedSize;
        }

        #endregion
    }
}
