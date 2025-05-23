﻿using System.Collections.Generic;
using System.Linq;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents a view point from a room.
    /// </summary>
    public sealed class ViewPoint
    {
        #region StaticProperties

        /// <summary>
        /// Get a view point representing no view.
        /// </summary>
        public static ViewPoint NoView { get; } = new();

        #endregion

        #region Properties

        /// <summary>
        /// Get the room that lies in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>The room.</returns>
        public Room this[Direction direction] => SurroundingRooms.ContainsKey(direction) ? SurroundingRooms[direction] : null;

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool Any => SurroundingRooms.Any();

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool AnyVisited => SurroundingRooms.Any(x => x.Value?.HasBeenVisited ?? false);

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool AnyNotVisited => SurroundingRooms.Any(x => !x.Value?.HasBeenVisited ?? false);

        /// <summary>
        /// Get the surrounding rooms.
        /// </summary>
        private Dictionary<Direction, Room> SurroundingRooms { get; } = [];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ViewPoint class.
        /// </summary>
        private ViewPoint()
        {

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new ViewPoint.
        /// </summary>
        /// <param name="region">The region to create the view point from.</param>
        /// <returns>The view point.</returns>
        public static ViewPoint Create(Region region)
        {
            return Create(region, region.CurrentRoom);
        }

        /// <summary>
        /// Create a new ViewPoint.
        /// </summary>
        /// <param name="region">The region to create the view point from.</param>
        /// <param name="room">The room within the region to create the view point from.</param>
        /// <returns>The view point.</returns>
        public static ViewPoint Create(Region region, Room room)
        {
            ViewPoint viewPoint = new();

            foreach (var direction in new[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.Up, Direction.Down })
            {
                if (room.FindExit(direction, false, out _))
                    viewPoint.SurroundingRooms.Add(direction, region.GetAdjoiningRoom(direction, room));
            }

            return viewPoint;
        }

        #endregion
    }
}
