using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;

namespace NetAF.Commands.Game
{
    /// <summary>
    /// Represents the Move command.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    public class Move(Direction direction) : ICommand
    {
        #region Constants

        /// <summary>
        /// Get the prefix for successful moves.
        /// </summary>
        public const string SuccessfulMovePrefix = "Moved";

        #endregion

        #region Properties

        /// <summary>
        /// Get the direction.
        /// </summary>
        public Direction Direction { get; } = direction;

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

            if (game.Overworld.CurrentRegion.Move(Direction))
                return new(ReactionResult.OK, $"{SuccessfulMovePrefix} {Direction}.");

            return new(ReactionResult.Error, $"Could not move {Direction}.");
        }

        #endregion
    }
}
