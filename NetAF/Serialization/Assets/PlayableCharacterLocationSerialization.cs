using NetAF.Logic;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a PlayableCharacterLocation.
    /// </summary>
    public sealed class PlayableCharacterLocationSerialization : IObjectSerialization<PlayableCharacterLocation>
    {
        #region Properties

        /// <summary>
        /// Get or set the player identifier.
        /// </summary>
        public string PlayerIdentifier { get; set; }

        /// <summary>
        /// Get or set the region identifier.
        /// </summary>
        public string RegionIdentifier { get; set; }

        /// <summary>
        /// Get or set the room identifier.
        /// </summary>
        public string RoomIdentifier { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a PlayableCharacterLocation.
        /// </summary>
        /// <param name="playableCharacterLocation">The PlayableCharacterLocation to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static PlayableCharacterLocationSerialization FromPlayableCharacterLocation(PlayableCharacterLocation playableCharacterLocation)
        {
            return new()
            {
                PlayerIdentifier = playableCharacterLocation?.PlayerIdentifier,
                RegionIdentifier = playableCharacterLocation?.RegionIdentifier,
                RoomIdentifier = playableCharacterLocation?.RoomIdentifier
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<PlayableCharacterLocation>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="location">The attribute to restore.</param>
        void IObjectSerialization<PlayableCharacterLocation>.Restore(PlayableCharacterLocation location)
        {
            ((IRestoreFromObjectSerialization<PlayableCharacterLocationSerialization>)location).RestoreFrom(this);
        }

        #endregion
    }
}
