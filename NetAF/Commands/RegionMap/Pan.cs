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
        public static CommandHelp NorthCommandHelp { get; } = new("North", "Pan north", CommandCategory.Scene, "N", displayAs: "North/N");

        /// <summary>
        /// Get the command help for south.
        /// </summary>
        public static CommandHelp SouthCommandHelp { get; } = new("South", "Pan south", CommandCategory.Scene, "S", displayAs: "South/S");

        /// <summary>
        /// Get the command help for east.
        /// </summary>
        public static CommandHelp EastCommandHelp { get; } = new("East", "Pan east", CommandCategory.Scene, "E", displayAs: "East/E");

        /// <summary>
        /// Get the command help for west.
        /// </summary>
        public static CommandHelp WestCommandHelp { get; } = new("West", "Pan west", CommandCategory.Scene, "W", displayAs: "West/W");

        /// <summary>
        /// Get the command help for up.
        /// </summary>
        public static CommandHelp UpCommandHelp { get; } = new("Up", "Pan up", CommandCategory.Scene, "U", displayAs: "Up/U");

        /// <summary>
        /// Get the command help for down.
        /// </summary>
        public static CommandHelp DownCommandHelp { get; } = new("Down", "Pan down", CommandCategory.Scene, "D", displayAs: "Down/D");

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
            switch (direction)
            {
                case Direction.North:
                    return new Point3D(current.X, current.Y + 1, current.Z);
                case Direction.East:
                    return new Point3D(current.X + 1, current.Y, current.Z);
                case Direction.South:
                    return new Point3D(current.X, current.Y - 1, current.Z);
                case Direction.West:
                    return new Point3D(current.X - 1, current.Y, current.Z);
                case Direction.Up:
                    return new Point3D(current.X, current.Y, current.Z + 1);
                case Direction.Down:
                    return new Point3D(current.X, current.Y, current.Z - 1);
                default:
                    return current;
            }
        }

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
