namespace NetAF.Logic
{
    /// <summary>
    /// Provides a container for game end conditions.
    /// </summary>
    public sealed class GameEndConditions
    {
        #region StaticProperties

        /// <summary>
        /// Get an end check that returns EndCheckResult.NotEnded.
        /// </summary>
        public static EndCheck NotEnded => (g) => EndCheckResult.NotEnded;

        /// <summary>
        /// Get a value for no end.
        /// </summary>
        public static GameEndConditions NoEnd { get; } = new GameEndConditions(NotEnded, NotEnded);

        #endregion

        #region Properties

        /// <summary>
        /// Get the condition that determines if the game was completed.
        /// </summary>
        public EndCheck CompletionCondition { get; private set; }

        /// <summary>
        /// Get the condition that determines if the game has ended.
        /// </summary>
        public EndCheck GameOverCondition { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameEndConditions class.
        /// </summary>
        /// <param name="completionCondition">The condition that determines if the game was completed.</param>
        /// <param name="gameOverCondition">The condition that determines if the game has ended.</param>
        public GameEndConditions(EndCheck completionCondition, EndCheck gameOverCondition)
        {
            CompletionCondition = completionCondition;
            GameOverCondition = gameOverCondition;
        }

        #endregion
    }
}
