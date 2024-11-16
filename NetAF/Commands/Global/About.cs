using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Global
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
        public static CommandHelp CommandHelp { get; } = new("About", "View information about the games creator");

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

            game.ChangeMode(new AboutMode());
            return new(ReactionResult.GameModeChanged, string.Empty);
        }

        #endregion
    }
}