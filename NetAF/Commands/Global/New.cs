using NetAF.Logic;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the New command.
    /// </summary>
    public sealed class New : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("New", "Start a new game");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            GameExecutor.Restart();

            return new(ReactionResult.Silent, "New game.");
        }

        #endregion
    }
}