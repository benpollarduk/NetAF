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

        #endregion
    }
}
