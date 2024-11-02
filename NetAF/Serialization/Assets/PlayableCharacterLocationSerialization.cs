using NetAF.Logic;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a PlayableCharacterLocation.
    /// </summary>
    /// <param name="location">The location to serialize.</param>
    public sealed class PlayableCharacterLocationSerialization(PlayableCharacterLocation location) : IObjectSerialization<PlayableCharacterLocation>
    {
        #region Properties

        /// <summary>
        /// Get or set the player identifier.
        /// </summary>
        public string PlayerIdentifier { get; set; } = location.PlayerIdentifier;

        /// <summary>
        /// Get or set the region identifier.
        /// </summary>
        public string RegionIdentifier { get; set; } = location.RegionIdentifier;

        /// <summary>
        /// Get or set the room identifier.
        /// </summary>
        public string RoomIdentifier { get; set; } = location.RoomIdentifier;

        #endregion

        #region Implementation of IObjectSerialization<PlayableCharacterLocation>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="location">The attribute to restore.</param>
        public void Restore(PlayableCharacterLocation location)
        {
            location.RestoreFrom(this);
        }

        #endregion
    }
}
