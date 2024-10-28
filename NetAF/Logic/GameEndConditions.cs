namespace NetAF.Logic
{
    /// <summary>
    /// Provides a container for game end conditions.
    /// </summary>
    /// <param name="completionCondition">The condition that determines if the game was completed.</param>
    /// <param name="gameOverCondition">The condition that determines if the game has ended.</param>
    public sealed class GameEndConditions(EndCheck completionCondition, EndCheck gameOverCondition)
    {
        #region StaticProperties

        /// <summary>
        /// Get an end check that returns EndCheckResult.NotEnded.
        /// </summary>
        public static EndCheck NotEnded => (g) => EndCheckResult.NotEnded;

        /// <summary>
        /// Get a value for no end.
        /// </summary>
        public static GameEndConditions NoEnd { get; } = new(NotEnded, NotEnded);

        #endregion

        #region Properties

        /// <summary>
        /// Get the condition that determines if the game was completed.
        /// </summary>
        public EndCheck CompletionCondition { get; private set; } = completionCondition;

        /// <summary>
        /// Get the condition that determines if the game has ended.
        /// </summary>
        public EndCheck GameOverCondition { get; private set; } = gameOverCondition;

        #endregion
    }
}
