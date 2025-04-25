using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    public sealed class ConsoleRegionMapBuilder(GridStringBuilder gridStringBuilder) : IConsoleRegionMapBuilder
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
        /// Get or set the character to use for lower levels.
        /// </summary>
        public char LowerLevel { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for indicating the player.
        /// </summary>
        public char Player { get; set; } = 'O';

        /// <summary>
        /// Get or set the character to use for indicating the focus.
        /// </summary>
        public char Focus { get; set; } = '+';

        /// <summary>
        /// Get or set the character to use for the current floor.
        /// </summary>
        public char CurrentFloorIndicator { get; set; } = '*';

        /// <summary>
        /// Get or set the focused room boundary color.
        /// </summary>
        public AnsiColor FocusedBoundaryColor { get; set; } = NetAFPalette.NetAFBlue;

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
        public AnsiColor PlayerColor { get; set; } = NetAFPalette.NetAFBlue;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; } = NetAFPalette.NetAFRed;

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
        /// <param name="view">The view from the room.</param>
        /// <param name="detail">The level of detail to use.</param>
        /// <param name="isPlayerRoom">True if this is the player room.</param>
        /// <param name="isFocusRoom">True if this is the focus room.</param>
        private void DrawCurrentFloorRoom(Room room, Point2D topLeft, ViewPoint view, RegionMapDetail detail, bool isPlayerRoom, bool isFocusRoom)
        {
            // get the configured builder
            var builder = GetConfiguredRoomMapBuilder(detail, room.HasBeenVisited || isPlayerRoom, isFocusRoom); 

            // draw room
            builder.BuildRoomMap(room, view, KeyType.None, topLeft, out _, out _);

            if (!isPlayerRoom && !isFocusRoom)
                return;

            var roomSize = builder.RenderedSize;
            var markerLocationX = topLeft.X + (roomSize.Width / 2);
            var markerLocationY = topLeft.Y + (roomSize.Height / 2);

            // draw player or focus in room
            if (isPlayerRoom)
                gridStringBuilder.SetCell(markerLocationX, markerLocationY, Player, PlayerColor);
            else
                gridStringBuilder.SetCell(markerLocationX, markerLocationY, Focus, FocusedBoundaryColor);
        }

        /// <summary>
        /// Draw a room on a lower level.
        /// </summary>
        /// <param name="topLeft">The top left of the room.</param>
        /// <param name="roomSize">The size of the room.</param>
        private void DrawLowerLevelRoom(Point2D topLeft, Size roomSize)
        {
            /*
             * .....
             * .....
             * .....
             *
             */

            for (var y = 0; y < roomSize.Height; y++)
                for (var x = 0; x < roomSize.Width; x++)
                    gridStringBuilder.SetCell(topLeft.X + x, topLeft.Y + y, LowerLevel, LowerLevelColor);
        }

        /// <summary>
        /// Get the room map builder.
        /// </summary>
        /// <param name="detail">The detail level to use.</param>
        /// <param name="hasBeenVisited">If the room to be drawn has been visited.</param>
        /// <param name="isFocusRoom">If the room to be drawn is the focused room.</param>
        /// <returns>The configured room map builder.</returns>
        private IConsoleRoomMapBuilder GetConfiguredRoomMapBuilder(RegionMapDetail detail, bool hasBeenVisited, bool isFocusRoom)
        {
            AnsiColor boundaryColor = UnvisitedBoundaryColor;

            if (isFocusRoom)
                boundaryColor = FocusedBoundaryColor;
            else if (hasBeenVisited)
                boundaryColor = VisitedBoundaryColor;

            switch (detail)
            {
                case RegionMapDetail.Detailed:
                    var detailedBuilder = new ConsoleRoomMapBuilder(gridStringBuilder)
                    {
                        BoundaryColor = boundaryColor,
                        DisplayDirections = false
                    };
                    return detailedBuilder;
                default:
                    var undetatiledBuilder = new ConsoleBasicRoomMapBuilder(gridStringBuilder)
                    {
                        BoundaryColor = boundaryColor
                    };
                    return undetatiledBuilder;
            }
        }

        #endregion

        #region Records

        /// <summary>
        /// Provides parameters for matrix conversion.
        /// </summary>
        /// <param name="GridStartPosition">The position to start building at.</param>
        /// <param name="AvailableSize">The available size, in the grid.</param>
        /// <param name="RoomPosition">The position of the room, in the matrix.</param>
        /// <param name="RoomSize">The size of the room.</param>
        /// <param name="FocusPosition">The focus position, in the matrix.</param>
        private sealed record MatrixConversionParameters(Point2D GridStartPosition, Size AvailableSize, Point2D RoomPosition, Size RoomSize, Point2D FocusPosition);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and convert a position in a matrix to a grid layout position.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="parameters">The matrix conversion parameters.</param>
        /// <param name="gridLeft">The left position to begin rendering the room at, in the grid.</param>
        /// <param name="gridTop">The top position to begin rendering the room at, in the grid.</param>
        /// <returns>True if the matrix position could be converted to a grid position and fit in the available space.</returns>
        private static bool TryConvertMatrixPositionToGridLayoutPosition(Matrix matrix, MatrixConversionParameters parameters, out int gridLeft, out int gridTop)
        {
            // calculate room left in grid
            var tranlatedX = matrix.Width - parameters.RoomPosition.X;
            var tranlatedFocusX = matrix.Width - parameters.FocusPosition.X;
            var roomLeft = (tranlatedX - tranlatedFocusX) * parameters.RoomSize.Width;

            // calculate centralised left
            var centralAreaX = (parameters.AvailableSize.Width / 2d) - parameters.RoomSize.Width;
            var roomCentralisationX = parameters.RoomSize.Width * 0.5d;
            gridLeft = (int)Math.Floor(centralAreaX - roomLeft + roomCentralisationX);
            gridLeft += parameters.GridStartPosition.X;

            // calculate room top in grid
            var roomTop = (parameters.RoomPosition.Y - parameters.FocusPosition.Y) * parameters.RoomSize.Height;

            // calculate centralised top
            var centralAreaY = (parameters.AvailableSize.Height / 2d) - parameters.RoomSize.Height / 2d;
            var roomCentralisationY = parameters.RoomSize.Height * 0d;
            gridTop = (int)Math.Floor(centralAreaY - roomTop + roomCentralisationY);
            gridTop += parameters.GridStartPosition.Y;

            return gridLeft >= parameters.GridStartPosition.X &&
                   gridLeft + parameters.RoomSize.Width - 1 < parameters.AvailableSize.Width &&
                   gridTop >= parameters.GridStartPosition.Y &&
                   gridTop + parameters.RoomSize.Height - 1 < parameters.AvailableSize.Height;
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapDetail detail)
        {
            BuildRegionMap(region, focusPosition, detail, new(0, 0), new(int.MaxValue, int.MaxValue));
        }

        #endregion

        #region Implementation of IConsoleRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapDetail detail, Point2D startPosition, Size maxSize)
        {
            var matrix = region.ToMatrix();
            var playerRoom = region.GetPositionOfRoom(region.CurrentRoom);
            var playerFloor = playerRoom.Position.Z;
            var focusFloor = focusPosition.Z;
            var rooms = matrix.ToRooms().Where(r => r != null).ToArray();
            var unvisitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => !r.Room.HasBeenVisited).ToList();
            var visitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => r.Room.HasBeenVisited).ToList();
            var multiLevel = matrix.Depth > 1 && (region.IsVisibleWithoutDiscovery || matrix.FindAllZWithVisitedRooms().Length > 1);
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
                    if (!region.IsVisibleWithoutDiscovery && !Array.Exists(roomsOnThisFloor, r => r.HasBeenVisited))
                        continue;

                    var isFocusFloor = floor == focusFloor;

                    if (floor == playerFloor)
                        gridStringBuilder.DrawWrapped($"{CurrentFloorIndicator} L{floor}", x, ++y, maxAvailableWidth, VisitedBoundaryColor, out _, out _);
                    else
                        gridStringBuilder.DrawWrapped($"L{floor}", x + 2, ++y, maxAvailableWidth, isFocusFloor ? FocusedBoundaryColor : LowerLevelColor, out _, out _);
                }

                x += indicatorLength;
                maxAvailableWidth -= indicatorLength;
            }

            // determine the room size
            var roomSize = GetConfiguredRoomMapBuilder(detail, false, false).RenderedSize;

            // firstly draw lower levels
            if (ShowLowerFloors)
            {
                List<RoomPosition> lowerLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z < focusFloor)];

                if (region.IsVisibleWithoutDiscovery)
                    lowerLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z < focusFloor));

                foreach (var position in lowerLevelRooms)
                {
                    var roomOnFocusFloorAtXY = matrix[position.Position.X, position.Position.Y, focusFloor];
                    var hasVisibleRoomAtXYOnFocusFloor = roomOnFocusFloorAtXY != null && (roomOnFocusFloorAtXY.HasBeenVisited || region.IsVisibleWithoutDiscovery);

                    if (!hasVisibleRoomAtXYOnFocusFloor && TryConvertMatrixPositionToGridLayoutPosition(matrix, new MatrixConversionParameters(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), new Point2D(position.Position.X, position.Position.Y), roomSize, new Point2D(focusPosition.X, focusPosition.Y)), out var left, out var top))
                        DrawLowerLevelRoom(new Point2D(left, top), roomSize);
                }
            }

            // now focus level
            List<RoomPosition> focusLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z == focusFloor)];

            if (region.IsVisibleWithoutDiscovery)
                focusLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z == focusFloor));

            foreach (var position in focusLevelRooms)
            {
                if (TryConvertMatrixPositionToGridLayoutPosition(matrix, new MatrixConversionParameters(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), new Point2D(position.Position.X, position.Position.Y), roomSize, new Point2D(focusPosition.X, focusPosition.Y)), out var left, out var top))
                    DrawCurrentFloorRoom(position.Room, new Point2D(left, top), ViewPoint.Create(region, position.Room), detail, position.Room == playerRoom.Room, position.Position.Equals(focusPosition));
            }
        }

        #endregion
    }
}
