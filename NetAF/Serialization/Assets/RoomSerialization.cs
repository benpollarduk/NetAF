using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Room.
    /// </summary>
    /// <param name="room">The room to serialize.</param>
    public class RoomSerialization(Room room) : ExaminableSerialization(room), IObjectSerialization<Room>
    {
        #region Properties

        /// <summary>
        /// Get if the room has been visited.
        /// </summary>
        public readonly bool HasBeenVisited = room.HasBeenVisited;

        /// <summary>
        /// Get the item serializations.
        /// </summary>
        public readonly ItemSerialization[] Items = room?.Items?.Select(x => new ItemSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get the exit serializations.
        /// </summary>
        public readonly ExitSerialization[] Exits = room?.Exits?.Select(x => new ExitSerialization(x))?.ToArray() ?? [];

        /// <summary>
        /// Get the character serializations.
        /// </summary>
        public readonly CharacterSerialization[] Characters = room?.Characters?.Select(x => new CharacterSerialization(x))?.ToArray() ?? [];

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
