using NetAF.Logic;

namespace NetAF.Commands.Execution
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
        public static CommandHelp CommandHelp { get; } = new("Exit", "Exit the game", CommandCategory.Execution);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            GameExecutor.CancelExecution();
            return new(ReactionResult.Silent, "Exiting...");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}