using NetAF.Logic;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Exit command.
    /// </summary>
    public sealed class Exit : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Exit", "Exit the game");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            GameExecutor.CancelExecution();
            return new(ReactionResult.Silent, "Exiting...");
        }

        #endregion
    }
}