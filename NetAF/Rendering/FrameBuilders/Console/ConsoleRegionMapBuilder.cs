﻿using System;
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
        /// <param name="left">The left of the room.</param>
        /// <param name="top">The top of the room.</param>
        /// <param name="isCurrentRoom">True if this is the current room.</param>
        private void DrawCurrentFloorRoom(Room room, int left, int top, bool isCurrentRoom)
        {
            /*
             * |   |
             *  ^Ov|
             * |---|
             */

            var color = room.HasBeenVisited ? VisitedBoundaryColor : UnvisitedBoundaryColor;

            gridStringBuilder.SetCell(left, top, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(left + 1, top, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(left + 2, top, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(left + 3, top, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(left + 1, top, UnLockedExit, color);
                gridStringBuilder.SetCell(left + 2, top, UnLockedExit, color);
                gridStringBuilder.SetCell(left + 3, top, UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(left + 1, top, HorizontalBoundary, color);
                gridStringBuilder.SetCell(left + 2, top, HorizontalBoundary, color);
                gridStringBuilder.SetCell(left + 3, top, HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(left + 4, top, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(left, top + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(left, top + 1, UnLockedExit, color);
            else
                gridStringBuilder.SetCell(left, top + 1, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(left + 1, top + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(left + 1, top + 1, Convert.ToChar("^"), color);
            else
                gridStringBuilder.SetCell(left + 1, top + 1, EmptySpace, color);

            if (isCurrentRoom)
                gridStringBuilder.SetCell(left + 2, top + 1, Player, PlayerColor);
            else
                gridStringBuilder.SetCell(left + 2, top + 1, EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(left + 3, top + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(left + 3, top + 1, Convert.ToChar("v"), color);
            else
                gridStringBuilder.SetCell(left + 3, top + 1, EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(left + 4, top + 1, LockedExit, LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(left + 4, top + 1, UnLockedExit, color);
            else
                gridStringBuilder.SetCell(left + 4, top + 1, VerticalBoundary, color);

            gridStringBuilder.SetCell(left, top + 2, VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(left + 1, top + 2, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(left + 2, top + 2, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(left + 3, top + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(left + 1, top + 2, UnLockedExit, color);
                gridStringBuilder.SetCell(left + 2, top + 2, UnLockedExit, color);
                gridStringBuilder.SetCell(left + 3, top + 2, UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(left + 1, top + 2, HorizontalBoundary, color);
                gridStringBuilder.SetCell(left + 2, top + 2, HorizontalBoundary, color);
                gridStringBuilder.SetCell(left + 3, top + 2, HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(left + 4, top + 2, VerticalBoundary, color);
        }

        /// <summary>
        /// Draw a room on a lower level.
        /// </summary>
        /// <param name="left">The left of the room.</param>
        /// <param name="top">The top of the room.</param>
        private void DrawLowerLevelRoom(int left, int top)
        {
            /*
             * .....
             * .....
             * .....
             *
             */

            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 5; x++)
                    gridStringBuilder.SetCell(left + x, top + y, LowerLevel, LowerLevelColor);

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and convert a position in a matrix to a grid layout position.
        /// </summary>
        /// <param name="gridStartX">The x position to start at in the grid.</param>
        /// <param name="gridStartY">The x position to start at in the grid.</param>
        /// <param name="availableSize">The available size, in the grid.</param>
        /// <param name="matrix">The matrix.</param>
        /// <param name="roomX">The x position of the room, in the matrix.</param>
        /// <param name="roomY">The y position of the room, in the matrix.</param>
        /// <param name="playerX">The x position of the player, in the matrix.</param>
        /// <param name="playerY">The y position of the player, in the matrix.</param>
        /// <param name="gridLeft">The left position to begin rendering the room at, in the grid.</param>
        /// <param name="gridTop">The top position to begin rendering the room at, in the grid.</param>
        /// <returns>True if the matrix position could be converted to a grid position and fit in the available space.</returns>
        private static bool TryConvertMatrixPositionToGridLayoutPosition(int gridStartX, int gridStartY, Size availableSize, Matrix matrix, int roomX, int roomY, int playerX, int playerY, out int gridLeft, out int gridTop)
        {
            const int roomWidth = 5;
            const int roomHeight = 3;

            // set position of room, Y is inverted
            gridLeft = gridStartX + roomX * roomWidth;
            gridTop = gridStartY + (matrix.Height - 1) * roomHeight - roomY * roomHeight;

            // check if map will fit
            if (matrix.Width * roomWidth > availableSize.Width || matrix.Height * roomHeight > availableSize.Height)
            {
                // centralise on player
                gridLeft += availableSize.Width / 2 - playerX * roomWidth + roomWidth / 2;
                gridTop += availableSize.Height / 2 + (playerY - matrix.Height) * roomHeight - roomHeight / 2;
            }
            else
            {
                // centralise on area
                gridLeft += (int)Math.Floor(availableSize.Width / 2d - matrix.Width / 2d * roomWidth);
                gridTop += (int)Math.Floor(availableSize.Height / 2d - matrix.Height / 2d * roomHeight);
            }

            return gridLeft >= gridStartX &&
                   gridLeft + roomWidth - 1 < availableSize.Width &&
                   gridTop >= gridStartY &&
                   gridTop + roomHeight - 1 < availableSize.Height;
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="x">The x position to start building at.</param>
        /// <param name="y">The y position to start building at.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        public void BuildRegionMap(Region region, int x, int y, Size maxSize)
        {
            var matrix = region.ToMatrix();
            var currentRoom = region.GetPositionOfRoom(region.CurrentRoom);
            var currentFloor = currentRoom.Z;
            var rooms = matrix.ToRooms().Where(r => r != null).ToArray();
            var unvisitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => !r.Room.HasBeenVisited).ToList();
            var visitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => r.Room.HasBeenVisited).ToList();
            var multiLevel = matrix.Depth > 1;
            var indicatorLength = 3 + matrix.Depth.ToString().Length;
            var maxAvailableWidth = maxSize.Width;

            if (multiLevel)
            {
                // draw floor indicators

                for (var floor = matrix.Depth - 1; floor >= 0; floor--)
                {
                    var roomsOnThisFloor = rooms.Where(r => region.GetPositionOfRoom(r).Z == floor).ToArray();

                    // only draw levels indicators where a region is visible without discovery or a room on the floor has been visited
                    if (!region.VisibleWithoutDiscovery && !Array.Exists(roomsOnThisFloor, r => r.HasBeenVisited))
                        continue;

                    if (floor == currentFloor)
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
                List<RoomPosition> lowerLevelRooms = [.. visitedRoomPositions.Where(r => r.Z < currentFloor)];

                if (region.VisibleWithoutDiscovery)
                    lowerLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Z < currentFloor));

                foreach (var position in lowerLevelRooms)
                {
                    if (TryConvertMatrixPositionToGridLayoutPosition(x, y, new Size(maxAvailableWidth, maxSize.Height), matrix, position.X, position.Y, currentRoom.X, currentRoom.Y, out var left, out var top))
                        DrawLowerLevelRoom(left, top);
                }
            }

            // now current level
            List<RoomPosition> currentLevelRooms = [.. visitedRoomPositions.Where(r => r.Z == currentFloor)];

            if (region.VisibleWithoutDiscovery)
                currentLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Z == currentFloor));

            foreach (var position in currentLevelRooms)
            {
                if (TryConvertMatrixPositionToGridLayoutPosition(x, y, new Size(maxAvailableWidth, maxSize.Height), matrix, position.X, position.Y, currentRoom.X, currentRoom.Y, out var left, out var top))
                    DrawCurrentFloorRoom(position.Room, left, top, position.Room == region.CurrentRoom);
            }
        }

        #endregion
    }
}
