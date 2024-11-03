using NetAF.Logic;
using NetAF.Serialization.Assets;
using System.Linq;

namespace NetAF.Serialization
{
    /// <summary>
    /// Represents a serialization of a Game.
    /// </summary>
    /// <param name="game">The game to serialize.</param>
    public class GameSerialization(Game game) : IObjectSerialization<Game>
    {
        #region Properties

        /// <summary>
        /// Get or set the active player identifier.
        /// </summary>
        public string ActivePlayerIdentifier { get; set; } = game?.Player?.Identifier?.IdentifiableName;

        /// <summary>
        /// Get or set the player serializations.
        /// </summary>
        public CharacterSerialization[] Players { get; set; } = game?.Catalog?.Players?.Select(x => new CharacterSerialization(x)).ToArray() ?? [];

        /// <summary>
        /// Get or set the overworld serialization.
        /// </summary>
        public OverworldSerialization Overworld { get; set; } = new(game?.Overworld);

        /// <summary>
        /// Get or set the overworld serialization.
        /// </summary>
        public PlayableCharacterLocationSerialization[] InactivePlayerLocations { get; set; } = game?.GetInactivePlayerLocations().Select(x => new PlayableCharacterLocationSerialization(x)).ToArray() ?? [];

        #endregion

        #region Implementation of IObjectSerialization<Game>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="game">The asset to restore.</param>
        public void Restore(Game game)
        {
            game.RestoreFrom(this);
        }

        #endregion
    }
}
