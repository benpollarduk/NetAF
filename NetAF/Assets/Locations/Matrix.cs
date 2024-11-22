using System;
using System.Linq;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Provides a 3D matrix of rooms.
    /// </summary>
    /// <param name="roomPositions">The rooms to be represented.</param>
    public sealed class Matrix(RoomPosition[] roomPositions)
    {
        #region Fields

        private readonly RoomPosition[] roomPositions = roomPositions;

        #endregion

        #region Properties

        /// <summary>
        /// Get a room in this matrix.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z] => Array.Find(roomPositions, r => r.IsAtPosition(x, y, z))?.Room;

        /// <summary>
        /// Get the position of a room in this matrix.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The position of the room, else false.</returns>
        public Point3D? this[Room room] => Array.Find(roomPositions, r => r.Room == room)?.Position;

        /// <summary>
        /// Get the width of the matrix.
        /// </summary>
        public int Width => roomPositions.Length > 0 ? roomPositions.Max(r => r.Position.X) - roomPositions.Min(r => r.Position.X) + 1 : 0;

        /// <summary>
        /// Get the height of the matrix.
        /// </summary>
        public int Height => roomPositions.Length > 0 ? roomPositions.Max(r => r.Position.Y) - roomPositions.Min(r => r.Position.Y) + 1 : 0;

        /// <summary>
        /// Get the depth of the matrix.
        /// </summary>
        public int Depth => roomPositions.Length > 0 ? roomPositions.Max(r => r.Position.Z) - roomPositions.Min(r => r.Position.Z) + 1 : 0;

        #endregion

        #region Methods

        /// <summary>
        /// Return this matrix as a one dimensional array of rooms.
        /// </summary>
        /// <returns>The rooms, as a one dimensional array.</returns>
        public Room[] ToRooms()
        {
            return roomPositions.Select(x => x.Room).ToArray();
        }

        /// <summary>
        /// Find all rooms on a specified Z plane.
        /// </summary>
        /// <param name="z">The Z plane.</param>
        /// <returns>All rooms on the specified Z plane.</returns>
        public Room[] FindAllRoomsOnZ(int z)
        {
            return roomPositions.Where(r => r.Position.Z == z).Select(r => r.Room).ToArray();
        }

        /// <summary>
        /// Find the distance between two rooms.
        /// </summary>
        /// <param name="a">Room a.</param>
        /// <param name="b">Room b.</param>
        /// <returns>The distance between the two rooms.</returns>
        public double DistanceBetweenRooms(Room a, Room b)
        {
            var aPos = this[a];
            var bPos = this[b];

            if (!aPos.HasValue || !bPos.HasValue)
                return 0d;

            return DistanceBetweenPoints(aPos.Value, bPos.Value);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Find the distance between two points.
        /// </summary>
        /// <param name="a">Point a.</param>
        /// <param name="b">Point b.</param>
        /// <returns>The distance between the two points.</returns>
        public static double DistanceBetweenPoints(Point3D a, Point3D b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.Z - a.Z, 2));
        }

        #endregion
    }
}
