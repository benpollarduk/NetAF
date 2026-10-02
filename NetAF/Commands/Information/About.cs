using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Information
{
    /// <summary>
    /// Represents the About command.
    /// </summary>
    public sealed class About : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("About", "View information about the games creator", CommandCategory.Information);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            game.ChangeMode(new AboutMode());
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}