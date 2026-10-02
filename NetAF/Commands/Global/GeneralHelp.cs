using NetAF.Logic;
using NetAF.Logic.Modes;
using System.Linq;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the GeneralHelp command.
    /// </summary>
    /// <param name="command">The command to display help for.</param>
    /// <param name="prompts">The prompts to display for the command.</param>
    public sealed class GeneralHelp(CommandHelp command, Prompt[] prompts) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Help", "View detailed help for a command", CommandCategory.Global, shortcut: "H", displayAs: "Help __", instructions: $"Display detailed help for a specific command. Use the '{CommandList.CommandHelp.Command}' command to view a list of commands.");

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            game.ChangeMode(new HelpMode(command, prompts));
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            if (game == null)
                return [];

            var commands = game.GetContextualCommands();
            return [..commands.Select(x => new Prompt(x.Command))];
        }

        #endregion
    }
}
