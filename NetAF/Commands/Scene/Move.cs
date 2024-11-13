using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;

namespace NetAF.Commands.Scene
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

        #region StaticProperties

        /// <summary>
        /// Get the command help for north.
        /// </summary>
        public static CommandHelp NorthCommandHelp { get; } = new("North", "Move north", "N");

        /// <summary>
        /// Get the command help for south.
        /// </summary>
        public static CommandHelp SouthCommandHelp { get; } = new("South", "Move south", "S");

        /// <summary>
        /// Get the command help for east.
        /// </summary>
        public static CommandHelp EastCommandHelp { get; } = new("East", "Move east", "E");

        /// <summary>
        /// Get the command help for west.
        /// </summary>
        public static CommandHelp WestCommandHelp { get; } = new("West", "Move west", "W");

        /// <summary>
        /// Get the command help for up.
        /// </summary>
        public static CommandHelp UpCommandHelp { get; } = new("Up", "Move up", "U");

        /// <summary>
        /// Get the command help for down.
        /// </summary>
        public static CommandHelp DownCommandHelp { get; } = new("Down", "Move down", "D");

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
