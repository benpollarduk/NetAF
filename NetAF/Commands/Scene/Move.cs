using NetAF.Assets.Locations;
using NetAF.Logic;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Move command.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    public sealed class Move(Direction direction) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help for north.
        /// </summary>
        public static CommandHelp NorthCommandHelp { get; } = new("North", "Move north", CommandCategory.Movement, "N", displayAs: "North/N");

        /// <summary>
        /// Get the command help for south.
        /// </summary>
        public static CommandHelp SouthCommandHelp { get; } = new("South", "Move south", CommandCategory.Movement, "S", displayAs: "South/S");

        /// <summary>
        /// Get the command help for east.
        /// </summary>
        public static CommandHelp EastCommandHelp { get; } = new("East", "Move east", CommandCategory.Movement, "E", displayAs: "East/E");

        /// <summary>
        /// Get the command help for west.
        /// </summary>
        public static CommandHelp WestCommandHelp { get; } = new("West", "Move west", CommandCategory.Movement, "W", displayAs: "West/W");

        /// <summary>
        /// Get the command help for up.
        /// </summary>
        public static CommandHelp UpCommandHelp { get; } = new("Up", "Move up", CommandCategory.Movement, "U", displayAs: "Up/U");

        /// <summary>
        /// Get the command help for down.
        /// </summary>
        public static CommandHelp DownCommandHelp { get; } = new("Down", "Move down", CommandCategory.Movement, "D", displayAs: "Down/D");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            var region = game.Overworld.CurrentRegion;
            var targetRoom = region.GetAdjoiningRoom(direction);
            var movingToPreviouslyUnvisitedRoom = targetRoom != null && !targetRoom.HasBeenVisited;

            if (region.Move(direction))
            {
                var introduction = targetRoom?.Introduction?.GetDescription() ?? string.Empty;

                if (movingToPreviouslyUnvisitedRoom && !string.IsNullOrEmpty(introduction))
                    return new(ReactionResult.Inform, introduction);

                return new(ReactionResult.Silent, $"Moved {direction}.");
            }

            return new(ReactionResult.Error, $"Could not move {direction}.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
