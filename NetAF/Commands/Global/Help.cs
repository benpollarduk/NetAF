using NetAF.Logic.Modes;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Help command.
    /// </summary>
    /// <param name="command">The command to display help for.</param>
    public sealed class Help(CommandHelp command) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Help", "View detailed help for a command", shortcut: "H", displayAs: "Help __", instructions: $"Display help for a specific command. Use {CommandList.CommandHelp.Command} to view a list of commands.");

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

            game.ChangeMode(new HelpMode(command));
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        #endregion
    }
}