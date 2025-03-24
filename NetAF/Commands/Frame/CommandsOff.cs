using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the Commands Off command.
    /// </summary>
    public sealed class CommandsOff : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Commands Off", "Turn commands off", CommandCategory.Frame);

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

            SceneMode.DisplayCommandList = false;
            return new(ReactionResult.Inform, "Commands have been turned off.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}