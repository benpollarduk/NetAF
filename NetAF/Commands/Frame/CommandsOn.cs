using NetAF.Assets.Interaction;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOn command.
    /// </summary>
    internal class CommandsOn : ICommand
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

            game.Configuration.DisplayCommandListInSceneFrames = true;
            return new(ReactionResult.OK, "Commands have been turned on.");
        }

        #endregion
    }
}