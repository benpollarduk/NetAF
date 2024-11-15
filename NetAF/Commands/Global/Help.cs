using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Help command.
    /// </summary>
    public sealed class Help : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Help", "View game help");

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

            List<CommandHelp> commands =
            [
                .. game.Configuration.Interpreter.SupportedCommands,
                .. game.Configuration.Interpreter.GetContextualCommandHelp(game),
                .. game.Mode.Interpreter?.SupportedCommands ?? [],
                .. game.Mode.Interpreter?.GetContextualCommandHelp(game) ?? [],
            ];

            game.ChangeMode(new HelpMode([.. commands.Distinct()]));
            return new(ReactionResult.ModeChanged, string.Empty);
        }

        #endregion
    }
}