namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents a room position.
    /// </summary>
    /// <param name="room">The room/</param>
    /// <param name="position">The position of the room.</param>
    public class RoomPosition(Room room, Point3D position)
    {
        #region Properties

        /// <summary>
        /// Get the room.
        /// </summary>
        public Room Room { get; } = room;

        /// <summary>
        /// Get the position of the room.
        /// </summary>
        public Point3D Position { get; } = position;

        #endregion

        #region Methods

        /// <summary>
        /// Get if this RoomPosition is at a position.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="z">The Z position.</param>
        /// <returns>True if this is at the position, else false.</returns>
        public bool IsAtPosition(int x, int y, int z)
        {
            return IsAtPosition(new Point3D(x, y, z));
        }

        /// <summary>
        /// Get if this RoomPosition is at a position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>True if this is at the position, else false.</returns>
        public bool IsAtPosition(Point3D position)
        {
            return Position.X == position.X && Position.Y == position.Y && Position.Z == position.Z;
        }

        #endregion
    }
}
