using NetAF.Assets.Locations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Room.
    /// </summary>
    public sealed class RoomSerialization : ExaminableSerialization, IObjectSerialization<Room>
    {
        #region Properties

        /// <summary>
        /// Get or set if the room has been visited.
        /// </summary>
        public bool HasBeenVisited { get; set; }

        /// <summary>
        /// Get or set the item serializations.
        /// </summary>
        public ItemSerialization[] Items { get; set; }

        /// <summary>
        /// Get or set the exit serializations.
        /// </summary>
        public ExitSerialization[] Exits { get; set; }

        /// <summary>
        /// Get or set the character serializations.
        /// </summary>
        public NonPlayableCharacterSerialization[] Characters { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a Room.
        /// </summary>
        /// <param name="room">The Room to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static RoomSerialization FromRoom(Room room)
        {
            return new()
            {
                Identifier = room?.Identifier?.IdentifiableName,
                IsPlayerVisible = room?.IsPlayerVisible ?? false,
                AttributeManager = AttributeManagerSerialization.FromAttributeManager(room?.Attributes),
                Commands = room?.Commands?.Select(CustomCommandSerialization.FromCustomCommand).ToArray() ?? [],
                HasBeenVisited = room?.HasBeenVisited ?? false,
                Items = room?.Items?.Select(ItemSerialization.FromItem).ToArray() ?? [],
                Exits = room?.Exits?.Select(ExitSerialization.FromExit).ToArray() ?? [],
                Characters = room?.Characters?.Select(NonPlayableCharacterSerialization.FromNonPlayableCharacter).ToArray() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Room>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="room">The room to restore.</param>
        public void Restore(Room room)
        {
            ((IRestoreFromObjectSerialization<RoomSerialization>)room).RestoreFrom(this);
        }

        #endregion
    }
}
