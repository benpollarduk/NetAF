using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a high detail room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    public sealed class ConsoleHighDetailRoomMapBuilder(GridStringBuilder gridStringBuilder) : IConsoleRoomMapBuilder
    {
        #region StaticProperties

        /// <summary>
        /// Get the maximum size of the key.
        /// </summary>
        public static readonly Size MaximumKeySize = new(25, 4);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing a point of interest.
        /// </summary>
        public char PointOfInterest { get; set; } = '!';

        /// <summary>
        /// Get or set the character used for representing no point of interest.
        /// </summary>
        public char NoPointOfInterest { get; set; } = ' ';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; } = '+';

        /// <summary>
        /// Get or set the padding between the key and the map.
        /// </summary>
        public int KeyPadding { get; set; } = 6;

        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public AnsiColor BoundaryColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the point of interest color.
        /// </summary>
        public AnsiColor PointOfInterestColor { get; set; } = NetAFPalette.NetAFBlue;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; } = NetAFPalette.NetAFRed;

        /// <summary>
        /// Get or set the visited exit color.
        /// </summary>
        public AnsiColor VisitedExitColor { get; set; } = NetAFPalette.NetAFYellow;

        /// <summary>
        /// Get or set the unvisited exit color.
        /// </summary>
        public AnsiColor UnvisitedExitColor { get; set; } = NetAFPalette.NetAFGreen;

        /// <summary>
        /// Get or set if directions are displayed.
        /// </summary>
        public bool DisplayDirections { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Draw the north border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawNorthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, VerticalExitBorder, BoundaryColor);

                if (DisplayDirections)
                {
                    if (viewPoint[Direction.North]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'n', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'N', UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the south border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawSouthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 6, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);

                if (DisplayDirections)
                {
                    if (viewPoint[Direction.South]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 's', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 'S', UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 6, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the east border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawEastBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);

                if (DisplayDirections)
                {
                    if (viewPoint[Direction.East]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'e', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'E', UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 5, VerticalBoundary, BoundaryColor);
        }

        /// <summary>
        /// Draw the west border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawWestBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);

                if (DisplayDirections)
                {
                    if (viewPoint[Direction.West]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'w', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'W', UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 5, VerticalBoundary, BoundaryColor);
        }

        /// <summary>
        /// Draw the up exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawUpExit(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            if (room.HasLockedExitInDirection(Direction.Up))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Up))
            {
                if (DisplayDirections)
                {
                    if (viewPoint[Direction.Up]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'u', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'U', UnvisitedExitColor);
                }
                else
                {
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, '^', BoundaryColor);
                }
            }
        }

        /// <summary>
        /// Draw the down exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawDownExit(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            if (room.HasLockedExitInDirection(Direction.Down))
            {
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Down))
            {
                if (DisplayDirections)
                {
                    if (viewPoint[Direction.Down]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'd', VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'D', UnvisitedExitColor);
                }
                else
                {
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'v', BoundaryColor);
                }
            }
        }

        /// <summary>
        /// Get the point of interest indicator.
        /// </summary>
        /// <param name="numberOfPointsOfInterest">The number of points of interest.</param>
        /// <param name="detail">The point of interest detail.</param>
        /// <returns>The point of interest indicator.</returns>
        private char GetPointOfInterestIndicator(int numberOfPointsOfInterest, PointOfInterestDetail detail)
        {
            char indicator = NoPointOfInterest;

            switch (detail)
            {
                case PointOfInterestDetail.None:

                    break;

                case PointOfInterestDetail.Low:

                    if (numberOfPointsOfInterest > 0)
                        indicator = PointOfInterest;

                    break;

                case PointOfInterestDetail.High:

                    if (numberOfPointsOfInterest > 0)
                        indicator = numberOfPointsOfInterest < 10 ? numberOfPointsOfInterest.ToString()[0] : PointOfInterest;

                    break;

                default:

                    throw new NotImplementedException();
            }

            return indicator;
        }

        /// <summary>
        /// Draw the item or character.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="detail">The point of interest detail.</param>
        private void DrawItemOrCharacter(Room room, Point2D startPosition, PointOfInterestDetail detail)
        {
            var numberOfPointsOfInterest = GetNumberOfPointsOfInterest(room);
            var indicator = GetPointOfInterestIndicator(numberOfPointsOfInterest, detail);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 3, indicator, PointOfInterestColor);
        }

        /// <summary>
        /// Draw the key.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="options">The render options.</param>
        /// <param name="startPosition">The start position of the overall render.</param>
        /// <param name="mapStart">The start position of the map.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        private void DrawKey(Room room, ViewPoint viewPoint, RoomMapRenderOptions options, Point2D startPosition, Point2D mapStart, out int endX, out int endY)
        {
            var numberOfPointsOfInterest = GetNumberOfPointsOfInterest(room);
            var pointOfInterestIndicator = GetPointOfInterestIndicator(numberOfPointsOfInterest, options.PointOfInterestDetail);

            Dictionary<string, AnsiColor> keyLines = [];
            var lockedExitString = $"{LockedExit} = Locked Exit";
            var notVisitedExitString = "N/E/S/W/U/D = Unvisited";
            var visitedExitString = "n/e/s/w/u/d = Visited";
            var pointOfInterestString = options.PointOfInterestDetail switch
            {
                PointOfInterestDetail.None => string.Empty,
                PointOfInterestDetail.Low => $"{pointOfInterestIndicator} = Point of interest",
                PointOfInterestDetail.High => $"{pointOfInterestIndicator} = Point{(numberOfPointsOfInterest == 1 ? "" : "s")} of interest",
                _ => throw new NotImplementedException()
            };

            switch (options.KeyType)
            {
                case KeyType.Dynamic:

                    if (room.Exits.Where(x => x.IsPlayerVisible).Any(x => x.IsLocked))
                        keyLines.Add(lockedExitString, LockedExitColor);

                    if (viewPoint.AnyNotVisited)
                        keyLines.Add(notVisitedExitString, UnvisitedExitColor);

                    if (viewPoint.AnyVisited)
                        keyLines.Add(visitedExitString, VisitedExitColor);

                    if (room.EnteredFrom.HasValue)
                        keyLines.Add($"{room.EnteredFrom.Value.ToString().ToLower()[..1]} = Entrance", VisitedExitColor);

                    if (numberOfPointsOfInterest > 0 && options.PointOfInterestDetail != PointOfInterestDetail.None)
                        keyLines.Add(pointOfInterestString, PointOfInterestColor);

                    break;

                case KeyType.Full:

                    keyLines.Add(lockedExitString, LockedExitColor);
                    keyLines.Add(notVisitedExitString, UnvisitedExitColor);
                    keyLines.Add(visitedExitString, VisitedExitColor);
                    keyLines.Add(pointOfInterestString, PointOfInterestColor);

                    break;

                case KeyType.None:

                    break;

                default:

                    throw new NotImplementedException();
            }

            endX = mapStart.X + 8;
            endY = mapStart.Y;

            if (keyLines.Keys.Count == 0)
                return;

            int startKeyX;
            int startKeyY;

            switch (options.KeyPlacement)
            {
                case KeyPlacement.Below:
                    // place the key beneath the map, aligned to the map's left edge
                    startKeyX = mapStart.X;
                    startKeyY = mapStart.Y + RenderedSize.Height;
                    break;
                case KeyPlacement.Above:
                    // place the key above the map, aligned to the map's left edge
                    startKeyX = mapStart.X;
                    startKeyY = startPosition.Y - 1;
                    break;
                case KeyPlacement.Left:
                    // place the key to the left of the map, aligned to the overall left edge
                    startKeyX = startPosition.X;
                    startKeyY = mapStart.Y;
                    break;
                case KeyPlacement.Right:
                    // place the key to the right of the map
                    startKeyX = endX + KeyPadding;
                    startKeyY = mapStart.Y;
                    break;
                default:
                    throw new NotImplementedException();
            }

            var maxWidth = keyLines.Max(x => x.Key.Length) + startKeyX + 1;
            endY = startKeyY;

            foreach (var keyLine in keyLines)
                gridStringBuilder.DrawWrapped(keyLine.Key, startKeyX, endY + 1, maxWidth, keyLine.Value, out endX, out endY);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the number of points of interest.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The number of points of interest.</returns>
        private static int GetNumberOfPointsOfInterest(Room room)
        {
            var items = room.Items.Where(x => x.IsPlayerVisible).ToArray();
            var characters = room.Characters.Where(x => x.IsPlayerVisible).ToArray();
            return items.Length + characters.Length;
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(9, 7);

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            BuildRoomMap(room, viewPoint, options, new Point2D(0, 0), out _, out _);
        }

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            // determine the required size
            Size renderSizeWithKey = options.KeyPlacement switch
            {
                KeyPlacement.Below => new Size(Math.Max(RenderedSize.Width, MaximumKeySize.Width), RenderedSize.Height + MaximumKeySize.Height),
                KeyPlacement.Above => new Size(Math.Max(RenderedSize.Width, MaximumKeySize.Width), RenderedSize.Height + MaximumKeySize.Height),
                KeyPlacement.Right => new Size(RenderedSize.Width + KeyPadding + MaximumKeySize.Width, Math.Max(RenderedSize.Height, MaximumKeySize.Height)),
                KeyPlacement.Left => new Size(RenderedSize.Width + KeyPadding + MaximumKeySize.Width, Math.Max(RenderedSize.Height, MaximumKeySize.Height)),
                _ => throw new NotImplementedException()
            };

            // get size depending on key
            return options.KeyType switch
            {
                KeyType.None => RenderedSize,
                KeyType.Dynamic => renderSizeWithKey,
                KeyType.Full => renderSizeWithKey,
                _ => throw new NotImplementedException()
            };
        }

        #endregion

        #region Implementation of IConsoleRoomMapBuilder

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options, Point2D startPosition, out int endX, out int endY)
        {
            /*
             * *-| N |-*
             * |       |
             * - U   D -
             * W   ?   E
             * -       -
             * |       |
             * *-| S |-*
             */

            // offset the map to leave room for the key when it is placed to the left or above
            var mapStart = options.KeyType == KeyType.None ? startPosition : options.KeyPlacement switch
            {
                KeyPlacement.Left => new Point2D(startPosition.X + MaximumKeySize.Width + KeyPadding, startPosition.Y),
                KeyPlacement.Above => new Point2D(startPosition.X, startPosition.Y + MaximumKeySize.Height),
                _ => startPosition
            };

            DrawNorthBorder(room, viewPoint, mapStart);
            DrawSouthBorder(room, viewPoint, mapStart);
            DrawEastBorder(room, viewPoint, mapStart);
            DrawWestBorder(room, viewPoint, mapStart);
            DrawUpExit(room, viewPoint, mapStart);
            DrawDownExit(room, viewPoint, mapStart);
            DrawItemOrCharacter(room, mapStart, options.PointOfInterestDetail);
            DrawKey(room, viewPoint, options, startPosition, mapStart, out endX, out endY);

            if (endY < mapStart.Y + 6)
                endY = mapStart.Y + 6;
        }

        #endregion
    }
}
