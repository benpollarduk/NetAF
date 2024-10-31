using NetAF.Logic;
using NetAF.Serialization.Assets;

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
        /// Get or set the player serialization.
        /// </summary>
        public CharacterSerialization Player { get; set; } = new(game?.Player);

        /// <summary>
        /// Get or set the overworld serialization.
        /// </summary>
        public OverworldSerialization Overworld { get; set; } = new(game?.Overworld);

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
