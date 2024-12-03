using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Logic
{
    /// <summary>
    /// Provides a record of the location of a playable character.
    /// </summary>
    /// <param name="playerIdentifier">The player identifier.</param>
    /// <param name="regionIdentifier">The identifier for the region that the player is in.</param>
    /// <param name="roomIdentifier">The identifier for the room that the player is in.</param>
    public class PlayableCharacterLocation(string playerIdentifier, string regionIdentifier, string roomIdentifier) : IRestoreFromObjectSerialization<PlayableCharacterLocationSerialization>
    {
        #region Properties

        /// <summary>
        /// Get the player.
        /// </summary>
        public string PlayerIdentifier { get; private set; } = playerIdentifier;

        /// <summary>
        /// Get the identifier for the region that the player is in.
        /// </summary>
        public string RegionIdentifier { get; private set; } = regionIdentifier;

        /// <summary>
        /// Get the identifier for room that the player is in.
        /// </summary>
        public string RoomIdentifier { get; private set; } = roomIdentifier;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of PlayableCharacterLocation from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The location.</returns>
        public static PlayableCharacterLocation FromSerialization(PlayableCharacterLocationSerialization serialization)
        {
            PlayableCharacterLocation location = new(string.Empty, string.Empty, string.Empty);
            ((IRestoreFromObjectSerialization<PlayableCharacterLocationSerialization>)location).RestoreFrom(serialization);
            return location;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<PlayableCharacterLocationSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<PlayableCharacterLocationSerialization>.RestoreFrom(PlayableCharacterLocationSerialization serialization)
        {
            PlayerIdentifier = serialization.PlayerIdentifier;
            RegionIdentifier = serialization.RegionIdentifier;
            RoomIdentifier = serialization.RoomIdentifier;
        }

        #endregion
    }
}
