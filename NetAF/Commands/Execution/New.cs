using NetAF.Logic;

namespace NetAF.Commands.Execution
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
        public static CommandHelp CommandHelp { get; } = new("New", "Start a new game", CommandCategory.Execution);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            GameExecutor.Restart();

            return new(ReactionResult.Silent, "New game.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}