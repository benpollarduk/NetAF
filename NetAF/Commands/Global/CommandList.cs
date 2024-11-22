using NetAF.Logic.Modes;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Commands command.
    /// </summary>
    public sealed class CommandList : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Commands", "View a list of commands");

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

            game.ChangeMode(new CommandListMode(game.GetContextualCommands()));
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        #endregion
    }
}