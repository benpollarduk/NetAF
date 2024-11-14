using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the End command.
    /// </summary>
    public class End : ICommand
    {
        #region Constants

        /// <summary>
        /// Get the string for successful ended.
        /// </summary>
        public const string SuccessfulEnd = "Ended.";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("End", "End the current mode");

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

            game.ChangeMode(new SceneMode());
            return new(ReactionResult.OK, SuccessfulEnd);
        }

        #endregion
    }
}