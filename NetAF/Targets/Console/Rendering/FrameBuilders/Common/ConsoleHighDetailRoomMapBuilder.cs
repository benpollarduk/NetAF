using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Common
{
    /// <summary>
    /// Provides a high detail room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The grid string builder.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleHighDetailRoomMapBuilder(GridStringBuilder gridStringBuilder, ConsoleMapRenderProperties properties) : IConsoleRoomMapBuilder
    {
        #region StaticProperties

        /// <summary>
        /// Get the maximum size of the key.
        /// </summary>
        public static readonly Size MaximumKeySize = new(24, 4);

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
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, properties.Corner, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);

                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.North]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'n', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'N', properties.UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y, properties.Corner, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the south border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawSouthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 6, properties.Corner, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);

                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.South]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 's', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 'S', properties.UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 6, properties.Corner, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the east border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawEastBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 1, properties.VerticalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);

                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.East]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'e', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'E', properties.UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.VerticalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 5, properties.VerticalBoundary, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the west border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawWestBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 1, properties.VerticalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);

                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.West]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'w', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'W', properties.UnvisitedExitColor);
                }

                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.VerticalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 5, properties.VerticalBoundary, properties.BoundaryColor);
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
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Up))
            {
                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.Up]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'u', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'U', properties.UnvisitedExitColor);
                }
                else
                {
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, '^', properties.BoundaryColor);
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
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Down))
            {
                if (properties.DisplayDirections)
                {
                    if (viewPoint[Direction.Down]?.HasBeenVisited ?? false)
                        gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'd', properties.VisitedExitColor);
                    else
                        gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'D', properties.UnvisitedExitColor);
                }
                else
                {
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'v', properties.BoundaryColor);
                }
            }
        }

        /// <summary>
        /// Get the point of interest indicator.
        /// </summary>
        /// <param name="total">The total number of points of interest.</param>
        /// <param name="unexamined">The number of unexamined points of interest.</param>
        /// <param name="detail">The point of interest detail.</param>
        /// <returns>The point of interest indicator.</returns>
        private char GetPointOfInterestIndicator(int total, int unexamined, PointOfInterestDetail detail)
        {
            char indicator = properties.NoPointOfInterest;

            switch (detail)
            {
                case PointOfInterestDetail.None:

                    break;

                case PointOfInterestDetail.Low:

                    if (total > 0)
                        indicator = properties.PointOfInterest;

                    break;

                case PointOfInterestDetail.High:

                    if (unexamined > 9)
                        indicator = properties.PointOfInterest;
                    else if (unexamined > 0)
                        indicator = unexamined.ToString()[0];
                    else if (total > 0)
                        indicator = properties.PointOfInterest;

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
            GetNumberOfPointsOfInterest(room, out var total, out var unexamined);
            var indicator = GetPointOfInterestIndicator(total, unexamined, detail);
            gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 3, indicator, properties.PointOfInterestColor);
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
            GetNumberOfPointsOfInterest(room, out var total, out var unexamined);
            var pointOfInterestIndicator = GetPointOfInterestIndicator(total, unexamined, options.PointOfInterestDetail);

            Dictionary<string, AnsiColor> keyLines = [];
            var lockedExitString = $"{properties.LockedExit} = Locked Exit";
            var notVisitedExitString = "N/E/S/W/U/D = Unvisited";
            var visitedExitString = "n/e/s/w/u/d = Visited";
            var pointOfInterestString = GetPointOfInterestStringForKey(options.PointOfInterestDetail, pointOfInterestIndicator, unexamined);

            switch (options.KeyType)
            {
                case KeyType.Dynamic:

                    if (room.Exits.Where(x => x.IsPlayerVisible).Any(x => x.IsLocked))
                        keyLines.Add(lockedExitString, properties.LockedExitColor);

                    if (viewPoint.AnyNotVisited)
                        keyLines.Add(notVisitedExitString, properties.UnvisitedExitColor);

                    if (viewPoint.AnyVisited)
                        keyLines.Add(visitedExitString, properties.VisitedExitColor);

                    if (room.EnteredFrom.HasValue)
                        keyLines.Add($"{room.EnteredFrom.Value.ToString().ToLower()[..1]} = Entrance", properties.VisitedExitColor);

                    if (total > 0 && options.PointOfInterestDetail != PointOfInterestDetail.None)
                        keyLines.Add(pointOfInterestString, properties.PointOfInterestColor);

                    break;

                case KeyType.Full:

                    keyLines.Add(lockedExitString, properties.LockedExitColor);
                    keyLines.Add(notVisitedExitString, properties.UnvisitedExitColor);
                    keyLines.Add(visitedExitString, properties.VisitedExitColor);
                    keyLines.Add(pointOfInterestString, properties.PointOfInterestColor);

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
                    startKeyY = mapStart.Y + RenderedSize.Height + properties.VerticalKeyPadding;
                    break;
                case KeyPlacement.Above:
                    // place the key above the map, aligned to the map's left edge
                    startKeyX = mapStart.X;
                    startKeyY = startPosition.Y - properties.VerticalKeyPadding;
                    break;
                case KeyPlacement.Left:
                    // place the key to the left of the map, aligned to the overall left edge
                    startKeyX = startPosition.X;
                    startKeyY = mapStart.Y;
                    break;
                case KeyPlacement.Right:
                    // place the key to the right of the map
                    startKeyX = endX + properties.HorizontalKeyPadding;
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
        /// Get a string representing the point of interest.
        /// </summary>
        /// <param name="detail">The level of detail for the point of interest.</param>
        /// <param name="pointOfInterestIndicator">The indicator for the point of interest.</param>
        /// <param name="numberOfPointsOfInterest">The number of points of interest.</param>
        /// <returns>A string representing the point of interest.</returns>
        private static string GetPointOfInterestStringForKey(PointOfInterestDetail detail, char pointOfInterestIndicator, int numberOfPointsOfInterest)
        {
            return detail switch
            {
                PointOfInterestDetail.None => string.Empty,
                PointOfInterestDetail.Low => $"{pointOfInterestIndicator} = Point of interest",
                PointOfInterestDetail.High => $"{pointOfInterestIndicator} = Point{(numberOfPointsOfInterest == 1 ? "" : "s")} of interest",
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Get the number of points of interest.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="total">The total number of points of interest.</param>
        /// <param name="unexamined">The number of unexamined points of interest.</param>
        private static void GetNumberOfPointsOfInterest(Room room, out int total, out int unexamined)
        {
            var items = room.Items.Where(x => x.IsPlayerVisible).ToArray();
            var characters = room.Characters.Where(x => x.IsPlayerVisible).ToArray();
            total = items.Length + characters.Length;
            unexamined = items.Count(x => !x.HasBeenExamined) + characters.Count(x => !x.HasBeenExamined);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(9, 7);

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            // determine the required size
            Size renderSizeWithKey = options.KeyPlacement switch
            {
                KeyPlacement.Below => new Size(Math.Max(RenderedSize.Width, MaximumKeySize.Width), RenderedSize.Height + MaximumKeySize.Height + properties.VerticalKeyPadding),
                KeyPlacement.Above => new Size(Math.Max(RenderedSize.Width, MaximumKeySize.Width), RenderedSize.Height + MaximumKeySize.Height + properties.VerticalKeyPadding),
                KeyPlacement.Right => new Size(RenderedSize.Width + properties.HorizontalKeyPadding + MaximumKeySize.Width, Math.Max(RenderedSize.Height, MaximumKeySize.Height)),
                KeyPlacement.Left => new Size(RenderedSize.Width + properties.HorizontalKeyPadding + MaximumKeySize.Width, Math.Max(RenderedSize.Height, MaximumKeySize.Height)),
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
                KeyPlacement.Left => new Point2D(startPosition.X + MaximumKeySize.Width + properties.HorizontalKeyPadding, startPosition.Y),
                KeyPlacement.Above => new Point2D(startPosition.X, startPosition.Y + MaximumKeySize.Height + properties.VerticalKeyPadding),
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
