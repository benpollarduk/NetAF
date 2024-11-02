using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Room.
    /// </summary>
    /// <param name="room">The room to serialize.</param>
    public sealed class RoomSerialization(Room room) : ExaminableSerialization(room), IObjectSerialization<Room>
    {
        #region Properties

        /// <summary>
        /// Get or set if the room has been visited.
        /// </summary>
        public bool HasBeenVisited { get; set; } = room?.HasBeenVisited ?? false;

        /// <summary>
        /// Get or set the item serializations.
        /// </summary>
        public ItemSerialization[] Items { get; set; } = room?.Items?.Select(x => new ItemSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get or set the exit serializations.
        /// </summary>
        public ExitSerialization[] Exits { get; set; } = room?.Exits?.Select(x => new ExitSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get or set the character serializations.
        /// </summary>
        public NonPlayableCharacterSerialization[] Characters { get; set; } = room?.Characters?.Select(x => new NonPlayableCharacterSerialization(x))?.ToArray() ?? [];

        #endregion

        #region Implementation of IObjectSerialization<Room>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="room">The room to restore.</param>
        public void Restore(Room room)
        {
            room.RestoreFrom(this);
        }

        #endregion
    }
}
