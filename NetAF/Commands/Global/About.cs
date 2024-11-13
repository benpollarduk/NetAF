using NetAF.Assets.Interaction;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the About command.
    /// </summary>
    public class About : ICommand
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

            game.DisplayAbout();

            return new(ReactionResult.Internal, string.Empty);
        }

        #endregion
    }
}