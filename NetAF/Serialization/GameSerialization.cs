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
        /// Get the player serialization.
        /// </summary>
        public readonly CharacterSerialization Player = new(game.Player);

        /// <summary>
        /// Get the overworld serialization.
        /// </summary>
        public readonly OverworldSerialization Overworld = new(game.Overworld);

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
