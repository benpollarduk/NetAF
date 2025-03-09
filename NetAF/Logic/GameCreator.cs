namespace NetAF.Logic.Callbacks
{
    /// <summary>
    /// Represents the call used for Game creation.
    /// </summary>
    public sealed class GameCreator
    {
        #region Fields

        private readonly GameCreationCallback callback;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameCreator class.
        /// </summary>
        /// <param name="callback">The callback to use to create instances of the game.</param>
        internal GameCreator(GameCreationCallback callback)
        {
            this.callback = callback;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new instance of a game.
        /// </summary>
        /// <returns>The new instance of the game.</returns>
        internal Game Invoke()
        {
            return callback.Invoke();
        }

        #endregion
    }
}