using NetAF.Assets.Locations;
using NetAF.Logic;

namespace NetAF.Commands.Movement
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
        public static CommandHelp NorthCommandHelp { get; } = new("North", "Move north", CommandCategory.Movement, "N", displayAs: "North/N", synonyms: ["Ahead", "Forward", "Forwards"]);

        /// <summary>
        /// Get the command help for south.
        /// </summary>
        public static CommandHelp SouthCommandHelp { get; } = new("South", "Move south", CommandCategory.Movement, "S", displayAs: "South/S", synonyms: ["Back", "Backward", "Backwards"]);

        /// <summary>
        /// Get the command help for east.
        /// </summary>
        public static CommandHelp EastCommandHelp { get; } = new("East", "Move east", CommandCategory.Movement, "E", displayAs: "East/E", synonyms: ["Right"]);

        /// <summary>
        /// Get the command help for west.
        /// </summary>
        public static CommandHelp WestCommandHelp { get; } = new("West", "Move west", CommandCategory.Movement, "W", displayAs: "West/W", synonyms: ["Left"]);

        /// <summary>
        /// Get the command help for up.
        /// </summary>
        public static CommandHelp UpCommandHelp { get; } = new("Up", "Move up", CommandCategory.Movement, "U", displayAs: "Up/U", synonyms: ["Above", "Ascend", "Upwards"]);

        /// <summary>
        /// Get the command help for down.
        /// </summary>
        public static CommandHelp DownCommandHelp { get; } = new("Down", "Move down", CommandCategory.Movement, "D", displayAs: "Down/D", synonyms: ["Below", "Descend", "Downwards"]);

        /// <summary>
        /// Get the general command help.
        /// </summary>
        private static CommandHelp GeneralCommandHelp { get; } = new($"{NorthCommandHelp.Command}/{SouthCommandHelp.Command}/{EastCommandHelp.Command}/{WestCommandHelp.Command}", "Move", CommandCategory.Movement);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => GeneralCommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            var region = game.Overworld.CurrentRegion;
            var targetRoom = region.GetAdjoiningRoom(direction);
            var movingToPreviouslyUnvisitedRoom = targetRoom != null && !targetRoom.HasBeenVisited;

            var reaction = region.Move(direction);

            switch (reaction.Result)
            {
                case ReactionResult.Silent:

                    var introduction = targetRoom?.Introduction?.GetDescription() ?? string.Empty;

                    if (movingToPreviouslyUnvisitedRoom && !string.IsNullOrEmpty(introduction))
                        return new(ReactionResult.Inform, introduction);

                    return new(ReactionResult.Silent, $"Moved {direction}.");

                default:

                    return reaction;
            }
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
