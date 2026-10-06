using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the Pan command.
    /// </summary>
    /// <param name="direction">The direction to pan.</param>
    public sealed class Pan(Direction direction) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help for north.
        /// </summary>
        public static CommandHelp NorthCommandHelp { get; } = new("North", "Pan north", CommandCategory.RegionMap, "N", displayAs: "North/N");

        /// <summary>
        /// Get the command help for south.
        /// </summary>
        public static CommandHelp SouthCommandHelp { get; } = new("South", "Pan south", CommandCategory.RegionMap, "S", displayAs: "South/S");

        /// <summary>
        /// Get the command help for east.
        /// </summary>
        public static CommandHelp EastCommandHelp { get; } = new("East", "Pan east", CommandCategory.RegionMap, "E", displayAs: "East/E");

        /// <summary>
        /// Get the command help for west.
        /// </summary>
        public static CommandHelp WestCommandHelp { get; } = new("West", "Pan west", CommandCategory.RegionMap, "W", displayAs: "West/W");

        /// <summary>
        /// Get the command help for up.
        /// </summary>
        public static CommandHelp UpCommandHelp { get; } = new("Up", "Pan up", CommandCategory.RegionMap, "U", displayAs: "Up/U");

        /// <summary>
        /// Get the command help for down.
        /// </summary>
        public static CommandHelp DownCommandHelp { get; } = new("Down", "Pan down", CommandCategory.RegionMap, "D", displayAs: "Down/D");

        /// <summary>
        /// Get the general command help.
        /// </summary>
        private static CommandHelp GeneralCommandHelp { get; } = new($"{NorthCommandHelp.Command}/{SouthCommandHelp.Command}/{EastCommandHelp.Command}/{WestCommandHelp.Command}", "Pan", CommandCategory.RegionMap);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the pan position.
        /// </summary>
        /// <param name="current">The current pan position.</param>
        /// <param name="direction">The direction to pan.</param>
        /// <returns>The modified pan position.</returns>
        public static Point3D GetPanPosition(Point3D current, Direction direction)
        {
            return direction switch
            {
                Direction.North => new Point3D(current.X, current.Y + 1, current.Z),
                Direction.East => new Point3D(current.X + 1, current.Y, current.Z),
                Direction.South => new Point3D(current.X, current.Y - 1, current.Z),
                Direction.West => new Point3D(current.X - 1, current.Y, current.Z),
                Direction.Up => new Point3D(current.X, current.Y, current.Z + 1),
                Direction.Down => new Point3D(current.X, current.Y, current.Z - 1),
                _ => current,
            };
        }

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => GeneralCommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is RegionMapMode regionMapMode)
            {
                var newPosition = GetPanPosition(regionMapMode.FocusPosition, direction);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, newPosition))
                {
                    regionMapMode.FocusPosition = newPosition;
                    return new(ReactionResult.Silent, $"Panned {direction}.");
                }
                else
                {
                    return new(ReactionResult.Silent, $"Could not pan {direction}.");
                }
            }

            return new(ReactionResult.Error, "Not in region map mode.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}
