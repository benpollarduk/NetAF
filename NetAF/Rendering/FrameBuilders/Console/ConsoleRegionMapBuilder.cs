using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;

namespace NetAF.Rendering.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    public sealed class ConsoleRegionMapBuilder(GridStringBuilder gridStringBuilder) : IRegionMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = Convert.ToChar("x");

        /// <summary>
        /// Get or set the character used for representing an unlocked exit.
        /// </summary>
        public char UnLockedExit { get; set; } = Convert.ToChar(" ");

        /// <summary>
        /// Get or set the character used for representing an empty space.
        /// </summary>
        public char EmptySpace { get; set; } = Convert.ToChar(" ");

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = Convert.ToChar("-");

        /// <summary>
        /// Get or set the character to use for lower levels.
        /// </summary>
        public char LowerLevel { get; set; } = Convert.ToChar(".");

        /// <summary>
        /// Get or set the character to use for indicating the player.
        /// </summary>
        public char Player { get; set; } = Convert.ToChar("O");

        /// <summary>
        /// Get or set the character to use for the current floor.
        /// </summary>
        public char CurrentFloorIndicator { get; set; } = Convert.ToChar("*");

        /// <summary>
        /// Get or set the focused room boundary color.
        /// </summary>
        public AnsiColor FocusedBoundaryColor { get; set; } = AnsiColor.Cyan;

        /// <summary>
        /// Get or set the visited room boundary color.
        /// </summary>
        public AnsiColor VisitedBoundaryColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the unvisited room boundary color.
        /// </summary>
        public AnsiColor UnvisitedBoundaryColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the player color.
        /// </summary>
        public AnsiColor PlayerColor { get; set; } = AnsiColor.Blue;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; } = AnsiColor.Red;

        /// <summary>
        /// Get or set the lower level color.
        /// </summary>
        public AnsiColor LowerLevelColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set if lower floors should be shown.
        /// </summary>
        public bool ShowLowerFloors { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Draw a room on the current floor.
        /// </summary>
        /// <param name="room">The room to draw.</param>
        /// <param name="topLeft">The top left of the room.</param>
        /// <param name="isPlayerRoom">True if this is the player room.</param>
        /// <param name="isFocusRoom">True if this is the focus room.</param>
        private void DrawCurrentFloorRoom(Room room, Point2D topLeft, bool isPlayerRoom, bool isFocusRoom)
        {
            /*
             * |   |
             *  ^Ov|
             * |---|
             */            

            AnsiColor color = UnvisitedBoundaryColor;

            if (isFocusRoom)
                color = FocusedBoundaryColor;
            else if (room.HasBeenVisited)
                color = VisitedBoundaryColor;

            gridStringBuilder.SetCell(topLeft.X, topLeft.Y, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, UnLockedExit, color);
            else
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, Convert.ToChar("^"), color);
            else
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, EmptySpace, color);

            if (isPlayerRoom)
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 1, Player, PlayerColor);
            else
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 1, EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, Convert.ToChar("v"), color);
            else
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, UnLockedExit, color);
            else
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, VerticalBoundary, color);

            gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 2, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 2, VerticalBoundary, color);
        }

        /// <summary>
        /// Draw a room on a lower level.
        /// </summary>
        /// <param name="topLeft">The top left of the room.</param>
        private void DrawLowerLevelRoom(Point2D topLeft)
        {
            /*
             * .....
             * .....
             * .....
             *
             */

            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 5; x++)
                    gridStringBuilder.SetCell(topLeft.X + x, topLeft.Y + y, LowerLevel, LowerLevelColor);

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and convert a position in a matrix to a grid layout position.
        /// </summary>
        /// <param name="gridStartPosition">The position to start building at.</param>
        /// <param name="availableSize">The available size, in the grid.</param>
        /// <param name="matrix">The matrix.</param>
        /// <param name="roomPosition">The position of the room, in the matrix.</param>
        /// <param name="playerPosition">The x position of the player, in the matrix.</param>
        /// <param name="gridLeft">The left position to begin rendering the room at, in the grid.</param>
        /// <param name="gridTop">The top position to begin rendering the room at, in the grid.</param>
        /// <returns>True if the matrix position could be converted to a grid position and fit in the available space.</returns>
        private static bool TryConvertMatrixPositionToGridLayoutPosition(Point2D gridStartPosition, Size availableSize, Matrix matrix, Point2D roomPosition, Point2D playerPosition, out int gridLeft, out int gridTop)
        {
            const int roomWidth = 5;
            const int roomHeight = 3;

            // set position of room, Y is inverted
            gridLeft = gridStartPosition.X + roomPosition.X * roomWidth;
            gridTop = gridStartPosition.Y + (matrix.Height - 1) * roomHeight - roomPosition.Y * roomHeight;

            // check if map will fit
            if (matrix.Width * roomWidth > availableSize.Width || matrix.Height * roomHeight > availableSize.Height)
            {
                // centralise on player
                gridLeft += availableSize.Width / 2 - playerPosition.X * roomWidth + roomWidth / 2;
                gridTop += availableSize.Height / 2 + (playerPosition.Y - matrix.Height) * roomHeight - roomHeight / 2;
            }
            else
            {
                // centralise on area
                gridLeft += (int)Math.Floor(availableSize.Width / 2d - matrix.Width / 2d * roomWidth);
                gridTop += (int)Math.Floor(availableSize.Height / 2d - matrix.Height / 2d * roomHeight);
            }

            return gridLeft >= gridStartPosition.X &&
                   gridLeft + roomWidth - 1 < availableSize.Width &&
                   gridTop >= gridStartPosition.Y &&
                   gridTop + roomHeight - 1 < availableSize.Height;
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        public void BuildRegionMap(Region region, Point2D startPosition, Point3D focusPosition, Size maxSize)
        {
            var matrix = region.ToMatrix();
            var playerRoom = region.GetPositionOfRoom(region.CurrentRoom);
            var playerFloor = playerRoom.Position.Z;
            var focusFloor = focusPosition.Z;
            var rooms = matrix.ToRooms().Where(r => r != null).ToArray();
            var unvisitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => !r.Room.HasBeenVisited).ToList();
            var visitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => r.Room.HasBeenVisited).ToList();
            var multiLevel = matrix.Depth > 1;
            var indicatorLength = 3 + matrix.Depth.ToString().Length;
            var maxAvailableWidth = maxSize.Width;
            var x = startPosition.X;
            var y = startPosition.Y;

            if (multiLevel)
            {
                // draw floor indicators

                for (var floor = matrix.Depth - 1; floor >= 0; floor--)
                {
                    var roomsOnThisFloor = rooms.Where(r => region.GetPositionOfRoom(r).Position.Z == floor).ToArray();

                    // only draw levels indicators where a region is visible without discovery or a room on the floor has been visited
                    if (!region.VisibleWithoutDiscovery && !Array.Exists(roomsOnThisFloor, r => r.HasBeenVisited))
                        continue;

                    if (floor == playerFloor)
                        gridStringBuilder.DrawWrapped($"{CurrentFloorIndicator} L{floor}", x, ++y, maxAvailableWidth, VisitedBoundaryColor, out _, out _);
                    else
                        gridStringBuilder.DrawWrapped($"L{floor}", x + 2, ++y, maxAvailableWidth, LowerLevelColor, out _, out _);
                }

                x += indicatorLength;
                maxAvailableWidth -= indicatorLength;
            }

            // firstly draw lower levels
            if (ShowLowerFloors)
            {
                List<RoomPosition> lowerLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z < focusFloor)];

                if (region.VisibleWithoutDiscovery)
                    lowerLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z < focusFloor));

                foreach (var position in lowerLevelRooms)
                {
                    if (TryConvertMatrixPositionToGridLayoutPosition(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), matrix, new Point2D(position.Position.X, position.Position.Y), new Point2D(playerRoom.Position.X, playerRoom.Position.Y), out var left, out var top))
                        DrawLowerLevelRoom(new Point2D(left, top));
                }
            }

            // now focus level
            List<RoomPosition> focusLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z == focusFloor)];

            if (region.VisibleWithoutDiscovery)
                focusLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z == focusFloor));

            foreach (var position in focusLevelRooms)
            {
                if (TryConvertMatrixPositionToGridLayoutPosition(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), matrix, new Point2D(position.Position.X, position.Position.Y), new Point2D(playerRoom.Position.X, playerRoom.Position.Y), out var left, out var top))
                    DrawCurrentFloorRoom(position.Room, new Point2D(left, top), position.Room == playerRoom.Room, position.Position.Equals(focusPosition));
            }
        }

        #endregion
    }
}
