using NetAF.Assets.Interaction;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the Exit command.
    /// </summary>
    internal class Exit : ICommand
    {
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

            game.End();
            return new(ReactionResult.OK, "Exiting...");
        }

        #endregion
    }
}