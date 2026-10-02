using NetAF.Logic;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the End command.
    /// </summary>
    public sealed class End : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("End", "End the current mode", CommandCategory.Global);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            game.NextMode();
            return new(ReactionResult.Silent, "Ended.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}