﻿using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Logic.Modes;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the Pan command.
    /// </summary>
    /// <param name="direction">The direction to pan.</param>
    public class Pan(Direction direction) : ICommand
    {
        #region Constants

        /// <summary>
        /// Get the string for successful pan.
        /// </summary>
        public const string SuccessfulPan = "Panned.";

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
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Mode is RegionMapMode regionMapMode)
            {
                var newPosition = GetPanPosition(regionMapMode.FocusPosition, direction);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, newPosition))
                {
                    regionMapMode.FocusPosition = newPosition;
                    return new(ReactionResult.Silent, SuccessfulPan);
                }
                else
                {
                    return new(ReactionResult.Silent, $"Could not pan {direction}.");
                }
            }

            return new(ReactionResult.Error, "Not in region map mode.");
        }

        #endregion
    }
}