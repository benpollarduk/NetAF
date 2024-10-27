namespace NetAF.Logic
{
    /// <summary>
    /// Represents a collection of asset generators required to create a game.
    /// </summary>
    public class GameAssetGenerators
    {
        #region Properties

        /// <summary>
        /// Get the overworld generator.
        /// </summary>
        public OverworldCreationCallback OverworldGenerator { get; private set; }

        /// <summary>
        /// Get the player generator.
        /// </summary>
        public PlayerCreationCallback PlayerGenerator { get; private set;  }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameAssetGenerators class.
        /// </summary>
        /// <param name="overworldGenerator">The overworld generator.</param>
        /// <param name="playerGenerator">The player generator.</param>
        public GameAssetGenerators(OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator)
        {
            OverworldGenerator = overworldGenerator;
            PlayerGenerator = playerGenerator;
        }

        #endregion
    }
}
